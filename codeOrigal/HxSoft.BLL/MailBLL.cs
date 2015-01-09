using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.DAL;

namespace HxSoft.BLL
{
    /// <summary>
    ///邮件订阅-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2011-4-27
    /// </summary>

    public class MailBLL
    {

        private readonly MailDAL mailDAL = new MailDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return mailDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strMailID)
        {
            return mailDAL.CheckInfo(strFieldName, strFieldValue, strMailID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public MailModel GetInfo(string strMailID)
        {
            return mailDAL.GetInfo(strMailID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public MailModel GetCacheInfo(string strMailID)
        {
            string key = "Cache_Mail_Model_" + strMailID;
            if (HttpRuntime.Cache[key] != null)
                return (MailModel)HttpRuntime.Cache[key];
            else
            {
                MailModel mailModel = mailDAL.GetInfo(strMailID);
                CacheHelper.AddCache(key, mailModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return mailModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(MailModel mailModel)
        {
            mailDAL.InsertInfo(mailModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(MailModel mailModel, string strMailID)
        {
            mailDAL.UpdateInfo(mailModel, strMailID);
            string key = "Cache_Mail_Model_" + strMailID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strMailID)
        {
            mailDAL.DeleteInfo(strMailID);
            string key = "Cache_Mail_Model_" + strMailID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strMailID)
        {
            return mailDAL.GetValueByField(strFieldName, strMailID);
        }
        #endregion

        #region 导出邮件地址
        /// <summary>
        /// 导出邮件地址
        /// </summary>
        public void EmailExport(string strSql, string strFilePath, string strFileName)
        {
            mailDAL.EmailExport(strSql, strFilePath, strFileName);
        }
        #endregion
    }
}
