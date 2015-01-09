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
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        //ҳ���ʼ��
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
        //        tr.Text = "��������";
        //        tr.NavigateUrl = "Site.aspx";
        //        tr.Target = "MainFrm";
        //        root.ChildNodes.AddAt(0, tr);
        //    }


        //    if (GetData.LimitChk("User") || GetData.LimitChk("UserLog") || GetData.LimitChk("Company"))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "��Ա����";
        //        tr.Expanded = false;
        //        root.ChildNodes.AddAt(0, tr);

        //        if (GetData.LimitChk("UserLog"))
        //        {
        //            TreeNode tr1 = new TreeNode();
        //            tr1.Text = "��Ա��־";
        //            tr1.NavigateUrl = "UserLog.aspx";
        //            tr1.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr1);
        //        }

        //        if (GetData.LimitChk("User"))
        //        {
        //            TreeNode tr2 = new TreeNode();
        //            tr2.Text = "��Ա����";
        //            tr2.NavigateUrl = "User.aspx";
        //            tr2.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr2);
        //        }

        //        if (GetData.LimitChk("Company"))
        //        {
        //            TreeNode tr3 = new TreeNode();
        //            tr3.Text = "��˾��Ϣ";
        //            tr3.NavigateUrl = "UserCompany.aspx";
        //            tr3.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr3);
        //        }
        //    }

        //    if (GetData.LimitChk("AdPosition") || GetData.LimitChk("Ad"))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "������";
        //        tr.Expanded = false;
        //        root.ChildNodes.AddAt(0, tr);

        //        if (GetData.LimitChk("Ad"))
        //        {
        //            TreeNode tr1 = new TreeNode();
        //            tr1.Text = "������";
        //            tr1.NavigateUrl = "Ad.aspx";
        //            tr1.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr1);
        //        }

        //        if (GetData.LimitChk("AdPosition"))
        //        {
        //            TreeNode tr2 = new TreeNode();
        //            tr2.Text = "���λ����";
        //            tr2.NavigateUrl = "AdPosition.aspx";
        //            tr2.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr2);
        //        }
        //    }

          

        //    if (GetData.LimitChk(""))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "������Ϣ����";
        //        tr.Expanded = false;

        //        root.ChildNodes.AddAt(0, tr);

        //        TreeNode tr1 = new TreeNode();
        //        tr1.Text = "����Ϣ";
        //        tr1.NavigateUrl = "AdminUserBuy.aspx";
        //        tr1.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr1);

        //        TreeNode tr2 = new TreeNode();
        //        tr2.Text = "��Ӧ��Ϣ";
        //        tr2.NavigateUrl = "AdminUserSupply.aspx";
        //        tr2.Target = "MainFrm";
              
        //        tr.ChildNodes.AddAt(0, tr2);

        //    }


        //    if (GetData.LimitChk(""))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "��������";
        //        tr.Expanded = false;
        //        root.ChildNodes.AddAt(0, tr);

        //        TreeNode tr1 = new TreeNode();
        //        tr1.Text = "������ҵ�񶩵�";
        //        tr1.NavigateUrl = "";
        //        tr1.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr1);

        //        TreeNode tr2 = new TreeNode();
        //        tr2.Text = "IC���۶���";
        //        tr2.NavigateUrl = "AdminICOrder.aspx";
        //        tr2.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr2);

        //        TreeNode tr3 = new TreeNode();
        //        tr3.Text = "IC��װ���Զ���";
        //        tr3.NavigateUrl = "";
        //        tr3.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr3);

        //        TreeNode tr4 = new TreeNode();
        //        tr4.Text = "��ҵ���޶���";
        //        tr4.NavigateUrl = "";
        //        tr4.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr4);
        //    }

        //    if (GetData.LimitChk(""))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "ѯ�۱��۹���";
        //        tr.Expanded = false;
        //        root.ChildNodes.AddAt(0, tr);

        //        TreeNode tr1 = new TreeNode();
        //        tr1.Text = "ѯ����Ϣ";
        //        tr1.NavigateUrl = "AdminUserAskPrice.aspx";
        //        tr1.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr1);

        //        TreeNode tr2 = new TreeNode();
        //        tr2.Text = "������Ϣ";
        //        tr2.NavigateUrl = "";
        //        tr2.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr2);
        //    }

        //    if (GetData.LimitChk(""))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "������������";
        //        tr.Expanded = false;
        //        root.ChildNodes.AddAt(0, tr);

        //        TreeNode tr1 = new TreeNode();
        //        tr1.Text = "���Թ���";
        //        tr1.NavigateUrl = "";
        //        tr1.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr1);

        //        TreeNode tr2 = new TreeNode();
        //        tr2.Text = "ר�ҹ���";
        //        tr2.NavigateUrl = "";
        //        tr2.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr2);


        //        TreeNode tr3 = new TreeNode();
        //        tr3.Text = "�������";
        //        tr3.NavigateUrl = "";
        //        tr3.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr3);


        //    }


        //    if (GetData.LimitChk("Product") || GetData.LimitChk("UserProduct"))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "��Ʒ����";
        //        tr.Expanded = false;
        //        root.ChildNodes.AddAt(0, tr);

        //        TreeNode tr1 = new TreeNode();
        //        tr1.Text = "���߲�Ʒ";
        //        tr1.NavigateUrl = "Product.aspx";
        //        tr1.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr1);

        //        TreeNode tr2 = new TreeNode();
        //        tr2.Text = "�û���Ʒ";
        //        tr2.NavigateUrl = "UserProduct.aspx";
        //        tr2.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr2);

        //    }

        //    if (GetData.LimitChk(""))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "���ݹ���";
        //        tr.Expanded = false;
        //        root.ChildNodes.AddAt(0, tr);

        //        TreeNode tr1 = new TreeNode();
        //        tr1.Text = "���¹���";
        //        tr1.NavigateUrl = "Article.aspx";
        //        tr1.Target = "MainFrm";
        //        tr.ChildNodes.AddAt(0, tr1);
        //    }

        //    if (GetData.LimitChk("Class"))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "��Ŀ����";
        //        tr.Expanded = false;
        //        tr.NavigateUrl = "Class.aspx";
        //        tr.Target = "MainFrm";
        //        root.ChildNodes.AddAt(0, tr);
        //    }

        //    if (GetData.LimitChk("Limit") || GetData.LimitChk("AdminGroup") || GetData.LimitChk("Admin") || GetData.LimitChk("AdminLog"))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "ϵͳ����";
        //        tr.Expanded = false;
        //        root.ChildNodes.AddAt(0, tr);

        //        if (GetData.LimitChk("AdminLog"))
        //        {
        //            TreeNode tr1 = new TreeNode();
        //            tr1.Text = "����Ա��־";
        //            tr1.NavigateUrl = "AdminLog.aspx";
        //            tr1.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr1);
        //        }

        //        if (GetData.LimitChk("Admin"))
        //        {
        //            TreeNode tr2 = new TreeNode();
        //            tr2.Text = "����Ա����";
        //            tr2.NavigateUrl = "Admin.aspx";
        //            tr2.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr2);

        //        }

        //        if (GetData.LimitChk("AdminGroup"))
        //        {
        //            TreeNode tr3 = new TreeNode();
        //            tr3.Text = "���������";
        //            tr3.NavigateUrl = "AdminGroup.aspx";
        //            tr3.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr3);
        //        }

        //        if (GetData.LimitChk("Limit"))
        //        {
        //            TreeNode tr4 = new TreeNode();
        //            tr4.Text = "Ȩ���ֶ�";
        //            tr4.NavigateUrl = "Limit.aspx";
        //            tr4.Target = "MainFrm";
        //            tr.ChildNodes.AddAt(0, tr4);
        //        }
        //    }


        //    if (GetData.LimitChk("Config"))
        //    {
        //        TreeNode tr = new TreeNode();
        //        tr.Text = "��վ����";
        //        tr.NavigateUrl = "Config.aspx";
        //        tr.Target = "MainFrm";
        //        root.ChildNodes.AddAt(0, tr);
        //    }

        //}





    }
}
