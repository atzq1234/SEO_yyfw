using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///权限字段-实体类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    [Serializable]
    public class LimitModel
    {
        private string _limitid, _limitfield, _limitvalue, _parentid, _childnum, _listid,_isdist, _adminid, _addtime, _isclose;

        /// <summary>
        /// LimitID
        /// </summary>
        public string LimitID
        {
            get { return _limitid; }
            set { _limitid = value; }
        }
        /// <summary>
        /// LimitField
        /// </summary>
        public string LimitField
        {
            get { return _limitfield; }
            set { _limitfield = value; }
        }
        /// <summary>
        /// LimitValue
        /// </summary>
        public string LimitValue
        {
            get { return _limitvalue; }
            set { _limitvalue = value; }
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
        /// IsDist
        /// </summary>
        public string IsDist
        {
            get { return _isdist; }
            set { _isdist = value; }
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
