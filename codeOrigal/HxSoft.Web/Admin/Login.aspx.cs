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
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string Url//返回地址
        {
            get
            {
                string strUrl = Config.Request(Request.QueryString["Url"], "");
                return "Index_Index.aspx?Url=" + strUrl;
            }
        }
        //初始化页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }


        //登陆
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string strAdminName = txtAdminName.Text.Trim();
            string strAdminPass = txtAdminPass.Text.Trim();
            strAdminPass = Config.md5(strAdminPass);
            string strVerifyCode = txtVerifyCode.Text.Trim();

            if (Session["VerifyCode"] == null)
            {
                errMsg.Text = "验证码有误!";
            }
            else
            {
                if (strVerifyCode != Session["VerifyCode"].ToString())
                {
                    errMsg.Text = "验证码有误!";
                }
                else
                {
                    if (Factory.Admin().Login(strAdminName, strAdminPass))
                    {
                        Factory.AdminLog().InsertLog("登录系统！", Session["AdminID"].ToString());
                        Response.Redirect(Url);
                        //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('登录成功!');location.href='" + Url + "'", true);
                    }
                    else
                    {
                        errMsg.Text = "用户名或密码有误!";
                    }
                }
            }
        }

    }
}
