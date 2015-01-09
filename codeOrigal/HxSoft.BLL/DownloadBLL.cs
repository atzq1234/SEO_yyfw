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
    ///下载管理-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2011-2-24
    /// </summary>

    public class DownloadBLL
    {

        private readonly DownloadDAL dowDAL = new DownloadDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return dowDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strDownloadID)
        {
            return dowDAL.CheckInfo(strFieldName, strFieldValue, strDownloadID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public DownloadModel GetInfo(string strDownloadID)
        {
            return dowDAL.GetInfo(strDownloadID);
        }
        /// <summary>
        /// 前台读取信息
        /// </summary>
        public DownloadModel GetInfo2(string strDownloadID)
        {
            return dowDAL.GetInfo2(strDownloadID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public DownloadModel GetCacheInfo(string strDownloadID)
        {
            string key = "Cache_Download_Model_" + strDownloadID;
            if (HttpRuntime.Cache[key] != null)
                return (DownloadModel)HttpRuntime.Cache[key];
            else
            {
                DownloadModel dowModel = dowDAL.GetInfo(strDownloadID);
                CacheHelper.AddCache(key, dowModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return dowModel;
            }
        }
        /// <summary>
        /// 前台从缓存读取信息
        /// </summary>
        public DownloadModel GetCacheInfo2(string strDownloadID)
        {
            string key = "Cache_Download_Model_" + strDownloadID;
            if (HttpRuntime.Cache[key] != null)
                return (DownloadModel)HttpRuntime.Cache[key];
            else
            {
                DownloadModel dowModel = dowDAL.GetInfo2(strDownloadID);
                CacheHelper.AddCache(key, dowModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return dowModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(DownloadModel dowModel)
        {
            dowDAL.InsertInfo(dowModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(DownloadModel dowModel, string strDownloadID)
        {
            dowDAL.UpdateInfo(dowModel, strDownloadID);
            string key = "Cache_Download_Model_" + strDownloadID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strDownloadID)
        {
            dowDAL.DeleteInfo(strDownloadID);
            string key = "Cache_Download_Model_" + strDownloadID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strArticleID, string strIsClose)
        {
            dowDAL.UpdateCloseStatus(strArticleID, strIsClose);
            string key = "Cache_Article_Model_" + strArticleID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 转移信息
        /// <summary>
        /// 转移信息
        /// </summary>
        public void TransferInfo(string strDownloadID, string strClassID)
        {
            dowDAL.TransferInfo(strDownloadID, strClassID);
        }
        #endregion

        #region 取第一个信息ID
        /// <summary>
        /// 取第一个信息ID
        /// </summary>
        public string GetFirstID(string strClassID)
        {
            return dowDAL.GetFirstID(strClassID);
        }
        #endregion

        #region 访问数加1
        /// <summary>
        /// 访问数加1
        /// </summary>
        public void Click(string strArticleID)
        {
            dowDAL.Click(strArticleID);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return dowDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            dowDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strDownloadID)
        {
            return dowDAL.GetValueByField(strFieldName, strDownloadID);
        }
        #endregion


    }
}
