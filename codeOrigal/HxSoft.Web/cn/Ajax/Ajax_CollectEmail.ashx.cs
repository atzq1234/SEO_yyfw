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

namespace HxSoft.Web.cn.Ajax
{
    //this page add by yang
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Ajax_CollectEmail : IHttpHandler
    {
        public string strEmail
        {
            get
            {
                return Config.Request(HttpContext.Current.Request.Form["Email"], "");
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (strEmail == string.Empty)
            {
                context.Response.Write("请输入您的邮箱");
            }
            else if (!Config.IsMail(strEmail))
            {
                context.Response.Write("请输入您的邮箱");
            }
            else
            {
                MailModel mailModel = new MailModel();
                mailModel.MailAddress = strEmail;
                mailModel.IsRec = "1";
                mailModel.AddTime = DateTime.Now.ToString();
                if (Factory.Mail().CheckInfo("MailAddress", mailModel.MailAddress))
                {
                    context.Response.Write("该邮件地址已成功订阅!");
                }
                else
                {
                    Factory.Mail().InsertInfo(mailModel);
                    context.Response.Write("订阅成功!");
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
