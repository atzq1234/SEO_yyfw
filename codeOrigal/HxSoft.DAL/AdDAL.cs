using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HxSoft.Model;
using HxSoft.Common;
using System.IO;

namespace HxSoft.DAL
{
    /// <summary>
    ///广告管理-数据访问类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    public class AdDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Ad where " + strFieldName + "=@" + strFieldName + "");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strAdID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Ad where " + strFieldName + "=@" + strFieldName + " and AdID<>@AdID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@AdID",strAdID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public AdModel GetInfo(string strAdID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Ad where AdID=@AdID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdID",strAdID)};
            AdModel adModel = new AdModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    adModel.AdName = dr["AdName"].ToString();
                    adModel.AdIntro = dr["AdIntro"].ToString();
                    adModel.AdPositionID = dr["AdPositionID"].ToString();
                    adModel.AdSmallPic = dr["AdSmallPic"].ToString();
                    adModel.AdPath = dr["AdPath"].ToString();
                    adModel.AdLink = dr["AdLink"].ToString();
                    adModel.ClickNum = dr["ClickNum"].ToString();
                    adModel.ListID = dr["ListID"].ToString();
                    adModel.AdminID = dr["AdminID"].ToString();
                    adModel.AddTime = dr["AddTime"].ToString();
                    adModel.IsClose = dr["IsClose"].ToString();
                    return adModel;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 前台读取信息
        /// </summary>
        public AdModel GetInfo2(string strAdID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Ad where IsClose=0 and AdID=@AdID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdID",strAdID)};
            AdModel adModel = new AdModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    adModel.AdName = dr["AdName"].ToString();
                    adModel.AdIntro = dr["AdIntro"].ToString();
                    adModel.AdPositionID = dr["AdPositionID"].ToString();
                    adModel.AdSmallPic = dr["AdSmallPic"].ToString();
                    adModel.AdPath = dr["AdPath"].ToString();
                    adModel.AdLink = dr["AdLink"].ToString();
                    adModel.ClickNum = dr["ClickNum"].ToString();
                    adModel.ListID = dr["ListID"].ToString();
                    adModel.AdminID = dr["AdminID"].ToString();
                    adModel.AddTime = dr["AddTime"].ToString();
                    adModel.IsClose = dr["IsClose"].ToString();
                    return adModel;
                }
                else
                {
                    return null;
                }
            }
        }

        public IList<AdModel> GetInfoList(string strAdPositionID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Ad where IsClose=0 and AdPositionID=@AdPositionID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdPositionID",strAdPositionID)};
            IList<AdModel> adList = new List<AdModel>();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                while (dr.Read())
                {
                    AdModel adModel = new AdModel();
                    adModel.AdName = dr["AdName"].ToString();
                    adModel.AdIntro = dr["AdIntro"].ToString();
                    adModel.AdPositionID = dr["AdPositionID"].ToString();
                    adModel.AdSmallPic = dr["AdSmallPic"].ToString();
                    adModel.AdPath = dr["AdPath"].ToString();
                    adModel.AdLink = dr["AdLink"].ToString();
                    adModel.ClickNum = dr["ClickNum"].ToString();
                    adModel.ListID = dr["ListID"].ToString();
                    adModel.AdminID = dr["AdminID"].ToString();
                    adModel.AddTime = dr["AddTime"].ToString();
                    adModel.IsClose = dr["IsClose"].ToString();
                    adList.Add(adModel);
                }
                if (adList.Count > 0)
                {
                    return adList;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(AdModel adModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Ad(AdName,AdIntro,AdPositionID,AdSmallPic,AdPath,AdLink,ClickNum,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@AdName,@AdIntro,@AdPositionID,@AdSmallPic,@AdPath,@AdLink,@ClickNum,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdName",adModel.AdName),
            Config.Conn().CreateDbParameter("@AdIntro",adModel.AdIntro),
            Config.Conn().CreateDbParameter("@AdPositionID",adModel.AdPositionID),
            Config.Conn().CreateDbParameter("@AdSmallPic",adModel.AdSmallPic),
            Config.Conn().CreateDbParameter("@AdPath",adModel.AdPath),
            Config.Conn().CreateDbParameter("@AdLink",adModel.AdLink),
            Config.Conn().CreateDbParameter("@ClickNum",adModel.ClickNum),
            Config.Conn().CreateDbParameter("@ListID",adModel.ListID),
            Config.Conn().CreateDbParameter("@AdminID",adModel.AdminID),
            Config.Conn().CreateDbParameter("@AddTime",adModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",adModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(AdModel adModel, string strAdID)
        {
            StringBuilder sql = new StringBuilder("update t_Ad set ");
            sql.Append(" AdName=@AdName,");
            sql.Append(" AdIntro=@AdIntro,");
            sql.Append(" AdPositionID=@AdPositionID,");
            sql.Append(" AdSmallPic=@AdSmallPic,");
            sql.Append(" AdPath=@AdPath,");
            sql.Append(" AdLink=@AdLink,");
            sql.Append(" ClickNum=@ClickNum,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where AdID=@AdID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdName",adModel.AdName),
            Config.Conn().CreateDbParameter("@AdIntro",adModel.AdIntro),
            Config.Conn().CreateDbParameter("@AdPositionID",adModel.AdPositionID),
            Config.Conn().CreateDbParameter("@AdSmallPic",adModel.AdSmallPic),
            Config.Conn().CreateDbParameter("@AdPath",adModel.AdPath),
            Config.Conn().CreateDbParameter("@AdLink",adModel.AdLink),
            Config.Conn().CreateDbParameter("@ClickNum",adModel.ClickNum),
            Config.Conn().CreateDbParameter("@ListID",adModel.ListID),
            //Config.Conn().CreateDbParameter("@AdminID",adModel.AdminID),
            //Config.Conn().CreateDbParameter("@AddTime",adModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",adModel.IsClose),
            Config.Conn().CreateDbParameter("@AdID",strAdID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strAdID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Ad where AdID=@AdID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdID",strAdID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strAdID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Ad set IsClose=@IsClose where AdID=@AdID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@AdID",strAdID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            StringBuilder sql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                sql.Append("select ListID from t_Ad order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_Ad order by ListID desc");
            }
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                if (dr.Read())
                {
                    string strListID = Convert.ToString((int)dr["ListID"] + 1);
                    return strListID;
                }
                else
                {
                    return "1";
                }
            }
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            if (Convert.ToInt32(strListID) > Convert.ToInt32(strOldListID))
            {
                StringBuilder sql = new StringBuilder();
                if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
                {
                    sql.Append("create table tmp as select AdID from t_Ad where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_Ad set ListID=ListID-1 where AdID in(select AdID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Ad set ListID=ListID-1 where AdID in(select AdID from t_Ad where  ListID<=@ListID and ListID>@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
            else if (Convert.ToInt32(strListID) < Convert.ToInt32(strOldListID))
            {
                StringBuilder sql = new StringBuilder();
                if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
                {
                    sql.Append("create table tmp as select AdID from t_Ad where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_Ad set ListID=ListID+1 where AdID in(select AdID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Ad set ListID=ListID+1 where AdID in(select AdID from t_Ad where  ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion

        #region 广告位中的第一条广告ID
        /// <summary>
        /// 广告位中的第一条广告ID
        /// </summary>
        public string GetFirstID(string strAdPositionID)
        {
            StringBuilder sql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                sql.Append("select AdID from t_Ad where IsClose=0 and AdPositionID=@AdPositionID order by ListID asc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 AdID from t_Ad where IsClose=0 and AdPositionID=@AdPositionID order by ListID asc");
            }
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdPositionID",strAdPositionID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    return dr["AdID"].ToString();
                }
                else
                {
                    return "0";
                }
            }
        }
        #endregion

        #region 访问数加1
        /// <summary>
        /// 读取信息
        /// </summary>
        public void Click(string strAdID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Ad set ClickNum=ClickNum+1 where AdID=@AdID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdID",strAdID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strAdID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Ad where AdID=@AdID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@AdID",strAdID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    return dr[strFieldName].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        #endregion

        #region 生成XML文件
        /// <summary>
        /// 生成XML文件
        /// </summary>
        public void CreateXML(string strAdPositionID, string strPath)
        {
            StringBuilder strXml = new StringBuilder();
            strXml.Append("<?xml version=\"1.0\" encoding=\"gb2312\"?>\n");
            strXml.Append("<imgList>\n");
            strXml.Append("<pic>\n");
            string sql = "select * from t_Ad where IsClose=0 and AdPositionID=" + strAdPositionID + " order by ListID desc";
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                while (dr.Read())
                {
                    strXml.Append("<list path=\"" + dr["AdPath"].ToString() + "\" smallpath=\"" + dr["AdSmallPic"].ToString() + "\" smallinfo=\"" + dr["AdName"].ToString() + "\">" + dr["AdLink"].ToString() + "</list>\n");
                }
            }
            strXml.Append("</pic>\n");
            strXml.Append("<rollTime fade_in=\"10\">3</rollTime>\n");
            strXml.Append("<text font=\"宋体\" size=\"14\" bold=\"true\" color=\"0xfdedede\"></text>\n");
            strXml.Append("</imgList>\n");
            // using (StreamWriter sw = File.CreateText(strPath))
            using (StreamWriter sw = new StreamWriter(strPath, false, Encoding.GetEncoding("gb2312")))
            {
                sw.WriteLine(strXml.ToString());
            }
        }
        #endregion

        #region Flash幻灯片广告
        /// <summary>
        /// Flash幻灯片广告
        /// </summary>
        public StringBuilder ShowFlashSlide(string strAdPositionID,string strWidth, string strHeight)
        {
            StringBuilder strScript = new StringBuilder();
            strScript.Append("var imgUrl_" + strAdPositionID + " = new Array();\n");
            strScript.Append("var imgtext_" + strAdPositionID + " = new Array();\n");
            strScript.Append("var imgLink_" + strAdPositionID + " = new Array();\n");
            string sql = "select top 5 * from t_Ad where IsClose=0 and AdPositionID=" + strAdPositionID + " order by ListID asc";
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                sql = "select * from t_Ad where IsClose=0 and AdPositionID=" + strAdPositionID + " order by ListID asc limit 0,5";
            }
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql, null))
            {
                int i = 0;
                while (dr.Read())
                {
                    strScript.Append("imgUrl_" + strAdPositionID + "[" + i + "]='" + dr["AdPath"].ToString() + "';\n");
                    strScript.Append("imgLink_" + strAdPositionID + "[" + i + "]='/AdClick.ashx?AdID=" + dr["AdID"].ToString() + "';\n");
                    strScript.Append("imgtext_" + strAdPositionID + "[" + i + "]='';\n");
                    i++;
                }
            }
            strScript.Append("var pics_" + strAdPositionID + "=imgUrl_" + strAdPositionID + "[0];\n");
            strScript.Append("var links_" + strAdPositionID + "=imgLink_" + strAdPositionID + "[0];\n");
            strScript.Append("var texts_" + strAdPositionID + "=imgtext_" + strAdPositionID + "[0];\n");
            strScript.Append("for(var i=1;i<imgUrl_" + strAdPositionID + ".length;i++){\n");
            strScript.Append("pics_" + strAdPositionID + "+='|'+imgUrl_" + strAdPositionID + "[i];\n");
            strScript.Append("links_" + strAdPositionID + "+='|'+imgLink_" + strAdPositionID + "[i];\n");
            strScript.Append("texts_" + strAdPositionID + "+='|'+imgtext_" + strAdPositionID + "[i];\n");
            strScript.Append("}\n");

            strScript.Append("//大小控制\n");
            strScript.Append("var focus_width_" + strAdPositionID + "=" + strWidth + ";//宽度控制\n");
            strScript.Append("var focus_height_" + strAdPositionID + "=" + strHeight + ";//高度控制\n");
            strScript.Append("var text_height_" + strAdPositionID + "=0; //下方标题高度控制\n");
            strScript.Append("var swf_height_" + strAdPositionID + " = focus_height_" + strAdPositionID + "+text_height_" + strAdPositionID + ";\n");

            strScript.Append("document.write('<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\" width=\"'+ focus_width_" + strAdPositionID + " +'\" height=\"'+ swf_height_" + strAdPositionID + " +'\">');\n");
            strScript.Append("document.write('<param name=\"allowScriptAccess\" value=\"sameDomain\" />');\n");
            strScript.Append("document.write('<param name=\"movie\" value=\"/App_Themes/Flash/flashplay.swf\" />');\n");
            strScript.Append("document.write('<param name=\"quality\" value=\"high\" />');\n");
            strScript.Append("document.write('<param name=\"bgcolor\" value=\"#fff\">');\n");
            strScript.Append("document.write('<param name=\"menu\" value=\"false\">');\n");
            strScript.Append("document.write('<param name=wmode value=\"opaque\">');\n");
            strScript.Append("document.write('<param name=\"FlashVars\" value=\"pics='+pics_" + strAdPositionID + "+'&links='+links_" + strAdPositionID + "+'&texts='+texts_" + strAdPositionID + "+'&borderwidth='+focus_width_" + strAdPositionID + "+'&borderheight='+focus_height_" + strAdPositionID + "+'&textheight='+text_height_" + strAdPositionID + "+'\">');\n");
            strScript.Append("document.write('<embed src=\"/App_Themes/Flash/flashplay.swf\" wmode=\"opaque\" FlashVars=\"pics='+pics_" + strAdPositionID + "+'&links='+links_" + strAdPositionID + "+'&texts='+texts_" + strAdPositionID + "+'&borderwidth='+focus_width_" + strAdPositionID + "+'&borderheight='+focus_height_" + strAdPositionID + "+'&textheight='+text_height_" + strAdPositionID + "+'\" menu=\"false\" bgcolor=\"#fff\" quality=\"high\" width=\"'+ focus_width_" + strAdPositionID + " +'\" height=\"'+ focus_height_" + strAdPositionID + " +'\" allowScriptAccess=\"sameDomain\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" />');\n");
            strScript.Append("document.write('</object>');\n");
            return strScript;
        }
        #endregion
    }
}
