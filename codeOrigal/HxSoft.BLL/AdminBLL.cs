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
    ///����Ա����-ҵ���߼���
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>

    public class AdminBLL
    {

        private readonly AdminDAL admDAL = new AdminDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return admDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strAdminID)
        {
            return admDAL.CheckInfo(strFieldName, strFieldValue, strAdminID);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public AdminModel GetInfo(string strAdminID)
        {
            return admDAL.GetInfo(strAdminID);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
        /// </summary>
        public AdminModel GetCacheInfo(string strAdminID)
        {
            string key = "Cache_Admin_Model_" + strAdminID;
            if (HttpRuntime.Cache[key] != null)
                return (AdminModel)HttpRuntime.Cache[key];
            else
            {
                AdminModel admModel = admDAL.GetInfo(strAdminID);
                CacheHelper.AddCache(key, admModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return admModel;
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(AdminModel admModel)
        {
            admDAL.InsertInfo(admModel);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(AdminModel admModel, string strAdminID)
        {
            admDAL.UpdateInfo(admModel, strAdminID);
            string key = "Cache_Admin_Model_" + strAdminID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strAdminID)
        {
            admDAL.DeleteInfo(strAdminID);
            string key = "Cache_Admin_Model_" + strAdminID;
            CacheHelper.RemoveCache(key);

            AdminInGroupBLL admInGrBLL = new AdminInGroupBLL();
            admInGrBLL.DeleteInfoByAdminID(strAdminID);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strAdminID, string strIsClose)
        {
            admDAL.UpdateCloseStatus(strAdminID, strIsClose);
        }
        #endregion

        #region ��¼
        /// <summary>
        /// ��¼
        /// </summary>
        public bool Login(string strAdminName, string strAdminPass)
        {
            return admDAL.Login(strAdminName, strAdminPass);
        }
        #endregion

        #region �Ƿ��¼
        /// <summary>
        /// �Ƿ��¼
        /// </summary>
        public bool IsLogin()
        {
            return admDAL.IsLogin();
        }
        #endregion

        #region ��½���
        /// <summary>
        /// ��½���
        /// </summary>
        public void LoginChk()
        {
            string strReturnUrl = Config.AdminPath + "Login.aspx?Url=" + HttpContext.Current.Request.Url.ToString();
            if (!admDAL.IsLogin())
            {
                Config.MsgGotoUrl("��ݹ���,�����µ�¼!", strReturnUrl);
            }
        }
        #endregion

        #region �޸�����
        /// <summary>
        /// �޸�����
        /// </summary>
        public void ResetPassword(string strAdminID, string strAdminPass)
        {
            admDAL.ResetPassword(strAdminID, strAdminPass);
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strAdminID)
        {
            return admDAL.GetValueByField(strFieldName, strAdminID);
        }
        #endregion

    }
}
