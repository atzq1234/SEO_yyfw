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
    ///��վ����-ҵ���߼���
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>

    public class ConfigBLL
    {

        private readonly ConfigDAL confDAL = new ConfigDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return confDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strConfigID)
        {
            return confDAL.CheckInfo(strFieldName, strFieldValue, strConfigID);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public ConfigModel GetInfo(string strConfigID)
        {
            return confDAL.GetInfo(strConfigID);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
        /// </summary>
        public ConfigModel GetCacheInfo(string strConfigID)
        {
            string key = "Cache_Config_Model_" + strConfigID;
            if (HttpRuntime.Cache[key] != null)
                return (ConfigModel)HttpRuntime.Cache[key];
            else
            {
                ConfigModel confModel = confDAL.GetInfo(strConfigID);
                CacheHelper.AddCache(key, confModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return confModel;
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(ConfigModel confModel)
        {
            confDAL.InsertInfo(confModel);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(ConfigModel confModel, string strConfigID)
        {
            confDAL.UpdateInfo(confModel, strConfigID);
            string key = "Cache_Config_Model_" + strConfigID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strConfigID)
        {
            confDAL.DeleteInfo(strConfigID);
            string key = "Cache_Config_Model_" + strConfigID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strConfigID)
        {
            return confDAL.GetValueByField(strFieldName, strConfigID);
        }
        #endregion

        #region ȡվ���б�
        /// <summary>
        /// ȡվ���б�(a)
        /// </summary>
        public StringBuilder GetConfigList(string strSeparator)
        {
            return confDAL.GetConfigList(strSeparator);
        }
        #endregion

        #region ��ȡ�����
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public string GetListID()
        {
            return confDAL.GetListID();
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            confDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region ȡĬ��վ��
        /// <summary>
        /// ȡĬ��վ��
        /// </summary>
        public string GetDefaultSiteDir()
        {
            return confDAL.GetDefaultSiteDir();
        }
        #endregion

    }
}
