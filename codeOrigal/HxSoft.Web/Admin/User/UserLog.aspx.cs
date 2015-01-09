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

namespace HxSoft.Web.Admin.User
{
    public partial class UserLog : System.Web.UI.Page
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
                return Config.Request(Request["OrderKey"], "UserlogID");
            }
        }
        public string strAscDesc1
        {
            get
            {
                return Config.Request(Request["AscDesc"], "desc");
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
        public string strLogContent
        {
            get
            {
                return Config.Request(Request["txtLogContent"], "");
            }
        }
        public string strScriptFile
        {
            get
            {
                return Config.Request(Request["txtScriptFile"], "");
            }
        }
        public string strIPAddress
        {
            get
            {
                return Config.Request(Request["txtIPAddress"], "");
            }
        }
        public string strUser
        {
            get
            {
                return Config.Request(Request["txtUser"], "");
            }
        }
        public string strAddTime1
        {
            get
            {
                return Config.Request(Request["txtAddTime1"], "");
            }
        }
        public string strAddTime2
        {
            get
            {
                return Config.Request(Request["txtAddTime2"], "");
            }
        }
        #endregion
        #region ****查询语句****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (strLogContent != "") TempSql.Append(" and LogContent like @LogContent");
                if (strScriptFile != "") TempSql.Append(" and ScriptFile like @ScriptFile");
                if (strIPAddress != "") TempSql.Append(" and IPAddress like @IPAddress");
                if (strUser != "") TempSql.Append(" and UserID in(select UserID from t_User where (UserName like @User or RealName like @User))");
                if (strAddTime1 != "") TempSql.Append(" and AddTime >= @AddTime1");
                if (strAddTime2 != "") TempSql.Append(" and AddTime <= @AddTime+' 23:59:59'");
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
                if (strLogContent != "") listParams.Add(Config.Conn().CreateDbParameter("@LogContent", "%" + strLogContent + "%"));
                if (strScriptFile != "") listParams.Add(Config.Conn().CreateDbParameter("@ScriptFile", "%" + strScriptFile + "%"));
                if (strIPAddress != "") listParams.Add(Config.Conn().CreateDbParameter("@IPAddress", "%" + strIPAddress + "%"));
                if (strUser != "") listParams.Add(Config.Conn().CreateDbParameter("@User", "%" + strUser + "%"));
                if (strAddTime1 != "-1") listParams.Add(Config.Conn().CreateDbParameter("@AddTime1", strAddTime1));
                if (strAddTime2 != "-1") listParams.Add(Config.Conn().CreateDbParameter("@AddTime2", strAddTime2));
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
                TempUrl.Append("txtLogContent=" + Server.UrlEncode(strLogContent) + "&");
                TempUrl.Append("txtScriptFile=" + Server.UrlEncode(strScriptFile) + "&");
                TempUrl.Append("txtIPAddress=" + Server.UrlEncode(strIPAddress) + "&");
                TempUrl.Append("txtUser=" + Server.UrlEncode(strUser) + "&");
                TempUrl.Append("txtAddTime1=" + Server.UrlEncode(strAddTime1) + "&");
                TempUrl.Append("txtAddTime2=" + Server.UrlEncode(strAddTime2) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("UserLog");
            lbtnDel.Visible = GetData.LimitChk("UserLogDel");
            if (!Page.IsPostBack)
            {
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_UserLog where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strUserlogID = Config.Request(Request.Form["UserlogID"], "0");
            if (strUserlogID != "0")
            {
                string[] arrUserlogID = strUserlogID.Split(new char[] { ',' });
                StringBuilder strTempUserlogID = new StringBuilder();
                for (int i = 0; i < arrUserlogID.Length; i++)
                {
                    Factory.UserLog().DeleteInfo(arrUserlogID[i]);
                    strTempUserlogID.Append(arrUserlogID[i]);
                    if (i + 1 < arrUserlogID.Length) strTempUserlogID.Append(",");
                }
                Factory.AdminLog().InsertLog("删除会员日志。", Session["AdminID"].ToString());
                Config.MsgGotoUrl("删除成功!", "UserLog.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        //查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Repeater_Bind(repList);
        }
    }
}
