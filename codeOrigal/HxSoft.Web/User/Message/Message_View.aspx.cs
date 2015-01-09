using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;
using System.Data;
using System.Data.Common;

namespace HxSoft.Web.User.Message
{
    public partial class Message_View : System.Web.UI.Page
    {
        //this page add by yang
        public string MessageID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["MessageID"], -1).ToString();
            }
        }
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        #region ****查询参数****
        public string strDictionaryID
        {
            get
            {
                return Config.RequestNumeric(Request["drpDictionaryID"], -1).ToString();
            }
        }
        public string strTitle
        {
            get
            {
                return Config.Request(Request["Title"], "");
            }
        }
        #endregion
        #region ****查询语句****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                TempSql.Append(" and UserID = @UserID");
                if (strDictionaryID != "-1") TempSql.Append(" and DictionaryID = @DictionaryID");
                if (strTitle != "") TempSql.Append(" and Title like @Title");
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
                listParams.Add(Config.Conn().CreateDbParameter("@UserID", Session["UserID"].ToString()));
                if (strDictionaryID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@DictionaryID", strDictionaryID));
                if (strTitle != "") listParams.Add(Config.Conn().CreateDbParameter("@Title", "%" + strTitle + "%"));
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
                TempUrl.Append("Title=" + Server.UrlEncode(strTitle) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowInfo();
            repList_Bind();
            Factory.Message().UpdateReadStatus(MessageID, "1");
        }

        //绑定数据-回复列表
        protected void repList_Bind()
        {
            string sql = "select * from t_Message where 1=1 and ParentID=" + MessageID + " order by AddTime desc";
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.Repeater.ToString(), repList);
        }


        //显示数据
        protected void ShowInfo()
        {
            MessageModel feeModel = new MessageModel();
            feeModel = Factory.Message().GetInfo(MessageID);
            if (feeModel != null)
            {
                if (feeModel.UserID == Session["UserID"].ToString())
                {
                    litTitle.Text = feeModel.Title;
                    litMessageContent.Text = feeModel.MessageContent;
                    litAddTime.Text = feeModel.AddTime;
                    if (feeModel.IsEnd == "1")
                    {
                        tab_reply.Visible = false;
                        tab_back.Visible = true;
                    }
                    else
                    {
                        tab_reply.Visible = true;
                        tab_back.Visible = false;
                    }
                }
                else
                {
                    Config.ShowEnd("您没有查看此信息的权限！");
                }
            }
            else
            {
                Config.ShowEnd("参数错误！");
            }
        }

        //回复
        //会员回复:已读,未回复
        protected void btnSave_Click(object sender, EventArgs e)
        {
            MessageModel queModel = new MessageModel();
            queModel = Factory.Message().GetInfo(MessageID);
            if (queModel != null)
            {
                if (queModel.UserID == Session["UserID"].ToString())
                {
                    MessageModel feeModel_2 = new MessageModel();
                    feeModel_2.DictionaryID = queModel.DictionaryID;
                    feeModel_2.UserID = Session["UserID"].ToString();
                    feeModel_2.Title = "回复:" + queModel.Title;
                    feeModel_2.MessageContent = Config.HTMLCls(txtMessageContent.Text.Trim());
                    feeModel_2.AdminID = "0";
                    feeModel_2.ParentID = MessageID;
                    feeModel_2.AddTime = DateTime.Now.ToString();
                    feeModel_2.IsRead = "0";
                    feeModel_2.IsReply = "0";
                    feeModel_2.IsEnd = "0";
                    Factory.Message().InsertInfo(feeModel_2);
                    //是否结束该主题
                    if (chkIsEnd.Checked)
                    {
                        Factory.Message().UpdateEndStatus(MessageID, "1");//结束主题
                    }
                    else
                    {
                        //设置为未回复
                        Factory.Message().UpdateReplyStatus(MessageID, "0");
                    }
                    Factory.UserLog().InsertLog("会员回复留言！", Session["UserID"].ToString());
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('回复成功!');location.href='" + Request.UrlReferrer.ToString() + "'", true);
                    Response.Redirect(Request.UrlReferrer.ToString());
                }
            }
        }
    }
}