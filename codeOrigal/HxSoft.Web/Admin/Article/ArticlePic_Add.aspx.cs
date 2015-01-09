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
using System.Collections.Generic;
using System.Data.Common;
using System.IO;

namespace HxSoft.Web.Admin.Article
{
    public partial class ArticlePic_Add : System.Web.UI.Page
    {
        /// <summary>
        ///图片/视频
        /// 创建人:杨小明
        /// 日期:2012-1-19
        /// </summary>
        //定义全局变量
        public string ArticlePicID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ArticlePicID"], 0).ToString();
            }
        }
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        public int ArticlePage
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ArticlePage"], 1);
            }
        }
        public string ArticleID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ArticleID"], 0).ToString();
            }
        }
        #region ****排序参数****
        public string strOrderKey
        {
            get
            {
                return Config.Request(Request["OrderKey"], "ArticlePicID");
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
                    IsThumb.Visible = setModel.IsArticleThumb != "1";
                }
                if (ArticlePicID == "0")
                {
                    GetData.LimitChkMsg("ArticlePicAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.ArticlePic().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("ArticlePicEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ArticlePicModel artPicModel = new ArticlePicModel();
            string strOldListID = hidlistID.Value;
            artPicModel.ArticleID = ArticleID;
            artPicModel.Title = txtTitle.Text.Trim();
            artPicModel.SmallPic = txtSmallPic.Text.Trim();
            artPicModel.BigPic = txtBigPic.Text.Trim();
            SetModel seModel = Factory.Set().GetInfo();
            if (seModel != null)
            {
                if (seModel.IsArticleThumb == "1" && File.Exists(Server.MapPath(txtBigPic.Text)))
                {
                    string strFileName = Path.GetFileNameWithoutExtension(Server.MapPath(txtBigPic.Text)).ToLower();
                    string strFileExt = Path.GetExtension(Server.MapPath(txtBigPic.Text)).ToLower();
                    string strThumbPath = Config.FileUploadPath + "Article/thumb/";
                    Config.CheckFolder(Server.MapPath(strThumbPath));
                    string strSmallPath = strThumbPath + strFileName + strFileExt;
                    if (Config.IsPicture2(strFileExt))
                    {
                        WaterImage.MakeThumbnail(Server.MapPath(txtBigPic.Text.Trim()), Server.MapPath(strSmallPath), int.Parse(seModel.ArticleThumbWidth), int.Parse(seModel.ArticleThumbHeight), "Cut");
                        artPicModel.SmallPic = strSmallPath;

                        PathModel pathModel = new PathModel();
                        pathModel.Path = strSmallPath;
                        pathModel.AdminID = Session["AdminID"].ToString();
                        Factory.Path().InsertInfo(pathModel);
                    }
                }
            }
            artPicModel.Description = Config.HTMLCls(txtDescription.Text.Trim());
            artPicModel.ListID = txtListID.Text.Trim();
            artPicModel.AdminID = Session["AdminID"].ToString();
            artPicModel.AddTime = DateTime.Now.ToString();
            artPicModel.IsClose = radIsClose.SelectedValue;
            if (ArticlePicID == "0")
            {
                Factory.ArticlePic().OrderInfo(artPicModel.ListID, strOldListID);
                Factory.ArticlePic().InsertInfo(artPicModel);
                Factory.AdminLog().InsertLog("添加名称为" + artPicModel.Title + "的图片!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "ArticlePic.aspx?ArticleID=" + ArticleID + "&ArticlePage=" + ArticlePage);
            }
            else
            {
                ArticlePicModel artPicModel_2 = new ArticlePicModel();
                artPicModel_2 = Factory.ArticlePic().GetInfo(ArticlePicID);
                if (artPicModel_2 != null)
                {
                    if (GetData.CheckAdminID(artPicModel_2.AdminID, "ArticlePicAll"))//检查创建者
                    {
                        Factory.ArticlePic().OrderInfo(artPicModel.ListID, strOldListID);
                        Factory.ArticlePic().UpdateInfo(artPicModel, ArticlePicID);
                        Factory.AdminLog().InsertLog("修改编号为" + ArticlePicID + "的图片!", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "ArticlePic.aspx?ArticleID=" + ArticleID + "&ArticlePage=" + ArticlePage + "&" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            ArticlePicModel artPicModel = new ArticlePicModel();
            artPicModel = Factory.ArticlePic().GetInfo(ArticlePicID);
            if (artPicModel != null)
            {
                if (GetData.CheckAdminID(artPicModel.AdminID, "ArticlePicAll"))//检查创建者
                {
                    //txtArticleID.Text = artPicModel.ArticleID;
                    txtTitle.Text = artPicModel.Title;
                    txtSmallPic.Text = artPicModel.SmallPic;
                    txtBigPic.Text = artPicModel.BigPic;
                    txtDescription.Text = Config.HTMLToTextarea(artPicModel.Description);
                    txtListID.Text = artPicModel.ListID;
                    hidlistID.Value = artPicModel.ListID;
                    //txtAdminID.Text = artPicModel.AdminID;
                    //txtAddTime.Text = artPicModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, artPicModel.IsClose);
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
