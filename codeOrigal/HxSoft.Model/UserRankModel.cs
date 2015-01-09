using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///用户级别-实体类
    /// 创建人:
    /// 日期:2010-12-7
    /// </summary>
    [Serializable]
    public class UserRankModel
    {
        private string _userrankid, _userrankname, _limitvalues, _listid, _adminid, _addtime, _isclose;

        /// <summary>
        /// UserRankID
        /// </summary>
        public string UserRankID
        {
            get { return _userrankid; }
            set { _userrankid = value; }
        }
        /// <summary>
        /// UserRankName
        /// </summary>
        public string UserRankName
        {
            get { return _userrankname; }
            set { _userrankname = value; }
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

