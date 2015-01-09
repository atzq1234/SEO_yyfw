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

    public class ProductBLL
    {

        private readonly ProductDAL proDAL = new ProductDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return proDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strProductID)
        {
            return proDAL.CheckInfo(strFieldName, strFieldValue, strProductID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public ProductModel GetInfo(string strProductID)
        {
            return proDAL.GetInfo(strProductID);
        }
        /// <summary>
        /// 前台读取信息
        /// </summary>
        public ProductModel GetInfo2(string strProductID)
        {
            return proDAL.GetInfo2(strProductID);
        }

        public IList<ProductModel> GetInfoTopList(string strIsRecommand, int topnum)
        {
            return proDAL.GetInfoTopList(strIsRecommand, topnum);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public ProductModel GetCacheInfo(string strProductID)
        {
            string key = "Cache_Product_Model_" + strProductID;
            if (HttpRuntime.Cache[key] != null)
                return (ProductModel)HttpRuntime.Cache[key];
            else
            {
                ProductModel proModel = proDAL.GetInfo(strProductID);
                CacheHelper.AddCache(key, proModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return proModel;
            }
        }
        /// <summary>
        /// 前台从缓存读取信息
        /// </summary>
        public ProductModel GetCacheInfo2(string strProductID)
        {
            string key = "Cache_Product_Model_" + strProductID;
            if (HttpRuntime.Cache[key] != null)
                return (ProductModel)HttpRuntime.Cache[key];
            else
            {
                ProductModel proModel = proDAL.GetInfo2(strProductID);
                CacheHelper.AddCache(key, proModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return proModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(ProductModel proModel)
        {
            proDAL.InsertInfo(proModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(ProductModel proModel, string strProductID)
        {
            proDAL.UpdateInfo(proModel, strProductID);
            string key = "Cache_Product_Model_" + strProductID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strProductID)
        {
            proDAL.DeleteInfo(strProductID);
            string key = "Cache_Product_Model_" + strProductID;
            CacheHelper.RemoveCache(key);

            ProductPicDAL proPicDAL = new ProductPicDAL();
            proPicDAL.DeleteInfoByProductID(strProductID);
        }
        #endregion

        #region 更新关闭状态
        /// <summary>
        /// 更新关闭状态
        /// </summary>
        public void UpdateCloseStatus(string strProductID, string strIsClose)
        {
            proDAL.UpdateCloseStatus(strProductID, strIsClose);
            string key = "Cache_Product_Model_" + strProductID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 更新推荐状态
        /// <summary>
        /// 更新推荐状态
        /// </summary>
        public void UpdateRecommendStatus(string strProductID, string strRecommend)
        {
            proDAL.UpdateRecommendStatus(strProductID, strRecommend);
            string key = "Cache_Product_Model_" + strProductID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 转移信息
        /// <summary>
        /// 转移信息
        /// </summary>
        public void TransferInfo(string strProductID, string strClassID)
        {
            proDAL.TransferInfo(strProductID, strClassID);
        }
        #endregion       

        #region 取第一个信息ID
        /// <summary>
        /// 取第一个信息ID
        /// </summary>
        public string GetFirstID(string strClassID)
        {
            return proDAL.GetFirstID(strClassID);
        }
        #endregion

        #region 访问数加1
        /// <summary>
        /// 访问数加1
        /// </summary>
        public void Click(string strProductID)
        {
            proDAL.Click(strProductID);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return proDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            proDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strProductID)
        {
            return proDAL.GetValueByField(strFieldName, strProductID);
        }
        #endregion

        #region 取上一篇信息ID
        /// <summary>
        /// 取上一篇信息ID
        /// </summary>
        public string GetPrevID(string strClassID, string strProductID)
        {
            return proDAL.GetPrevID(strClassID, strProductID);
        }
        #endregion

        #region 取下一篇信息ID
        /// <summary>
        /// 取下一篇信息ID
        /// </summary>
        public string GetNextID(string strClassID, string strProductID)
        {
            return proDAL.GetNextID(strClassID, strProductID);
        }
        #endregion

    }
}
