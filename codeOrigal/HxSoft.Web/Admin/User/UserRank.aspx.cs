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

namespace HxSoft.Web.Admin.User
{
    public partial class UserRank : System.Web.UI.Page
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
        public string strUserRankName
        {
            get
            {
                return Config.Request(Request["txtUserRankName"], "");
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
                if (strUserRankName != "") TempSql.Append(" and UserRankName like @UserRankName");
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
                if (strUserRankName != "") listParams.Add(Config.Conn().CreateDbParameter("@UserRankName", "%" + strUserRankName + "%"));
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
                TempUrl.Append("txtUserRankName=" + Server.UrlEncode(strUserRankName) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        public bool boolSetLimit
        {
            get { return GetData.LimitChk("UserRankSetLimit"); }
        }
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("UserRank");
            lbtnAdd.Visible = GetData.LimitChk("UserRankAdd");
            lbtnEdit.Visible = GetData.LimitChk("UserRankEdit");
            lbtnDel.Visible = GetData.LimitChk("UserRankDel");
            lbtnOpen.Visible = GetData.LimitChk("UserRankOpen");
            lbtnClose.Visible = GetData.LimitChk("UserRankClose");
            //lbtnSetAdmin.Visible = GetData.LimitChk("UserRankSetAdmin");
            //lbtnSetLimit.Visible = GetData.LimitChk("UserRankSetLimit");
            if (!Page.IsPostBack)
            {
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_UserRank where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strUserRankID = Config.Request(Request.Form["UserRankID"], "0");
            if (strUserRankID != "0")
            {
                Response.Redirect("UserRank_Add.aspx?UserRankID=" + strUserRankID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strUserRankID = Config.Request(Request.Form["UserRankID"], "0");
            if (strUserRankID != "0")
            {
                string[] arrUserRankID = strUserRankID.Split(new char[] { ',' });
                StringBuilder strTempUserRankID = new StringBuilder();
                UserRankModel userRankModel = new UserRankModel();
                int n = 0;
                for (int i = 0; i < arrUserRankID.Length; i++)
                {
                    userRankModel = Factory.UserRank().GetInfo(arrUserRankID[i]);
                    if (userRankModel != null)
                    {
                        if (GetData.CheckAdminID(userRankModel.AdminID, "UserRankAll"))//检查创建者
                        {
                            Factory.UserRank().DeleteInfo(arrUserRankID[i]);
                            strTempUserRankID.Append(arrUserRankID[i]);
                            if (i + 1 < arrUserRankID.Length) strTempUserRankID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempUserRankID.ToString() + "的会员级别!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempUserRankID.ToString() + "会员级别删除成功!", "UserRank.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strUserRankID = Config.Request(Request.Form["UserRankID"], "0");
            if (strUserRankID != "0")
            {
                string[] arrUserRankID = strUserRankID.Split(new char[] { ',' });
                StringBuilder strTempUserRankID = new StringBuilder();
                UserRankModel userRankModel = new UserRankModel();
                int n = 0;
                for (int i = 0; i < arrUserRankID.Length; i++)
                {
                    userRankModel = Factory.UserRank().GetInfo(arrUserRankID[i]);
                    if (userRankModel != null)
                    {
                        if (GetData.CheckAdminID(userRankModel.AdminID, "UserRankAll"))//检查创建者
                        {
                            Factory.UserRank().UpdateCloseStatus(arrUserRankID[i],"0");
                            strTempUserRankID.Append(arrUserRankID[i]);
                            if (i + 1 < arrUserRankID.Length) strTempUserRankID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempUserRankID.ToString() + "的会员级别!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempUserRankID.ToString() + "会员级别开放成功!", "UserRank.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strUserRankID = Config.Request(Request.Form["UserRankID"], "0");
            if (strUserRankID != "0")
            {
                string[] arrUserRankID = strUserRankID.Split(new char[] { ',' });
                StringBuilder strTempUserRankID = new StringBuilder();
                UserRankModel userRankModel = new UserRankModel();
                int n = 0;
                for (int i = 0; i < arrUserRankID.Length; i++)
                {
                    userRankModel = Factory.UserRank().GetInfo(arrUserRankID[i]);
                    if (userRankModel != null)
                    {
                        if (GetData.CheckAdminID(userRankModel.AdminID, "UserRankAll"))//检查创建者
                        {
                            Factory.UserRank().UpdateCloseStatus(arrUserRankID[i],"1");
                            strTempUserRankID.Append(arrUserRankID[i]);
                            if (i + 1 < arrUserRankID.Length) strTempUserRankID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempUserRankID.ToString() + "的会员级别!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempUserRankID.ToString() + "会员级别关闭成功!", "UserRank.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        protected void repList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string strUserRankID = e.CommandArgument.ToString();
            //设置操作权限
            if (e.CommandName == "SetLimit")
            {
                Response.Redirect("UserRank_SetLimit.aspx?UserRankID=" + strUserRankID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        protected void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
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
