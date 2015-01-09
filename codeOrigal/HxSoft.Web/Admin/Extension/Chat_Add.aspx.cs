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
using System.Collections.Generic;
using System.Data.Common;

namespace HxSoft.Web.Admin.Extension
{
    public partial class Chat_Add : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string ChatID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ChatID"], 0).ToString();
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
                return Config.Request(Request["OrderKey"], "ListID");
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
        public string strTypeID
        {
            get
            {
                return Config.Request(Request["drpTypeID"], "-1");
            }
        }
        public string strNickName
        {
            get
            {
                return Config.Request(Request["txtNickName"], "");
            }
        }
        public string strAccount
        {
            get
            {
                return Config.Request(Request["txtAccount"], "");
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
                if (strTypeID != "-1") TempSql.Append(" and TypeID =@TypeID");
                if (strNickName != "") TempSql.Append(" and NickName like @NickName");
                if (strAccount != "") TempSql.Append(" and Account like @Account");
                if (strIsClose != "-1") TempSql.Append(" and IsClose =@IsClose");
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
                if (strTypeID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@TypeID", strTypeID));
                if (strNickName != "") listParams.Add(Config.Conn().CreateDbParameter("@NickName", "%" + strNickName + "%"));
                if (strAccount != "") listParams.Add(Config.Conn().CreateDbParameter("@Account", "%" + strAccount + "%"));
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
                TempUrl.Append("drpTypeID=" + Server.UrlEncode(strTypeID) + "&");
                TempUrl.Append("txtNickName=" + Server.UrlEncode(strNickName) + "&");
                TempUrl.Append("txtAccount=" + Server.UrlEncode(strAccount) + "&");
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
                //语言版本
                GetData.GetConfigList(drpConfigID, "LanguageVer", "ConfigID");
                this.drpConfigID.Items.Insert(0, new ListItem("请选择", "-1"));

                if (ChatID == "0")
                {
                    GetData.LimitChkMsg("ChatAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.Chat().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("ChatEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChatModel chaModel = new ChatModel();
            string strOldListID = hidlistID.Value;
            chaModel.ConfigID = drpConfigID.SelectedValue;
            chaModel.TypeID = radTypeID.SelectedValue;
            chaModel.NickName = txtNickName.Text.Trim();
            chaModel.Account = txtAccount.Text.Trim();
            chaModel.ChatKey = txtChatKey.Text.Trim();
            chaModel.ListID = txtListID.Text.Trim();
            chaModel.AdminID = Session["AdminID"].ToString();
            chaModel.AddTime = DateTime.Now.ToString();
            chaModel.IsClose = radIsClose.SelectedValue;
            if (ChatID == "0")
            {
                Factory.Chat().OrderInfo(chaModel.ListID, strOldListID);
                Factory.Chat().InsertInfo(chaModel);
                Factory.AdminLog().InsertLog("添加名称为\"" + chaModel.Account + "\"的聊天帐号。", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "Chat.aspx");
            }
            else
            {
                ChatModel chaModel_2 = new ChatModel();
                chaModel_2 = Factory.Chat().GetInfo(ChatID);
                if (chaModel_2 != null)
                {
                    if (GetData.CheckAdminID(chaModel_2.AdminID, "ChatAll"))//检查创建者
                    {
                        Factory.Chat().OrderInfo(chaModel.ListID, strOldListID);
                        Factory.Chat().UpdateInfo(chaModel, ChatID);
                        Factory.AdminLog().InsertLog("修改编号为" + ChatID + "的聊天帐号。", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "Chat.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            ChatModel chaModel = new ChatModel();
            chaModel = Factory.Chat().GetInfo(ChatID);
            if (chaModel != null)
            {
                if (GetData.CheckAdminID(chaModel.AdminID, "ChatAll"))//检查创建者
                {
                    Config.setDefaultSelected(drpConfigID, chaModel.ConfigID);
                    radTypeID.ClearSelection();
                    Config.setDefaultSelected(radTypeID, chaModel.TypeID);
                    txtNickName.Text = chaModel.NickName;
                    txtAccount.Text = chaModel.Account;
                    txtChatKey.Text = chaModel.ChatKey;
                    txtListID.Text = chaModel.ListID;
                    hidlistID.Value = chaModel.ListID;
                    //txtAdminID.Text = chaModel.AdminID;
                    //txtAddTime.Text = chaModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, chaModel.IsClose);
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
