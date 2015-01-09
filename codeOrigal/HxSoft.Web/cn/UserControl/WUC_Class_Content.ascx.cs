using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.cn.UserControl
{
    public partial class WUC_Class_Content : System.Web.UI.UserControl
    {
        private string _classid;
        /// <summary>
        /// 栏目ID
        /// </summary>
        public string ClassID
        {
            get { return _classid; }
            set { _classid = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ClassModel claModel = new ClassModel();
            claModel = Factory.Class().GetCacheInfo2(ClassID);
            if (claModel != null)
            {
                //litClassName.Text = claModel.ClassName;
                
                Page.Header.Title = Server.HtmlEncode(claModel.ClassName) + " - " + Page.Header.Title;
                //先清除母版页设置的keywords和description
                Page.Header.Controls.Remove(Page.Header.FindControl("keywords"));
                Page.Header.Controls.Remove(Page.Header.FindControl("description"));
                Page.Header.Controls.Add(Config.SetKeywords(Server.HtmlEncode(claModel.Keywords)));
                Page.Header.Controls.Add(Config.SetDescription(Server.HtmlEncode(claModel.Description)));
                //
                litClassContent.Text = claModel.ClassContent;
            }
        }
    }
}