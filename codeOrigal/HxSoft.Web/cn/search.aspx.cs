using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;
using HxSoft.Web.cn.UserControl;
using Newtonsoft.Json;
using System.IO;

namespace HxSoft.Web.cn
{
    /// <summary>
    /// 栏目判断页
    /// </summary>
    public partial class search : System.Web.UI.Page
    {
        /// <summary>
        /// 栏目ID
        /// </summary>
        public string ClassID
        {
            get
            {
                return "-1";
            }
        }
        /// <summary>
        /// 栏目路径
        /// </summary>
        public string ClassPath
        {
            get
            {
                return "-1";
            }
        }
        /// <summary>
        /// 主栏目ID
        /// </summary>
        public string ParentID
        {
            get
            {
                return "-1";
            }
        }
        /// <summary>
        /// 当前主栏目ID
        /// </summary>
        public string CurrentParentID
        {
            get
            {
                return "-1";
            }
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            WUC_Header WUC_Header1 = (WUC_Header)this.Master.FindControl("WUC_Header1");
            WUC_Header1.CurrentParentID = CurrentParentID;
            //WUC_Banner1.ParentID = ParentID;
            //WUC_Banner1.ClassID = ClassID;
            WUC_Left1.ParentID = ParentID;
            WUC_Left1.ClassID = ClassID;
            WUC_Left1.ClassPath = ClassPath;
            WUC_Nav1.ClassID = ClassID;
            WUC_Nav1.ClassPath = ClassPath;
        }
    }
}