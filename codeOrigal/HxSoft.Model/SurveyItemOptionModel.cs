using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///调查子选顶 -实体类
    /// 创建人:杨小明
    /// 日期:2011-12-26
    /// </summary>
    [Serializable]
    public class SurveyItemOptionModel
    {
        private string _surveyitemoptionid, _itemoptionname, _surveyitemid,_listid, _adminid;

        /// <summary>
        /// SurveyItemOptionID
        /// </summary>
        public string SurveyItemOptionID
        {
            get { return _surveyitemoptionid; }
            set { _surveyitemoptionid = value; }
        }
        /// <summary>
        /// ItemOptionName
        /// </summary>
        public string ItemOptionName
        {
            get { return _itemoptionname; }
            set { _itemoptionname = value; }
        }
        /// <summary>
        /// SurveyItemID
        /// </summary>
        public string SurveyItemID
        {
            get { return _surveyitemid; }
            set { _surveyitemid = value; }
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
    }
}

