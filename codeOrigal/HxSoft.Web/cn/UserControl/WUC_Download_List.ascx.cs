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
    public partial class WUC_Download_List : System.Web.UI.UserControl
    {
        #region ****属性****
        private string _classid, _datalink, _orderfield = "AddTime", _orderkey = "desc", _styleclass = "";
        private int _topnum = 0, _titlenum = 0, _pagesize;
        private bool _isshowsub = false, _isonlyrecommend = false;
        /// <summary>
        /// 分类ID
        /// </summary>
        public string ClassID
        {
            get { return _classid; }
            set { _classid = value; }
        }
        /// <summary>
        /// 详细信息链接
        /// </summary>
        public string DataLink
        {
            get { return _datalink; }
            set { _datalink = value; }
        }
        /// <summary>
        /// 排序字段(默认为AddTime)
        /// </summary>
        public string OrderField
        {
            get { return _orderfield; }
            set { _orderfield = value; }
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
        /// 样式类名
        /// </summary>
        public string StyleClass
        {
            get { return _styleclass; }
            set { _styleclass = value; }
        }
        /// <summary>
        /// 调用信息数量(默认显示所有记录)
        /// </summary>
        public int TopNum
        {
            get { return _topnum; }
            set { _topnum = value; }
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
        /// 分页数
        /// </summary>
        public int PageSize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }
        /// <summary>
        /// 是否调用子分类信息(默认不调用子分类信息)
        /// </summary>
        public bool IsShowSub
        {
            get { return _isshowsub; }
            set { _isshowsub = value; }
        }
        /// <summary>
        /// 是否只调用推荐信息(默认为否)
        /// </summary>
        public bool IsOnlyRecommend
        {
            get { return _isonlyrecommend; }
            set { _isonlyrecommend = value; }
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
        public string IsSearch
        {
            get
            {
                return Config.RequestNumeric(Request["IsSearch"], 0).ToString();
            }
        }
        public string SearchKey
        {
            get
            {
                return Config.Request(Request["SearchKey"], "");
            }
        }

        #endregion
        #region****查询语句****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (SearchKey != "") TempSql.Append(" and DownName like @SearchKey");
                if (ClassID != "-1" && IsSearch != "1")
                {
                    if (IsShowSub)
                    {
                        TempSql.Append(" and (ClassID=@ClassID or ClassID in (" + Factory.Class().GetSubClassSql(ClassID) + "))");
                    }
                    else
                    {
                        TempSql.Append(" and ClassID=@ClassID");
                    }
                }
                if (IsOnlyRecommend) TempSql.Append(" and IsRecommend=1");
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
                if (SearchKey != "") listParams.Add(Config.Conn().CreateDbParameter("@SearchKey", "%" + SearchKey + "%"));
                if (ClassID != "-1" && IsSearch != "1") listParams.Add(Config.Conn().CreateDbParameter("@ClassID", ClassID));
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
                TempUrl.Append("IsSearch=" + Server.UrlEncode(IsSearch) + "&");
                TempUrl.Append("SearchKey=" + Server.UrlEncode(SearchKey) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClassID == "-1" || IsSearch == "1")
            {
                litClassName.Text = "搜索结果";
                Page.Header.Title = "搜索结果" + " - " + Page.Header.Title;
            }
            else
            {
                //栏目名称
                ClassModel claModel = new ClassModel();
                claModel = Factory.Class().GetCacheInfo2(ClassID);
                if (claModel != null)
                {
                    litClassName.Text = claModel.ClassName;

                    Page.Header.Title = Server.HtmlEncode(claModel.ClassName) + " - " + Page.Header.Title;
                    //先清除母版页设置的keywords和description
                    Page.Header.Controls.Remove(Page.Header.FindControl("keywords"));
                    Page.Header.Controls.Remove(Page.Header.FindControl("description"));
                    Page.Header.Controls.Add(Config.SetKeywords(Server.HtmlEncode(claModel.Keywords)));
                    Page.Header.Controls.Add(Config.SetDescription(Server.HtmlEncode(claModel.Description)));
                }
            }
            //列表绑定
            string strSqlTop, strMySqlTop;
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                strSqlTop = "";
                strMySqlTop = TopNum > 0 ? "limit 0, " + TopNum : "";
            }
            else
            {
                strSqlTop = TopNum > 0 ? "top " + TopNum : "";
                strMySqlTop = "";
            }
            string sql = "select " + strSqlTop + " * from t_Download where IsClose=0 " + SqlQuery + " order by " + OrderField + " " + OrderKey + " " + strMySqlTop;
            pager.InnerHtml = Factory.Acc().DataPageBindForCn(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), repList, PageSize, page, "?" + UrlPara).ToString();
        }
    }
}