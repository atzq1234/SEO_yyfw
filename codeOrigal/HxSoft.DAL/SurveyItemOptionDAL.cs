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
    ///调查子选顶 -数据访问类
    /// 创建人:杨小明
    /// 日期:2011-12-26
    /// </summary>
    public class SurveyItemOptionDAL
    {
        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_SurveyItemOption where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strSurveyItemOptionID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_SurveyItemOption where " + strFieldName + "=@" + strFieldName + " and SurveyItemOptionID<>@SurveyItemOptionID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@SurveyItemOptionID",strSurveyItemOptionID)};
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
        public string GetValueByField(string strFieldName, string strSurveyItemOptionID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_SurveyItemOption where SurveyItemOptionID=@SurveyItemOptionID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@SurveyItemOptionID",strSurveyItemOptionID)};
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
        public SurveyItemOptionModel GetInfo(string strSurveyItemOptionID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_SurveyItemOption where SurveyItemOptionID=@SurveyItemOptionID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@SurveyItemOptionID",strSurveyItemOptionID)};
            SurveyItemOptionModel surChItModel = new SurveyItemOptionModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    surChItModel.ItemOptionName = dr["ItemOptionName"].ToString();
                    surChItModel.SurveyItemID = dr["SurveyItemID"].ToString();
                    surChItModel.ListID = dr["ListID"].ToString();
                    surChItModel.AdminID = dr["AdminID"].ToString();
                    return surChItModel;
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
        public void InsertInfo(SurveyItemOptionModel surChItModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_SurveyItemOption(ItemOptionName,SurveyItemID,ListID,AdminID)");
            sql.Append(" values(@ItemOptionName,@SurveyItemID,@ListID,@AdminID)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ItemOptionName",surChItModel.ItemOptionName),
Config.Conn().CreateDbParameter("@SurveyItemID",surChItModel.SurveyItemID),
Config.Conn().CreateDbParameter("@ListID",surChItModel.ListID),
Config.Conn().CreateDbParameter("@AdminID",surChItModel.AdminID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(SurveyItemOptionModel surChItModel, string strSurveyItemOptionID)
        {
            StringBuilder sql = new StringBuilder("update t_SurveyItemOption set ");
            sql.Append(" ItemOptionName=@ItemOptionName,");
            sql.Append(" SurveyItemID=@SurveyItemID,");
            sql.Append(" ListID=@ListID,");
            sql.Append(" AdminID=@AdminID");
            sql.Append(" where  SurveyItemOptionID=@SurveyItemOptionID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ItemOptionName",surChItModel.ItemOptionName),
Config.Conn().CreateDbParameter("@SurveyItemID",surChItModel.SurveyItemID),
Config.Conn().CreateDbParameter("@ListID",surChItModel.ListID),
Config.Conn().CreateDbParameter("@AdminID",surChItModel.AdminID),
Config.Conn().CreateDbParameter("@SurveyItemOptionID",strSurveyItemOptionID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strSurveyItemOptionID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_SurveyItemOption where SurveyItemOptionID=@SurveyItemOptionID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@SurveyItemOptionID",strSurveyItemOptionID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID(string strSurveyItemID)
        {
            StringBuilder sql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                sql.Append("select ListID from t_SurveyItemOption where SurveyItemID=@SurveyItemID order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_SurveyItemOption where SurveyItemID=@SurveyItemID order by ListID desc");
            }
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@SurveyItemID",strSurveyItemID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
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
        public void OrderInfo(string strSurveyItemID, string strListID, string strOldListID)
        {
            if (Convert.ToInt32(strListID) > Convert.ToInt32(strOldListID))
            {
                StringBuilder sql = new StringBuilder();
                if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
                {
                    sql.Append("create table tmp as select SurveyItemOptionID from t_SurveyItemOption where SurveyItemID=@SurveyItemID and ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_SurveyItemOption set ListID=ListID-1 where SurveyItemOptionID in(select SurveyItemOptionID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_SurveyItemOption set ListID=ListID-1 where SurveyItemOptionID in(select SurveyItemOptionID from t_SurveyItemOption where SurveyItemID=@SurveyItemID and ListID<=@ListID and ListID>@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@SurveyItemID",strSurveyItemID),
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
            else if (Convert.ToInt32(strListID) < Convert.ToInt32(strOldListID))
            {
                StringBuilder sql = new StringBuilder();
                if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
                {
                    sql.Append("create table tmp as select SurveyItemOptionID from t_SurveyItemOption where SurveyItemID=@SurveyItemID and ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_SurveyItemOption set ListID=ListID+1 where SurveyItemOptionID in(select SurveyItemOptionID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_SurveyItemOption set ListID=ListID+1 where SurveyItemOptionID in(select SurveyItemOptionID from t_SurveyItemOption where SurveyItemID=@SurveyItemID and ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@SurveyItemID",strSurveyItemID),
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion
        
        #region 根据SurveyItemID删除信息
        /// <summary>
        /// 根据SurveyItemID删除信息
        /// </summary>
        public void DeleteInfoBySurveyItemID(string strSurveyItemID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_SurveyItemOption where SurveyItemID=@SurveyItemID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@SurveyItemID",strSurveyItemID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 根据SurveyID删除信息
        /// <summary>
        /// 根据SurveyID删除信息
        /// </summary>
        public void DeleteInfoBySurveyID(string strSurveyID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_SurveyItemOption where SurveyItemID in (select SurveyItemID from t_SurveyItem where SurveyID=@SurveyID)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@SurveyID",strSurveyID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新SurveyItemID
        /// <summary>
        /// 更新信息SurveyItemID
        /// </summary>
        public void UpdateSurveyItemID(string strSurveyItemID, string strAdminID)
        {
            StringBuilder sql = new StringBuilder("update t_SurveyItemOption set ");
            sql.Append(" SurveyItemID=@SurveyItemID");
            sql.Append(" where SurveyItemID=0 and AdminID=@AdminID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@SurveyItemID",strSurveyItemID),
            Config.Conn().CreateDbParameter("@AdminID",strAdminID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

    }
}
