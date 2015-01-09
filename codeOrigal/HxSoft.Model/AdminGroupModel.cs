using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///管理组管理-实体类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    [Serializable]
    public class AdminGroupModel
    {
        private string _admingroupid, _admingroupname, _limitvalues, _listid, _adminid, _addtime, _isclose;

        /// <summary>
        /// AdminGroupID
        /// </summary>
        public string AdminGroupID
        {
            get { return _admingroupid; }
            set { _admingroupid = value; }
        }
        /// <summary>
        /// AdminGroupName
        /// </summary>
        public string AdminGroupName
        {
            get { return _admingroupname; }
            set { _admingroupname = value; }
        }
        /// <summary>
        /// LimitValues
        /// </summary>
        public string LimitValues
        {
            get { return _limitvalues; }
            set { _limitvalues = value; }
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
