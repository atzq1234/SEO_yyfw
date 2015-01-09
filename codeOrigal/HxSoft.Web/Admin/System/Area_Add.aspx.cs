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
    public partial class Area_Add : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string AreaID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["AreaID"], 0).ToString();
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
        public string strAreaName
        {
            get
            {
                return Config.Request(Request["txtAreaName"], "");
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
                if (strAreaName != "") TempSql.Append(" and AreaName like @AreaName");
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
                if (strAreaName != "") listParams.Add(Config.Conn().CreateDbParameter("@AreaName", "%" + strAreaName + "%"));
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
                TempUrl.Append("txtAreaName=" + Server.UrlEncode(strAreaName) + "&");
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
                if (AreaID == "0")
                {
                    GetData.LimitChkMsg("AreaAdd");
                    lblTitle.Text = "添加";
                    lblParent.Text = Factory.Area().ShowPath(ParentID).ToString();
                    string strListID = Factory.Area().GetListID(ParentID);
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("AreaEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AreaModel areaModel = new AreaModel();
            string strOldListID = hidlistID.Value;
            areaModel.AreaName = txtAreaName.Text.Trim();
            areaModel.ParentID = ParentID;
            areaModel.ChildNum = "0";
            areaModel.ListID = txtListID.Text.Trim();
            areaModel.AdminID = Session["AdminID"].ToString();
            areaModel.AddTime = DateTime.Now.ToString();
            areaModel.IsClose = radIsClose.SelectedValue;
            if (AreaID == "0")
            {
                if (!Factory.Area().CheckInfo("AreaName", areaModel.AreaName, areaModel.ParentID))
                {
                    Factory.Area().OrderInfo(areaModel.ParentID, areaModel.ListID, strOldListID);
                    Factory.Area().InsertInfo(areaModel);
                    Factory.AdminLog().InsertLog("添加名称为\"" + areaModel.AreaName + "\"的地区。", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("添加成功！", "Area.aspx?ParentID=" + areaModel.ParentID);
                }
                else
                {
                    errMsg.Text = "已存在相同地区!";
                }
            }
            else
            {
                AreaModel areaModel_2 = new AreaModel();
                areaModel_2 = Factory.Area().GetInfo(AreaID);
                if (areaModel_2 != null)
                {
                    if (GetData.CheckAdminID(areaModel_2.AdminID, "AreaAll"))//检查创建者
                    {
                        if (!Factory.Area().CheckInfo("AreaName", areaModel.AreaName, areaModel.ParentID, AreaID))
                        {
                            Factory.Area().OrderInfo(areaModel.ParentID, areaModel.ListID, strOldListID);
                            Factory.Area().UpdateInfo(areaModel, AreaID);
                            Factory.AdminLog().InsertLog("修改编号为" + AreaID + "的地区。", Session["AdminID"].ToString());
                            Config.MsgGotoUrl("修改成功！", "Area.aspx?ParentID=" + areaModel.ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page);
                        }
                        else
                        {
                            errMsg.Text = "已存在相同地区!";
                        }
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            AreaModel areaModel = new AreaModel();
            areaModel = Factory.Area().GetInfo(AreaID);
            if (areaModel != null)
            {
                if (GetData.CheckAdminID(areaModel.AdminID, "AreaAll"))//检查创建者
                {
                    txtAreaName.Text = areaModel.AreaName;
                    lblParent.Text = Factory.Area().ShowPath(areaModel.ParentID).ToString();
                    //txtChildNum.Text = areaModel.ChildNum;
                    txtListID.Text = areaModel.ListID;
                    hidlistID.Value = areaModel.ListID;
                    //txtAddAdminID.Text = areaModel.AddAdminID;
                    //txtAddTime.Text = areaModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, areaModel.IsClose);
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
