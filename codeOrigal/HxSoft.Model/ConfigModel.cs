using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///网站配置-实体类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    [Serializable]
    public class ConfigModel
    {
        private string _languagever, _configid, _websitename, _websiteurl, _websitekeywords, _websitedescription, _mailreceiveaddress, _mailsmtpserver, _mailusername, _mailpassword, _footerinfo, _listid, _adminid, _addtime, _isclose;

        /// <summary>
        /// LanguageVer
        /// </summary>
        public string LanguageVer
        {
            get { return _languagever; }
            set { _languagever = value; }
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
        /// WebsiteName
        /// </summary>
        public string WebsiteName
        {
            get { return _websitename; }
            set { _websitename = value; }
        }
        /// <summary>
        /// WebsiteUrl
        /// </summary>
        public string WebsiteUrl
        {
            get { return _websiteurl; }
            set { _websiteurl = value; }
        }
        /// <summary>
        /// WebsiteKeywords
        /// </summary>
        public string WebsiteKeywords
        {
            get { return _websitekeywords; }
            set { _websitekeywords = value; }
        }
        /// <summary>
        /// WebsiteDescription
        /// </summary>
        public string WebsiteDescription
        {
            get { return _websitedescription; }
            set { _websitedescription = value; }
        }
        /// <summary>
        /// MailReceiveAddress
        /// </summary>
        public string MailReceiveAddress
        {
            get { return _mailreceiveaddress; }
            set { _mailreceiveaddress = value; }
        }
        /// <summary>
        /// MailSmtpServer
        /// </summary>
        public string MailSmtpServer
        {
            get { return _mailsmtpserver; }
            set { _mailsmtpserver = value; }
        }
        /// <summary>
        /// MailUserName
        /// </summary>
        public string MailUserName
        {
            get { return _mailusername; }
            set { _mailusername = value; }
        }
        /// <summary>
        /// MailPassword
        /// </summary>
        public string MailPassword
        {
            get { return _mailpassword; }
            set { _mailpassword = value; }
        }
        /// <summary>
        /// FooterInfo
        /// </summary>
        public string FooterInfo
        {
            get { return _footerinfo; }
            set { _footerinfo = value; }
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

