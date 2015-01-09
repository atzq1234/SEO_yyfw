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
    public partial class Limit_Add : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string LimitID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["LimitID"], 0).ToString();
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
        public string strLimitField
        {
            get
            {
                return Config.Request(Request["txtLimitField"], "");
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
                if (strLimitField != "") TempSql.Append(" and LimitField like @LimitField");
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
                if (strLimitField != "") listParams.Add(Config.Conn().CreateDbParameter("@LimitField", "%" + strLimitField + "%"));
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
                TempUrl.Append("txtLimitField=" + Server.UrlEncode(strLimitField) + "&");
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
                if (LimitID == "0")
                {
                    GetData.LimitChkMsg("LimitAdd");
                    lblTitle.Text = "添加";
                    lblParent.Text = Factory.Limit().ShowPath(ParentID).ToString();
                    string strListID = Factory.Limit().GetListID(ParentID);
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("LimitEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            LimitModel limModel = new LimitModel();
            string strOldListID = hidlistID.Value;
            limModel.LimitField = txtLimitField.Text.Trim();
            limModel.LimitValue = txtLimitValue.Text.Trim();
            limModel.ParentID = ParentID;
            limModel.ChildNum = "0";
            if (chkIsDist.Checked)
            {
                limModel.IsDist = "1";
            }
            else
            {
                limModel.IsDist = "0";
            }
            limModel.ListID = txtListID.Text.Trim();
            limModel.AdminID = Session["AdminID"].ToString();
            limModel.AddTime = DateTime.Now.ToString();
            limModel.IsClose = radIsClose.SelectedValue;
            if (LimitID == "0")
            {
                if (!Factory.Limit().CheckInfo("LimitField", limModel.LimitValue))
                {
                    Factory.Limit().OrderInfo(limModel.ParentID, limModel.ListID, strOldListID);
                    Factory.Limit().InsertInfo(limModel);
                    Factory.AdminLog().InsertLog("添加名称为\"" + limModel.LimitField + "\"的权限字段。", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("添加成功！", "Limit.aspx?ParentID=" + limModel.ParentID);
                }
                else
                {
                    errMsg.Text = "已存在相同权限值!";
                }
            }
            else
            {
                LimitModel limModel_2 = new LimitModel();
                limModel_2 = Factory.Limit().GetInfo(LimitID);
                if (limModel_2 != null)
                {
                    if (GetData.CheckAdminID(limModel.AdminID, "LimitAll"))//检查创建者
                    {
                        if (!Factory.Limit().CheckInfo("LimitField", limModel.LimitValue, LimitID))
                        {
                            Factory.Limit().OrderInfo(limModel.ParentID, limModel.ListID, strOldListID);
                            Factory.Limit().UpdateInfo(limModel, LimitID);
                            Factory.AdminLog().InsertLog("修改编号为" + LimitID + "的权限字段。", Session["AdminID"].ToString());
                            Config.MsgGotoUrl("修改成功！", "Limit.aspx?ParentID=" + limModel.ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page);
                        }
                        else
                        {
                            errMsg.Text = "已存在相同权限值!";
                        }
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            LimitModel limModel = new LimitModel();
            limModel = Factory.Limit().GetInfo(LimitID);
            if (limModel != null)
            {
                if (GetData.CheckAdminID(limModel.AdminID, "LimitAll"))//检查创建者
                {
                    txtLimitField.Text = limModel.LimitField;
                    txtLimitValue.Text = limModel.LimitValue;
                    lblParent.Text = Factory.Limit().ShowPath(limModel.ParentID).ToString();
                    //txtChildNum.Text = limModel.ChildNum;
                    if (limModel.IsDist == "1")
                    {
                        chkIsDist.Checked = true;
                    }
                    txtListID.Text = limModel.ListID;
                    hidlistID.Value = limModel.ListID;
                    //txtAddAdminID.Text = limModel.AddAdminID;
                    //txtAddTime.Text = limModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, limModel.IsClose);
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
