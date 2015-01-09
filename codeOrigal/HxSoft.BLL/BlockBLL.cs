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
    ///片段内容管理-业务逻辑类
    /// 创建人:Admin
    /// 日期:2012-10-17
    /// </summary>

    public class BlockBLL
    {

        private readonly BlockDAL bloDAL = new BlockDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return bloDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strBlockID)
        {
            return bloDAL.CheckInfo(strFieldName, strFieldValue, strBlockID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strBlockID)
        {
            return bloDAL.GetValueByField(strFieldName, strBlockID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public BlockModel GetInfo(string strBlockID)
        {
            return bloDAL.GetInfo(strBlockID);
        }

        public BlockModel GetInfo2(string strBlockID)
        {
            return bloDAL.GetInfo2(strBlockID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public BlockModel GetCacheInfo(string strBlockID)
        {
            string key = "Cache_Block_Model_" + strBlockID;
            if (HttpRuntime.Cache[key] != null)
                return (BlockModel)HttpRuntime.Cache[key];
            else
            {
                BlockModel bloModel = bloDAL.GetInfo(strBlockID);
                CacheHelper.AddCache(key, bloModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return bloModel;
            }
        }

        public BlockModel GetCacheInfo2(string strBlockID)
        {
            string key = "Cache_Block_Model_" + strBlockID;
            if (HttpRuntime.Cache[key] != null)
                return (BlockModel)HttpRuntime.Cache[key];
            else
            {
                BlockModel bloModel = bloDAL.GetInfo2(strBlockID);
                CacheHelper.AddCache(key, bloModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return bloModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(BlockModel bloModel)
        {
            bloDAL.InsertInfo(bloModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(BlockModel bloModel, string strBlockID)
        {
            bloDAL.UpdateInfo(bloModel, strBlockID);
            string key = "Cache_Block_Model_" + strBlockID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strBlockID)
        {
            bloDAL.DeleteInfo(strBlockID);
            string key = "Cache_Block_Model_" + strBlockID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strBlockID, string strIsClose)
        {
            bloDAL.UpdateCloseStatus(strBlockID, strIsClose);
            string key = "Cache_Block_Model_" + strBlockID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

    }
}
