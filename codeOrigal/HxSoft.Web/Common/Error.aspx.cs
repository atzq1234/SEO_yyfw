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
using System.IO;
using HxSoft.Common;

namespace HxSoft.Web.Common
{
    public partial class Error : System.Web.UI.Page
    {
        public string act
        {
            get
            {
                return Config.Request(Request.QueryString["act"], "");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (act.Equals("s"))
            {
                Error_List();
            }
        }

        private void Error_List()
        {
            if (Directory.Exists(Server.MapPath("/error/")))
            {
                DirectoryInfo dir = new DirectoryInfo(Server.MapPath("/error/"));
                FileInfo[] arrFile = dir.GetFiles();
                repError.DataSource = arrFile;
                repError.DataBind();

            }
        }
    }
}
