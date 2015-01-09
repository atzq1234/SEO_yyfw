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
    ///��������-ҵ���߼���
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>

    public class LinkBLL
    {

        private readonly LinkDAL linkDAL = new LinkDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return linkDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strLinkID)
        {
            return linkDAL.CheckInfo(strFieldName, strFieldValue, strLinkID);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public LinkModel GetInfo(string strLinkID)
        {
            return linkDAL.GetInfo(strLinkID);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
        /// </summary>
        public LinkModel GetCacheInfo(string strLinkID)
        {
            string key = "Cache_Link_Model_" + strLinkID;
            if (HttpRuntime.Cache[key] != null)
                return (LinkModel)HttpRuntime.Cache[key];
            else
            {
                LinkModel linkModel = linkDAL.GetInfo(strLinkID);
                CacheHelper.AddCache(key, linkModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return linkModel;
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(LinkModel linkModel)
        {
            linkDAL.InsertInfo(linkModel);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(LinkModel linkModel, string strLinkID)
        {
            linkDAL.UpdateInfo(linkModel, strLinkID);
            string key = "Cache_Link_Model_" + strLinkID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strLinkID)
        {
            linkDAL.DeleteInfo(strLinkID);
            string key = "Cache_Link_Model_" + strLinkID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strLinkID, string strIsClose)
        {
            linkDAL.UpdateCloseStatus(strLinkID, strIsClose);
        }
        #endregion

        #region ת����Ϣ
        /// <summary>
        /// ת����Ϣ
        /// </summary>
        public void TransferInfo(string strLinkID, string strConfigID)
        {
            linkDAL.TransferInfo(strLinkID, strConfigID);
        }
        #endregion

        #region ��ȡ�����
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public string GetListID()
        {
            return linkDAL.GetListID();
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            linkDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strLinkID)
        {
            return linkDAL.GetValueByField(strFieldName, strLinkID);
        }
        #endregion


    }
}
