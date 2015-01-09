using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///文章图片-实体类
    /// 创建人:杨小明
    /// 日期:2012-1-19
    /// </summary>
    [Serializable]
    public class ArticlePicModel
    {
        private string _articlepicid, _articleid, _title, _smallpic, _bigpic, _description, _listid, _adminid, _addtime, _isclose;

        /// <summary>
        /// ArticlePicID
        /// </summary>
        public string ArticlePicID
        {
            get { return _articlepicid; }
            set { _articlepicid = value; }
        }
        /// <summary>
        /// ArticleID
        /// </summary>
        public string ArticleID
        {
            get { return _articleid; }
            set { _articleid = value; }
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
        /// SmallPic
        /// </summary>
        public string SmallPic
        {
            get { return _smallpic; }
            set { _smallpic = value; }
        }
        /// <summary>
        /// BigPic
        /// </summary>
        public string BigPic
        {
            get { return _bigpic; }
            set { _bigpic = value; }
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

