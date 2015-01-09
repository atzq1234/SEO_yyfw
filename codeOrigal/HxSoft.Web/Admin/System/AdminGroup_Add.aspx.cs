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

namespace HxSoft.Web.Admin._System
{
    public partial class AdminGroup_Add : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        public string AdminGroupID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["AdminGroupID"], 0).ToString();
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
        public string strAdminGroupName
        {
            get
            {
                return Config.Request(Request["txtAdminGroupName"], "");
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
                if (strAdminGroupName != "") TempSql.Append(" and AdminGroupName like @AdminGroupName");
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
                if (strAdminGroupName != "") listParams.Add(Config.Conn().CreateDbParameter("@AdminGroupName", "%" + strAdminGroupName + "%"));
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
                TempUrl.Append("txtAdminGroupName=" + Server.UrlEncode(strAdminGroupName) + "&");
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
                if (AdminGroupID == "0")
                {
                    GetData.LimitChkMsg("AdminGroupAdd");
                    lblTitle.Text = "���";
                    string strListID = Factory.AdminGroup().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("AdminGroupEdit");
                    lblTitle.Text = "�޸�";
                    ShowInfo();
                }
            }
        }
        //��������
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdminGroupModel admGrModel = new AdminGroupModel();
            string strOldListID = hidlistID.Value;
            admGrModel.AdminGroupName = txtAdminGroupName.Text.Trim();
            admGrModel.LimitValues = "";
            admGrModel.ListID = txtListID.Text.Trim();
            admGrModel.AdminID = Session["AdminID"].ToString();
            admGrModel.AddTime = DateTime.Now.ToString();
            admGrModel.IsClose = radIsClose.SelectedValue;
            if (AdminGroupID == "0")
            {
                if (!Factory.AdminGroup().CheckInfo("AdminGroupName", admGrModel.AdminGroupName))
                {
                    Factory.AdminGroup().OrderInfo(admGrModel.ListID, strOldListID);
                    Factory.AdminGroup().InsertInfo(admGrModel);
                    Factory.AdminLog().InsertLog("�������Ϊ\"" + admGrModel.AdminGroupName + "\"�Ĺ����顣", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("��ӳɹ���", "AdminGroup.aspx");
                }
                else
                {
                    errMsg.Text = "�Ѵ�����ͬ������!";
                }
            }
            else
            {
                if (!Factory.AdminGroup().CheckInfo("AdminGroupName", admGrModel.AdminGroupName, AdminGroupID))
                {
                    AdminGroupModel admGrModel_2 = new AdminGroupModel();
                    admGrModel_2 = Factory.AdminGroup().GetInfo(AdminGroupID);
                    if (admGrModel_2 != null)
                    {
                        if (GetData.CheckAdminID(admGrModel_2.AdminID, "AdminGroupAll"))//��鴴����
                        {
                            Factory.AdminGroup().OrderInfo(admGrModel.ListID, strOldListID);
                            Factory.AdminGroup().UpdateInfo(admGrModel, AdminGroupID);
                            Factory.AdminLog().InsertLog("�޸ı��Ϊ" + AdminGroupID + "�Ĺ����顣", Session["AdminID"].ToString());
                            Config.MsgGotoUrl("�޸ĳɹ���", "AdminGroup.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                        }
                    }
                }
                else
                {
                    errMsg.Text = "�Ѵ�����ͬ������!";
                }
            }
        }
        //��ʾ����
        protected void ShowInfo()
        {
            AdminGroupModel admGrModel = new AdminGroupModel();
            admGrModel = Factory.AdminGroup().GetInfo(AdminGroupID);
            if (admGrModel != null)
            {
                if (GetData.CheckAdminID(admGrModel.AdminID, "AdminGroupAll"))//��鴴����
                {
                    txtAdminGroupName.Text = admGrModel.AdminGroupName;
                    //txtLimitValues.Text = admGrModel.LimitValues;
                    txtListID.Text = admGrModel.ListID;
                    hidlistID.Value = admGrModel.ListID;
                    //txtAdminID.Text = admGrModel.AdminID;
                    //txtAddTime.Text = admGrModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, admGrModel.IsClose);
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
