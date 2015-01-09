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
    ///会员级别-数据访问类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    public class UserRankDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_UserRank where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strUserRankID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_UserRank where " + strFieldName + "=@" + strFieldName + " and UserRankID<>@UserRankID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@UserRankID",strUserRankID)};
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
        public UserRankModel GetInfo(string strUserRankID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_UserRank where UserRankID=@UserRankID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@UserRankID",strUserRankID)};
            UserRankModel userRankModel = new UserRankModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    userRankModel.UserRankName = dr["UserRankName"].ToString();
                    userRankModel.LimitValues = dr["LimitValues"].ToString();
                    userRankModel.ListID = dr["ListID"].ToString();
                    userRankModel.AdminID = dr["AdminID"].ToString();
                    userRankModel.AddTime = dr["AddTime"].ToString();
                    userRankModel.IsClose = dr["IsClose"].ToString();
                    return userRankModel;
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
        public void InsertInfo(UserRankModel userRankModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_UserRank(UserRankName,LimitValues,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@UserRankName,@LimitValues,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@UserRankName",userRankModel.UserRankName),
            Config.Conn().CreateDbParameter("@LimitValues",userRankModel.LimitValues),
            Config.Conn().CreateDbParameter("@ListID",userRankModel.ListID),
            Config.Conn().CreateDbParameter("@AdminID",userRankModel.AdminID),
            Config.Conn().CreateDbParameter("@AddTime",userRankModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",userRankModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(UserRankModel userRankModel, string strUserRankID)
        {
            StringBuilder sql = new StringBuilder("update t_UserRank set ");
            sql.Append(" UserRankName=@UserRankName,");
            //sql.Append(" LimitValues=@LimitValues,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where UserRankID=@UserRankID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@UserRankName",userRankModel.UserRankName),
            //Config.Conn().CreateDbParameter("@LimitValues",userRankModel.LimitValues),
            Config.Conn().CreateDbParameter("@ListID",userRankModel.ListID),
            //Config.Conn().CreateDbParameter("@AdminID",userRankModel.AdminID),
            //Config.Conn().CreateDbParameter("@AddTime",userRankModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",userRankModel.IsClose),
            Config.Conn().CreateDbParameter("@UserRankID",strUserRankID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strUserRankID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_UserRank where UserRankID=@UserRankID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@UserRankID",strUserRankID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strUserRankID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_UserRank set IsClose=@IsClose where UserRankID=@UserRankID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@UserRankID",strUserRankID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 设置操作权限
        /// <summary>
        /// 设置操作权限
        /// </summary>
        public void SetLimit(UserRankModel userRankModel, string strUserRankID)
        {
            StringBuilder sql = new StringBuilder("update t_UserRank set ");
            sql.Append(" LimitValues=@LimitValues");
            sql.Append(" where UserRankID=@UserRankID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@LimitValues",userRankModel.LimitValues),
            Config.Conn().CreateDbParameter("@UserRankID",strUserRankID)};
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
                sql.Append("select ListID from t_UserRank order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_UserRank order by ListID desc");
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
                    sql.Append("create table tmp as select UserRankID from t_UserRank where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_UserRank set ListID=ListID-1 where UserRankID in(select UserRankID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_UserRank set ListID=ListID-1 where UserRankID in(select UserRankID from t_UserRank where  ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select UserRankID from t_UserRank where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_UserRank set ListID=ListID+1 where UserRankID in(select UserRankID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_UserRank set ListID=ListID+1 where UserRankID in(select UserRankID from t_UserRank where  ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion

        #region 取权限字段值
        public StringBuilder GetLimitValues(string strUserID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select t_UserRank.LimitValues from t_UserRank,t_User where t_UserRank.UserRankID=t_User.UserRankID and t_User.UserID=@UserID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@UserID",strUserID)};
            StringBuilder strTemp = new StringBuilder();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                while (dr.Read())
                {
                    strTemp.Append(dr["LimitValues"].ToString());
                }
            }
            return strTemp;
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strUserRankID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_UserRank where UserRankID=@UserRankID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@UserRankID",strUserRankID)};
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
