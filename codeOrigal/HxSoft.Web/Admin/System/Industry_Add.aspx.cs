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

namespace HxSoft.Web.Admin._System
{
    public partial class Industry_Add : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string IndustryID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["IndustryID"], 0).ToString();
            }
        }
        public string ParentID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ParentID"], 0).ToString();
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
        public string strIndustryName
        {
            get
            {
                return Config.Request(Request["txtIndustryName"], "");
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
                if (strIndustryName != "") TempSql.Append(" and IndustryName like @IndustryName");
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
                if (strIndustryName != "") listParams.Add(Config.Conn().CreateDbParameter("@IndustryName", "%" + strIndustryName + "%"));
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
                TempUrl.Append("txtIndustryName=" + Server.UrlEncode(strIndustryName) + "&");
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
                if (IndustryID == "0")
                {
                    GetData.LimitChkMsg("IndustryAdd");
                    lblTitle.Text = "添加";
                    lblParent.Text = Factory.Industry().ShowPath(ParentID).ToString();
                    string strListID = Factory.Industry().GetListID(ParentID);
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("IndustryEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            IndustryModel indModel = new IndustryModel();
            string strOldListID = hidlistID.Value;
            indModel.IndustryName = txtIndustryName.Text.Trim();
            indModel.ParentID = ParentID;
            indModel.ChildNum = "0";
            indModel.ListID = txtListID.Text.Trim();
            indModel.AdminID = Session["AdminID"].ToString();
            indModel.AddTime = DateTime.Now.ToString();
            indModel.IsClose = radIsClose.SelectedValue;
            if (IndustryID == "0")
            {
                if (!Factory.Industry().CheckInfo("IndustryName", indModel.IndustryName, indModel.ParentID))
                {
                    Factory.Industry().OrderInfo(indModel.ParentID, indModel.ListID, strOldListID);
                    Factory.Industry().InsertInfo(indModel);
                    Factory.AdminLog().InsertLog("添加名称为\"" + indModel.IndustryName + "\"的行业。", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("添加成功！", "Industry.aspx?ParentID=" + indModel.ParentID);
                }
                else
                {
                    errMsg.Text = "已存在相同行业!";
                }
            }
            else
            {
                IndustryModel indModel_2 = new IndustryModel();
                indModel_2 = Factory.Industry().GetInfo(IndustryID);
                if (indModel_2 != null)
                {
                    if (GetData.CheckAdminID(indModel_2.AdminID, "IndustryAll"))//检查创建者
                    {
                        if (!Factory.Industry().CheckInfo("IndustryName", indModel.IndustryName, indModel.ParentID, IndustryID))
                        {
                            Factory.Industry().OrderInfo(indModel.ParentID, indModel.ListID, strOldListID);
                            Factory.Industry().UpdateInfo(indModel, IndustryID);
                            Factory.AdminLog().InsertLog("修改编号为" + IndustryID + "的行业。", Session["AdminID"].ToString());
                            Config.MsgGotoUrl("修改成功！", "Industry.aspx?ParentID=" + indModel.ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page);
                        }
                        else
                        {
                            errMsg.Text = "已存在相同行业!";
                        }
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            IndustryModel indModel = new IndustryModel();
            indModel = Factory.Industry().GetInfo(IndustryID);
            if (indModel != null)
            {
                if (GetData.CheckAdminID(indModel.AdminID, "IndustryAll"))//检查创建者
                {
                    txtIndustryName.Text = indModel.IndustryName;
                    lblParent.Text = Factory.Industry().ShowPath(indModel.ParentID).ToString();
                    //txtChildNum.Text = indModel.ChildNum;
                    txtListID.Text = indModel.ListID;
                    hidlistID.Value = indModel.ListID;
                    //txtAddAdminID.Text = indModel.AddAdminID;
                    //txtAddTime.Text = indModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, indModel.IsClose);
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