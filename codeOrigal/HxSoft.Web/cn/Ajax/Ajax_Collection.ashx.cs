using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.cn.Ajax
{
    /// <summary>
    /// Ajax_Collection 的摘要说明
    /// </summary>
    public class Ajax_Collection : IHttpHandler, IRequiresSessionState
    {
        public string strTitle
        {
            get
            {
                return Config.Request(HttpContext.Current.Request.Form["Title"], "");
            }
        }

        public string strUrl
        {
            get
            {
                return Config.Request(HttpContext.Current.Request.Form["Url"], "");
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (!Factory.User().IsLogin())
            {
                context.Response.Write("请先登陆!");
            }
            else
            {
                CollectionModel colModel = new CollectionModel();
                colModel.Title = strTitle;
                colModel.Url = strUrl.ToLower();
                colModel.UserID = context.Session["UserID"].ToString();
                colModel.AddTime = DateTime.Now.ToString();
                if (Factory.Collection().IsCollect(colModel.Url, colModel.UserID))
                {
                    context.Response.Write("该文章已经收藏!");
                }
                else
                {
                    Factory.Collection().InsertInfo(colModel);
                    Factory.UserLog().InsertLog("收藏文章!", context.Session["UserID"].ToString());
                    context.Response.Write("收藏成功!");
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