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
    /// 管理员日志管理-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class AdminLogBLL
    {

        private readonly AdminLogDAL admlogDAL = new AdminLogDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return admlogDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strAdminLogID)
        {
            return admlogDAL.CheckInfo(strFieldName, strFieldValue, strAdminLogID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public AdminLogModel GetInfo(string strAdminLogID)
        {
            return admlogDAL.GetInfo(strAdminLogID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public AdminLogModel GetCacheInfo(string strAdminLogID)
        {
            string key = "Cache_Log_Model_" + strAdminLogID;
            if (HttpRuntime.Cache[key] != null)
                return (AdminLogModel)HttpRuntime.Cache[key];
            else
            {
                AdminLogModel admlogModel = admlogDAL.GetInfo(strAdminLogID);
                CacheHelper.AddCache(key, admlogModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return admlogModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(AdminLogModel admlogModel)
        {
            admlogDAL.InsertInfo(admlogModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(AdminLogModel admlogModel, string strAdminLogID)
        {
            admlogDAL.UpdateInfo(admlogModel, strAdminLogID);
            string key = "Cache_Log_Model_" + strAdminLogID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strAdminLogID)
        {
            admlogDAL.DeleteInfo(strAdminLogID);
            string key = "Cache_Log_Model_" + strAdminLogID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 添加操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void InsertLog(string strLogContent, string strAdminID)
        {
            AdminLogModel admlogModel = new AdminLogModel();
            admlogModel.LogContent = strLogContent;
            admlogModel.ScriptFile = HttpContext.Current.Request.FilePath;
            admlogModel.IpAddress = HttpContext.Current.Request.UserHostAddress;
            admlogModel.AdminID = strAdminID;
            admlogModel.AddTime = DateTime.Now.ToString();
            admlogDAL.InsertInfo(admlogModel);
        }
        #endregion

    }
}
