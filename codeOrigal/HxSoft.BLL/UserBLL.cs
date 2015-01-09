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
    ///��Ա����-ҵ���߼���
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>

    public class UserBLL
    {

        private readonly UserDAL userDAL = new UserDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return userDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strUserID)
        {
            return userDAL.CheckInfo(strFieldName, strFieldValue, strUserID);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public UserModel GetInfo(string strUserID)
        {
            return userDAL.GetInfo(strUserID);
        }
        #endregion

        #region ��ȡ��Ϣ(�����û���)
        /// <summary>
        /// ��ȡ��Ϣ(�����û���)
        /// </summary>
        public UserModel GetInfoByUserName(string strUserName)
        {
            return userDAL.GetInfoByUserName(strUserName);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
        /// </summary>
        public UserModel GetCacheInfo(string strUserID)
        {
            string key = "Cache_User_Model_" + strUserID;
            if (HttpRuntime.Cache[key] != null)
                return (UserModel)HttpRuntime.Cache[key];
            else
            {
                UserModel userModel = userDAL.GetInfo(strUserID);
                CacheHelper.AddCache(key, userModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return userModel;
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(UserModel userModel)
        {
            userDAL.InsertInfo(userModel);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(UserModel userModel, string strUserID)
        {
            userDAL.UpdateInfo(userModel, strUserID);
            string key = "Cache_User_Model_" + strUserID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strUserID)
        {
            userDAL.DeleteInfo(strUserID);
            string key = "Cache_User_Model_" + strUserID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strUserID, string strIsClose)
        {
            userDAL.UpdateCloseStatus(strUserID, strIsClose);
        }
        #endregion

        #region �������״̬
        /// <summary>
        /// �������״̬
        /// </summary>
        public void UpdateAuditStatus(string strUserID, string strIsAudit)
        {
            userDAL.UpdateAuditStatus(strUserID, strIsAudit);
        }
        #endregion

        #region ���»�Ա�ȼ�
        /// <summary>
        /// ���»�Ա�ȼ�
        /// </summary>
        public void UpdateUserRank(string strUserID, string strUserRankID)
        {
            userDAL.UpdateUserRank(strUserID, strUserRankID);
        }
        #endregion

        #region ��¼
        /// <summary>
        /// ��¼
        /// </summary>
        public UserModel Login(string strUserName, string strUserPass)
        {
            return userDAL.Login(strUserName, strUserPass);
        }
        #endregion

        #region �Ƿ��¼
        /// <summary>
        /// �Ƿ��¼
        /// </summary>
        public bool IsLogin()
        {
            return userDAL.IsLogin();
        }
        #endregion

        #region ��½���
        /// <summary>
        /// ��½���
        /// </summary>
        public void LoginChk()
        {
            string strReturnUrl = "/User/Login.aspx?Url=" + HttpContext.Current.Request.Url.ToString();
            if (!userDAL.IsLogin())
            {
                Config.MsgGotoUrl("���¼!", strReturnUrl);
            }
        }
        #endregion

        #region ��Ա��������
        /// <summary>
        /// ��Ա��������
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="strUserID"></param>
        public void UpdateInfoByUser(UserModel userModel, string strUserID)
        {
            userDAL.UpdateInfoByUser(userModel,strUserID);
        }
        #endregion

        #region ��Ա�������뱣��
        /// <summary>
        /// ��Ա�������뱣��
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="strUserID"></param>
        public void UpdatePassQuestion(UserModel userModel, string strUserID)
        {
            userDAL.UpdatePassQuestion(userModel, strUserID);
        }
        #endregion

        #region ��Ա�޸�����
        /// <summary>
        /// ��Ա�޸�����
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="strUserID"></param>
        public void SetPass(UserModel userModel, string strUserID)
        {
            userDAL.SetPass(userModel, strUserID);
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strUserID)
        {
            return userDAL.GetValueByField(strFieldName, strUserID);
        }
        #endregion

        #region �����ʼ���ַ
        /// <summary>
        /// �����ʼ���ַ
        /// </summary>
        public void EmailExport(string strSql, string strFilePath, string strFileName)
        {
            userDAL.EmailExport(strSql, strFilePath, strFileName);
        }
        #endregion

    }
}
