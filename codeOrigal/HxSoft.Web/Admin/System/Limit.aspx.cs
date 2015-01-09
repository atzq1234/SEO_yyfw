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
using System.Data.Common;
using System.Collections.Generic;

namespace HxSoft.Web.Admin._System
{
    public partial class Limit : System.Web.UI.Page
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
        public string ParentID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ParentID"], 0).ToString();
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
            GetData.LimitChkMsg("Limit");
            lbtnAdd.Visible = GetData.LimitChk("LimitAdd");
            lbtnEdit.Visible = GetData.LimitChk("LimitEdit");
            lbtnMove.Visible = GetData.LimitChk("LimitMove");
            lbtnDel.Visible = GetData.LimitChk("LimitDel");
            lbtnOpen.Visible = GetData.LimitChk("LimitOpen");
            lbtnClose.Visible = GetData.LimitChk("LimitClose");
            if (!Page.IsPostBack)
            {
                lblNav.Text=Factory.Limit().ShowPath(ParentID).ToString();
                btnQuery.PostBackUrl = "Limit.aspx?ParentID=" + ParentID;
                lbtnAdd.OnClientClick = "javascript:return GoTo('Limit_Add.aspx?ParentID="+ParentID+"')";
                //返回上级
                LimitModel limModel = new LimitModel();
                limModel = Factory.Limit().GetInfo(ParentID);
                if (limModel != null)
                {
                    lbtnGoBack.OnClientClick = "javascript:return GoTo('Limit.aspx?ParentID=" + limModel.ParentID + "')";
                }
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Limit where 1=1 and ParentID=" + ParentID + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strLimitID = Config.Request(Request.Form["LimitID"], "0");
            if (strLimitID != "0")
            {
                Response.Redirect("Limit_Add.aspx?ParentID=" + ParentID + "&LimitID=" + strLimitID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //移动
        protected void lbtnMove_Click(object sender, EventArgs e)
        {
            string strLimitID = Config.Request(Request.Form["LimitID"], "0");
            if (strLimitID != "0")
            {
                Response.Redirect("Limit_Move.aspx?ParentID=" + ParentID + "&LimitID=" + strLimitID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strLimitID = Config.Request(Request.Form["LimitID"], "0");
            if (strLimitID != "0")
            {
                string[] arrLimitID = strLimitID.Split(new char[] { ',' });
                StringBuilder strTempLimitID = new StringBuilder();
                LimitModel limModel = new LimitModel();
                int n = 0;
                for (int i = 0; i < arrLimitID.Length; i++)
                {
                    limModel = Factory.Limit().GetInfo(arrLimitID[i]);
                    if (limModel != null)
                    {
                        if (Convert.ToInt32(limModel.ChildNum)==0)
                        {
                            if (GetData.CheckAdminID(limModel.AdminID, "LimitAll"))//检查创建者
                            {
                                Factory.Limit().DeleteInfo(arrLimitID[i]);
                                strTempLimitID.Append(arrLimitID[i]);
                                if (i + 1 < arrLimitID.Length) strTempLimitID.Append(",");
                                n++;
                            }
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempLimitID.ToString() + "的权限字段!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempLimitID.ToString() + "权限字段删除成功!", "Limit.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败，请先删除子级!");
                }
            }
        }

        //批量开放
        protected void btnOpen_Click(object sender, EventArgs e)
        {
            string strLimitID = Config.Request(Request.Form["LimitID"], "0");
            if (strLimitID != "0")
            {
                string[] arrLimitID = strLimitID.Split(new char[] { ',' });
                StringBuilder strTempLimitID = new StringBuilder();
                LimitModel limModel = new LimitModel();
                int n = 0;
                for (int i = 0; i < arrLimitID.Length; i++)
                {
                    limModel = Factory.Limit().GetInfo(arrLimitID[i]);
                    if (limModel != null)
                    {
                        if (GetData.CheckAdminID(limModel.AdminID, "LimitAll"))//检查创建者
                        {
                            Factory.Limit().UpdateCloseStatus(arrLimitID[i], "0");
                            strTempLimitID.Append(arrLimitID[i]);
                            if (i + 1 < arrLimitID.Length) strTempLimitID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempLimitID.ToString() + "的权限字段!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempLimitID.ToString() + "权限字段开放成功!", "Limit.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strLimitID = Config.Request(Request.Form["LimitID"], "0");
            if (strLimitID != "0")
            {
                string[] arrLimitID = strLimitID.Split(new char[] { ',' });
                StringBuilder strTempLimitID = new StringBuilder();
                LimitModel limModel = new LimitModel();
                int n = 0;
                for (int i = 0; i < arrLimitID.Length; i++)
                {
                    limModel = Factory.Limit().GetInfo(arrLimitID[i]);
                    if (limModel != null)
                    {
                        if (GetData.CheckAdminID(limModel.AdminID, "LimitAll"))//检查创建者
                        {
                            Factory.Limit().UpdateCloseStatus(arrLimitID[i], "1");
                            strTempLimitID.Append(arrLimitID[i]);
                            if (i + 1 < arrLimitID.Length) strTempLimitID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempLimitID.ToString() + "的权限字段!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempLimitID.ToString() + "权限字段关闭成功!", "Limit.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
