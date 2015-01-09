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
    public partial class WUC_Chat : System.Web.UI.UserControl
    {
        private string _configid;
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
            string sql = "select * from t_Chat where IsClose=0 and ConfigID=" + ConfigID + " order by ListID asc";
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.Repeater.ToString(), repChatList);
        }
    }
}