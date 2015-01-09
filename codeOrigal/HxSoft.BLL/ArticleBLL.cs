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
    ///���¹���-ҵ���߼���
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>

    public class ArticleBLL
    {

        private readonly ArticleDAL artDAL = new ArticleDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return artDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strArticleID)
        {
            return artDAL.CheckInfo(strFieldName, strFieldValue, strArticleID);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public ArticleModel GetInfo(string strArticleID)
        {
            return artDAL.GetInfo(strArticleID);
        }
        /// <summary>
        /// ǰ̨��ȡ��Ϣ
        /// </summary>
        public ArticleModel GetInfo2(string strArticleID)
        {
            return artDAL.GetInfo2(strArticleID);
        }

        public IList<ArticleModel> GetInfoList(string strClassID, int topnum)
        {
            return artDAL.GetInfoList(strClassID, topnum);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
        /// </summary>
        public ArticleModel GetCacheInfo(string strArticleID)
        {
            string key = "Cache_Article_Model_" + strArticleID;
            if (HttpRuntime.Cache[key] != null)
                return (ArticleModel)HttpRuntime.Cache[key];
            else
            {
                ArticleModel artModel = artDAL.GetInfo(strArticleID);
                CacheHelper.AddCache(key, artModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return artModel;
            }
        }
        /// <summary>
        /// ǰ̨�ӻ����ȡ��Ϣ
        /// </summary>
        public ArticleModel GetCacheInfo2(string strArticleID)
        {
            string key = "Cache_Article_Model_" + strArticleID;
            if (HttpRuntime.Cache[key] != null)
                return (ArticleModel)HttpRuntime.Cache[key];
            else
            {
                ArticleModel artModel = artDAL.GetInfo2(strArticleID);
                CacheHelper.AddCache(key, artModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return artModel;
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(ArticleModel artModel)
        {
            artDAL.InsertInfo(artModel);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(ArticleModel artModel, string strArticleID)
        {
            artDAL.UpdateInfo(artModel, strArticleID);
            string key = "Cache_Article_Model_" + strArticleID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strArticleID)
        {
            artDAL.DeleteInfo(strArticleID);
            string key = "Cache_Article_Model_" + strArticleID;
            CacheHelper.RemoveCache(key);

            ArticlePicDAL artPicDAL = new ArticlePicDAL();
            artPicDAL.DeleteInfoByArticleID(strArticleID);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strArticleID, string strIsClose)
        {
            artDAL.UpdateCloseStatus(strArticleID, strIsClose);
            string key = "Cache_Article_Model_" + strArticleID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ת����Ϣ
        /// <summary>
        /// ת����Ϣ
        /// </summary>
        public void TransferInfo(string strArticleID, string strClassID)
        {
            artDAL.TransferInfo(strArticleID, strClassID);
        }
        #endregion

        #region ȡ��һ����ϢID
        /// <summary>
        /// ȡ��һ����ϢID
        /// </summary>
        public string GetFirstID(string strClassID)
        {
            return artDAL.GetFirstID(strClassID);
        }
          #endregion

        #region ��������1
        /// <summary>
        /// ��������1
        /// </summary>
        public void Click(string strArticleID)
        {
            artDAL.Click(strArticleID);
        }
        #endregion

        #region ��ȡ�����
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public string GetListID()
        {
            return artDAL.GetListID();
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            artDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strArticleID)
        {
            return artDAL.GetValueByField(strFieldName, strArticleID);
        }
        #endregion

        #region ȡ��һƪ��ϢID
        /// <summary>
        /// ȡ��һƪ��ϢID
        /// </summary>
        public string GetPrevID(string strClassID, string strArticleID)
        {
            return artDAL.GetPrevID(strClassID, strArticleID);
        }
        #endregion

        #region ȡ��һƪ��ϢID
        /// <summary>
        /// ȡ��һƪ��ϢID
        /// </summary>
        public string GetNextID(string strClassID, string strArticleID)
        {
            return artDAL.GetNextID(strClassID, strArticleID);
        }
        #endregion

        #region RSS�ļ�
        /// <summary>
        /// RSS�ļ�
        /// </summary>
        /// <param name="strClassID"></param>
        /// <returns></returns>
        public StringBuilder RSS(string strClassID)
        {
            return artDAL.RSS(strClassID);
        }
        #endregion

    }
}
