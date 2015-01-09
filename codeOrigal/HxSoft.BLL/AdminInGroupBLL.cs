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
    ///����Ա���������-ҵ���߼���
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>

    public class AdminInGroupBLL
    {

        private readonly AdminInGroupDAL admInGrDAL = new AdminInGroupDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strAdminID, string strAdminGroupID)
        {
            return admInGrDAL.CheckInfo(strAdminID, strAdminGroupID);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(AdminInGroupModel admInGrModel)
        {
            //���Ȩ�޻���
            string key = "Cache_AdminGroup_LimitValues_" + admInGrModel.AdminID;
            CacheHelper.RemoveCache(key);
            admInGrDAL.InsertInfo(admInGrModel);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strAdminID, string strAdminGroupID)
        {
            //���Ȩ�޻���
            string key = "Cache_AdminGroup_LimitValues_" + strAdminID;
            CacheHelper.RemoveCache(key);
            admInGrDAL.DeleteInfo(strAdminID, strAdminGroupID);
        }

        /// <summary>
        /// ����AdminIDɾ����Ϣ
        /// </summary>
        public void DeleteInfoByAdminID(string strAdminID)
        {
            //���Ȩ�޻���
            string key = "Cache_AdminGroup_LimitValues_" + strAdminID;
            CacheHelper.RemoveCache(key);
            admInGrDAL.DeleteInfoByAdminID(strAdminID);
        }

        /// <summary>
        /// ����AdminGroupIDɾ����Ϣ
        /// </summary>
        public void DeleteInfoByAdminGroupID(string strAdminGroupID)
        {
            //���Ȩ�޻���
            admInGrDAL.RemoveLimitCache(strAdminGroupID);
            admInGrDAL.DeleteInfoByAdminGroupID(strAdminGroupID);
        }

        #endregion

        #region ����������еĹ���ԱȨ�޻���
        /// <summary>
        /// ����������еĹ���ԱȨ�޻���
        /// </summary>
        /// <param name="strAdminGroupID"></param>
        /// <returns></returns>
        public void RemoveLimitCache(string strAdminGroupID)
        {
            admInGrDAL.RemoveLimitCache(strAdminGroupID);
        }
        #endregion

    }
}
