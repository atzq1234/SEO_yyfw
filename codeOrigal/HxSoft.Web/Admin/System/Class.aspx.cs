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
    public partial class Class : System.Web.UI.Page
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
        public string strClassName
        {
            get
            {
                return Config.Request(Request["txtClassName"], "");
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
                if (strClassName != "") TempSql.Append(" and ClassName like @ClassName");
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
                if (strClassName != "") listParams.Add(Config.Conn().CreateDbParameter("@ClassName", "%" + strClassName + "%"));
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
                TempUrl.Append("txtClassName=" + Server.UrlEncode(strClassName) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("Class");
            lbtnAdd.Visible = GetData.LimitChk("ClassAdd");
            lbtnEdit.Visible = GetData.LimitChk("ClassEdit");
            lbtnMove.Visible = GetData.LimitChk("ClassMove");
            lbtnDel.Visible = GetData.LimitChk("ClassDel");
            lbtnOpen.Visible = GetData.LimitChk("ClassOpen");
            lbtnClose.Visible = GetData.LimitChk("ClassClose");
            if (!Page.IsPostBack)
            {
                lblNav.Text = Factory.Class().ShowPath(ParentID).ToString();
                btnQuery.PostBackUrl = "Class.aspx?ParentID=" + ParentID;
                lbtnAdd.OnClientClick = "javascript:return GoTo('Class_Add.aspx?ParentID=" + ParentID + "')";
                //返回上级
                ClassModel claModel = new ClassModel();
                claModel = Factory.Class().GetInfo(ParentID);
                if (claModel != null)
                {
                    lbtnGoBack.OnClientClick = "javascript:return GoTo('Class.aspx?ParentID=" + claModel.ParentID + "')";
                }
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Class where 1=1 and ParentID=" + ParentID + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strClassID = Config.Request(Request.Form["ClassID"], "0");
            if (strClassID != "0")
            {
                Response.Redirect("Class_Add.aspx?ParentID=" + ParentID + "&ClassID=" + strClassID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //移动
        protected void lbtnMove_Click(object sender, EventArgs e)
        {
            string strClassID = Config.Request(Request.Form["ClassID"], "0");
            if (strClassID != "0")
            {
                Response.Redirect("Class_Move.aspx?ParentID=" + ParentID + "&ClassID=" + strClassID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strClassID = Config.Request(Request.Form["ClassID"], "0");
            if (strClassID != "0")
            {
                string[] arrClassID = strClassID.Split(new char[] { ',' });
                StringBuilder strTempClassID = new StringBuilder();
                ClassModel claModel = new ClassModel();
                int n = 0;
                for (int i = 0; i < arrClassID.Length; i++)
                {
                    claModel = Factory.Class().GetInfo(arrClassID[i]);
                    if (claModel != null)
                    {
                        if (Convert.ToInt32(claModel.ChildNum) == 0)
                        {
                            if (GetData.CheckAdminID(claModel.AdminID, "ClassAll"))//检查创建者
                            {
                                Factory.Class().DeleteInfo(arrClassID[i]);
                                strTempClassID.Append(arrClassID[i]);
                                if (i + 1 < arrClassID.Length) strTempClassID.Append(",");
                                n++;
                            }
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempClassID.ToString() + "的栏目!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempClassID.ToString() + "栏目删除成功!", "Class.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strClassID = Config.Request(Request.Form["ClassID"], "0");
            if (strClassID != "0")
            {
                string[] arrClassID = strClassID.Split(new char[] { ',' });
                StringBuilder strTempClassID = new StringBuilder();
                ClassModel claModel = new ClassModel();
                int n = 0;
                for (int i = 0; i < arrClassID.Length; i++)
                {
                    claModel = Factory.Class().GetInfo(arrClassID[i]);
                    if (claModel != null)
                    {
                        if (GetData.CheckAdminID(claModel.AdminID, "ClassAll"))//检查创建者
                        {
                            Factory.Class().UpdateCloseStatus(arrClassID[i], "0");
                            strTempClassID.Append(arrClassID[i]);
                            if (i + 1 < arrClassID.Length) strTempClassID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempClassID.ToString() + "的栏目!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempClassID.ToString() + "栏目开放成功!", "Class.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strClassID = Config.Request(Request.Form["ClassID"], "0");
            if (strClassID != "0")
            {
                string[] arrClassID = strClassID.Split(new char[] { ',' });
                StringBuilder strTempClassID = new StringBuilder();
                ClassModel claModel = new ClassModel();
                int n = 0;
                for (int i = 0; i < arrClassID.Length; i++)
                {
                    claModel = Factory.Class().GetInfo(arrClassID[i]);
                    if (claModel != null)
                    {
                        if (GetData.CheckAdminID(claModel.AdminID, "ClassAll"))//检查创建者
                        {
                            Factory.Class().UpdateCloseStatus(arrClassID[i], "1");
                            strTempClassID.Append(arrClassID[i]);
                            if (i + 1 < arrClassID.Length) strTempClassID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempClassID.ToString() + "的栏目!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempClassID.ToString() + "栏目关闭成功!", "Class.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
