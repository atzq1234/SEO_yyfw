﻿using System;
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
    public partial class Product_Copy : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string ProductID
        {
            get
            {
                return Config.Request(Request.QueryString["ProductID"], "0");
            }
        }
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
            GetData.LimitChkMsg("ProductCopy");
            if (!Page.IsPostBack)
            {
                //产品分类
                Factory.Class().ShowSelectTree("0", drpClassID, "", Config.SysProductMouldID);
                drpClassID.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strClassID = drpClassID.SelectedValue;
            string[] arrProductID = ProductID.Split(new char[] { ',' });
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
                        proModel.ClassID = strClassID;
                        proModel.ListID = Factory.Product().GetListID();
                        proModel.ClickNum = "0";
                        proModel.IsRecommend = "0";
                        proModel.AdminID = Session["AdminID"].ToString();
                        proModel.AddTime = DateTime.Now.ToString();
                        proModel.IsClose = "0";
                        Factory.Product().InsertInfo(proModel);
                        strTempProductID.Append(arrProductID[i]);
                        if (i + 1 < arrProductID.Length) strTempProductID.Append(",");
                        n++;
                    }
                }
            }
            if (n > 0)
            {
                Factory.AdminLog().InsertLog("复制编号为" + strTempProductID.ToString() + "的产品!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("编号为" + strTempProductID.ToString() + "的产品复制成功!", "Product.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
            else
            {
                Config.MsgGoBack("操作失败!");
            }
        }
    }
}
