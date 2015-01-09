using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///下载管理-实体类
    /// 创建人:杨小明
    /// 日期:2011-2-24
    /// </summary>
    [Serializable]
    public class DownloadModel
    {
        private string _downid, _classid, _downname,_downpic, _downurl, _downsize, _tags, _keywords, _description, _details, _clicknum, _listid, _isrecommend, _adminid, _addtime, _isclose;

        /// <summary>
        /// DownloadID
        /// </summary>
        public string DownloadID
        {
            get { return _downid; }
            set { _downid = value; }
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
        /// DownName
        /// </summary>
        public string DownName
        {
            get { return _downname; }
            set { _downname = value; }
        }
        /// <summary>
        /// DownPic
        /// </summary>
        public string DownPic
        {
            get { return _downpic; }
            set { _downpic = value; }
        }
        /// <summary>
        /// DownUrl
        /// </summary>
        public string DownUrl
        {
            get { return _downurl; }
            set { _downurl = value; }
        }
        /// <summary>
        /// DownSize
        /// </summary>
        public string DownSize
        {
            get { return _downsize; }
            set { _downsize = value; }
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
        /// IsRecommend
        /// </summary>
        public string IsRecommend
        {
            get { return _isrecommend; }
            set { _isrecommend = value; }
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

