using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///留言板-实体类
    /// 创建人:杨小明
    /// 日期:2011-9-16
    /// </summary>
    [Serializable]
    public class GuestbookModel
    {
        private string _guestbookid, _nickname, _bookcontent, _ipaddress, _addtime, _isreply, _replycontent, _replytime, _adminid, _isclose;

        /// <summary>
        /// GuestbookID
        /// </summary>
        public string GuestbookID
        {
            get { return _guestbookid; }
            set { _guestbookid = value; }
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
        /// BookContent
        /// </summary>
        public string BookContent
        {
            get { return _bookcontent; }
            set { _bookcontent = value; }
        }
        /// <summary>
        /// IpAddress
        /// </summary>
        public string IpAddress
        {
            get { return _ipaddress; }
            set { _ipaddress = value; }
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
        /// IsReply
        /// </summary>
        public string IsReply
        {
            get { return _isreply; }
            set { _isreply = value; }
        }
        /// <summary>
        /// ReplyContent
        /// </summary>
        public string ReplyContent
        {
            get { return _replycontent; }
            set { _replycontent = value; }
        }
        /// <summary>
        /// ReplyTime
        /// </summary>
        public string ReplyTime
        {
            get { return _replytime; }
            set { _replytime = value; }
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
        /// IsClose
        /// </summary>
        public string IsClose
        {
            get { return _isclose; }
            set { _isclose = value; }
        }

        public string TelePhone { get; set; }
        public string Email { get; set; }
    }
}

