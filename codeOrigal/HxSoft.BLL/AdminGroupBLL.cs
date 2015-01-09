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
    ///管理组管理-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class AdminGroupBLL
    {

        private readonly AdminGroupDAL admGrDAL = new AdminGroupDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return admGrDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strAdminGroupID)
        {
            return admGrDAL.CheckInfo(strFieldName, strFieldValue, strAdminGroupID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public AdminGroupModel GetInfo(string strAdminGroupID)
        {
            return admGrDAL.GetInfo(strAdminGroupID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public AdminGroupModel GetCacheInfo(string strAdminGroupID)
        {
            string key = "Cache_AdminGroup_Model_" + strAdminGroupID;
            if (HttpRuntime.Cache[key] != null)
                return (AdminGroupModel)HttpRuntime.Cache[key];
            else
            {
                AdminGroupModel admGrModel = admGrDAL.GetInfo(strAdminGroupID);
                CacheHelper.AddCache(key, admGrModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return admGrModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(AdminGroupModel admGrModel)
        {
            admGrDAL.InsertInfo(admGrModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(AdminGroupModel admGrModel, string strAdminGroupID)
        {
            admGrDAL.UpdateInfo(admGrModel, strAdminGroupID);
            string key = "Cache_AdminGroup_Model_" + strAdminGroupID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strAdminGroupID)
        {
            admGrDAL.DeleteInfo(strAdminGroupID);
            string key = "Cache_AdminGroup_Model_" + strAdminGroupID;
            CacheHelper.RemoveCache(key);

            AdminInGroupBLL admInGrBLL = new AdminInGroupBLL();
            admInGrBLL.DeleteInfoByAdminGroupID(strAdminGroupID);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strAdminGroupID, string strIsClose)
        {
            admGrDAL.UpdateCloseStatus(strAdminGroupID, strIsClose);
        }
        #endregion

        #region 设置操作权限
        /// <summary>
        /// 设置操作权限
        /// </summary>
        public void SetLimit(AdminGroupModel admGrModel, string strAdminGroupID)
        {
            admGrDAL.SetLimit(admGrModel, strAdminGroupID);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return admGrDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            admGrDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region 取权限字段值
        public StringBuilder GetLimitValues(string strAdminID)
        {
            string key = "Cache_AdminGroup_LimitValues_" + strAdminID;
            if (HttpRuntime.Cache[key] != null)
                return (StringBuilder)HttpRuntime.Cache[key];
            else
            {
                StringBuilder strLimitValues = admGrDAL.GetLimitValues(strAdminID);
                CacheHelper.AddCache(key, strLimitValues, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return strLimitValues;
            }
        }
        #endregion

        #region 取管理员组
        public StringBuilder GetAdminGroupNames(string strAdminID)
        {
            return admGrDAL.GetAdminGroupNames(strAdminID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strAdminGroupID)
        {
            return admGrDAL.GetValueByField(strFieldName, strAdminGroupID);
        }
        #endregion


    }
}
