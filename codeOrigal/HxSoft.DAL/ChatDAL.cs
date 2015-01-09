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
    ///聊天工具-数据访问类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    public class ChatDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Chat where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strChatID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Chat where " + strFieldName + "=@" + strFieldName + " and ChatID<>@ChatID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@ChatID",strChatID)};
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
        public ChatModel GetInfo(string strChatID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Chat where ChatID=@ChatID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ChatID",strChatID)};
            ChatModel chaModel = new ChatModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    chaModel.ConfigID = dr["ConfigID"].ToString();
                    chaModel.TypeID = dr["TypeID"].ToString();
                    chaModel.NickName = dr["NickName"].ToString();
                    chaModel.Account = dr["Account"].ToString();
                    chaModel.ChatKey = dr["ChatKey"].ToString();
                    chaModel.ListID = dr["ListID"].ToString();
                    chaModel.AdminID = dr["AdminID"].ToString();
                    chaModel.AddTime = dr["AddTime"].ToString();
                    chaModel.IsClose = dr["IsClose"].ToString();
                    return chaModel;
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
        public void InsertInfo(ChatModel chaModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Chat(ConfigID,TypeID,NickName,Account,ChatKey,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@ConfigID,@TypeID,@NickName,@Account,@ChatKey,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ConfigID",chaModel.ConfigID),
Config.Conn().CreateDbParameter("@TypeID",chaModel.TypeID),
Config.Conn().CreateDbParameter("@NickName",chaModel.NickName),
Config.Conn().CreateDbParameter("@Account",chaModel.Account),
Config.Conn().CreateDbParameter("@ChatKey",chaModel.ChatKey),
Config.Conn().CreateDbParameter("@ListID",chaModel.ListID),
Config.Conn().CreateDbParameter("@AdminID",chaModel.AdminID),
Config.Conn().CreateDbParameter("@AddTime",chaModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",chaModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(ChatModel chaModel, string strChatID)
        {
            StringBuilder sql = new StringBuilder("update t_Chat set ");
            sql.Append(" ConfigID=@ConfigID,");
            sql.Append(" TypeID=@TypeID,");
            sql.Append(" NickName=@NickName,");
            sql.Append(" Account=@Account,");
            sql.Append(" ChatKey=@ChatKey,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where  ChatID=@ChatID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ConfigID",chaModel.ConfigID),
Config.Conn().CreateDbParameter("@TypeID",chaModel.TypeID),
Config.Conn().CreateDbParameter("@NickName",chaModel.NickName),
Config.Conn().CreateDbParameter("@Account",chaModel.Account),
Config.Conn().CreateDbParameter("@ChatKey",chaModel.ChatKey),
Config.Conn().CreateDbParameter("@ListID",chaModel.ListID),
//Config.Conn().CreateDbParameter("@AdminID",chaModel.AdminID),
//Config.Conn().CreateDbParameter("@AddTime",chaModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",chaModel.IsClose),
Config.Conn().CreateDbParameter("@ChatID",strChatID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strChatID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Chat where ChatID=@ChatID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ChatID",strChatID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strChatID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Chat set IsClose=@IsClose where ChatID=@ChatID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@ChatID",strChatID)};
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
                sql.Append("select ListID from t_Chat order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_Chat order by ListID desc");
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
                    sql.Append("create table tmp as select ChatID from t_Chat where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_Chat set ListID=ListID-1 where ChatID in(select ChatID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Chat set ListID=ListID-1 where ChatID in(select ChatID from t_Chat where  ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select ChatID from t_Chat where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_Chat set ListID=ListID+1 where ChatID in(select ChatID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Chat set ListID=ListID+1 where ChatID in(select ChatID from t_Chat where  ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strChatID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Chat where ChatID=@ChatID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ChatID",strChatID)};
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
