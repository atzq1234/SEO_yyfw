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

namespace HxSoft.Web.Admin._System
{
    public partial class Admin : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
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
        public bool boolSetAdminGroup
        {
            get { return GetData.LimitChk("AdminSetAdminGroup"); }
        }
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("Admin");
            lbtnAdd.Visible = GetData.LimitChk("AdminAdd");
            lbtnEdit.Visible = GetData.LimitChk("AdminEdit");
            lbtnDel.Visible = GetData.LimitChk("AdminDel");
            lbtnOpen.Visible = GetData.LimitChk("AdminOpen");
            lbtnClose.Visible = GetData.LimitChk("AdminClose");
            //lbtnSetAdminGroup.Visible = GetData.LimitChk("AdminSetAdminGroup");
            if (!Page.IsPostBack)
            {
                //管理组
                Factory.Acc().DataBind("select * from t_AdminGroup order by ListID asc",  null,Config.DataBindObjTypeCollection.DropDownList.ToString(), drpAdminGroupID, "AdminGroupName", "AdminGroupID");
                drpAdminGroupID.Items.Insert(0,new ListItem("不限","-1"));
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Admin where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strAdminID = Config.Request(Request.Form["AdminID"], "0");
            if (strAdminID != "0")
            {
                Response.Redirect("Admin_Add.aspx?AdminID=" + strAdminID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strAdminID = Config.Request(Request.Form["AdminID"], "0");
            if (strAdminID != "0")
            {
                string[] arrAdminID = strAdminID.Split(new char[] { ',' });
                StringBuilder strTempAdminID = new StringBuilder();
                AdminModel admModel = new AdminModel();
                int n = 0;
                for (int i = 0; i < arrAdminID.Length; i++)
                {
                    admModel = Factory.Admin().GetInfo(arrAdminID[i]);
                    if (admModel != null)
                    {
                        if (GetData.CheckAdminID(admModel.ManageAdminID, "AdminAll") && arrAdminID[i] != "1")//检查创建者,不允许删除超级管理员
                        {
                            Factory.Admin().DeleteInfo(arrAdminID[i]);
                            strTempAdminID.Append(arrAdminID[i]);
                            if (i + 1 < arrAdminID.Length) strTempAdminID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempAdminID.ToString() + "的管理员!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempAdminID.ToString() + "管理员删除成功!", "Admin.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strAdminID = Config.Request(Request.Form["AdminID"], "0");
            if (strAdminID != "0")
            {
                string[] arrAdminID = strAdminID.Split(new char[] { ',' });
                StringBuilder strTempAdminID = new StringBuilder();
                AdminModel admModel = new AdminModel();
                int n = 0;
                for (int i = 0; i < arrAdminID.Length; i++)
                {
                    admModel = Factory.Admin().GetInfo(arrAdminID[i]);
                    if (admModel != null)
                    {
                        if (GetData.CheckAdminID(admModel.ManageAdminID, "AdminAll"))//检查创建者
                        {
                            Factory.Admin().UpdateCloseStatus(arrAdminID[i], "0");
                            strTempAdminID.Append(arrAdminID[i]);
                            if (i + 1 < arrAdminID.Length) strTempAdminID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempAdminID.ToString() + "的管理员!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempAdminID.ToString() + "管理员开放成功!", "Admin.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strAdminID = Config.Request(Request.Form["AdminID"], "0");
            if (strAdminID != "0")
            {
                string[] arrAdminID = strAdminID.Split(new char[] { ',' });
                StringBuilder strTempAdminID = new StringBuilder();
                AdminModel admModel = new AdminModel();
                int n = 0;
                for (int i = 0; i < arrAdminID.Length; i++)
                {
                    admModel = Factory.Admin().GetInfo(arrAdminID[i]);
                    if (admModel != null)
                    {
                        if (GetData.CheckAdminID(admModel.ManageAdminID, "AdminAll") && arrAdminID[i] != "1")//检查创建者,不允许关闭超级管理员
                        {
                            Factory.Admin().UpdateCloseStatus(arrAdminID[i], "1");
                            strTempAdminID.Append(arrAdminID[i]);
                            if (i + 1 < arrAdminID.Length) strTempAdminID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempAdminID.ToString() + "的管理员!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempAdminID.ToString() + "管理员关闭成功!", "Admin.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }
        //设置管理组
        protected void btnSetAdminGroup_Click(object sender, EventArgs e)
        {
            string strAdminID = Config.Request(Request.Form["AdminID"], "0");
            if (strAdminID != "0")
            {
                Response.Redirect("Admin_SetAdminGroup.aspx?AdminID=" + strAdminID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        protected void repList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string strAdminID = e.CommandArgument.ToString();
            //分配管理组
            if (e.CommandName == "SetAdminGroup")
            {
                Response.Redirect("Admin_SetAdminGroup.aspx?AdminID=" + strAdminID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        protected void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lbtnSetAdminGroup = (LinkButton)e.Item.FindControl("lbtnSetAdminGroup");
            lbtnSetAdminGroup.Enabled = boolSetAdminGroup;
        }


        //查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Repeater_Bind(repList);
        }
    }
}
