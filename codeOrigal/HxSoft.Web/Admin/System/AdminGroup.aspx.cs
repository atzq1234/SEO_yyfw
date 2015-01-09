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
    public partial class AdminGroup : System.Web.UI.Page
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
        public bool boolSetAdmin
        {
            get { return GetData.LimitChk("AdminGroupSetAdmin"); }
        }
        public bool boolSetLimit
        {
            get { return GetData.LimitChk("AdminGroupSetLimit"); }
        }
        //ҳ���ʼ��
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("AdminGroup");
            lbtnAdd.Visible = GetData.LimitChk("AdminGroupAdd");
            lbtnEdit.Visible = GetData.LimitChk("AdminGroupEdit");
            lbtnDel.Visible = GetData.LimitChk("AdminGroupDel");
            lbtnOpen.Visible = GetData.LimitChk("AdminGroupOpen");
            lbtnClose.Visible = GetData.LimitChk("AdminGroupClose");
            //lbtnSetAdmin.Visible = GetData.LimitChk("AdminGroupSetAdmin");
            //lbtnSetLimit.Visible = GetData.LimitChk("AdminGroupSetLimit");
            if (!Page.IsPostBack)
            {
                Repeater_Bind(repList);
            }
        }
        //������
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_AdminGroup where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //�޸�
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strAdminGroupID = Config.Request(Request.Form["AdminGroupID"], "0");
            if (strAdminGroupID != "0")
            {
                Response.Redirect("AdminGroup_Add.aspx?AdminGroupID=" + strAdminGroupID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //ɾ��
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strAdminGroupID = Config.Request(Request.Form["AdminGroupID"], "0");
            if (strAdminGroupID != "0")
            {
                string[] arrAdminGroupID = strAdminGroupID.Split(new char[] { ',' });
                StringBuilder strTempAdminGroupID = new StringBuilder();
                AdminGroupModel admGrModel = new AdminGroupModel();
                int n = 0;
                for (int i = 0; i < arrAdminGroupID.Length; i++)
                {
                    admGrModel = Factory.AdminGroup().GetInfo(arrAdminGroupID[i]);
                    if (admGrModel != null)
                    {
                        if (GetData.CheckAdminID(admGrModel.AdminID, "AdminGroupAll"))//��鴴����
                        {
                            Factory.AdminGroup().DeleteInfo(arrAdminGroupID[i]);
                            strTempAdminGroupID.Append(arrAdminGroupID[i]);
                            if (i + 1 < arrAdminGroupID.Length) strTempAdminGroupID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("ɾ�����Ϊ" + strTempAdminGroupID.ToString() + "�Ĺ�����!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempAdminGroupID.ToString() + "������ɾ���ɹ�!", "AdminGroup.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strAdminGroupID = Config.Request(Request.Form["AdminGroupID"], "0");
            if (strAdminGroupID != "0")
            {
                string[] arrAdminGroupID = strAdminGroupID.Split(new char[] { ',' });
                StringBuilder strTempAdminGroupID = new StringBuilder();
                AdminGroupModel admGrModel = new AdminGroupModel();
                int n = 0;
                for (int i = 0; i < arrAdminGroupID.Length; i++)
                {
                    admGrModel = Factory.AdminGroup().GetInfo(arrAdminGroupID[i]);
                    if (admGrModel != null)
                    {
                        if (GetData.CheckAdminID(admGrModel.AdminID, "AdminGroupAll"))//��鴴����
                        {
                            Factory.AdminGroup().UpdateCloseStatus(arrAdminGroupID[i],"0");
                            strTempAdminGroupID.Append(arrAdminGroupID[i]);
                            if (i + 1 < arrAdminGroupID.Length) strTempAdminGroupID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("���ű��Ϊ" + strTempAdminGroupID.ToString() + "�Ĺ�����!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempAdminGroupID.ToString() + "�����鿪�ųɹ�!", "AdminGroup.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strAdminGroupID = Config.Request(Request.Form["AdminGroupID"], "0");
            if (strAdminGroupID != "0")
            {
                string[] arrAdminGroupID = strAdminGroupID.Split(new char[] { ',' });
                StringBuilder strTempAdminGroupID = new StringBuilder();
                AdminGroupModel admGrModel = new AdminGroupModel();
                int n = 0;
                for (int i = 0; i < arrAdminGroupID.Length; i++)
                {
                    admGrModel = Factory.AdminGroup().GetInfo(arrAdminGroupID[i]);
                    if (admGrModel != null)
                    {
                        if (GetData.CheckAdminID(admGrModel.AdminID, "AdminGroupAll"))//��鴴����
                        {
                            Factory.AdminGroup().UpdateCloseStatus(arrAdminGroupID[i],"1");
                            strTempAdminGroupID.Append(arrAdminGroupID[i]);
                            if (i + 1 < arrAdminGroupID.Length) strTempAdminGroupID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("�رձ��Ϊ" + strTempAdminGroupID.ToString() + "�Ĺ�����!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempAdminGroupID.ToString() + "������رճɹ�!", "AdminGroup.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("����ʧ��!");
                }
            }
        }

        protected void repList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string strAdminGroupID = e.CommandArgument.ToString();
            //���ù���Ա
            if (e.CommandName == "SetAdmin")
            {
                Response.Redirect("AdminGroup_SetAdmin.aspx?AdminGroupID=" + strAdminGroupID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
            //���ò���Ȩ��
            if (e.CommandName == "SetLimit")
            {
                Response.Redirect("AdminGroup_SetLimit.aspx?AdminGroupID=" + strAdminGroupID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        protected void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lbtnSetAdmin = (LinkButton)e.Item.FindControl("lbtnSetAdmin");
            lbtnSetAdmin.Enabled = boolSetAdmin;

            LinkButton lbtnSetLimit = (LinkButton)e.Item.FindControl("lbtnSetLimit");
            lbtnSetLimit.Enabled = boolSetLimit;
        }

        //��ѯ
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Repeater_Bind(repList);
        }
    }
}
