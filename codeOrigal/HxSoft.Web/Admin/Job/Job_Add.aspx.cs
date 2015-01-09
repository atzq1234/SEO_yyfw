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
using System.Collections.Generic;
using System.Data.Common;

namespace HxSoft.Web.Admin.Job
{
    public partial class Job_Add : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010/11/2
        /// </summary>
        //定义全局变量
        public string JobID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["JobID"], 0).ToString();
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
            if (!Page.IsPostBack)
            {
                //站点列表
                GetData.GetConfigList(drpConfigID, "LanguageVer", "ConfigID");
                this.drpConfigID.Items.Insert(0, new ListItem("请选择", "-1"));
                
                //栏目分类
                //Factory.Class().ShowSelectTree("0", drpClassID, "",Config.SysJobMouldID);
                drpClassID.Items.Insert(0, new ListItem("请选择", "-1"));
                if (JobID == "0")
                {
                    GetData.LimitChkMsg("JobAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.Job().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("JobEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            JobModel jobModel = new JobModel();
            string strOldListID = hidlistID.Value;
            jobModel.ClassID = drpClassID.SelectedValue;
            jobModel.JobName = txtJobName.Text.Trim();
            jobModel.Department = txtDepartment.Text.Trim();
            jobModel.JobNum = txtJobNum.Text.Trim();
            jobModel.Salary = txtSalary.Text.Trim();
            jobModel.WorkPlace = txtWorkPlace.Text.Trim();
            jobModel.EndTime = txtEndTime.Text.Trim();
            jobModel.Tags = txtTags.Text.Trim();
            jobModel.Keywords = txtKeywords.Text.Trim();
            jobModel.Description = Config.HTMLCls(txtDescription.Text.Trim());
            jobModel.Demand = Config.HTMLCls(txtDemand.Text.Trim());
            //jobModel.Demand = txtDemand.Value;
            jobModel.ClickNum = txtClickNum.Text.Trim();
            jobModel.ListID = txtListID.Text.Trim();
            jobModel.AdminID = Session["AdminID"].ToString();
            jobModel.AddTime = DateTime.Now.ToString();
            jobModel.IsClose = radIsClose.SelectedValue;
            if (chkIsRecommend.Checked)
            {
                jobModel.IsRecommend = "1";
            }
            else
            {
                jobModel.IsRecommend = "0";
            }
            if (JobID == "0")
            {
                Factory.Job().OrderInfo(jobModel.ListID, strOldListID);
                Factory.Job().InsertInfo(jobModel);
                Factory.AdminLog().InsertLog("添加名称为\"" + jobModel.JobName + "\"的招聘。", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "Job.aspx");
            }
            else
            {
                JobModel jobModel_2 = new JobModel();
                jobModel_2 = Factory.Job().GetInfo(JobID);
                if (jobModel_2 != null)
                {
                    if (GetData.CheckAdminID(jobModel_2.AdminID, "JobAll"))//检查创建者
                    {
                        Factory.Job().OrderInfo(jobModel.ListID, strOldListID);
                        Factory.Job().UpdateInfo(jobModel, JobID);
                        Factory.AdminLog().InsertLog("修改编号为" + JobID + "的招聘。", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "Job.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            JobModel jobModel = new JobModel();
            jobModel = Factory.Job().GetInfo(JobID);
            if (jobModel != null)
            {
                if (GetData.CheckAdminID(jobModel.AdminID, "JobAll"))//检查创建者
                {
                    ClassModel claModel = Factory.Class().GetInfo(jobModel.ClassID);
                    if (claModel != null)
                    {
                        Config.setDefaultSelected(drpConfigID, claModel.ConfigID);
                        Factory.Class().ShowSelectTree("0", drpClassID, " and ConfigID=" + claModel.ConfigID, Config.SysJobMouldID);
                    }
                    Config.setDefaultSelected(drpClassID, jobModel.ClassID);
                    txtJobName.Text = jobModel.JobName;
                    txtDepartment.Text = jobModel.Department;
                    txtJobNum.Text = jobModel.JobNum;
                    txtSalary.Text = jobModel.Salary;
                    txtWorkPlace.Text = jobModel.WorkPlace;
                    txtEndTime.Text = Convert.ToDateTime(jobModel.EndTime).ToShortDateString();
                    txtTags.Text = jobModel.Tags;
                    txtKeywords.Text = jobModel.Keywords;
                    txtDescription.Text = Config.HTMLToTextarea(jobModel.Description);
                    txtDemand.Text = Config.HTMLToTextarea(jobModel.Demand);
                    //txtDemand.Value = jobModel.Demand;
                    txtClickNum.Text = jobModel.ClickNum;
                    txtListID.Text = jobModel.ListID;
                    hidlistID.Value = jobModel.ListID;
                    //txtAdminID.Text = jobModel.AdminID;
                    //txtAddTime.Text = jobModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, jobModel.IsClose);
                    if (jobModel.IsRecommend == "1")
                    {
                        chkIsRecommend.Checked = true;
                    }
                }
                else
                {
                    Config.ShowEnd("您没有查看此信息的权限！");
                }
            }
            else
            {
                Config.ShowEnd("您没有查看此信息的权限！");
            }
        }

        //栏目分类
        protected void drpConfigID_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpClassID.Items.Clear();
            Factory.Class().ShowSelectTree("0", drpClassID, " and ConfigID=" + drpConfigID.SelectedValue, Config.SysJobMouldID);
            drpClassID.Items.Insert(0, new ListItem("请选择", "-1"));
        }

    }
}
