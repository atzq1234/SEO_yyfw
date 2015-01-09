using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.cn.UserControl
{
    public partial class WUC_Feedback : System.Web.UI.UserControl
    {
        private string _configid, _classid;
        /// <summary>
        /// 配置ID
        /// </summary>
        public string ConfigID
        {
            get { return _configid; }
            set { _configid = value; }
        }
        /// <summary>
        /// 栏目ID
        /// </summary>
        public string ClassID
        {
            get { return _classid; }
            set { _classid = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //ClassModel claModel = new ClassModel();
            //claModel = Factory.Class().GetCacheInfo2(ClassID);
            //if (claModel != null)
            //{
            //    //litClassName.Text = claModel.ClassName;

            //    Page.Header.Title = Server.HtmlEncode(claModel.ClassName) + " - " + Page.Header.Title;
            //    //先清除母版页设置的keywords和description
            //    Page.Header.Controls.Remove(Page.Header.FindControl("keywords"));
            //    Page.Header.Controls.Remove(Page.Header.FindControl("description"));
            //    Page.Header.Controls.Add(Config.SetKeywords(Server.HtmlEncode(claModel.Keywords)));
            //    Page.Header.Controls.Add(Config.SetDescription(Server.HtmlEncode(claModel.Description)));
            //}
        }

        //protected void btnSend_Click(object sender, EventArgs e)
        //{
        //    string strVerifyCode = txtVerifyCode.Text.Trim();

        //    if (Session["VerifyCode"] == null)
        //    {
        //        errMsg.Text = "验证码有误";
        //    }
        //    else
        //    {
        //        if (strVerifyCode != Session["VerifyCode"].ToString())
        //        {
        //            errMsg.Text = "验证码有误";
        //        }
        //        else
        //        {
        //            string strName = Config.HTMLCls(txtName.Text);
        //            string strCompany = Config.HTMLCls(txtCompany.Text);
        //            string strAddress = Config.HTMLCls(txtAddress.Text);
        //            string strTel = Config.HTMLCls(txtTel.Text);
        //            string strEmail = Config.HTMLCls(txtEmail.Text);
        //            string strSubject = Config.HTMLCls(txtSubject.Text);
        //            string strContent = Config.HTMLCls(txtContent.Text);

        //            string strTitle = "来自[" + strName + "]的投诉建议-" + Request.ServerVariables["SERVER_NAME"]; ;
        //            string strIpAddress = Request.UserHostAddress;
        //            string strAddTime = DateTime.Now.ToString();

        //            StringBuilder strFeedbackContent = new StringBuilder("");
        //            strFeedbackContent.Append("<table width=\"500\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" bgcolor=\"#000000\">\n");
        //            strFeedbackContent.Append("<tbody>\n");
        //            strFeedbackContent.Append("<tr>\n");
        //            strFeedbackContent.Append("<td colspan=\"2\" align=\"center\" bgcolor=\"#FFFFFF\">" + strTitle + "</td>\n");
        //            strFeedbackContent.Append("</tr>\n");
        //            strFeedbackContent.Append("<tr>\n");
        //            strFeedbackContent.Append("<td width=\"20%\" align=\"right\" bgcolor=\"#FFFFFF\">姓名:</td>\n");
        //            strFeedbackContent.Append("<td width=\"80%\" align=\"left\" bgcolor=\"#FFFFFF\">" + strName + "</td>\n");
        //            strFeedbackContent.Append("</tr>\n");
        //            strFeedbackContent.Append("<tr>\n");
        //            strFeedbackContent.Append("<td width=\"20%\" align=\"right\" bgcolor=\"#FFFFFF\">公司名称:</td>\n");
        //            strFeedbackContent.Append("<td width=\"80%\" align=\"left\" bgcolor=\"#FFFFFF\">" + strCompany + "</td>\n");
        //            strFeedbackContent.Append("</tr>\n");
        //            strFeedbackContent.Append("<tr>\n");
        //            strFeedbackContent.Append("<td align=\"right\" bgcolor=\"#FFFFFF\">联系地址:</td>\n");
        //            strFeedbackContent.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + strAddress + "</td>\n");
        //            strFeedbackContent.Append("</tr>\n");
        //            strFeedbackContent.Append("<tr>\n");
        //            strFeedbackContent.Append("<td align=\"right\" bgcolor=\"#FFFFFF\">联系电话:</td>\n");
        //            strFeedbackContent.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + strTel + "</td>\n");
        //            strFeedbackContent.Append("</tr>\n");
        //            strFeedbackContent.Append("<tr>\n");
        //            strFeedbackContent.Append("<td align=\"right\" bgcolor=\"#FFFFFF\">E-Mail:</td>\n");
        //            strFeedbackContent.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + strEmail + "</td>\n");
        //            strFeedbackContent.Append("</tr>\n");
        //            strFeedbackContent.Append("<tr>\n");
        //            strFeedbackContent.Append("<td align=\"right\" bgcolor=\"#FFFFFF\">主题:</td>\n");
        //            strFeedbackContent.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + strSubject + "</td>\n");
        //            strFeedbackContent.Append("</tr>\n");
        //            strFeedbackContent.Append("<tr>\n");
        //            strFeedbackContent.Append("<td align=\"right\" bgcolor=\"#FFFFFF\">内容:</td>\n");
        //            strFeedbackContent.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + strContent + "</td>\n");
        //            strFeedbackContent.Append("</tr>\n");
        //            strFeedbackContent.Append("<tr>\n");
        //            strFeedbackContent.Append("<td align=\"right\" bgcolor=\"#FFFFFF\">IP:</td>\n");
        //            strFeedbackContent.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + strIpAddress + "</td>\n");
        //            strFeedbackContent.Append("</tr>\n");
        //            strFeedbackContent.Append("<tr>\n");
        //            strFeedbackContent.Append("<td align=\"right\" bgcolor=\"#FFFFFF\">添加时间:</td>\n");
        //            strFeedbackContent.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + strAddTime + "</td>\n");
        //            strFeedbackContent.Append("</tr>\n");
        //            strFeedbackContent.Append("</tbody>\n");
        //            strFeedbackContent.Append("</table>\n");

        //            //将反馈信息添加到数据库中
        //            FeedbackModel feeModel = new FeedbackModel();
        //            feeModel.DictionaryID = Config.FeedbackDictionaryMouldID;
        //            feeModel.Title = strTitle;
        //            feeModel.FeedbackContent = strFeedbackContent.ToString();
        //            feeModel.IpAddress = strIpAddress;
        //            feeModel.AddTime = strAddTime;
        //            feeModel.IsDeal = "0";
        //            feeModel.DealMeno = "";
        //            Factory.Feedback().InsertInfo(feeModel);

        //            //将反馈信息发送到邮箱
        //            ConfigModel confModel = new ConfigModel();
        //            confModel = Factory.Config().GetCacheInfo(ConfigID);
        //            if (confModel != null)
        //            {
        //                try
        //                {
        //                    Config.SendMailMessage(confModel.MailSmtpServer, confModel.MailUserName, Config.Decrypt(confModel.MailPassword), confModel.MailReceiveAddress, "", "", strTitle, strFeedbackContent.ToString(), "", true, false);
        //                }
        //                catch (Exception ex)
        //                {
        //                    Config.Err(ex);
        //                }
        //            }

        //            Config.MsgGotoUrl("发送成功!", Request.UrlReferrer.ToString());
        //        }
        //    }
        //}

        protected void btnSend_Click(object sender, EventArgs e)
        {
            //string strVerifyCode = txtVerifyCode.Text.Trim();

            //if (Session["VerifyCode"] == null)
            //{
            //    errMsg.Text = "验证码有误";
            //}
            //else
            //{
            //    if (strVerifyCode != Session["VerifyCode"].ToString())
            //    {
            //        errMsg.Text = "验证码有误";
            //    }
            //    else
            //    {
            GuestbookModel gbookModel = new GuestbookModel();
            gbookModel.NickName = txtNickName.Value.Trim();
            gbookModel.TelePhone = txtTelePhone.Value;
            gbookModel.Email = txtEmail.Value;
            gbookModel.BookContent = Config.HTMLCls(txtBookContent.Value.Trim());
            gbookModel.IpAddress = Request.UserHostAddress.ToString();
            gbookModel.AddTime = DateTime.Now.ToString();
            gbookModel.IsReply = "0";
            gbookModel.ReplyContent = "";
            gbookModel.ReplyTime = "1900-1-1";
            gbookModel.AdminID = "0";
            gbookModel.IsClose = "0";
            if (gbookModel.NickName == string.Empty)
            {
                errMsg.Text = "请输入昵称!";
            }
            else if (gbookModel.BookContent == string.Empty)
            {
                errMsg.Text = "请输入留言内容!";
            }
            else
            {
                Factory.Guestbook().InsertInfo(gbookModel);
                Config.MsgGotoUrl("留言成功,请等待回复！", Request.UrlReferrer.ToString());
            }
            // }
            //}
        }
    }
}