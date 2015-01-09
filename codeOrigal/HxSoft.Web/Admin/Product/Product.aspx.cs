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

namespace HxSoft.Web.Admin.Product
{
    public partial class Product : System.Web.UI.Page
    {
        /// <summary>
        ///产品管理
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
                return Config.Request(Request["OrderKey"], "ProductID");
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
        public string strProductName
        {
            get
            {
                return Config.Request(Request["txtProductName"], "");
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
                if (strProductName != "") TempSql.Append(" and ProductName like @ProductName");
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
                if (strProductName != "") listParams.Add(Config.Conn().CreateDbParameter("@ProductName", "%" + strProductName + "%"));
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
                TempUrl.Append("txtProductName=" + Server.UrlEncode(strProductName) + "&");
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
            GetData.LimitChkMsg("Product");
            lbtnAdd.Visible = GetData.LimitChk("ProductAdd");
            lbtnEdit.Visible = GetData.LimitChk("ProductEdit");
            lbtnDel.Visible = GetData.LimitChk("ProductDel");
            lbtnOpen.Visible = GetData.LimitChk("ProductOpen");
            lbtnClose.Visible = GetData.LimitChk("ProductClose");
            lbtnCopy.Visible = GetData.LimitChk("ProductCopy");
            lbtnTransfer.Visible = GetData.LimitChk("ProductTransfer");
            if (!Page.IsPostBack)
            {
                //产品分类
                Factory.Class().ShowSelectTree("0", drpClassID, "", Config.SysProductMouldID);
                drpClassID.Items.Insert(0, new ListItem("不限", "-1"));
                
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Product where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strProductID = Config.Request(Request.Form["ProductID"], "0");
            if (strProductID != "0")
            {
                Response.Redirect("Product_Add.aspx?ProductID=" + strProductID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strProductID = Config.Request(Request.Form["ProductID"], "0");
            if (strProductID != "0")
            {
                string[] arrProductID = strProductID.Split(new char[] { ',' });
                StringBuilder strTempProductID = new StringBuilder();
                ProductModel proModel = new ProductModel();
                int n = 0;
                for (int i = 0; i < arrProductID.Length; i++)
                {
                    proModel = Factory.Product().GetInfo(arrProductID[i]);
                    if (proModel != null)
                    {
                        if (GetData.CheckAdminID(proModel.AdminID, "ProductAll"))//检查创建者
                        {
                            Factory.Product().DeleteInfo(arrProductID[i]);
                            strTempProductID.Append(arrProductID[i]);
                            if (i + 1 < arrProductID.Length) strTempProductID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempProductID.ToString() + "的产品!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempProductID.ToString() + "产品删除成功!", "Product.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strProductID = Config.Request(Request.Form["ProductID"], "0");
            if (strProductID != "0")
            {
                string[] arrProductID = strProductID.Split(new char[] { ',' });
                StringBuilder strTempProductID = new StringBuilder();
                ProductModel proModel = new ProductModel();
                int n = 0;
                for (int i = 0; i < arrProductID.Length; i++)
                {
                    proModel = Factory.Product().GetInfo(arrProductID[i]);
                    if (proModel != null)
                    {
                        if (GetData.CheckAdminID(proModel.AdminID, "ProductAll"))//检查创建者
                        {
                            Factory.Product().UpdateCloseStatus(arrProductID[i], "0");
                            strTempProductID.Append(arrProductID[i]);
                            if (i + 1 < arrProductID.Length) strTempProductID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempProductID.ToString() + "的产品!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempProductID.ToString() + "产品开放成功!", "Product.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }

            }
            
           
        }

        //批量关闭
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string strProductID = Config.Request(Request.Form["ProductID"], "0");
            if (strProductID != "0")
            {
                string[] arrProductID = strProductID.Split(new char[] { ',' });
                StringBuilder strTempProductID = new StringBuilder();
                ProductModel proModel = new ProductModel();
                int n = 0;
                for (int i = 0; i < arrProductID.Length; i++)
                {
                    proModel = Factory.Product().GetInfo(arrProductID[i]);
                    if (proModel != null)
                    {
                        if (GetData.CheckAdminID(proModel.AdminID, "ProductAll"))//检查创建者
                        {
                            Factory.Product().UpdateCloseStatus(arrProductID[i], "1");
                            strTempProductID.Append(arrProductID[i]);
                            if (i + 1 < arrProductID.Length) strTempProductID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempProductID.ToString() + "的产品!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempProductID.ToString() + "产品关闭成功!", "Product.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
           
           
        }

        //批量推荐
        protected void btnRecommend_Click(object sender, EventArgs e)
        {
            string strProductID = Config.Request(Request.Form["ProductID"], "0");
            if (strProductID != "0")
            {
                string[] arrProductID = strProductID.Split(new char[] { ',' });
                StringBuilder strTempProductID = new StringBuilder();
                ProductModel proModel = new ProductModel();
                int n = 0;
                for (int i = 0; i < arrProductID.Length; i++)
                {
                    proModel = Factory.Product().GetInfo(arrProductID[i]);
                    if (proModel != null)
                    {
                        if (GetData.CheckAdminID(proModel.AdminID, "ProductAll"))//检查创建者
                        {
                            Factory.Product().UpdateRecommendStatus(arrProductID[i], "1");
                            strTempProductID.Append(arrProductID[i]);
                            if (i + 1 < arrProductID.Length) strTempProductID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("推荐编号为" + strTempProductID.ToString() + "的产品!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempProductID.ToString() + "的产品推荐成功!", "Product.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
           
           
        }

        //取消推荐
        protected void btnCancelRecommend_Click(object sender, EventArgs e)
        {
            string strProductID = Config.Request(Request.Form["ProductID"], "0");
            if (strProductID != "0")
            {
                string[] arrProductID = strProductID.Split(new char[] { ',' });
                StringBuilder strTempProductID = new StringBuilder();
                ProductModel proModel = new ProductModel();
                int n = 0;
                for (int i = 0; i < arrProductID.Length; i++)
                {
                    proModel = Factory.Product().GetInfo(arrProductID[i]);
                    if (proModel != null)
                    {
                        if (GetData.CheckAdminID(proModel.AdminID, "ProductAll"))//检查创建者
                        {
                            Factory.Product().UpdateRecommendStatus(arrProductID[i], "0");
                            strTempProductID.Append(arrProductID[i]);
                            if (i + 1 < arrProductID.Length) strTempProductID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("取消编号为" + strTempProductID.ToString() + "的产品推荐!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempProductID.ToString() + "的产品取消推荐成功!", "Product.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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

        //转移产品
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            string strProductID = Config.Request(Request.Form["ProductID"], "0");
            if (strProductID != "0")
            {
                Response.Redirect("Product_Transfer.aspx?ProductID=" + strProductID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
           
            
        }

        //复制产品
        protected void btnCopy_Click(object sender, EventArgs e)
        {
            string strProductID = Config.Request(Request.Form["ProductID"], "0");
            if (strProductID != "0")
            {
                Response.Redirect("Product_Copy.aspx?ProductID=" + strProductID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
            
        }

    }
}
