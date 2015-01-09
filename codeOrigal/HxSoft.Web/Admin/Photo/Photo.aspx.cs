using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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

namespace HxSoft.Web.Admin.Photo
{
    public partial class Photo : System.Web.UI.Page
    {
        /// <summary>
        ///相册管理
        /// 创建人:
        /// 日期:2012-9-20
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
                return Config.Request(Request["OrderKey"], "PhotoID");
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
        public string strTitle
        {
            get
            {
                return Config.Request(Request["txtTitle"], "");
            }
        }
        #endregion
        #region ****查询语句****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (strClassID != "-1") TempSql.Append(" and ClassID = @ClassID");
                if (strTitle != "") TempSql.Append(" and Title like @Title");
                return TempSql.ToString();
            }
        }
        #endregion
        #region ****DbParameter参数****
        public DbParameter[] SqlParams
        {
            get
            {
                List<DbParameter> listParams = new List<DbParameter>();
                if (strClassID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@ClassID", strClassID));
                if (strTitle != "") listParams.Add(Config.Conn().CreateDbParameter("@Title", "%" + strTitle + "%"));
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
                TempUrl.Append("txtTitle=" + Server.UrlEncode(strTitle) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("Photo");
            lbtnAdd.Visible = GetData.LimitChk("PhotoAdd");
            lbtnEdit.Visible = GetData.LimitChk("PhotoEdit");
            lbtnDel.Visible = GetData.LimitChk("PhotoDel");
            lbtnOpen.Visible = GetData.LimitChk("PhotoOpen");
            lbtnClose.Visible = GetData.LimitChk("PhotoClose");
            if (!Page.IsPostBack)
            {
                //栏目分类
                Factory.Class().ShowSelectTree("0", drpClassID, "", Config.SysPhotoMouldID);
                drpClassID.Items.Insert(0, new ListItem("请选择", "-1"));
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Photo where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strPhotoID = Config.Request(Request.Form["PhotoID"], "0");
            if (strPhotoID != "0")
            {
                Response.Redirect("Photo_Add.aspx?PhotoID=" + strPhotoID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strPhotoID = Config.Request(Request.Form["PhotoID"], "0");
            if (strPhotoID != "0")
            {
                string[] arrPhotoID = strPhotoID.Split(new char[] { ',' });
                StringBuilder strTempPhotoID = new StringBuilder();
                PhotoModel phoModel = new PhotoModel();
                int n = 0;
                for (int i = 0; i < arrPhotoID.Length; i++)
                {
                    phoModel = Factory.Photo().GetInfo(arrPhotoID[i]);
                    if (phoModel != null)
                    {
                        if (GetData.CheckAdminID(phoModel.AdminID, "PhotoAll"))//检查创建者
                        {
                            Factory.Photo().DeleteInfo(arrPhotoID[i]);
                            strTempPhotoID.Append(arrPhotoID[i]);
                            if (i + 1 < arrPhotoID.Length) strTempPhotoID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempPhotoID.ToString() + "的相册!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempPhotoID.ToString() + "的相册删除成功!", "Photo.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strPhotoID = Config.Request(Request.Form["PhotoID"], "0");
            if (strPhotoID != "0")
            {
                string[] arrPhotoID = strPhotoID.Split(new char[] { ',' });
                StringBuilder strTempPhotoID = new StringBuilder();
                PhotoModel phoModel = new PhotoModel();
                int n = 0;
                for (int i = 0; i < arrPhotoID.Length; i++)
                {
                    phoModel = Factory.Photo().GetInfo(arrPhotoID[i]);
                    if (phoModel != null)
                    {
                        if (GetData.CheckAdminID(phoModel.AdminID, "PhotoAll"))//检查创建者
                        {
                            Factory.Photo().UpdateCloseStatus(arrPhotoID[i], "0");
                            strTempPhotoID.Append(arrPhotoID[i]);
                            if (i + 1 < arrPhotoID.Length) strTempPhotoID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempPhotoID.ToString() + "的相册!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempPhotoID.ToString() + "的相册开放成功!", "Photo.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        //关闭
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string strPhotoID = Config.Request(Request.Form["PhotoID"], "0");
            if (strPhotoID != "0")
            {
                string[] arrPhotoID = strPhotoID.Split(new char[] { ',' });
                StringBuilder strTempPhotoID = new StringBuilder();
                PhotoModel phoModel = new PhotoModel();
                int n = 0;
                for (int i = 0; i < arrPhotoID.Length; i++)
                {
                    phoModel = Factory.Photo().GetInfo(arrPhotoID[i]);
                    if (phoModel != null)
                    {
                        if (GetData.CheckAdminID(phoModel.AdminID, "PhotoAll"))//检查创建者
                        {
                            Factory.Photo().UpdateCloseStatus(arrPhotoID[i], "1");
                            strTempPhotoID.Append(arrPhotoID[i]);
                            if (i + 1 < arrPhotoID.Length) strTempPhotoID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempPhotoID.ToString() + "的相册!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempPhotoID.ToString() + "的相册关闭成功!", "Photo.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
