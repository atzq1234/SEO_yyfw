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
    public partial class ClassProperty : System.Web.UI.Page
    {
        /// <summary>
        ///栏目属性
        /// 创建人:杨小明
        /// 日期:2012-4-16
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
                return Config.Request(Request["OrderKey"], "ClassPropertyID");
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
        public string strPropertyName
        {
            get
            {
                return Config.Request(Request["txtPropertyName"], "");
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
                if (strPropertyName != "") TempSql.Append(" and PropertyName like @PropertyName");
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
                if (strPropertyName != "") listParams.Add(Config.Conn().CreateDbParameter("@PropertyName", "%" + strPropertyName + "%"));
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
                TempUrl.Append("txtPropertyName=" + Server.UrlEncode(strPropertyName) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("ClassProperty");
            lbtnAdd.Visible = GetData.LimitChk("ClassPropertyAdd");
            lbtnEdit.Visible = GetData.LimitChk("ClassPropertyEdit");
            lbtnDel.Visible = GetData.LimitChk("ClassPropertyDel");
            lbtnOpen.Visible = GetData.LimitChk("ClassPropertyOpen");
            lbtnClose.Visible = GetData.LimitChk("ClassPropertyClose");
            if (!Page.IsPostBack)
            {
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_ClassProperty where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strClassPropertyID = Config.Request(Request.Form["ClassPropertyID"], "0");
            if (strClassPropertyID != "0")
            {
                Response.Redirect("ClassProperty_Add.aspx?ClassPropertyID=" + strClassPropertyID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strClassPropertyID = Config.Request(Request.Form["ClassPropertyID"], "0");
            if (strClassPropertyID != "0")
            {
                string[] arrClassPropertyID = strClassPropertyID.Split(new char[] { ',' });
                StringBuilder strTempClassPropertyID = new StringBuilder();
                ClassPropertyModel claProModel = new ClassPropertyModel();
                int n = 0;
                for (int i = 0; i < arrClassPropertyID.Length; i++)
                {
                    claProModel = Factory.ClassProperty().GetInfo(arrClassPropertyID[i]);
                    if (claProModel != null)
                    {
                        if (GetData.CheckAdminID(claProModel.AdminID, "ClassPropertyAll"))//检查创建者
                        {
                            Factory.ClassProperty().DeleteInfo(arrClassPropertyID[i]);
                            strTempClassPropertyID.Append(arrClassPropertyID[i]);
                            if (i + 1 < arrClassPropertyID.Length) strTempClassPropertyID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempClassPropertyID.ToString() + "的栏目属性!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempClassPropertyID.ToString() + "的栏目属性删除成功!", "ClassProperty.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        //开放
        protected void btnOpen_Click(object sender, EventArgs e)
        {
            string strClassPropertyID = Config.Request(Request.Form["ClassPropertyID"], "0");
            if (strClassPropertyID != "0")
            {
                string[] arrClassPropertyID = strClassPropertyID.Split(new char[] { ',' });
                StringBuilder strTempClassPropertyID = new StringBuilder();
                ClassPropertyModel claProModel = new ClassPropertyModel();
                int n = 0;
                for (int i = 0; i < arrClassPropertyID.Length; i++)
                {
                    claProModel = Factory.ClassProperty().GetInfo(arrClassPropertyID[i]);
                    if (claProModel != null)
                    {
                        if (GetData.CheckAdminID(claProModel.AdminID, "ClassPropertyAll"))//检查创建者
                        {
                            Factory.ClassProperty().UpdateCloseStatus(arrClassPropertyID[i], "0");
                            strTempClassPropertyID.Append(arrClassPropertyID[i]);
                            if (i + 1 < arrClassPropertyID.Length) strTempClassPropertyID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempClassPropertyID.ToString() + "的栏目属性!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempClassPropertyID.ToString() + "的栏目属性开放成功!", "ClassProperty.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        //删除
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string strClassPropertyID = Config.Request(Request.Form["ClassPropertyID"], "0");
            if (strClassPropertyID != "0")
            {
                string[] arrClassPropertyID = strClassPropertyID.Split(new char[] { ',' });
                StringBuilder strTempClassPropertyID = new StringBuilder();
                ClassPropertyModel claProModel = new ClassPropertyModel();
                int n = 0;
                for (int i = 0; i < arrClassPropertyID.Length; i++)
                {
                    claProModel = Factory.ClassProperty().GetInfo(arrClassPropertyID[i]);
                    if (claProModel != null)
                    {
                        if (GetData.CheckAdminID(claProModel.AdminID, "ClassPropertyAll"))//检查创建者
                        {
                            Factory.ClassProperty().UpdateCloseStatus(arrClassPropertyID[i], "1");
                            strTempClassPropertyID.Append(arrClassPropertyID[i]);
                            if (i + 1 < arrClassPropertyID.Length) strTempClassPropertyID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempClassPropertyID.ToString() + "的栏目属性!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempClassPropertyID.ToString() + "的栏目属性关闭成功!", "ClassProperty.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
