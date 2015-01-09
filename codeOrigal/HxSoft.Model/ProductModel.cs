using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///产品管理-实体类
    /// 创建人:杨小明
    /// 日期:2011-4-27
    /// </summary>
    [Serializable]
    public class ProductModel
    {
        private string _productid, _classid, _productname, _smallpic, _bigpic, _tags, _keywords, _description, _details, _clicknum, _listid, _isrecommend, _adminid, _addtime, _isclose;

        /// <summary>
        /// ProductID
        /// </summary>
        public string ProductID
        {
            get { return _productid; }
            set { _productid = value; }
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
        /// ProductName
        /// </summary>
        public string ProductName
        {
            get { return _productname; }
            set { _productname = value; }
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

