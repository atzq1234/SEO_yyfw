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

namespace HxSoft.Web.Admin.Message
{
    public partial class Message : System.Web.UI.Page
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
            lbtnDel.Visible = GetData.LimitChk("MessageDel");
            if (!Page.IsPostBack)
            {
                //留言分类
                Factory.Dictionary().ShowSelectTree(Config.SysMessageDictionaryMouldID, drpDictionaryID, "");
                drpDictionaryID.Items.Insert(0, new ListItem("不限", "-1"));
                //
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql_f_1 = "(select count(*) from t_Message as b where b.ParentID=a.MessageID) as ReplyCount";
            string sql = "select *," + sql_f_1 + " from t_Message as a where 1=1 and ParentID=0 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strMessageID = Config.Request(Request.Form["MessageID"], "0");
            if (strMessageID != "0")
            {
                string[] arrMessageID = strMessageID.Split(new char[] { ',' });
                StringBuilder strTempMessageID = new StringBuilder();
                for (int i = 0; i < arrMessageID.Length; i++)
                {
                    Factory.Message().DeleteInfo(arrMessageID[i]);
                    strTempMessageID.Append(arrMessageID[i]);
                    if (i + 1 < arrMessageID.Length) strTempMessageID.Append(",");
                }
                Factory.AdminLog().InsertLog("删除编号为" + strTempMessageID.ToString() + "的留言信息。", Session["AdminID"].ToString());
                Config.MsgGotoUrl("编号为" + strTempMessageID.ToString() + "的留言信息删除成功!", "Message.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        //查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Repeater_Bind(repList);
        }

    }
}
