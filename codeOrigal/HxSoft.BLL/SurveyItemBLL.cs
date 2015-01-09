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
    ///调查选顶 -业务逻辑类
    /// 创建人:杨小明
    /// 日期:2011-12-26
    /// </summary>

    public class SurveyItemBLL
    {

        private readonly SurveyItemDAL surItDAL = new SurveyItemDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return surItDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strSurveyItemID)
        {
            return surItDAL.CheckInfo(strFieldName, strFieldValue, strSurveyItemID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strSurveyItemID)
        {
            return surItDAL.GetValueByField(strFieldName, strSurveyItemID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public SurveyItemModel GetInfo(string strSurveyItemID)
        {
            return surItDAL.GetInfo(strSurveyItemID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public SurveyItemModel GetCacheInfo(string strSurveyItemID)
        {
            string key = "Cache_SurveyItem_Model_" + strSurveyItemID;
            if (HttpRuntime.Cache[key] != null)
                return (SurveyItemModel)HttpRuntime.Cache[key];
            else
            {
                SurveyItemModel surItModel = surItDAL.GetInfo(strSurveyItemID);
                CacheHelper.AddCache(key, surItModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return surItModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(SurveyItemModel surItModel)
        {
            surItDAL.InsertInfo(surItModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(SurveyItemModel surItModel, string strSurveyItemID)
        {
            surItDAL.UpdateInfo(surItModel, strSurveyItemID);
            string key = "Cache_SurveyItem_Model_" + strSurveyItemID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strSurveyItemID)
        {
            surItDAL.DeleteInfo(strSurveyItemID);
            string key = "Cache_SurveyItem_Model_" + strSurveyItemID;
            CacheHelper.RemoveCache(key);

            SurveyItemOptionDAL surChItDAL = new SurveyItemOptionDAL();
            surChItDAL.DeleteInfoBySurveyItemID(strSurveyItemID);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strSurveyItemID, string strIsClose)
        {
            surItDAL.UpdateCloseStatus(strSurveyItemID, strIsClose);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID(string strSurveyID)
        {
            return surItDAL.GetListID(strSurveyID);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strSurveyID, string strListID, string strOldListID)
        {
            surItDAL.OrderInfo(strSurveyID, strListID, strOldListID);
        }
        #endregion

        #region 取SurveyItemID
        /// <summary>
        /// 取SurveyItemID
        /// </summary>
        public string GetSurveyItemID()
        {
            return surItDAL.GetSurveyItemID();
        }
        #endregion

        #region 根据SurveyID删除信息
        /// <summary>
        /// 根据SurveyID删除信息
        /// </summary>
        public void DeleteInfoBySurveyID(string strSurveyID)
        {
            surItDAL.DeleteInfoBySurveyID(strSurveyID);
        }
        #endregion

    }
}
