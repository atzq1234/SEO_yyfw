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
    ///邮件订阅-数据访问类
    /// 创建人:杨小明
    /// 日期:2011-4-27
    /// </summary>
    public class MailDAL
    {
        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Mail where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strMailID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Mail where " + strFieldName + "=@" + strFieldName + " and MailID<>@MailID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@MailID",strMailID)};
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
        public MailModel GetInfo(string strMailID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Mail where MailID=@MailID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@MailID",strMailID)};
            MailModel mailModel = new MailModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    mailModel.MailAddress = dr["MailAddress"].ToString();
                    mailModel.IsRec = dr["IsRec"].ToString();
                    mailModel.AddTime = dr["AddTime"].ToString();
                    return mailModel;
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
        public void InsertInfo(MailModel mailModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Mail(MailAddress,IsRec,AddTime)");
            sql.Append(" values(@MailAddress,@IsRec,@AddTime)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@MailAddress",mailModel.MailAddress),
Config.Conn().CreateDbParameter("@IsRec",mailModel.IsRec),
Config.Conn().CreateDbParameter("@AddTime",mailModel.AddTime)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(MailModel mailModel, string strMailID)
        {
            StringBuilder sql = new StringBuilder("update t_Mail set ");
            sql.Append(" MailAddress=@MailAddress,");
            sql.Append(" IsRec=@IsRec,");
            sql.Append(" AddTime=@AddTime");
            sql.Append(" where  MailID=@MailID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@MailAddress",mailModel.MailAddress),
Config.Conn().CreateDbParameter("@IsRec",mailModel.IsRec),
Config.Conn().CreateDbParameter("@AddTime",mailModel.AddTime),
Config.Conn().CreateDbParameter("@MailID",strMailID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strMailID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Mail where MailID=@MailID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@MailID",strMailID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strMailID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Mail where MailID=@MailID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@MailID",strMailID)};
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

        #region 导出邮件地址
        /// <summary>
        /// 导出邮件地址
        /// </summary>
        public void EmailExport(string strSql, string strFilePath, string strFileName)
        {
            using (StreamWriter sw = File.CreateText(strFilePath))
            {
                using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, strSql, null))
                {
                    while (dr.Read())
                    {
                        sw.WriteLine(dr["MailAddress"].ToString());
                    }
                }
                Config.DirectDownFile(strFilePath, strFileName);
            }
        }
        #endregion

    }
}
