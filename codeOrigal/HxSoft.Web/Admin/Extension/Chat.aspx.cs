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

namespace HxSoft.Web.Admin.Extension
{
    public partial class Chat : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
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
            GetData.LimitChkMsg("Chat");
            lbtnAdd.Visible = GetData.LimitChk("ChatAdd");
            lbtnEdit.Visible = GetData.LimitChk("ChatEdit");
            lbtnDel.Visible = GetData.LimitChk("ChatDel");
            lbtnOpen.Visible = GetData.LimitChk("ChatOpen");
            lbtnClose.Visible = GetData.LimitChk("ChatClose");
            if (!Page.IsPostBack)
            {
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Chat where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strChatID = Config.Request(Request.Form["ChatID"], "0");
            if (strChatID != "0")
            {
                Response.Redirect("Chat_Add.aspx?ChatID=" + strChatID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strChatID = Config.Request(Request.Form["ChatID"], "0");
            if (strChatID != "0")
            {
                string[] arrChatID = strChatID.Split(new char[] { ',' });
                StringBuilder strTempChatID = new StringBuilder();
                ChatModel chaModel = new ChatModel();
                int n = 0;
                for (int i = 0; i < arrChatID.Length; i++)
                {
                    chaModel = Factory.Chat().GetInfo(arrChatID[i]);
                    if (chaModel != null)
                    {
                        if (GetData.CheckAdminID(chaModel.AdminID, "ChatAll"))//检查创建者
                        {
                            Factory.Chat().DeleteInfo(arrChatID[i]);
                            strTempChatID.Append(arrChatID[i]);
                            if (i + 1 < arrChatID.Length) strTempChatID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempChatID.ToString() + "的聊天帐号!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempChatID.ToString() + "聊天帐号删除成功!", "Chat.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }


        //批量开放
        protected void btnOpen_Click(object sender, EventArgs e)
        {
            string strChatID = Config.Request(Request.Form["ChatID"], "0");
            if (strChatID != "0")
            {
                string[] arrChatID = strChatID.Split(new char[] { ',' });
                StringBuilder strTempChatID = new StringBuilder();
                ChatModel chaModel = new ChatModel();
                int n = 0;
                for (int i = 0; i < arrChatID.Length; i++)
                {
                    chaModel = Factory.Chat().GetInfo(arrChatID[i]);
                    if (chaModel != null)
                    {
                        if (GetData.CheckAdminID(chaModel.AdminID, "ChatAll"))//检查创建者
                        {
                            Factory.Chat().UpdateCloseStatus(arrChatID[i], "0");
                            strTempChatID.Append(arrChatID[i]);
                            if (i + 1 < arrChatID.Length) strTempChatID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempChatID.ToString() + "的聊天帐号!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempChatID.ToString() + "聊天帐号开放成功!", "Chat.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        //批量关闭
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string strChatID = Config.Request(Request.Form["ChatID"], "0");
            if (strChatID != "0")
            {
                string[] arrChatID = strChatID.Split(new char[] { ',' });
                StringBuilder strTempChatID = new StringBuilder();
                ChatModel chaModel = new ChatModel();
                int n = 0;
                for (int i = 0; i < arrChatID.Length; i++)
                {
                    chaModel = Factory.Chat().GetInfo(arrChatID[i]);
                    if (chaModel != null)
                    {
                        if (GetData.CheckAdminID(chaModel.AdminID, "ChatAll"))//检查创建者
                        {
                            Factory.Chat().UpdateCloseStatus(arrChatID[i], "1");
                            strTempChatID.Append(arrChatID[i]);
                            if (i + 1 < arrChatID.Length) strTempChatID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempChatID.ToString() + "的聊天帐号!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempChatID.ToString() + "聊天帐号关闭成功!", "Chat.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        //查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Repeater_Bind(repList);
        }

    }
}
