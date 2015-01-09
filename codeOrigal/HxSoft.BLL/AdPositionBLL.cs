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
    ///广告位管理-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class AdPositionBLL
    {

        private readonly AdPositionDAL adPosDAL = new AdPositionDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return adPosDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strAdPositionID)
        {
            return adPosDAL.CheckInfo(strFieldName, strFieldValue, strAdPositionID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strAdPositionID)
        {
            return adPosDAL.GetValueByField(strFieldName, strAdPositionID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public AdPositionModel GetInfo(string strAdPositionID)
        {
            return adPosDAL.GetInfo(strAdPositionID);
        }

        /// <summary>
        /// 前台读取信息
        /// </summary>
        public AdPositionModel GetInfo2(string strAdPositionID)
        {
            return adPosDAL.GetInfo2(strAdPositionID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public AdPositionModel GetCacheInfo(string strAdPositionID)
        {
            string key = "Cache_AdPosition_Model_" + strAdPositionID;
            if (HttpRuntime.Cache[key] != null)
                return (AdPositionModel)HttpRuntime.Cache[key];
            else
            {
                AdPositionModel adPosModel = adPosDAL.GetInfo(strAdPositionID);
                CacheHelper.AddCache(key, adPosModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return adPosModel;
            }
        }
        /// <summary>
        /// 前台从缓存读取信息
        /// </summary>
        public AdPositionModel GetCacheInfo2(string strAdPositionID)
        {
            string key = "Cache_AdPosition_Model_" + strAdPositionID;
            if (HttpRuntime.Cache[key] != null)
                return (AdPositionModel)HttpRuntime.Cache[key];
            else
            {
                AdPositionModel adPosModel = adPosDAL.GetInfo2(strAdPositionID);
                CacheHelper.AddCache(key, adPosModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return adPosModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(AdPositionModel adPosModel)
        {
            adPosDAL.InsertInfo(adPosModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(AdPositionModel adPosModel, string strAdPositionID)
        {
            adPosDAL.UpdateInfo(adPosModel, strAdPositionID);
            string key = "Cache_AdPosition_Model_" + strAdPositionID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strAdPositionID)
        {
            adPosDAL.DeleteInfo(strAdPositionID);
            string key = "Cache_AdPosition_Model_" + strAdPositionID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strAdPositionID, string strIsClose)
        {
            adPosDAL.UpdateCloseStatus(strAdPositionID,strIsClose);
            string key = "Cache_AdPosition_Model_" + strAdPositionID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return adPosDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            adPosDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

    }
}
