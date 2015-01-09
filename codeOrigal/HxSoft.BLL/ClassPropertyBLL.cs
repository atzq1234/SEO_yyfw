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
    ///栏目属性-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2012-4-16
    /// </summary>

    public class ClassPropertyBLL
    {

        private readonly ClassPropertyDAL claProDAL = new ClassPropertyDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return claProDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strClassPropertyID)
        {
            return claProDAL.CheckInfo(strFieldName, strFieldValue, strClassPropertyID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strClassPropertyID)
        {
            return claProDAL.GetValueByField(strFieldName, strClassPropertyID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public ClassPropertyModel GetInfo(string strClassPropertyID)
        {
            return claProDAL.GetInfo(strClassPropertyID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public ClassPropertyModel GetCacheInfo(string strClassPropertyID)
        {
            string key = "Cache_ClassProperty_Model_" + strClassPropertyID;
            if (HttpRuntime.Cache[key] != null)
                return (ClassPropertyModel)HttpRuntime.Cache[key];
            else
            {
                ClassPropertyModel claProModel = claProDAL.GetInfo(strClassPropertyID);
                CacheHelper.AddCache(key, claProModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return claProModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(ClassPropertyModel claProModel)
        {
            claProDAL.InsertInfo(claProModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(ClassPropertyModel claProModel, string strClassPropertyID)
        {
            claProDAL.UpdateInfo(claProModel, strClassPropertyID);
            string key = "Cache_ClassProperty_Model_" + strClassPropertyID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strClassPropertyID)
        {
            claProDAL.DeleteInfo(strClassPropertyID);
            string key = "Cache_ClassProperty_Model_" + strClassPropertyID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strClassPropertyID, string strIsClose)
        {
            claProDAL.UpdateCloseStatus(strClassPropertyID, strIsClose);
            string key = "Cache_ClassProperty_Model_" + strClassPropertyID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return claProDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            claProDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion
    }
}
