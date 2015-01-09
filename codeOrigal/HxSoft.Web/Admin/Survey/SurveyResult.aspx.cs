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
    public partial class SurveyResult : System.Web.UI.Page
    {
        /// <summary>
        ///调查选顶 
        /// 创建人:杨小明
        /// 日期:2011-12-26
        /// </summary>
        //定义全局变量
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
            GetData.LimitChkMsg("SurveyResult");
            lbtnDel.Visible = GetData.LimitChk("SurveyResultDel");
            if (!Page.IsPostBack)
            {
                lblSubject.Text = Factory.Survey().GetValueByField("Subject", SurveyID);
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_SurveyResult where 1=1 and SurveyID=" + SurveyID + " " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, null,Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?ProductPage=" + ProductPage + "&" + UrlOrderPara + UrlPara).ToString();
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strSurveyResultID = Config.Request(Request.Form["SurveyResultID"], "0");
            if (strSurveyResultID != "0")
            {
                string[] arrSurveyResultID = strSurveyResultID.Split(new char[] { ',' });
                StringBuilder strTempSurveyResultID = new StringBuilder();
                for (int i = 0; i < arrSurveyResultID.Length; i++)
                {
                    Factory.SurveyResult().DeleteInfo(arrSurveyResultID[i]);
                    strTempSurveyResultID.Append(arrSurveyResultID[i]);
                    if (i + 1 < arrSurveyResultID.Length) strTempSurveyResultID.Append(",");
                }
                Factory.AdminLog().InsertLog("删除编号为" + strTempSurveyResultID.ToString() + "的调查结果!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("删除成功!", "SurveyResult.aspx?ProductPage=" + ProductPage + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

    }
}
