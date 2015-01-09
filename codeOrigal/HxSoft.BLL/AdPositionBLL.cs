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
    ///���λ����-ҵ���߼���
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>

    public class AdPositionBLL
    {

        private readonly AdPositionDAL adPosDAL = new AdPositionDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return adPosDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strAdPositionID)
        {
            return adPosDAL.CheckInfo(strFieldName, strFieldValue, strAdPositionID);
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strAdPositionID)
        {
            return adPosDAL.GetValueByField(strFieldName, strAdPositionID);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public AdPositionModel GetInfo(string strAdPositionID)
        {
            return adPosDAL.GetInfo(strAdPositionID);
        }

        /// <summary>
        /// ǰ̨��ȡ��Ϣ
        /// </summary>
        public AdPositionModel GetInfo2(string strAdPositionID)
        {
            return adPosDAL.GetInfo2(strAdPositionID);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
        /// </summary>
        public AdPositionModel GetCacheInfo(string strAdPositionID)
        {
            string key = "Cache_AdPosition_Model_" + strAdPositionID;
            if (HttpRuntime.Cache[key] != null)
                return (AdPositionModel)HttpRuntime.Cache[key];
            else
            {
                AdPositionModel adPosModel = adPosDAL.GetInfo(strAdPositionID);
                CacheHelper.AddCache(key, adPosModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return adPosModel;
            }
        }
        /// <summary>
        /// ǰ̨�ӻ����ȡ��Ϣ
        /// </summary>
        public AdPositionModel GetCacheInfo2(string strAdPositionID)
        {
            string key = "Cache_AdPosition_Model_" + strAdPositionID;
            if (HttpRuntime.Cache[key] != null)
                return (AdPositionModel)HttpRuntime.Cache[key];
            else
            {
                AdPositionModel adPosModel = adPosDAL.GetInfo2(strAdPositionID);
                CacheHelper.AddCache(key, adPosModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return adPosModel;
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(AdPositionModel adPosModel)
        {
            adPosDAL.InsertInfo(adPosModel);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(AdPositionModel adPosModel, string strAdPositionID)
        {
            adPosDAL.UpdateInfo(adPosModel, strAdPositionID);
            string key = "Cache_AdPosition_Model_" + strAdPositionID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strAdPositionID)
        {
            adPosDAL.DeleteInfo(strAdPositionID);
            string key = "Cache_AdPosition_Model_" + strAdPositionID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strAdPositionID, string strIsClose)
        {
            adPosDAL.UpdateCloseStatus(strAdPositionID,strIsClose);
            string key = "Cache_AdPosition_Model_" + strAdPositionID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ��ȡ�����
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public string GetListID()
        {
            return adPosDAL.GetListID();
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            adPosDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

    }
}
