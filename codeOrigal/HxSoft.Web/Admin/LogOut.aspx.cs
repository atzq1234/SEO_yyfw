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
    public partial class Logout : System.Web.UI.Page
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
            if (!Page.IsPostBack)
            {
                Factory.AdminLog().InsertLog("�˳�ϵͳ��", Session["AdminID"].ToString());
                HttpCookie UserCookie = new HttpCookie("AdminLoginInfo");
                UserCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(UserCookie);
                Session.Abandon();
                Response.Redirect("Login.aspx");
            }
        }
    }
}
