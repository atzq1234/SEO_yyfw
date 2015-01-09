using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///调查选顶 -实体类
    /// 创建人:杨小明
    /// 日期:2011-12-26
    /// </summary>
    [Serializable]
    public class SurveyItemModel
    {
        private string _surveyitemid, _itemname, _typeid, _surveyid,_listid, _adminid, _addtime, _isclose;

        /// <summary>
        /// SurveyItemID
        /// </summary>
        public string SurveyItemID
        {
            get { return _surveyitemid; }
            set { _surveyitemid = value; }
        }
        /// <summary>
        /// ItemName
        /// </summary>
        public string ItemName
        {
            get { return _itemname; }
            set { _itemname = value; }
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
        /// SurveyID
        /// </summary>
        public string SurveyID
        {
            get { return _surveyid; }
            set { _surveyid = value; }
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

