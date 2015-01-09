using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    /// 管理员日志管理-实体类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    [Serializable]
    public class AdminLogModel
    {
        private string _adminlogid, _logcontent, _scriptfile, _ipaddress, _adminid, _addtime;

        /// <summary>
        /// AdminLogID
        /// </summary>
        public string AdminLogID
        {
            get { return _adminlogid; }
            set { _adminlogid = value; }
        }
        /// <summary>
        /// LogContent
        /// </summary>
        public string LogContent
        {
            get { return _logcontent; }
            set { _logcontent = value; }
        }
        /// <summary>
        /// ScriptFile
        /// </summary>
        public string ScriptFile
        {
            get { return _scriptfile; }
            set { _scriptfile = value; }
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
    }
}
