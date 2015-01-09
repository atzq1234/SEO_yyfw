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
    ///���������-ҵ���߼���
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>

    public class AdminGroupBLL
    {

        private readonly AdminGroupDAL admGrDAL = new AdminGroupDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return admGrDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strAdminGroupID)
        {
            return admGrDAL.CheckInfo(strFieldName, strFieldValue, strAdminGroupID);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public AdminGroupModel GetInfo(string strAdminGroupID)
        {
            return admGrDAL.GetInfo(strAdminGroupID);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
        /// </summary>
        public AdminGroupModel GetCacheInfo(string strAdminGroupID)
        {
            string key = "Cache_AdminGroup_Model_" + strAdminGroupID;
            if (HttpRuntime.Cache[key] != null)
                return (AdminGroupModel)HttpRuntime.Cache[key];
            else
            {
                AdminGroupModel admGrModel = admGrDAL.GetInfo(strAdminGroupID);
                CacheHelper.AddCache(key, admGrModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return admGrModel;
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(AdminGroupModel admGrModel)
        {
            admGrDAL.InsertInfo(admGrModel);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(AdminGroupModel admGrModel, string strAdminGroupID)
        {
            admGrDAL.UpdateInfo(admGrModel, strAdminGroupID);
            string key = "Cache_AdminGroup_Model_" + strAdminGroupID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strAdminGroupID)
        {
            admGrDAL.DeleteInfo(strAdminGroupID);
            string key = "Cache_AdminGroup_Model_" + strAdminGroupID;
            CacheHelper.RemoveCache(key);

            AdminInGroupBLL admInGrBLL = new AdminInGroupBLL();
            admInGrBLL.DeleteInfoByAdminGroupID(strAdminGroupID);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strAdminGroupID, string strIsClose)
        {
            admGrDAL.UpdateCloseStatus(strAdminGroupID, strIsClose);
        }
        #endregion

        #region ���ò���Ȩ��
        /// <summary>
        /// ���ò���Ȩ��
        /// </summary>
        public void SetLimit(AdminGroupModel admGrModel, string strAdminGroupID)
        {
            admGrDAL.SetLimit(admGrModel, strAdminGroupID);
        }
        #endregion

        #region ��ȡ�����
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public string GetListID()
        {
            return admGrDAL.GetListID();
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            admGrDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region ȡȨ���ֶ�ֵ
        public StringBuilder GetLimitValues(string strAdminID)
        {
            string key = "Cache_AdminGroup_LimitValues_" + strAdminID;
            if (HttpRuntime.Cache[key] != null)
                return (StringBuilder)HttpRuntime.Cache[key];
            else
            {
                StringBuilder strLimitValues = admGrDAL.GetLimitValues(strAdminID);
                CacheHelper.AddCache(key, strLimitValues, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return strLimitValues;
            }
        }
        #endregion

        #region ȡ����Ա��
        public StringBuilder GetAdminGroupNames(string strAdminID)
        {
            return admGrDAL.GetAdminGroupNames(strAdminID);
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strAdminGroupID)
        {
            return admGrDAL.GetValueByField(strFieldName, strAdminGroupID);
        }
        #endregion


    }
}
