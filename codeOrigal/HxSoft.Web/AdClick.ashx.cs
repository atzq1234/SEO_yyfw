using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web
{
    /// <summary>
    /// AdClick 的摘要说明
    /// </summary>
    public class AdClick : IHttpHandler
    {
        /// <summary>
        /// 广告ID
        /// </summary>
        public string AdID
        {
            get
            {
                if (!Config.IsNumeric(HttpContext.Current.Request.QueryString["AdID"]))
                {
                    return "0";
                }
                else
                {
                    return HttpContext.Current.Request.QueryString["AdID"].ToString();
                }
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            AdModel adModel = new AdModel();
            adModel = Factory.Ad().GetInfo2(AdID);
            if (adModel != null)
            {
                Factory.Ad().Click(AdID);
                context.Response.Redirect(adModel.AdLink);
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