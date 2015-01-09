using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///聊天工具-实体类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    [Serializable]
    public class ChatModel
    {
        private string _chatid,_configid, _typeid, _nickname, _account, _chatkey, _listid, _adminid, _addtime, _isclose;

        /// <summary>
        /// ChatID
        /// </summary>
        public string ChatID
        {
            get { return _chatid; }
            set { _chatid = value; }
        }
        /// <summary>
        /// ConfigID
        /// </summary>
        public string ConfigID
        {
            get { return _configid; }
            set { _configid = value; }
        }
        /// <summary>
        /// TypeID
        /// </summary>
        public string TypeID
        {
            get { return _typeid; }
            set { _typeid = value; }
        }
        /// <summary>
        /// NickName
        /// </summary>
        public string NickName
        {
            get { return _nickname; }
            set { _nickname = value; }
        }
        /// <summary>
        /// Account
        /// </summary>
        public string Account
        {
            get { return _account; }
            set { _account = value; }
        }
        /// <summary>
        /// ChatKey
        /// </summary>
        public string ChatKey
        {
            get { return _chatkey; }
            set { _chatkey = value; }
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

