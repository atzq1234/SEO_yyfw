using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using HxSoft.Common;

namespace HxSoft.Web.Admin.Ajax
{
    /// <summary>
    /// Ajax_GetPinYin 的摘要说明
    /// </summary>
    public class Ajax_GetPinYin : IHttpHandler
    {
        public string strClassName
        {
            get
            {
                if (HttpContext.Current.Request.Form["txtClassName"] != null)
                    return Config.HTMLClear(HttpContext.Current.Request.Form["txtClassName"].ToString());
                else
                    return "";
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(CHS2PinYin.Convert(strClassName,"",false));
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