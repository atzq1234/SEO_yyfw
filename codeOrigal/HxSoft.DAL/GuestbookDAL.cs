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
    ///留言板-数据访问类
    /// 创建人:杨小明
    /// 日期:2011-9-16
    /// </summary>
    public class GuestbookDAL
    {
        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Guestbook where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strGuestbookID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Guestbook where " + strFieldName + "=@" + strFieldName + " and GuestbookID<>@GuestbookID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@GuestbookID",strGuestbookID)};
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
        public string GetValueByField(string strFieldName, string strGuestbookID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Guestbook where GuestbookID=@GuestbookID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@GuestbookID",strGuestbookID)};
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
        public GuestbookModel GetInfo(string strGuestbookID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Guestbook where GuestbookID=@GuestbookID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@GuestbookID",strGuestbookID)};
            GuestbookModel gbookModel = new GuestbookModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    gbookModel.NickName = dr["NickName"].ToString();
                    gbookModel.BookContent = dr["BookContent"].ToString();
                    gbookModel.IpAddress = dr["IpAddress"].ToString();
                    gbookModel.AddTime = dr["AddTime"].ToString();
                    gbookModel.IsReply = dr["IsReply"].ToString();
                    gbookModel.ReplyContent = dr["ReplyContent"].ToString();
                    gbookModel.ReplyTime = dr["ReplyTime"].ToString();
                    gbookModel.AdminID = dr["AdminID"].ToString();
                    gbookModel.IsClose = dr["IsClose"].ToString();
                    gbookModel.TelePhone = dr["TelePhone"].ToString();
                    gbookModel.Email = dr["Email"].ToString();
                    return gbookModel;
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
        public void InsertInfo(GuestbookModel gbookModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Guestbook(NickName,BookContent,IpAddress,AddTime,IsReply,ReplyContent,ReplyTime,AdminID,IsClose,TelePhone,Email)");
            sql.Append(" values(@NickName,@BookContent,@IpAddress,@AddTime,@IsReply,@ReplyContent,@ReplyTime,@AdminID,@IsClose,@TelePhone,@Email)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@NickName",gbookModel.NickName),
Config.Conn().CreateDbParameter("@BookContent",gbookModel.BookContent),
Config.Conn().CreateDbParameter("@IpAddress",gbookModel.IpAddress),
Config.Conn().CreateDbParameter("@AddTime",gbookModel.AddTime),
Config.Conn().CreateDbParameter("@IsReply",gbookModel.IsReply),
Config.Conn().CreateDbParameter("@ReplyContent",gbookModel.ReplyContent),
Config.Conn().CreateDbParameter("@ReplyTime",gbookModel.ReplyTime),
Config.Conn().CreateDbParameter("@AdminID",gbookModel.AdminID),
Config.Conn().CreateDbParameter("@IsClose",gbookModel.IsClose),
                                      Config.Conn().CreateDbParameter("@TelePhone",gbookModel.TelePhone),
                                      Config.Conn().CreateDbParameter("@Email",gbookModel.Email)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(GuestbookModel gbookModel, string strGuestbookID)
        {
            StringBuilder sql = new StringBuilder("update t_Guestbook set ");
            sql.Append(" NickName=@NickName,");
            sql.Append(" BookContent=@BookContent,");
            //sql.Append(" IpAddress=@IpAddress,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsReply=@IsReply,");
            sql.Append(" ReplyContent=@ReplyContent,");
            sql.Append(" ReplyTime=@ReplyTime,");
            sql.Append(" AdminID=@AdminID,");
            sql.Append(" IsClose=@IsClose,");
            sql.Append(" TelePhone=@TelePhone,");
            sql.Append(" Email=@Email");
            sql.Append(" where  GuestbookID=@GuestbookID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@NickName",gbookModel.NickName),
Config.Conn().CreateDbParameter("@BookContent",gbookModel.BookContent),
//Config.Conn().CreateDbParameter("@IpAddress",gbookModel.IpAddress),
//Config.Conn().CreateDbParameter("@AddTime",gbookModel.AddTime),
Config.Conn().CreateDbParameter("@IsReply",gbookModel.IsReply),
Config.Conn().CreateDbParameter("@ReplyContent",gbookModel.ReplyContent),
Config.Conn().CreateDbParameter("@ReplyTime",gbookModel.ReplyTime),
Config.Conn().CreateDbParameter("@AdminID",gbookModel.AdminID),
Config.Conn().CreateDbParameter("@IsClose",gbookModel.IsClose),
Config.Conn().CreateDbParameter("@GuestbookID",strGuestbookID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strGuestbookID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Guestbook where GuestbookID=@GuestbookID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@GuestbookID",strGuestbookID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strGuestbookID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Guestbook set IsClose=@IsClose where GuestbookID=@GuestbookID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@GuestbookID",strGuestbookID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion


    }
}
