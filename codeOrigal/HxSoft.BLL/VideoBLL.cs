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
    ///视频管理-业务逻辑类
    /// 创建人:
    /// 日期:2012-9-19
    /// </summary>

    public class VideoBLL
    {

        private readonly VideoDAL vidDAL = new VideoDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return vidDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strVideoID)
        {
            return vidDAL.CheckInfo(strFieldName, strFieldValue, strVideoID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strVideoID)
        {
            return vidDAL.GetValueByField(strFieldName, strVideoID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public VideoModel GetInfo(string strVideoID)
        {
            return vidDAL.GetInfo(strVideoID);
        }

        public VideoModel GetInfo2(string strVideoID)
        {
            return vidDAL.GetInfo2(strVideoID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public VideoModel GetCacheInfo(string strVideoID)
        {
            string key = "Cache_Video_Model_" + strVideoID;
            if (HttpRuntime.Cache[key] != null)
                return (VideoModel)HttpRuntime.Cache[key];
            else
            {
                VideoModel vidModel = vidDAL.GetInfo(strVideoID);
                CacheHelper.AddCache(key, vidModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return vidModel;
            }
        }

        /// <summary>
        /// 前台从缓存读取信息
        /// </summary>
        public VideoModel GetCacheInfo2(string strVideoID)
        {
            string key = "Cache_Video_Model_" + strVideoID;
            if (HttpRuntime.Cache[key] != null)
                return (VideoModel)HttpRuntime.Cache[key];
            else
            {
                VideoModel vidModel = vidDAL.GetInfo2(strVideoID);
                CacheHelper.AddCache(key, vidModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return vidModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(VideoModel vidModel)
        {
            vidDAL.InsertInfo(vidModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(VideoModel vidModel, string strVideoID)
        {
            vidDAL.UpdateInfo(vidModel, strVideoID);
            string key = "Cache_Video_Model_" + strVideoID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strVideoID)
        {
            vidDAL.DeleteInfo(strVideoID);
            string key = "Cache_Video_Model_" + strVideoID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strVideoID, string strIsClose)
        {
            vidDAL.UpdateCloseStatus(strVideoID, strIsClose);
            string key = "Cache_Video_Model_" + strVideoID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return vidDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            vidDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region 取上一篇信息ID
        /// <summary>
        /// 取上一篇信息ID
        /// </summary>
        public string GetPrevID(string strClassID, string strVideoID)
        {
            return vidDAL.GetPrevID(strClassID, strVideoID);
        }
        #endregion

        #region 取下一篇信息ID
        /// <summary>
        /// 取下一篇信息ID
        /// </summary>
        public string GetNextID(string strClassID, string strVideoID)
        {
            return vidDAL.GetNextID(strClassID, strVideoID);
        }
        #endregion
    }
}
