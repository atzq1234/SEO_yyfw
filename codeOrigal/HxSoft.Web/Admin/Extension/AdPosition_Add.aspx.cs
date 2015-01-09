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
    public partial class AdPosition_Add : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string AdPositionID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["AdPositionID"], 0).ToString();
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
        public string strAdPositionName
        {
            get
            {
                return Config.Request(Request["txtAdPositionName"], "");
            }
        }
        public string strTypeID
        {
            get
            {
                return Config.Request(Request["drpTypeID"], "-1");
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
                if (strAdPositionName != "") TempSql.Append(" and AdPositionName like @AdPositionName");
                if (strTypeID != "-1") TempSql.Append(" and TypeID = @TypeID");
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
                if (strAdPositionName != "") listParams.Add(Config.Conn().CreateDbParameter("@AdPositionName", "%" + strAdPositionName + "%"));
                if (strTypeID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@TypeID", strTypeID));
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
                TempUrl.Append("txtAdPositionName=" + Server.UrlEncode(strAdPositionName) + "&");
                TempUrl.Append("drpTypeID=" + Server.UrlEncode(strTypeID) + "&");
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
                if (AdPositionID == "0")
                {
                    GetData.LimitChkMsg("AdPositionAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.AdPosition().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("AdPositionEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdPositionModel adPosModel = new AdPositionModel();
            string strOldListID = hidlistID.Value;
            adPosModel.AdPositionName = txtAdPositionName.Text.Trim();
            adPosModel.AdPositionIntro = Config.HTMLCls(txtAdPositionIntro.Text.Trim());
            adPosModel.TypeID = drpTypeID.SelectedValue;
            adPosModel.Width = txtWidth.Text.Trim();
            adPosModel.Height = txtHeight.Text.Trim();
            adPosModel.Price = txtAdPrice.Text.Trim();
            adPosModel.ListID = txtListID.Text.Trim();
            adPosModel.AdminID = Session["AdminID"].ToString();
            adPosModel.AddTime = DateTime.Now.ToString();
            adPosModel.IsClose = radIsClose.SelectedValue;
            if (AdPositionID == "0")
            {
                Factory.AdPosition().OrderInfo(adPosModel.ListID, strOldListID);
                Factory.AdPosition().InsertInfo(adPosModel);
                Factory.AdminLog().InsertLog("添加名称为\"" + adPosModel.AdPositionName + "\"的广告位。", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "AdPosition.aspx");
            }
            else
            {
                AdPositionModel adPosModel_2 = new AdPositionModel();
                adPosModel_2 = Factory.AdPosition().GetInfo(AdPositionID);
                if (adPosModel_2 != null)
                {
                    if (GetData.CheckAdminID(adPosModel_2.AdminID, "AdPositionAll"))//检查创建者
                    {
                        Factory.AdPosition().OrderInfo(adPosModel.ListID, strOldListID);
                        Factory.AdPosition().UpdateInfo(adPosModel, AdPositionID);
                        Factory.AdminLog().InsertLog("修改编号为" + AdPositionID + "的广告位。", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "AdPosition.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            AdPositionModel adPosModel = new AdPositionModel();
            adPosModel = Factory.AdPosition().GetInfo(AdPositionID);
            if (adPosModel != null)
            {
                if (GetData.CheckAdminID(adPosModel.AdminID, "AdPositionAll"))//检查创建者
                {
                    txtAdPositionName.Text = adPosModel.AdPositionName;
                    txtAdPositionIntro.Text = Config.HTMLToTextarea(adPosModel.AdPositionIntro);
                    Config.setDefaultSelected(drpTypeID, adPosModel.TypeID);
                    txtWidth.Text = adPosModel.Width;
                    txtHeight.Text = adPosModel.Height;
                    txtAdPrice.Text = adPosModel.Price;
                    txtListID.Text = adPosModel.ListID;
                    hidlistID.Value = adPosModel.ListID;
                    //txtAdminID.Text = adPosModel.AdminID;
                    //txtAddTime.Text = adPosModel.AddTime;

                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, adPosModel.IsClose);
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
