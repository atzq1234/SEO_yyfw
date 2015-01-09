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
    public partial class UserInfo : System.Web.UI.Page
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
                txtRealName.Text = userModel.RealName;
                Config.setDefaultSelected(radSex, userModel.Sex);
                txtEmail.Text = userModel.Email;
                txtMobile.Text = userModel.Mobile;
                txtAddress.Text = userModel.Address;
                txtCompany.Text = userModel.Company;
                txtComment.Text = Config.HTMLToTextarea(userModel.Comment);
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
            userModel.RealName = txtRealName.Text.Trim();
            userModel.Sex = radSex.SelectedValue;
            userModel.Email = txtEmail.Text.Trim();
            userModel.Mobile = txtMobile.Text.Trim();
            userModel.Address = txtAddress.Text.Trim();
            userModel.Company = txtCompany.Text.Trim();
            userModel.Comment = Config.HTMLCls(txtComment.Text.Trim());
            if (string.IsNullOrEmpty(userModel.RealName))
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
            else
            {
                Factory.User().UpdateInfoByUser(userModel, Session["UserID"].ToString());
                Factory.UserLog().InsertLog("修改会员资料！", Session["UserID"].ToString());
                //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('修改成功');", true);
                Config.MsgGotoUrl("修改成功!", Request.UrlReferrer.ToString());
            }
        }
    }
}
