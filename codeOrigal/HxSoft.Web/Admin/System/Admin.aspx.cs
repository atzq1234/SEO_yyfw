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
using System.Data.Common;
using System.Collections.Generic;

namespace HxSoft.Web.Admin._System
{
    public partial class Admin : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
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
        public bool boolSetAdminGroup
        {
            get { return GetData.LimitChk("AdminSetAdminGroup"); }
        }
        //ҳ���ʼ��
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("Admin");
            lbtnAdd.Visible = GetData.LimitChk("AdminAdd");
            lbtnEdit.Visible = GetData.LimitChk("AdminEdit");
            lbtnDel.Visible = GetData.LimitChk("AdminDel");
            lbtnOpen.Visible = GetData.LimitChk("AdminOpen");
            lbtnClose.Visible = GetData.LimitChk("AdminClose");
            //lbtnSetAdminGroup.Visible = GetData.LimitChk("AdminSetAdminGroup");
            if (!Page.IsPostBack)
            {
                //������
                Factory.Acc().DataBind("select * from t_AdminGroup order by ListID asc",  null,Config.DataBindObjTypeCollection.DropDownList.ToString(), drpAdminGroupID, "AdminGroupName", "AdminGroupID");
                drpAdminGroupID.Items.Insert(0,new ListItem("����","-1"));
                Repeater_Bind(repList);
            }
        }
        //������
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Admin where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //�޸�
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strAdminID = Config.Request(Request.Form["AdminID"], "0");
            if (strAdminID != "0")
            {
                Response.Redirect("Admin_Add.aspx?AdminID=" + strAdminID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //ɾ��
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strAdminID = Config.Request(Request.Form["AdminID"], "0");
            if (strAdminID != "0")
            {
                string[] arrAdminID = strAdminID.Split(new char[] { ',' });
                StringBuilder strTempAdminID = new StringBuilder();
                AdminModel admModel = new AdminModel();
                int n = 0;
                for (int i = 0; i < arrAdminID.Length; i++)
                {
                    admModel = Factory.Admin().GetInfo(arrAdminID[i]);
                    if (admModel != null)
                    {
                        if (GetData.CheckAdminID(admModel.ManageAdminID, "AdminAll") && arrAdminID[i] != "1")//��鴴����,������ɾ����������Ա
                        {
                            Factory.Admin().DeleteInfo(arrAdminID[i]);
                            strTempAdminID.Append(arrAdminID[i]);
                            if (i + 1 < arrAdminID.Length) strTempAdminID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("ɾ�����Ϊ" + strTempAdminID.ToString() + "�Ĺ���Ա!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempAdminID.ToString() + "����Աɾ���ɹ�!", "Admin.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("����ʧ��!");
                }
            }
        }
        //��������
        protected void btnOpen_Click(object sender, EventArgs e)
        {
            string strAdminID = Config.Request(Request.Form["AdminID"], "0");
            if (strAdminID != "0")
            {
                string[] arrAdminID = strAdminID.Split(new char[] { ',' });
                StringBuilder strTempAdminID = new StringBuilder();
                AdminModel admModel = new AdminModel();
                int n = 0;
                for (int i = 0; i < arrAdminID.Length; i++)
                {
                    admModel = Factory.Admin().GetInfo(arrAdminID[i]);
                    if (admModel != null)
                    {
                        if (GetData.CheckAdminID(admModel.ManageAdminID, "AdminAll"))//��鴴����
                        {
                            Factory.Admin().UpdateCloseStatus(arrAdminID[i], "0");
                            strTempAdminID.Append(arrAdminID[i]);
                            if (i + 1 < arrAdminID.Length) strTempAdminID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("���ű��Ϊ" + strTempAdminID.ToString() + "�Ĺ���Ա!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempAdminID.ToString() + "����Ա���ųɹ�!", "Admin.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("����ʧ��!");
                }
            }
        }

        //�����ر�
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string strAdminID = Config.Request(Request.Form["AdminID"], "0");
            if (strAdminID != "0")
            {
                string[] arrAdminID = strAdminID.Split(new char[] { ',' });
                StringBuilder strTempAdminID = new StringBuilder();
                AdminModel admModel = new AdminModel();
                int n = 0;
                for (int i = 0; i < arrAdminID.Length; i++)
                {
                    admModel = Factory.Admin().GetInfo(arrAdminID[i]);
                    if (admModel != null)
                    {
                        if (GetData.CheckAdminID(admModel.ManageAdminID, "AdminAll") && arrAdminID[i] != "1")//��鴴����,������رճ�������Ա
                        {
                            Factory.Admin().UpdateCloseStatus(arrAdminID[i], "1");
                            strTempAdminID.Append(arrAdminID[i]);
                            if (i + 1 < arrAdminID.Length) strTempAdminID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("�رձ��Ϊ" + strTempAdminID.ToString() + "�Ĺ���Ա!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempAdminID.ToString() + "����Ա�رճɹ�!", "Admin.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("����ʧ��!");
                }
            }
        }
        //���ù�����
        protected void btnSetAdminGroup_Click(object sender, EventArgs e)
        {
            string strAdminID = Config.Request(Request.Form["AdminID"], "0");
            if (strAdminID != "0")
            {
                Response.Redirect("Admin_SetAdminGroup.aspx?AdminID=" + strAdminID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        protected void repList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string strAdminID = e.CommandArgument.ToString();
            //���������
            if (e.CommandName == "SetAdminGroup")
            {
                Response.Redirect("Admin_SetAdminGroup.aspx?AdminID=" + strAdminID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        protected void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lbtnSetAdminGroup = (LinkButton)e.Item.FindControl("lbtnSetAdminGroup");
            lbtnSetAdminGroup.Enabled = boolSetAdminGroup;
        }


        //��ѯ
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Repeater_Bind(repList);
        }
    }
}
