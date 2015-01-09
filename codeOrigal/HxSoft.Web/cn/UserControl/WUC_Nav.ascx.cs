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
    public partial class WUC_Nav : System.Web.UI.UserControl
    {
        private string _classid,_classpath;
        /// <summary>
        /// 栏目ID
        /// </summary>
        public string ClassID
        {
            get { return _classid; }
            set { _classid = value; }
        }
        /// <summary>
        /// 栏目路径
        /// </summary>
        public string ClassPath
        {
            get { return _classpath; }
            set { _classpath = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClassID == "-1")
            {
                litClassNav.Text = "搜索结果";
            }
            else
            {
                //递归栏目导航
                if (string.IsNullOrEmpty(ClassPath))
                {
                    ClassPath = Factory.Class().GetPath(ClassID).ToString();
                }
                litClassNav.Text = Factory.Class().GetClassNav(ClassPath, 0, " &gt; ").ToString();
            }
        }
    }
}