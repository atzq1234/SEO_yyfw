using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.IO;
using HxSoft.Common;


namespace HxSoft.Web
{
    public class Global : System.Web.HttpApplication
    {
        //this page add by yang
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        //����Ӧ�ó���Χ�Ĵ��������
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            Config.Err(ex);
        }
    }
}