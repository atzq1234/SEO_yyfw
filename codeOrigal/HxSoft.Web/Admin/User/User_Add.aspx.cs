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
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
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
        #region ****�������****
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
        #region ****�������****
        public string SqlOrder
        {
            get
            {
                return " order by IsAudit asc," + strOrderKey + " " + strAscDesc1;
            }
        }
        #endregion
        #region ****Url�������****
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
        #region ****��ѯ����****
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
        #region ****��ѯ���****
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
        #region****DbParameter����****
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
        #region ****Url����****
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
        //ҳ���ʼ��
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            if (!Page.IsPostBack)
            {
                //��Ա����
                Factory.Acc().DataBind("select * from t_UserRank  order by ListID asc", null, Config.DataBindObjTypeCollection.DropDownList.ToString(), drpUserRankID, "UserRankName", "UserRankID");
                drpUserRankID.Items.Insert(0, new ListItem("��ѡ��", "-1"));

                if (UserID == "0")
                {
                    GetData.LimitChkMsg("UserAdd");
                    lblTitle.Text = "���";
                    lblPassMsg.Visible = false;
                }
                else
                {
                    GetData.LimitChkMsg("UserEdit");
                    lblTitle.Text = "�޸�";
                    RequiredFieldValidator2.Enabled = false;
                    lblPassMsg.Visible = true;
                    ShowInfo();
                }
            }
        }
        //��������
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
                    Factory.AdminLog().InsertLog("����ʺ�Ϊ\"" + userModel.UserName + "\"�Ļ�Ա��", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("��ӳɹ���", "User.aspx");
                }
                else
                {
                    errMsg.Text = "�Ѵ�����ͬ��Ա�ʺ�!";
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
                        //if (GetData.CheckAdminID(userModel_2.AdminID, "UserAll"))//��鴴����
                        //{
                        if (strUserPass != string.Empty)//�޸�����
                        {
                            userModel.UserPass = Config.md5(strUserPass);
                        }
                        else
                        {
                            userModel.UserPass = userModel_2.UserPass;
                        }
                        Factory.User().UpdateInfo(userModel, UserID);
                        Factory.AdminLog().InsertLog("�޸ı��Ϊ" + UserID + "�Ļ�Ա��", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("�޸ĳɹ���", "User.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                        //}
                    }
                }
                else
                {
                    errMsg.Text = "�Ѵ�����ͬ��Ա�ʺ�!";
                }
            }
        }
        //��ʾ����
        protected void ShowInfo()
        {
            UserModel userModel = new UserModel();
            userModel = Factory.User().GetInfo(UserID);
            if (userModel != null)
            {
                //if (GetData.CheckAdminID(userModel.AdminID, "UserAll"))//��鴴����
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
                //    Config.ShowEnd("��û�в鿴����Ϣ��Ȩ��");
                //}
            }
            else
            {
                Config.ShowEnd("��û�в鿴����Ϣ��Ȩ�ޣ�");
            }
        }

    }
}
