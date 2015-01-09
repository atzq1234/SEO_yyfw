using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///管理员管理-实体类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    [Serializable]
    public class AdminModel
    {
        private string _adminid, _adminname, _adminpass,  _realname, _email, _department, _comment, _loginnum, _lastlogintime, _thislogintime, _manageadminid, _addtime, _isclose;

        /// <summary>
        /// AdminID
        /// </summary>
        public string AdminID
        {
            get { return _adminid; }
            set { _adminid = value; }
        }
        /// <summary>
        /// AdminName
        /// </summary>
        public string AdminName
        {
            get { return _adminname; }
            set { _adminname = value; }
        }
        /// <summary>
        /// AdminPass
        /// </summary>
        public string AdminPass
        {
            get { return _adminpass; }
            set { _adminpass = value; }
        }
        /// <summary>
        /// RealName
        /// </summary>
        public string RealName
        {
            get { return _realname; }
            set { _realname = value; }
        }
        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        /// <summary>
        /// Department
        /// </summary>
        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }
        /// <summary>
        /// Comment
        /// </summary>
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }
        /// <summary>
        /// LoginNum
        /// </summary>
        public string LoginNum
        {
            get { return _loginnum; }
            set { _loginnum = value; }
        }
        /// <summary>
        /// LastLoginTime
        /// </summary>
        public string LastLoginTime
        {
            get { return _lastlogintime; }
            set { _lastlogintime = value; }
        }
        /// <summary>
        /// ThisLoginTime
        /// </summary>
        public string ThisLoginTime
        {
            get { return _thislogintime; }
            set { _thislogintime = value; }
        }
        /// <summary>
        /// ManageAdminID
        /// </summary>
        public string ManageAdminID
        {
            get { return _manageadminid; }
            set { _manageadminid = value; }
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
