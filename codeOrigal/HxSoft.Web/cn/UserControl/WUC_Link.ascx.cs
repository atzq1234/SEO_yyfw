using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HxSoft.ClassFactory;
using HxSoft.Common;
using HxSoft.Model;

namespace HxSoft.Web.cn.UserControl
{
    public partial class WUC_Link : System.Web.UI.UserControl
    {
        private string _classid,_configid;
        /// <summary>
        /// 分类ID
        /// </summary>
        public string ClassID
        {
            get { return _classid; }
            set { _classid = value; }
        }

        /// <summary>
        /// 站点ID
        /// </summary>
        public string ConfigID
        {
            get { return _configid; }
            set { _configid = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //栏目名称
            ClassModel claModel = new ClassModel();
            claModel = Factory.Class().GetCacheInfo2(ClassID);
            if (claModel != null)
            {
                litClassName.Text = claModel.ClassName;

                Page.Header.Title = Server.HtmlEncode(claModel.ClassName) + " - " + Page.Header.Title;
                //先清除母版页设置的keywords和description
                Page.Header.Controls.Remove(Page.Header.FindControl("keywords"));
                Page.Header.Controls.Remove(Page.Header.FindControl("description"));
                Page.Header.Controls.Add(Config.SetKeywords(Server.HtmlEncode(claModel.Keywords)));
                Page.Header.Controls.Add(Config.SetDescription(Server.HtmlEncode(claModel.Description)));
            }

            Link_Bind("2", repLinkPic);
            Link_Bind("1", repLinkText);
        }

        //列表绑定
        protected void Link_Bind(string strTypeID, Repeater rep)
        {
            string sql = "select * from t_Link where IsClose=0 and ConfigID=" + ConfigID + " and TypeID=" + strTypeID + " order by ListID asc";
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.Repeater.ToString(), rep);
        }
    }
}