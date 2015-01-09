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
    ///调查子选顶 -业务逻辑类
    /// 创建人:杨小明
    /// 日期:2011-12-26
    /// </summary>

    public class SurveyItemOptionBLL
    {

        private readonly SurveyItemOptionDAL surChItDAL = new SurveyItemOptionDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return surChItDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strSurveyItemOptionID)
        {
            return surChItDAL.CheckInfo(strFieldName, strFieldValue, strSurveyItemOptionID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strSurveyItemOptionID)
        {
            return surChItDAL.GetValueByField(strFieldName, strSurveyItemOptionID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public SurveyItemOptionModel GetInfo(string strSurveyItemOptionID)
        {
            return surChItDAL.GetInfo(strSurveyItemOptionID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public SurveyItemOptionModel GetCacheInfo(string strSurveyItemOptionID)
        {
            string key = "Cache_SurveyItemOption_Model_" + strSurveyItemOptionID;
            if (HttpRuntime.Cache[key] != null)
                return (SurveyItemOptionModel)HttpRuntime.Cache[key];
            else
            {
                SurveyItemOptionModel surChItModel = surChItDAL.GetInfo(strSurveyItemOptionID);
                CacheHelper.AddCache(key, surChItModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return surChItModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(SurveyItemOptionModel surChItModel)
        {
            surChItDAL.InsertInfo(surChItModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(SurveyItemOptionModel surChItModel, string strSurveyItemOptionID)
        {
            surChItDAL.UpdateInfo(surChItModel, strSurveyItemOptionID);
            string key = "Cache_SurveyItemOption_Model_" + strSurveyItemOptionID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strSurveyItemOptionID)
        {
            surChItDAL.DeleteInfo(strSurveyItemOptionID);
            string key = "Cache_SurveyItemOption_Model_" + strSurveyItemOptionID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID(string strSurveyItemID)
        {
            return surChItDAL.GetListID(strSurveyItemID);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strSurveyItemID, string strListID, string strOldListID)
        {
            surChItDAL.OrderInfo(strSurveyItemID, strListID, strOldListID);
        }
        #endregion

        #region 根据SurveyItemID删除信息
        /// <summary>
        /// 根据SurveyItemID删除信息
        /// </summary>
        public void DeleteInfoBySurveyItemID(string strSurveyItemID)
        {
            surChItDAL.DeleteInfoBySurveyItemID(strSurveyItemID);
        }
        #endregion

        #region 根据SurveyID删除信息
        /// <summary>
        /// 根据SurveyID删除信息
        /// </summary>
        public void DeleteInfoBySurveyID(string strSurveyID)
        {
            surChItDAL.DeleteInfoBySurveyID(strSurveyID);
        }
        #endregion
        
        #region 更新SurveyItemID
        /// <summary>
        /// 更新信息SurveyItemID
        /// </summary>
        public void UpdateSurveyItemID(string strSurveyItemID, string strAdminID)
        {
            surChItDAL.UpdateSurveyItemID(strSurveyItemID, strAdminID);
        }
        #endregion

    }
}
