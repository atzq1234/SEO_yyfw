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
    public partial class AdminGroup : System.Web.UI.Page
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
        public bool boolSetAdmin
        {
            get { return GetData.LimitChk("AdminGroupSetAdmin"); }
        }
        public bool boolSetLimit
        {
            get { return GetData.LimitChk("AdminGroupSetLimit"); }
        }
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("AdminGroup");
            lbtnAdd.Visible = GetData.LimitChk("AdminGroupAdd");
            lbtnEdit.Visible = GetData.LimitChk("AdminGroupEdit");
            lbtnDel.Visible = GetData.LimitChk("AdminGroupDel");
            lbtnOpen.Visible = GetData.LimitChk("AdminGroupOpen");
            lbtnClose.Visible = GetData.LimitChk("AdminGroupClose");
            //lbtnSetAdmin.Visible = GetData.LimitChk("AdminGroupSetAdmin");
            //lbtnSetLimit.Visible = GetData.LimitChk("AdminGroupSetLimit");
            if (!Page.IsPostBack)
            {
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_AdminGroup where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strAdminGroupID = Config.Request(Request.Form["AdminGroupID"], "0");
            if (strAdminGroupID != "0")
            {
                Response.Redirect("AdminGroup_Add.aspx?AdminGroupID=" + strAdminGroupID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strAdminGroupID = Config.Request(Request.Form["AdminGroupID"], "0");
            if (strAdminGroupID != "0")
            {
                string[] arrAdminGroupID = strAdminGroupID.Split(new char[] { ',' });
                StringBuilder strTempAdminGroupID = new StringBuilder();
                AdminGroupModel admGrModel = new AdminGroupModel();
                int n = 0;
                for (int i = 0; i < arrAdminGroupID.Length; i++)
                {
                    admGrModel = Factory.AdminGroup().GetInfo(arrAdminGroupID[i]);
                    if (admGrModel != null)
                    {
                        if (GetData.CheckAdminID(admGrModel.AdminID, "AdminGroupAll"))//检查创建者
                        {
                            Factory.AdminGroup().DeleteInfo(arrAdminGroupID[i]);
                            strTempAdminGroupID.Append(arrAdminGroupID[i]);
                            if (i + 1 < arrAdminGroupID.Length) strTempAdminGroupID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempAdminGroupID.ToString() + "的管理组!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempAdminGroupID.ToString() + "管理组删除成功!", "AdminGroup.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strAdminGroupID = Config.Request(Request.Form["AdminGroupID"], "0");
            if (strAdminGroupID != "0")
            {
                string[] arrAdminGroupID = strAdminGroupID.Split(new char[] { ',' });
                StringBuilder strTempAdminGroupID = new StringBuilder();
                AdminGroupModel admGrModel = new AdminGroupModel();
                int n = 0;
                for (int i = 0; i < arrAdminGroupID.Length; i++)
                {
                    admGrModel = Factory.AdminGroup().GetInfo(arrAdminGroupID[i]);
                    if (admGrModel != null)
                    {
                        if (GetData.CheckAdminID(admGrModel.AdminID, "AdminGroupAll"))//检查创建者
                        {
                            Factory.AdminGroup().UpdateCloseStatus(arrAdminGroupID[i],"0");
                            strTempAdminGroupID.Append(arrAdminGroupID[i]);
                            if (i + 1 < arrAdminGroupID.Length) strTempAdminGroupID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempAdminGroupID.ToString() + "的管理组!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempAdminGroupID.ToString() + "管理组开放成功!", "AdminGroup.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strAdminGroupID = Config.Request(Request.Form["AdminGroupID"], "0");
            if (strAdminGroupID != "0")
            {
                string[] arrAdminGroupID = strAdminGroupID.Split(new char[] { ',' });
                StringBuilder strTempAdminGroupID = new StringBuilder();
                AdminGroupModel admGrModel = new AdminGroupModel();
                int n = 0;
                for (int i = 0; i < arrAdminGroupID.Length; i++)
                {
                    admGrModel = Factory.AdminGroup().GetInfo(arrAdminGroupID[i]);
                    if (admGrModel != null)
                    {
                        if (GetData.CheckAdminID(admGrModel.AdminID, "AdminGroupAll"))//检查创建者
                        {
                            Factory.AdminGroup().UpdateCloseStatus(arrAdminGroupID[i],"1");
                            strTempAdminGroupID.Append(arrAdminGroupID[i]);
                            if (i + 1 < arrAdminGroupID.Length) strTempAdminGroupID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempAdminGroupID.ToString() + "的管理组!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempAdminGroupID.ToString() + "管理组关闭成功!", "AdminGroup.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        protected void repList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string strAdminGroupID = e.CommandArgument.ToString();
            //设置管理员
            if (e.CommandName == "SetAdmin")
            {
                Response.Redirect("AdminGroup_SetAdmin.aspx?AdminGroupID=" + strAdminGroupID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
            //设置操作权限
            if (e.CommandName == "SetLimit")
            {
                Response.Redirect("AdminGroup_SetLimit.aspx?AdminGroupID=" + strAdminGroupID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        protected void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lbtnSetAdmin = (LinkButton)e.Item.FindControl("lbtnSetAdmin");
            lbtnSetAdmin.Enabled = boolSetAdmin;

            LinkButton lbtnSetLimit = (LinkButton)e.Item.FindControl("lbtnSetLimit");
            lbtnSetLimit.Enabled = boolSetLimit;
        }

        //查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Repeater_Bind(repList);
        }
    }
}
