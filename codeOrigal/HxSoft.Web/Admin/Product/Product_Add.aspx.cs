using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;
using System.IO;
using System.Data.Common;

namespace HxSoft.Web.Admin.Product
{
    public partial class Product_Add : System.Web.UI.Page
    {
        /// <summary>
        ///产品管理
        /// 创建人:杨小明
        /// 日期:2011-2-24
        /// </summary>
        //定义全局变量
        public string ProductID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ProductID"], 0).ToString();
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
            if (!Page.IsPostBack)
            {
                SetModel setModel = Factory.Set().GetInfo();
                if (setModel != null)
                {
                    IsThumb.Visible = setModel.IsProductThumb != "1";
                }
                //站点列表
                GetData.GetConfigList(drpConfigID, "LanguageVer", "ConfigID");
                this.drpConfigID.Items.Insert(0, new ListItem("请选择", "-1"));

                //产品分类
                //Factory.Class().ShowSelectTree("0", drpClassID, "", Config.SysProductMouldID);
                drpClassID.Items.Insert(0, new ListItem("请选择", "-1"));

                if (ProductID == "0")
                {
                    GetData.LimitChkMsg("ProductAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.Product().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("ProductEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ProductModel proModel = new ProductModel();
            string strOldListID = hidlistID.Value;
            proModel.ClassID = drpClassID.SelectedValue;
            proModel.ProductName = txtProductName.Text.Trim();
            proModel.SmallPic = txtSmallPic.Text.Trim();
            proModel.BigPic = txtBigPic.Text.Trim();
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
                        proModel.SmallPic = strSmallPath;
                        PathModel pathModel = new PathModel();
                        pathModel.Path = strSmallPath;
                        pathModel.AdminID = Session["AdminID"].ToString();
                        Factory.Path().InsertInfo(pathModel);
                    }
                }
            }
            proModel.Tags = txtTags.Text.Trim();
            proModel.Keywords = txtKeywords.Text.Trim();
            proModel.Description = Config.HTMLCls(txtDescription.Text.Trim());
            proModel.Details = txtDetails.Value;
            proModel.ClickNum = txtClickNum.Text;
            proModel.ListID = txtListID.Text.Trim();
            if (chkIsRecommend.Checked)
            {
                proModel.IsRecommend = "1";
            }
            else
            {
                proModel.IsRecommend = "0";
            }
            proModel.AdminID = Session["AdminID"].ToString();
            proModel.AddTime = DateTime.Now.ToString();
            proModel.IsClose = radIsClose.Text.Trim();
            if (ProductID == "0")
            {
                Factory.Product().OrderInfo(proModel.ListID, strOldListID);
                Factory.Product().InsertInfo(proModel);
                Factory.AdminLog().InsertLog("添加名称为\"" + proModel.ProductName + "\"的产品。", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "Product.aspx");

            }
            else
            {
                ProductModel proModel_2 = new ProductModel();
                proModel_2 = Factory.Product().GetInfo(ProductID);
                if (proModel_2 != null)
                {
                    if (GetData.CheckAdminID(proModel_2.AdminID, "ProductAll"))//检查创建者
                    {
                        Factory.Product().OrderInfo(proModel.ListID, strOldListID);
                        Factory.Product().UpdateInfo(proModel, ProductID);
                        Factory.AdminLog().InsertLog("修改编号为" + ProductID + "的产品。", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "Product.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            ProductModel proModel = new ProductModel();
            proModel = Factory.Product().GetInfo(ProductID);
            if (proModel != null)
            {
                if (GetData.CheckAdminID(proModel.AdminID, "ProductAll"))//检查创建者
                {
                    ClassModel claModel = Factory.Class().GetInfo(proModel.ClassID);
                    if (claModel != null)
                    {
                        Config.setDefaultSelected(drpConfigID, claModel.ConfigID);
                        Factory.Class().ShowSelectTree("0", drpClassID, " and ConfigID=" + claModel.ConfigID, Config.SysProductMouldID);
                    }
                    Config.setDefaultSelected(drpClassID, proModel.ClassID);
                    txtProductName.Text = proModel.ProductName;
                    txtSmallPic.Text = proModel.SmallPic;
                    txtBigPic.Text = proModel.BigPic;
                    if (txtBigPic.Text.Trim() != "")
                    {
                        if (!File.Exists(Server.MapPath(txtBigPic.Text.Trim())))
                        {
                            Err.Visible = true;
                        }
                    }
                    txtTags.Text = proModel.Tags;
                    txtKeywords.Text = proModel.Keywords;
                    txtDescription.Text = Config.HTMLToTextarea(proModel.Description);
                    txtDetails.Value = proModel.Details;
                    txtClickNum.Text = proModel.ClickNum;
                    txtListID.Text = proModel.ListID;
                    hidlistID.Value = proModel.ListID;
                    if (proModel.IsRecommend == "1")
                    {
                        chkIsRecommend.Checked = true;
                    }
                    //txtAdminID.Text = proModel.AdminID;
                    //txtAddTime.Text = proModel.AddTime;

                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, proModel.IsClose);
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

        //栏目分类
        protected void drpConfigID_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpClassID.Items.Clear();
            Factory.Class().ShowSelectTree("0", drpClassID, " and ConfigID=" + drpConfigID.SelectedValue, Config.SysProductMouldID);
            drpClassID.Items.Insert(0, new ListItem("请选择", "-1"));
        }

    }
}
