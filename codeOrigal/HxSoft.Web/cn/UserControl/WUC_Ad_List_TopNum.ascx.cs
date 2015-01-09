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
    public partial class WUC_Ad_List_TopNum : System.Web.UI.UserControl
    {
        //this page add by yang
        private string _adpositionid, _orderfield = "AdID", _orderkey = "desc";
        private int _topnum = 0;
        /// <summary>
        /// 广告位ID
        /// </summary>
        public string AdPositionID
        {
            get { return _adpositionid; }
            set { _adpositionid = value; }
        }
        /// <summary>
        /// 排序字段(默认为ArticleID)
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
        protected void Page_Load(object sender, EventArgs e)
        {
            //列表绑定
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
            string sql = "select " + strSqlTop + " a.*,b.Width,b.Height from t_Ad as a left join t_AdPosition as b on a.AdPositionID=b.AdPositionID where a.IsClose=0 and a.AdPositionID=" + AdPositionID + "  order by a." + OrderField + " " + OrderKey + " " + strMySqlTop;
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.Repeater.ToString(), repList);
        }
    }
}