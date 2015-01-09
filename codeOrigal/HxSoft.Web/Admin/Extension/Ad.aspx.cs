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
    public partial class Ad : System.Web.UI.Page
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
            GetData.LimitChkMsg("Ad");
            lbtnAdd.Visible = GetData.LimitChk("AdAdd");
            lbtnEdit.Visible = GetData.LimitChk("AdEdit");
            lbtnDel.Visible = GetData.LimitChk("AdDel");
            lbtnOpen.Visible = GetData.LimitChk("AdOpen");
            lbtnClose.Visible = GetData.LimitChk("AdClose");
            if (!Page.IsPostBack)
            {
                //广告位
                Factory.Acc().DataBind("select * from t_AdPosition order by ListID asc", null,Config.DataBindObjTypeCollection.DropDownList.ToString(), drpAdPositionID, "AdPositionName", "AdPositionID");
                drpAdPositionID.Items.Insert(0,new ListItem("不限","-1"));

                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql_f_1 = "(select TypeID from t_AdPosition as b where b.AdPositionID=a.AdPositionID ) as AdPositionTypeID";
            string sql = "select *," + sql_f_1 + " from t_Ad as a where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strAdID = Config.Request(Request.Form["AdID"], "0");
            if (strAdID != "0")
            {
                Response.Redirect("Ad_Add.aspx?AdID=" + strAdID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strAdID = Config.Request(Request.Form["AdID"], "0");
            if (strAdID != "0")
            {
                string[] arrAdID = strAdID.Split(new char[] { ',' });
                StringBuilder strTempAdID = new StringBuilder();
                AdModel adModel = new AdModel();
                int n = 0;
                for (int i = 0; i < arrAdID.Length; i++)
                {
                    adModel = Factory.Ad().GetInfo(arrAdID[i]);
                    if (adModel != null)
                    {
                        if (GetData.CheckAdminID(adModel.AdminID, "AdAll"))//检查创建者
                        {
                            Factory.Ad().DeleteInfo(arrAdID[i]);
                            strTempAdID.Append(arrAdID[i]);
                            if (i + 1 < arrAdID.Length) strTempAdID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempAdID.ToString() + "的广告!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempAdID.ToString() + "广告删除成功!", "Ad.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strAdID = Config.Request(Request.Form["AdID"], "0");
            if (strAdID != "0")
            {
                string[] arrAdID = strAdID.Split(new char[] { ',' });
                StringBuilder strTempAdID = new StringBuilder();
                AdModel adModel = new AdModel();
                int n = 0;
                for (int i = 0; i < arrAdID.Length; i++)
                {
                    adModel = Factory.Ad().GetInfo(arrAdID[i]);
                    if (adModel != null)
                    {
                        if (GetData.CheckAdminID(adModel.AdminID, "AdAll"))//检查创建者
                        {
                            Factory.Ad().UpdateCloseStatus(arrAdID[i], "0");
                            strTempAdID.Append(arrAdID[i]);
                            if (i + 1 < arrAdID.Length) strTempAdID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempAdID.ToString() + "的广告!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempAdID.ToString() + "广告开放成功!", "Ad.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strAdID = Config.Request(Request.Form["AdID"], "0");
            if (strAdID != "0")
            {
                string[] arrAdID = strAdID.Split(new char[] { ',' });
                StringBuilder strTempAdID = new StringBuilder();
                AdModel adModel = new AdModel();
                int n = 0;
                for (int i = 0; i < arrAdID.Length; i++)
                {
                    adModel = Factory.Ad().GetInfo(arrAdID[i]);
                    if (adModel != null)
                    {
                        if (GetData.CheckAdminID(adModel.AdminID, "AdAll"))//检查创建者
                        {
                            Factory.Ad().UpdateCloseStatus(arrAdID[i], "1");
                            strTempAdID.Append(arrAdID[i]);
                            if (i + 1 < arrAdID.Length) strTempAdID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempAdID.ToString() + "的广告!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempAdID.ToString() + "广告关闭成功!", "Ad.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
