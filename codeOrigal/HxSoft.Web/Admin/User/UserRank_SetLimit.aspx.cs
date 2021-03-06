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

namespace HxSoft.Web.Admin.User
{
    public partial class UserRank_SetLimit : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string UserRankID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["UserRankID"], 0).ToString();
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
        public string strUserRankName
        {
            get
            {
                return Config.Request(Request["txtUserRankName"], "");
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
                if (strUserRankName != "") TempSql.Append(" and UserRankName like @UserRankName");
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
                if (strUserRankName != "") listParams.Add(Config.Conn().CreateDbParameter("@UserRankName", "%" + strUserRankName + "%"));
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
                TempUrl.Append("txtUserRankName=" + Server.UrlEncode(strUserRankName) + "&");
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
                GetData.LimitChkMsg("UserRankSetLimit");
                if (UserRankID == "0")
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
            UserRankModel userRankModel = new UserRankModel();
            //***
            if (Request.Form["LimitValue"] == null)
            {
                userRankModel.LimitValues = "-1,-1";
            }
            else
            {
                string strLimitValue = Config.HTMLClear(Request.Form["LimitValue"].ToString());
                if (strLimitValue == string.Empty)
                {
                    userRankModel.LimitValues = "-1,-1";
                }
                else
                {
                    userRankModel.LimitValues = "-1," + strLimitValue + ",-1";
                }
            }
            //***

            UserRankModel userRankModel_2 = new UserRankModel();
            userRankModel_2 = Factory.UserRank().GetInfo(UserRankID);
            if (userRankModel_2 != null)
            {
                if (GetData.CheckAdminID(userRankModel_2.AdminID, "UserRankAll"))//检查创建者
                {
                    Factory.UserRank().SetLimit(userRankModel, UserRankID);
                    Factory.AdminLog().InsertLog("设置会员级别编号为" + UserRankID + "的操作权限。", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("分配成功！", "UserRank.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            //会员级别
            UserRankModel userRankModel = new UserRankModel();
            userRankModel = Factory.UserRank().GetInfo(UserRankID);
            if (userRankModel != null)
            {
                if (GetData.CheckAdminID(userRankModel.AdminID, "UserRankAll"))//检查创建者
                {
                    lblUserRankName.Text = userRankModel.UserRankName;
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

            //操作权限列表
            DataList1.DataKeyField = "LimitID";
            Factory.Acc().DataBind("select * from t_Limit where ParentID=" + Config.SysUserLimitMouldID + " and IsClose=0 order by ListID asc", null,Config.DataBindObjTypeCollection.DataList.ToString(), DataList1);
        }

        //子权限列表
        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            string strParentID = DataList1.DataKeys[e.Item.ItemIndex].ToString();
            DataList DataList2 = (DataList)e.Item.FindControl("DataList2");
            Factory.Acc().DataBind("select * from t_Limit where ParentID=" + strParentID + " and IsClose=0 order by ListID asc", null,Config.DataBindObjTypeCollection.DataList.ToString(), DataList2);
        }
    }
}
