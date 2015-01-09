using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HxSoft.ClassFactory;
using HxSoft.Common;
using HxSoft.Model;

namespace HxSoft.Web.cn.UserControl
{
    public partial class WUC_Header : System.Web.UI.UserControl
    {
        private string _configid = "-1", _parentid = "-1", _currentparentid = "-1";
        /// <summary>
        /// 配置ID
        /// </summary>
        public string ConfigID
        {
            get { return _configid; }
            set { _configid = value; }
        }
        /// <summary>
        /// 导航父级ID
        /// </summary>
        public string ParentID
        {
            get { return _parentid; }
            set { _parentid = value; }
        }
        /// <summary>
        /// 当前主栏目ID
        /// </summary>
        public string CurrentParentID
        {
            get { return _currentparentid; }
            set { _currentparentid = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Acc().DataBind("select * from t_Class where IsClose=0 and IsShowNav=1 and ParentID=" + ParentID + " and ConfigID=" + ConfigID + " order by ListID asc", null, Config.DataBindObjTypeCollection.Repeater.ToString(), repMenu);
        }
    }
}