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
    ///信息反馈-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2011-2-28
    /// </summary>

    public class FeedbackBLL
    {

        private readonly FeedbackDAL feeDAL = new FeedbackDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return feeDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strFeedbackID)
        {
            return feeDAL.CheckInfo(strFieldName, strFieldValue, strFeedbackID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public FeedbackModel GetInfo(string strFeedbackID)
        {
            return feeDAL.GetInfo(strFeedbackID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public FeedbackModel GetCacheInfo(string strFeedbackID)
        {
            string key = "Cache_Feedback_Model_" + strFeedbackID;
            if (HttpRuntime.Cache[key] != null)
                return (FeedbackModel)HttpRuntime.Cache[key];
            else
            {
                FeedbackModel feeModel = feeDAL.GetInfo(strFeedbackID);
                CacheHelper.AddCache(key, feeModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return feeModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(FeedbackModel feeModel)
        {
            feeDAL.InsertInfo(feeModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(FeedbackModel feeModel, string strFeedbackID)
        {
            feeDAL.UpdateInfo(feeModel, strFeedbackID);
            string key = "Cache_Feedback_Model_" + strFeedbackID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strFeedbackID)
        {
            feeDAL.DeleteInfo(strFeedbackID);
            string key = "Cache_Feedback_Model_" + strFeedbackID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 处理信息
        /// <summary>
        /// 处理信息
        /// </summary>
        public void DealInfo(FeedbackModel feeModel, string strFeedbackID)
        {
            feeDAL.DealInfo(feeModel, strFeedbackID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strFeedbackID)
        {
            return feeDAL.GetValueByField(strFieldName, strFeedbackID);
        }
        #endregion

    }
}
