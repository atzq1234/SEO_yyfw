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

namespace HxSoft.Web.User.Account
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        //this page add by yang
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowInfo();
        }

        //�û���Ϣ
        protected void ShowInfo()
        {
            UserModel userModel = new UserModel();
            userModel = Factory.User().GetInfo(Session["UserID"].ToString());
            if (userModel != null)
            {
                lblUserName.Text = userModel.UserName;
            }
            else
            {
                Config.MsgGoBack("�û�������!");
            }
        }

        //modify
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strOldUserPass = txtOldUserPass.Text.Trim();
            string strNewUserPass = txtNewUserPass.Text.Trim();
            strOldUserPass = Config.md5(strOldUserPass);
            strNewUserPass = Config.md5(strNewUserPass);
            UserModel userModel = Factory.User().GetInfo(Session["UserID"].ToString());
            if (userModel != null)
            {
                if (userModel.UserPass != strOldUserPass)
                {
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('�����벻��ȷ!');$('#" + txtOldUserPass.ClientID + "').focus();", true);
                    errMsg.Text = "�����벻��ȷ!";
                }
                else
                {
                    userModel.UserPass = strNewUserPass;
                    Factory.User().SetPass(userModel, Session["UserID"].ToString());
                    Factory.UserLog().InsertLog("�޸����룡", Session["UserID"].ToString());
                    HttpCookie UserCookie = new HttpCookie("UserLoginInfo");
                    UserCookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(UserCookie);
                    Session.Abandon();
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('�޸ĳɹ�! �����µ�¼!');location.href='../Login.aspx'", true);
                    Config.MsgGotoUrl("�޸ĳɹ�! �����µ�¼!",Request.UrlReferrer.ToString());
                }
            }
        }


    }
}
