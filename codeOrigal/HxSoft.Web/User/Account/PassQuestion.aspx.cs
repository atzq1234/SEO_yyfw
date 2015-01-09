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
    public partial class PassQuestion : System.Web.UI.Page
    {
        //this page add by yang
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowInfo();
        }

        //用户信息
        protected void ShowInfo()
        {
            UserModel userModel = new UserModel();
            userModel = Factory.User().GetInfo(Session["UserID"].ToString());
            if (userModel != null)
            {
                lblUserName.Text = userModel.UserName;
                lblPassQuestion.Text = userModel.PassQuestion;
                txtNewPassQuestion.Text = userModel.PassQuestion;
            }
            else
            {
                Config.MsgGoBack("用户不存在!");
            }
        }

        //modify
        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserModel userModel = new UserModel();
            string strOldPassAnswer = txtOldPassAnswer.Text;
            userModel = Factory.User().GetInfo(Session["UserID"].ToString());
            if (userModel != null)
            {
                if (userModel.PassAnswer != strOldPassAnswer)
                {
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('原问题答案不正确!');$('#" + txtOldPassAnswer.ClientID + "').focus();", true);
                    errMsg.Text = "原问题答案不正确!";
                }
                else
                {
                    userModel.PassQuestion = txtNewPassQuestion.Text.Trim();
                    userModel.PassAnswer = txtNewPassAnswer.Text.Trim();
                    Factory.User().UpdatePassQuestion(userModel, Session["UserID"].ToString());
                    Factory.UserLog().InsertLog("修改密码保护资料！", Session["UserID"].ToString());
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('修改成功');location.href='" + Request.UrlReferrer.ToString() + "';", true);
                    Config.MsgGotoUrl("修改成功!",Request.UrlReferrer.ToString());
                }
            }
        }
    }
}
