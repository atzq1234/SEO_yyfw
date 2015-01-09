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

namespace HxSoft.Web.Admin.Message
{
    public partial class Message_View : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string MessageID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["MessageID"], 0).ToString();
            }
        }
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        public bool IsCanReply
        {
            get 
            { 
                return GetData.LimitChk("MessageReply");
            }
        }
        public bool IsCanDel
        {
            get
            {
                return GetData.LimitChk("MessageDel");
            }
        }

        #region ****排序参数****
        public string strOrderKey
        {
            get
            {
                return Config.Request(Request["OrderKey"], "AddTime");
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
                return " order by IsReply asc," + strOrderKey + " " + strAscDesc1;
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
        public string strDictionaryID
        {
            get
            {
                return Config.Request(Request["DictionaryID"], "-1");
            }
        }
        public string strTitle
        {
            get
            {
                return Config.Request(Request["txtTitle"], "");
            }
        }
        public string strUserName
        {
            get
            {
                return Config.Request(Request["txtUserName"], "");
            }
        }
        public string strIsReply
        {
            get
            {
                return Config.Request(Request["radIsReply"], "-1");
            }
        }
        #endregion
        #region ****查询语句****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (strDictionaryID != "-1") TempSql.Append(" and (DictionaryID=@DictionaryID or DictionaryID in (" + Factory.Dictionary().GetSubDictionarySql(strDictionaryID) + "))");
                if (strTitle != "") TempSql.Append(" and Title like @Title");
                if (strUserName != "") TempSql.Append(" and UserID in (select UserID from t_User where UserName like @UserName)");
                if (strIsReply != "-1") TempSql.Append(" and IsReply = @IsReply");
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
                if (strDictionaryID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@DictionaryID", strDictionaryID));
                if (strTitle != "") listParams.Add(Config.Conn().CreateDbParameter("@Title", "%" + strTitle + "%"));
                if (strUserName != "") listParams.Add(Config.Conn().CreateDbParameter("@UserName", "%" + strUserName + "%"));
                if (strIsReply != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsReply", strIsReply));
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
                TempUrl.Append("DictionaryID=" + Server.UrlEncode(strDictionaryID) + "&");
                TempUrl.Append("txtTitle=" + Server.UrlEncode(strTitle) + "&");
                TempUrl.Append("txtUserName=" + Server.UrlEncode(strUserName) + "&");
                TempUrl.Append("radIsReply=" + Server.UrlEncode(strIsReply) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("Message");
            if (!Page.IsPostBack)
            {
                ShowInfo();
                Repeater_Bind(repList);
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            MessageModel mesModel = new MessageModel();
            mesModel.DictionaryID = hidDictionaryID.Value;
            mesModel.UserID = "0";
            mesModel.Title = "Re:" + lblTitle.Text.Trim();
            mesModel.MessageContent = Config.HTMLCls(txtMessageContent.Text.Trim());
            mesModel.AdminID = Session["AdminID"].ToString();
            mesModel.ParentID = MessageID;
            mesModel.AddTime = DateTime.Now.ToString();
            mesModel.IsRead = "0";
            mesModel.IsReply = "0";
            mesModel.IsEnd = "0";
            Factory.Message().InsertInfo(mesModel);
            //是否结束该主题
            if (chkIsEnd.Checked)
            {
                Factory.Message().UpdateEndStatus(MessageID, "1");//结束
            }
            //设置主题为未读
            Factory.Message().UpdateReadStatus(MessageID, "0");
            //设置主题为已回复
            Factory.Message().UpdateReplyStatus(MessageID, "1");
            Factory.AdminLog().InsertLog("回复编号为" + MessageID + "的留言。", Session["AdminID"].ToString());
            Config.MsgGotoUrl("回复成功！", "Message_View.aspx?MessageID=" + MessageID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
        }
        //显示数据
        protected void ShowInfo()
        {
            MessageModel mesModel = new MessageModel();
            mesModel = Factory.Message().GetInfo(MessageID);
            if (mesModel != null)
            {
                hidDictionaryID.Value = mesModel.DictionaryID;
                lblUserName.Text = GetData.GetUserName(mesModel.UserID);
                lblTitle.Text = mesModel.Title;
                lblMessageContent.Text = mesModel.MessageContent;
                lblAddTime.Text = mesModel.AddTime;
                if (mesModel.UserID == "-1")//游客
                {
                    tr1_1.Visible = false;
                    tr1_2.Visible = false;
                    tr1_3.Visible = false;
                    //
                    tr2_1.Visible = IsCanReply;
                    tr2_2.Visible = !IsCanReply;
                }
                else//会员
                {
                    if (mesModel.IsEnd == "1" && mesModel.IsReply == "1")
                    {
                        tr1_1.Visible = false;
                        tr1_2.Visible = false;
                        tr1_3.Visible = false;
                        //
                        tr2_1.Visible = false;
                        tr2_2.Visible = true;
                    }
                    else
                    {
                        tr1_1.Visible = IsCanReply;
                        tr1_2.Visible = IsCanReply;
                        tr1_3.Visible = IsCanReply;
                        //
                        tr2_1.Visible = false;
                        tr2_2.Visible = !IsCanReply;
                    }
                }
            }
            else
            {
                Config.ShowEnd("您没有查看此信息的权限！");
            }
        }

        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Message where 1=1 and ParentID=" + MessageID + " order by AddTime asc";
            Factory.Acc().DataBind(sql,null,Config.DataBindObjTypeCollection.Repeater.ToString(), rep);
        }

        //删除数据
        protected void repList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string strMessageID = e.CommandArgument.ToString();
                Factory.Message().DeleteInfo(strMessageID);
                Factory.AdminLog().InsertLog("删除编号为" + strMessageID.ToString() + "的留言回复。", Session["AdminID"].ToString());
                Config.MsgGotoUrl("编号为" + strMessageID.ToString() + "留言回复删除成功!", "Message_View.aspx?MessageID=" + MessageID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        //设置为已回复
        protected void btnReply_Click(object sender, EventArgs e)
        {
            if (MessageID != "0")
            {
                Factory.Message().UpdateReplyStatus(MessageID, "1");
                Factory.AdminLog().InsertLog("设置编号为" + MessageID + "的留言为已回复。", Session["AdminID"].ToString());
                Config.MsgGotoUrl("设置成功！", "Message_View.aspx?MessageID=" + MessageID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
    }
}
