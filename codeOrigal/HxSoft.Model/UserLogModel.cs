using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    /// 会员日志管理-实体类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    [Serializable]
    public class UserLogModel
    {
        private string _userlogid, _logcontent, _scriptfile, _ipaddress, _userid, _addtime;

        /// <summary>
        /// UserLogID
        /// </summary>
        public string UserLogID
        {
            get { return _userlogid; }
            set { _userlogid = value; }
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
        /// UserID
        /// </summary>
        public string UserID
        {
            get { return _userid; }
            set { _userid = value; }
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
