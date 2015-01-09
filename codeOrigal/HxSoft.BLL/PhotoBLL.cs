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
    ///相册管理-业务逻辑类
    /// 创建人:
    /// 日期:2012-9-20
    /// </summary>

    public class PhotoBLL
    {

        private readonly PhotoDAL phoDAL = new PhotoDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return phoDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strPhotoID)
        {
            return phoDAL.CheckInfo(strFieldName, strFieldValue, strPhotoID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strPhotoID)
        {
            return phoDAL.GetValueByField(strFieldName, strPhotoID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public PhotoModel GetInfo(string strPhotoID)
        {
            return phoDAL.GetInfo(strPhotoID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public PhotoModel GetCacheInfo(string strPhotoID)
        {
            string key = "Cache_Photo_Model_" + strPhotoID;
            if (HttpRuntime.Cache[key] != null)
                return (PhotoModel)HttpRuntime.Cache[key];
            else
            {
                PhotoModel phoModel = phoDAL.GetInfo(strPhotoID);
                CacheHelper.AddCache(key, phoModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return phoModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(PhotoModel phoModel)
        {
            phoDAL.InsertInfo(phoModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(PhotoModel phoModel, string strPhotoID)
        {
            phoDAL.UpdateInfo(phoModel, strPhotoID);
            string key = "Cache_Photo_Model_" + strPhotoID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strPhotoID)
        {
            phoDAL.DeleteInfo(strPhotoID);
            string key = "Cache_Photo_Model_" + strPhotoID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strPhotoID, string strIsClose)
        {
            phoDAL.UpdateCloseStatus(strPhotoID, strIsClose);
            string key = "Cache_Photo_Model_" + strPhotoID;
            CacheHelper.RemoveCache(key);
        }
        #endregion


        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return phoDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            phoDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion
    }
}
