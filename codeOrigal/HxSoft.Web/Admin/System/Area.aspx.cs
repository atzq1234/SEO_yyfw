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
    public partial class Area : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        DataAccess datAc = new DataAccess(Config.AreaConnStr);
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
            GetData.LimitChkMsg("Area");
            lbtnAdd.Visible = GetData.LimitChk("AreaAdd");
            lbtnEdit.Visible = GetData.LimitChk("AreaEdit");
            lbtnMove.Visible = GetData.LimitChk("AreaMove");
            lbtnDel.Visible = GetData.LimitChk("AreaDel");
            lbtnOpen.Visible = GetData.LimitChk("AreaOpen");
            lbtnClose.Visible = GetData.LimitChk("AreaClose");
            lbtnImport.Visible = GetData.LimitChk("AreaImport");
            if (!Page.IsPostBack)
            {
                lblNav.Text = Factory.Area().ShowPath(ParentID).ToString();
                btnQuery.PostBackUrl = "Area.aspx?ParentID=" + ParentID;
                lbtnAdd.OnClientClick = "javascript:return GoTo('Area_Add.aspx?ParentID=" + ParentID + "')";
                //返回上级
                AreaModel areaModel = new AreaModel();
                areaModel = Factory.Area().GetInfo(ParentID);
                if (areaModel != null)
                {
                    lbtnGoBack.OnClientClick = "javascript:return GoTo('Area.aspx?ParentID=" + areaModel.ParentID + "')";
                }
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Area where 1=1 and ParentID=" + ParentID + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strAreaID = Config.Request(Request.Form["AreaID"], "0");
            if (strAreaID != "0")
            {
                Response.Redirect("Area_Add.aspx?ParentID=" + ParentID + "&AreaID=" + strAreaID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //移动
        protected void lbtnMove_Click(object sender, EventArgs e)
        {
            string strAreaID = Config.Request(Request.Form["AreaID"], "0");
            if (strAreaID != "0")
            {
                Response.Redirect("Area_Move.aspx?ParentID=" + ParentID + "&AreaID=" + strAreaID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strAreaID = Config.Request(Request.Form["AreaID"], "0");
            if (strAreaID != "0")
            {
                string[] arrAreaID = strAreaID.Split(new char[] { ',' });
                StringBuilder strTempAreaID = new StringBuilder();
                AreaModel areaModel = new AreaModel();
                int n = 0;
                for (int i = 0; i < arrAreaID.Length; i++)
                {
                    areaModel = Factory.Area().GetInfo(arrAreaID[i]);
                    if (areaModel != null)
                    {
                        if (Convert.ToInt32(areaModel.ChildNum) == 0)
                        {
                            if (GetData.CheckAdminID(areaModel.AdminID, "AreaAll"))//检查创建者
                            {
                                Factory.Area().DeleteInfo(arrAreaID[i]);
                                strTempAreaID.Append(arrAreaID[i]);
                                if (i + 1 < arrAreaID.Length) strTempAreaID.Append(",");
                                n++;
                            }
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempAreaID.ToString() + "的地区!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempAreaID.ToString() + "地区删除成功!", "Area.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strAreaID = Config.Request(Request.Form["AreaID"], "0");
            if (strAreaID != "0")
            {
                string[] arrAreaID = strAreaID.Split(new char[] { ',' });
                StringBuilder strTempAreaID = new StringBuilder();
                AreaModel areaModel = new AreaModel();
                int n = 0;
                for (int i = 0; i < arrAreaID.Length; i++)
                {
                    areaModel = Factory.Area().GetInfo(arrAreaID[i]);
                    if (areaModel != null)
                    {
                        if (GetData.CheckAdminID(areaModel.AdminID, "AreaAll"))//检查创建者
                        {
                            Factory.Area().UpdateCloseStatus(arrAreaID[i], "0");
                            strTempAreaID.Append(arrAreaID[i]);
                            if (i + 1 < arrAreaID.Length) strTempAreaID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempAreaID.ToString() + "的地区!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempAreaID.ToString() + "地区开放成功!", "Area.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strAreaID = Config.Request(Request.Form["AreaID"], "0");
            if (strAreaID != "0")
            {
                string[] arrAreaID = strAreaID.Split(new char[] { ',' });
                StringBuilder strTempAreaID = new StringBuilder();
                AreaModel areaModel = new AreaModel();
                int n = 0;
                for (int i = 0; i < arrAreaID.Length; i++)
                {
                    areaModel = Factory.Area().GetInfo(arrAreaID[i]);
                    if (areaModel != null)
                    {
                        if (GetData.CheckAdminID(areaModel.AdminID, "AreaAll"))//检查创建者
                        {
                            Factory.Area().UpdateCloseStatus(arrAreaID[i], "1");
                            strTempAreaID.Append(arrAreaID[i]);
                            if (i + 1 < arrAreaID.Length) strTempAreaID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempAreaID.ToString() + "的地区!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempAreaID.ToString() + "地区关闭成功!", "Area.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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

        //导入地区数据
        protected void lbtnImport_Click(object sender, EventArgs e)
        {
            ImportArea_1();
            ImportArea_2();
            Factory.AdminLog().InsertLog("导入地区数据。", Session["AdminID"].ToString());
            Config.MsgGotoUrl("导入成功！", "Area.aspx");
        }

        #region 导入地区
        /// <summary>
        /// 导入省份
        /// </summary>
        public void ImportArea_1()
        {
            string strParentID = "0";
            string sql = "select * from t_Area where ParentID=0 order by AreaID asc";
            DataTable dt = datAc.GetDataSet(CommandType.Text, sql, null).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (!Factory.Area().CheckInfo("AreaName", dr["AreaName"].ToString(), strParentID))
                {
                    AreaModel areaModel = new AreaModel();
                    areaModel.AreaName = dr["AreaName"].ToString();
                    areaModel.ParentID = strParentID;
                    areaModel.ChildNum = "0";
                    areaModel.ListID = Factory.Area().GetListID(strParentID);
                    areaModel.AdminID = Session["AdminID"].ToString();
                    areaModel.AddTime = DateTime.Now.ToString();
                    areaModel.IsClose = "0";
                    Factory.Area().InsertInfo(areaModel);
                }
            }
        }

        /// <summary>
        /// 导入城市
        /// </summary>
        public void ImportArea_2()
        {
            string sql_1 = "select * from t_Area where ParentID=0 order by AreaID asc";
            DataTable dt_1 = Factory.Acc().GetDataTable(sql_1, null);
            for (int i = 0; i < dt_1.Rows.Count; i++)
            {
                DataRow dr_1 = dt_1.Rows[i];
                string strParentID = dr_1["AreaID"].ToString();
                string sql_2 = "select * from t_Area where ParentID=(select AreaID from t_Area where AreaName='" + dr_1["AreaName"].ToString() + "') order by AreaID asc";
                DataTable dt_2 = datAc.GetDataSet(CommandType.Text, sql_2, null).Tables[0];
                for (int j = 0; j < dt_2.Rows.Count; j++)
                {
                    DataRow dr_2 = dt_2.Rows[j];
                    if (!Factory.Area().CheckInfo("AreaName", dr_2["AreaName"].ToString(), strParentID))
                    {
                        AreaModel areaModel = new AreaModel();
                        areaModel.AreaName = dr_2["AreaName"].ToString();
                        areaModel.ParentID = strParentID;
                        areaModel.ChildNum = "0";
                        areaModel.ListID = Factory.Area().GetListID(strParentID);
                        areaModel.AdminID = Session["AdminID"].ToString();
                        areaModel.AddTime = DateTime.Now.ToString();
                        areaModel.IsClose = "0";
                        Factory.Area().InsertInfo(areaModel);
                    }
                }
            }
        }
        #endregion

    }
}
