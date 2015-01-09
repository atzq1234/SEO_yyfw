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

namespace HxSoft.Web.Admin.Survey
{
    public partial class Survey : System.Web.UI.Page
    {
        /// <summary>
        ///在线调查
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
        #region ****排序参数****
        public string strOrderKey
        {
            get
            {
                return Config.Request(Request["OrderKey"], "SurveyID");
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
        public string strClassID
        {
            get
            {
                return Config.Request(Request["drpClassID"], "-1");
            }
        }
        public string strSubject
        {
            get
            {
                return Config.Request(Request["txtSubject"], "");
            }
        }
        public string strIsRecommend
        {
            get
            {
                return Config.Request(Request["radIsRecommend"], "-1");
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
                if (strClassID != "-1") TempSql.Append(" and (ClassID=@ClassID or ClassID in (" + Factory.Class().GetSubClassSql(strClassID) + "))");
                if (strSubject != "") TempSql.Append(" and Subject like @Subject");
                if (strIsRecommend != "-1") TempSql.Append(" and IsRecommend =@IsRecommend");
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
                if (strClassID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@ClassID", strClassID));
                if (strSubject != "") listParams.Add(Config.Conn().CreateDbParameter("@Subject", "%" + strSubject + "%"));
                if (strIsRecommend != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsRecommend", strIsRecommend));
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
                TempUrl.Append("drpClassID=" + Server.UrlEncode(strClassID) + "&");
                TempUrl.Append("txtSubject=" + Server.UrlEncode(strSubject) + "&");
                TempUrl.Append("radIsRecommend=" + Server.UrlEncode(strIsRecommend) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("Survey");
            lbtnAdd.Visible = GetData.LimitChk("SurveyAdd");
            lbtnEdit.Visible = GetData.LimitChk("SurveyEdit");
            lbtnDel.Visible = GetData.LimitChk("SurveyDel");
            lbtnOpen.Visible = GetData.LimitChk("SurveyOpen");
            lbtnClose.Visible = GetData.LimitChk("SurveyClose");
            lbtnTransfer.Visible = GetData.LimitChk("SurveyTransfer");
            if (!Page.IsPostBack)
            {
                //调查分类
                Factory.Class().ShowSelectTree("0", drpClassID, "", Config.SysSurveyMouldID);
                drpClassID.Items.Insert(0, new ListItem("不限", "-1"));
                
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Survey where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            string strSurveyID = Config.Request(Request.Form["SurveyID"], "0");
            if (strSurveyID != "0")
            {
                Response.Redirect("Survey_Add.aspx?SurveyID=" + strSurveyID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strSurveyID = Config.Request(Request.Form["SurveyID"], "0");
            if (strSurveyID != "0")
            {
                string[] arrSurveyID = strSurveyID.Split(new char[] { ',' });
                StringBuilder strTempSurveyID = new StringBuilder();
                SurveyModel surModel = new SurveyModel();
                int n = 0;
                for (int i = 0; i < arrSurveyID.Length; i++)
                {
                    surModel = Factory.Survey().GetInfo(arrSurveyID[i]);
                    if (surModel != null)
                    {
                        if (GetData.CheckAdminID(surModel.AdminID, "SurveyAll"))//检查创建者
                        {
                            Factory.Survey().DeleteInfo(arrSurveyID[i]);
                            strTempSurveyID.Append(arrSurveyID[i]);
                            if (i + 1 < arrSurveyID.Length) strTempSurveyID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempSurveyID.ToString() + "的在线调查!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempSurveyID.ToString() + "在线调查删除成功!", "Survey.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }


        //批量开放
        protected void btnOpen_Click(object sender, EventArgs e)
        {
            string strSurveyID = Config.Request(Request.Form["SurveyID"], "0");
            if (strSurveyID != "0")
            {
                string[] arrSurveyID = strSurveyID.Split(new char[] { ',' });
                StringBuilder strTempSurveyID = new StringBuilder();
                SurveyModel surModel = new SurveyModel();
                int n = 0;
                for (int i = 0; i < arrSurveyID.Length; i++)
                {
                    surModel = Factory.Survey().GetInfo(arrSurveyID[i]);
                    if (surModel != null)
                    {
                        if (GetData.CheckAdminID(surModel.AdminID, "SurveyAll"))//检查创建者
                        {
                            Factory.Survey().UpdateCloseStatus(arrSurveyID[i], "0");
                            strTempSurveyID.Append(arrSurveyID[i]);
                            if (i + 1 < arrSurveyID.Length) strTempSurveyID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempSurveyID.ToString() + "的在线调查!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempSurveyID.ToString() + "在线调查开放成功!", "Survey.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        //批量关闭
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string strSurveyID = Config.Request(Request.Form["SurveyID"], "0");
            if (strSurveyID != "0")
            {
                string[] arrSurveyID = strSurveyID.Split(new char[] { ',' });
                StringBuilder strTempSurveyID = new StringBuilder();
                SurveyModel surModel = new SurveyModel();
                int n = 0;
                for (int i = 0; i < arrSurveyID.Length; i++)
                {
                    surModel = Factory.Survey().GetInfo(arrSurveyID[i]);
                    if (surModel != null)
                    {
                        if (GetData.CheckAdminID(surModel.AdminID, "SurveyAll"))//检查创建者
                        {
                            Factory.Survey().UpdateCloseStatus(arrSurveyID[i], "1");
                            strTempSurveyID.Append(arrSurveyID[i]);
                            if (i + 1 < arrSurveyID.Length) strTempSurveyID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempSurveyID.ToString() + "的在线调查!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempSurveyID.ToString() + "在线调查关闭成功!", "Survey.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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


        //转移调查
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            string strSurveyID = Config.Request(Request.Form["SurveyID"], "0");
            if (strSurveyID != "0")
            {
                Response.Redirect("Survey_Transfer.aspx?SurveyID=" + strSurveyID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
    }
}
