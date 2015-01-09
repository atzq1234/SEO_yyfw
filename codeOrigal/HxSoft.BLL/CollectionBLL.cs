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
    ///产品收藏-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-8-15
    /// </summary>

    public class CollectionBLL
    {

        private readonly CollectionDAL colDAL = new CollectionDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return colDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strCollectionID)
        {
            return colDAL.CheckInfo(strFieldName, strFieldValue, strCollectionID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public CollectionModel GetInfo(string strCollectionID)
        {
            return colDAL.GetInfo(strCollectionID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public CollectionModel GetCacheInfo(string strCollectionID)
        {
            string key = "Cache_Collection_Model_" + strCollectionID;
            if (HttpRuntime.Cache[key] != null)
                return (CollectionModel)HttpRuntime.Cache[key];
            else
            {
                CollectionModel colModel = colDAL.GetInfo(strCollectionID);
                CacheHelper.AddCache(key, colModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return colModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(CollectionModel colModel)
        {
            colDAL.InsertInfo(colModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(CollectionModel colModel, string strCollectionID)
        {
            colDAL.UpdateInfo(colModel, strCollectionID);
            string key = "Cache_Collection_Model_" + strCollectionID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strCollectionID)
        {
            colDAL.DeleteInfo(strCollectionID);
            string key = "Cache_Collection_Model_" + strCollectionID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 是否收藏
        /// <summary>
        /// 是否收藏
        /// </summary>
        public bool IsCollect(string strUrl, string strUserID)
        {
            return colDAL.IsCollect(strUrl, strUserID);
        }
        #endregion
    }
}
