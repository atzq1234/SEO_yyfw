using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///信息反馈-实体类
    /// 创建人:杨小明
    /// 日期:2011-2-28
    /// </summary>
    [Serializable]
    public class FeedbackModel
    {
        private string _feedbackid, _dictionaryid, _title, _feedbackcontent, _ipaddress, _addtime, _isdeal, _dealmeno;

        /// <summary>
        /// FeedbackID
        /// </summary>
        public string FeedbackID
        {
            get { return _feedbackid; }
            set { _feedbackid = value; }
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
        /// Title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// FeedbackContent
        /// </summary>
        public string FeedbackContent
        {
            get { return _feedbackcontent; }
            set { _feedbackcontent = value; }
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
        /// IsDeal
        /// </summary>
        public string IsDeal
        {
            get { return _isdeal; }
            set { _isdeal = value; }
        }
        /// <summary>
        /// DealMeno
        /// </summary>
        public string DealMeno
        {
            get { return _dealmeno; }
            set { _dealmeno = value; }
        }
    }
}

