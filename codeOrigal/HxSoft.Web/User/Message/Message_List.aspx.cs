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
    public partial class Message_List : System.Web.UI.Page
    {
        //this page add by yang
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
            drpDictionaryID_Bind();
            repList_Bind();
        }
        //留言分类
        private void drpDictionaryID_Bind()
        {
            Factory.Acc().DataBind("select * from t_Dictionary where IsClose=0 and ParentID=" + Config.SysMessageDictionaryMouldID + " order by ListID asc", null, Config.DataBindObjTypeCollection.Repeater.ToString(), repDictionaryID);
        }

        //绑定数据
        protected void repList_Bind()
        {
            string sql_f_1 = "(select count(*) from t_Message as b where b.ParentID=a.MessageID) as ReplyCount";
            string sql = "select *," + sql_f_1 + " from t_Message as a where 1=1 and ParentID=0 " + SqlQuery + " order by AddTime desc";
            pager.InnerHtml = Factory.Acc().DataPageBindForCn(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), repList, 10, page, "?" + UrlPara).ToString();
        }
    }
}