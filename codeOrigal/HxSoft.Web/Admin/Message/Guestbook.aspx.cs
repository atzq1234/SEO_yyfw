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

namespace HxSoft.Web.Admin.Message
{
    public partial class Guestbook : System.Web.UI.Page
    {
        /// <summary>
        ///留言板
        /// 创建人:杨小明
        /// 日期:2011-9-16
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
            GetData.LimitChkMsg("Guestbook");
            lbtnAdd.Visible = GetData.LimitChk("GuestbookAdd");
            lbtnEdit.Visible = GetData.LimitChk("GuestbookEdit");
            lbtnOpen.Visible = GetData.LimitChk("GuestbookOpen");
            lbtnClose.Visible = GetData.LimitChk("GuestbookClose");
            lbtnDel.Visible = GetData.LimitChk("GuestbookDel");
            if (!Page.IsPostBack)
            {
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Guestbook where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strGuestbookID = Config.Request(Request.Form["GuestbookID"], "0");
            if (strGuestbookID != "0")
            {
                Response.Redirect("Guestbook_Add.aspx?GuestbookID=" + strGuestbookID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strGuestbookID = Config.Request(Request.Form["GuestbookID"], "0");
            if (strGuestbookID != "0")
            {
                string[] arrGuestbookID = strGuestbookID.Split(new char[] { ',' });
                StringBuilder strTempGuestbookID = new StringBuilder();
                for (int i = 0; i < arrGuestbookID.Length; i++)
                {
                    Factory.Guestbook().DeleteInfo(arrGuestbookID[i]);
                    strTempGuestbookID.Append(arrGuestbookID[i]);
                    if (i + 1 < arrGuestbookID.Length) strTempGuestbookID.Append(",");
                }
                Factory.AdminLog().InsertLog("删除编号为" + strTempGuestbookID.ToString() + "的留言!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("删除成功!", "Guestbook.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //开放
        protected void lbtnOpen_Click(object sender, EventArgs e)
        {
            string strGuestbookID = Config.Request(Request.Form["GuestbookID"], "0");
            if (strGuestbookID != "0")
            {
                string[] arrGuestbookID = strGuestbookID.Split(new char[] { ',' });
                StringBuilder strTempGuestbookID = new StringBuilder();
                for (int i = 0; i < arrGuestbookID.Length; i++)
                {
                    Factory.Guestbook().UpdateCloseStatus(arrGuestbookID[i],"0");
                    strTempGuestbookID.Append(arrGuestbookID[i]);
                    if (i + 1 < arrGuestbookID.Length) strTempGuestbookID.Append(",");
                }
                Factory.AdminLog().InsertLog("开放编号为" + strTempGuestbookID.ToString() + "的留言!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("开放成功!", "Guestbook.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //关闭
        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            string strGuestbookID = Config.Request(Request.Form["GuestbookID"], "0");
            if (strGuestbookID != "0")
            {
                string[] arrGuestbookID = strGuestbookID.Split(new char[] { ',' });
                StringBuilder strTempGuestbookID = new StringBuilder();
                for (int i = 0; i < arrGuestbookID.Length; i++)
                {
                    Factory.Guestbook().UpdateCloseStatus(arrGuestbookID[i],"1");
                    strTempGuestbookID.Append(arrGuestbookID[i]);
                    if (i + 1 < arrGuestbookID.Length) strTempGuestbookID.Append(",");
                }
                Factory.AdminLog().InsertLog("关闭编号为" + strTempGuestbookID.ToString() + "的留言!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("关闭成功!", "Guestbook.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        //查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Repeater_Bind(repList);
        }

    }
}
