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
    ///��Ա�������-ҵ���߼���
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>

    public class UserRankBLL
    {

        private readonly UserRankDAL userRankDAL = new UserRankDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return userRankDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strUserRankID)
        {
            return userRankDAL.CheckInfo(strFieldName, strFieldValue, strUserRankID);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public UserRankModel GetInfo(string strUserRankID)
        {
            return userRankDAL.GetInfo(strUserRankID);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
        /// </summary>
        public UserRankModel GetCacheInfo(string strUserRankID)
        {
            string key = "Cache_UserRank_Model_" + strUserRankID;
            if (HttpRuntime.Cache[key] != null)
                return (UserRankModel)HttpRuntime.Cache[key];
            else
            {
                UserRankModel userRankModel = userRankDAL.GetInfo(strUserRankID);
                CacheHelper.AddCache(key, userRankModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return userRankModel;
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(UserRankModel userRankModel)
        {
            userRankDAL.InsertInfo(userRankModel);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(UserRankModel userRankModel, string strUserRankID)
        {
            userRankDAL.UpdateInfo(userRankModel, strUserRankID);
            string key = "Cache_UserRank_Model_" + strUserRankID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strUserRankID)
        {
            userRankDAL.DeleteInfo(strUserRankID);
            string key = "Cache_UserRank_Model_" + strUserRankID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strUserRankID, string strIsClose)
        {
            userRankDAL.UpdateCloseStatus(strUserRankID, strIsClose);
        }
        #endregion

        #region ���ò���Ȩ��
        /// <summary>
        /// ���ò���Ȩ��
        /// </summary>
        public void SetLimit(UserRankModel userRankModel, string strUserRankID)
        {
            userRankDAL.SetLimit(userRankModel, strUserRankID);
        }
        #endregion

        #region ��ȡ�����
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public string GetListID()
        {
            return userRankDAL.GetListID();
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            userRankDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region ȡȨ���ֶ�ֵ
        public StringBuilder GetLimitValues(string strUserID)
        {
            string key = "Cache_UserRank_LimitValues_" + strUserID;
            if (HttpRuntime.Cache[key] != null)
                return (StringBuilder)HttpRuntime.Cache[key];
            else
            {
                StringBuilder strLimitValues = userRankDAL.GetLimitValues(strUserID);
                CacheHelper.AddCache(key, strLimitValues, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return strLimitValues;
            }
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strUserRankID)
        {
            return userRankDAL.GetValueByField(strFieldName, strUserRankID);
        }
        #endregion


    }
}
