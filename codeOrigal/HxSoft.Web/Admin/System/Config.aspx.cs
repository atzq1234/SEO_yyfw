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
    public partial class _Config : System.Web.UI.Page
    {
        /// <summary>
        ///系统配置
        /// 创建人:杨小明
        /// 日期:2011-8-25
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
        public string strWebsiteName
        {
            get
            {
                return Config.Request(Request["txtWebsiteName"], "");
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
                if (strWebsiteName != "") TempSql.Append(" and WebsiteName like @WebsiteName");
                if (strIsClose != "-1") TempSql.Append(" and IsClose = @IsClose");
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
                if (strWebsiteName != "") listParams.Add(Config.Conn().CreateDbParameter("@WebsiteName", "%" + strWebsiteName + "%"));
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
                TempUrl.Append("txtWebsiteName=" + Server.UrlEncode(strWebsiteName) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("Config");
            lbtnAdd.Visible = GetData.LimitChk("ConfigAdd");
            lbtnEdit.Visible = GetData.LimitChk("ConfigEdit");
            lbtnDel.Visible = GetData.LimitChk("ConfigDel");
            if (!Page.IsPostBack)
            {
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Config where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, null, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strConfigID = Config.Request(Request.Form["ConfigID"], "0");
            if (strConfigID != "0")
            {
                Response.Redirect("Config_Add.aspx?ConfigID=" + strConfigID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strConfigID = Config.Request(Request.Form["ConfigID"], "0");
            if (strConfigID != "0")
            {
                string[] arrConfigID = strConfigID.Split(new char[] { ',' });
                StringBuilder strTempConfigID = new StringBuilder();
                ConfigModel confModel = new ConfigModel();
                int n = 0;
                for (int i = 0; i < arrConfigID.Length; i++)
                {
                    confModel = Factory.Config().GetInfo(arrConfigID[i]);
                    if (confModel != null)
                    {
                        if (GetData.CheckAdminID(confModel.AdminID, "ConfigAll"))//检查创建者
                        {
                            Factory.Config().DeleteInfo(arrConfigID[i]);
                            strTempConfigID.Append(arrConfigID[i]);
                            if (i + 1 < arrConfigID.Length) strTempConfigID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempConfigID.ToString() + "的网站配置!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempConfigID.ToString() + "网站配置删除成功!", "Config.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
