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

namespace HxSoft.Web.Admin.User
{
    public partial class User_Add : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string UserID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["UserID"], 0).ToString();
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
            if (!Page.IsPostBack)
            {
                //会员级别
                Factory.Acc().DataBind("select * from t_UserRank  order by ListID asc", null, Config.DataBindObjTypeCollection.DropDownList.ToString(), drpUserRankID, "UserRankName", "UserRankID");
                drpUserRankID.Items.Insert(0, new ListItem("请选择", "-1"));

                if (UserID == "0")
                {
                    GetData.LimitChkMsg("UserAdd");
                    lblTitle.Text = "添加";
                    lblPassMsg.Visible = false;
                }
                else
                {
                    GetData.LimitChkMsg("UserEdit");
                    lblTitle.Text = "修改";
                    RequiredFieldValidator2.Enabled = false;
                    lblPassMsg.Visible = true;
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserModel userModel = new UserModel();
            userModel.UserName = txtUserName.Text.Trim();
            string strUserPass = txtUserPass.Text.Trim();
            userModel.PassQuestion = txtPassQuestion.Text.Trim();
            userModel.PassAnswer = txtPassAnswer.Text.Trim();
            userModel.RealName = txtRealName.Text.Trim();
            userModel.Sex = radSex.SelectedValue;
            userModel.Email = txtEmail.Text.Trim();
            userModel.Mobile = txtMobile.Text.Trim();
            userModel.Address = txtAddress.Text.Trim();
            userModel.Company = txtCompany.Text.Trim();
            userModel.Comment = Config.HTMLCls(txtComment.Text.Trim());
            userModel.UserRankID = drpUserRankID.SelectedValue;
            userModel.IsAudit = radIsAudit.SelectedValue;
            userModel.Point = txtPoint.Text.Trim();
            userModel.LoginNum = "0";
            userModel.LastLoginTime = "1900-1-1";
            userModel.ThisLoginTime = "1900-1-1";
            userModel.AddTime = DateTime.Now.ToString();
            userModel.IsClose = radIsClose.SelectedValue;
            if (UserID == "0")
            {
                if (!Factory.User().CheckInfo("UserName", userModel.UserName))
                {
                    userModel.UserPass = Config.md5(strUserPass);
                    Factory.User().InsertInfo(userModel);
                    Factory.AdminLog().InsertLog("添加帐号为\"" + userModel.UserName + "\"的会员。", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("添加成功！", "User.aspx");
                }
                else
                {
                    errMsg.Text = "已存在相同会员帐号!";
                }
            }
            else
            {
                if (!Factory.User().CheckInfo("UserName", userModel.UserName, UserID))
                {
                    UserModel userModel_2 = new UserModel();
                    userModel_2 = Factory.User().GetInfo(UserID);
                    if (userModel_2 != null)
                    {
                        //if (GetData.CheckAdminID(userModel_2.AdminID, "UserAll"))//检查创建者
                        //{
                        if (strUserPass != string.Empty)//修改密码
                        {
                            userModel.UserPass = Config.md5(strUserPass);
                        }
                        else
                        {
                            userModel.UserPass = userModel_2.UserPass;
                        }
                        Factory.User().UpdateInfo(userModel, UserID);
                        Factory.AdminLog().InsertLog("修改编号为" + UserID + "的会员。", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "User.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                        //}
                    }
                }
                else
                {
                    errMsg.Text = "已存在相同会员帐号!";
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            UserModel userModel = new UserModel();
            userModel = Factory.User().GetInfo(UserID);
            if (userModel != null)
            {
                //if (GetData.CheckAdminID(userModel.AdminID, "UserAll"))//检查创建者
                //{
                txtUserName.Text = userModel.UserName;
                //txtUserPass.Text = userModel.UserPass;
                txtPassQuestion.Text = userModel.PassQuestion;
                txtPassAnswer.Text = userModel.PassAnswer;
                txtRealName.Text = userModel.RealName;
                radSex.ClearSelection();
                Config.setDefaultSelected(radSex, userModel.Sex);
                txtEmail.Text = userModel.Email;
                txtMobile.Text = userModel.Mobile;
                txtAddress.Text = userModel.Address;
                txtCompany.Text = userModel.Company;
                txtComment.Text = Config.HTMLToTextarea(userModel.Comment);
                Config.setDefaultSelected(drpUserRankID, userModel.UserRankID);
                radIsAudit.ClearSelection();
                Config.setDefaultSelected(radIsAudit, userModel.IsAudit);
                txtPoint.Text = userModel.Point;
                //txtLoginNum.Text = userModel.LoginNum;
                //txtLastLoginTime.Text = userModel.LastLoginTime;
                //txtThisLoginTime.Text = userModel.ThisLoginTime;
                //txtAddTime.Text = userModel.AddTime;
                radIsClose.ClearSelection();
                Config.setDefaultSelected(radIsClose, userModel.IsClose);
                //}
                //else
                //{
                //    Config.ShowEnd("您没有查看此信息的权限");
                //}
            }
            else
            {
                Config.ShowEnd("您没有查看此信息的权限！");
            }
        }

    }
}
