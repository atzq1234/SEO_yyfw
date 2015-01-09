using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;
using System.Collections.Generic;
using System.Data.Common;

namespace HxSoft.Web.Admin.User
{
    public partial class UserRank_Add : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        public string UserRankID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["UserRankID"], 0).ToString();
            }
        }
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        #region ****�������****
        public string strOrderKey
        {
            get
            {
                return Config.Request(Request["OrderKey"], "ListID");
            }
        }
        public string strAscDesc1
        {
            get
            {
                return Config.Request(Request["AscDesc"], "asc");
            }
        }
        public string strAscDesc2
        {
            get
            {
                if (strAscDesc1 == "asc")
                    return "desc";
                else
                    return "asc";
            }
        }
        #endregion
        #region ****�������****
        public string SqlOrder
        {
            get
            {
                return " order by " + strOrderKey + " " + strAscDesc1;
            }
        }
        #endregion
        #region ****Url�������****
        public string UrlOrderPara
        {
            get
            {
                StringBuilder TempUrl = new StringBuilder("");
                TempUrl.Append("OrderKey=" + Server.UrlEncode(strOrderKey) + "&");
                TempUrl.Append("AscDesc=" + Server.UrlEncode(strAscDesc1) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        #region ****��ѯ����****
        public string strUserRankName
        {
            get
            {
                return Config.Request(Request["txtUserRankName"], "");
            }
        }
        public string strIsClose
        {
            get
            {
                return Config.Request(Request["radIsClose"], "-1");
            }
        }
        #endregion
        #region ****��ѯ���****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (strUserRankName != "") TempSql.Append(" and UserRankName like @UserRankName");
                if (strIsClose != "-1") TempSql.Append(" and IsClose =@IsClose");
                return TempSql.ToString();
            }
        }
        #endregion
        #region****DbParameter����****
        public DbParameter[] SqlParams
        {
            get
            {
                List<DbParameter> listParams = new List<DbParameter>();
                if (strUserRankName != "") listParams.Add(Config.Conn().CreateDbParameter("@UserRankName", "%" + strUserRankName + "%"));
                if (strIsClose != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsClose", strIsClose));
                return listParams.ToArray();
            }
        }
        #endregion
        #region ****Url����****
        public string UrlPara
        {
            get
            {
                StringBuilder TempUrl = new StringBuilder("");
                TempUrl.Append("txtUserRankName=" + Server.UrlEncode(strUserRankName) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //ҳ���ʼ��
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            if (!Page.IsPostBack)
            {
                if (UserRankID == "0")
                {
                    GetData.LimitChkMsg("UserRankAdd");
                    lblTitle.Text = "���";
                    string strListID = Factory.UserRank().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("UserRankEdit");
                    lblTitle.Text = "�޸�";
                    ShowInfo();
                }
            }
        }
        //��������
        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserRankModel userRankModel = new UserRankModel();
            string strOldListID = hidlistID.Value;
            userRankModel.UserRankName = txtUserRankName.Text.Trim();
            userRankModel.LimitValues = "";
            userRankModel.ListID = txtListID.Text.Trim();
            userRankModel.AdminID = Session["AdminID"].ToString();
            userRankModel.AddTime = DateTime.Now.ToString();
            userRankModel.IsClose = radIsClose.SelectedValue;
            if (UserRankID == "0")
            {
                if (!Factory.UserRank().CheckInfo("UserRankName", userRankModel.UserRankName))
                {
                    Factory.UserRank().OrderInfo(userRankModel.ListID, strOldListID);
                    Factory.UserRank().InsertInfo(userRankModel);
                    Factory.AdminLog().InsertLog("�������Ϊ\"" + userRankModel.UserRankName + "\"�Ļ�Ա����", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("��ӳɹ���", "UserRank.aspx");
                }
                else
                {
                    errMsg.Text = "�Ѵ�����ͬ��Ա����!";
                }
            }
            else
            {
                if (!Factory.UserRank().CheckInfo("UserRankName", userRankModel.UserRankName, UserRankID))
                {
                    UserRankModel userRankModel_2 = new UserRankModel();
                    userRankModel_2 = Factory.UserRank().GetInfo(UserRankID);
                    if (userRankModel_2 != null)
                    {
                        if (GetData.CheckAdminID(userRankModel_2.AdminID, "UserRankAll"))//��鴴����
                        {
                            Factory.UserRank().OrderInfo(userRankModel.ListID, strOldListID);
                            Factory.UserRank().UpdateInfo(userRankModel, UserRankID);
                            Factory.AdminLog().InsertLog("�޸ı��Ϊ" + UserRankID + "�Ļ�Ա����", Session["AdminID"].ToString());
                            Config.MsgGotoUrl("�޸ĳɹ���", "UserRank.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                        }
                    }
                }
                else
                {
                    errMsg.Text = "�Ѵ�����ͬ��Ա����!";
                }
            }
        }
        //��ʾ����
        protected void ShowInfo()
        {
            UserRankModel userRankModel = new UserRankModel();
            userRankModel = Factory.UserRank().GetInfo(UserRankID);
            if (userRankModel != null)
            {
                if (GetData.CheckAdminID(userRankModel.AdminID, "UserRankAll"))//��鴴����
                {
                    txtUserRankName.Text = userRankModel.UserRankName;
                    //txtLimitValues.Text = userRankModel.LimitValues;
                    txtListID.Text = userRankModel.ListID;
                    hidlistID.Value = userRankModel.ListID;
                    //txtAdminID.Text = userRankModel.AdminID;
                    //txtAddTime.Text = userRankModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, userRankModel.IsClose);
                }
                else
                {
                    Config.ShowEnd("��û�в鿴����Ϣ��Ȩ��");
                }
            }
            else
            {
                Config.ShowEnd("��û�в鿴����Ϣ��Ȩ�ޣ�");
            }
        }
    }
}
