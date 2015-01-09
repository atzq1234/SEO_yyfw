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

namespace HxSoft.Web.Admin.Job
{
    public partial class Job : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010/11/2
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
                return Config.Request(Request["OrderKey"], "JobID");
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
        public string strJobName
        {
            get
            {
                return Config.Request(Request["txtJobName"], "");
            }
        }
        public string strDepartment
        {
            get
            {
                return Config.Request(Request["txtDepartment"], "");
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
                if (strJobName != "") TempSql.Append(" and JobName like @JobName");
                if (strDepartment != "") TempSql.Append(" and Department like @Department");
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
                if (strJobName != "") listParams.Add(Config.Conn().CreateDbParameter("@JobName", "%" + strJobName + "%"));
                if (strDepartment != "") listParams.Add(Config.Conn().CreateDbParameter("@Department", "%" + strDepartment + "%"));
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
                TempUrl.Append("txtJobName=" + Server.UrlEncode(strJobName) + "&");
                TempUrl.Append("txtDepartment=" + Server.UrlEncode(strDepartment) + "&");
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
            GetData.LimitChkMsg("Job");
            lbtnAdd.Visible = GetData.LimitChk("JobAdd");
            lbtnEdit.Visible = GetData.LimitChk("JobEdit");
            lbtnDel.Visible = GetData.LimitChk("JobDel");
            lbtnOpen.Visible = GetData.LimitChk("JobOpen");
            lbtnClose.Visible = GetData.LimitChk("JobClose");
            lbtnTransfer.Visible = GetData.LimitChk("JobTransfer");
            if (!Page.IsPostBack)
            {
                Factory.Class().ShowSelectTree("0", drpClassID, "",Config.SysJobMouldID);
                drpClassID.Items.Insert(0, new ListItem("不限", "-1"));
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Job where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strJobID = Config.Request(Request.Form["JobID"], "0");
            if (strJobID != "0")
            {
                Response.Redirect("Job_Add.aspx?JobID=" + strJobID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strJobID = Config.Request(Request.Form["JobID"], "0");
            if (strJobID != "0")
            {
                string[] arrJobID = strJobID.Split(new char[] { ',' });
                StringBuilder strTempJobID = new StringBuilder();
                JobModel jobModel = new JobModel();
                int n = 0;
                for (int i = 0; i < arrJobID.Length; i++)
                {
                    jobModel = Factory.Job().GetInfo(arrJobID[i]);
                    if (jobModel != null)
                    {
                        if (GetData.CheckAdminID(jobModel.AdminID, "JobAll"))//检查创建者
                        {
                            Factory.Job().DeleteInfo(arrJobID[i]);
                            strTempJobID.Append(arrJobID[i]);
                            if (i + 1 < arrJobID.Length) strTempJobID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempJobID.ToString() + "的招聘信息!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempJobID.ToString() + "招聘信息删除成功!", "Job.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strJobID = Config.Request(Request.Form["JobID"], "0");
            if (strJobID != "0")
            {
                string[] arrJobID = strJobID.Split(new char[] { ',' });
                StringBuilder strTempJobID = new StringBuilder();
                JobModel jobModel = new JobModel();
                int n = 0;
                for (int i = 0; i < arrJobID.Length; i++)
                {
                    jobModel = Factory.Job().GetInfo(arrJobID[i]);
                    if (jobModel != null)
                    {
                        if (GetData.CheckAdminID(jobModel.AdminID, "JobAll"))//检查创建者
                        {
                            Factory.Job().UpdateCloseStatus(arrJobID[i], "0");
                            strTempJobID.Append(arrJobID[i]);
                            if (i + 1 < arrJobID.Length) strTempJobID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempJobID.ToString() + "的招聘信息!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempJobID.ToString() + "招聘信息开放成功!", "Job.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strJobID = Config.Request(Request.Form["JobID"], "0");
            if (strJobID != "0")
            {
                string[] arrJobID = strJobID.Split(new char[] { ',' });
                StringBuilder strTempJobID = new StringBuilder();
                JobModel jobModel = new JobModel();
                int n = 0;
                for (int i = 0; i < arrJobID.Length; i++)
                {
                    jobModel = Factory.Job().GetInfo(arrJobID[i]);
                    if (jobModel != null)
                    {
                        if (GetData.CheckAdminID(jobModel.AdminID, "JobAll"))//检查创建者
                        {
                            Factory.Job().UpdateCloseStatus(arrJobID[i], "1");
                            strTempJobID.Append(arrJobID[i]);
                            if (i + 1 < arrJobID.Length) strTempJobID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempJobID.ToString() + "的招聘信息!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempJobID.ToString() + "招聘信息关闭成功!", "Job.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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

        //转移招聘
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            string strJobID = Config.Request(Request.Form["JobID"], "0");
            if (strJobID != "0")
            {
                Response.Redirect("Job_Transfer.aspx?JobID=" + strJobID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
    }
}
