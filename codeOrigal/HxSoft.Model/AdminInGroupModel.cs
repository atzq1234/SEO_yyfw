using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///����Ա���������-ʵ����
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>
    [Serializable]
    public class AdminInGroupModel
    {
        private string _adminid, _admingroupid;

        /// <summary>
        /// AdminID
        /// </summary>
        public string AdminID
        {
            get { return _adminid; }
            set { _adminid = value; }
        }
        /// <summary>
        /// AdminGroupID
        /// </summary>
        public string AdminGroupID
        {
            get { return _admingroupid; }
            set { _admingroupid = value; }
        }
    }
}
