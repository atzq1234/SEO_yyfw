using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.cn
{
	public partial class Site_Config : System.Web.UI.UserControl
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
            ConfigModel confModel = new ConfigModel();
            confModel = Factory.Config().GetCacheInfo(ConfigID);
            if (confModel != null)
            {
                if (confModel.IsClose == "1")
                {
                    Config.ShowEnd("网站维护中,请稍后访问!");
                }
                else
                {
                    Page.Header.Title = Server.HtmlEncode(confModel.WebsiteName);
                    Page.Header.Controls.Add(Config.SetKeywords(Server.HtmlEncode(confModel.WebsiteKeywords)));
                    Page.Header.Controls.Add(Config.SetDescription(Server.HtmlEncode(confModel.WebsiteDescription)));
                }
            }
            else
            {
                Config.ShowEnd("Site Configuration Error!");
            }
        }
	}
}