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
    ///产品图片-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2012-1-19
    /// </summary>

    public class ProductPicBLL
    {

        private readonly ProductPicDAL proPicDAL = new ProductPicDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return proPicDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strProductPicID)
        {
            return proPicDAL.CheckInfo(strFieldName, strFieldValue, strProductPicID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strProductPicID)
        {
            return proPicDAL.GetValueByField(strFieldName, strProductPicID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public ProductPicModel GetInfo(string strProductPicID)
        {
            return proPicDAL.GetInfo(strProductPicID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public ProductPicModel GetCacheInfo(string strProductPicID)
        {
            string key = "Cache_ProductPic_Model_" + strProductPicID;
            if (HttpRuntime.Cache[key] != null)
                return (ProductPicModel)HttpRuntime.Cache[key];
            else
            {
                ProductPicModel proPicModel = proPicDAL.GetInfo(strProductPicID);
                CacheHelper.AddCache(key, proPicModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return proPicModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(ProductPicModel proPicModel)
        {
            proPicDAL.InsertInfo(proPicModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(ProductPicModel proPicModel, string strProductPicID)
        {
            proPicDAL.UpdateInfo(proPicModel, strProductPicID);
            string key = "Cache_ProductPic_Model_" + strProductPicID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strProductPicID)
        {
            proPicDAL.DeleteInfo(strProductPicID);
            string key = "Cache_ProductPic_Model_" + strProductPicID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strProductPicID, string strIsClose)
        {
            proPicDAL.UpdateCloseStatus(strProductPicID, strIsClose);
            string key = "Cache_ProductPic_Model_" + strProductPicID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return proPicDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            proPicDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region 根据ProductID删除信息
        /// <summary>
        /// 根据ProductID删除信息
        /// </summary>
        public void DeleteInfoByProductID(string strProductID)
        {
            proPicDAL.DeleteInfoByProductID(strProductID);
        }
        #endregion

    }
}
