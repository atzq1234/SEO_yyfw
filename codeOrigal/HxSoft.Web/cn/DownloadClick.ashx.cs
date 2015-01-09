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
    /// AdClick 的摘要说明
    /// </summary>
    public class DownloadClick : IHttpHandler
    {
        public string DownloadID
        {
            get
            {
                return Config.RequestNumeric(HttpContext.Current.Request.QueryString["DownloadID"], -1).ToString();
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            DownloadModel dowModel = new DownloadModel();
            dowModel = Factory.Download().GetInfo2(DownloadID);
            if (dowModel != null)
            {
                Factory.Download().Click(DownloadID);
                context.Response.Redirect(dowModel.DownUrl);
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