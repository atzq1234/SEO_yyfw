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
    ///�����ֵ�-ҵ���߼���
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>

    public class DictionaryBLL
    {

        private readonly DictionaryDAL dictDAL = new DictionaryDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue, string strParentID)
        {
            return dictDAL.CheckInfo(strFieldName, strFieldValue, strParentID);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strParentID, string strDictionaryID)
        {
            return dictDAL.CheckInfo(strFieldName, strFieldValue, strParentID, strDictionaryID);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public DictionaryModel GetInfo(string strDictionaryID)
        {
            return dictDAL.GetInfo(strDictionaryID);
        }
        /// <summary>
        /// ǰ̨��ȡ��Ϣ
        /// </summary>
        public DictionaryModel GetInfo2(string strDictionaryID)
        {
            return dictDAL.GetInfo2(strDictionaryID);
        }

        public IList<DictionaryModel> GetInfoListByParentID(string strParentID)
        {
            return dictDAL.GetInfoListByParentID(strParentID);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
        /// </summary>
        public DictionaryModel GetCacheInfo(string strDictionaryID)
        {
            string key = "Cache_Dictionary_Model_" + strDictionaryID;
            if (HttpRuntime.Cache[key] != null)
                return (DictionaryModel)HttpRuntime.Cache[key];
            else
            {
                DictionaryModel dictModel = dictDAL.GetInfo(strDictionaryID);
                CacheHelper.AddCache(key, dictModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return dictModel;
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(DictionaryModel dictModel)
        {
            dictDAL.InsertInfo(dictModel);
            //�����Ӽ���
            dictDAL.AddChildNum(dictModel.ParentID);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(DictionaryModel dictModel, string strDictionaryID)
        {
            dictDAL.UpdateInfo(dictModel, strDictionaryID);
            string key = "Cache_Dictionary_Model_" + strDictionaryID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strDictionaryID)
        {
            //ȡParentID
            DictionaryModel dictModel = GetInfo(strDictionaryID);
            if (dictModel != null)
            {
                //�����Ӽ���
                dictDAL.CutChildNum(dictModel.ParentID);
            }
            string key = "Cache_Dictionary_Model_" + strDictionaryID;
            CacheHelper.RemoveCache(key);
            dictDAL.DeleteInfo(strDictionaryID);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strDictionaryID, string strIsClose)
        {
            dictDAL.UpdateCloseStatus(strDictionaryID, strIsClose);
        }
        #endregion

        #region �ƶ���Ϣ
        /// <summary>
        /// �ƶ���Ϣ
        /// </summary>
        public void MoveInfo(DictionaryModel dictModel, string strDictionaryID)
        {
            dictDAL.MoveInfo(dictModel, strDictionaryID);
        }
        #endregion

        #region ��ȡ�����
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public string GetListID(string strParentID)
        {
            return dictDAL.GetListID(strParentID);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strParentID, string strListID, string strOldListID)
        {
            dictDAL.OrderInfo(strParentID, strListID, strOldListID);
        }
        #endregion

        #region ��ʾ���������б�
        /// <summary>
        /// ��ʾ���������б�
        /// </summary>
        public void ShowSelectTree(string strParentID, DropDownList drp, string strSql)
        {
            dictDAL.ShowSelectTree(strParentID, 0, drp, strSql);
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
                dictDAL.AddChildNum(strParentID);
                dictDAL.CutChildNum(strOldParentID);
            }
        }
        #endregion

        #region ��ʾ·��
        /// <summary>
        /// ��ʾ·��
        /// </summary>
        public StringBuilder ShowPath(string strDictionaryID)
        {
            StringBuilder tempStr = new StringBuilder("�����");
            DictionaryModel dictModel = new DictionaryModel();
            dictModel = dictDAL.GetInfo(strDictionaryID);
            if (dictModel == null)
            {
                return tempStr;
            }
            else
            {
                string strPath = dictDAL.GetPath(strDictionaryID).ToString();
                string[] arrPath = strPath.Split(new char[] { ',' });
                for (int i = 0; i < arrPath.Length; i++)
                {
                    DictionaryModel dictModel_2 = new DictionaryModel();
                    dictModel_2 = dictDAL.GetInfo(arrPath[i]);
                    if (dictModel_2 != null)
                    {
                        tempStr.Append(" > " + dictModel_2.DictionaryName);
                    }
                }
                return tempStr;
            }
        }
        #endregion

        #region ȡ��һ�����ID
        /// <summary>
        /// ȡ��һ�����ID
        /// </summary>
        public string GetFirstDictionaryID(string strParentID)
        {
            return dictDAL.GetFirstDictionaryID(strParentID);
        }
        #endregion

        #region ȡ�ӷ���ID
        /// <summary>
        /// ȡ�ӷ���ID
        /// </summary>
        public StringBuilder GetSubDictionaryID(string strParentID)
        {
            StringBuilder strTemp = new StringBuilder();
            strTemp.Append("-1,");
            strTemp.Append(dictDAL.GetSubDictionaryID(strParentID));
            strTemp.Append("-1");
            return strTemp;
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strDictionaryID)
        {
            return dictDAL.GetValueByField(strFieldName,strDictionaryID);
        }
        #endregion

        #region Ajaxȡ���
        /// <summary>
        /// Ajaxȡ���
        /// </summary>
        /// <param name="strParentID"></param>
        /// <param name="strType">select,checkbox,radio</param>
        /// <param name="strObjName"></param>
        /// <param name="intValType">0��1,0ȡID,1ȡVal</param>
        /// <returns></returns>
        public StringBuilder AjaxGetDictionaryList(string strParentID, string strType, string strObjName, int intValType)
        {
            return dictDAL.AjaxGetDictionaryList(strParentID, strType, strObjName, intValType);
        }
        #endregion

        #region ���������ID��SQL
        /// <summary>
        /// ��������ĿID��SQL
        /// </summary>
        public string GetSubDictionarySql(string strDictionaryID)
        {
            return GetSubDictionarySql(strDictionaryID);
        }
        #endregion

    }
}
