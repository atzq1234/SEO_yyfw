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
    public partial class Index_Left : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                // CreateTree();  
               
            }

        }

     

        //private void CreateTree()
        //{
        //    TreeNode root = TreeView1.Nodes[0];


        //    if (GetData.LimitChk("Site"))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "友情链接";
        //        tr.NavigateUrl = "Site.aspx";
        //        tr.Target = "MainFrm";
        //        root.ChildNodes.AddAt(0, tr);
        //    }


        //    if (GetData.LimitChk("User") || GetData.LimitChk("UserLog") || GetData.LimitChk("Company"))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "会员管理";
        //        tr.Expanded = false;
        //        root.ChildNodes.AddAt(0, tr);

        //        if (GetData.LimitChk("UserLog"))
        //        {
        //            TreeNode tr1 = new TreeNode();
        //            tr1.Text = "会员日志";
        //            tr1.NavigateUrl = "UserLog.aspx";
        //            tr1.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr1);
        //        }

        //        if (GetData.LimitChk("User"))
        //        {
        //            TreeNode tr2 = new TreeNode();
        //            tr2.Text = "会员管理";
        //            tr2.NavigateUrl = "User.aspx";
        //            tr2.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr2);
        //        }

        //        if (GetData.LimitChk("Company"))
        //        {
        //            TreeNode tr3 = new TreeNode();
        //            tr3.Text = "公司信息";
        //            tr3.NavigateUrl = "UserCompany.aspx";
        //            tr3.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr3);
        //        }
        //    }

        //    if (GetData.LimitChk("AdPosition") || GetData.LimitChk("Ad"))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "广告管理";
        //        tr.Expanded = false;
        //        root.ChildNodes.AddAt(0, tr);

        //        if (GetData.LimitChk("Ad"))
        //        {
        //            TreeNode tr1 = new TreeNode();
        //            tr1.Text = "广告管理";
        //            tr1.NavigateUrl = "Ad.aspx";
        //            tr1.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr1);
        //        }

        //        if (GetData.LimitChk("AdPosition"))
        //        {
        //            TreeNode tr2 = new TreeNode();
        //            tr2.Text = "广告位管理";
        //            tr2.NavigateUrl = "AdPosition.aspx";
        //            tr2.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr2);
        //        }
        //    }

          

        //    if (GetData.LimitChk(""))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "供求信息管理";
        //        tr.Expanded = false;

        //        root.ChildNodes.AddAt(0, tr);

        //        TreeNode tr1 = new TreeNode();
        //        tr1.Text = "求购信息";
        //        tr1.NavigateUrl = "AdminUserBuy.aspx";
        //        tr1.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr1);

        //        TreeNode tr2 = new TreeNode();
        //        tr2.Text = "供应信息";
        //        tr2.NavigateUrl = "AdminUserSupply.aspx";
        //        tr2.Target = "MainFrm";
              
        //        tr.ChildNodes.AddAt(0, tr2);

        //    }


        //    if (GetData.LimitChk(""))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "订单管理";
        //        tr.Expanded = false;
        //        root.ChildNodes.AddAt(0, tr);

        //        TreeNode tr1 = new TreeNode();
        //        tr1.Text = "进出口业务订单";
        //        tr1.NavigateUrl = "";
        //        tr1.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr1);

        //        TreeNode tr2 = new TreeNode();
        //        tr2.Text = "IC销售订单";
        //        tr2.NavigateUrl = "AdminICOrder.aspx";
        //        tr2.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr2);

        //        TreeNode tr3 = new TreeNode();
        //        tr3.Text = "IC封装测试订单";
        //        tr3.NavigateUrl = "";
        //        tr3.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr3);

        //        TreeNode tr4 = new TreeNode();
        //        tr4.Text = "物业租赁订单";
        //        tr4.NavigateUrl = "";
        //        tr4.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr4);
        //    }

        //    if (GetData.LimitChk(""))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "询价报价管理";
        //        tr.Expanded = false;
        //        root.ChildNodes.AddAt(0, tr);

        //        TreeNode tr1 = new TreeNode();
        //        tr1.Text = "询价信息";
        //        tr1.NavigateUrl = "AdminUserAskPrice.aspx";
        //        tr1.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr1);

        //        TreeNode tr2 = new TreeNode();
        //        tr2.Text = "报价信息";
        //        tr2.NavigateUrl = "";
        //        tr2.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr2);
        //    }

        //    if (GetData.LimitChk(""))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "互动交流管理";
        //        tr.Expanded = false;
        //        root.ChildNodes.AddAt(0, tr);

        //        TreeNode tr1 = new TreeNode();
        //        tr1.Text = "留言管理";
        //        tr1.NavigateUrl = "";
        //        tr1.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr1);

        //        TreeNode tr2 = new TreeNode();
        //        tr2.Text = "专家管理";
        //        tr2.NavigateUrl = "";
        //        tr2.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr2);


        //        TreeNode tr3 = new TreeNode();
        //        tr3.Text = "问题管理";
        //        tr3.NavigateUrl = "";
        //        tr3.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr3);


        //    }


        //    if (GetData.LimitChk("Product") || GetData.LimitChk("UserProduct"))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "产品管理";
        //        tr.Expanded = false;
        //        root.ChildNodes.AddAt(0, tr);

        //        TreeNode tr1 = new TreeNode();
        //        tr1.Text = "赛高产品";
        //        tr1.NavigateUrl = "Product.aspx";
        //        tr1.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr1);

        //        TreeNode tr2 = new TreeNode();
        //        tr2.Text = "用户产品";
        //        tr2.NavigateUrl = "UserProduct.aspx";
        //        tr2.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr2);

        //    }

        //    if (GetData.LimitChk(""))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "内容管理";
        //        tr.Expanded = false;
        //        root.ChildNodes.AddAt(0, tr);

        //        TreeNode tr1 = new TreeNode();
        //        tr1.Text = "文章管理";
        //        tr1.NavigateUrl = "Article.aspx";
        //        tr1.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr1);
        //    }

        //    if (GetData.LimitChk("Class"))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "栏目管理";
        //        tr.Expanded = false;
        //        tr.NavigateUrl = "Class.aspx";
        //        tr.Target = "MainFrm";
        //        root.ChildNodes.AddAt(0, tr);
        //    }

        //    if (GetData.LimitChk("Limit") || GetData.LimitChk("AdminGroup") || GetData.LimitChk("Admin") || GetData.LimitChk("AdminLog"))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "系统管理";
        //        tr.Expanded = false;
        //        root.ChildNodes.AddAt(0, tr);

        //        if (GetData.LimitChk("AdminLog"))
        //        {
        //            TreeNode tr1 = new TreeNode();
        //            tr1.Text = "管理员日志";
        //            tr1.NavigateUrl = "AdminLog.aspx";
        //            tr1.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr1);
        //        }

        //        if (GetData.LimitChk("Admin"))
        //        {
        //            TreeNode tr2 = new TreeNode();
        //            tr2.Text = "管理员管理";
        //            tr2.NavigateUrl = "Admin.aspx";
        //            tr2.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr2);

        //        }

        //        if (GetData.LimitChk("AdminGroup"))
        //        {
        //            TreeNode tr3 = new TreeNode();
        //            tr3.Text = "管理组管理";
        //            tr3.NavigateUrl = "AdminGroup.aspx";
        //            tr3.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr3);
        //        }

        //        if (GetData.LimitChk("Limit"))
        //        {
        //            TreeNode tr4 = new TreeNode();
        //            tr4.Text = "权限字段";
        //            tr4.NavigateUrl = "Limit.aspx";
        //            tr4.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr4);
        //        }
        //    }


        //    if (GetData.LimitChk("Config"))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "网站配置";
        //        tr.NavigateUrl = "Config.aspx";
        //        tr.Target = "MainFrm";
        //        root.ChildNodes.AddAt(0, tr);
        //    }

        //}





    }
}
