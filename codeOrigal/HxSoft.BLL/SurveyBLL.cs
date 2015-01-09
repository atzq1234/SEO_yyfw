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
    ///在线调查-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2011-12-26
    /// </summary>

    public class SurveyBLL
    {

        private readonly SurveyDAL surDAL = new SurveyDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return surDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strSurveyID)
        {
            return surDAL.CheckInfo(strFieldName, strFieldValue, strSurveyID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strSurveyID)
        {
            return surDAL.GetValueByField(strFieldName, strSurveyID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public SurveyModel GetInfo(string strSurveyID)
        {
            return surDAL.GetInfo(strSurveyID);
        }
        /// <summary>
        /// 前台读取信息
        /// </summary>
        public SurveyModel GetInfo2(string strSurveyID)
        {
            return surDAL.GetInfo2(strSurveyID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public SurveyModel GetCacheInfo(string strSurveyID)
        {
            string key = "Cache_Survey_Model_" + strSurveyID;
            if (HttpRuntime.Cache[key] != null)
                return (SurveyModel)HttpRuntime.Cache[key];
            else
            {
                SurveyModel surModel = surDAL.GetInfo(strSurveyID);
                CacheHelper.AddCache(key, surModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return surModel;
            }
        }
        /// <summary>
        /// 前台从缓存读取信息
        /// </summary>
        public SurveyModel GetCacheInfo2(string strSurveyID)
        {
            string key = "Cache_Survey_Model_" + strSurveyID;
            if (HttpRuntime.Cache[key] != null)
                return (SurveyModel)HttpRuntime.Cache[key];
            else
            {
                SurveyModel surModel = surDAL.GetInfo2(strSurveyID);
                CacheHelper.AddCache(key, surModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return surModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(SurveyModel surModel)
        {
            surDAL.InsertInfo(surModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(SurveyModel surModel, string strSurveyID)
        {
            surDAL.UpdateInfo(surModel, strSurveyID);
            string key = "Cache_Survey_Model_" + strSurveyID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strSurveyID)
        {
            surDAL.DeleteInfo(strSurveyID);
            string key = "Cache_Survey_Model_" + strSurveyID;
            CacheHelper.RemoveCache(key);

            SurveyItemOptionDAL surChItDAL = new SurveyItemOptionDAL();
            surChItDAL.DeleteInfoBySurveyID(strSurveyID);

            SurveyItemDAL surItDAL = new SurveyItemDAL();
            surItDAL.DeleteInfoBySurveyID(strSurveyID);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strSurveyID, string strIsClose)
        {
            surDAL.UpdateCloseStatus(strSurveyID, strIsClose);
        }
        #endregion

        #region 转移信息
        /// <summary>
        /// 转移信息
        /// </summary>
        public void TransferInfo(string strSurveyID, string strClassID)
        {
            surDAL.TransferInfo(strSurveyID, strClassID);
        }
        #endregion

        #region 访问数加1
        /// <summary>
        /// 访问数加1
        /// </summary>
        public void Click(string strSurveyID)
        {
            surDAL.Click(strSurveyID);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return surDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            surDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion
    }
}
