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
    ///��Ʒ�ղ�-ҵ���߼���
    /// ������:��С��
    /// ����:2010-8-15
    /// </summary>

    public class CollectionBLL
    {

        private readonly CollectionDAL colDAL = new CollectionDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return colDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strCollectionID)
        {
            return colDAL.CheckInfo(strFieldName, strFieldValue, strCollectionID);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public CollectionModel GetInfo(string strCollectionID)
        {
            return colDAL.GetInfo(strCollectionID);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
        /// </summary>
        public CollectionModel GetCacheInfo(string strCollectionID)
        {
            string key = "Cache_Collection_Model_" + strCollectionID;
            if (HttpRuntime.Cache[key] != null)
                return (CollectionModel)HttpRuntime.Cache[key];
            else
            {
                CollectionModel colModel = colDAL.GetInfo(strCollectionID);
                CacheHelper.AddCache(key, colModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return colModel;
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(CollectionModel colModel)
        {
            colDAL.InsertInfo(colModel);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(CollectionModel colModel, string strCollectionID)
        {
            colDAL.UpdateInfo(colModel, strCollectionID);
            string key = "Cache_Collection_Model_" + strCollectionID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strCollectionID)
        {
            colDAL.DeleteInfo(strCollectionID);
            string key = "Cache_Collection_Model_" + strCollectionID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region �Ƿ��ղ�
        /// <summary>
        /// �Ƿ��ղ�
        /// </summary>
        public bool IsCollect(string strUrl, string strUserID)
        {
            return colDAL.IsCollect(strUrl, strUserID);
        }
        #endregion
    }
}
