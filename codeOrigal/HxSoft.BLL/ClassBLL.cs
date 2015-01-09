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
    ///栏目管理-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class ClassBLL
    {

        private readonly ClassDAL claDAL = new ClassDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue, string strConfigID)
        {
            return claDAL.CheckInfo(strFieldName, strFieldValue, strConfigID);
        }
        public bool CheckInfo(string strFieldName, string strFieldValue, string strConfigID, string strClassID)
        {
            return claDAL.CheckInfo(strFieldName, strFieldValue, strConfigID, strClassID);
        }
        public bool CheckInfo2(string strFieldName, string strFieldValue, string strParentID)
        {
            return claDAL.CheckInfo2(strFieldName, strFieldValue, strParentID);
        }
        public bool CheckInfo2(string strFieldName, string strFieldValue, string strParentID, string strClassID)
        {
            return claDAL.CheckInfo2(strFieldName, strFieldValue, strParentID, strClassID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public ClassModel GetInfo(string strClassID)
        {
            return claDAL.GetInfo(strClassID);
        }
        /// <summary>
        /// 前台读取信息
        /// </summary>
        public ClassModel GetInfo2(string strClassID)
        {
            return claDAL.GetInfo2(strClassID);
        }

        public IList<ClassModel> GetInfoListByParentID(string strParentID)
        {
            return claDAL.GetInfoListByParentID(strParentID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public ClassModel GetCacheInfo(string strClassID)
        {
            string key = "Cache_Class_Model_" + strClassID;
            if (HttpRuntime.Cache[key] != null)
                return (ClassModel)HttpRuntime.Cache[key];
            else
            {
                ClassModel claModel = claDAL.GetInfo(strClassID);
                CacheHelper.AddCache(key, claModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return claModel;
            }
        }
        /// <summary>
        /// 前台从缓存读取信息
        /// </summary>
        public ClassModel GetCacheInfo2(string strConfigID)
        {
            string key = "Cache_Class_Model_" + strConfigID;
            if (HttpRuntime.Cache[key] != null)
                return (ClassModel)HttpRuntime.Cache[key];
            else
            {
                ClassModel claModel = claDAL.GetInfo2(strConfigID);
                CacheHelper.AddCache(key, claModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return claModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(ClassModel claModel)
        {
            claDAL.InsertInfo(claModel);
            //增加子级数
            claDAL.AddChildNum(claModel.ParentID);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(ClassModel claModel, string strClassID)
        {
            claDAL.UpdateInfo(claModel, strClassID);
            string key = "Cache_Class_Model_" + strClassID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strClassID)
        {
            //取ParentID
            ClassModel claModel = GetInfo(strClassID);
            if (claModel != null)
            {
                //减少子级数
                claDAL.CutChildNum(claModel.ParentID);
            }
            string key = "Cache_Class_Model_" + strClassID;
            CacheHelper.RemoveCache(key);
            claDAL.DeleteInfo(strClassID);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strClassID, string strIsClose)
        {
            claDAL.UpdateCloseStatus(strClassID, strIsClose);
            string key = "Cache_Class_Model_" + strClassID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 移动信息
        /// <summary>
        /// 移动信息
        /// </summary>
        public void MoveInfo(ClassModel claModel, string strClassID)
        {
            claDAL.MoveInfo(claModel, strClassID);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID(string strParentID)
        {
            return claDAL.GetListID(strParentID);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strParentID, string strListID, string strOldListID)
        {
            claDAL.OrderInfo(strParentID,strListID,strOldListID);
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
                claDAL.AddChildNum(strParentID);
                claDAL.CutChildNum(strOldParentID);
            }
        }
        #endregion

        #region 显示下拉树形列表
        /// <summary>
        /// 显示下拉树形列表
        /// </summary>
        public void ShowSelectTree(string strParentID, DropDownList drp, string strSql, string strClassPropertyID)
        {
            claDAL.ShowSelectTree(strParentID, 0, drp, strSql,strClassPropertyID);
        }
        #endregion

        #region 取第一个类别ID
        /// <summary>
        /// 取第一个类别ID
        /// </summary>
        public string GetFirstClassID(string strParentID)
        {
            return claDAL.GetFirstClassID(strParentID);
        }
        #endregion

        #region 取子栏目ID
        /// <summary>
        /// 取子栏目ID
        /// </summary>
        public StringBuilder GetSubClassID(string strParentID)
        {
            StringBuilder strTemp = new StringBuilder();
            strTemp.Append("-1,");
            strTemp.Append(claDAL.GetSubClassID(strParentID));
            strTemp.Append("-1");
            return strTemp;
        }
        #endregion

        #region 取路径
        /// <summary>
        /// 取路径
        /// </summary>
        public StringBuilder GetPath(string strClassID)
        {
            return claDAL.GetPath(strClassID);
        }
        #endregion

        #region 取当前栏目
        /// <summary>
        /// 取当前栏目
        /// </summary>
        public StringBuilder GetClassBlock(string strClassPath, int intDepth)
        {
            return claDAL.GetClassBlock(strClassPath, intDepth);
        }
        #endregion

        #region 取栏目列表
        /// <summary>
        /// 取栏目列表(a)
        /// </summary>
        public StringBuilder GetClassList(string ParentID, string strSeparator, int intShowLen, string strLinkKey)
        {
            return claDAL.GetClassList(ParentID, strSeparator, intShowLen, strLinkKey);
        }
        /// <summary>
        /// 取栏目列表(li)
        /// </summary>
        public StringBuilder GetClassList(string ParentID, string strStyleClass, string ClassID, int intShowLen, string strLinkKey)
        {
            return claDAL.GetClassList(ParentID, strStyleClass, ClassID, intShowLen, strLinkKey);
        }
        /// <summary>
        /// 取栏目列表(递归)
        /// </summary>
        public StringBuilder GetClassList(string ParentID, string strStyleClass, string strClassPath, int i, int intShowLen, string strLinkKey)
        {
            return claDAL.GetClassList(ParentID, strStyleClass, strClassPath, i, intShowLen, strLinkKey);
        }
        #endregion

        #region 显示路径
        /// <summary>
        /// 显示路径
        /// </summary>
        public StringBuilder ShowPath(string strClassID)
        {
            StringBuilder tempStr = new StringBuilder("根结点");
            ClassModel claModel = new ClassModel();
            claModel = claDAL.GetInfo(strClassID);
            if (claModel == null)
            {
                return tempStr;
            }
            else
            {
                string strPath = claDAL.GetPath(strClassID).ToString();
                string[] arrPath = strPath.Split(new char[] { ',' });
                for (int i = 0; i < arrPath.Length; i++)
                {
                    ClassModel claModel_2 = new ClassModel();
                    claModel_2 = claDAL.GetInfo(arrPath[i]);
                    if (claModel_2 != null)
                    {
                        tempStr.Append(" > " + claModel_2.ClassName);
                    }
                }
                return tempStr;
            }
        }
        #endregion

        #region 取位置导航
        /// <summary>
        /// 取位置导航
        /// </summary>
        public StringBuilder GetClassNav(string strClassPath, int intDepth,  string strNavStr)
        {
            StringBuilder strClassNav = new StringBuilder();
            string[] ArrClassPath = strClassPath.Split(new char[] { ',' });
            for (int i = intDepth; i < ArrClassPath.Length; i++)
            {
                ClassModel claModel = new ClassModel();
                claModel = claDAL.GetInfo(ArrClassPath[i]);
                if (claModel!=null)
                {
                    string strTempLinkUrl,strTempTarget;
                    if (claModel.LinkUrl != string.Empty)
                    {
                        strTempLinkUrl = claModel.LinkUrl;
                        strTempTarget = "target=\"" + claModel.Target + "\"";
                    }
                    else
                    {
                        strTempLinkUrl = claModel.ClassEnName + Config.FileExt;
                        strTempTarget = "";
                    }
                    strClassNav.Append("<a href=\"" + strTempLinkUrl + "\" " + strTempTarget + ">" + claModel.ClassName + "</a>");
                    if (i != ArrClassPath.Length - 1) strClassNav.Append(strNavStr);
                }
            }
            return strClassNav;
        }
        #endregion

        #region 根据栏目英文名取栏目ID
        /// <summary>
        /// 根据栏目英文名取栏目ID
        /// </summary>
        public string GetClassIDByClassEnName(string strConfigID, string strClassEnName)
        {
            return claDAL.GetClassIDByClassEnName(strConfigID,strClassEnName);
        }
        #endregion

        #region 根据栏目ID取栏目英文名
        /// <summary>
        /// 根据栏目ID取栏目英文名
        /// </summary>
        public string GetClassEnNameByClassID(string strClassID)
        {
            return claDAL.GetClassEnNameByClassID(strClassID);
        }
        #endregion

        #region 取顶级类别ID
        /// <summary>
        /// 取顶级类别ID
        /// </summary>
        public string GetTopClassID(string strClassID)
        {
            return claDAL.GetTopClassID(strClassID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strClassID)
        {
            return claDAL.GetValueByField(strFieldName, strClassID);
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
        public StringBuilder AjaxGetClassList(string strParentID, string strType, string strObjName)
        {
            return claDAL.AjaxGetClassList(strParentID, strType, strObjName);
        }
        #endregion

        #region 返回子栏目ID的SQL
        /// <summary>
        /// 返回子栏目ID的SQL
        /// </summary>
        public string GetSubClassSql(string strClassID)
        {
            return claDAL.GetSubClassSql(strClassID);
        }
        #endregion


        #region 同步更新子栏目参数
        /// <summary>
        /// 同步更新子栏目参数
        /// </summary>
        public void UpdateSubClassConfig(string strClassID, string strClassConfig, string strClassPropertyID)
        {
            claDAL.UpdateSubClassConfig(strClassID, strClassConfig, strClassPropertyID);
        }
        #endregion


        #region 根据栏目路径取ClassID
        /// <summary>
        /// 根据栏目路径取ClassID
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string GetClassIDByPath(string strClassPath, int index, string strClassID)
        {
            return claDAL.GetClassIDByPath(strClassPath, index, strClassID);
        }
        #endregion

    }
}
