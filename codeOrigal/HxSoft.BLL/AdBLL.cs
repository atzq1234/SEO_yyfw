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
    ///������-ҵ���߼���
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>

    public class AdBLL
    {

        private readonly AdDAL adDAL = new AdDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
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

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public AdModel GetInfo(string strAdID)
        {
            return adDAL.GetInfo(strAdID);
        }
        /// <summary>
        /// ǰ̨��ȡ��Ϣ
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

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
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
        /// ǰ̨�ӻ����ȡ��Ϣ
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

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(AdModel adModel)
        {
            adDAL.InsertInfo(adModel);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(AdModel adModel, string strAdID)
        {
            adDAL.UpdateInfo(adModel, strAdID);
            string key = "Cache_Ad_Model_" + strAdID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strAdID)
        {
            adDAL.DeleteInfo(strAdID);
            string key = "Cache_Ad_Model_" + strAdID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strAdID, string strIsClose)
        {
            adDAL.UpdateCloseStatus(strAdID, strIsClose);
            string key = "Cache_Ad_Model_" + strAdID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ��ȡ�����
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public string GetListID()
        {
            return adDAL.GetListID();
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            adDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region ���λ�еĵ�һ�����ID
        /// <summary>
        /// ���λ�еĵ�һ�����ID
        /// </summary>
        public string GetFirstID(string strAdPositionID)
        {
            return adDAL.GetFirstID(strAdPositionID);
        }
        #endregion

        #region ��������1
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public void Click(string strAdID)
        {
            adDAL.Click(strAdID);
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strAdID)
        {
            return adDAL.GetValueByField(strFieldName, strAdID);
        }
        #endregion

        #region ����XML�ļ�
        /// <summary>
        /// ����XML�ļ�
        /// </summary>
        public void CreateXML(string strAdPositionID, string strPath)
        {
            adDAL.CreateXML(strAdPositionID, strPath);
        }
        #endregion

        #region ��ͨ��ʾ���(ͼƬ��flash)
        /// <summary>
        /// ��ͨ��ʾ���(ͼƬ��flash)
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

        #region Flash�õ�Ƭ���
        /// <summary>
        /// Flash�õ�Ƭ���
        /// </summary>
        public StringBuilder ShowFlashSlide(string strAdPositionID, string strWidth, string strHeight)
        {
            return adDAL.ShowFlashSlide(strAdPositionID, strWidth, strHeight);
        }
        #endregion

        #region ��ʾ�������
        /// <summary>
        /// ��ʾ�������
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

        #region ��ʾ�������
        /// <summary>
        /// ��ʾ�������
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
