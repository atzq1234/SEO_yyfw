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
    ///��ҵ����-ҵ���߼���
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>

    public class IndustryBLL
    {

        private readonly IndustryDAL indDAL = new IndustryDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue, string strParentID)
        {
            return indDAL.CheckInfo(strFieldName, strFieldValue, strParentID);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strParentID, string strIndustryID)
        {
            return indDAL.CheckInfo(strFieldName, strFieldValue, strParentID, strIndustryID);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public IndustryModel GetInfo(string strIndustryID)
        {
            return indDAL.GetInfo(strIndustryID);
        }
        /// <summary>
        /// ǰ̨��ȡ��Ϣ
        /// </summary>
        public IndustryModel GetInfo2(string strIndustryID)
        {
            return indDAL.GetInfo2(strIndustryID);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
        /// </summary>
        public IndustryModel GetCacheInfo(string strIndustryID)
        {
            string key = "Cache_Industry_Model_" + strIndustryID;
            if (HttpRuntime.Cache[key] != null)
                return (IndustryModel)HttpRuntime.Cache[key];
            else
            {
                IndustryModel indModel = indDAL.GetInfo(strIndustryID);
                CacheHelper.AddCache(key, indModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return indModel;
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(IndustryModel indModel)
        {
            indDAL.InsertInfo(indModel);
            //�����Ӽ���
            indDAL.AddChildNum(indModel.ParentID);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(IndustryModel indModel, string strIndustryID)
        {
            indDAL.UpdateInfo(indModel, strIndustryID);
            string key = "Cache_Industry_Model_" + strIndustryID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strIndustryID)
        {
            //ȡParentID
            IndustryModel indModel = GetInfo(strIndustryID);
            if (indModel != null)
            {
                //�����Ӽ���
                indDAL.CutChildNum(indModel.ParentID);
            }
            string key = "Cache_Industry_Model_" + strIndustryID;
            CacheHelper.RemoveCache(key);
            indDAL.DeleteInfo(strIndustryID);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strIndustryID, string strIsClose)
        {
            indDAL.UpdateCloseStatus(strIndustryID, strIsClose);
        }
        #endregion

        #region �ƶ���Ϣ
        /// <summary>
        /// �ƶ���Ϣ
        /// </summary>
        public void MoveInfo(IndustryModel indModel, string strIndustryID)
        {
            indDAL.MoveInfo(indModel, strIndustryID);
        }
        #endregion

        #region ��ȡ�����
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public string GetListID(string strParentID)
        {
            return indDAL.GetListID(strParentID);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strParentID, string strListID, string strOldListID)
        {
            indDAL.OrderInfo(strParentID,strListID,strOldListID);
        }
        #endregion

        #region ��ʾ���������б�
        /// <summary>
        /// ��ʾ���������б�
        /// </summary>
        public void ShowSelectTree(string strParentID, DropDownList drp, string strSql)
        {
            indDAL.ShowSelectTree(strParentID, 0, drp, strSql);
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
                indDAL.AddChildNum(strParentID);
                indDAL.CutChildNum(strOldParentID);
            }
        }
        #endregion

        #region ��ʾ·��
        /// <summary>
        /// ��ʾ·��
        /// </summary>
        public StringBuilder ShowPath(string strIndustryID)
        {
            StringBuilder tempStr = new StringBuilder("�����");
            IndustryModel indModel = new IndustryModel();
            indModel = indDAL.GetInfo(strIndustryID);
            if (indModel == null)
            {
                return tempStr;
            }
            else
            {
                string strPath = indDAL.GetPath(strIndustryID).ToString();
                string[] arrPath = strPath.Split(new char[] { ',' });
                for (int i = 0; i < arrPath.Length; i++)
                {
                    IndustryModel indModel_2 = new IndustryModel();
                    indModel_2 = indDAL.GetInfo(arrPath[i]);
                    if (indModel_2 != null)
                    {
                        tempStr.Append(" > " + indModel_2.IndustryName);
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
        public string GetFirstIndustryID(string strParentID)
        {
            return indDAL.GetFirstIndustryID(strParentID);
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strIndustryID)
        {
            return indDAL.GetValueByField(strFieldName, strIndustryID);
        }
        #endregion

        #region Ajaxȡ���
        /// <summary>
        /// Ajaxȡ���
        /// </summary>
        /// <param name="strParentID"></param>
        /// <param name="strType">select,checkbox,radio</param>
        /// <param name="strObjName"></param>
        /// <returns></returns>
        public StringBuilder AjaxGetIndustryList(string strParentID, string strType, string strObjName)
        {
            return indDAL.AjaxGetIndustryList(strParentID, strType, strObjName);
        }
        #endregion

    }
}
