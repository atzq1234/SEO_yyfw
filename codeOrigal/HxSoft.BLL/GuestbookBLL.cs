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
    ///留言板-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2011-9-16
    /// </summary>

    public class GuestbookBLL
    {

        private readonly GuestbookDAL gbookDAL = new GuestbookDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return gbookDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strGuestbookID)
        {
            return gbookDAL.CheckInfo(strFieldName, strFieldValue, strGuestbookID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strGuestbookID)
        {
            return gbookDAL.GetValueByField(strFieldName, strGuestbookID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public GuestbookModel GetInfo(string strGuestbookID)
        {
            return gbookDAL.GetInfo(strGuestbookID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public GuestbookModel GetCacheInfo(string strGuestbookID)
        {
            string key = "Cache_Guestbook_Model_" + strGuestbookID;
            if (HttpRuntime.Cache[key] != null)
                return (GuestbookModel)HttpRuntime.Cache[key];
            else
            {
                GuestbookModel gbookModel = gbookDAL.GetInfo(strGuestbookID);
                CacheHelper.AddCache(key, gbookModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return gbookModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(GuestbookModel gbookModel)
        {
            gbookDAL.InsertInfo(gbookModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(GuestbookModel gbookModel, string strGuestbookID)
        {
            gbookDAL.UpdateInfo(gbookModel, strGuestbookID);
            string key = "Cache_Guestbook_Model_" + strGuestbookID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strGuestbookID)
        {
            gbookDAL.DeleteInfo(strGuestbookID);
            string key = "Cache_Guestbook_Model_" + strGuestbookID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strGuestbookID, string strIsClose)
        {
            gbookDAL.UpdateCloseStatus(strGuestbookID, strIsClose);
        }
        #endregion


    }
}
