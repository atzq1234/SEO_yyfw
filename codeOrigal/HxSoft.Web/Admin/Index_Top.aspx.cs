using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.Admin
{
    public partial class Index_Top : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            if (!Page.IsPostBack)
            {
            }
        }

        public string Welcome()
        {
            if (Factory.Admin().IsLogin())
            {
                return "尊敬的" + Factory.Admin().GetValueByField("AdminName", Session["AdminID"].ToString()) + ",";
            }
            else
            {
                return "";
            }
        }

        public string Hello()
        {
            int t = DateTime.Now.Hour;
            if (t > 0 && t <= 6)
            {
                return "早上好!";
            }
            else if (t > 6 && t <= 12)
            {
                return "上午好!";
            }
            else if (t > 12 && t <= 14)
            {
                return "中午好!";
            }
            else if (t > 14 && t <= 18)
            {
                return "下午好!";
            }
            else if (t > 18 && t <= 24)
            {
                return "晚上好!";
            }
            else
            {
                return "";
            }
        }

        public string ShowAdminGroupName()
        {
            if (Factory.Admin().IsLogin())
            {
                return "所属管理组：" + Factory.AdminGroup().GetAdminGroupNames(Session["AdminID"].ToString());
            }
            else
            {
                return "";
            }
        }

    }
}
