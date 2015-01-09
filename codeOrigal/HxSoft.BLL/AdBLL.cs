using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
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
    ///广告管理-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class AdBLL
    {

        private readonly AdDAL adDAL = new AdDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return adDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strAdID)
        {
            return adDAL.CheckInfo(strFieldName, strFieldValue, strAdID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public AdModel GetInfo(string strAdID)
        {
            return adDAL.GetInfo(strAdID);
        }
        /// <summary>
        /// 前台读取信息
        /// </summary>
        public AdModel GetInfo2(string strAdID)
        {
            return adDAL.GetInfo2(strAdID);
        }

        public IList<AdModel> GetInfoList(string strAdPositionID)
        {
            return adDAL.GetInfoList(strAdPositionID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public AdModel GetCacheInfo(string strAdID)
        {
            string key = "Cache_Ad_Model_" + strAdID;
            if (HttpRuntime.Cache[key] != null)
                return (AdModel)HttpRuntime.Cache[key];
            else
            {
                AdModel adModel = adDAL.GetInfo(strAdID);
                CacheHelper.AddCache(key, adModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return adModel;
            }
        }
        /// <summary>
        /// 前台从缓存读取信息
        /// </summary>
        public AdModel GetCacheInfo2(string strAdID)
        {
            string key = "Cache_Ad_Model_" + strAdID;
            if (HttpRuntime.Cache[key] != null)
                return (AdModel)HttpRuntime.Cache[key];
            else
            {
                AdModel adModel = adDAL.GetInfo2(strAdID);
                CacheHelper.AddCache(key, adModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return adModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(AdModel adModel)
        {
            adDAL.InsertInfo(adModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(AdModel adModel, string strAdID)
        {
            adDAL.UpdateInfo(adModel, strAdID);
            string key = "Cache_Ad_Model_" + strAdID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strAdID)
        {
            adDAL.DeleteInfo(strAdID);
            string key = "Cache_Ad_Model_" + strAdID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strAdID, string strIsClose)
        {
            adDAL.UpdateCloseStatus(strAdID, strIsClose);
            string key = "Cache_Ad_Model_" + strAdID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return adDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            adDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region 广告位中的第一条广告ID
        /// <summary>
        /// 广告位中的第一条广告ID
        /// </summary>
        public string GetFirstID(string strAdPositionID)
        {
            return adDAL.GetFirstID(strAdPositionID);
        }
        #endregion

        #region 访问数加1
        /// <summary>
        /// 读取信息
        /// </summary>
        public void Click(string strAdID)
        {
            adDAL.Click(strAdID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strAdID)
        {
            return adDAL.GetValueByField(strFieldName, strAdID);
        }
        #endregion

        #region 生成XML文件
        /// <summary>
        /// 生成XML文件
        /// </summary>
        public void CreateXML(string strAdPositionID, string strPath)
        {
            adDAL.CreateXML(strAdPositionID, strPath);
        }
        #endregion

        #region 普通显示广告(图片或flash)
        /// <summary>
        /// 普通显示广告(图片或flash)
        /// </summary>
        public StringBuilder ShowPicOrFlash(string strAdPositionID, string strWidth, string strHeight)
        {
            StringBuilder strScript = new StringBuilder();
            string strAdID = adDAL.GetFirstID(strAdPositionID);
            AdModel adModel = new AdModel();
            adModel = GetCacheInfo2(strAdID);
            if (adModel != null)
            {
                if (adModel.AdPath != string.Empty)
                {
                    string FileExt = adModel.AdPath.Substring((adModel.AdPath.Length - 3), 3).ToLower();
                    if (FileExt == "swf")
                    {
                        strScript.Append("document.write('<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\" width=\"" + strWidth + "\" height=\"" + strHeight + "\" >');\n");
                        strScript.Append("document.write('<param name=\"movie\" value=\"" + adModel.AdPath + "\">');\n");
                        strScript.Append("document.write('<param name=\"quality\" value=\"high\">');\n");
                        strScript.Append("document.write('<param name=\"wmode\" value=\"transparent\">');\n");
                        strScript.Append("document.write('<embed src=\"" + adModel.AdPath + "\" wmode=\"transparent\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"" + strWidth + "\" height=\"" + strHeight + "\"></embed>');\n");
                        strScript.Append("document.write('</object>');\n");
                    }
                    else
                    {
                        if (adModel.AdLink == "#" || adModel.AdLink == string.Empty)
                        {
                            strScript.Append("document.write('<img src=\"" + adModel.AdPath + "\" width=\"" + strWidth + "\" height=\"" + strHeight + "\" alt=\"" + adModel.AdName + "\" border=\"0\"/>');\n");
                        }
                        else
                        {
                            strScript.Append("document.write('<a href=\"/AdClick.ashx?AdID=" + strAdID + "\" target=\"_blank\"><img src=\"" + adModel.AdPath + "\" width=\"" + strWidth + "\" height=\"" + strHeight + "\" alt=\"" + adModel.AdName + "\" border=\"0\"/></a>');\n");
                        }
                    }
                }
            }
            return strScript;
        }
        #endregion

        #region Flash幻灯片广告
        /// <summary>
        /// Flash幻灯片广告
        /// </summary>
        public StringBuilder ShowFlashSlide(string strAdPositionID, string strWidth, string strHeight)
        {
            return adDAL.ShowFlashSlide(strAdPositionID, strWidth, strHeight);
        }
        #endregion

        #region 显示浮动广告
        /// <summary>
        /// 显示浮动广告
        /// </summary>
        public StringBuilder ShowFloat(string strAdPositionID, string strWidth, string strHeight)
        {
            StringBuilder strScript = new StringBuilder();
            strScript.Append("document.write('<div id=\"position_" + strAdPositionID + "\" style=\"width:" + strWidth + "px;height:" + strHeight + "px;position: absolute;\">');\n");
            strScript.Append(ShowPicOrFlash(strAdPositionID, strWidth, strHeight));
            strScript.Append("document.write('</div>');\n");
            strScript.Append("floatAd(\"position_" + strAdPositionID + "\");\n");
            return strScript;
        }
        #endregion

        #region 显示对联广告
        /// <summary>
        /// 显示对联广告
        /// </summary>
        public StringBuilder ShowDistich(string strAdPositionID, string strWidth, string strHeight)
        {
            string strTemp = ShowPicOrFlash(strAdPositionID,strWidth, strHeight).ToString();
            StringBuilder strScript = new StringBuilder();
            strScript.Append("document.write('<div id=\"position_" + strAdPositionID + "_1\" style=\"width:" + strWidth + "px;height:" + strHeight + "px;position: absolute;top:40px;left:5px\">');\n");
            strScript.Append(strTemp);
            strScript.Append("document.write('</div>');\n");
            strScript.Append("document.write('<div id=\"position_" + strAdPositionID + "_2\" style=\"width:" + strWidth + "px;height:" + strHeight + "px;position: absolute;top:40px;right:5px\">');\n");
            strScript.Append(strTemp);
            strScript.Append("document.write('</div>');\n");
            strScript.Append("initEcAd(\"position_" + strAdPositionID + "_1\",\"position_" + strAdPositionID + "_2\");\n");
            return strScript;
        }
        #endregion

    }
}
