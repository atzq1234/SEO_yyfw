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
    public partial class Class : System.Web.UI.Page
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
        public string ParentID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ParentID"], 0).ToString();
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
        public string strClassName
        {
            get
            {
                return Config.Request(Request["txtClassName"], "");
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
                if (strClassName != "") TempSql.Append(" and ClassName like @ClassName");
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
                if (strClassName != "") listParams.Add(Config.Conn().CreateDbParameter("@ClassName", "%" + strClassName + "%"));
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
                TempUrl.Append("txtClassName=" + Server.UrlEncode(strClassName) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //ҳ���ʼ��
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("Class");
            lbtnAdd.Visible = GetData.LimitChk("ClassAdd");
            lbtnEdit.Visible = GetData.LimitChk("ClassEdit");
            lbtnMove.Visible = GetData.LimitChk("ClassMove");
            lbtnDel.Visible = GetData.LimitChk("ClassDel");
            lbtnOpen.Visible = GetData.LimitChk("ClassOpen");
            lbtnClose.Visible = GetData.LimitChk("ClassClose");
            if (!Page.IsPostBack)
            {
                lblNav.Text = Factory.Class().ShowPath(ParentID).ToString();
                btnQuery.PostBackUrl = "Class.aspx?ParentID=" + ParentID;
                lbtnAdd.OnClientClick = "javascript:return GoTo('Class_Add.aspx?ParentID=" + ParentID + "')";
                //�����ϼ�
                ClassModel claModel = new ClassModel();
                claModel = Factory.Class().GetInfo(ParentID);
                if (claModel != null)
                {
                    lbtnGoBack.OnClientClick = "javascript:return GoTo('Class.aspx?ParentID=" + claModel.ParentID + "')";
                }
                Repeater_Bind(repList);
            }
        }
        //������
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Class where 1=1 and ParentID=" + ParentID + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara).ToString();
        }
        //�޸�
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strClassID = Config.Request(Request.Form["ClassID"], "0");
            if (strClassID != "0")
            {
                Response.Redirect("Class_Add.aspx?ParentID=" + ParentID + "&ClassID=" + strClassID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //�ƶ�
        protected void lbtnMove_Click(object sender, EventArgs e)
        {
            string strClassID = Config.Request(Request.Form["ClassID"], "0");
            if (strClassID != "0")
            {
                Response.Redirect("Class_Move.aspx?ParentID=" + ParentID + "&ClassID=" + strClassID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        //ɾ��
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strClassID = Config.Request(Request.Form["ClassID"], "0");
            if (strClassID != "0")
            {
                string[] arrClassID = strClassID.Split(new char[] { ',' });
                StringBuilder strTempClassID = new StringBuilder();
                ClassModel claModel = new ClassModel();
                int n = 0;
                for (int i = 0; i < arrClassID.Length; i++)
                {
                    claModel = Factory.Class().GetInfo(arrClassID[i]);
                    if (claModel != null)
                    {
                        if (Convert.ToInt32(claModel.ChildNum) == 0)
                        {
                            if (GetData.CheckAdminID(claModel.AdminID, "ClassAll"))//��鴴����
                            {
                                Factory.Class().DeleteInfo(arrClassID[i]);
                                strTempClassID.Append(arrClassID[i]);
                                if (i + 1 < arrClassID.Length) strTempClassID.Append(",");
                                n++;
                            }
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("ɾ�����Ϊ" + strTempClassID.ToString() + "����Ŀ!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempClassID.ToString() + "��Ŀɾ���ɹ�!", "Class.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("����ʧ�ܣ�����ɾ���Ӽ�!");
                }
            }
        }

        //��������
        protected void btnOpen_Click(object sender, EventArgs e)
        {
            string strClassID = Config.Request(Request.Form["ClassID"], "0");
            if (strClassID != "0")
            {
                string[] arrClassID = strClassID.Split(new char[] { ',' });
                StringBuilder strTempClassID = new StringBuilder();
                ClassModel claModel = new ClassModel();
                int n = 0;
                for (int i = 0; i < arrClassID.Length; i++)
                {
                    claModel = Factory.Class().GetInfo(arrClassID[i]);
                    if (claModel != null)
                    {
                        if (GetData.CheckAdminID(claModel.AdminID, "ClassAll"))//��鴴����
                        {
                            Factory.Class().UpdateCloseStatus(arrClassID[i], "0");
                            strTempClassID.Append(arrClassID[i]);
                            if (i + 1 < arrClassID.Length) strTempClassID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("���ű��Ϊ" + strTempClassID.ToString() + "����Ŀ!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempClassID.ToString() + "��Ŀ���ųɹ�!", "Class.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strClassID = Config.Request(Request.Form["ClassID"], "0");
            if (strClassID != "0")
            {
                string[] arrClassID = strClassID.Split(new char[] { ',' });
                StringBuilder strTempClassID = new StringBuilder();
                ClassModel claModel = new ClassModel();
                int n = 0;
                for (int i = 0; i < arrClassID.Length; i++)
                {
                    claModel = Factory.Class().GetInfo(arrClassID[i]);
                    if (claModel != null)
                    {
                        if (GetData.CheckAdminID(claModel.AdminID, "ClassAll"))//��鴴����
                        {
                            Factory.Class().UpdateCloseStatus(arrClassID[i], "1");
                            strTempClassID.Append(arrClassID[i]);
                            if (i + 1 < arrClassID.Length) strTempClassID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("�رձ��Ϊ" + strTempClassID.ToString() + "����Ŀ!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempClassID.ToString() + "��Ŀ�رճɹ�!", "Class.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("����ʧ��!");
                }
            }
        }

        //��ѯ
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Repeater_Bind(repList);
        }
    }
}
