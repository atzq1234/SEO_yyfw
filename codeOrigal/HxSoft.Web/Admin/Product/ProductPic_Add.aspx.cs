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
using System.IO;

namespace HxSoft.Web.Admin.Product
{
    public partial class ProductPic_Add : System.Web.UI.Page
    {
        /// <summary>
        ///产品图片
        /// 创建人:杨小明
        /// 日期:2012-1-19
        /// </summary>
        //定义全局变量
        public string ProductPicID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ProductPicID"], 0).ToString();
            }
        }
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        public int ProductPage
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ProductPage"], 1);
            }
        }
        public string ProductID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ProductID"], 0).ToString();
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
            if (!Page.IsPostBack)
            {
                SetModel setModel = Factory.Set().GetInfo();
                if (setModel != null)
                {
                    IsThumb.Visible = setModel.IsProductThumb != "1";
                }
                if (ProductPicID == "0")
                {
                    GetData.LimitChkMsg("ProductPicAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.ProductPic().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("ProductPicEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ProductPicModel proPicModel = new ProductPicModel();
            string strOldListID = hidlistID.Value;
            proPicModel.ProductID = ProductID;
            proPicModel.Title = txtTitle.Text.Trim();
            proPicModel.SmallPic = txtSmallPic.Text.Trim();
            proPicModel.BigPic = txtBigPic.Text.Trim();
            SetModel seModel = Factory.Set().GetInfo();
            if (seModel != null)
            {
                if (seModel.IsProductThumb == "1" && File.Exists(Server.MapPath(txtBigPic.Text)))
                {
                    string strFileName = Path.GetFileNameWithoutExtension(Server.MapPath(txtBigPic.Text)).ToLower();
                    string strFileExt = Path.GetExtension(Server.MapPath(txtBigPic.Text)).ToLower();
                    string strThumbPath = Config.FileUploadPath + "Product/thumb/";
                    Config.CheckFolder(Server.MapPath(strThumbPath));
                    string strSmallPath = strThumbPath + strFileName + strFileExt;
                    if (Config.IsPicture2(strFileExt))
                    {
                        WaterImage.MakeThumbnail(Server.MapPath(txtBigPic.Text.Trim()), Server.MapPath(strSmallPath), int.Parse(seModel.ProductThumbWidth), int.Parse(seModel.ProductThumbHeight), "Cut");
                        proPicModel.SmallPic = strSmallPath;
                        PathModel pathModel = new PathModel();
                        pathModel.Path = strSmallPath;
                        pathModel.AdminID = Session["AdminID"].ToString();
                        Factory.Path().InsertInfo(pathModel);
                    }
                }
            }
            proPicModel.Description = Config.HTMLCls(txtDescription.Text.Trim());
            proPicModel.ListID = txtListID.Text.Trim();
            proPicModel.AdminID = Session["AdminID"].ToString();
            proPicModel.AddTime = DateTime.Now.ToString();
            proPicModel.IsClose = radIsClose.SelectedValue;
            if (ProductPicID == "0")
            {
                Factory.ProductPic().OrderInfo(proPicModel.ListID, strOldListID);
                Factory.ProductPic().InsertInfo(proPicModel);
                Factory.AdminLog().InsertLog("添加名称为" + proPicModel.Title + "的产品图片!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "ProductPic.aspx?ProductID=" + ProductID + "&ProductPage=" + ProductPage);
            }
            else
            {
                ProductPicModel proPicModel_2 = new ProductPicModel();
                proPicModel_2 = Factory.ProductPic().GetInfo(ProductPicID);
                if (proPicModel_2 != null)
                {
                    if (GetData.CheckAdminID(proPicModel_2.AdminID, "ProductPicAll"))//检查创建者
                    {
                        Factory.ProductPic().OrderInfo(proPicModel.ListID, strOldListID);
                        Factory.ProductPic().UpdateInfo(proPicModel, ProductPicID);
                        Factory.AdminLog().InsertLog("修改编号为" + ProductPicID + "的产品图片!", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "ProductPic.aspx?ProductID=" + ProductID + "&ProductPage=" + ProductPage + "&" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            ProductPicModel proPicModel = new ProductPicModel();
            proPicModel = Factory.ProductPic().GetInfo(ProductPicID);
            if (proPicModel != null)
            {
                if (GetData.CheckAdminID(proPicModel.AdminID, "ProductPicAll"))//检查创建者
                {
                    //txtProductID.Text = proPicModel.ProductID;
                    txtTitle.Text = proPicModel.Title;
                    txtSmallPic.Text = proPicModel.SmallPic;
                    txtBigPic.Text = proPicModel.BigPic;
                    txtDescription.Text = Config.HTMLToTextarea(proPicModel.Description);
                    txtListID.Text = proPicModel.ListID;
                    hidlistID.Value = proPicModel.ListID;
                    //txtAdminID.Text = proPicModel.AdminID;
                    //txtAddTime.Text = proPicModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, proPicModel.IsClose);
                }
                else
                {
                    Config.ShowEnd("您没有查看此信息的权限！");
                }
            }
            else
            {
                Config.ShowEnd("您没有查看此信息的权限！");
            }
        }

    }
}
