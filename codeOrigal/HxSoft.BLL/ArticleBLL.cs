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
    ///文章管理-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class ArticleBLL
    {

        private readonly ArticleDAL artDAL = new ArticleDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return artDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strArticleID)
        {
            return artDAL.CheckInfo(strFieldName, strFieldValue, strArticleID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public ArticleModel GetInfo(string strArticleID)
        {
            return artDAL.GetInfo(strArticleID);
        }
        /// <summary>
        /// 前台读取信息
        /// </summary>
        public ArticleModel GetInfo2(string strArticleID)
        {
            return artDAL.GetInfo2(strArticleID);
        }

        public IList<ArticleModel> GetInfoList(string strClassID, int topnum)
        {
            return artDAL.GetInfoList(strClassID, topnum);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public ArticleModel GetCacheInfo(string strArticleID)
        {
            string key = "Cache_Article_Model_" + strArticleID;
            if (HttpRuntime.Cache[key] != null)
                return (ArticleModel)HttpRuntime.Cache[key];
            else
            {
                ArticleModel artModel = artDAL.GetInfo(strArticleID);
                CacheHelper.AddCache(key, artModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return artModel;
            }
        }
        /// <summary>
        /// 前台从缓存读取信息
        /// </summary>
        public ArticleModel GetCacheInfo2(string strArticleID)
        {
            string key = "Cache_Article_Model_" + strArticleID;
            if (HttpRuntime.Cache[key] != null)
                return (ArticleModel)HttpRuntime.Cache[key];
            else
            {
                ArticleModel artModel = artDAL.GetInfo2(strArticleID);
                CacheHelper.AddCache(key, artModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return artModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(ArticleModel artModel)
        {
            artDAL.InsertInfo(artModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(ArticleModel artModel, string strArticleID)
        {
            artDAL.UpdateInfo(artModel, strArticleID);
            string key = "Cache_Article_Model_" + strArticleID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strArticleID)
        {
            artDAL.DeleteInfo(strArticleID);
            string key = "Cache_Article_Model_" + strArticleID;
            CacheHelper.RemoveCache(key);

            ArticlePicDAL artPicDAL = new ArticlePicDAL();
            artPicDAL.DeleteInfoByArticleID(strArticleID);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strArticleID, string strIsClose)
        {
            artDAL.UpdateCloseStatus(strArticleID, strIsClose);
            string key = "Cache_Article_Model_" + strArticleID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 转移信息
        /// <summary>
        /// 转移信息
        /// </summary>
        public void TransferInfo(string strArticleID, string strClassID)
        {
            artDAL.TransferInfo(strArticleID, strClassID);
        }
        #endregion

        #region 取第一个信息ID
        /// <summary>
        /// 取第一个信息ID
        /// </summary>
        public string GetFirstID(string strClassID)
        {
            return artDAL.GetFirstID(strClassID);
        }
          #endregion

        #region 访问数加1
        /// <summary>
        /// 访问数加1
        /// </summary>
        public void Click(string strArticleID)
        {
            artDAL.Click(strArticleID);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return artDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            artDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strArticleID)
        {
            return artDAL.GetValueByField(strFieldName, strArticleID);
        }
        #endregion

        #region 取上一篇信息ID
        /// <summary>
        /// 取上一篇信息ID
        /// </summary>
        public string GetPrevID(string strClassID, string strArticleID)
        {
            return artDAL.GetPrevID(strClassID, strArticleID);
        }
        #endregion

        #region 取下一篇信息ID
        /// <summary>
        /// 取下一篇信息ID
        /// </summary>
        public string GetNextID(string strClassID, string strArticleID)
        {
            return artDAL.GetNextID(strClassID, strArticleID);
        }
        #endregion

        #region RSS文件
        /// <summary>
        /// RSS文件
        /// </summary>
        /// <param name="strClassID"></param>
        /// <returns></returns>
        public StringBuilder RSS(string strClassID)
        {
            return artDAL.RSS(strClassID);
        }
        #endregion

    }
}
