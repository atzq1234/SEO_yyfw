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

namespace HxSoft.Web.Admin.Survey
{
    public partial class SurveyResult_Details : System.Web.UI.Page
    {
        /// <summary>
        ///调查选顶 
        /// 创建人:杨小明
        /// 日期:2011-12-26
        /// </summary>
        //定义全局变量
        public string SurveyResultID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["SurveyResultID"], 0).ToString();
            }
        }
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        public string SurveyID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["SurveyID"], 0).ToString();
            }
        }
        public int ProductPage
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ProductPage"], 1);
            }
        }
        #region ****排序参数****
        public string strOrderKey
        {
            get
            {
                return Config.Request(Request["OrderKey"], "SurveyResultID");
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
        #endregion
        #region ****查询语句****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                return TempSql.ToString();
            }
        }
        #endregion
        #region ****Url参数****
        public string UrlPara
        {
            get
            {
                StringBuilder TempUrl = new StringBuilder("");
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
                ShowInfo();
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            SurveyResultModel surResModel = new SurveyResultModel();
            surResModel = Factory.SurveyResult().GetInfo(SurveyResultID);
            if (surResModel != null)
            {
                lblSurveyResult.Text = surResModel.SurveyContent;
            }
            else
            {
                Config.ShowEnd("您没有查看此信息的权限！");
            }
        }
    }
}
