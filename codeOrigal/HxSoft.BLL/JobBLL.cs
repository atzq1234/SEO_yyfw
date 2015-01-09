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
    ///��Ƹ��-ҵ���߼���
    /// ������:��С��
    /// ����:2010/11/2
    /// </summary>

    public class JobBLL
    {

        private readonly JobDAL jobDAL = new JobDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return jobDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strJobID)
        {
            return jobDAL.CheckInfo(strFieldName, strFieldValue, strJobID);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public JobModel GetInfo(string strJobID)
        {
            return jobDAL.GetInfo(strJobID);
        }
        /// <summary>
        /// ǰ̨��ȡ��Ϣ
        /// </summary>
        public JobModel GetInfo2(string strJobID)
        {
            return jobDAL.GetInfo2(strJobID);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
        /// </summary>
        public JobModel GetCacheInfo(string strJobID)
        {
            string key = "Cache_Job_Model_" + strJobID;
            if (HttpRuntime.Cache[key] != null)
                return (JobModel)HttpRuntime.Cache[key];
            else
            {
                JobModel JobModel = jobDAL.GetInfo(strJobID);
                CacheHelper.AddCache(key, JobModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return JobModel;
            }
        }
        /// <summary>
        /// ǰ̨�ӻ����ȡ��Ϣ
        /// </summary>
        public JobModel GetCacheInfo2(string strJobID)
        {
            string key = "Cache_Job_Model_" + strJobID;
            if (HttpRuntime.Cache[key] != null)
                return (JobModel)HttpRuntime.Cache[key];
            else
            {
                JobModel JobModel = jobDAL.GetInfo2(strJobID);
                CacheHelper.AddCache(key, JobModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return JobModel;
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(JobModel JobModel)
        {
            jobDAL.InsertInfo(JobModel);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(JobModel JobModel, string strJobID)
        {
            jobDAL.UpdateInfo(JobModel, strJobID);
            string key = "Cache_Job_Model_" + strJobID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strJobID)
        {
            jobDAL.DeleteInfo(strJobID);
            string key = "Cache_Job_Model_" + strJobID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ת����Ϣ
        /// <summary>
        /// ת����Ϣ
        /// </summary>
        public void TransferInfo(string strJobID, string strClassID)
        {
            jobDAL.TransferInfo(strJobID, strClassID);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strJobID, string strIsClose)
        {
            jobDAL.UpdateCloseStatus(strJobID, strIsClose);
            string key = "Cache_Job_Model_" + strJobID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ��ȡ�����
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public string GetListID()
        {
            return jobDAL.GetListID();
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            jobDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region ��������1
        /// <summary>
        /// ��������1
        /// </summary>
        public void Click(string strJobID)
        {
            jobDAL.Click(strJobID);
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strJobID)
        {
            return jobDAL.GetValueByField(strFieldName, strJobID);
        }
        #endregion

    }
}
