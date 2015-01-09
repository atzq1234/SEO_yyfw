using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using HxSoft.ClassFactory;
using HxSoft.Common;
using HxSoft.Model;

namespace HxSoft.Web.Admin.Extension
{
    public partial class AdPosition : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
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
            GetData.LimitChkMsg("AdPosition");
            lbtnAdd.Visible = GetData.LimitChk("AdPositionAdd");
            lbtnEdit.Visible = GetData.LimitChk("AdPositionEdit");
            lbtnDel.Visible = GetData.LimitChk("AdPositionDel");
            lbtnOpen.Visible = GetData.LimitChk("AdPositionOpen");
            lbtnClose.Visible = GetData.LimitChk("AdPositionClose");
            if (!Page.IsPostBack)
            {
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_AdPosition where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strAdPositionID = Config.Request(Request.Form["AdPositionID"], "0");
            if (strAdPositionID != "0")
            {
                Response.Redirect("AdPosition_Add.aspx?AdPositionID=" + strAdPositionID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strAdPositionID = Config.Request(Request.Form["AdPositionID"], "0");
            if (strAdPositionID != "0")
            {
                string[] arrAdPositionID = strAdPositionID.Split(new char[] { ',' });
                StringBuilder strTempAdPositionID = new StringBuilder();
                AdPositionModel adPosModel = new AdPositionModel();
                int n = 0;
                for (int i = 0; i < arrAdPositionID.Length; i++)
                {
                    adPosModel = Factory.AdPosition().GetInfo(arrAdPositionID[i]);
                    if (adPosModel != null)
                    {
                        if (GetData.CheckAdminID(adPosModel.AdminID, "AdPositionAll"))//检查创建者
                        {
                            Factory.AdPosition().DeleteInfo(arrAdPositionID[i]);
                            strTempAdPositionID.Append(arrAdPositionID[i]);
                            if (i + 1 < arrAdPositionID.Length) strTempAdPositionID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempAdPositionID.ToString() + "的广告位!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempAdPositionID.ToString() + "广告位删除成功!", "AdPosition.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }


        //批量开放
        protected void btnOpen_Click(object sender, EventArgs e)
        {
            string strAdPositionID = Config.Request(Request.Form["AdPositionID"], "0");
            if (strAdPositionID != "0")
            {
                string[] arrAdPositionID = strAdPositionID.Split(new char[] { ',' });
                StringBuilder strTempAdPositionID = new StringBuilder();
                AdPositionModel adPosModel = new AdPositionModel();
                int n = 0;
                for (int i = 0; i < arrAdPositionID.Length; i++)
                {
                    adPosModel = Factory.AdPosition().GetInfo(arrAdPositionID[i]);
                    if (adPosModel != null)
                    {
                        if (GetData.CheckAdminID(adPosModel.AdminID, "AdPositionAll"))//检查创建者
                        {
                            Factory.AdPosition().UpdateCloseStatus(arrAdPositionID[i], "0");
                            strTempAdPositionID.Append(arrAdPositionID[i]);
                            if (i + 1 < arrAdPositionID.Length) strTempAdPositionID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempAdPositionID.ToString() + "的广告位!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempAdPositionID.ToString() + "广告位开放成功!", "AdPosition.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        //批量关闭
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string strAdPositionID = Config.Request(Request.Form["AdPositionID"], "0");
            if (strAdPositionID != "0")
            {
                string[] arrAdPositionID = strAdPositionID.Split(new char[] { ',' });
                StringBuilder strTempAdPositionID = new StringBuilder();
                AdPositionModel adPosModel = new AdPositionModel();
                int n = 0;
                for (int i = 0; i < arrAdPositionID.Length; i++)
                {
                    adPosModel = Factory.AdPosition().GetInfo(arrAdPositionID[i]);
                    if (adPosModel != null)
                    {
                        if (GetData.CheckAdminID(adPosModel.AdminID, "AdPositionAll"))//检查创建者
                        {
                            Factory.AdPosition().UpdateCloseStatus(arrAdPositionID[i], "1");
                            strTempAdPositionID.Append(arrAdPositionID[i]);
                            if (i + 1 < arrAdPositionID.Length) strTempAdPositionID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempAdPositionID.ToString() + "的广告位!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempAdPositionID.ToString() + "广告位关闭成功!", "AdPosition.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        //查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Repeater_Bind(repList);
        }
    }
}
