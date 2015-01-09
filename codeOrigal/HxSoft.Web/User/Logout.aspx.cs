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
    public partial class Logout : System.Web.UI.Page
    {
        //this page add by yang
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.User().LoginChk();
            Factory.UserLog().InsertLog("ÍË³öÏµÍ³£¡", Session["UserID"].ToString());
            HttpCookie UserCookie = new HttpCookie("UserLoginInfo");
            UserCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(UserCookie);
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}
