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

namespace HxSoft.Web.Admin.Product
{
    public partial class ProductPic : System.Web.UI.Page
    {
        /// <summary>
        ///产品图片
        /// 创建人:杨小明
        /// 日期:2012-1-19
        /// </summary>
        //定义全局变量
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        public string ProductID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ProductID"], 0).ToString();
            }
        }
        public int ProductPage
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ProductPage"], 1);
            }
        }
        #region ****排序参数****
        public string strOrderKey
        {
            get
            {
                return Config.Request(Request["OrderKey"], "ProductPicID");
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
        public string strTitle
        {
            get
            {
                return Config.Request(Request["txtTitle"], "");
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
                if (strTitle != "") TempSql.Append(" and Title like @Title");
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
                if (strTitle != "") listParams.Add(Config.Conn().CreateDbParameter("@Title", "%" + strTitle + "%"));
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
                TempUrl.Append("txtTitle=" + Server.UrlEncode(strTitle) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("ProductPic");
            lbtnAdd.Visible = GetData.LimitChk("ProductPicAdd");
            lbtnEdit.Visible = GetData.LimitChk("ProductPicEdit");
            lbtnDel.Visible = GetData.LimitChk("ProductPicDel");
            lbtnOpen.Visible = GetData.LimitChk("ProductPicOpen");
            lbtnClose.Visible = GetData.LimitChk("ProductPicClose");
            if (!Page.IsPostBack)
            {
                lblProductName.Text = "("+Factory.Product().GetValueByField("ProductName",ProductID)+")";
                btnQuery.PostBackUrl = "ProductPic.aspx?ProductID=" + ProductID + "&ProductPage=" + ProductPage;
                lbtnAdd.OnClientClick = "javascript:return GoTo('ProductPic_Add.aspx?ProductID=" + ProductID + "&ProductPage=" + ProductPage + "')";
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_ProductPic where 1=1  and ProductID =" + ProductID + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?ProductID=" + ProductID + "&ProductPage=" + ProductPage + "&" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strProductPicID = Config.Request(Request.Form["ProductPicID"], "0");
            if (strProductPicID != "0")
            {
                Response.Redirect("ProductPic_Add.aspx?ProductID=" + ProductID + "&ProductPage=" + ProductPage + "&ProductPicID=" + strProductPicID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strProductPicID = Config.Request(Request.Form["ProductPicID"], "0");
            if (strProductPicID != "0")
            {
                string[] arrProductPicID = strProductPicID.Split(new char[] { ',' });
                StringBuilder strTempProductPicID = new StringBuilder();
                ProductPicModel proPicModel = new ProductPicModel();
                int n = 0;
                for (int i = 0; i < arrProductPicID.Length; i++)
                {
                    proPicModel = Factory.ProductPic().GetInfo(arrProductPicID[i]);
                    if (proPicModel != null)
                    {
                        if (GetData.CheckAdminID(proPicModel.AdminID, "ProductPicAll"))//检查创建者
                        {
                            Factory.ProductPic().DeleteInfo(arrProductPicID[i]);
                            strTempProductPicID.Append(arrProductPicID[i]);
                            if (i + 1 < arrProductPicID.Length) strTempProductPicID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempProductPicID.ToString() + "的产品图片!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempProductPicID.ToString() + "的产品图片删除成功!", "ProductPic.aspx?ProductID=" + ProductID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strProductPicID = Config.Request(Request.Form["ProductPicID"], "0");
            if (strProductPicID != "0")
            {
                string[] arrProductPicID = strProductPicID.Split(new char[] { ',' });
                StringBuilder strTempProductPicID = new StringBuilder();
                ProductPicModel proPicModel = new ProductPicModel();
                int n = 0;
                for (int i = 0; i < arrProductPicID.Length; i++)
                {
                    proPicModel = Factory.ProductPic().GetInfo(arrProductPicID[i]);
                    if (proPicModel != null)
                    {
                        if (GetData.CheckAdminID(proPicModel.AdminID, "ProductPicAll"))//检查创建者
                        {
                            Factory.ProductPic().UpdateCloseStatus(arrProductPicID[i], "0");
                            strTempProductPicID.Append(arrProductPicID[i]);
                            if (i + 1 < arrProductPicID.Length) strTempProductPicID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempProductPicID.ToString() + "的产品图片!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempProductPicID.ToString() + "的产品图片开放成功!", "ProductPic.aspx?ProductID=" + ProductID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strProductPicID = Config.Request(Request.Form["ProductPicID"], "0");
            if (strProductPicID != "0")
            {
                string[] arrProductPicID = strProductPicID.Split(new char[] { ',' });
                StringBuilder strTempProductPicID = new StringBuilder();
                ProductPicModel proPicModel = new ProductPicModel();
                int n = 0;
                for (int i = 0; i < arrProductPicID.Length; i++)
                {
                    proPicModel = Factory.ProductPic().GetInfo(arrProductPicID[i]);
                    if (proPicModel != null)
                    {
                        if (GetData.CheckAdminID(proPicModel.AdminID, "ProductPicAll"))//检查创建者
                        {
                            Factory.ProductPic().UpdateCloseStatus(arrProductPicID[i], "1");
                            strTempProductPicID.Append(arrProductPicID[i]);
                            if (i + 1 < arrProductPicID.Length) strTempProductPicID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempProductPicID.ToString() + "的产品图片!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempProductPicID.ToString() + "的产品图片关闭成功!", "ProductPic.aspx?ProductID=" + ProductID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
