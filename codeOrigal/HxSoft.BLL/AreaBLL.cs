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
    ///地区管理-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class AreaBLL
    {

        private readonly AreaDAL areaDAL = new AreaDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
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

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public AreaModel GetInfo(string strAreaID)
        {
            return areaDAL.GetInfo(strAreaID);
        }
        /// <summary>
        /// 前台读取信息
        /// </summary>
        public AreaModel GetInfo2(string strAreaID)
        {
            return areaDAL.GetInfo2(strAreaID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
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

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(AreaModel claModel)
        {
            areaDAL.InsertInfo(claModel);
            //增加子级数
            areaDAL.AddChildNum(claModel.ParentID);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(AreaModel claModel, string strAreaID)
        {
            areaDAL.UpdateInfo(claModel, strAreaID);
            string key = "Cache_Area_Model_" + strAreaID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strAreaID)
        {
            //取ParentID
            AreaModel claModel = GetInfo(strAreaID);
            if (claModel != null)
            {
                //减少子级数
                areaDAL.CutChildNum(claModel.ParentID);
            }
            string key = "Cache_Area_Model_" + strAreaID;
            CacheHelper.RemoveCache(key);
            areaDAL.DeleteInfo(strAreaID);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strAreaID, string strIsClose)
        {
            areaDAL.UpdateCloseStatus(strAreaID, strIsClose);
        }
        #endregion

        #region 移动信息
        /// <summary>
        /// 移动信息
        /// </summary>
        public void MoveInfo(AreaModel claModel, string strAreaID)
        {
            areaDAL.MoveInfo(claModel, strAreaID);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID(string strParentID)
        {
            return areaDAL.GetListID(strParentID);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strParentID, string strListID, string strOldListID)
        {
            areaDAL.OrderInfo(strParentID, strListID, strOldListID);
        }
        #endregion

        #region 显示下拉树形列表
        /// <summary>
        /// 显示下拉树形列表
        /// </summary>
        public void ShowSelectTree(string strParentID, DropDownList drp, string strSql)
        {
            areaDAL.ShowSelectTree(strParentID, 0, drp, strSql);
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
                areaDAL.AddChildNum(strParentID);
                areaDAL.CutChildNum(strOldParentID);
            }
        }
        #endregion

        #region 显示路径
        /// <summary>
        /// 显示路径
        /// </summary>
        public StringBuilder ShowPath(string strAreaID)
        {
            StringBuilder tempStr = new StringBuilder("根结点");
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

        #region 取第一个类别ID
        /// <summary>
        /// 取第一个类别ID
        /// </summary>
        public string GetFirstAreaID(string strParentID)
        {
            return areaDAL.GetFirstAreaID(strParentID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strAreaID)
        {
            return areaDAL.GetValueByField(strFieldName, strAreaID);
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
        public StringBuilder AjaxGetAreaList(string strParentID, string strType, string strObjName)
        {
            return areaDAL.AjaxGetAreaList(strParentID, strType, strObjName);
        }
        #endregion


    }
}
