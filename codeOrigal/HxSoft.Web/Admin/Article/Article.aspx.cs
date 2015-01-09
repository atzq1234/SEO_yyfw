using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using HxSoft.ClassFactory;
using HxSoft.Common;
using HxSoft.Model;

namespace HxSoft.Web.Admin.Article
{
    public partial class Article : System.Web.UI.Page
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
                return Config.Request(Request["OrderKey"], "ArticleID");
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
        public string strClassID
        {
            get
            {
                return Config.Request(Request["drpClassID"], "-1");
            }
        }
        public string strTitle
        {
            get
            {
                return Config.Request(Request["txtTitle"], "");
            }
        }
        public string strIsRecommend
        {
            get
            {
                return Config.Request(Request["radIsRecommend"], "-1");
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
                if (strClassID != "-1") TempSql.Append(" and (ClassID=@ClassID or ClassID in (" + Factory.Class().GetSubClassSql(strClassID) + "))");
                if (strTitle != "") TempSql.Append(" and Title like @Title");
                if (strIsRecommend != "-1") TempSql.Append(" and IsRecommend =@IsRecommend");
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
                if (strClassID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@ClassID", strClassID));
                if (strTitle != "") listParams.Add(Config.Conn().CreateDbParameter("@Title", "%" + strTitle + "%"));
                if (strIsRecommend != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsRecommend", strIsRecommend));
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
                TempUrl.Append("drpClassID=" + Server.UrlEncode(strClassID) + "&");
                TempUrl.Append("txtTitle=" + Server.UrlEncode(strTitle) + "&");
                TempUrl.Append("radIsRecommend=" + Server.UrlEncode(strIsRecommend) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //ҳ���ʼ��
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("Article");
            lbtnAdd.Visible = GetData.LimitChk("ArticleAdd");
            lbtnEdit.Visible = GetData.LimitChk("ArticleEdit");
            lbtnDel.Visible = GetData.LimitChk("ArticleDel");
            lbtnOpen.Visible = GetData.LimitChk("ArticleOpen");
            lbtnClose.Visible = GetData.LimitChk("ArticleClose");
            lbtnTransfer.Visible = GetData.LimitChk("ArticleTransfer");
            if (!Page.IsPostBack)
            {
                //���·���
                Factory.Class().ShowSelectTree("0", drpClassID, "", Config.SysArticleMouldID);
                drpClassID.Items.Insert(0, new ListItem("����", "-1"));

                Repeater_Bind(repList);
            }
        }
        //������
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Article where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //�޸�
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strArticleID = Config.Request(Request.Form["ArticleID"], "0");
            if (strArticleID != "0")
            {
                Response.Redirect("Article_Add.aspx?ArticleID=" + strArticleID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //ɾ��
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strArticleID = Config.Request(Request.Form["ArticleID"], "0");
            if (strArticleID != "0")
            {
                string[] arrArticleID = strArticleID.Split(new char[] { ',' });
                StringBuilder strTempArticleID = new StringBuilder();
                ArticleModel artModel = new ArticleModel();
                int n = 0;
                for (int i = 0; i < arrArticleID.Length; i++)
                {
                    artModel = Factory.Article().GetInfo(arrArticleID[i]);
                    if (artModel != null)
                    {
                        if (GetData.CheckAdminID(artModel.AdminID, "ArticleAll"))//��鴴����
                        {
                            Factory.Article().DeleteInfo(arrArticleID[i]);
                            strTempArticleID.Append(arrArticleID[i]);
                            if (i + 1 < arrArticleID.Length) strTempArticleID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("ɾ�����Ϊ" + strTempArticleID.ToString() + "������!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempArticleID.ToString() + "����ɾ���ɹ�!", "Article.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strArticleID = Config.Request(Request.Form["ArticleID"], "0");
            if (strArticleID != "0")
            {
                string[] arrArticleID = strArticleID.Split(new char[] { ',' });
                StringBuilder strTempArticleID = new StringBuilder();
                ArticleModel artModel = new ArticleModel();
                int n = 0;
                for (int i = 0; i < arrArticleID.Length; i++)
                {
                    artModel = Factory.Article().GetInfo(arrArticleID[i]);
                    if (artModel != null)
                    {
                        if (GetData.CheckAdminID(artModel.AdminID, "ArticleAll"))//��鴴����
                        {
                            Factory.Article().UpdateCloseStatus(arrArticleID[i], "0");
                            strTempArticleID.Append(arrArticleID[i]);
                            if (i + 1 < arrArticleID.Length) strTempArticleID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("���ű��Ϊ" + strTempArticleID.ToString() + "������!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempArticleID.ToString() + "���¿��ųɹ�!", "Article.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strArticleID = Config.Request(Request.Form["ArticleID"], "0");
            if (strArticleID != "0")
            {
                string[] arrArticleID = strArticleID.Split(new char[] { ',' });
                StringBuilder strTempArticleID = new StringBuilder();
                ArticleModel artModel = new ArticleModel();
                int n = 0;
                for (int i = 0; i < arrArticleID.Length; i++)
                {
                    artModel = Factory.Article().GetInfo(arrArticleID[i]);
                    if (artModel != null)
                    {
                        if (GetData.CheckAdminID(artModel.AdminID, "ArticleAll"))//��鴴����
                        {
                            Factory.Article().UpdateCloseStatus(arrArticleID[i], "1");
                            strTempArticleID.Append(arrArticleID[i]);
                            if (i + 1 < arrArticleID.Length) strTempArticleID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("�رձ��Ϊ" + strTempArticleID.ToString() + "������!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempArticleID.ToString() + "���¹رճɹ�!", "Article.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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

        //ת������
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            string strArticleID = Config.Request(Request.Form["ArticleID"], "0");
            if (strArticleID != "0")
            {
                Response.Redirect("Article_Transfer.aspx?ArticleID=" + strArticleID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
    }
}
