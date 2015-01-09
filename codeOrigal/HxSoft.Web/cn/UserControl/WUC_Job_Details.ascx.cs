using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;
using System.IO;

namespace HxSoft.Web.cn.UserControl
{
    public partial class WUC_Job_Details : System.Web.UI.UserControl
    {
        /// <summary>
        /// 信息ID(只读)
        /// </summary>
        public string JobID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["JobID"], -1).ToString();
            }
        }
        public JobModel jobModel
        {
            get
            {
                return Factory.Job().GetCacheInfo2(JobID);
            }
        }
        /// <summary>
        /// 分类ID(只读)
        /// </summary>
        public string ClassID
        {
            get
            {
                if (jobModel != null)
                {
                    return jobModel.ClassID;
                }
                else
                {
                    return "-1";
                }
            }
        }
        public string ConfigID="-1";
        protected void Page_Load(object sender, EventArgs e)
        {
            //栏目名称
            ClassModel claModel = new ClassModel();
            claModel = Factory.Class().GetCacheInfo2(ClassID);
            if (claModel != null)
            {
                ConfigID = claModel.ConfigID;
                litClassName.Text = claModel.ClassName;
            }
            //详细内容
            if (jobModel != null)
            {
                Factory.Job().Click(JobID);
                //
                Page.Header.Title = Server.HtmlEncode(jobModel.JobName) + " - " + Page.Header.Title;
                //先清除母版页设置的keywords和description
                Page.Header.Controls.Remove(Page.Header.FindControl("keywords"));
                Page.Header.Controls.Remove(Page.Header.FindControl("description"));
                Page.Header.Controls.Add(Config.SetKeywords(Server.HtmlEncode(jobModel.Keywords)));
                Page.Header.Controls.Add(Config.SetDescription(Server.HtmlEncode(jobModel.Description)));
                //
                litDepartment.Text = jobModel.Department;
                litJobName.Text = jobModel.JobName;
                litJobNum.Text = jobModel.JobNum;
                litSalary.Text = jobModel.Salary;
                litWorkPlace.Text = jobModel.WorkPlace;
                litEndTime.Text = Convert.ToDateTime(jobModel.EndTime).ToString("yyyy-MM-dd");
                litDemand.Text = jobModel.Demand;
            }
            else
            {
                Config.ShowEnd("参数错误!");
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string strVerifyCode = txtVerifyCode.Text.Trim();

            if (Session["VerifyCode"] == null)
            {
                errMsg.Text = "验证码有误";
            }
            else
            {
                if (strVerifyCode != Session["VerifyCode"].ToString())
                {
                    errMsg.Text = "验证码有误";
                }
                else
                {
                    //上传文件
                    Config.CheckFolder(Server.MapPath(Config.FileUploadPath));
                    string strSaveDir = Config.FileUploadPath + "Job_Upload/";
                    Config.CheckFolder(Server.MapPath(strSaveDir));
                    string strFileExt = Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();
                    string strFileName = Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName).ToLower();
                    int intFileLength = FileUpload1.PostedFile.ContentLength;
                    string strSavePath = strSaveDir + strFileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + strFileExt;
                    if (intFileLength < 10)
                    {
                        errMsg.Text = "请选择您要上传的文件!";
                    }
                    else
                    {
                        if (strFileExt.IndexOf(".asp") > -1 || strFileExt.IndexOf(".asa") > -1 || strFileExt.IndexOf(".aspx") > -1 || strFileExt.IndexOf(".ashx") > -1 || strFileExt.IndexOf(".jsp") > -1 || strFileExt.IndexOf(".php") > -1 || (strFileExt.IndexOf(".doc") < 0 && strFileExt.IndexOf(".docx") < 0))
                        {
                            errMsg.Text = "请上传doc或docx文件!";
                        }
                        else
                        {
                            FileUpload1.PostedFile.SaveAs(Server.MapPath(strSavePath));


                            string strName = Config.HTMLCls(txtName.Text);
                            string strTel = Config.HTMLCls(txtTel.Text);
                            string strEmail = Config.HTMLCls(txtEmail.Text);

                            string strTitle = "来自[" + strName + "]的职位应聘-" + litJobName.Text;
                            string strIpAddress = Request.UserHostAddress;
                            string strAddTime = DateTime.Now.ToString();

                            StringBuilder strUploadContent = new StringBuilder("");
                            strUploadContent.Append("<table width=\"500\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" bgcolor=\"#000000\">\n");
                            strUploadContent.Append("<tbody>\n");
                            strUploadContent.Append("<tr>\n");
                            strUploadContent.Append("<td colspan=\"2\" align=\"center\" bgcolor=\"#FFFFFF\">" + strTitle + "</td>\n");
                            strUploadContent.Append("</tr>\n");
                            strUploadContent.Append("<tr>\n");
                            strUploadContent.Append("<td width=\"20%\" align=\"right\" bgcolor=\"#FFFFFF\">应聘职位:</td>\n");
                            strUploadContent.Append("<td width=\"80%\" align=\"left\" bgcolor=\"#FFFFFF\">" + litJobName.Text + "</td>\n");

                            strUploadContent.Append("</tr>\n");
                            strUploadContent.Append("<tr>\n");
                            strUploadContent.Append("<td width=\"20%\" align=\"right\" bgcolor=\"#FFFFFF\">姓名:</td>\n");
                            strUploadContent.Append("<td width=\"80%\" align=\"left\" bgcolor=\"#FFFFFF\">" + strName + "</td>\n");
                            strUploadContent.Append("</tr>\n");
                            strUploadContent.Append("<tr>\n");
                            strUploadContent.Append("<td align=\"right\" bgcolor=\"#FFFFFF\">联系电话:</td>\n");
                            strUploadContent.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + strTel + "</td>\n");
                            strUploadContent.Append("</tr>\n");
                            strUploadContent.Append("<tr>\n");
                            strUploadContent.Append("<td align=\"right\" bgcolor=\"#FFFFFF\">E-Mail:</td>\n");
                            strUploadContent.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + strEmail + "</td>\n");
                            strUploadContent.Append("</tr>\n");

                            StringBuilder strUploadContent2 = new StringBuilder();
                            strUploadContent2.Append("<tr>\n");
                            strUploadContent2.Append("<td align=\"right\" bgcolor=\"#FFFFFF\">文件:</td>\n");
                            strUploadContent2.Append("<td align=\"left\" bgcolor=\"#FFFFFF\"><a href=\"" + strSavePath + "\" target=\"_blank\">" + strSavePath + "</a></td>\n");
                            strUploadContent2.Append("</tr>\n");

                            StringBuilder strUploadContent3 = new StringBuilder();
                            strUploadContent3.Append("<tr>\n");
                            strUploadContent3.Append("<td align=\"right\" bgcolor=\"#FFFFFF\">IP:</td>\n");
                            strUploadContent3.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + strIpAddress + "</td>\n");
                            strUploadContent3.Append("</tr>\n");
                            strUploadContent3.Append("<tr>\n");
                            strUploadContent3.Append("<td align=\"right\" bgcolor=\"#FFFFFF\">申请时间:</td>\n");
                            strUploadContent3.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + strAddTime + "</td>\n");
                            strUploadContent3.Append("</tr>\n");
                            strUploadContent3.Append("</tbody>\n");
                            strUploadContent3.Append("</table>\n");
                            
                            //将反馈信息添加到数据库中
                            FeedbackModel feeModel = new FeedbackModel();
                            feeModel.DictionaryID = Config.JobDictionaryMouldID;
                            feeModel.Title = strTitle;
                            feeModel.FeedbackContent = strUploadContent.ToString() + strUploadContent2.ToString() + strUploadContent3.ToString();
                            feeModel.IpAddress = strIpAddress;
                            feeModel.AddTime = strAddTime;
                            feeModel.IsDeal = "0";
                            feeModel.DealMeno = "";
                            Factory.Feedback().InsertInfo(feeModel);

                            //将反馈信息发送到邮箱
                            ConfigModel confModel = new ConfigModel();
                            confModel = Factory.Config().GetCacheInfo(ConfigID);
                            if (confModel != null)
                            {
                                try
                                {
                                    Config.SendMailMessage(confModel.MailSmtpServer, confModel.MailUserName, Config.Decrypt(confModel.MailPassword), confModel.MailReceiveAddress, "", "", strTitle, strUploadContent.ToString() + strUploadContent3.ToString(), Server.MapPath(strSavePath), true, false);
                                }
                                catch (Exception ex)
                                {
                                    Config.Err(ex);
                                }
                            }

                            Config.MsgGotoUrl("发送成功!", Request.UrlReferrer.ToString());
                        }
                    }

                }
            }
        }
    }
}