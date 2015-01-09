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
    public partial class AdminGroup_SetAdmin : System.Web.UI.Page
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
                GetData.LimitChkMsg("AdminGroupSetAdmin");
                if (AdminGroupID == "0")
                {
                    Config.ShowEnd("请选择要操作的记录!");
                }
                else
                {
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdminInGroupModel admInGrModel = new AdminInGroupModel();
            admInGrModel.AdminID = drpAdminID.SelectedValue;
            admInGrModel.AdminGroupID = AdminGroupID;

            if (!Factory.AdminInGroup().CheckInfo(admInGrModel.AdminID, admInGrModel.AdminGroupID))
            {
                AdminGroupModel admGrModel = new AdminGroupModel();
                admGrModel = Factory.AdminGroup().GetInfo(admInGrModel.AdminGroupID);
                if (admGrModel != null)
                {
                    if (GetData.CheckAdminID(admGrModel.AdminID, "AdminGroupAll"))//检查创建者
                    {
                        Factory.AdminInGroup().InsertInfo(admInGrModel);
                        Factory.AdminLog().InsertLog("向编号为" + admInGrModel.AdminGroupID + "的管理组分配编号为" + admInGrModel.AdminID + "的管理员!", Session["AdminID"].ToString());
                        Response.Redirect("AdminGroup_SetAdmin.aspx?AdminGroupID=" + admInGrModel.AdminGroupID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            //管理组
            AdminGroupModel admGrModel = new AdminGroupModel();
            admGrModel = Factory.AdminGroup().GetInfo(AdminGroupID);
            if (admGrModel != null)
            {
                if (GetData.CheckAdminID(admGrModel.AdminID, "AdminGroupAll"))//检查创建者
                {
                    lblAdminGroupName.Text = admGrModel.AdminGroupName;
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

            //管理员
            string strSql;
            if (Session["AdminID"].ToString() != Config.SystemAdminID)
            {
                strSql = " and AdminID<>" + Config.SystemAdminID;
            }
            else
            {
                strSql = "";
            }
            Factory.Acc().DataBind("select * from t_Admin where AdminID not in(select AdminID from t_AdminInGroup where AdminGroupID=" + AdminGroupID + ") "+strSql+"  order by AdminID asc", null,Config.DataBindObjTypeCollection.DropDownList.ToString(), drpAdminID, "AdminName", "AdminID");
            drpAdminID.Items.Insert(0, new ListItem("请选择", "-1"));

            //已分配管理组列表
            GridView1.DataKeyNames = new string[] { "AdminID", "AdminGroupID" };
            Factory.Acc().DataBind("select * from t_AdminInGroup where AdminGroupID=" + AdminGroupID + " order by AdminID asc", null,Config.DataBindObjTypeCollection.GridView.ToString(), GridView1);
        }

        //删除
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            AdminGroupModel admGrModel = new AdminGroupModel();
            admGrModel = Factory.AdminGroup().GetInfo(AdminGroupID);
            if (admGrModel != null)
            {
                if (admGrModel.AdminID == Session["AdminID"].ToString())//检查创建者
                {
                    string strAdminID = GridView1.DataKeys[e.RowIndex].Values["AdminID"].ToString();
                    string strAdminGroupID = GridView1.DataKeys[e.RowIndex].Values["AdminGroupID"].ToString();
                    Factory.AdminInGroup().DeleteInfo(strAdminID, strAdminGroupID);
                    Factory.AdminLog().InsertLog("删除管理组编号为" + strAdminGroupID + "与管理员编号为" + strAdminID + "的管理员分配!", Session["AdminID"].ToString());
                    Response.Redirect("AdminGroup_SetAdmin.aspx?AdminGroupID=" + AdminGroupID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
            }
        }
    }
}
