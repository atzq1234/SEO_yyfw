using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///文件管理-实体类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    [Serializable]
    public class PathModel
    {
        private string _path, _adminid;

        /// <summary>
        /// Path
        /// </summary>
        public string Path
        {
            get { return _path; }
            set { _path = value; }
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

