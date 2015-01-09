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
    public partial class ResetPassword : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strOldAdminPass = Config.md5(txtOldAdminPass.Text);
            string strNewAdminPass = Config.md5(txtNewAdminPass.Text);
            AdminModel admModel = new AdminModel();
            admModel = Factory.Admin().GetInfo(Session["AdminID"].ToString());
            if (admModel != null)
            {
                if (admModel.AdminPass == strOldAdminPass)
                {
                    Factory.Admin().ResetPassword(Session["AdminID"].ToString(), strNewAdminPass);
                    Factory.AdminLog().InsertLog("修改密码。", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("密码修改成功!需要重新登录!", "LogOut.aspx");
                }
                else
                {
                    errMsg.Text = "旧密码不正确!";
                }
            }
        }

        //显示数据
        protected void ShowInfo()
        {
            AdminModel admModel = new AdminModel();
            admModel = Factory.Admin().GetInfo(Session["AdminID"].ToString());
            if (admModel != null)
            {
                lblAdminName.Text = admModel.AdminName;
            }
            else
            {
                Config.ShowEnd("您没有查看此信息的权限！");
            }
        }

    }
}
