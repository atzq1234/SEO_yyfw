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

namespace HxSoft.Web.Admin._System
{
    public partial class AdminLog : System.Web.UI.Page
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
                return Config.Request(Request["OrderKey"], "AdminLogID");
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
        public string strAdmin
        {
            get
            {
                return Config.Request(Request["txtAdmin"], "");
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
                if (strAdmin != "") TempSql.Append(" and AdminID in(select AdminID from t_Admin where (AdminName like @Admin or RealName like @Admin))");
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
                if (strAdmin != "") listParams.Add(Config.Conn().CreateDbParameter("@Admin", "%" + strAdmin + "%"));
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
                TempUrl.Append("txtAdmin=" + Server.UrlEncode(strAdmin) + "&");
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
            GetData.LimitChkMsg("AdminLog");
            lbtnDel.Visible = GetData.LimitChk("AdminLogDel");
            if (!IsPostBack)
            {
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_AdminLog where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strAdminLogID = Config.Request(Request.Form["AdminLogID"], "0");
            if (strAdminLogID != "0")
            {
                string[] arrAdminLogID = strAdminLogID.Split(new char[] { ',' });
                StringBuilder strTempAdminLogID = new StringBuilder();
                AdminLogModel admlogModel = new AdminLogModel();
                int n = 0;
                for (int i = 0; i < arrAdminLogID.Length; i++)
                {
                    admlogModel = Factory.AdminLog().GetInfo(arrAdminLogID[i]);
                    if (admlogModel != null)
                    {
                        if (GetData.CheckAdminID(admlogModel.AdminID, "AdminLogAll"))//检查创建者
                        {
                            Factory.AdminLog().DeleteInfo(arrAdminLogID[i]);
                            strTempAdminLogID.Append(arrAdminLogID[i]);
                            if (i + 1 < arrAdminLogID.Length) strTempAdminLogID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempAdminLogID.ToString() + "的管理员日志!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempAdminLogID.ToString() + "的管理员日志删除成功!", "AdminLog.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
