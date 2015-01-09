using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.cn
{
    /// <summary>
    /// rss 的摘要说明
    /// </summary>
    public class rss : IHttpHandler
    {
        public string strClassID
        {
            get
            {
                return Config.RequestNumeric(HttpContext.Current.Request.QueryString["ClassID"], -1).ToString();
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/xml";
            context.Response.Charset = "utf-8";
            context.Response.Write(Factory.Article().RSS(strClassID).ToString());
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