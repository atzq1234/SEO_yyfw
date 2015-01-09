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
using System.IO;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.Admin.Photo
{
    public partial class Photo_Add : System.Web.UI.Page
    {
        /// <summary>
        ///相册管理
        /// 创建人:
        /// 日期:2012-9-20
        /// </summary>
        //定义全局变量
        public string PhotoID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["PhotoID"], 0).ToString();
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
            if (!Page.IsPostBack)
            {
                SetModel setModel = Factory.Set().GetInfo();
                if (setModel != null)
                {
                    IsThumb.Visible = setModel.IsPhotoThumb != "1";
                }
                //站点列表
                GetData.GetConfigList(drpConfigID, "LanguageVer", "ConfigID");
                this.drpConfigID.Items.Insert(0, new ListItem("请选择", "-1"));

                //栏目分类
                //Factory.Class().ShowSelectTree("0", drpClassID, "", Config.SysPhotoMouldID);
                drpClassID.Items.Insert(0, new ListItem("请选择", "-1"));

                if (PhotoID == "0")
                {
                    GetData.LimitChkMsg("PhotoAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.Photo().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("PhotoEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            PhotoModel phoModel = new PhotoModel();
            string strOldListID = hidlistID.Value;
            phoModel.ClassID = drpClassID.SelectedValue;
            phoModel.Title = txtTitle.Text.Trim();
            phoModel.SmallPic = txtSmallPic.Text.Trim();
            phoModel.BigPic = txtBigPic.Text.Trim();
            SetModel seModel = Factory.Set().GetInfo();
            if (seModel != null)
            {
                if (seModel.IsPhotoThumb == "1" && File.Exists(Server.MapPath(txtBigPic.Text)))
                {
                    string strFileName = Path.GetFileNameWithoutExtension(Server.MapPath(txtBigPic.Text)).ToLower();
                    string strFileExt = Path.GetExtension(Server.MapPath(txtBigPic.Text)).ToLower();
                    string strThumbPath = Config.FileUploadPath + "Photo/thumb/";
                    Config.CheckFolder(Server.MapPath(strThumbPath));
                    string strSmallPath = strThumbPath + strFileName + strFileExt;
                    if (Config.IsPicture2(strFileExt))
                    {
                        WaterImage.MakeThumbnail(Server.MapPath(txtBigPic.Text.Trim()), Server.MapPath(strSmallPath), int.Parse(seModel.PhotoThumbWidth), int.Parse(seModel.PhotoThumbHeight), "Cut");
                        phoModel.SmallPic = strSmallPath;

                        PathModel pathModel = new PathModel();
                        pathModel.Path = strSmallPath;
                        pathModel.AdminID = Session["AdminID"].ToString();
                        Factory.Path().InsertInfo(pathModel);
                    }
                }
            }
            phoModel.Description = Config.HTMLCls(txtDescription.Text.Trim());
            phoModel.ListID = txtListID.Text.Trim();
            phoModel.AdminID = Session["AdminID"].ToString();
            phoModel.IsClose = radIsClose.SelectedValue;
            phoModel.AddTime = DateTime.Now.ToString();

            if (PhotoID == "0")
            {
                Factory.Photo().OrderInfo(phoModel.ListID, strOldListID);
                Factory.Photo().InsertInfo(phoModel);
                Factory.AdminLog().InsertLog("添加名称为" + phoModel.Title + "的相册!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "Photo.aspx");
            }
            else
            {
                PhotoModel phoModel_2 = new PhotoModel();
                phoModel_2 = Factory.Photo().GetInfo(PhotoID);
                if (phoModel_2 != null)
                {
                    if (GetData.CheckAdminID(phoModel_2.AdminID, "PhotoAll"))//检查创建者
                    {
                        Factory.Photo().OrderInfo(phoModel.ListID, strOldListID);
                        Factory.Photo().UpdateInfo(phoModel, PhotoID);
                        Factory.AdminLog().InsertLog("修改编号为" + PhotoID + "的相册!", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "Photo.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            PhotoModel phoModel = new PhotoModel();
            phoModel = Factory.Photo().GetInfo(PhotoID);
            if (phoModel != null)
            {
                if (GetData.CheckAdminID(phoModel.AdminID, "PhotoAll"))//检查创建者
                {
                    ClassModel claModel = Factory.Class().GetInfo(phoModel.ClassID);
                    if (claModel != null)
                    {
                        Config.setDefaultSelected(drpConfigID, claModel.ConfigID);
                        Factory.Class().ShowSelectTree("0", drpClassID, " and ConfigID=" + claModel.ConfigID, Config.SysPhotoMouldID);
                    }
                    Config.setDefaultSelected(drpClassID, phoModel.ClassID);
                    txtTitle.Text = phoModel.Title;
                    txtSmallPic.Text = phoModel.SmallPic;
                    txtBigPic.Text = phoModel.BigPic;
                    if (txtBigPic.Text.Trim() != "")
                    {
                        if (!File.Exists(Server.MapPath(txtBigPic.Text.Trim())))
                        {
                            Err.Visible = true;
                        }
                    }
                    txtDescription.Text = Config.HTMLToTextarea(phoModel.Description);
                    txtListID.Text = phoModel.ListID;
                    hidlistID.Value = phoModel.ListID;
                    Config.setDefaultSelected(radIsClose, phoModel.IsClose);
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
            Factory.Class().ShowSelectTree("0", drpClassID, " and ConfigID=" + drpConfigID.SelectedValue, Config.SysPhotoMouldID);
            drpClassID.Items.Insert(0, new ListItem("请选择", "-1"));
        }

    }
}
