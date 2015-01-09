using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.SessionState;
using HxSoft.Common;

namespace HxSoft.Web.Common.Ajax
{
    //this page add by yang
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Ajax_CheckVerifyCode : IHttpHandler, IRequiresSessionState
    {
        public string strVerifyCode
        {
            get
            {
                return Config.Request(HttpContext.Current.Request.Form["txtVerifyCode"], "");
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (strVerifyCode == string.Empty)
            {
                context.Response.Write("0");
            }
            else
            {
                if (context.Session["VerifyCode"] == null)
                {
                    context.Response.Write("0");
                }
                else
                {
                    if (context.Session["VerifyCode"].ToString() == string.Empty)
                    {
                        context.Response.Write("0");
                    }
                    else
                    {
                        if (strVerifyCode != context.Session["VerifyCode"].ToString())
                        {
                            context.Response.Write("0");
                        }
                        else
                        {
                            context.Response.Write("1");
                        }
                    }
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
