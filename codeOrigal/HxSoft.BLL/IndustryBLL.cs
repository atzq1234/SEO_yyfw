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
    ///行业管理-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class IndustryBLL
    {

        private readonly IndustryDAL indDAL = new IndustryDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
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

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public IndustryModel GetInfo(string strIndustryID)
        {
            return indDAL.GetInfo(strIndustryID);
        }
        /// <summary>
        /// 前台读取信息
        /// </summary>
        public IndustryModel GetInfo2(string strIndustryID)
        {
            return indDAL.GetInfo2(strIndustryID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
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

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(IndustryModel indModel)
        {
            indDAL.InsertInfo(indModel);
            //增加子级数
            indDAL.AddChildNum(indModel.ParentID);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(IndustryModel indModel, string strIndustryID)
        {
            indDAL.UpdateInfo(indModel, strIndustryID);
            string key = "Cache_Industry_Model_" + strIndustryID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strIndustryID)
        {
            //取ParentID
            IndustryModel indModel = GetInfo(strIndustryID);
            if (indModel != null)
            {
                //减少子级数
                indDAL.CutChildNum(indModel.ParentID);
            }
            string key = "Cache_Industry_Model_" + strIndustryID;
            CacheHelper.RemoveCache(key);
            indDAL.DeleteInfo(strIndustryID);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strIndustryID, string strIsClose)
        {
            indDAL.UpdateCloseStatus(strIndustryID, strIsClose);
        }
        #endregion

        #region 移动信息
        /// <summary>
        /// 移动信息
        /// </summary>
        public void MoveInfo(IndustryModel indModel, string strIndustryID)
        {
            indDAL.MoveInfo(indModel, strIndustryID);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID(string strParentID)
        {
            return indDAL.GetListID(strParentID);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strParentID, string strListID, string strOldListID)
        {
            indDAL.OrderInfo(strParentID,strListID,strOldListID);
        }
        #endregion

        #region 显示下拉树形列表
        /// <summary>
        /// 显示下拉树形列表
        /// </summary>
        public void ShowSelectTree(string strParentID, DropDownList drp, string strSql)
        {
            indDAL.ShowSelectTree(strParentID, 0, drp, strSql);
        }
        #endregion

        #region 移动时更新子级数
        /// <summary>
        /// 修改时更新子级数
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

        #region 显示路径
        /// <summary>
        /// 显示路径
        /// </summary>
        public StringBuilder ShowPath(string strIndustryID)
        {
            StringBuilder tempStr = new StringBuilder("根结点");
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

        #region 取第一个类别ID
        /// <summary>
        /// 取第一个类别ID
        /// </summary>
        public string GetFirstIndustryID(string strParentID)
        {
            return indDAL.GetFirstIndustryID(strParentID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strIndustryID)
        {
            return indDAL.GetValueByField(strFieldName, strIndustryID);
        }
        #endregion

        #region Ajax取类别
        /// <summary>
        /// Ajax取类别
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
