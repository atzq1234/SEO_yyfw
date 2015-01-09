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
    ///会员级别管理-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class UserRankBLL
    {

        private readonly UserRankDAL userRankDAL = new UserRankDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return userRankDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strUserRankID)
        {
            return userRankDAL.CheckInfo(strFieldName, strFieldValue, strUserRankID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public UserRankModel GetInfo(string strUserRankID)
        {
            return userRankDAL.GetInfo(strUserRankID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public UserRankModel GetCacheInfo(string strUserRankID)
        {
            string key = "Cache_UserRank_Model_" + strUserRankID;
            if (HttpRuntime.Cache[key] != null)
                return (UserRankModel)HttpRuntime.Cache[key];
            else
            {
                UserRankModel userRankModel = userRankDAL.GetInfo(strUserRankID);
                CacheHelper.AddCache(key, userRankModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return userRankModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(UserRankModel userRankModel)
        {
            userRankDAL.InsertInfo(userRankModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(UserRankModel userRankModel, string strUserRankID)
        {
            userRankDAL.UpdateInfo(userRankModel, strUserRankID);
            string key = "Cache_UserRank_Model_" + strUserRankID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strUserRankID)
        {
            userRankDAL.DeleteInfo(strUserRankID);
            string key = "Cache_UserRank_Model_" + strUserRankID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strUserRankID, string strIsClose)
        {
            userRankDAL.UpdateCloseStatus(strUserRankID, strIsClose);
        }
        #endregion

        #region 设置操作权限
        /// <summary>
        /// 设置操作权限
        /// </summary>
        public void SetLimit(UserRankModel userRankModel, string strUserRankID)
        {
            userRankDAL.SetLimit(userRankModel, strUserRankID);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return userRankDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            userRankDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region 取权限字段值
        public StringBuilder GetLimitValues(string strUserID)
        {
            string key = "Cache_UserRank_LimitValues_" + strUserID;
            if (HttpRuntime.Cache[key] != null)
                return (StringBuilder)HttpRuntime.Cache[key];
            else
            {
                StringBuilder strLimitValues = userRankDAL.GetLimitValues(strUserID);
                CacheHelper.AddCache(key, strLimitValues, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return strLimitValues;
            }
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strUserRankID)
        {
            return userRankDAL.GetValueByField(strFieldName, strUserRankID);
        }
        #endregion


    }
}
