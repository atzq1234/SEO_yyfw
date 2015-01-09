using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///留言反馈-实体类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    [Serializable]
    public class MessageModel
    {
        private string _id, _dictionaryid, _userid, _title, _messagecontent, _adminid, _parentid, _addtime, _isread, _isreply, _isend;

        /// <summary>
        /// MessageID
        /// </summary>
        public string MessageID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// DictionaryID
        /// </summary>
        public string DictionaryID
        {
            get { return _dictionaryid; }
            set { _dictionaryid = value; }
        }
        /// <summary>
        /// UserID
        /// </summary>
        public string UserID
        {
            get { return _userid; }
            set { _userid = value; }
        }
        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// MessageContent
        /// </summary>
        public string MessageContent
        {
            get { return _messagecontent; }
            set { _messagecontent = value; }
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
        /// ParentID
        /// </summary>
        public string ParentID
        {
            get { return _parentid; }
            set { _parentid = value; }
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
        /// IsRead
        /// </summary>
        public string IsRead
        {
            get { return _isread; }
            set { _isread = value; }
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
        /// IsEnd
        /// </summary>
        public string IsEnd
        {
            get { return _isend; }
            set { _isend = value; }
        }
    }
}

