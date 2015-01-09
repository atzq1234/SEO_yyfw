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
    ///管理员管理组管理-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class AdminInGroupBLL
    {

        private readonly AdminInGroupDAL admInGrDAL = new AdminInGroupDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strAdminID, string strAdminGroupID)
        {
            return admInGrDAL.CheckInfo(strAdminID, strAdminGroupID);
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(AdminInGroupModel admInGrModel)
        {
            //清除权限缓存
            string key = "Cache_AdminGroup_LimitValues_" + admInGrModel.AdminID;
            CacheHelper.RemoveCache(key);
            admInGrDAL.InsertInfo(admInGrModel);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strAdminID, string strAdminGroupID)
        {
            //清除权限缓存
            string key = "Cache_AdminGroup_LimitValues_" + strAdminID;
            CacheHelper.RemoveCache(key);
            admInGrDAL.DeleteInfo(strAdminID, strAdminGroupID);
        }

        /// <summary>
        /// 根据AdminID删除信息
        /// </summary>
        public void DeleteInfoByAdminID(string strAdminID)
        {
            //清除权限缓存
            string key = "Cache_AdminGroup_LimitValues_" + strAdminID;
            CacheHelper.RemoveCache(key);
            admInGrDAL.DeleteInfoByAdminID(strAdminID);
        }

        /// <summary>
        /// 根据AdminGroupID删除信息
        /// </summary>
        public void DeleteInfoByAdminGroupID(string strAdminGroupID)
        {
            //清除权限缓存
            admInGrDAL.RemoveLimitCache(strAdminGroupID);
            admInGrDAL.DeleteInfoByAdminGroupID(strAdminGroupID);
        }

        #endregion

        #region 清除管理组中的管理员权限缓存
        /// <summary>
        /// 清除管理组中的管理员权限缓存
        /// </summary>
        /// <param name="strAdminGroupID"></param>
        /// <returns></returns>
        public void RemoveLimitCache(string strAdminGroupID)
        {
            admInGrDAL.RemoveLimitCache(strAdminGroupID);
        }
        #endregion

    }
}
