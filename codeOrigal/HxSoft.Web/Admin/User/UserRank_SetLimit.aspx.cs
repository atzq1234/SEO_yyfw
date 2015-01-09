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
    public partial class UserRank_SetLimit : System.Web.UI.Page
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
                GetData.LimitChkMsg("UserRankSetLimit");
                if (UserRankID == "0")
                {
                    Config.ShowEnd("��ѡ��Ҫ�����ļ�¼!");
                }
                else
                {
                    ShowInfo();
                }
            }
        }
        //��������
        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserRankModel userRankModel = new UserRankModel();
            //***
            if (Request.Form["LimitValue"] == null)
            {
                userRankModel.LimitValues = "-1,-1";
            }
            else
            {
                string strLimitValue = Config.HTMLClear(Request.Form["LimitValue"].ToString());
                if (strLimitValue == string.Empty)
                {
                    userRankModel.LimitValues = "-1,-1";
                }
                else
                {
                    userRankModel.LimitValues = "-1," + strLimitValue + ",-1";
                }
            }
            //***

            UserRankModel userRankModel_2 = new UserRankModel();
            userRankModel_2 = Factory.UserRank().GetInfo(UserRankID);
            if (userRankModel_2 != null)
            {
                if (GetData.CheckAdminID(userRankModel_2.AdminID, "UserRankAll"))//��鴴����
                {
                    Factory.UserRank().SetLimit(userRankModel, UserRankID);
                    Factory.AdminLog().InsertLog("���û�Ա������Ϊ" + UserRankID + "�Ĳ���Ȩ�ޡ�", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("����ɹ���", "UserRank.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                }
            }
        }
        //��ʾ����
        protected void ShowInfo()
        {
            //��Ա����
            UserRankModel userRankModel = new UserRankModel();
            userRankModel = Factory.UserRank().GetInfo(UserRankID);
            if (userRankModel != null)
            {
                if (GetData.CheckAdminID(userRankModel.AdminID, "UserRankAll"))//��鴴����
                {
                    lblUserRankName.Text = userRankModel.UserRankName;
                }
                else
                {
                    Config.ShowEnd("��û�в鿴����Ϣ��Ȩ�ޣ�");
                }
            }
            else
            {
                Config.ShowEnd("��û�в鿴����Ϣ��Ȩ�ޣ�");
            }

            //����Ȩ���б�
            DataList1.DataKeyField = "LimitID";
            Factory.Acc().DataBind("select * from t_Limit where ParentID=" + Config.SysUserLimitMouldID + " and IsClose=0 order by ListID asc", null,Config.DataBindObjTypeCollection.DataList.ToString(), DataList1);
        }

        //��Ȩ���б�
        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            string strParentID = DataList1.DataKeys[e.Item.ItemIndex].ToString();
            DataList DataList2 = (DataList)e.Item.FindControl("DataList2");
            Factory.Acc().DataBind("select * from t_Limit where ParentID=" + strParentID + " and IsClose=0 order by ListID asc", null,Config.DataBindObjTypeCollection.DataList.ToString(), DataList2);
        }
    }
}
