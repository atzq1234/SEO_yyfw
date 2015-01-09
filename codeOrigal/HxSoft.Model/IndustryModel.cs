using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///行业管理-实体类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    [Serializable]
    public class IndustryModel
    {
        private string _classid, _industryname, _parentid, _childnum, _listid, _adminid, _addtime, _isclose;

        /// <summary>
        /// IndustryID
        /// </summary>
        public string IndustryID
        {
            get { return _classid; }
            set { _classid = value; }
        }
        /// <summary>
        /// IndustryName
        /// </summary>
        public string IndustryName
        {
            get { return _industryname; }
            set { _industryname = value; }
        }
        /// <summary>
        /// ParentID
        /// </summary>
        public string ParentID
        {
            get { return _parentid; }
            set { _parentid = value; }
        }
        /// <summary>
        /// ChildNum
        /// </summary>
        public string ChildNum
        {
            get { return _childnum; }
            set { _childnum = value; }
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
