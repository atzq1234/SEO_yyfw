using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.SessionState;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.Common.Ajax
{
    //this page add by yang
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Ajax_CheckUserName : IHttpHandler
    {
        public string strUserName
        {
            get 
            {
                return Config.Request(HttpContext.Current.Request.Form["txtUserName"], "");
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (strUserName == string.Empty)
            {
                context.Response.Write("0");
            }
            else
            {
                if (Factory.User().CheckInfo("UserName", strUserName))
                {
                    context.Response.Write("0");
                }
                else
                {
                    context.Response.Write("1");
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
