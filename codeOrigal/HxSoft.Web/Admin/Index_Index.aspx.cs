using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.Admin
{
    public partial class Index_Index : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string Url//返回地址
        {
            get
            {
                string strUrl = Config.Request(Request.QueryString["Url"], "Index_Main.aspx");
                if (strUrl.IndexOf("Index_Index.aspx") > -1)
                {
                    return "Index_Main.aspx";
                }
                else
                {
                    return strUrl;
                }
            }
        }
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            if (!Page.IsPostBack)
            {
                Config.CheckFolder(Server.MapPath(Config.FileUploadPath));
                Config.CheckFolder(Server.MapPath(Config.FileUploadPath + "UploadFiles/"));
            }
        }
    }
}
