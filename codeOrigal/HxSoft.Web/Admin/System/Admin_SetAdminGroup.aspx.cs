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
    public partial class Admin_SetAdminGroup : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        public string AdminID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["AdminID"], 0).ToString();
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
                return Config.Request(Request["OrderKey"], "AdminID");
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
        public string strAdminName
        {
            get
            {
                return Config.Request(Request["txtAdminName"], "");
            }
        }
        public string strAdminGroupID
        {
            get
            {
                return Config.Request(Request["drpAdminGroupID"], "-1");
            }
        }
        public string strRealName
        {
            get
            {
                return Config.Request(Request["txtRealName"], "");
            }
        }
        public string strEmail
        {
            get
            {
                return Config.Request(Request["txtEmail"], "");
            }
        }
        public string strDepartment
        {
            get
            {
                return Config.Request(Request["txtDepartment"], "");
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
                if (strAdminName != "") TempSql.Append(" and AdminName like @AdminName");
                if (strAdminGroupID != "-1") TempSql.Append(" and AdminID in(select AdminID from t_AdminInGroup where AdminGroupID=@AdminGroupID)");
                if (strRealName != "") TempSql.Append(" and RealName like @RealName");
                if (strEmail != "") TempSql.Append(" and Email like @Email");
                if (strDepartment != "") TempSql.Append(" and Department like @Department");
                if (strIsClose != "-1") TempSql.Append(" and IsClose=@IsClose");
                if (Session["AdminID"].ToString() != Config.SystemAdminID) TempSql.Append(" and AdminID<>" + Config.SystemAdminID);
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
                if (strAdminName != "") listParams.Add(Config.Conn().CreateDbParameter("@AdminName", "%" + strAdminName + "%"));
                if (strAdminGroupID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@AdminGroupID", strAdminGroupID));
                if (strRealName != "") listParams.Add(Config.Conn().CreateDbParameter("@RealName", "%" + strRealName + "%"));
                if (strEmail != "") listParams.Add(Config.Conn().CreateDbParameter("@Email", "%" + strEmail + "%"));
                if (strDepartment != "") listParams.Add(Config.Conn().CreateDbParameter("@Department", "%" + strDepartment + "%"));
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
                TempUrl.Append("txtAdminName=" + Server.UrlEncode(strAdminName) + "&");
                TempUrl.Append("drpAdminGroupID=" + Server.UrlEncode(strAdminGroupID) + "&");
                TempUrl.Append("txtRealName=" + Server.UrlEncode(strRealName) + "&");
                TempUrl.Append("txtEmail=" + Server.UrlEncode(strEmail) + "&");
                TempUrl.Append("txtDepartment=" + Server.UrlEncode(strDepartment) + "&");
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
                GetData.LimitChkMsg("AdminSetAdminGroup");
                if (AdminID == "0")
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
            AdminInGroupModel admInGrModel = new AdminInGroupModel();
            admInGrModel.AdminID = AdminID;
            admInGrModel.AdminGroupID = drpAdminGroupID.SelectedValue;

            if (!Factory.AdminInGroup().CheckInfo(admInGrModel.AdminID, admInGrModel.AdminGroupID))
            {
                AdminModel admModel = new AdminModel();
                admModel = Factory.Admin().GetInfo(admInGrModel.AdminID);
                if (admModel != null)
                {
                    if (GetData.CheckAdminID(admModel.ManageAdminID, "AdminAll"))//��鴴����
                    {
                        Factory.AdminInGroup().InsertInfo(admInGrModel);
                        Factory.AdminLog().InsertLog("������Ϊ" + admInGrModel.AdminID + "�Ĺ���Ա�����Ϊ" + admInGrModel.AdminGroupID + "�Ĺ�����!", Session["AdminID"].ToString());
                        Response.Redirect("Admin_SetAdminGroup.aspx?AdminID=" + admInGrModel.AdminID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                    }
                }
            }
        }
        //��ʾ����
        protected void ShowInfo()
        {
            //����Ա
            AdminModel admModel = new AdminModel();
            admModel = Factory.Admin().GetInfo(AdminID);
            if (admModel != null)
            {
                if (GetData.CheckAdminID(admModel.ManageAdminID, "AdminAll"))//��鴴����
                {
                    lblAdminName.Text = admModel.AdminName;
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

            //������
            Factory.Acc().DataBind("select * from t_AdminGroup where AdminGroupID not in(select AdminGroupID from t_AdminInGroup where AdminID=" + AdminID + ")  order by ListID asc", null,Config.DataBindObjTypeCollection.DropDownList.ToString(), drpAdminGroupID, "AdminGroupName", "AdminGroupID");
            drpAdminGroupID.Items.Insert(0, new ListItem("��ѡ��", "-1"));

            //�ѷ���������б�
            GridView1.DataKeyNames = new string[] { "AdminID", "AdminGroupID" };
            Factory.Acc().DataBind("select * from t_AdminInGroup where AdminID=" + AdminID + " order by AdminGroupID asc", null,Config.DataBindObjTypeCollection.GridView.ToString(), GridView1);
        }

        //ɾ��
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            AdminModel admModel = new AdminModel();
            admModel = Factory.Admin().GetInfo(AdminID);
            if (admModel != null)
            {
                if (GetData.CheckAdminID(admModel.ManageAdminID, "AdminAll"))//��鴴����
                {
                    string strAdminID = GridView1.DataKeys[e.RowIndex].Values["AdminID"].ToString();
                    string strAdminGroupID = GridView1.DataKeys[e.RowIndex].Values["AdminGroupID"].ToString();
                    Factory.AdminInGroup().DeleteInfo(strAdminID, strAdminGroupID);
                    Factory.AdminLog().InsertLog("ɾ������Ա���Ϊ" + strAdminID + "���������Ϊ" + strAdminGroupID + "�Ĺ��������!", Session["AdminID"].ToString());
                    Response.Redirect("Admin_SetAdminGroup.aspx?AdminID=" + AdminID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
            }
        }
    }
}
