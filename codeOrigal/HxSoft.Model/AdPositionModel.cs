using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///广告位管理-实体类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    [Serializable]
    public class AdPositionModel
    {
        private string _adpositionid, _adpositionname, _adpositionintro, _typeid, _width, _height, _price, _listid, _adminid, _addtime, _isclose;

        /// <summary>
        /// AdPositionID
        /// </summary>
        public string AdPositionID
        {
            get { return _adpositionid; }
            set { _adpositionid = value; }
        }
        /// <summary>
        /// AdPositionName
        /// </summary>
        public string AdPositionName
        {
            get { return _adpositionname; }
            set { _adpositionname = value; }
        }
        /// <summary>
        /// AdPositionIntro
        /// </summary>
        public string AdPositionIntro
        {
            get { return _adpositionintro; }
            set { _adpositionintro = value; }
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
        /// Width
        /// </summary>
        public string Width
        {
            get { return _width; }
            set { _width = value; }
        }
        /// <summary>
        /// Height
        /// </summary>
        public string Height
        {
            get { return _height; }
            set { _height = value; }
        }


        /// <summary>
        /// Price
        /// </summary>
        public string Price
        {
            get { return _price; }
            set { _price = value; }
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

