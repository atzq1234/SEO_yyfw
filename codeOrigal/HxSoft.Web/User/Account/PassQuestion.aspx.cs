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

        //�û���Ϣ
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
                Config.MsgGoBack("�û�������!");
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
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('ԭ����𰸲���ȷ!');$('#" + txtOldPassAnswer.ClientID + "').focus();", true);
                    errMsg.Text = "ԭ����𰸲���ȷ!";
                }
                else
                {
                    userModel.PassQuestion = txtNewPassQuestion.Text.Trim();
                    userModel.PassAnswer = txtNewPassAnswer.Text.Trim();
                    Factory.User().UpdatePassQuestion(userModel, Session["UserID"].ToString());
                    Factory.UserLog().InsertLog("�޸����뱣�����ϣ�", Session["UserID"].ToString());
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('�޸ĳɹ�');location.href='" + Request.UrlReferrer.ToString() + "';", true);
                    Config.MsgGotoUrl("�޸ĳɹ�!",Request.UrlReferrer.ToString());
                }
            }
        }
    }
}
