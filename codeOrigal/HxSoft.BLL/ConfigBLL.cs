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
    ///网站配置-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class ConfigBLL
    {

        private readonly ConfigDAL confDAL = new ConfigDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return confDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strConfigID)
        {
            return confDAL.CheckInfo(strFieldName, strFieldValue, strConfigID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public ConfigModel GetInfo(string strConfigID)
        {
            return confDAL.GetInfo(strConfigID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public ConfigModel GetCacheInfo(string strConfigID)
        {
            string key = "Cache_Config_Model_" + strConfigID;
            if (HttpRuntime.Cache[key] != null)
                return (ConfigModel)HttpRuntime.Cache[key];
            else
            {
                ConfigModel confModel = confDAL.GetInfo(strConfigID);
                CacheHelper.AddCache(key, confModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return confModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(ConfigModel confModel)
        {
            confDAL.InsertInfo(confModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(ConfigModel confModel, string strConfigID)
        {
            confDAL.UpdateInfo(confModel, strConfigID);
            string key = "Cache_Config_Model_" + strConfigID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strConfigID)
        {
            confDAL.DeleteInfo(strConfigID);
            string key = "Cache_Config_Model_" + strConfigID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strConfigID)
        {
            return confDAL.GetValueByField(strFieldName, strConfigID);
        }
        #endregion

        #region 取站点列表
        /// <summary>
        /// 取站点列表(a)
        /// </summary>
        public StringBuilder GetConfigList(string strSeparator)
        {
            return confDAL.GetConfigList(strSeparator);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return confDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            confDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region 取默认站点
        /// <summary>
        /// 取默认站点
        /// </summary>
        public string GetDefaultSiteDir()
        {
            return confDAL.GetDefaultSiteDir();
        }
        #endregion

    }
}
