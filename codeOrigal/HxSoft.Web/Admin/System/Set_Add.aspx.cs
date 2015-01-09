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

namespace HxSoft.Web.Admin._System
{
    public partial class Set_Add : System.Web.UI.Page
    {
        /// <summary>
        ///系统配置管理
        /// 创建人:Admin
        /// 日期:2012-10-13
        /// </summary>
        //定义全局变量
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("Set");
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SetModel seModel = new SetModel();

            seModel.WaterTypeID = radWaterState.SelectedValue;
            seModel.WaterText = txtWaterText.Text.Trim();
            seModel.Font = drFontStyle.SelectedValue;
            seModel.FontSize = drFontSize.SelectedValue;
            seModel.FontColor = drFontColor.SelectedValue;
            seModel.WaterPic = txtWaterPic.Text.Trim();
            seModel.WaterPosition = drPicPosition.SelectedValue;
            seModel.IsArticleThumb = radArticleThumbState.SelectedValue;
            seModel.ArticleThumbWidth = txtArticleThumbWidth.Text.Trim();
            seModel.ArticleThumbHeight = txtArticleThumbHeight.Text.Trim();
            seModel.IsProductThumb = radProductThumbState.SelectedValue;
            seModel.ProductThumbWidth = txtProductThumbWidth.Text.Trim();
            seModel.ProductThumbHeight = txtProductThumbHeight.Text.Trim();
            seModel.IsPhotoThumb = radPhotoThumbState.SelectedValue;
            seModel.PhotoThumbWidth = txtPhotoThumbWidth.Text.Trim();
            seModel.PhotoThumbHeight = txtPhotoThumbHeight.Text.Trim();

            SetModel seModel_1 = new SetModel();
            seModel_1 = Factory.Set().GetInfo();
            if (seModel_1 == null)
            {
                Factory.Set().InsertInfo(seModel);
                Factory.AdminLog().InsertLog("添加系统配置。", Session["AdminID"].ToString());
                Config.MsgGotoUrl("保存成功！", "Set_Add.aspx");
            }
            else
            {
                Factory.Set().UpdateInfo(seModel);
                Factory.AdminLog().InsertLog("修改系统配置。", Session["AdminID"].ToString());
                Config.MsgGotoUrl("保存成功！", "Set_Add.aspx");
            }
        }

        //显示数据
        protected void ShowInfo()
        {
            SetModel seModel = new SetModel();
            seModel = Factory.Set().GetInfo();
            if (seModel != null)
            {
                radWaterState.ClearSelection();
                Config.setDefaultSelected(radWaterState, seModel.WaterTypeID);//水印状态
                txtWaterText.Text = seModel.WaterText;
                Config.setDefaultSelected(drFontStyle, seModel.Font); //字体样式
                Config.setDefaultSelected(drFontSize, seModel.FontSize.ToString());//字体大小
                Config.setDefaultSelected(drFontColor, seModel.FontColor);//字体大小
                txtWaterPic.Text = seModel.WaterPic;
                Config.setDefaultSelected(drPicPosition, seModel.WaterPosition);//图片位置
                radArticleThumbState.ClearSelection();
                Config.setDefaultSelected(radArticleThumbState, seModel.IsArticleThumb);//文章缩略图状态
                txtArticleThumbWidth.Text = seModel.ArticleThumbWidth;
                txtArticleThumbHeight.Text = seModel.ArticleThumbHeight;
                radProductThumbState.ClearSelection();
                Config.setDefaultSelected(radProductThumbState, seModel.IsProductThumb);//产品缩略图状态
                txtProductThumbWidth.Text = seModel.ProductThumbWidth;
                txtProductThumbHeight.Text = seModel.ProductThumbHeight;
                radPhotoThumbState.ClearSelection();
                Config.setDefaultSelected(radPhotoThumbState, seModel.IsPhotoThumb);//相册缩略图状态
                txtPhotoThumbWidth.Text = seModel.PhotoThumbWidth;
                txtPhotoThumbHeight.Text = seModel.PhotoThumbHeight;
            }
        }


    }
}


