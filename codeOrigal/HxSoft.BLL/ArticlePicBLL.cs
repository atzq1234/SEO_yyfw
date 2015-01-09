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
    ///文章图片-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2012-1-19
    /// </summary>

    public class ArticlePicBLL
    {

        private readonly ArticlePicDAL artPicDAL = new ArticlePicDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return artPicDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strArticlePicID)
        {
            return artPicDAL.CheckInfo(strFieldName, strFieldValue, strArticlePicID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strArticlePicID)
        {
            return artPicDAL.GetValueByField(strFieldName, strArticlePicID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public ArticlePicModel GetInfo(string strArticlePicID)
        {
            return artPicDAL.GetInfo(strArticlePicID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public ArticlePicModel GetCacheInfo(string strArticlePicID)
        {
            string key = "Cache_ArticlePic_Model_" + strArticlePicID;
            if (HttpRuntime.Cache[key] != null)
                return (ArticlePicModel)HttpRuntime.Cache[key];
            else
            {
                ArticlePicModel proPicModel = artPicDAL.GetInfo(strArticlePicID);
                CacheHelper.AddCache(key, proPicModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return proPicModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(ArticlePicModel proPicModel)
        {
            artPicDAL.InsertInfo(proPicModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(ArticlePicModel proPicModel, string strArticlePicID)
        {
            artPicDAL.UpdateInfo(proPicModel, strArticlePicID);
            string key = "Cache_ArticlePic_Model_" + strArticlePicID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strArticlePicID)
        {
            artPicDAL.DeleteInfo(strArticlePicID);
            string key = "Cache_ArticlePic_Model_" + strArticlePicID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strArticlePicID, string strIsClose)
        {
            artPicDAL.UpdateCloseStatus(strArticlePicID, strIsClose);
            string key = "Cache_ArticlePic_Model_" + strArticlePicID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return artPicDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            artPicDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region 根据ArticleID删除信息
        /// <summary>
        /// 根据ArticleID删除信息
        /// </summary>
        public void DeleteInfoByArticleID(string strArticleID)
        {
            artPicDAL.DeleteInfoByArticleID(strArticleID);
        }
        #endregion
    }
}
