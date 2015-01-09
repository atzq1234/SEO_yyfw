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
    ///留言反馈-数据访问类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    public class MessageDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Message where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strMessageID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Message where " + strFieldName + "=@" + strFieldName + " and MessageID<>@MessageID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@MessageID",strMessageID)};
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
        public MessageModel GetInfo(string strMessageID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Message where MessageID=@MessageID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@MessageID",strMessageID)};
            MessageModel mesModel = new MessageModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    mesModel.DictionaryID = dr["DictionaryID"].ToString();
                    mesModel.UserID = dr["UserID"].ToString();
                    mesModel.Title = dr["Title"].ToString();
                    mesModel.MessageContent = dr["MessageContent"].ToString();
                    mesModel.AdminID = dr["AdminID"].ToString();
                    mesModel.ParentID = dr["ParentID"].ToString();
                    mesModel.AddTime = dr["AddTime"].ToString();
                    mesModel.IsRead = dr["IsRead"].ToString();
                    mesModel.IsReply = dr["IsReply"].ToString();
                    mesModel.IsEnd = dr["IsEnd"].ToString();
                    return mesModel;
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
        public void InsertInfo(MessageModel mesModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Message(DictionaryID,UserID,Title,MessageContent,AdminID,ParentID,AddTime,IsRead,IsReply,IsEnd)");
            sql.Append(" values(@DictionaryID,@UserID,@Title,@MessageContent,@AdminID,@ParentID,@AddTime,@IsRead,@IsReply,@IsEnd)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@DictionaryID",mesModel.DictionaryID),
            Config.Conn().CreateDbParameter("@UserID",mesModel.UserID),
            Config.Conn().CreateDbParameter("@Title",mesModel.Title),
            Config.Conn().CreateDbParameter("@MessageContent",mesModel.MessageContent),
            Config.Conn().CreateDbParameter("@AdminID",mesModel.AdminID),
            Config.Conn().CreateDbParameter("@ParentID",mesModel.ParentID),
            Config.Conn().CreateDbParameter("@AddTime",mesModel.AddTime),
            Config.Conn().CreateDbParameter("@IsRead",mesModel.IsRead),
            Config.Conn().CreateDbParameter("@IsReply",mesModel.IsReply),
            Config.Conn().CreateDbParameter("@IsEnd",mesModel.IsEnd)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(MessageModel mesModel, string strMessageID)
        {
            StringBuilder sql = new StringBuilder("update t_Message set ");
            sql.Append(" DictionaryID=@DictionaryID,");
            sql.Append(" UserID=@UserID,");
            sql.Append(" Title=@Title,");
            sql.Append(" MessageContent=@MessageContent,");
            sql.Append(" AdminID=@AdminID,");
            sql.Append(" ParentID=@ParentID,");
            sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsRead=@IsRead,");
            sql.Append(" IsReply=@IsReply,");
            sql.Append(" IsEnd=@IsEnd");
            sql.Append(" where  MessageID=@MessageID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@DictionaryID",mesModel.DictionaryID),
            Config.Conn().CreateDbParameter("@UserID",mesModel.UserID),
            Config.Conn().CreateDbParameter("@Title",mesModel.Title),
            Config.Conn().CreateDbParameter("@MessageContent",mesModel.MessageContent),
            Config.Conn().CreateDbParameter("@AdminID",mesModel.AdminID),
            Config.Conn().CreateDbParameter("@ParentID",mesModel.ParentID),
            Config.Conn().CreateDbParameter("@AddTime",mesModel.AddTime),
            Config.Conn().CreateDbParameter("@IsReply",mesModel.IsReply),
            Config.Conn().CreateDbParameter("@IsRead",mesModel.IsRead),
            Config.Conn().CreateDbParameter("@IsEnd",mesModel.IsEnd),
            Config.Conn().CreateDbParameter("@MessageID",strMessageID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strMessageID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Message where MessageID=@MessageID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@MessageID",strMessageID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        /// <summary>
        /// 根据ParentID删除信息
        /// </summary>
        public void DeleteInfoByParentID(string strParentID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Message where ParentID=@ParentID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ParentID",strParentID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 会员/管理员回复信息时更新主题回复状态
        /// <summary>
        /// 会员/管理员回复信息时更新主题回复状态
        /// </summary>
        public void UpdateReplyStatus(string strMessageID, string strIsReply)
        {
            StringBuilder sql = new StringBuilder("update t_Message set ");
            sql.Append(" IsReply=@IsReply");
            sql.Append(" where MessageID=@MessageID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsReply",strIsReply),
            Config.Conn().CreateDbParameter("@MessageID",strMessageID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 会员阅读回复内容时更新主题已读状态
        /// <summary>
        /// 会员阅读回复内容时更新主题已读状态
        /// </summary>
        public void UpdateReadStatus(string strMessageID, string strIsRead)
        {
            StringBuilder sql = new StringBuilder("update t_Message set ");
            sql.Append("IsRead=@IsRead");
            sql.Append(" where  MessageID=@MessageID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsRead",strIsRead),
            Config.Conn().CreateDbParameter("@MessageID",strMessageID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新结束状态
        /// <summary>
        /// 更新结束状态
        /// </summary>
        public void UpdateEndStatus(string strMessageID, string strIsEnd)
        {
            StringBuilder sql = new StringBuilder("update t_Message set ");
            sql.Append("IsEnd=@IsEnd");
            sql.Append(" where  MessageID=@MessageID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsEnd",strIsEnd),
            Config.Conn().CreateDbParameter("@MessageID",strMessageID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strMessageID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Message where MessageID=@MessageID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@MessageID",strMessageID)};
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
