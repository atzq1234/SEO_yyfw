using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.cn.UserControl
{
    public partial class WUC_Sitemap : System.Web.UI.UserControl
    {
        private string _configid,_classid;
        /// <summary>
        /// 配置ID
        /// </summary>
        public string ConfigID
        {
            get { return _configid; }
            set { _configid = value; }
        }
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
                litClassName.Text = claModel.ClassName;

                Page.Header.Title = Server.HtmlEncode(claModel.ClassName) + " - " + Page.Header.Title;
                //先清除母版页设置的keywords和description
                Page.Header.Controls.Remove(Page.Header.FindControl("keywords"));
                Page.Header.Controls.Remove(Page.Header.FindControl("description"));
                Page.Header.Controls.Add(Config.SetKeywords(Server.HtmlEncode(claModel.Keywords)));
                Page.Header.Controls.Add(Config.SetDescription(Server.HtmlEncode(claModel.Description)));
            }

            //网站地图
            Factory.Acc().DataBind("select ClassID,ClassName,ClassEnName,LinkUrl from t_Class where ParentID=0 and ConfigID=" + ConfigID + " and IsClose=0 order by ListID asc", null, Config.DataBindObjTypeCollection.Repeater.ToString(), repSitemap1);
        }

        protected void repSitemap1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            string strParentID = drv["ClassID"].ToString();
            string sql = "select ClassID,ClassName,ClassEnName,LinkUrl from t_Class where ParentID=" + strParentID + " and ConfigID=" + ConfigID + " and IsClose=0 order by ListID asc";
            Repeater repSitemap2 = (Repeater)e.Item.FindControl("repSitemap2");
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.Repeater.ToString(), repSitemap2);
        }

        public string GetUrl(string strLinkUrl,string strClassEnName)
        {
            if (strLinkUrl != string.Empty)
            {
                return strLinkUrl;
            }
            else
            {
                return strClassEnName + Config.FileExt;
            }
        }
    }
}