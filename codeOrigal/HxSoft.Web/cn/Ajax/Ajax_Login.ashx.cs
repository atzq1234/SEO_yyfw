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
using System.IO;
using Newtonsoft.Json;

namespace HxSoft.Web.cn.Ajax
{
    //this page add by yang
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Ajax_Login : IHttpHandler, IRequiresSessionState
    {
        public string strUserName
        {
            get
            {
                if (HttpContext.Current.Request.Form["UserName"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Request.Form["UserName"].ToString();
                }
            }
        }
        public string strUserPass
        {
            get
            {
                if (HttpContext.Current.Request.Form["UserPass"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Request.Form["UserPass"].ToString();
                }
            }
        }
        public string strIsCheck
        {
            get
            {
                if (HttpContext.Current.Request.Form["IsCheck"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Request.Form["IsCheck"].ToString();
                }
            }
        }
        public string strVerifyCode
        {
            get
            {
                if (HttpContext.Current.Request.Form["VerifyCode"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Request.Form["VerifyCode"].ToString();
                }
            }
        }
        public string Url//返回地址
        {
            get
            {
                string strUrl = "user_info.aspx";
                if (HttpContext.Current.Request.Form["Url"] == null)
                {
                    return strUrl;
                }
                else
                {
                    if (HttpContext.Current.Request.Form["Url"].ToString() != string.Empty)
                    {
                        strUrl = HttpContext.Current.Request.Form["Url"].ToString();
                    }
                    return strUrl;
                }
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            StringWriter sw = new StringWriter();
            JsonTextWriter tw = new JsonTextWriter(sw);
            tw.WriteStartObject();
            if (HttpContext.Current.Session["VerifyCode"] == null && strIsCheck=="1")
            {
                tw.WritePropertyName("status");
                tw.WriteValue(0);
                tw.WritePropertyName("msg");
                tw.WriteValue("验证码有误");
                tw.WritePropertyName("obj");
                tw.WriteValue("VerifyCode");
            }
            else
            {
                if (strUserName == string.Empty)
                {
                    tw.WritePropertyName("status");
                    tw.WriteValue(0);
                    tw.WritePropertyName("msg");
                    tw.WriteValue("请输入帐号");
                    tw.WritePropertyName("obj");
                    tw.WriteValue("UserName");
                }
                else if (strUserPass == string.Empty)
                {
                    tw.WritePropertyName("status");
                    tw.WriteValue(0);
                    tw.WritePropertyName("msg");
                    tw.WriteValue("请输入密码");
                    tw.WritePropertyName("obj");
                    tw.WriteValue("UserPass");
                }
                else if (strVerifyCode == string.Empty && strIsCheck == "1")
                {
                    tw.WritePropertyName("status");
                    tw.WriteValue(0);
                    tw.WritePropertyName("msg");
                    tw.WriteValue("请输入验证码");
                    tw.WritePropertyName("obj");
                    tw.WriteValue("VerifyCode");
                }
                else if (strVerifyCode != HttpContext.Current.Session["VerifyCode"].ToString() && strIsCheck == "1")
                {
                    tw.WritePropertyName("status");
                    tw.WriteValue(0);
                    tw.WritePropertyName("msg");
                    tw.WriteValue("验证码有误");
                    tw.WritePropertyName("obj");
                    tw.WriteValue("VerifyCode");
                }
                else
                {
                    UserModel userModel = Factory.User().Login(strUserName, Config.md5(strUserPass));
                    if (userModel != null)
                    {
                        if (userModel.IsClose == "1")
                        {
                            tw.WritePropertyName("status");
                            tw.WriteValue(0);
                            tw.WritePropertyName("msg");
                            tw.WriteValue("帐号已被关闭");
                            tw.WritePropertyName("obj");
                            tw.WriteValue("");
                        }
                        else
                        {
                            if (userModel.IsAudit == "0")
                            {
                                tw.WritePropertyName("status");
                                tw.WriteValue(0);
                                tw.WritePropertyName("msg");
                                tw.WriteValue("帐号未审核");
                                tw.WritePropertyName("obj");
                                tw.WriteValue("");
                            }
                            else
                            {
                                tw.WritePropertyName("status");
                                tw.WriteValue(1);
                                tw.WritePropertyName("msg");
                                tw.WriteValue(Url);
                                tw.WritePropertyName("obj");
                                tw.WriteValue("");
                                Factory.UserLog().InsertLog("登录系统！", HttpContext.Current.Session["UserID"].ToString());
                            }
                        }
                    }
                    else
                    {
                        tw.WritePropertyName("status");
                        tw.WriteValue(0);
                        tw.WritePropertyName("msg");
                        tw.WriteValue("邮件地址或密码有误");
                        tw.WritePropertyName("obj");
                        tw.WriteValue("");
                    }
                }
            }
            tw.WriteEndObject();
            tw.Flush();
            string json = sw.ToString();
            context.Response.Write(json);
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
