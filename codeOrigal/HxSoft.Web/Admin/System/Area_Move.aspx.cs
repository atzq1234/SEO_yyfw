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
    public partial class Area_Move : System.Web.UI.Page
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
                return Config.Request(Request.QueryString["AreaID"], "0");
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
                GetData.LimitChkMsg("AreaMove");
                if (AreaID == "0")
                {
                    Config.ShowEnd("请选择要操作的记录!");
                }
                else
                {
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            StringBuilder strTempAreaID = new StringBuilder();
            AreaModel areaModel = new AreaModel();
            areaModel.ParentID = drpParentID.SelectedValue;
            string[] arrAreaID = hidAreaID.Value.Split(new char[] { ',' });
            int n = 0;
            for (int i = 0; i < arrAreaID.Length; i++)
            {
                AreaModel areaModel_2 = new AreaModel();
                areaModel_2 = Factory.Area().GetInfo(arrAreaID[i]);
                if (areaModel_2 != null)
                {
                    if (GetData.CheckAdminID(areaModel_2.AdminID, "AreaAll"))//检查创建者
                    {
                        //父级不一样,取新父级排序
                        if (areaModel.ParentID != areaModel_2.ParentID)
                        {
                            areaModel.ListID = Factory.Area().GetListID(areaModel.ParentID);
                        }
                        else
                        {
                            areaModel.ListID = areaModel_2.ListID;
                        }
                        Factory.Area().MoveInfo(areaModel, arrAreaID[i]);
                        Factory.Area().UpdateChildNum(areaModel.ParentID, areaModel_2.ParentID);
                        strTempAreaID.Append(arrAreaID[i]);
                        if (i + 1 < arrAreaID.Length) strTempAreaID.Append(",");
                        n++;
                    }
                }
            }
            if (n > 0)
            {
                Factory.AdminLog().InsertLog("移动编号为" + strTempAreaID.ToString() + "的地区!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("编号为" + strTempAreaID.ToString() + "地区移动成功!", "Area.aspx?ParentID=" + areaModel.ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
            else
            {
                Config.MsgGoBack("操作失败!");
            }
        }


        //显示数据
        protected void ShowInfo()
        {
            AreaModel areaModel = new AreaModel();
            string[] arrAreaID = AreaID.Split(new char[] { ','});
            for (int i = 0; i < arrAreaID.Length; i++)
            {
                areaModel = Factory.Area().GetInfo(arrAreaID[i]);
                if (areaModel != null)
                {
                    if (GetData.CheckAdminID(areaModel.AdminID, "AreaAll"))//检查创建者
                    {
                        lblAreaName.Text = lblAreaName.Text + areaModel.AreaName;
                        hidAreaID.Value=hidAreaID.Value+arrAreaID[i];
                        if (i + 1 < arrAreaID.Length)
                        {
                            lblAreaName.Text = lblAreaName.Text + "，";
                            hidAreaID.Value = hidAreaID.Value + ",";
                        }
                    }
                }
            }
            Factory.Area().ShowSelectTree("0", drpParentID, " and ParentID not in(" + AreaID + ") and AreaID not in(" + AreaID + ")");
            drpParentID.Items.Insert(0, new ListItem("根结点", "0"));
            drpParentID.Attributes.Add("size", "20");
            Config.setDefaultSelected(drpParentID, ParentID);
        }
    }
}
