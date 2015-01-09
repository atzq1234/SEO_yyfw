using System;
using System.Collections.Generic;
using System.Web;
using HxSoft.Common;
using System.IO;
namespace HxSoft.Web.Admin.Ajax
{
    /// <summary>
    /// Ajax_File_IsExits 的摘要说明
    /// </summary>
    public class Ajax_File_IsExits : IHttpHandler
    {
        public string strFolderPath
        {
            get
            {
                if (HttpContext.Current.Request.Form["FolderPath"] != null)
                    return Config.FolderNameReplace(HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Form["FolderPath"].ToString()));
                else
                    return Config.FileUploadPath;
            }
        }
        public string strFileName
        {
            get
            {
                if (HttpContext.Current.Request.Form["FileName"] != null)
                    return Config.FileNameReplace(HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Form["FileName"].ToString()));
                else
                    return "";
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Config.CheckFolder(HttpContext.Current.Server.MapPath(Config.FileUploadPath));
            Config.CheckFolder(HttpContext.Current.Server.MapPath(strFolderPath));

            try
            {
                string strFilePath = strFolderPath + strFileName;
                if (File.Exists(HttpContext.Current.Server.MapPath(strFilePath)))
                {
                    Config.ShowEnd("1");
                }
                else
                {
                    Config.ShowEnd("0");
                }
            }
            catch(Exception ex)
            {
                Config.Err(ex);
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