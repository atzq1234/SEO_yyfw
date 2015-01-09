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
    ///留言反馈-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class MessageBLL
    {

        private readonly MessageDAL mesDAL = new MessageDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return mesDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strMessageID)
        {
            return mesDAL.CheckInfo(strFieldName, strFieldValue, strMessageID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public MessageModel GetInfo(string strMessageID)
        {
            return mesDAL.GetInfo(strMessageID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public MessageModel GetCacheInfo(string strMessageID)
        {
            string key = "Cache_Message_Model_" + strMessageID;
            if (HttpRuntime.Cache[key] != null)
                return (MessageModel)HttpRuntime.Cache[key];
            else
            {
                MessageModel mesModel = mesDAL.GetInfo(strMessageID);
                CacheHelper.AddCache(key, mesModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return mesModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(MessageModel mesModel)
        {
            mesDAL.InsertInfo(mesModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(MessageModel mesModel, string strMessageID)
        {
            mesDAL.UpdateInfo(mesModel, strMessageID);
            string key = "Cache_Message_Model_" + strMessageID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strMessageID)
        {
            mesDAL.DeleteInfo(strMessageID);
            string key = "Cache_Message_Model_" + strMessageID;
            CacheHelper.RemoveCache(key);

            mesDAL.DeleteInfoByParentID(strMessageID);
        }
        #endregion

        #region 会员/管理员回复信息时更新主题回复状态
        /// <summary>
        /// 会员/管理员回复信息时更新主题回复状态
        /// </summary>
        public void UpdateReplyStatus(string strMessageID, string strIsReply)
        {
            mesDAL.UpdateReplyStatus(strMessageID, strIsReply);
        }
        #endregion

        #region 会员阅读回复内容时更新主题已读状态
        /// <summary>
        /// 会员阅读回复内容时更新主题已读状态
        /// </summary>
        public void UpdateReadStatus(string strMessageID, string strIsRead)
        {
            mesDAL.UpdateReadStatus(strMessageID, strIsRead);
        }
        #endregion

        #region 更新结束状态
        /// <summary>
        /// 更新结束状态
        /// </summary>
        public void UpdateEndStatus(string strMessageID, string strIsEnd)
        {
            mesDAL.UpdateEndStatus(strMessageID, strIsEnd);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strMessageID)
        {
            return mesDAL.GetValueByField(strFieldName, strMessageID);
        }
        #endregion


    }
}
