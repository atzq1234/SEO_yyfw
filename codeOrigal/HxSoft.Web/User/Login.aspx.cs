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

namespace HxSoft.Web.User
{
    public partial class Login : System.Web.UI.Page
    {
        //this page add by yang
        public string Url//返回地址
        {
            get
            {
                string strUrl = "Login.aspx";
                return Config.Request(Request.QueryString["Url"], strUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Factory.User().IsLogin())
            {
                lblUserName.Text = Factory.User().GetValueByField("UserName", Session["UserID"].ToString());
            }
        }

        //login
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string strUserName = txtUserName.Text.Trim();
            string strUserPass = txtUserPass.Text.Trim();
            string strVerifyCode = txtVerifyCode.Text.Trim();

            if (string.IsNullOrEmpty(strUserName))
            {
                errMsg.Text = "用户名不能为空!";
            }
            else if (string.IsNullOrEmpty(strUserPass))
            {
                errMsg.Text = "密码不能为空!";
            }
            else if (Session["VerifyCode"] == null)
            {
                errMsg.Text = "验证码有误!";
            }
            else if (strVerifyCode != Session["VerifyCode"].ToString())
            {
                errMsg.Text = "验证码有误!";
            }
            else
            {
                UserModel userModel = Factory.User().Login(strUserName, Config.md5(strUserPass));
                if (userModel == null)
                {
                    errMsg.Text = "用户名或密码有误!";
                }
                else
                {
                    if (userModel.IsClose == "1")
                    {
                        errMsg.Text = "帐号已被关闭!";
                    }
                    else if (userModel.IsAudit == "0")
                    {
                        errMsg.Text = "帐号未审核!";
                    }
                    else
                    {
                        Factory.UserLog().InsertLog("登录系统！", Session["UserID"].ToString());
                        //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('登录成功!');location.href='"+Url+"'", true);
                        Response.Redirect(Url);
                    }
                }
            }
        }
    }
}
