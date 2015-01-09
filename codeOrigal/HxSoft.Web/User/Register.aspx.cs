using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.User
{
    public partial class Register : System.Web.UI.Page
    {
        //this page add by yang
        protected void Page_Load(object sender, EventArgs e)
        {
        }


        //reg
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strVerifyCode = txtVerifyCode.Text.Trim();
            UserModel userModel = new UserModel();
            userModel.UserName = txtUserName.Text.Trim();
            string strUserPass = txtUserPass.Text.Trim();
            string strUserPassConfirm = txtUserPassConfirm.Text.Trim();
            userModel.PassQuestion = txtPassQuestion.Text.Trim();
            userModel.PassAnswer = txtPassAnswer.Text.Trim();
            userModel.RealName = txtRealName.Text.Trim();
            userModel.Sex = radSex.SelectedValue;
            userModel.Email = txtEmail.Text.Trim();
            userModel.Mobile = txtMobile.Text.Trim();
            userModel.Address = txtAddress.Text.Trim();
            userModel.Company = txtCompany.Text.Trim();
            userModel.Comment = Config.HTMLCls(txtComment.Text.Trim());
            userModel.UserRankID = "1";
            userModel.Point = "0";
            userModel.IsAudit = "0";
            userModel.LoginNum = "0";
            userModel.LastLoginTime = "1900-1-1";
            userModel.ThisLoginTime = "1900-1-1";
            userModel.AddTime = DateTime.Now.ToString();
            userModel.IsClose = "0";
            if (string.IsNullOrEmpty(userModel.UserName))
            {
                errMsg.Text = "用户名不能为空!";
            }
            else if (string.IsNullOrEmpty(strUserPass))
            {
                errMsg.Text = "密码不能为空!";
            }
            else if (strUserPass!=strUserPassConfirm)
            {
                errMsg.Text = "两次密码不一致!";
            }
            else if (string.IsNullOrEmpty(userModel.PassQuestion))
            {
                errMsg.Text = "密码保护问题不能为空!";
            }
            else if (string.IsNullOrEmpty(userModel.PassAnswer))
            {
                errMsg.Text = "密码保护答案不能为空!";
            }
            else if (string.IsNullOrEmpty(userModel.RealName))
            {
                errMsg.Text = "姓名不能为空!";
            }
            else if (!Config.IsMail(userModel.Email))
            {
                errMsg.Text = "请输入正确的E-mail!";
            }
            else if (!Config.IsTel(userModel.Mobile))
            {
                errMsg.Text = "请输入正确的联系电话!";
            }
            else if (string.IsNullOrEmpty(userModel.Address))
            {
                errMsg.Text = "联系地址不能为空!";
            }
            else if (string.IsNullOrEmpty(userModel.Company))
            {
                errMsg.Text = "公司名称不能为空!";
            }
            else if (Session["VerifyCode"] == null)
            {
                errMsg.Text = "验证码有误!";
            }
            else if (strVerifyCode != Session["VerifyCode"].ToString())
            {
                errMsg.Text = "验证码有误!";
            }
            else if (!Factory.User().CheckInfo("UserName", userModel.UserName))
            {
                userModel.UserPass = Config.md5(strUserPass);
                Factory.User().InsertInfo(userModel);
                UserModel userModel_2 = new UserModel();
                userModel_2 = Factory.User().GetInfoByUserName(userModel.UserName);
                if (userModel_2 != null)
                {
                    Factory.UserLog().InsertLog("会员\"" + userModel.UserName + "\"新注册。", userModel_2.UserID);
                }
                Config.MsgGotoUrl("注册成功! 请等待管理员审核!", "/");
                //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('注册成功! 请等待管理员审核!');location.href='/'", true);
            }
            else
            {
                errMsg.Text = "用户名已存在!";
            }
        }
    }
}

