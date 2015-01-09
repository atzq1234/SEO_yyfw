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
    public partial class WUC_Class_Content_Part : System.Web.UI.UserControl
    {
        //this page add by yang
        private string _classid;
        private int _contentnum = 0;
        private bool _isshowhtml = false;
        /// <summary>
        /// 分类ID
        /// </summary>
        public string ClassID
        {
            get { return _classid; }
            set { _classid = value; }
        }
        /// <summary>
        ///显示内容字数(默认显示全部内容)
        /// </summary>
        public int ContentNum
        {
            get { return _contentnum; }
            set { _contentnum = value; }
        }
        /// <summary>
        ///显示内容是否包括HTML代码(默认不包括HTML代码)
        /// </summary>
        public bool IsShowHTML
        {
            get { return _isshowhtml; }
            set { _isshowhtml = value; }
        }

        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            //详细内容
            ClassModel claModel = new ClassModel();
            claModel = Factory.Class().GetCacheInfo2(ClassID);
            if (claModel != null)
            {
                if (IsShowHTML)
                {
                    litClassContent.Text = Config.ShowPartStr(claModel.ClassContent, ContentNum);
                }
                else
                {
                    litClassContent.Text = Config.ShowPartStr(Config.HTMLRemove(claModel.ClassContent), ContentNum);
                }
            }
        }
    }
}