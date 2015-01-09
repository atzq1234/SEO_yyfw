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
using System.Data.Common;
using System.Collections.Generic;
using System.IO;

namespace HxSoft.Web.Admin._System
{
    public partial class Config_Add : System.Web.UI.Page
    {
        /// <summary>
        ///系统配置
        /// 创建人:杨小明
        /// 日期:2011-8-25
        /// </summary>
        //定义全局变量
        public string ConfigID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ConfigID"], 0).ToString();
            }
        }
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        #region ****排序参数****
        public string strOrderKey
        {
            get
            {
                return Config.Request(Request["OrderKey"], "ConfigID");
            }
        }
        public string strAscDesc1
        {
            get
            {
                return Config.Request(Request["AscDesc"], "asc");
            }
        }
        public string strAscDesc2
        {
            get
            {
                if (strAscDesc1 == "asc")
                    return "desc";
                else
                    return "asc";
            }
        }
        #endregion
        #region ****排序语句****
        public string SqlOrder
        {
            get
            {
                return " order by " + strOrderKey + " " + strAscDesc1;
            }
        }
        #endregion
        #region ****Url排序参数****
        public string UrlOrderPara
        {
            get
            {
                StringBuilder TempUrl = new StringBuilder("");
                TempUrl.Append("OrderKey=" + Server.UrlEncode(strOrderKey) + "&");
                TempUrl.Append("AscDesc=" + Server.UrlEncode(strAscDesc1) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        #region ****查询参数****
        public string strWebsiteName
        {
            get
            {
                return Config.Request(Request["txtWebsiteName"], "");
            }
        }
        public string strIsClose
        {
            get
            {
                return Config.Request(Request["radIsClose"], "-1");
            }
        }
        #endregion
        #region ****查询语句****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (strWebsiteName != "") TempSql.Append(" and WebsiteName like @WebsiteName");
                if (strIsClose != "-1") TempSql.Append(" and IsClose = @IsClose");
                return TempSql.ToString();
            }
        }
        #endregion
        #region****DbParameter参数****
        public DbParameter[] SqlParams
        {
            get
            {
                List<DbParameter> listParams = new List<DbParameter>();
                if (strWebsiteName != "") listParams.Add(Config.Conn().CreateDbParameter("@WebsiteName", "%" + strWebsiteName + "%"));
                if (strIsClose != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsClose", strIsClose));
                return listParams.ToArray();
            }
        }
        #endregion
        #region ****Url参数****
        public string UrlPara
        {
            get
            {
                StringBuilder TempUrl = new StringBuilder("");
                TempUrl.Append("txtWebsiteName=" + Server.UrlEncode(strWebsiteName) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            if (!Page.IsPostBack)
            {
                if (ConfigID == "0")
                {
                    GetData.LimitChkMsg("ConfigAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.Config().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("ConfigEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ConfigModel confModel = new ConfigModel();
            string strOldListID = hidlistID.Value;
            confModel.LanguageVer = txtLanguageVer.Text.Trim();
            confModel.WebsiteName = txtWebsiteName.Text.Trim();
            confModel.WebsiteUrl = txtWebsiteUrl.Text.Trim();
            confModel.WebsiteKeywords = txtWebsiteKeywords.Text.Trim();
            confModel.WebsiteDescription = txtWebsiteDescription.Text.Trim();
            confModel.MailReceiveAddress = txtMailReceiveAddress.Text.Trim();
            confModel.MailSmtpServer = txtMailSmtpServer.Text.Trim();
            confModel.MailUserName = txtMailUserName.Text.Trim();
            confModel.MailPassword = txtMailPassword.Text.Trim();
            confModel.FooterInfo = txtFooterInfo.Text.Trim();
            confModel.ListID = txtListID.Text.Trim();
            confModel.AdminID = Session["AdminID"].ToString();
            confModel.AddTime = DateTime.Now.ToString();
            confModel.IsClose = radIsClose.SelectedValue;
            if (ConfigID == "0")
            {
                string strWebDir = Server.MapPath(confModel.WebsiteUrl);
                if (!Directory.Exists(strWebDir))
                {
                    errMsg.Text = "站点目录不存在!";
                }
                else
                {
                    confModel.MailPassword = Config.Encrypt(confModel.MailPassword);
                    Factory.Config().OrderInfo(confModel.ListID, strOldListID);
                    Factory.Config().InsertInfo(confModel);
                    Factory.AdminLog().InsertLog("添加名称为" + confModel.WebsiteName + "的网站配置!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("添加成功！", "Config.aspx");
                }
            }
            else
            {
                string strWebDir = Server.MapPath(confModel.WebsiteUrl);
                if (!Directory.Exists(strWebDir))
                {
                    errMsg.Text = "站点目录不存在!";
                }
                else
                {
                    ConfigModel confModel_2 = new ConfigModel();
                    confModel_2 = Factory.Config().GetInfo(ConfigID);
                    if (confModel_2 != null)
                    {
                        if (GetData.CheckAdminID(confModel_2.AdminID, "ConfigAll"))//检查创建者
                        {
                            if (confModel_2.MailPassword != confModel.MailPassword)
                            {
                                confModel.MailPassword = Config.Encrypt(confModel.MailPassword);
                            }
                            Factory.Config().OrderInfo(confModel.ListID, strOldListID);
                            Factory.Config().UpdateInfo(confModel, ConfigID);
                            Factory.AdminLog().InsertLog("修改编号为" + ConfigID + "的的网站配置!", Session["AdminID"].ToString());
                            Config.MsgGotoUrl("修改成功！", "Config.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                        }
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            ConfigModel confModel = new ConfigModel();
            confModel = Factory.Config().GetInfo(ConfigID);
            if (confModel != null)
            {
                if (GetData.CheckAdminID(confModel.AdminID, "ConfigAll"))//检查创建者
                {
                    txtLanguageVer.Text = confModel.LanguageVer;
                    txtWebsiteName.Text = confModel.WebsiteName;
                    txtWebsiteUrl.Text = confModel.WebsiteUrl;
                    txtWebsiteKeywords.Text = confModel.WebsiteKeywords;
                    txtWebsiteDescription.Text = confModel.WebsiteDescription;
                    txtMailReceiveAddress.Text = confModel.MailReceiveAddress;
                    txtMailSmtpServer.Text = confModel.MailSmtpServer;
                    txtMailUserName.Text = confModel.MailUserName;
                    txtMailPassword.Text = confModel.MailPassword;
                    txtFooterInfo.Text = confModel.FooterInfo;
                    txtListID.Text = confModel.ListID;
                    hidlistID.Value = confModel.ListID;
                    //txtAdminID.Text = confModel.AdminID;
                    //txtAddTime.Text = confModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, confModel.IsClose);
                }
                else
                {
                    Config.ShowEnd("您没有查看此信息的权限！");
                }
            }
            else
            {
                Config.ShowEnd("您没有查看此信息的权限！");
            }
        }

    }
}
