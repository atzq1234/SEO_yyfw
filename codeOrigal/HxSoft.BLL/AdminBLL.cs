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
    ///管理员管理-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class AdminBLL
    {

        private readonly AdminDAL admDAL = new AdminDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return admDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strAdminID)
        {
            return admDAL.CheckInfo(strFieldName, strFieldValue, strAdminID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public AdminModel GetInfo(string strAdminID)
        {
            return admDAL.GetInfo(strAdminID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public AdminModel GetCacheInfo(string strAdminID)
        {
            string key = "Cache_Admin_Model_" + strAdminID;
            if (HttpRuntime.Cache[key] != null)
                return (AdminModel)HttpRuntime.Cache[key];
            else
            {
                AdminModel admModel = admDAL.GetInfo(strAdminID);
                CacheHelper.AddCache(key, admModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return admModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(AdminModel admModel)
        {
            admDAL.InsertInfo(admModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(AdminModel admModel, string strAdminID)
        {
            admDAL.UpdateInfo(admModel, strAdminID);
            string key = "Cache_Admin_Model_" + strAdminID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strAdminID)
        {
            admDAL.DeleteInfo(strAdminID);
            string key = "Cache_Admin_Model_" + strAdminID;
            CacheHelper.RemoveCache(key);

            AdminInGroupBLL admInGrBLL = new AdminInGroupBLL();
            admInGrBLL.DeleteInfoByAdminID(strAdminID);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strAdminID, string strIsClose)
        {
            admDAL.UpdateCloseStatus(strAdminID, strIsClose);
        }
        #endregion

        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        public bool Login(string strAdminName, string strAdminPass)
        {
            return admDAL.Login(strAdminName, strAdminPass);
        }
        #endregion

        #region 是否登录
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin()
        {
            return admDAL.IsLogin();
        }
        #endregion

        #region 登陆检查
        /// <summary>
        /// 登陆检查
        /// </summary>
        public void LoginChk()
        {
            string strReturnUrl = Config.AdminPath + "Login.aspx?Url=" + HttpContext.Current.Request.Url.ToString();
            if (!admDAL.IsLogin())
            {
                Config.MsgGotoUrl("身份过期,请重新登录!", strReturnUrl);
            }
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        public void ResetPassword(string strAdminID, string strAdminPass)
        {
            admDAL.ResetPassword(strAdminID, strAdminPass);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strAdminID)
        {
            return admDAL.GetValueByField(strFieldName, strAdminID);
        }
        #endregion

    }
}
