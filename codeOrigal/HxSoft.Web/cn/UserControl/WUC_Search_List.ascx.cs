using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;
using System.Data.Common;

namespace HxSoft.Web.cn.UserControl
{
    public partial class WUC_Search_List : System.Web.UI.UserControl
    {
        #region ****属性****
        private string _configid, _searchtype = "Article", _orderkey = "desc";
        private int _titlenum = 0, _pagesize = 10;
        /// <summary>
        /// 配置ID
        /// </summary>
        public string ConfigID
        {
            get { return _configid; }
            set { _configid = value; }
        }
        /// <summary>
        /// 搜索类型(默认搜索文章表)
        /// </summary>
        public string SearchType
        {
            get { return _searchtype; }
            set { _searchtype = value; }
        }
        /// <summary>
        /// 排序方法(默认为倒序)
        /// </summary>
        public string OrderKey
        {
            get { return _orderkey; }
            set { _orderkey = value; }
        }
        /// <summary>
        /// 标题字数(默认显示完整标题)
        /// </summary>
        public int TitleNum
        {
            get { return _titlenum; }
            set { _titlenum = value; }
        }
        /// <summary>
        /// 分页数(默认显示10条记录)
        /// </summary>
        public int PageSize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }
        /// <summary>
        /// 分页(只读)
        /// </summary>
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        #endregion
        #region****查询参数****
        public string SearchKey
        {
            get
            {
                return Config.Request(Request["SearchKey"], "");
            }
        }
        #endregion
        #region****DbParameter参数****
        public DbParameter[] SqlParams
        {
            get
            {
                List<DbParameter> listParams = new List<DbParameter>();
                listParams.Add(Config.Conn().CreateDbParameter("@SearchKey", "%" + SearchKey + "%"));
                return listParams.ToArray();
            }
        }
        #endregion
        #region****Url参数****
        public string UrlPara
        {
            get
            {
                StringBuilder TempUrl = new StringBuilder("");
                TempUrl.Append("SearchKey=" + Server.UrlEncode(SearchKey) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            litClassName.Text = "搜索结果";
            Page.Header.Title = "搜索结果" + " - " + Page.Header.Title;

            //列表绑定
            string strTemp = " and ClassID in(select ClassID from t_Class where ConfigID=" + ConfigID + ")";
            StringBuilder strSql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                strSql.Append("select Title as sTitle,concat('article-details-',ArticleID,'" + Config.FileExt + "') as sLinkUrl,AddTime as sAddTime from t_Article where IsClose=0 " + strTemp + " and Title like @SearchKey");
                strSql.Append(" union all select ClassName as sTitle,concat(ClassEnName,'" + Config.FileExt + "') as sLinkUrl,AddTime as sAddTime from t_Class where IsClose=0 " + strTemp + " and ClassName like @SearchKey");
                strSql.Append(" union all select ProductName as sTitle,concat('product-details-',ProductID,'" + Config.FileExt + "') as sLinkUrl,AddTime as sAddTime from t_Product where IsClose=0 " + strTemp + " and ProductName like @SearchKey");
                strSql.Append(" union all select JobName as sTitle,concat('job-details-',JobID,'" + Config.FileExt + "') as sLinkUrl,AddTime as sAddTime from t_Job where IsClose=0 " + strTemp + " and JobName like @SearchKey");
                strSql.Append(" union all select DownName as sTitle,concat('download-details-',DownloadID,'" + Config.FileExt + "') as sLinkUrl,AddTime as sAddTime from t_Download where IsClose=0 " + strTemp + " and DownName like @SearchKey");
                strSql.Append(" union all select Subject as sTitle,concat('survey-details-',SurveyID,'" + Config.FileExt + "') as sLinkUrl,AddTime as sAddTime from t_Survey where IsClose=0 " + strTemp + " and Subject like @SearchKey");
            }
            else
            {
                strSql.Append("select Title as sTitle,('article-details-'+ltrim(str(ArticleID))+'" + Config.FileExt + "') as sLinkUrl,AddTime as sAddTime from t_Article where IsClose=0 " + strTemp + " and Title like @SearchKey");
                strSql.Append(" union all select ClassName as sTitle,(ClassEnName+'" + Config.FileExt + "') as sLinkUrl,AddTime as sAddTime from t_Class where IsClose=0 " + strTemp + " and ClassName like @SearchKey");
                strSql.Append(" union all select ProductName as sTitle,('product-details-'+ltrim(str(ProductID))+'" + Config.FileExt + "') as sLinkUrl,AddTime as sAddTime from t_Product where IsClose=0 " + strTemp + " and ProductName like @SearchKey");
                strSql.Append(" union all select JobName as sTitle,('job-details-'+ltrim(str(JobID))+'" + Config.FileExt + "') as sLinkUrl,AddTime as sAddTime from t_Job where IsClose=0 " + strTemp + " and JobName like @SearchKey");
                strSql.Append(" union all select DownName as sTitle,('download-details-'+ltrim(str(DownloadID))+'" + Config.FileExt + "') as sLinkUrl,AddTime as sAddTime from t_Download where IsClose=0 " + strTemp + " and DownName like @SearchKey");
                strSql.Append(" union all select Subject as sTitle,('survey-details-'+ltrim(str(SurveyID))+'" + Config.FileExt + "') as sLinkUrl,AddTime as sAddTime from t_Survey where IsClose=0 " + strTemp + " and Subject like @SearchKey");
            }
            strSql.Append(" order by sAddTime " + OrderKey);
            //Config.ShowEnd(strSql.ToString());
            pager.InnerHtml = Factory.Acc().DataPageBindForCn(strSql.ToString(), SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), repList, PageSize, page, "?" + UrlPara).ToString();
        }

        public string GetString(object obj)
        {
            try
            {
                return ASCIIEncoding.ASCII.GetString((byte[])obj);
            }
            catch
            {
                return obj.ToString();
            }
        }
    }
}