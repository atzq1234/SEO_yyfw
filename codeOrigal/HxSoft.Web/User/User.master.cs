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

namespace HxSoft.Web.User
{
    public partial class User : System.Web.UI.MasterPage
    {
        //this page add by yang
        protected void Page_Init(object sender, EventArgs e)
        {
            Factory.User().LoginChk();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
