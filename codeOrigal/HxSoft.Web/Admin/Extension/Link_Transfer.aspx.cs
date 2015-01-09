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

namespace HxSoft.Web.Admin.Link
{
    public partial class Link_Transfer : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string LinkID
        {
            get
            {
                return Config.Request(Request.QueryString["LinkID"], "0");
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
        public string strTypeID
        {
            get
            {
                return Config.Request(Request["drpTypeID"], "-1");
            }
        }
        public string strSiteName
        {
            get
            {
                return Config.Request(Request["txtSiteName"], "");
            }
        }
        public string strConfigID
        {
            get
            {
                return Config.Request(Request["drpConfigID"], "-1");
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
                if (strConfigID != "-1") TempSql.Append(" and ConfigID=@ConfigID");
                if (strTypeID != "-1") TempSql.Append(" and TypeID =@TypeID");
                if (strSiteName != "") TempSql.Append(" and SiteName like @SiteName");
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
                if (strConfigID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@ConfigID", strConfigID));
                if (strTypeID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@TypeID", strTypeID));
                if (strSiteName != "") listParams.Add(Config.Conn().CreateDbParameter("@SiteName", "%" + strSiteName + "%"));
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
                TempUrl.Append("drpConfigID=" + Server.UrlEncode(strConfigID) + "&");
                TempUrl.Append("drpTypeID=" + Server.UrlEncode(strTypeID) + "&");
                TempUrl.Append("txtSiteName=" + Server.UrlEncode(strSiteName) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("LinkTransfer");
            if (!Page.IsPostBack)
            {
                //站点列表
                GetData.GetConfigList(drpConfigID, "LanguageVer", "ConfigID");
                this.drpConfigID.Items.Insert(0, new ListItem("请选择", "-1"));
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strConfigID = drpConfigID.SelectedValue;
            string[] arrLinkID = LinkID.Split(new char[] { ',' });
            StringBuilder strTempLinkID = new StringBuilder();
            LinkModel linkModel = new LinkModel();
            int n = 0;
            for (int i = 0; i < arrLinkID.Length; i++)
            {
                linkModel = Factory.Link().GetInfo(arrLinkID[i]);
                if (linkModel != null)
                {
                    if (GetData.CheckAdminID(linkModel.AdminID, "LinkAll"))//检查创建者
                    {
                        Factory.Link().TransferInfo(arrLinkID[i], strConfigID);
                        strTempLinkID.Append(arrLinkID[i]);
                        if (i + 1 < arrLinkID.Length) strTempLinkID.Append(",");
                        n++;
                    }
                }
            }
            if (n > 0)
            {
                Factory.AdminLog().InsertLog("转移编号为" + strTempLinkID.ToString() + "的友情链接!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("编号为" + strTempLinkID.ToString() + "友情链接转移成功!", "Link.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
            else
            {
                Config.MsgGoBack("操作失败!");
            }
        }
    }
}
