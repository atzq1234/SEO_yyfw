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
    public partial class AdminGroup_Add : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string AdminGroupID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["AdminGroupID"], 0).ToString();
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
                return Config.Request(Request["OrderKey"], "ListID");
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
        public string strAdminGroupName
        {
            get
            {
                return Config.Request(Request["txtAdminGroupName"], "");
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
                if (strAdminGroupName != "") TempSql.Append(" and AdminGroupName like @AdminGroupName");
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
                if (strAdminGroupName != "") listParams.Add(Config.Conn().CreateDbParameter("@AdminGroupName", "%" + strAdminGroupName + "%"));
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
                TempUrl.Append("txtAdminGroupName=" + Server.UrlEncode(strAdminGroupName) + "&");
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
                if (AdminGroupID == "0")
                {
                    GetData.LimitChkMsg("AdminGroupAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.AdminGroup().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("AdminGroupEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdminGroupModel admGrModel = new AdminGroupModel();
            string strOldListID = hidlistID.Value;
            admGrModel.AdminGroupName = txtAdminGroupName.Text.Trim();
            admGrModel.LimitValues = "";
            admGrModel.ListID = txtListID.Text.Trim();
            admGrModel.AdminID = Session["AdminID"].ToString();
            admGrModel.AddTime = DateTime.Now.ToString();
            admGrModel.IsClose = radIsClose.SelectedValue;
            if (AdminGroupID == "0")
            {
                if (!Factory.AdminGroup().CheckInfo("AdminGroupName", admGrModel.AdminGroupName))
                {
                    Factory.AdminGroup().OrderInfo(admGrModel.ListID, strOldListID);
                    Factory.AdminGroup().InsertInfo(admGrModel);
                    Factory.AdminLog().InsertLog("添加名称为\"" + admGrModel.AdminGroupName + "\"的管理组。", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("添加成功！", "AdminGroup.aspx");
                }
                else
                {
                    errMsg.Text = "已存在相同管理组!";
                }
            }
            else
            {
                if (!Factory.AdminGroup().CheckInfo("AdminGroupName", admGrModel.AdminGroupName, AdminGroupID))
                {
                    AdminGroupModel admGrModel_2 = new AdminGroupModel();
                    admGrModel_2 = Factory.AdminGroup().GetInfo(AdminGroupID);
                    if (admGrModel_2 != null)
                    {
                        if (GetData.CheckAdminID(admGrModel_2.AdminID, "AdminGroupAll"))//检查创建者
                        {
                            Factory.AdminGroup().OrderInfo(admGrModel.ListID, strOldListID);
                            Factory.AdminGroup().UpdateInfo(admGrModel, AdminGroupID);
                            Factory.AdminLog().InsertLog("修改编号为" + AdminGroupID + "的管理组。", Session["AdminID"].ToString());
                            Config.MsgGotoUrl("修改成功！", "AdminGroup.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                        }
                    }
                }
                else
                {
                    errMsg.Text = "已存在相同管理组!";
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            AdminGroupModel admGrModel = new AdminGroupModel();
            admGrModel = Factory.AdminGroup().GetInfo(AdminGroupID);
            if (admGrModel != null)
            {
                if (GetData.CheckAdminID(admGrModel.AdminID, "AdminGroupAll"))//检查创建者
                {
                    txtAdminGroupName.Text = admGrModel.AdminGroupName;
                    //txtLimitValues.Text = admGrModel.LimitValues;
                    txtListID.Text = admGrModel.ListID;
                    hidlistID.Value = admGrModel.ListID;
                    //txtAdminID.Text = admGrModel.AdminID;
                    //txtAddTime.Text = admGrModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, admGrModel.IsClose);
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
