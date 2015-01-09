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
    public partial class Ad_Add : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string AdID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["AdID"], 0).ToString();
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
        public string strAdName
        {
            get
            {
                return Config.Request(Request["txtAdName"], "");
            }
        }
        public string strAdPositionID
        {
            get
            {
                return Config.Request(Request["drpAdPositionID"], "-1");
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
                if (strAdName != "") TempSql.Append(" and AdName like @AdName");
                if (strAdPositionID != "-1") TempSql.Append(" and AdPositionID = @AdPositionID");
                if (strIsClose != "-1") TempSql.Append(" and IsClose =" + strIsClose);
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
                if (strAdName != "") listParams.Add(Config.Conn().CreateDbParameter("@AdName", "%" + strAdName + "%"));
                if (strAdPositionID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@AdPositionID", strAdPositionID));
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
                TempUrl.Append("txtAdName=" + Server.UrlEncode(strAdName) + "&");
                TempUrl.Append("drpAdPositionID=" + Server.UrlEncode(strAdPositionID) + "&");
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
                //广告位
                Factory.Acc().DataBind("select * from t_AdPosition order by ListID asc",  null,Config.DataBindObjTypeCollection.DropDownList.ToString(), drpAdPositionID, "AdPositionName", "AdPositionID");
                drpAdPositionID.Items.Insert(0, new ListItem("请选择", "-1"));
                
                if (AdID == "0")
                {
                    GetData.LimitChkMsg("AdAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.Ad().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("AdEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdModel adModel = new AdModel();
            string strOldListID = hidlistID.Value;
            adModel.AdName = txtAdName.Text.Trim();
            adModel.AdIntro = Config.HTMLCls(txtAdIntro.Text.Trim());
            adModel.AdPositionID = drpAdPositionID.SelectedValue;
            adModel.AdSmallPic = txtAdSmallPic.Text.Trim();
            adModel.AdPath = txtAdPath.Text.Trim();
            adModel.AdLink = txtAdLink.Text.Trim();
            adModel.ClickNum = txtClickNum.Text.Trim();
            adModel.ListID = txtListID.Text.Trim();
            adModel.AdminID = Session["AdminID"].ToString();
            adModel.AddTime = DateTime.Now.ToString();
            adModel.IsClose = radIsClose.SelectedValue;
            if (AdID == "0")
            {
                Factory.Ad().OrderInfo(adModel.ListID, strOldListID);
                Factory.Ad().InsertInfo(adModel);
                Factory.AdminLog().InsertLog("添加名称为\"" + adModel.AdName + "\"的广告。", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "Ad.aspx");
            }
            else
            {
                AdModel adModel_2 = new AdModel();
                adModel_2 = Factory.Ad().GetInfo(AdID);
                if (adModel_2 != null)
                {
                    if (GetData.CheckAdminID(adModel_2.AdminID, "AdAll"))//检查创建者
                    {
                        Factory.Ad().OrderInfo(adModel.ListID, strOldListID);
                        Factory.Ad().UpdateInfo(adModel, AdID);
                        Factory.AdminLog().InsertLog("修改编号为" + AdID + "的广告。", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "Ad.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            AdModel adModel = new AdModel();
            adModel = Factory.Ad().GetInfo(AdID);
            if (adModel != null)
            {
                if (GetData.CheckAdminID(adModel.AdminID, "AdAll"))//检查创建者
                {
                    txtAdName.Text = adModel.AdName;
                    txtAdIntro.Text = Config.HTMLToTextarea(adModel.AdIntro);
                    Config.setDefaultSelected(drpAdPositionID, adModel.AdPositionID);
                    txtAdSmallPic.Text = adModel.AdSmallPic;
                    txtAdPath.Text = adModel.AdPath;
                    txtAdLink.Text = adModel.AdLink;
                    txtClickNum.Text = adModel.ClickNum;
                    txtListID.Text = adModel.ListID;
                    hidlistID.Value = adModel.ListID;
                    //txtAdminID.Text = adModel.AdminID;
                    //txtAddTime.Text = adModel.AddTime;

                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, adModel.IsClose);
                }
                else
                {
                    Config.ShowEnd("您没有查看此信息的权限"); 
                }
            }
            else
            {
                Config.ShowEnd("您没有查看此信息的权限！");
            }
        }
    }
}
