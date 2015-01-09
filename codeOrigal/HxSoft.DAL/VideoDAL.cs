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

namespace HxSoft.DAL
{
    /// <summary>
    ///视频管理-数据访问类
    /// 创建人:
    /// 日期:2012-9-19
    /// </summary>
    public class VideoDAL
    {
        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Video where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strVideoID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Video where " + strFieldName + "=@" + strFieldName + " and VideoID<>@VideoID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@VideoID",strVideoID)};
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

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strVideoID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Video where VideoID=@VideoID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@VideoID",strVideoID)};
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

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public VideoModel GetInfo(string strVideoID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Video where VideoID=@VideoID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@VideoID",strVideoID)};
            VideoModel vidModel = new VideoModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    vidModel.ClassID = dr["ClassID"].ToString();
                    vidModel.Title = dr["Title"].ToString();
                    vidModel.VideoPic = dr["VideoPic"].ToString();
                    vidModel.VideoPath = dr["VideoPath"].ToString();
                    vidModel.Description = dr["Description"].ToString();
                    vidModel.ListID = dr["ListID"].ToString();
                    vidModel.AdminID = dr["AdminID"].ToString();
                    vidModel.AddTime = dr["AddTime"].ToString();
                    vidModel.IsClose = dr["IsClose"].ToString();
                    return vidModel;
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
        public VideoModel GetInfo2(string strVideoID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Video where IsClose=0 and VideoID=@VideoID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@VideoID",strVideoID)};
            VideoModel vidModel = new VideoModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    vidModel.ClassID = dr["ClassID"].ToString();
                    vidModel.Title = dr["Title"].ToString();
                    vidModel.VideoPic = dr["VideoPic"].ToString();
                    vidModel.VideoPath = dr["VideoPath"].ToString();
                    vidModel.Description = dr["Description"].ToString();
                    vidModel.ListID = dr["ListID"].ToString();
                    vidModel.AdminID = dr["AdminID"].ToString();
                    vidModel.AddTime = dr["AddTime"].ToString();
                    vidModel.IsClose = dr["IsClose"].ToString();
                    return vidModel;
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
        public void InsertInfo(VideoModel vidModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Video(ClassID,Title,VideoPic,VideoPath,Description,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@ClassID,@Title,@VideoPic,@VideoPath,@Description,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassID",vidModel.ClassID),
Config.Conn().CreateDbParameter("@Title",vidModel.Title),
Config.Conn().CreateDbParameter("@VideoPic",vidModel.VideoPic),
Config.Conn().CreateDbParameter("@VideoPath",vidModel.VideoPath),
Config.Conn().CreateDbParameter("@Description",vidModel.Description),
Config.Conn().CreateDbParameter("@ListID",vidModel.ListID),
Config.Conn().CreateDbParameter("@AdminID",vidModel.AdminID),
Config.Conn().CreateDbParameter("@AddTime",vidModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",vidModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(VideoModel vidModel, string strVideoID)
        {
            StringBuilder sql = new StringBuilder("update t_Video set ");
            sql.Append(" ClassID=@ClassID,");
            sql.Append(" Title=@Title,");
            sql.Append(" VideoPic=@VideoPic,");
            sql.Append(" VideoPath=@VideoPath,");
            sql.Append(" Description=@Description,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where  VideoID=@VideoID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassID",vidModel.ClassID),
Config.Conn().CreateDbParameter("@Title",vidModel.Title),
Config.Conn().CreateDbParameter("@VideoPic",vidModel.VideoPic),
Config.Conn().CreateDbParameter("@VideoPath",vidModel.VideoPath),
Config.Conn().CreateDbParameter("@Description",vidModel.Description),
Config.Conn().CreateDbParameter("@ListID",vidModel.ListID),
//Config.Conn().CreateDbParameter("@AdminID",vidModel.AdminID),
//Config.Conn().CreateDbParameter("@AddTime",vidModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",vidModel.IsClose),
Config.Conn().CreateDbParameter("@VideoID",strVideoID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strVideoID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Video where VideoID=@VideoID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@VideoID",strVideoID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strVideoID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Video set IsClose=@IsClose where VideoID=@VideoID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@IsClose",strIsClose),
Config.Conn().CreateDbParameter("@VideoID",strVideoID)};
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
                sql.Append("select ListID from t_Video order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_Video order by ListID desc");
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
                    sql.Append("create table tmp as select VideoID from t_Video where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_Video set ListID=ListID-1 where VideoID in(select VideoID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Video set ListID=ListID-1 where VideoID in(select VideoID from t_Video where  ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select VideoID from t_Video where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_Video set ListID=ListID+1 where VideoID in(select VideoID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Video set ListID=ListID+1 where VideoID in(select VideoID from t_Video where  ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion

        #region 取上一篇信息ID
        /// <summary>
        /// 取上一篇信息ID
        /// </summary>
        public string GetPrevID(string strClassID, string strVideoID)
        {
            StringBuilder sql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                sql.Append("select VideoID from t_Video where IsClose=0 and ClassID=@ClassID and VideoID<@VideoID order by VideoID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 VideoID from t_Video where IsClose=0 and ClassID=@ClassID and VideoID<@VideoID order by VideoID desc");
            }
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID),
            Config.Conn().CreateDbParameter("@VideoID",strVideoID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    return dr["VideoID"].ToString();
                }
                else
                {
                    return "0";
                }
            }
        }
        #endregion

        #region 取下一篇信息ID
        /// <summary>
        /// 取下一篇信息ID
        /// </summary>
        public string GetNextID(string strClassID, string strVideoID)
        {
            StringBuilder sql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                sql.Append("select VideoID from t_Video where IsClose=0 and ClassID=@ClassID and VideoID>@VideoID order by VideoID asc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 VideoID from t_Video where IsClose=0 and ClassID=@ClassID and VideoID>@VideoID order by VideoID asc");
            }
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID),
            Config.Conn().CreateDbParameter("@VideoID",strVideoID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    return dr["VideoID"].ToString();
                }
                else
                {
                    return "0";
                }
            }
        }
        #endregion
    }
}
