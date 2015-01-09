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
    public partial class Dictionary : System.Web.UI.Page
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
        public string strDictionaryName
        {
            get
            {
                return Config.Request(Request["txtDictionaryName"], "");
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
                if (strDictionaryName != "") TempSql.Append(" and DictionaryName like @DictionaryName");
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
                if (strDictionaryName != "") listParams.Add(Config.Conn().CreateDbParameter("@DictionaryName", "%" + strDictionaryName + "%"));
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
                TempUrl.Append("txtDictionaryName=" + Server.UrlEncode(strDictionaryName) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //ҳ���ʼ��
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("Dictionary");
            lbtnAdd.Visible = GetData.LimitChk("DictionaryAdd");
            lbtnEdit.Visible = GetData.LimitChk("DictionaryEdit");
            lbtnMove.Visible = GetData.LimitChk("DictionaryMove");
            lbtnDel.Visible = GetData.LimitChk("DictionaryDel");
            lbtnOpen.Visible = GetData.LimitChk("DictionaryOpen");
            lbtnClose.Visible = GetData.LimitChk("DictionaryClose");
            if (!Page.IsPostBack)
            {
                lblNav.Text=Factory.Dictionary().ShowPath(ParentID).ToString();
                btnQuery.PostBackUrl = "Dictionary.aspx?ParentID=" + ParentID;
                lbtnAdd.OnClientClick = "javascript:return GoTo('Dictionary_Add.aspx?ParentID="+ParentID+"')";
                //�����ϼ�
                DictionaryModel dictModel = new DictionaryModel();
                dictModel = Factory.Dictionary().GetInfo(ParentID);
                if (dictModel != null)
                {
                    lbtnGoBack.OnClientClick = "javascript:return GoTo('Dictionary.aspx?ParentID=" + dictModel.ParentID + "')";
                }
                Repeater_Bind(repList);
            }
        }
        //������
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Dictionary where 1=1 and ParentID=" + ParentID + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara).ToString();
        }
        //�޸�
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strDictionaryID = Config.Request(Request.Form["DictionaryID"], "0");
            if (strDictionaryID != "0")
            {
                Response.Redirect("Dictionary_Add.aspx?ParentID=" + ParentID + "&DictionaryID=" + strDictionaryID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //�ƶ�
        protected void lbtnMove_Click(object sender, EventArgs e)
        {
            string strDictionaryID = Config.Request(Request.Form["DictionaryID"], "0");
            if (strDictionaryID != "0")
            {
                Response.Redirect("Dictionary_Move.aspx?ParentID=" + ParentID + "&DictionaryID=" + strDictionaryID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        //ɾ��
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strDictionaryID = Config.Request(Request.Form["DictionaryID"], "0");
            if (strDictionaryID != "0")
            {
                string[] arrDictionaryID = strDictionaryID.Split(new char[] { ',' });
                StringBuilder strTempDictionaryID = new StringBuilder();
                DictionaryModel dictModel = new DictionaryModel();
                int n = 0;
                for (int i = 0; i < arrDictionaryID.Length; i++)
                {
                    dictModel = Factory.Dictionary().GetInfo(arrDictionaryID[i]);
                    if (dictModel != null)
                    {
                        if (Convert.ToInt32(dictModel.ChildNum)==0)
                        {
                            if (GetData.CheckAdminID(dictModel.AdminID, "DictionaryAll"))//��鴴����
                            {
                                Factory.Dictionary().DeleteInfo(arrDictionaryID[i]);
                                strTempDictionaryID.Append(arrDictionaryID[i]);
                                if (i + 1 < arrDictionaryID.Length) strTempDictionaryID.Append(",");
                                n++;
                            }
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("ɾ�����Ϊ" + strTempDictionaryID.ToString() + "���ֵ�!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempDictionaryID.ToString() + "�ֵ�ɾ���ɹ�!", "Dictionary.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strDictionaryID = Config.Request(Request.Form["DictionaryID"], "0");
            if (strDictionaryID != "0")
            {
                string[] arrDictionaryID = strDictionaryID.Split(new char[] { ',' });
                StringBuilder strTempDictionaryID = new StringBuilder();
                DictionaryModel dictModel = new DictionaryModel();
                int n = 0;
                for (int i = 0; i < arrDictionaryID.Length; i++)
                {
                    dictModel = Factory.Dictionary().GetInfo(arrDictionaryID[i]);
                    if (dictModel != null)
                    {
                        if (GetData.CheckAdminID(dictModel.AdminID, "DictionaryAll"))//��鴴����
                        {
                            Factory.Dictionary().UpdateCloseStatus(arrDictionaryID[i], "0");
                            strTempDictionaryID.Append(arrDictionaryID[i]);
                            if (i + 1 < arrDictionaryID.Length) strTempDictionaryID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("���ű��Ϊ" + strTempDictionaryID.ToString() + "���ֵ�!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempDictionaryID.ToString() + "�ֵ俪�ųɹ�!", "Dictionary.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strDictionaryID = Config.Request(Request.Form["DictionaryID"], "0");
            if (strDictionaryID != "0")
            {
                string[] arrDictionaryID = strDictionaryID.Split(new char[] { ',' });
                StringBuilder strTempDictionaryID = new StringBuilder();
                DictionaryModel dictModel = new DictionaryModel();
                int n = 0;
                for (int i = 0; i < arrDictionaryID.Length; i++)
                {
                    dictModel = Factory.Dictionary().GetInfo(arrDictionaryID[i]);
                    if (dictModel != null)
                    {
                        if (GetData.CheckAdminID(dictModel.AdminID, "DictionaryAll"))//��鴴����
                        {
                            Factory.Dictionary().UpdateCloseStatus(arrDictionaryID[i], "1");
                            strTempDictionaryID.Append(arrDictionaryID[i]);
                            if (i + 1 < arrDictionaryID.Length) strTempDictionaryID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("�رձ��Ϊ" + strTempDictionaryID.ToString() + "���ֵ�!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempDictionaryID.ToString() + "�ֵ�رճɹ�!", "Dictionary.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
