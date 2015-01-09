using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.cn.UserControl
{
    public partial class WUC_Footer : System.Web.UI.UserControl
    {
        private string _parentid = "-1", _configid;
        /// <summary>
        /// 主栏目ID
        /// </summary>
        public string ParentID
        {
            get { return _parentid; }
            set { _parentid = value; }
        }
        /// <summary>
        /// 配置ID
        /// </summary>
        public string ConfigID
        {
            get { return _configid; }
            set { _configid = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //ConfigModel confModel = new ConfigModel();
            //confModel = Factory.Config().GetCacheInfo(ConfigID);
            //if (confModel != null)
            //{
            //    litFooterInfo.Text = confModel.FooterInfo;
            //}
            Link_Bind("1", repLinkText);
        }
        //列表绑定
        protected void Link_Bind(string strTypeID, Repeater rep)
        {
            string sql = "select * from t_Link where IsClose=0 and ConfigID=" + ConfigID + " and TypeID=" + strTypeID + " order by ListID asc";
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.Repeater.ToString(), rep);
        }
    }
}