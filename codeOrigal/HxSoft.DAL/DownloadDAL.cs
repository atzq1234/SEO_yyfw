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
    ///下载管理-数据访问类
    /// 创建人:杨小明
    /// 日期:2011-2-24
    /// </summary>
    public class DownloadDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Download where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strDownloadID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Download where " + strFieldName + "=@" + strFieldName + " and DownloadID<>@DownloadID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@DownloadID",strDownloadID)};
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
        public DownloadModel GetInfo(string strDownloadID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Download where DownloadID=@DownloadID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@DownloadID",strDownloadID)};
            DownloadModel dowModel = new DownloadModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    dowModel.ClassID = dr["ClassID"].ToString();
                    dowModel.DownName = dr["DownName"].ToString();
                    dowModel.DownPic = dr["DownPic"].ToString();
                    dowModel.DownUrl = dr["DownUrl"].ToString();
                    dowModel.DownSize = dr["DownSize"].ToString();
                    dowModel.Tags = dr["Tags"].ToString();
                    dowModel.Keywords = dr["Keywords"].ToString();
                    dowModel.Description = dr["Description"].ToString();
                    dowModel.Details = dr["Details"].ToString();
                    dowModel.ClickNum = dr["ClickNum"].ToString();
                    dowModel.ListID = dr["ListID"].ToString();
                    dowModel.IsRecommend = dr["IsRecommend"].ToString();
                    dowModel.AdminID = dr["AdminID"].ToString();
                    dowModel.AddTime = dr["AddTime"].ToString();
                    dowModel.IsClose = dr["IsClose"].ToString();
                    return dowModel;
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
        public DownloadModel GetInfo2(string strDownloadID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Download where IsClose=0 and DownloadID=@DownloadID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@DownloadID",strDownloadID)};
            DownloadModel dowModel = new DownloadModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    dowModel.ClassID = dr["ClassID"].ToString();
                    dowModel.DownName = dr["DownName"].ToString();
                    dowModel.DownPic = dr["DownPic"].ToString();
                    dowModel.DownUrl = dr["DownUrl"].ToString();
                    dowModel.DownSize = dr["DownSize"].ToString();
                    dowModel.Tags = dr["Tags"].ToString();
                    dowModel.Keywords = dr["Keywords"].ToString();
                    dowModel.Description = dr["Description"].ToString();
                    dowModel.Details = dr["Details"].ToString();
                    dowModel.ClickNum = dr["ClickNum"].ToString();
                    dowModel.ListID = dr["ListID"].ToString();
                    dowModel.IsRecommend = dr["IsRecommend"].ToString();
                    dowModel.AdminID = dr["AdminID"].ToString();
                    dowModel.AddTime = dr["AddTime"].ToString();
                    dowModel.IsClose = dr["IsClose"].ToString();
                    return dowModel;
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
        public void InsertInfo(DownloadModel dowModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Download(ClassID,DownName,DownPic,DownUrl,DownSize,Tags,Keywords,Description,Details,ClickNum,ListID,IsRecommend,AdminID,AddTime,IsClose)");
            sql.Append(" values(@ClassID,@DownName,@DownPic,@DownUrl,@DownSize,@Tags,@Keywords,@Description,@Details,@ClickNum,@ListID,@IsRecommend,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassID",dowModel.ClassID),
Config.Conn().CreateDbParameter("@DownName",dowModel.DownName),
Config.Conn().CreateDbParameter("@DownPic",dowModel.DownPic),
Config.Conn().CreateDbParameter("@DownUrl",dowModel.DownUrl),
Config.Conn().CreateDbParameter("@DownSize",dowModel.DownSize),
Config.Conn().CreateDbParameter("@Tags",dowModel.Tags),
Config.Conn().CreateDbParameter("@Keywords",dowModel.Keywords),
Config.Conn().CreateDbParameter("@Description",dowModel.Description),
Config.Conn().CreateDbParameter("@Details",dowModel.Details),
Config.Conn().CreateDbParameter("@ClickNum",dowModel.ClickNum),
Config.Conn().CreateDbParameter("@ListID",dowModel.ListID),
Config.Conn().CreateDbParameter("@IsRecommend",dowModel.IsRecommend),
Config.Conn().CreateDbParameter("@AdminID",dowModel.AdminID),
Config.Conn().CreateDbParameter("@AddTime",dowModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",dowModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(DownloadModel dowModel, string strDownloadID)
        {
            StringBuilder sql = new StringBuilder("update t_Download set ");
            sql.Append(" ClassID=@ClassID,");
            sql.Append(" DownName=@DownName,");
            sql.Append(" DownPic=@DownPic,");
            sql.Append(" DownUrl=@DownUrl,");
            sql.Append(" DownSize=@DownSize,");
            sql.Append(" Tags=@Tags,");
            sql.Append(" Keywords=@Keywords,");
            sql.Append(" Description=@Description,");
            sql.Append(" Details=@Details,");
            sql.Append(" ClickNum=@ClickNum,");
            sql.Append(" ListID=@ListID,");
            sql.Append(" IsRecommend=@IsRecommend,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where  DownloadID=@DownloadID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassID",dowModel.ClassID),
Config.Conn().CreateDbParameter("@DownName",dowModel.DownName),
Config.Conn().CreateDbParameter("@DownPic",dowModel.DownPic),
Config.Conn().CreateDbParameter("@DownUrl",dowModel.DownUrl),
Config.Conn().CreateDbParameter("@DownSize",dowModel.DownSize),
Config.Conn().CreateDbParameter("@Tags",dowModel.Tags),
Config.Conn().CreateDbParameter("@Keywords",dowModel.Keywords),
Config.Conn().CreateDbParameter("@Description",dowModel.Description),
Config.Conn().CreateDbParameter("@Details",dowModel.Details),
Config.Conn().CreateDbParameter("@ClickNum",dowModel.ClickNum),
Config.Conn().CreateDbParameter("@ListID",dowModel.ListID),
Config.Conn().CreateDbParameter("@IsRecommend",dowModel.IsRecommend),
//Config.Conn().CreateDbParameter("@AdminID",dowModel.AdminID),
//Config.Conn().CreateDbParameter("@AddTime",dowModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",dowModel.IsClose),
Config.Conn().CreateDbParameter("@DownloadID",strDownloadID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strDownloadID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Download where DownloadID=@DownloadID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@DownloadID",strDownloadID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strDownloadID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Download set IsClose=@IsClose where DownloadID=@DownloadID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@DownloadID",strDownloadID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 转移信息
        /// <summary>
        /// 转移信息
        /// </summary>
        public void TransferInfo(string strDownloadID, string strClassID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Download set ClassID=@ClassID where DownloadID=@DownloadID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID),
            Config.Conn().CreateDbParameter("@DownloadID",strDownloadID)};
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
                sql.Append("select ListID from t_Download order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_Download order by ListID desc");
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
                    sql.Append("create table tmp as select DownloadID from t_Download where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_Download set ListID=ListID-1 where DownloadID in(select DownloadID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Download set ListID=ListID-1 where DownloadID in(select DownloadID from t_Download where  ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select DownloadID from t_Download where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_Download set ListID=ListID+1 where DownloadID in(select DownloadID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Download set ListID=ListID+1 where DownloadID in(select DownloadID from t_Download where  ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion

        #region 取第一个信息ID
        /// <summary>
        /// 取第一个信息ID
        /// </summary>
        public string GetFirstID(string strClassID)
        {
            StringBuilder sql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                sql.Append("select DownloadID from t_Download where IsClose=0 and ClassID=@ClassID order by DownloadID asc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 DownloadID from t_Download where IsClose=0 and ClassID=@ClassID order by DownloadID asc");
            }
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    return dr["DownloadID"].ToString();
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
        /// 访问数加1
        /// </summary>
        public void Click(string strDownloadID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Download set ClickNum=ClickNum+1 where DownloadID=@DownloadID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@DownloadID",strDownloadID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strDownloadID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Download where DownloadID=@DownloadID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@DownloadID",strDownloadID)};
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


    }
}
