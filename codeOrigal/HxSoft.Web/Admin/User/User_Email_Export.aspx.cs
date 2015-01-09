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
using System.IO;
using System.Data.Common;
using System.Collections.Generic;

namespace HxSoft.Web.Admin.User
{
    public partial class User_Email_Export : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        #region ****排序参数****
        public string strOrderKey
        {
            get
            {
                return Config.Request(Request["OrderKey"], "UserID");
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
                return " order by IsAudit asc," + strOrderKey + " " + strAscDesc1;
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
        public string strUserRankID
        {
            get
            {
                return Config.Request(Request["drpUserRankID"], "-1");
            }
        }
        public string strUserName
        {
            get
            {
                return Config.Request(Request["txtUserName"], "");
            }
        }
        public string strRealName
        {
            get
            {
                return Config.Request(Request["txtRealName"], "");
            }
        }
        public string strEmail
        {
            get
            {
                return Config.Request(Request["txtEmail"], "");
            }
        }
        public string strIsAudit
        {
            get
            {
                return Config.Request(Request["radIsAudit"], "-1");
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
                if (strUserRankID != "-1") TempSql.Append(" and UserRankID =@UserRankID");
                if (strUserName != "") TempSql.Append(" and UserName like @UserName");
                if (strRealName != "") TempSql.Append(" and RealName like @RealName");
                if (strEmail != "") TempSql.Append(" and Email like @Email");
                if (strIsAudit != "-1") TempSql.Append(" and IsAudit =@IsAudit");
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
                if (strUserRankID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@UserRankID", strUserRankID));
                if (strUserName != "") listParams.Add(Config.Conn().CreateDbParameter("@UserName", "%" + strUserName + "%"));
                if (strRealName != "") listParams.Add(Config.Conn().CreateDbParameter("@RealName", "%" + strRealName + "%"));
                if (strEmail != "") listParams.Add(Config.Conn().CreateDbParameter("@Email", "%" + strEmail + "%"));
                if (strIsAudit != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsAudit", strIsAudit));
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
                TempUrl.Append("drpUserRankID=" + Server.UrlEncode(strUserRankID) + "&");
                TempUrl.Append("txtUserName=" + Server.UrlEncode(strUserName) + "&");
                TempUrl.Append("txtRealName=" + Server.UrlEncode(strRealName) + "&");
                TempUrl.Append("txtEmail=" + Server.UrlEncode(strEmail) + "&");
                TempUrl.Append("radIsAudit=" + Server.UrlEncode(strIsAudit) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion

        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("UserEmailExport");
            string strFileName = "temp_" + Session["AdminID"].ToString() + ".txt";
            string strFilePath = Server.MapPath(strFileName);
            string sql = "select * from t_User where 1=1 " + SqlQuery + " order by UserID asc";
            Factory.User().EmailExport(sql, strFilePath, strFileName);
        }
    }
}
