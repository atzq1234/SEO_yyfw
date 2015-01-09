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
    /// ����Ա��־����-ҵ���߼���
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>

    public class AdminLogBLL
    {

        private readonly AdminLogDAL admlogDAL = new AdminLogDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return admlogDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strAdminLogID)
        {
            return admlogDAL.CheckInfo(strFieldName, strFieldValue, strAdminLogID);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public AdminLogModel GetInfo(string strAdminLogID)
        {
            return admlogDAL.GetInfo(strAdminLogID);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
        /// </summary>
        public AdminLogModel GetCacheInfo(string strAdminLogID)
        {
            string key = "Cache_Log_Model_" + strAdminLogID;
            if (HttpRuntime.Cache[key] != null)
                return (AdminLogModel)HttpRuntime.Cache[key];
            else
            {
                AdminLogModel admlogModel = admlogDAL.GetInfo(strAdminLogID);
                CacheHelper.AddCache(key, admlogModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return admlogModel;
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(AdminLogModel admlogModel)
        {
            admlogDAL.InsertInfo(admlogModel);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(AdminLogModel admlogModel, string strAdminLogID)
        {
            admlogDAL.UpdateInfo(admlogModel, strAdminLogID);
            string key = "Cache_Log_Model_" + strAdminLogID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strAdminLogID)
        {
            admlogDAL.DeleteInfo(strAdminLogID);
            string key = "Cache_Log_Model_" + strAdminLogID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ��Ӳ�����־
        /// <summary>
        /// ��Ӳ�����־
        /// </summary>
        public void InsertLog(string strLogContent, string strAdminID)
        {
            AdminLogModel admlogModel = new AdminLogModel();
            admlogModel.LogContent = strLogContent;
            admlogModel.ScriptFile = HttpContext.Current.Request.FilePath;
            admlogModel.IpAddress = HttpContext.Current.Request.UserHostAddress;
            admlogModel.AdminID = strAdminID;
            admlogModel.AddTime = DateTime.Now.ToString();
            admlogDAL.InsertInfo(admlogModel);
        }
        #endregion

    }
}
