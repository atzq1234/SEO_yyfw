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

namespace HxSoft.Web.cn.UserControl
{
    public partial class WUC_Product_List_TopNum : System.Web.UI.UserControl
    {
        //this page add by yang
        private string _classid, _noclassid = "0", _datalink, _orderfield = "AddTime", _orderkey = "desc", _styleclass = "";
        private int _topnum = 0, _titlenum = 0;
        private bool _isshowsub = false, _isshowtime = false, _isonlyrecommend = false;
        /// <summary>
        /// 分类ID
        /// </summary>
        public string ClassID
        {
            get { return _classid; }
            set { _classid = value; }
        }
        /// <summary>
        /// 不显示分类ID
        /// </summary>
        public string NoClassID
        {
            get { return _noclassid; }
            set { _noclassid = value; }
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
        /// 是否调用子分类信息(默认不调用子分类信息)
        /// </summary>
        public bool IsShowSub
        {
            get { return _isshowsub; }
            set { _isshowsub = value; }
        }
        /// <summary>
        /// 是否显示日期(默认不显示日期)
        /// </summary>
        public bool IsShowTime
        {
            get { return _isshowtime; }
            set { _isshowtime = value; }
        }
        /// <summary>
        /// 是否只调用推荐信息(默认不调用推荐信息)
        /// </summary>
        public bool IsOnlyRecommend
        {
            get { return _isonlyrecommend; }
            set { _isonlyrecommend = value; }
        }
        /// <summary>
        /// 样式类名
        /// </summary>
        public string StyleClass
        {
            get { return _styleclass; }
            set { _styleclass = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //列表绑定
            //
            StringBuilder strSql = new StringBuilder();
            if (IsShowSub)
            {
                strSql.Append(" and (ClassID=" + ClassID + " or ClassID in ("+Factory.Class().GetSubClassSql(ClassID)+"))");
            }
            else
            {
                strSql.Append(" and ClassID=" + ClassID);
            }
            //
            if (NoClassID != "0")
            {
                strSql.Append(" and ClassID not in(-1," + NoClassID + ",-1)");
            }
            //
            if (IsOnlyRecommend)
            {
                strSql.Append(" and IsRecommend=1");
            }
            //
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
            string sql = "select " + strSqlTop + " * from t_Product where IsClose=0 " + strSql.ToString() + " order by " + OrderField + " " + OrderKey + " " + strMySqlTop;
            Factory.Acc().DataBind( sql, null,Config.DataBindObjTypeCollection.Repeater.ToString(), repList);
        }
    }
}