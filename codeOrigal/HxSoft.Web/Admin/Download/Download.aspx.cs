using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;
using System.Data;
using System.Data.Common;

namespace HxSoft.Web.Admin.Download
{
    public partial class Download : System.Web.UI.Page
    {
        /// <summary>
        ///下载管理
        /// 创建人:杨小明
        /// 日期:2011-2-24
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
                return Config.Request(Request["OrderKey"], "DownloadID");
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
        public string strClassID
        {
            get
            {
                return Config.Request(Request["drpClassID"], "-1");
            }
        }
        public string strDownName
        {
            get
            {
                return Config.Request(Request["txtDownName"], "");
            }
        }
        public string strIsRecommend
        {
            get
            {
                return Config.Request(Request["radIsRecommend"], "-1");
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
                if (strClassID != "-1") TempSql.Append(" and (ClassID=@ClassID or ClassID in (" + Factory.Class().GetSubClassSql(strClassID) + "))");
                if (strDownName != "") TempSql.Append(" and DownName like @DownName");
                if (strIsRecommend != "-1") TempSql.Append(" and IsRecommend =@IsRecommend");
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
                if (strClassID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@ClassID", strClassID));
                if (strDownName != "") listParams.Add(Config.Conn().CreateDbParameter("@DownName", "%" + strDownName + "%"));
                if (strIsRecommend != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsRecommend", strIsRecommend));
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
                TempUrl.Append("drpClassID=" + Server.UrlEncode(strClassID) + "&");
                TempUrl.Append("txtDownName=" + Server.UrlEncode(strDownName) + "&");
                TempUrl.Append("radIsRecommend=" + Server.UrlEncode(strIsRecommend) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("Download");
            lbtnAdd.Visible = GetData.LimitChk("DownloadAdd");
            lbtnEdit.Visible = GetData.LimitChk("DownloadEdit");
            lbtnDel.Visible = GetData.LimitChk("DownloadDel");
            lbtnOpen.Visible = GetData.LimitChk("DownloadOpen");
            lbtnClose.Visible = GetData.LimitChk("DownloadClose");
            lbtnTransfer.Visible = GetData.LimitChk("DownloadTransfer");
            if (!Page.IsPostBack)
            {
                //下载分类
                Factory.Class().ShowSelectTree("0", drpClassID, "", Config.SysDownloadMouldID);
                drpClassID.Items.Insert(0, new ListItem("不限", "-1"));

                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Download where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strDownloadID = Config.Request(Request.Form["DownloadID"], "0");
            if (strDownloadID != "0")
            {
                Response.Redirect("Download_Add.aspx?DownloadID=" + strDownloadID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strDownloadID = Config.Request(Request.Form["DownloadID"], "0");
            if (strDownloadID != "0")
            {
                string[] arrDownloadID = strDownloadID.Split(new char[] { ',' });
                StringBuilder strTempDownloadID = new StringBuilder();
                DownloadModel dowModel = new DownloadModel();
                int n = 0;
                for (int i = 0; i < arrDownloadID.Length; i++)
                {
                    dowModel = Factory.Download().GetInfo(arrDownloadID[i]);
                    if (dowModel != null)
                    {
                        if (GetData.CheckAdminID(dowModel.AdminID, "DownloadAll"))//检查创建者
                        {
                            Factory.Download().DeleteInfo(arrDownloadID[i]);
                            strTempDownloadID.Append(arrDownloadID[i]);
                            if (i + 1 < arrDownloadID.Length) strTempDownloadID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempDownloadID.ToString() + "的下载!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempDownloadID.ToString() + "下载删除成功!", "Download.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strDownloadID = Config.Request(Request.Form["DownloadID"], "0");
            if (strDownloadID != "0")
            {
                string[] arrDownloadID = strDownloadID.Split(new char[] { ',' });
                StringBuilder strTempDownloadID = new StringBuilder();
                DownloadModel dowModel = new DownloadModel();
                int n = 0;
                for (int i = 0; i < arrDownloadID.Length; i++)
                {
                    dowModel = Factory.Download().GetInfo(arrDownloadID[i]);
                    if (dowModel != null)
                    {
                        if (GetData.CheckAdminID(dowModel.AdminID, "DownloadAll"))//检查创建者
                        {
                            Factory.Download().UpdateCloseStatus(arrDownloadID[i], "0");
                            strTempDownloadID.Append(arrDownloadID[i]);
                            if (i + 1 < arrDownloadID.Length) strTempDownloadID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempDownloadID.ToString() + "的下载!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempDownloadID.ToString() + "下载开放成功!", "Download.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strDownloadID = Config.Request(Request.Form["DownloadID"], "0");
            if (strDownloadID != "0")
            {
                string[] arrDownloadID = strDownloadID.Split(new char[] { ',' });
                StringBuilder strTempDownloadID = new StringBuilder();
                DownloadModel dowModel = new DownloadModel();
                int n = 0;
                for (int i = 0; i < arrDownloadID.Length; i++)
                {
                    dowModel = Factory.Download().GetInfo(arrDownloadID[i]);
                    if (dowModel != null)
                    {
                        if (GetData.CheckAdminID(dowModel.AdminID, "DownloadAll"))//检查创建者
                        {
                            Factory.Download().UpdateCloseStatus(arrDownloadID[i], "1");
                            strTempDownloadID.Append(arrDownloadID[i]);
                            if (i + 1 < arrDownloadID.Length) strTempDownloadID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempDownloadID.ToString() + "的下载!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempDownloadID.ToString() + "下载关闭成功!", "Download.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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

        //转移文章
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            string strDownloadID = Config.Request(Request.Form["DownloadID"], "0");
            if (strDownloadID != "0")
            {
                Response.Redirect("Download_Transfer.aspx?DownloadID=" + strDownloadID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

    }
}
