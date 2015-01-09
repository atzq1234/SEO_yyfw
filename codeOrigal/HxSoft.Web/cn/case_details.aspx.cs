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

namespace HxSoft.Web.cn
{
    public partial class case_details : System.Web.UI.Page
    {
        public string ClassID
        {
            get
            {
                return WUC_Product_Details1.ClassID;
;
            }
        }
        public string ClassPath
        {
            get
            {
                return Factory.Class().GetPath(ClassID).ToString();
            }
        }
        public string ParentID
        {
            get
            {
                return Factory.Class().GetClassIDByPath(ClassPath, 0, ClassID);
            }
        }
        public string CurrentParentID
        {
            get
            {
                return Factory.Class().GetClassIDByPath(ClassPath, 0, ClassID);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            WUC_Header WUC_Header1 = (WUC_Header)this.Master.FindControl("WUC_Header1");
            WUC_Header1.CurrentParentID = CurrentParentID;
           // WUC_Banner1.ParentID = ParentID;
           // WUC_Banner1.ClassID = ClassID;
           // WUC_Left1.ParentID = ParentID;
            //WUC_Left1.ClassID = ClassID;
            //WUC_Left1.ClassPath = ClassPath;
            //WUC_Nav1.ClassID = ClassID;
           // WUC_Nav1.ClassPath = ClassPath;
        }
    }
}