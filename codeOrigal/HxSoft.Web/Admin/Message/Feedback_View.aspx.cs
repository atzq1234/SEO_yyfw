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
    public partial class Feedback_Add : System.Web.UI.Page
    {
        /// <summary>
        ///信息反馈
        /// 创建人:杨小明
        /// 日期:2011-2-28
        /// </summary>
        //定义全局变量
        public string FeedbackID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["FeedbackID"], 0).ToString();
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
                return Config.Request(Request["OrderKey"], "FeedbackID");
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
        public string strDictionaryID
        {
            get
            {
                return Config.Request(Request["drpDictionaryID"], "-1");
            }
        }
        public string strTitle
        {
            get
            {
                return Config.Request(Request["txtTitle"], "");
            }
        }
        public string strFeedbackContent
        {
            get
            {
                return Config.Request(Request["txtFeedbackContent"], "");
            }
        }
        public string strIpAddress
        {
            get
            {
                return Config.Request(Request["txtIpAddress"], "");
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
        public string strIsDeal
        {
            get
            {
                return Config.Request(Request["radIsDeal"], "-1");
            }
        }
        #endregion
        #region ****查询语句****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (strDictionaryID != "-1") TempSql.Append(" and DictionaryID =@DictionaryID");
                if (strTitle != "") TempSql.Append(" and Title like @Title");
                if (strFeedbackContent != "") TempSql.Append(" and FeedbackContent like @FeedbackContent");
                if (strIpAddress != "") TempSql.Append(" and IpAddress like @IpAddress");
                if (strAddTime1 != "") TempSql.Append(" and AddTime >= @AddTime1");
                if (strAddTime2 != "") TempSql.Append(" and AddTime <= @AddTime2+' 23:59:59'");
                if (strIsDeal != "-1") TempSql.Append(" and IsDeal =@IsDeal");
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
                if (strFeedbackContent != "") listParams.Add(Config.Conn().CreateDbParameter("@FeedbackContent", "%" + strFeedbackContent + "%"));
                if (strIpAddress != "") listParams.Add(Config.Conn().CreateDbParameter("@IpAddress", "%" + strIpAddress + "%"));
                if (strAddTime1 != "") listParams.Add(Config.Conn().CreateDbParameter("@AddTime1", strAddTime1));
                if (strAddTime2 != "") listParams.Add(Config.Conn().CreateDbParameter("@AddTime2", strAddTime2));
                if (strIsDeal != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsDeal", strIsDeal));
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
                TempUrl.Append("drpDictionaryID=" + Server.UrlEncode(strDictionaryID) + "&");
                TempUrl.Append("txtTitle=" + Server.UrlEncode(strTitle) + "&");
                TempUrl.Append("txtFeedbackContent=" + Server.UrlEncode(strFeedbackContent) + "&");
                TempUrl.Append("txtIpAddress=" + Server.UrlEncode(strIpAddress) + "&");
                TempUrl.Append("txtAddTime1=" + Server.UrlEncode(strAddTime1) + "&");
                TempUrl.Append("txtAddTime2=" + Server.UrlEncode(strAddTime2) + "&");
                TempUrl.Append("radIsDeal=" + Server.UrlEncode(strIsDeal) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("FeedbackDeal");
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            StringBuilder strDealMeno = new StringBuilder();
            strDealMeno.Append("处理结果:" + Config.HTMLCls(txtDealMeno.Text.Trim()) + "<br>");
            strDealMeno.Append("处理人:" + Factory.Admin().GetValueByField("AdminName",Session["AdminID"].ToString()) + "<br>");
            strDealMeno.Append("处理时间:" + DateTime.Now.ToString() + "<br>");
            strDealMeno.Append("=======================================<br>");
            FeedbackModel feeModel = new FeedbackModel();
            feeModel.IsDeal = "1";
            feeModel.DealMeno = lblDealMeno.Text + strDealMeno.ToString();
            Factory.Feedback().DealInfo(feeModel, FeedbackID);
            Factory.AdminLog().InsertLog("处理编号为" + FeedbackID.ToString() + "的信息反馈!", Session["AdminID"].ToString());
            Config.MsgGotoUrl("处理成功！", "Feedback.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
        }
        //显示数据
        protected void ShowInfo()
        {
            FeedbackModel feeModel = new FeedbackModel();
            feeModel = Factory.Feedback().GetInfo(FeedbackID);
            if (feeModel != null)
            {
                lblFeedbackContent.Text = feeModel.FeedbackContent;
                lblDealMeno.Text = feeModel.DealMeno;
            }
            else
            {
                Config.ShowEnd("您没有查看此信息的权限！");
            }
        }

    }
}
