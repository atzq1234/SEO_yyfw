using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///广告管理-实体类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    [Serializable]
    public class AdModel
    {
        private string _adid, _adname, _adintro, _adpositionid, _adsmallpic, _adpath, _adlink, _clicknum, _listid, _adminid, _addtime, _isclose;

        /// <summary>
        /// AdID
        /// </summary>
        public string AdID
        {
            get { return _adid; }
            set { _adid = value; }
        }
        /// <summary>
        /// AdName
        /// </summary>
        public string AdName
        {
            get { return _adname; }
            set { _adname = value; }
        }
        /// <summary>
        /// AdIntro
        /// </summary>
        public string AdIntro
        {
            get { return _adintro; }
            set { _adintro = value; }
        }
        /// <summary>
        /// AdPositionID
        /// </summary>
        public string AdPositionID
        {
            get { return _adpositionid; }
            set { _adpositionid = value; }
        }
        /// <summary>
        /// AdSmallPic
        /// </summary>
        public string AdSmallPic
        {
            get { return _adsmallpic; }
            set { _adsmallpic = value; }
        }
        /// <summary>
        /// AdPath
        /// </summary>
        public string AdPath
        {
            get { return _adpath; }
            set { _adpath = value; }
        }

        /// <summary>
        /// AdLink
        /// </summary>
        public string AdLink
        {
            get { return _adlink; }
            set { _adlink = value; }
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

