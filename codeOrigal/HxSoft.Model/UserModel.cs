using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///会员管理-实体类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    [Serializable]
    public class UserModel
    {
        private string _userid, _username, _userpass, _passquestion, _passanswer, _realname, _sex, _email, _mobiel, _address, _company, _comment, _userrankid, _isaudit,_point, _loginnum, _lastlogintime, _thislogintime, _addtime, _isclose;

        /// <summary>
        /// UserID
        /// </summary>
        public string UserID
        {
            get { return _userid; }
            set { _userid = value; }
        }
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        /// <summary>
        /// UserPass
        /// </summary>
        public string UserPass
        {
            get { return _userpass; }
            set { _userpass = value; }
        }
        /// <summary>
        /// PassQuestion
        /// </summary>
        public string PassQuestion
        {
            get { return _passquestion; }
            set { _passquestion = value; }
        }
        /// <summary>
        /// PassAnswer
        /// </summary>
        public string PassAnswer
        {
            get { return _passanswer; }
            set { _passanswer = value; }
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
        /// Sex
        /// </summary>
        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
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
        /// Mobile
        /// </summary>
        public string Mobile
        {
            get { return _mobiel; }
            set { _mobiel = value; }
        }
        /// <summary>
        /// Address
        /// </summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        /// <summary>
        /// Company
        /// </summary>
        public string Company
        {
            get { return _company; }
            set { _company = value; }
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
        /// UserRankID
        /// </summary>
        public string UserRankID
        {
            get { return _userrankid; }
            set { _userrankid = value; }
        }
        /// <summary>
        /// IsAudit
        /// </summary>
        public string IsAudit
        {
            get { return _isaudit; }
            set { _isaudit = value; }
        }
        /// <summary>
        /// Point
        /// </summary>
        public string Point
        {
            get { return _point; }
            set { _point = value; }
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

