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
using HxSoft.Common;

namespace HxSoft.Web.Common
{
    public partial class Demo : System.Web.UI.Page
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
            if (act.Equals("en"))
            {
                Panel1.Visible = true;
            }
            else
            {
                Panel1.Visible = false;
            }
            if (act.Equals("de"))
            {
                Panel2.Visible = true;
            }
            else
            {
                Panel2.Visible = false;
            }
        }

        protected void btnEncrypt_Click(object sender, EventArgs e)
        {
            string strEncrypt = txtEncrypt.Text;
            if (strEncrypt != string.Empty)
            {
                txtEncrypt.Text = Config.Encrypt(strEncrypt);
            }
        }

        protected void btnDecrypt_Click(object sender, EventArgs e)
        {
            string strDecrypt = txtDecrypt.Text;
            if (strDecrypt != string.Empty)
            {
                txtDecrypt.Text = Config.Decrypt(strDecrypt);
            }
        }
    }
}
