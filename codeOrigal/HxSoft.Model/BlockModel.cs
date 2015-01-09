using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///片段内容管理-实体类
    /// 创建人:Admin
    /// 日期:2012-10-17
    /// </summary>
    [Serializable]
    public class BlockModel
    {
        private string _blockid, _title, _blockcontent, _adminid, _addtime, _isclose;

        /// <summary>
        /// BlockID
        /// </summary>
        public string BlockID
        {
            get { return _blockid; }
            set { _blockid = value; }
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
        /// BlockContent
        /// </summary>
        public string BlockContent
        {
            get { return _blockcontent; }
            set { _blockcontent = value; }
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

