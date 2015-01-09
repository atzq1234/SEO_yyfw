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
    /// 会员日志管理-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class UserLogBLL
    {

        private readonly UserLogDAL admlogDAL = new UserLogDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return admlogDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strUserLogID)
        {
            return admlogDAL.CheckInfo(strFieldName, strFieldValue, strUserLogID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public UserLogModel GetInfo(string strUserLogID)
        {
            return admlogDAL.GetInfo(strUserLogID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public UserLogModel GetCacheInfo(string strUserLogID)
        {
            string key = "Cache_Log_Model_" + strUserLogID;
            if (HttpRuntime.Cache[key] != null)
                return (UserLogModel)HttpRuntime.Cache[key];
            else
            {
                UserLogModel admlogModel = admlogDAL.GetInfo(strUserLogID);
                CacheHelper.AddCache(key, admlogModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return admlogModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(UserLogModel admlogModel)
        {
            admlogDAL.InsertInfo(admlogModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(UserLogModel admlogModel, string strUserLogID)
        {
            admlogDAL.UpdateInfo(admlogModel, strUserLogID);
            string key = "Cache_Log_Model_" + strUserLogID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strUserLogID)
        {
            admlogDAL.DeleteInfo(strUserLogID);
            string key = "Cache_Log_Model_" + strUserLogID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 添加操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void InsertLog(string strLogContent, string strUserID)
        {
            UserLogModel admlogModel = new UserLogModel();
            admlogModel.LogContent = strLogContent;
            admlogModel.ScriptFile = HttpContext.Current.Request.FilePath;
            admlogModel.IpAddress = HttpContext.Current.Request.UserHostAddress;
            admlogModel.UserID = strUserID;
            admlogModel.AddTime = DateTime.Now.ToString();
            admlogDAL.InsertInfo(admlogModel);
        }
        #endregion

    }
}
