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

namespace HxSoft.Web.Admin._System
{
    public partial class Admin_Add : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string AdminID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["AdminID"], 0).ToString();
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
                return Config.Request(Request["OrderKey"], "AdminID");
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
        public string strAdminName
        {
            get
            {
                return Config.Request(Request["txtAdminName"], "");
            }
        }
        public string strAdminGroupID
        {
            get
            {
                return Config.Request(Request["drpAdminGroupID"], "-1");
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
        public string strDepartment
        {
            get
            {
                return Config.Request(Request["txtDepartment"], "");
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
                if (strAdminName != "") TempSql.Append(" and AdminName like @AdminName");
                if (strAdminGroupID != "-1") TempSql.Append(" and AdminID in(select AdminID from t_AdminInGroup where AdminGroupID=@AdminGroupID)");
                if (strRealName != "") TempSql.Append(" and RealName like @RealName");
                if (strEmail != "") TempSql.Append(" and Email like @Email");
                if (strDepartment != "") TempSql.Append(" and Department like @Department");
                if (strIsClose != "-1") TempSql.Append(" and IsClose=@IsClose");
                if (Session["AdminID"].ToString() != Config.SystemAdminID) TempSql.Append(" and AdminID<>" + Config.SystemAdminID);
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
                if (strAdminName != "") listParams.Add(Config.Conn().CreateDbParameter("@AdminName", "%" + strAdminName + "%"));
                if (strAdminGroupID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@AdminGroupID", strAdminGroupID));
                if (strRealName != "") listParams.Add(Config.Conn().CreateDbParameter("@RealName", "%" + strRealName + "%"));
                if (strEmail != "") listParams.Add(Config.Conn().CreateDbParameter("@Email", "%" + strEmail + "%"));
                if (strDepartment != "") listParams.Add(Config.Conn().CreateDbParameter("@Department", "%" + strDepartment + "%"));
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
                TempUrl.Append("txtAdminName=" + Server.UrlEncode(strAdminName) + "&");
                TempUrl.Append("drpAdminGroupID=" + Server.UrlEncode(strAdminGroupID) + "&");
                TempUrl.Append("txtRealName=" + Server.UrlEncode(strRealName) + "&");
                TempUrl.Append("txtEmail=" + Server.UrlEncode(strEmail) + "&");
                TempUrl.Append("txtDepartment=" + Server.UrlEncode(strDepartment) + "&");
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
                if (AdminID == "0")
                {
                    GetData.LimitChkMsg("AdminAdd");
                    lblTitle.Text = "添加";
                }
                else
                {
                    GetData.LimitChkMsg("AdminEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdminModel admModel = new AdminModel();
            admModel.AdminName = txtAdminName.Text.Trim();
            string strAdminPass = txtAdminPass.Text.Trim();
            admModel.RealName = txtRealName.Text.Trim();
            admModel.Email = txtEmail.Text.Trim();
            admModel.Department = txtDepartment.Text.Trim();
            admModel.Comment = Config.HTMLCls(txtComment.Text.Trim());
            admModel.LoginNum = "0";
            admModel.LastLoginTime = "1900-1-1";
            admModel.ThisLoginTime = "1900-1-1";
            admModel.ManageAdminID = Session["AdminID"].ToString();
            admModel.AddTime = DateTime.Now.ToString();
            admModel.IsClose = radIsClose.SelectedValue; ;
            if (AdminID == "0")
            {
                if (!Factory.Admin().CheckInfo("AdminName", admModel.AdminName))
                {
                    admModel.AdminPass = Config.md5(strAdminPass);
                    Factory.Admin().InsertInfo(admModel);
                    Factory.AdminLog().InsertLog("添加帐号为\"" + admModel.AdminName + "\"的管理员。", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("添加成功！", "Admin.aspx");
                }
                else
                {
                    errMsg.Text = "已存在相同管理员帐号!";
                }
            }
            else
            {
                if (!Factory.Admin().CheckInfo("AdminName", admModel.AdminName, AdminID))
                {
                    AdminModel admModel_2 = new AdminModel();
                    admModel_2 = Factory.Admin().GetInfo(AdminID);
                    if (admModel_2 != null)
                    {
                        if (GetData.CheckAdminID(admModel_2.ManageAdminID, "AdminAll"))//检查创建者
                        {
                            if (strAdminPass != string.Empty)//修改密码
                            {
                                admModel.AdminPass = Config.md5(strAdminPass);
                            }
                            else
                            {
                                admModel.AdminPass = admModel_2.AdminPass;
                            }
                            Factory.Admin().UpdateInfo(admModel, AdminID);
                            Factory.AdminLog().InsertLog("修改编号为" + AdminID + "的管理员。", Session["AdminID"].ToString());
                            Config.MsgGotoUrl("修改成功！", "Admin.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                        }
                    }
                }
                else
                {
                    errMsg.Text = "已存在相同管理员帐号!";
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            if (Session["AdminID"].ToString() != Config.SystemAdminID && AdminID == Config.SystemAdminID)
            {
                Config.ShowEnd("您没有查看此信息的权限！");
            }
            else
            {
                AdminModel admModel = new AdminModel();
                admModel = Factory.Admin().GetInfo(AdminID);
                if (admModel != null)
                {
                    if (GetData.CheckAdminID(admModel.ManageAdminID, "AdminAll"))//检查创建者
                    {
                        txtAdminName.Text = admModel.AdminName;
                        //txtAdminPass.Text = admModel.AdminPass;
                        RequiredFieldValidator2.Enabled = false;
                        txtRealName.Text = admModel.RealName;
                        txtEmail.Text = admModel.Email;
                        txtDepartment.Text = admModel.Department;
                        txtComment.Text = Config.HTMLToTextarea(admModel.Comment);
                        //txtLoginNum.Text = admModel.LoginNum;
                        //txtLastLoginTime.Text = admModel.LastLoginTime;
                        //txtThisLoginTime.Text = admModel.ThisLoginTime;
                        //txtManageAdminID.Text = admModel.ManageAdminID;
                        //txtAddTime.Text = admModel.AddTime;
                        radIsClose.ClearSelection();
                        Config.setDefaultSelected(radIsClose, admModel.IsClose);
                    }
                    else
                    {
                        Config.ShowEnd("您没有查看此信息的权限");
                    }
                }
                else
                {
                    Config.ShowEnd("您没有查看此信息的权限！");
                }
            }
        }
    }
}
