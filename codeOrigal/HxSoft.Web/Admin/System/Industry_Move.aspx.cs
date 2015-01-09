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
    public partial class Industry_Move : System.Web.UI.Page
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
                return Config.Request(Request.QueryString["IndustryID"], "0");
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
                GetData.LimitChkMsg("IndustryMove");
                if (IndustryID == "0")
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
            StringBuilder strTempIndustryID = new StringBuilder();
            IndustryModel indModel = new IndustryModel();
            indModel.ParentID = drpParentID.SelectedValue;
            string[] arrIndustryID = hidIndustryID.Value.Split(new char[] { ',' });
            int n = 0;
            for (int i = 0; i < arrIndustryID.Length; i++)
            {
                IndustryModel indModel_2 = new IndustryModel();
                indModel_2 = Factory.Industry().GetInfo(arrIndustryID[i]);
                if (indModel_2 != null)
                {
                    if (GetData.CheckAdminID(indModel_2.AdminID, "IndustryAll"))//检查创建者
                    {
                        //父级不一样,取新父级排序
                        if (indModel.ParentID != indModel_2.ParentID)
                        {
                            indModel.ListID = Factory.Industry().GetListID(indModel.ParentID);
                        }
                        else
                        {
                            indModel.ListID = indModel_2.ListID;
                        }
                        Factory.Industry().MoveInfo(indModel, arrIndustryID[i]);
                        Factory.Industry().UpdateChildNum(indModel.ParentID, indModel_2.ParentID);
                        strTempIndustryID.Append(arrIndustryID[i]);
                        if (i + 1 < arrIndustryID.Length) strTempIndustryID.Append(",");
                        n++;
                    }
                }
            }
            if (n > 0)
            {
                Factory.AdminLog().InsertLog("移动编号为" + strTempIndustryID.ToString() + "的行业!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("编号为" + strTempIndustryID.ToString() + "行业移动成功!", "Industry.aspx?ParentID=" + indModel.ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
            else
            {
                Config.MsgGoBack("操作失败!");
            }
        }


        //显示数据
        protected void ShowInfo()
        {
            IndustryModel indModel = new IndustryModel();
            string[] arrIndustryID = IndustryID.Split(new char[] { ','});
            for (int i = 0; i < arrIndustryID.Length; i++)
            {
                indModel = Factory.Industry().GetInfo(arrIndustryID[i]);
                if (indModel != null)
                {
                    if (GetData.CheckAdminID(indModel.AdminID, "IndustryAll"))//检查创建者
                    {
                        lblIndustryName.Text = lblIndustryName.Text + indModel.IndustryName;
                        hidIndustryID.Value=hidIndustryID.Value+arrIndustryID[i];
                        if (i + 1 < arrIndustryID.Length)
                        {
                            lblIndustryName.Text = lblIndustryName.Text + "，";
                            hidIndustryID.Value = hidIndustryID.Value + ",";
                        }
                    }
                }
            }
            Factory.Industry().ShowSelectTree("0", drpParentID, " and ParentID not in(" + IndustryID + ") and IndustryID not in(" + IndustryID + ")");
            drpParentID.Items.Insert(0, new ListItem("根结点", "0"));
            drpParentID.Attributes.Add("size", "20");
            Config.setDefaultSelected(drpParentID, ParentID);
        }
    }
}
