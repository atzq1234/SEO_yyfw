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
    public partial class WUC_Left : System.Web.UI.UserControl
    {
        private string _parentid, _classid, _classpath;
        /// <summary>
        /// 主栏目ID
        /// </summary>
        public string ParentID
        {
            get { return _parentid; }
            set { _parentid = value; }
        }
        /// <summary>
        /// 子栏目ID
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
                litClassName.Text = "搜索结果";
                litClassTree.Text = "<ul><li><a>搜索结果</a></li></ul>";
            }
            else
            {
                //栏目名称
                ClassModel claModel = new ClassModel();
                claModel = Factory.Class().GetCacheInfo2(ParentID);
                if (claModel != null)
                {
                    litClassName.Text = claModel.ClassName;
                }

                //栏目列表
                //litClassTree.Text = Factory.Class().GetClassList(ParentID,  "selected", ClassID, 40,"").ToString();

                //递归取栏目列表
                if (string.IsNullOrEmpty(ClassPath))
                {
                    ClassPath = Factory.Class().GetPath(ClassID).ToString();
                }
                litClassTree.Text = Factory.Class().GetClassList(ParentID, "selected", ClassPath, 0, 0, "").ToString();
                litScript.Text = Factory.Class().GetClassBlock(ClassPath, 0).ToString();
            }
        }
    }
}