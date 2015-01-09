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
    ///招聘表-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010/11/2
    /// </summary>

    public class JobBLL
    {

        private readonly JobDAL jobDAL = new JobDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return jobDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strJobID)
        {
            return jobDAL.CheckInfo(strFieldName, strFieldValue, strJobID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public JobModel GetInfo(string strJobID)
        {
            return jobDAL.GetInfo(strJobID);
        }
        /// <summary>
        /// 前台读取信息
        /// </summary>
        public JobModel GetInfo2(string strJobID)
        {
            return jobDAL.GetInfo2(strJobID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public JobModel GetCacheInfo(string strJobID)
        {
            string key = "Cache_Job_Model_" + strJobID;
            if (HttpRuntime.Cache[key] != null)
                return (JobModel)HttpRuntime.Cache[key];
            else
            {
                JobModel JobModel = jobDAL.GetInfo(strJobID);
                CacheHelper.AddCache(key, JobModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return JobModel;
            }
        }
        /// <summary>
        /// 前台从缓存读取信息
        /// </summary>
        public JobModel GetCacheInfo2(string strJobID)
        {
            string key = "Cache_Job_Model_" + strJobID;
            if (HttpRuntime.Cache[key] != null)
                return (JobModel)HttpRuntime.Cache[key];
            else
            {
                JobModel JobModel = jobDAL.GetInfo2(strJobID);
                CacheHelper.AddCache(key, JobModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return JobModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(JobModel JobModel)
        {
            jobDAL.InsertInfo(JobModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(JobModel JobModel, string strJobID)
        {
            jobDAL.UpdateInfo(JobModel, strJobID);
            string key = "Cache_Job_Model_" + strJobID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strJobID)
        {
            jobDAL.DeleteInfo(strJobID);
            string key = "Cache_Job_Model_" + strJobID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 转移信息
        /// <summary>
        /// 转移信息
        /// </summary>
        public void TransferInfo(string strJobID, string strClassID)
        {
            jobDAL.TransferInfo(strJobID, strClassID);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strJobID, string strIsClose)
        {
            jobDAL.UpdateCloseStatus(strJobID, strIsClose);
            string key = "Cache_Job_Model_" + strJobID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return jobDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            jobDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region 访问数加1
        /// <summary>
        /// 访问数加1
        /// </summary>
        public void Click(string strJobID)
        {
            jobDAL.Click(strJobID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strJobID)
        {
            return jobDAL.GetValueByField(strFieldName, strJobID);
        }
        #endregion

    }
}
