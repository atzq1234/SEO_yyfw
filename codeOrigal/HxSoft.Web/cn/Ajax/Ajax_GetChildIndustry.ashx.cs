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
    public class Ajax_GetChildIndustry : IHttpHandler
    {
        public string ParentID
        {
            get
            {
                return Config.RequestNumeric(HttpContext.Current.Request.Form["ParentID"], -1).ToString();
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            StringBuilder strOption = new StringBuilder("<option value=\"-1\">请选择</option>\n");
            strOption.Append(Factory.Industry().AjaxGetIndustryList(ParentID,"select",""));
            context.Response.Write(strOption.ToString());
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
