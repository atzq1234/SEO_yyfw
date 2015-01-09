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
    ///Ȩ���ֶ�-ҵ���߼���
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>

    public class LimitBLL
    {

        private readonly LimitDAL limDAL = new LimitDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return limDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strLimitID)
        {
            return limDAL.CheckInfo(strFieldName, strFieldValue, strLimitID);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public LimitModel GetInfo(string strLimitID)
        {
            return limDAL.GetInfo(strLimitID);
        }
        /// <summary>
        /// ����Ȩ��ֵ��ȡ��Ϣ
        /// </summary>
        public LimitModel GetInfoByLimitValue(string strLimitValue)
        {
            return limDAL.GetInfoByLimitValue(strLimitValue);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
        /// </summary>
        public LimitModel GetCacheInfo(string strLimitID)
        {
            string key = "Cache_Limit_Model_" + strLimitID;
            if (HttpRuntime.Cache[key] != null)
                return (LimitModel)HttpRuntime.Cache[key];
            else
            {
                LimitModel limModel = limDAL.GetInfo(strLimitID);
                CacheHelper.AddCache(key, limModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return limModel;
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(LimitModel limModel)
        {
            limDAL.InsertInfo(limModel);
            //�����Ӽ���
            limDAL.AddChildNum(limModel.ParentID);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strLimitID)
        {
            //ȡParentID
            LimitModel limModel = GetInfo(strLimitID);
            if (limModel != null)
            {
                //�����Ӽ���
                limDAL.CutChildNum(limModel.ParentID);
            }
            string key = "Cache_Limit_Model_" + strLimitID;
            CacheHelper.RemoveCache(key);
            limDAL.DeleteInfo(strLimitID);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strLimitID, string strIsClose)
        {
            limDAL.UpdateCloseStatus(strLimitID, strIsClose);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(LimitModel limModel, string strLimitID)
        {
            limDAL.UpdateInfo(limModel, strLimitID);
            string key = "Cache_Limit_Model_" + strLimitID;
            CacheHelper.RemoveCache(key);
        }
         #endregion

        #region �ƶ���Ϣ
        /// <summary>
        /// �ƶ���Ϣ
        /// </summary>
        public void MoveInfo(LimitModel limModel, string strLimitID)
        {
            limDAL.MoveInfo(limModel, strLimitID);
        }
        #endregion

        #region ��ȡ�����
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public string GetListID(string strParentID)
        {
            return limDAL.GetListID(strParentID);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strParentID, string strListID, string strOldListID)
        {
            limDAL.OrderInfo(strParentID,strListID,strOldListID);
        }
        #endregion

        #region ��ʾ���������б�
        /// <summary>
        /// ��ʾ���������б�
        /// </summary>
        public void ShowSelectTree(string strParentID, DropDownList drp, string strSql)
        {
            limDAL.ShowSelectTree(strParentID, 0, drp, strSql);
        }
        #endregion

        #region �ƶ�ʱ�����Ӽ���
        /// <summary>
        /// �޸�ʱ�����Ӽ���
        /// </summary>
        public void UpdateChildNum(string strParentID, string strOldParentID)
        {
            if (strParentID != strOldParentID)
            {
                limDAL.AddChildNum(strParentID);
                limDAL.CutChildNum(strOldParentID);
            }
        }
        #endregion

        #region ��ʾ·��
        /// <summary>
        /// ��ʾ·��
        /// </summary>
        public StringBuilder ShowPath(string strLimitID)
        {
            StringBuilder tempStr = new StringBuilder("�����");
            LimitModel limModel = new LimitModel();
            limModel = limDAL.GetInfo(strLimitID);
            if (limModel == null)
            {
                return tempStr;
            }
            else
            {
                string strPath = limDAL.GetPath(strLimitID).ToString();
                string[] arrPath = strPath.Split(new char[] { ',' });
                for (int i = 0; i < arrPath.Length; i++)
                {
                    LimitModel limModel_2 = new LimitModel();
                    limModel_2 = limDAL.GetInfo(arrPath[i]);
                    if (limModel_2 != null)
                    {
                        tempStr.Append(" > " + limModel_2.LimitField);
                    }
                }
                return tempStr;
            }
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strLimitID)
        {
            return limDAL.GetValueByField(strFieldName, strLimitID);
        }
        #endregion

        #region ����Ȩ��ֵȡ�ֶ�ֵ
        /// <summary>
        /// ����Ȩ��ֵȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByLimitValue(string strFieldName, string strLimitValue)
        {
            return limDAL.GetValueByLimitValue(strFieldName, strLimitValue);
        }
        #endregion

    }
}
