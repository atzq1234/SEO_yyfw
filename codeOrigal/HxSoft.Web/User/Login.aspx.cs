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
        public string Url//���ص�ַ
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
                errMsg.Text = "�û�������Ϊ��!";
            }
            else if (string.IsNullOrEmpty(strUserPass))
            {
                errMsg.Text = "���벻��Ϊ��!";
            }
            else if (Session["VerifyCode"] == null)
            {
                errMsg.Text = "��֤������!";
            }
            else if (strVerifyCode != Session["VerifyCode"].ToString())
            {
                errMsg.Text = "��֤������!";
            }
            else
            {
                UserModel userModel = Factory.User().Login(strUserName, Config.md5(strUserPass));
                if (userModel == null)
                {
                    errMsg.Text = "�û�������������!";
                }
                else
                {
                    if (userModel.IsClose == "1")
                    {
                        errMsg.Text = "�ʺ��ѱ��ر�!";
                    }
                    else if (userModel.IsAudit == "0")
                    {
                        errMsg.Text = "�ʺ�δ���!";
                    }
                    else
                    {
                        Factory.UserLog().InsertLog("��¼ϵͳ��", Session["UserID"].ToString());
                        //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('��¼�ɹ�!');location.href='"+Url+"'", true);
                        Response.Redirect(Url);
                    }
                }
            }
        }
    }
}
