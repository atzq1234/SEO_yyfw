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

    public class AreaBLL
    {

        private readonly AreaDAL areaDAL = new AreaDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue, string strParentID)
        {
            return areaDAL.CheckInfo(strFieldName, strFieldValue, strParentID);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strParentID, string strAreaID)
        {
            return areaDAL.CheckInfo(strFieldName, strFieldValue, strParentID, strAreaID);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public AreaModel GetInfo(string strAreaID)
        {
            return areaDAL.GetInfo(strAreaID);
        }
        /// <summary>
        /// ǰ̨��ȡ��Ϣ
        /// </summary>
        public AreaModel GetInfo2(string strAreaID)
        {
            return areaDAL.GetInfo2(strAreaID);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
        /// </summary>
        public AreaModel GetCacheInfo(string strAreaID)
        {
            string key = "Cache_Area_Model_" + strAreaID;
            if (HttpRuntime.Cache[key] != null)
                return (AreaModel)HttpRuntime.Cache[key];
            else
            {
                AreaModel claModel = areaDAL.GetInfo(strAreaID);
                CacheHelper.AddCache(key, claModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return claModel;
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(AreaModel claModel)
        {
            areaDAL.InsertInfo(claModel);
            //�����Ӽ���
            areaDAL.AddChildNum(claModel.ParentID);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(AreaModel claModel, string strAreaID)
        {
            areaDAL.UpdateInfo(claModel, strAreaID);
            string key = "Cache_Area_Model_" + strAreaID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strAreaID)
        {
            //ȡParentID
            AreaModel claModel = GetInfo(strAreaID);
            if (claModel != null)
            {
                //�����Ӽ���
                areaDAL.CutChildNum(claModel.ParentID);
            }
            string key = "Cache_Area_Model_" + strAreaID;
            CacheHelper.RemoveCache(key);
            areaDAL.DeleteInfo(strAreaID);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strAreaID, string strIsClose)
        {
            areaDAL.UpdateCloseStatus(strAreaID, strIsClose);
        }
        #endregion

        #region �ƶ���Ϣ
        /// <summary>
        /// �ƶ���Ϣ
        /// </summary>
        public void MoveInfo(AreaModel claModel, string strAreaID)
        {
            areaDAL.MoveInfo(claModel, strAreaID);
        }
        #endregion

        #region ��ȡ�����
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public string GetListID(string strParentID)
        {
            return areaDAL.GetListID(strParentID);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strParentID, string strListID, string strOldListID)
        {
            areaDAL.OrderInfo(strParentID, strListID, strOldListID);
        }
        #endregion

        #region ��ʾ���������б�
        /// <summary>
        /// ��ʾ���������б�
        /// </summary>
        public void ShowSelectTree(string strParentID, DropDownList drp, string strSql)
        {
            areaDAL.ShowSelectTree(strParentID, 0, drp, strSql);
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
                areaDAL.AddChildNum(strParentID);
                areaDAL.CutChildNum(strOldParentID);
            }
        }
        #endregion

        #region ��ʾ·��
        /// <summary>
        /// ��ʾ·��
        /// </summary>
        public StringBuilder ShowPath(string strAreaID)
        {
            StringBuilder tempStr = new StringBuilder("�����");
            AreaModel claModel = new AreaModel();
            claModel = areaDAL.GetInfo(strAreaID);
            if (claModel == null)
            {
                return tempStr;
            }
            else
            {
                string strPath = areaDAL.GetPath(strAreaID).ToString();
                string[] arrPath = strPath.Split(new char[] { ',' });
                for (int i = 0; i < arrPath.Length; i++)
                {
                    AreaModel claModel_2 = new AreaModel();
                    claModel_2 = areaDAL.GetInfo(arrPath[i]);
                    if (claModel_2 != null)
                    {
                        tempStr.Append(" > " + claModel_2.AreaName);
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
        public string GetFirstAreaID(string strParentID)
        {
            return areaDAL.GetFirstAreaID(strParentID);
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strAreaID)
        {
            return areaDAL.GetValueByField(strFieldName, strAreaID);
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
        public StringBuilder AjaxGetAreaList(string strParentID, string strType, string strObjName)
        {
            return areaDAL.AjaxGetAreaList(strParentID, strType, strObjName);
        }
        #endregion


    }
}
