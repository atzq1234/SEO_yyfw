using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///文章管理-实体类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    [Serializable]
    public class ArticleModel
    {
        private string _articleid, _classid, _title, _author, _comefrom, _picture,_video,_tags, _keywords, _description, _details, _isrecommend, _clicknum,_listid, _adminid, _addtime, _isclose;

        /// <summary>
        /// ArticleID
        /// </summary>
        public string ArticleID
        {
            get { return _articleid; }
            set { _articleid = value; }
        }
        /// <summary>
        /// ClassID
        /// </summary>
        public string ClassID
        {
            get { return _classid; }
            set { _classid = value; }
        }
        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// Author
        /// </summary>
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }
        /// <summary>
        /// ComeFrom
        /// </summary>
        public string ComeFrom
        {
            get { return _comefrom; }
            set { _comefrom = value; }
        }
        /// <summary>
        /// Picture
        /// </summary>
        public string Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }
        /// <summary>
        /// Video
        /// </summary>
        public string Video
        {
            get { return _video; }
            set { _video = value; }
        }
        /// <summary>
        /// Tags
        /// </summary>
        public string Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }
        /// <summary>
        /// Keywords
        /// </summary>
        public string Keywords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }
        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// Details
        /// </summary>
        public string Details
        {
            get { return _details; }
            set { _details = value; }
        }
        /// <summary>
        /// IsRecommend
        /// </summary>
        public string IsRecommend
        {
            get { return _isrecommend; }
            set { _isrecommend = value; }
        }
        /// <summary>
        /// ClickNum
        /// </summary>
        public string ClickNum
        {
            get { return _clicknum; }
            set { _clicknum = value; }
        }
        /// <summary>
        /// ListID
        /// </summary>
        public string ListID
        {
            get { return _listid; }
            set { _listid = value; }
        }
        /// <summary>
        /// AdminID
        /// </summary>
        public string AdminID
        {
            get { return _adminid; }
            set { _adminid = value; }
        }
        /// <summary>
        /// AddTime
        /// </summary>
        public string AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        /// <summary>
        /// IsClose
        /// </summary>
        public string IsClose
        {
            get { return _isclose; }
            set { _isclose = value; }
        }
    }
}

