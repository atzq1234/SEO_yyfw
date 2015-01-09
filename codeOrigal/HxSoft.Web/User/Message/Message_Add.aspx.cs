using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.User.Message
{
    public partial class Message_Add : System.Web.UI.Page
    {
        //this page add by yang
        protected void Page_Load(object sender, EventArgs e)
        {
            drpDictionaryID_Bind();
        }

        //问题分类
        private void drpDictionaryID_Bind()
        {
            Factory.Dictionary().ShowSelectTree(Config.SysMessageDictionaryMouldID, drpDictionaryID, "");
            drpDictionaryID.Items.Insert(0, new ListItem("--请选择留言分类--", "-1"));
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            MessageModel feeModel = new MessageModel();
            feeModel.DictionaryID = drpDictionaryID.SelectedValue;
            feeModel.UserID = Session["UserID"].ToString();
            feeModel.Title = txtTitle.Text.Trim();
            feeModel.MessageContent = Config.HTMLCls(txtMessageContent.Text.Trim());
            feeModel.AdminID = "0";
            feeModel.ParentID = "0";
            feeModel.AddTime = DateTime.Now.ToString();
            feeModel.IsReply = "0";
            feeModel.IsRead = "-1";
            feeModel.IsEnd = "0";
            if (string.IsNullOrEmpty(feeModel.DictionaryID) || feeModel.DictionaryID == "-1")
            {
                errMsg.Text = "请选择留言分类!";
            }
            else if (string.IsNullOrEmpty(feeModel.Title))
            {
                errMsg.Text = "标题不能为空!";
            }
            else if (string.IsNullOrEmpty(feeModel.MessageContent))
            {
                errMsg.Text = "留言内容不能为空!";
            }
            else
            {
                Factory.Message().InsertInfo(feeModel);
                Factory.UserLog().InsertLog("会员留言！", Session["UserID"].ToString());
                //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('提交成功!');location.href='Message_List.aspx'", true);
                Config.MsgGotoUrl("提交成功!", "Message_List.aspx");
            }
        }
    }
}