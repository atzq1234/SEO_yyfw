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
    public partial class SurveyItem : System.Web.UI.Page
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
                return Config.Request(Request["OrderKey"], "SurveyItemID");
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
        public string strItemName
        {
            get
            {
                return Config.Request(Request["txtItemName"], "");
            }
        }
        public string strTypeID
        {
            get
            {
                return Config.Request(Request["drpTypeID"], "-1");
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
                if (strItemName != "") TempSql.Append(" and ItemName like @ItemName");
                if (strTypeID != "-1") TempSql.Append(" and TypeID =@TypeID");
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
                if (strItemName != "") listParams.Add(Config.Conn().CreateDbParameter("@ItemName", "%" + strItemName + "%"));
                if (strTypeID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@TypeID", strTypeID));
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
                TempUrl.Append("txtItemName=" + Server.UrlEncode(strItemName) + "&");
                TempUrl.Append("drpTypeID=" + Server.UrlEncode(strTypeID) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("SurveyItem");
            lbtnAdd.Visible = GetData.LimitChk("SurveyItemAdd");
            lbtnEdit.Visible = GetData.LimitChk("SurveyItemEdit");
            lbtnDel.Visible = GetData.LimitChk("SurveyItemDel");
            lbtnOpen.Visible = GetData.LimitChk("SurveyItemOpen");
            lbtnClose.Visible = GetData.LimitChk("SurveyItemClose");
            if (!Page.IsPostBack)
            {
                lblSubject.Text = Factory.Survey().GetValueByField("Subject", SurveyID);
                Repeater_Bind(repList);

                btnQuery.PostBackUrl = "SurveyItem.aspx?SurveyID=" + SurveyID + "&ProductPage=" + ProductPage;
                lbtnAdd.OnClientClick = "javascript:return GoTo('SurveyItem_Add.aspx?SurveyID=" + SurveyID + "&ProductPage=" + ProductPage + "')";
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_SurveyItem where 1=1 and SurveyID=" + SurveyID + " " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?ProductPage=" + ProductPage + "&" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            string strSurveyItemID = Config.Request(Request.Form["SurveyItemID"], "0");
            if (strSurveyItemID != "0")
            {
                Response.Redirect("SurveyItem_Add.aspx?SurveyID=" + SurveyID + "&ProductPage=" + ProductPage + "&SurveyItemID=" + strSurveyItemID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strSurveyItemID = Config.Request(Request.Form["SurveyItemID"], "0");
            if (strSurveyItemID != "0")
            {
                string[] arrSurveyItemID = strSurveyItemID.Split(new char[] { ',' });
                StringBuilder strTempSurveyItemID = new StringBuilder();
                SurveyItemModel surItModel = new SurveyItemModel();
                int n = 0;
                for (int i = 0; i < arrSurveyItemID.Length; i++)
                {
                    surItModel = Factory.SurveyItem().GetInfo(arrSurveyItemID[i]);
                    if (surItModel != null)
                    {
                        if (GetData.CheckAdminID(surItModel.AdminID, "SurveyItemAll"))//检查创建者
                        {
                            Factory.SurveyItem().DeleteInfo(arrSurveyItemID[i]);
                            strTempSurveyItemID.Append(arrSurveyItemID[i]);
                            if (i + 1 < arrSurveyItemID.Length) strTempSurveyItemID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempSurveyItemID.ToString() + "的调查选项!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempSurveyItemID.ToString() + "的调查选项删除成功!", "SurveyItem.aspx?SurveyID=" + SurveyID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strSurveyItemID = Config.Request(Request.Form["SurveyItemID"], "0");
            if (strSurveyItemID != "0")
            {
                string[] arrSurveyItemID = strSurveyItemID.Split(new char[] { ',' });
                StringBuilder strTempSurveyItemID = new StringBuilder();
                SurveyItemModel surItModel = new SurveyItemModel();
                int n = 0;
                for (int i = 0; i < arrSurveyItemID.Length; i++)
                {
                    surItModel = Factory.SurveyItem().GetInfo(arrSurveyItemID[i]);
                    if (surItModel != null)
                    {
                        if (GetData.CheckAdminID(surItModel.AdminID, "SurveyItemAll"))//检查创建者
                        {
                            Factory.SurveyItem().UpdateCloseStatus(arrSurveyItemID[i], "0");
                            strTempSurveyItemID.Append(arrSurveyItemID[i]);
                            if (i + 1 < arrSurveyItemID.Length) strTempSurveyItemID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempSurveyItemID.ToString() + "的调查选项!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempSurveyItemID.ToString() + "的调查选项开放成功!", "SurveyItem.aspx?SurveyID=" + SurveyID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strSurveyItemID = Config.Request(Request.Form["SurveyItemID"], "0");
            if (strSurveyItemID != "0")
            {
                string[] arrSurveyItemID = strSurveyItemID.Split(new char[] { ',' });
                StringBuilder strTempSurveyItemID = new StringBuilder();
                SurveyItemModel surItModel = new SurveyItemModel();
                int n = 0;
                for (int i = 0; i < arrSurveyItemID.Length; i++)
                {
                    surItModel = Factory.SurveyItem().GetInfo(arrSurveyItemID[i]);
                    if (surItModel != null)
                    {
                        if (GetData.CheckAdminID(surItModel.AdminID, "SurveyItemAll"))//检查创建者
                        {
                            Factory.SurveyItem().UpdateCloseStatus(arrSurveyItemID[i], "1");
                            strTempSurveyItemID.Append(arrSurveyItemID[i]);
                            if (i + 1 < arrSurveyItemID.Length) strTempSurveyItemID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempSurveyItemID.ToString() + "的调查选项!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempSurveyItemID.ToString() + "的调查选项关闭成功!", "SurveyItem.aspx?SurveyID=" + SurveyID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
