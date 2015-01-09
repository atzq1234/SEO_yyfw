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
    ///聊天工具-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class ChatBLL
    {

        private readonly ChatDAL chaDAL = new ChatDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return chaDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strChatID)
        {
            return chaDAL.CheckInfo(strFieldName, strFieldValue, strChatID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public ChatModel GetInfo(string strChatID)
        {
            return chaDAL.GetInfo(strChatID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public ChatModel GetCacheInfo(string strChatID)
        {
            string key = "Cache_Chat_Model_" + strChatID;
            if (HttpRuntime.Cache[key] != null)
                return (ChatModel)HttpRuntime.Cache[key];
            else
            {
                ChatModel chaModel = chaDAL.GetInfo(strChatID);
                CacheHelper.AddCache(key, chaModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return chaModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(ChatModel chaModel)
        {
            chaDAL.InsertInfo(chaModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(ChatModel chaModel, string strChatID)
        {
            chaDAL.UpdateInfo(chaModel, strChatID);
            string key = "Cache_Chat_Model_" + strChatID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strChatID)
        {
            chaDAL.DeleteInfo(strChatID);
            string key = "Cache_Chat_Model_" + strChatID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strChatID, string strIsClose)
        {
            chaDAL.UpdateCloseStatus(strChatID, strIsClose);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return chaDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            chaDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strChatID)
        {
            return chaDAL.GetValueByField(strFieldName, strChatID);
        }
        #endregion

    }
}
