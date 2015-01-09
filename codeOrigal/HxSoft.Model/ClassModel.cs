using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///栏目管理-实体类
    /// 创建人:杨小明
    /// 日期:2012-4-20
    /// </summary>
    [Serializable]
    public class ClassModel
    {
        private string _classid, _configid, _classname, _classenname, _classpropertyid, _classtemplateid, _classpic, _linkurl, _target, _isgotofirst, _isshownav, _keywords, _description, _classcontent, _classconfig, _parentid, _childnum, _listid, _adminid, _addtime, _isclose;

        /// <summary>
        /// ClassID
        /// </summary>
        public string ClassID
        {
            get { return _classid; }
            set { _classid = value; }
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
        /// ClassName
        /// </summary>
        public string ClassName
        {
            get { return _classname; }
            set { _classname = value; }
        }
        /// <summary>
        /// ClassEnName
        /// </summary>
        public string ClassEnName
        {
            get { return _classenname; }
            set { _classenname = value; }
        }
        /// <summary>
        /// ClassPropertyID
        /// </summary>
        public string ClassPropertyID
        {
            get { return _classpropertyid; }
            set { _classpropertyid = value; }
        }
        /// <summary>
        /// ClassTemplateID
        /// </summary>
        public string ClassTemplateID
        {
            get { return _classtemplateid; }
            set { _classtemplateid = value; }
        }
        /// <summary>
        /// ClassPic
        /// </summary>
        public string ClassPic
        {
            get { return _classpic; }
            set { _classpic = value; }
        }
        /// <summary>
        /// LinkUrl
        /// </summary>
        public string LinkUrl
        {
            get { return _linkurl; }
            set { _linkurl = value; }
        }
        /// <summary>
        /// Target
        /// </summary>
        public string Target
        {
            get { return _target; }
            set { _target = value; }
        }
        /// <summary>
        /// IsGoToFirst
        /// </summary>
        public string IsGoToFirst
        {
            get { return _isgotofirst; }
            set { _isgotofirst = value; }
        }
        /// <summary>
        /// IsShowNav
        /// </summary>
        public string IsShowNav
        {
            get { return _isshownav; }
            set { _isshownav = value; }
        }
        /// <summary>
        /// Keywords
        /// </summary>
        public string Keywords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }
        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// ClassContent
        /// </summary>
        public string ClassContent
        {
            get { return _classcontent; }
            set { _classcontent = value; }
        }
        /// <summary>
        /// ClassConfig
        /// </summary>
        public string ClassConfig
        {
            get { return _classconfig; }
            set { _classconfig = value; }
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
        /// ChildNum
        /// </summary>
        public string ChildNum
        {
            get { return _childnum; }
            set { _childnum = value; }
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

