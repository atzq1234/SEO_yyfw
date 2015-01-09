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

namespace HxSoft.Web.Admin.Message
{
    public partial class Guestbook_Add : System.Web.UI.Page
    {
        /// <summary>
        ///留言板
        /// 创建人:杨小明
        /// 日期:2011-9-16
        /// </summary>
        //定义全局变量
        public string GuestbookID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["GuestbookID"], 0).ToString();
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
                return Config.Request(Request["OrderKey"], "GuestbookID");
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
        public string strNickName
        {
            get
            {
                return Config.Request(Request["txtNickName"], "");
            }
        }
        public string strBookContent
        {
            get
            {
                return Config.Request(Request["txtBookContent"], "");
            }
        }
        public string strIsReply
        {
            get
            {
                return Config.Request(Request["radIsReply"], "-1");
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
                if (strNickName != "") TempSql.Append(" and NickName like @NickName");
                if (strBookContent != "") TempSql.Append(" and BookContent like @BookContent");
                if (strIsReply != "-1") TempSql.Append(" and IsReply =@IsReply");
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
                if (strNickName != "") listParams.Add(Config.Conn().CreateDbParameter("@NickName", "%" + strNickName + "%"));
                if (strBookContent != "") listParams.Add(Config.Conn().CreateDbParameter("@BookContent", "%" + strBookContent + "%"));
                if (strIsReply != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsReply", strIsReply));
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
                TempUrl.Append("txtNickName=" + Server.UrlEncode(strNickName) + "&");
                TempUrl.Append("txtBookContent=" + Server.UrlEncode(strBookContent) + "&");
                TempUrl.Append("radIsReply=" + Server.UrlEncode(strIsReply) + "&");
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
                if (GuestbookID == "0")
                {
                    GetData.LimitChkMsg("GuestbookAdd");
                    lblTitle.Text = "添加";
                }
                else
                {
                    GetData.LimitChkMsg("GuestbookReply");
                    lblTitle.Text = "回复";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            GuestbookModel gbookModel = new GuestbookModel();
            gbookModel.NickName = txtNickName.Text.Trim();
            gbookModel.BookContent = Config.HTMLCls(txtBookContent.Text.Trim());
            gbookModel.IpAddress = Request.UserHostAddress.ToString();
            gbookModel.AddTime = DateTime.Now.ToString();
            gbookModel.IsReply = "1";
            gbookModel.ReplyContent = Config.HTMLCls(txtReplyContent.Text.Trim());
            gbookModel.ReplyTime = DateTime.Now.ToString();
            gbookModel.AdminID = Session["AdminID"].ToString();
            gbookModel.IsClose = radIsClose.SelectedValue;
            gbookModel.TelePhone = txtTelePhone.Text;
            gbookModel.Email = txtEmail.Text;
            if (GuestbookID == "0")
            {
                Factory.Guestbook().InsertInfo(gbookModel);
                Factory.AdminLog().InsertLog("添加留言!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "Guestbook.aspx");
            }
            else
            {
                Factory.Guestbook().UpdateInfo(gbookModel, GuestbookID);
                Factory.AdminLog().InsertLog("修改编号为" + GuestbookID + "的留言!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("修改成功！", "Guestbook.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            GuestbookModel gbookModel = new GuestbookModel();
            gbookModel = Factory.Guestbook().GetInfo(GuestbookID);
            if (gbookModel != null)
            {
                txtNickName.Text = gbookModel.NickName;
                txtBookContent.Text = Config.HTMLToTextarea(gbookModel.BookContent);
                litIpAddress.Text = gbookModel.IpAddress;
                litAddTime.Text = gbookModel.AddTime;
                //txtIsReply.Text = gbookModel.IsReply;
                txtReplyContent.Text = Config.HTMLToTextarea(gbookModel.ReplyContent);
                litReplyTime.Text = gbookModel.ReplyTime;
                litAdminID.Text = Factory.Admin().GetValueByField("AdminName", gbookModel.AdminID);
                radIsClose.ClearSelection();
                Config.setDefaultSelected(radIsClose, gbookModel.IsClose);
                txtTelePhone.Text = gbookModel.TelePhone;
                txtEmail.Text = gbookModel.Email;
            }
            else
            {
                Config.ShowEnd("您没有查看此信息的权限！");
            }
        }

    }
}
