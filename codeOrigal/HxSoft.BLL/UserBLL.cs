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
    ///会员管理-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class UserBLL
    {

        private readonly UserDAL userDAL = new UserDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return userDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strUserID)
        {
            return userDAL.CheckInfo(strFieldName, strFieldValue, strUserID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public UserModel GetInfo(string strUserID)
        {
            return userDAL.GetInfo(strUserID);
        }
        #endregion

        #region 读取信息(根据用户名)
        /// <summary>
        /// 读取信息(根据用户名)
        /// </summary>
        public UserModel GetInfoByUserName(string strUserName)
        {
            return userDAL.GetInfoByUserName(strUserName);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public UserModel GetCacheInfo(string strUserID)
        {
            string key = "Cache_User_Model_" + strUserID;
            if (HttpRuntime.Cache[key] != null)
                return (UserModel)HttpRuntime.Cache[key];
            else
            {
                UserModel userModel = userDAL.GetInfo(strUserID);
                CacheHelper.AddCache(key, userModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return userModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(UserModel userModel)
        {
            userDAL.InsertInfo(userModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(UserModel userModel, string strUserID)
        {
            userDAL.UpdateInfo(userModel, strUserID);
            string key = "Cache_User_Model_" + strUserID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strUserID)
        {
            userDAL.DeleteInfo(strUserID);
            string key = "Cache_User_Model_" + strUserID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strUserID, string strIsClose)
        {
            userDAL.UpdateCloseStatus(strUserID, strIsClose);
        }
        #endregion

        #region 更新审核状态
        /// <summary>
        /// 更新审核状态
        /// </summary>
        public void UpdateAuditStatus(string strUserID, string strIsAudit)
        {
            userDAL.UpdateAuditStatus(strUserID, strIsAudit);
        }
        #endregion

        #region 更新会员等级
        /// <summary>
        /// 更新会员等级
        /// </summary>
        public void UpdateUserRank(string strUserID, string strUserRankID)
        {
            userDAL.UpdateUserRank(strUserID, strUserRankID);
        }
        #endregion

        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        public UserModel Login(string strUserName, string strUserPass)
        {
            return userDAL.Login(strUserName, strUserPass);
        }
        #endregion

        #region 是否登录
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin()
        {
            return userDAL.IsLogin();
        }
        #endregion

        #region 登陆检查
        /// <summary>
        /// 登陆检查
        /// </summary>
        public void LoginChk()
        {
            string strReturnUrl = "/User/Login.aspx?Url=" + HttpContext.Current.Request.Url.ToString();
            if (!userDAL.IsLogin())
            {
                Config.MsgGotoUrl("请登录!", strReturnUrl);
            }
        }
        #endregion

        #region 会员更新资料
        /// <summary>
        /// 会员更新资料
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="strUserID"></param>
        public void UpdateInfoByUser(UserModel userModel, string strUserID)
        {
            userDAL.UpdateInfoByUser(userModel,strUserID);
        }
        #endregion

        #region 会员更新密码保护
        /// <summary>
        /// 会员更新密码保护
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="strUserID"></param>
        public void UpdatePassQuestion(UserModel userModel, string strUserID)
        {
            userDAL.UpdatePassQuestion(userModel, strUserID);
        }
        #endregion

        #region 会员修改密码
        /// <summary>
        /// 会员修改密码
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="strUserID"></param>
        public void SetPass(UserModel userModel, string strUserID)
        {
            userDAL.SetPass(userModel, strUserID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strUserID)
        {
            return userDAL.GetValueByField(strFieldName, strUserID);
        }
        #endregion

        #region 导出邮件地址
        /// <summary>
        /// 导出邮件地址
        /// </summary>
        public void EmailExport(string strSql, string strFilePath, string strFileName)
        {
            userDAL.EmailExport(strSql, strFilePath, strFileName);
        }
        #endregion

    }
}
