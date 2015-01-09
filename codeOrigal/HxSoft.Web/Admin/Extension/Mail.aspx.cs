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

namespace HxSoft.Web.Admin.Extension
{
    public partial class Mail : System.Web.UI.Page
    {
        /// <summary>
        ///邮件订阅
        /// 创建人:杨小明
        /// 日期:2011-4-27
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
                return Config.Request(Request["OrderKey"], "MailID");
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
        public string strMailAddress
        {
            get
            {
                return Config.Request(Request["txtMailAddress"], "");
            }
        }
        public string strIsRec
        {
            get
            {
                return Config.Request(Request["radIsRec"], "-1");
            }
        }
        #endregion
        #region ****查询语句****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (strMailAddress != "") TempSql.Append(" and MailAddress like @MailAddress");
                if (strIsRec != "-1") TempSql.Append(" and IsRec =" + strIsRec);
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
                if (strMailAddress != "") listParams.Add(Config.Conn().CreateDbParameter("@MailAddress", "%" + strMailAddress + "%"));
                if (strIsRec != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsRec", strIsRec));
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
                TempUrl.Append("txtMailAddress=" + Server.UrlEncode(strMailAddress) + "&");
                TempUrl.Append("radIsRec=" + Server.UrlEncode(strIsRec) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("Mail");
            //lbtnAdd.Visible = GetData.LimitChk("MailAdd");
            //lbtnEdit.Visible = GetData.LimitChk("MailEdit");
            lbtnDel.Visible = GetData.LimitChk("MailDel");
            lbtnExport.Visible = GetData.LimitChk("MailExport");
            if (!Page.IsPostBack)
            {
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Mail where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strMailID = Config.Request(Request.Form["MailID"], "0");
            if (strMailID != "0")
            {
                Response.Redirect("Mail_Add.aspx?MailID=" + strMailID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strMailID = Config.Request(Request.Form["MailID"], "0");
            if (strMailID != "0")
            {
                string[] arrMailID = strMailID.Split(new char[] { ',' });
                StringBuilder strTempMailID = new StringBuilder();
                for (int i = 0; i < arrMailID.Length; i++)
                {
                    Factory.Mail().DeleteInfo(arrMailID[i]);
                    strTempMailID.Append(arrMailID[i]);
                    if (i + 1 < arrMailID.Length) strTempMailID.Append(",");
                }
                Factory.AdminLog().InsertLog("删除编号为" + strTempMailID.ToString() + "的邮件地址!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("删除成功!", "Mail.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        //查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Repeater_Bind(repList);
        }

    }
}
