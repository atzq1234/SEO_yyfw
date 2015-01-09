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
using System.IO;
using System.Collections.Generic;
using System.Data.Common;

namespace HxSoft.Web.Admin.Article
{
    public partial class Article_Add : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string ArticleID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ArticleID"], 0).ToString();
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
                return Config.Request(Request["OrderKey"], "ArticleID");
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
                if (strTitle != "") TempSql.Append(" and Title like @Title");
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
                if (strTitle != "") listParams.Add(Config.Conn().CreateDbParameter("@Title", "%" + strTitle + "%"));
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
                TempUrl.Append("txtTitle=" + Server.UrlEncode(strTitle) + "&");
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
                    IsThumb.Visible = setModel.IsArticleThumb != "1";
                }
                //站点列表
                GetData.GetConfigList(drpConfigID, "LanguageVer", "ConfigID");
                this.drpConfigID.Items.Insert(0, new ListItem("请选择", "-1"));

                //文章分类
                //Factory.Class().ShowSelectTree("0", drpClassID, "", Config.SysArticleMouldID);
                drpClassID.Items.Insert(0, new ListItem("请选择", "-1"));

                //日期
                Config.DropDownList_Year(drpYear, 2000);
                Config.DropDownList_Month(drpMonth);
                Config.DropDownList_Day(drpDay);
                Config.DropDownList_Hour(drpHour);
                Config.DropDownList_Minute(drpMinute);
                Config.DropDownList_Second(drpSecond);

                if (ArticleID == "0")
                {
                    GetData.LimitChkMsg("ArticleAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.Article().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;

                    drpYear.ClearSelection();
                    Config.setDefaultSelected(drpYear, DateTime.Now.Year.ToString());
                    drpMonth.ClearSelection();
                    Config.setDefaultSelected(drpMonth, DateTime.Now.Month.ToString());
                    drpDay.ClearSelection();
                    Config.setDefaultSelected(drpDay, DateTime.Now.Day.ToString());
                    drpHour.ClearSelection();
                    Config.setDefaultSelected(drpHour, DateTime.Now.Hour.ToString());
                    drpMinute.ClearSelection();
                    Config.setDefaultSelected(drpMinute, Config.ShowZero(DateTime.Now.Minute).ToString());
                    drpSecond.ClearSelection();
                    Config.setDefaultSelected(drpSecond, Config.ShowZero(DateTime.Now.Second).ToString());
                }
                else
                {
                    GetData.LimitChkMsg("ArticleEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ArticleModel artModel = new ArticleModel();
            string strOldListID = hidlistID.Value;
            artModel.ClassID = drpClassID.SelectedValue;
            artModel.Title = txtTitle.Text.Trim();
            artModel.Author = txtAuthor.Text.Trim();
            artModel.ComeFrom = txtComeFrom.Text.Trim();
            artModel.Picture = txtPicture.Text.Trim();
            artModel.Video = txtVideo.Text.Trim();
            SetModel seModel = Factory.Set().GetInfo();
            if (seModel != null)
            {
                if (seModel.IsArticleThumb == "1" && File.Exists(Server.MapPath(txtVideo.Text)))
                {
                    string strFileName = Path.GetFileNameWithoutExtension(Server.MapPath(txtVideo.Text)).ToLower();
                    string strFileExt = Path.GetExtension(Server.MapPath(txtVideo.Text)).ToLower();
                    string strThumbPath = Config.FileUploadPath + "Article/thumb/";
                    Config.CheckFolder(Server.MapPath(strThumbPath));
                    string strSmallPath = strThumbPath + strFileName + strFileExt;
                    if (Config.IsPicture2(strFileExt))
                    {
                        WaterImage.MakeThumbnail(Server.MapPath(txtVideo.Text.Trim()), Server.MapPath(strSmallPath), int.Parse(seModel.ArticleThumbWidth), int.Parse(seModel.ArticleThumbHeight), "Cut");
                        artModel.Picture = strSmallPath;

                        PathModel pathModel = new PathModel();
                        pathModel.Path = strSmallPath;
                        pathModel.AdminID = Session["AdminID"].ToString();
                        Factory.Path().InsertInfo(pathModel);
                    }
                }
            }
            artModel.Tags = txtTags.Text.Trim();
            artModel.Keywords = txtKeywords.Text.Trim();
            artModel.Description = Config.HTMLCls(txtDescription.Text.Trim());
            artModel.Details = txtDetails.Value;
            if (chkIsRecommend.Checked)
            {
                artModel.IsRecommend = "1";
            }
            else
            {
                artModel.IsRecommend = "0";
            }
            artModel.ListID = txtListID.Text.Trim();
            artModel.ClickNum = txtClickNum.Text.Trim();
            artModel.AdminID = Session["AdminID"].ToString();
            artModel.AddTime = Convert.ToDateTime(drpYear.SelectedValue + "-" + drpMonth.SelectedValue + "-" + drpDay.SelectedValue + " " + drpHour.SelectedValue + ":" + drpMinute.SelectedValue + ":" + drpSecond.SelectedValue).ToString();
            artModel.IsClose = radIsClose.SelectedValue;
            if (ArticleID == "0")
            {
                Factory.Article().OrderInfo(artModel.ListID, strOldListID);
                Factory.Article().InsertInfo(artModel);
                Factory.AdminLog().InsertLog("添加名称为\"" + artModel.Title + "\"的文章。", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "Article.aspx");
            }
            else
            {
                ArticleModel artModel_2 = new ArticleModel();
                artModel_2 = Factory.Article().GetInfo(ArticleID);
                if (artModel_2 != null)
                {
                    if (GetData.CheckAdminID(artModel_2.AdminID, "ArticleAll"))//检查创建者
                    {
                        Factory.Article().OrderInfo(artModel.ListID, strOldListID);
                        Factory.Article().UpdateInfo(artModel, ArticleID);
                        Factory.AdminLog().InsertLog("修改编号为" + ArticleID + "的文章。", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "Article.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            ArticleModel artModel = new ArticleModel();
            artModel = Factory.Article().GetInfo(ArticleID);
            if (artModel != null)
            {
                if (GetData.CheckAdminID(artModel.AdminID, "ArticleAll"))//检查创建者
                {
                    ClassModel claModel = Factory.Class().GetInfo(artModel.ClassID);
                    if (claModel != null)
                    {
                        Config.setDefaultSelected(drpConfigID, claModel.ConfigID);
                        Factory.Class().ShowSelectTree("0", drpClassID, " and ConfigID=" + claModel.ConfigID, Config.SysArticleMouldID);
                    }
                    Config.setDefaultSelected(drpClassID, artModel.ClassID);
                    txtTitle.Text = artModel.Title;
                    txtAuthor.Text = artModel.Author;
                    txtComeFrom.Text = artModel.ComeFrom;
                    txtPicture.Text = artModel.Picture;
                    txtVideo.Text = artModel.Video;
                    if (txtVideo.Text.Trim() != "")
                    {
                        if (!File.Exists(Server.MapPath(txtVideo.Text.Trim())))
                        {
                            Err.Visible = true;
                        }
                    }
                    txtTags.Text = artModel.Tags;
                    txtKeywords.Text = artModel.Keywords;
                    txtDescription.Text = Config.HTMLToTextarea(artModel.Description);
                    txtDetails.Value = artModel.Details;
                    if (artModel.IsRecommend == "1")
                    {
                        chkIsRecommend.Checked = true;
                    }
                    txtClickNum.Text = artModel.ClickNum;
                    txtListID.Text = artModel.ListID;
                    hidlistID.Value = artModel.ListID;
                    //txtAdminID.Text = artModel.AdminID;
                    DateTime tempDate = Convert.ToDateTime(artModel.AddTime);
                    drpYear.ClearSelection();
                    Config.setDefaultSelected(drpYear, tempDate.Year.ToString());

                    drpMonth.ClearSelection();
                    Config.setDefaultSelected(drpMonth, tempDate.Month.ToString());

                    drpDay.ClearSelection();
                    Config.setDefaultSelected(drpDay, tempDate.Day.ToString());

                    drpHour.ClearSelection();
                    Config.setDefaultSelected(drpHour, tempDate.Hour.ToString());

                    drpMinute.ClearSelection();
                    Config.setDefaultSelected(drpMinute, Config.ShowZero(tempDate.Minute).ToString());

                    drpSecond.ClearSelection();
                    Config.setDefaultSelected(drpSecond, Config.ShowZero(tempDate.Second).ToString());
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, artModel.IsClose);
                }
                else
                {
                    Config.ShowEnd("您没有查看此文章的权限");
                }
            }
            else
            {
                Config.ShowEnd("您没有查看此文章的权限！");
            }
        }

        //栏目分类
        protected void drpConfigID_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpClassID.Items.Clear();
            Factory.Class().ShowSelectTree("0", drpClassID, " and ConfigID=" + drpConfigID.SelectedValue, Config.SysArticleMouldID);
            drpClassID.Items.Insert(0, new ListItem("请选择", "-1"));
        }
    }
}
