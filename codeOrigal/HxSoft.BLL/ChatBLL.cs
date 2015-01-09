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
    ///���칤��-ҵ���߼���
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>

    public class ChatBLL
    {

        private readonly ChatDAL chaDAL = new ChatDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
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

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public ChatModel GetInfo(string strChatID)
        {
            return chaDAL.GetInfo(strChatID);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
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

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(ChatModel chaModel)
        {
            chaDAL.InsertInfo(chaModel);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(ChatModel chaModel, string strChatID)
        {
            chaDAL.UpdateInfo(chaModel, strChatID);
            string key = "Cache_Chat_Model_" + strChatID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strChatID)
        {
            chaDAL.DeleteInfo(strChatID);
            string key = "Cache_Chat_Model_" + strChatID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strChatID, string strIsClose)
        {
            chaDAL.UpdateCloseStatus(strChatID, strIsClose);
        }
        #endregion

        #region ��ȡ�����
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public string GetListID()
        {
            return chaDAL.GetListID();
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            chaDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strChatID)
        {
            return chaDAL.GetValueByField(strFieldName, strChatID);
        }
        #endregion

    }
}
