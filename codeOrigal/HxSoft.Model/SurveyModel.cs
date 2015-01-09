using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///在线调查-实体类
    /// 创建人:杨小明
    /// 日期:2011-12-26
    /// </summary>
    [Serializable]
    public class SurveyModel
    {
        private string _surveyid,_classid, _subject,_intrcontent, _isrecommend,_clicknum,_listid,_adminid, _addtime, _isclose;

        /// <summary>
        /// SurveyID
        /// </summary>
        public string SurveyID
        {
            get { return _surveyid; }
            set { _surveyid = value; }
        }
        /// <summary>
        /// ClassID
        /// </summary>
        public string ClassID
        {
            get { return _classid; }
            set { _classid = value; }
        }
        /// <summary>
        /// Subject
        /// </summary>
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }
        /// <summary>
        /// IntrContent
        /// </summary>
        public string IntrContent
        {
            get { return _intrcontent; }
            set { _intrcontent = value; }
        }
        /// <summary>
        /// IsRecommend
        /// </summary>
        public string IsRecommend
        {
            get { return _isrecommend; }
            set { _isrecommend = value; }
        }
        /// <summary>
        /// ClickNum
        /// </summary>
        public string ClickNum
        {
            get { return _clicknum; }
            set { _clicknum = value; }
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

