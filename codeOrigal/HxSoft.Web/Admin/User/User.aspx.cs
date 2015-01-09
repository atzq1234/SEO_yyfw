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
    public partial class User : System.Web.UI.Page
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
                return Config.Request(Request["OrderKey"], "UserID");
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
                return " order by IsAudit asc," + strOrderKey + " " + strAscDesc1;
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
        public string strUserRankID
        {
            get
            {
                return Config.Request(Request["drpUserRankID"], "-1");
            }
        }
        public string strUserName
        {
            get
            {
                return Config.Request(Request["txtUserName"], "");
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
        public string strIsAudit
        {
            get
            {
                return Config.Request(Request["radIsAudit"], "-1");
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
                if (strUserRankID != "-1") TempSql.Append(" and UserRankID =@UserRankID");
                if (strUserName != "") TempSql.Append(" and UserName like @UserName");
                if (strRealName != "") TempSql.Append(" and RealName like @RealName");
                if (strEmail != "") TempSql.Append(" and Email like @Email");
                if (strIsAudit != "-1") TempSql.Append(" and IsAudit =@IsAudit");
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
                if (strUserRankID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@UserRankID", strUserRankID));
                if (strUserName != "") listParams.Add(Config.Conn().CreateDbParameter("@UserName", "%" + strUserName + "%"));
                if (strRealName != "") listParams.Add(Config.Conn().CreateDbParameter("@RealName", "%" + strRealName + "%"));
                if (strEmail != "") listParams.Add(Config.Conn().CreateDbParameter("@Email", "%" + strEmail + "%"));
                if (strIsAudit != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsAudit", strIsAudit));
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
                TempUrl.Append("drpUserRankID=" + Server.UrlEncode(strUserRankID) + "&");
                TempUrl.Append("txtUserName=" + Server.UrlEncode(strUserName) + "&");
                TempUrl.Append("txtRealName=" + Server.UrlEncode(strRealName) + "&");
                TempUrl.Append("txtEmail=" + Server.UrlEncode(strEmail) + "&");
                TempUrl.Append("radIsAudit=" + Server.UrlEncode(strIsAudit) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("User");
            lbtnAdd.Visible = GetData.LimitChk("UserAdd");
            lbtnEdit.Visible = GetData.LimitChk("UserEdit");
            lbtnDel.Visible = GetData.LimitChk("UserDel");
            lbtnOpen.Visible = GetData.LimitChk("UserOpen");
            lbtnClose.Visible = GetData.LimitChk("UserClose");
            lbtnAudit.Visible = GetData.LimitChk("UserAudit");
            lbtnNoAudit.Visible = GetData.LimitChk("UserNoAudit");
            lbtnImport.Visible = GetData.LimitChk("UserImport");
            lbtnExport.Visible = GetData.LimitChk("UserExport");
            lbtnExport.OnClientClick = "javascript:return GoTo('User_Export.aspx?" + UrlPara + "')";
            lbtnEmailExport.Visible = GetData.LimitChk("UserEmailExport");
            lbtnEmailExport.OnClientClick = "javascript:return GoTo('User_Email_Export.aspx?" + UrlPara + "')";
            if (!Page.IsPostBack)
            {
                //会员级别
                Factory.Acc().DataBind("select UserRankID,UserRankName from t_UserRank order by ListID asc", null,Config.DataBindObjTypeCollection.DropDownList.ToString(), drpUserRankID, "UserRankName", "UserRankID");
                drpUserRankID.Items.Insert(0, new ListItem("不限", "-1"));
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_User where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strUserID = Config.Request(Request.Form["UserID"], "0");
            if (strUserID != "0")
            {
                Response.Redirect("User_Add.aspx?UserID=" + strUserID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strUserID = Config.Request(Request.Form["UserID"], "0");
            if (strUserID != "0")
            {
                string[] arrUserID = strUserID.Split(new char[] { ',' });
                StringBuilder strTempUserID = new StringBuilder();
                UserModel userModel = new UserModel();
                int n = 0;
                for (int i = 0; i < arrUserID.Length; i++)
                {
                    userModel = Factory.User().GetInfo(arrUserID[i]);
                    if (userModel != null)
                    {
                        //if (GetData.CheckAdminID(userModel.AdminID, "UserAll"))//检查创建者
                        //{
                            Factory.User().DeleteInfo(arrUserID[i]);
                            strTempUserID.Append(arrUserID[i]);
                            if (i + 1 < arrUserID.Length) strTempUserID.Append(",");
                            n++;
                        //}
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempUserID.ToString() + "的会员!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempUserID.ToString() + "会员删除成功!", "User.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strUserID = Config.Request(Request.Form["UserID"], "0");
            if (strUserID != "0")
            {
                string[] arrUserID = strUserID.Split(new char[] { ',' });
                StringBuilder strTempUserID = new StringBuilder();
                UserModel userModel = new UserModel();
                int n = 0;
                for (int i = 0; i < arrUserID.Length; i++)
                {
                    userModel = Factory.User().GetInfo(arrUserID[i]);
                    if (userModel != null)
                    {
                        //if (GetData.CheckAdminID(userModel.AdminID, "UserAll"))//检查创建者
                        //{
                            Factory.User().UpdateCloseStatus(arrUserID[i], "0");
                            strTempUserID.Append(arrUserID[i]);
                            if (i + 1 < arrUserID.Length) strTempUserID.Append(",");
                            n++;
                        //}
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempUserID.ToString() + "的会员!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempUserID.ToString() + "会员开放成功!", "User.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strUserID = Config.Request(Request.Form["UserID"], "0");
            if (strUserID != "0")
            {
                string[] arrUserID = strUserID.Split(new char[] { ',' });
                StringBuilder strTempUserID = new StringBuilder();
                UserModel userModel = new UserModel();
                int n = 0;
                for (int i = 0; i < arrUserID.Length; i++)
                {
                    userModel = Factory.User().GetInfo(arrUserID[i]);
                    if (userModel != null)
                    {
                        //if (GetData.CheckAdminID(userModel.AdminID, "UserAll"))//检查创建者
                        //{
                            Factory.User().UpdateCloseStatus(arrUserID[i], "1");
                            strTempUserID.Append(arrUserID[i]);
                            if (i + 1 < arrUserID.Length) strTempUserID.Append(",");
                            n++;
                       // }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempUserID.ToString() + "的会员!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempUserID.ToString() + "会员关闭成功!", "User.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        //审核
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            string strUserID = Config.Request(Request.Form["UserID"], "0");
            if (strUserID != "0")
            {
                string[] arrUserID = strUserID.Split(new char[] { ',' });
                StringBuilder strTempUserID = new StringBuilder();
                UserModel userModel = new UserModel();
                int n = 0;
                for (int i = 0; i < arrUserID.Length; i++)
                {
                    userModel = Factory.User().GetInfo(arrUserID[i]);
                    if (userModel != null)
                    {
                        //if (GetData.CheckAdminID(userModel.AdminID, "UserAll"))//检查创建者
                        //{
                            Factory.User().UpdateAuditStatus(arrUserID[i], "1");
                            strTempUserID.Append(arrUserID[i]);
                            if (i + 1 < arrUserID.Length) strTempUserID.Append(",");
                            n++;
                       // }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("审核编号为" + strTempUserID.ToString() + "的会员!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempUserID.ToString() + "会员审核成功!", "User.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        //取消审核
        protected void btnNoAudit_Click(object sender, EventArgs e)
        {
            string strUserID = Config.Request(Request.Form["UserID"], "0");
            if (strUserID != "0")
            {
                string[] arrUserID = strUserID.Split(new char[] { ',' });
                StringBuilder strTempUserID = new StringBuilder();
                UserModel userModel = new UserModel();
                int n = 0;
                for (int i = 0; i < arrUserID.Length; i++)
                {
                    userModel = Factory.User().GetInfo(arrUserID[i]);
                    if (userModel != null)
                    {
                        //if (GetData.CheckAdminID(userModel.AdminID, "UserAll"))//检查创建者
                        //{
                            Factory.User().UpdateAuditStatus(arrUserID[i], "0");
                            strTempUserID.Append(arrUserID[i]);
                            if (i + 1 < arrUserID.Length) strTempUserID.Append(",");
                            n++;
                       // }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("取消审核编号为" + strTempUserID.ToString() + "的会员!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempUserID.ToString() + "会员审核取消成功!", "User.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
