using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///友情链接-实体类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    [Serializable]
    public class LinkModel
    {
        private string _linkid, _configid,_typeid, _sitename, _siteurl, _logourl, _listid, _adminid, _addtime, _isclose;

        /// <summary>
        /// LinkID
        /// </summary>
        public string LinkID
        {
            get { return _linkid; }
            set { _linkid = value; }
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
        /// SiteName
        /// </summary>
        public string SiteName
        {
            get { return _sitename; }
            set { _sitename = value; }
        }
        /// <summary>
        /// SiteUrl
        /// </summary>
        public string SiteUrl
        {
            get { return _siteurl; }
            set { _siteurl = value; }
        }
        /// <summary>
        /// LogoUrl
        /// </summary>
        public string LogoUrl
        {
            get { return _logourl; }
            set { _logourl = value; }
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

