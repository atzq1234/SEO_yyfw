using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///水印配置管理-实体类
    /// 创建人:Admin
    /// 日期:2012-10-13
    /// </summary>
    [Serializable]
    public class SetModel
    {
        private string _setid, _watertypeid,_fontsize, _watertext, _font, _fontcolor, _waterpic, _waterposition, _isarticlethumb, _articlethumbwidth, _articlethumbheight, _isproductthumb, _productthumbwidth, _productthumbheight, _isphotothumb, _photothumbwidth, _photothumbheight;

        /// <summary>
        /// SetID
        /// </summary>
        public string SetID
        {
            get { return _setid; }
            set { _setid = value; }
        }
        /// <summary>
        /// WaterTypeID
        /// </summary>
        public string WaterTypeID
        {
            get { return _watertypeid; }
            set { _watertypeid = value; }
        }
        /// <summary>
        /// WaterText
        /// </summary>
        public string WaterText
        {
            get { return _watertext; }
            set { _watertext = value; }
        }
        /// <summary>
        /// Font
        /// </summary>
        public string Font
        {
            get { return _font; }
            set { _font = value; }
        }
        /// <summary>
        /// FontSize
        /// </summary>
        public string FontSize
        {
            get { return _fontsize; }
            set { _fontsize = value; }
        }
        /// <summary>
        /// FontColor
        /// </summary>
        public string FontColor
        {
            get { return _fontcolor; }
            set { _fontcolor = value; }
        }
        /// <summary>
        /// WaterPic
        /// </summary>
        public string WaterPic
        {
            get { return _waterpic; }
            set { _waterpic = value; }
        }
        /// <summary>
        /// WaterPosition
        /// </summary>
        public string WaterPosition
        {
            get { return _waterposition; }
            set { _waterposition = value; }
        }
        /// <summary>
        /// IsArticleThumb
        /// </summary>
        public string IsArticleThumb
        {
            get { return _isarticlethumb; }
            set { _isarticlethumb = value; }
        }
        /// <summary>
        /// ArticleThumbWidth
        /// </summary>
        public string ArticleThumbWidth
        {
            get { return _articlethumbwidth; }
            set { _articlethumbwidth = value; }
        }
        /// <summary>
        /// ArticleThumbHeight
        /// </summary>
        public string ArticleThumbHeight
        {
            get { return _articlethumbheight; }
            set { _articlethumbheight = value; }
        }
        /// <summary>
        /// IsProductThumb
        /// </summary>
        public string IsProductThumb
        {
            get { return _isproductthumb; }
            set { _isproductthumb = value; }
        }
        /// <summary>
        /// ProductThumbWidth
        /// </summary>
        public string ProductThumbWidth
        {
            get { return _productthumbwidth; }
            set { _productthumbwidth = value; }
        }
        /// <summary>
        /// ProductThumbHeight
        /// </summary>
        public string ProductThumbHeight
        {
            get { return _productthumbheight; }
            set { _productthumbheight = value; }
        }
        /// <summary>
        /// IsPhotoThumb
        /// </summary>
        public string IsPhotoThumb
        {
            get { return _isphotothumb; }
            set { _isphotothumb = value; }
        }
        /// <summary>
        /// PhotoThumbWidth
        /// </summary>
        public string PhotoThumbWidth
        {
            get { return _photothumbwidth; }
            set { _photothumbwidth = value; }
        }
        /// <summary>
        /// PhotoThumbHeight
        /// </summary>
        public string PhotoThumbHeight
        {
            get { return _photothumbheight; }
            set { _photothumbheight = value; }
        }
    }
}

