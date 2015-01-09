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
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        #region ****排序参数****
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
        #region ****排序语句****
        public string SqlOrder
        {
            get
            {
                return " order by " + strOrderKey + " " + strAscDesc1;
            }
        }
        #endregion
        #region ****Url排序参数****
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
        #region ****查询参数****
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
        #region ****查询语句****
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
        #region****DbParameter参数****
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
        #region ****Url参数****
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
        //页面初始化
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
                //文章分类
                Factory.Class().ShowSelectTree("0", drpClassID, "", Config.SysArticleMouldID);
                drpClassID.Items.Insert(0, new ListItem("不限", "-1"));

                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Article where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strArticleID = Config.Request(Request.Form["ArticleID"], "0");
            if (strArticleID != "0")
            {
                Response.Redirect("Article_Add.aspx?ArticleID=" + strArticleID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
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
                        if (GetData.CheckAdminID(artModel.AdminID, "ArticleAll"))//检查创建者
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
                    Factory.AdminLog().InsertLog("删除编号为" + strTempArticleID.ToString() + "的文章!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempArticleID.ToString() + "文章删除成功!", "Article.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }


        //批量开放
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
                        if (GetData.CheckAdminID(artModel.AdminID, "ArticleAll"))//检查创建者
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
                    Factory.AdminLog().InsertLog("开放编号为" + strTempArticleID.ToString() + "的文章!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempArticleID.ToString() + "文章开放成功!", "Article.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        //批量关闭
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
                        if (GetData.CheckAdminID(artModel.AdminID, "ArticleAll"))//检查创建者
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
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempArticleID.ToString() + "的文章!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempArticleID.ToString() + "文章关闭成功!", "Article.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        //查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Repeater_Bind(repList);
        }

        //转移文章
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
