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

namespace HxSoft.Web.Admin.Extension
{
    public partial class Link_Add : System.Web.UI.Page
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
                return Config.RequestNumeric(Request.QueryString["LinkID"], 0).ToString();
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
            if (!Page.IsPostBack)
            {
                //站点列表
                GetData.GetConfigList(drpConfigID, "LanguageVer", "ConfigID");
                this.drpConfigID.Items.Insert(0, new ListItem("请选择", "-1"));

                if (LinkID == "0")
                {
                    GetData.LimitChkMsg("LinkAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.Link().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("LinkEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
                RequiredFieldValidator4.Enabled = false;
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            LinkModel linkModel = new LinkModel();
            string strOldListID = hidlistID.Value;
            linkModel.ConfigID = drpConfigID.SelectedValue;
            linkModel.TypeID = radTypeID.SelectedValue;
            linkModel.SiteName = txtSiteName.Text.Trim();
            linkModel.SiteUrl = txtSiteUrl.Text.Trim();
            linkModel.LogoUrl = txtLogoUrl.Text.Trim();
            linkModel.ListID = txtListID.Text.Trim();
            linkModel.AdminID = Session["AdminID"].ToString();
            linkModel.AddTime = DateTime.Now.ToString();
            linkModel.IsClose = radIsClose.SelectedValue;
            if (LinkID == "0")
            {
                Factory.Link().OrderInfo(linkModel.ListID, strOldListID);
                Factory.Link().InsertInfo(linkModel);
                Factory.AdminLog().InsertLog("添加名称为\"" + linkModel.SiteName + "\"的友情链接。", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "Link.aspx");
            }
            else
            {
                LinkModel linkModel_2 = new LinkModel();
                linkModel_2 = Factory.Link().GetInfo(LinkID);
                if (linkModel_2 != null)
                {
                    if (GetData.CheckAdminID(linkModel_2.AdminID, "LinkAll"))//检查创建者
                    {
                        Factory.Link().OrderInfo(linkModel.ListID, strOldListID);
                        Factory.Link().UpdateInfo(linkModel, LinkID);
                        Factory.AdminLog().InsertLog("修改编号为" + LinkID + "的友情链接。", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "Link.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            LinkModel linkModel = new LinkModel();
            linkModel = Factory.Link().GetInfo(LinkID);
            if (linkModel != null)
            {
                if (GetData.CheckAdminID(linkModel.AdminID, "LinkAll"))//检查创建者
                {
                    ClassModel claModel = Factory.Class().GetInfo(linkModel.ConfigID);
                    if (claModel != null)
                    {
                        Config.setDefaultSelected(drpConfigID, claModel.ConfigID);
                        Config.setDefaultSelected(radTypeID, linkModel.TypeID);
                        txtSiteName.Text = linkModel.SiteName;
                        txtSiteUrl.Text = linkModel.SiteUrl;
                        txtLogoUrl.Text = linkModel.LogoUrl;
                        txtListID.Text = linkModel.ListID;
                        hidlistID.Value = linkModel.ListID;
                        //txtAdminID.Text = linkModel.AdminID;
                        //txtAddTime.Text = linkModel.AddTime;
                        radIsClose.ClearSelection();
                        Config.setDefaultSelected(radIsClose, linkModel.IsClose);
                    }
                    else
                    {
                        Config.ShowEnd("您没有查看此信息的权限！");
                    }
                }
                else
                {
                    Config.ShowEnd("您没有查看此信息的权限！");
                }
            }
        }

        //类型选择
        protected void radTypeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            RequiredFieldValidator4.Enabled = true;
        }

    }
}
