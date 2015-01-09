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
    ///信息反馈-数据访问类
    /// 创建人:杨小明
    /// 日期:2011-2-28
    /// </summary>
    public class FeedbackDAL
    {
        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Feedback where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strFeedbackID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Feedback where " + strFieldName + "=@" + strFieldName + " and FeedbackID<>@FeedbackID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@FeedbackID",strFeedbackID)};
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
        public FeedbackModel GetInfo(string strFeedbackID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Feedback where FeedbackID=@FeedbackID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@FeedbackID",strFeedbackID)};
            FeedbackModel feeModel = new FeedbackModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    feeModel.DictionaryID = dr["DictionaryID"].ToString();
                    feeModel.Title = dr["Title"].ToString();
                    feeModel.FeedbackContent = dr["FeedbackContent"].ToString();
                    feeModel.IpAddress = dr["IpAddress"].ToString();
                    feeModel.AddTime = dr["AddTime"].ToString();
                    feeModel.IsDeal = dr["IsDeal"].ToString();
                    feeModel.DealMeno = dr["DealMeno"].ToString();
                    return feeModel;
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
        public void InsertInfo(FeedbackModel feeModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Feedback(DictionaryID,Title,FeedbackContent,IpAddress,AddTime,IsDeal,DealMeno)");
            sql.Append(" values(@DictionaryID,@Title,@FeedbackContent,@IpAddress,@AddTime,@IsDeal,@DealMeno)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@DictionaryID",feeModel.DictionaryID),
Config.Conn().CreateDbParameter("@Title",feeModel.Title),
Config.Conn().CreateDbParameter("@FeedbackContent",feeModel.FeedbackContent),
Config.Conn().CreateDbParameter("@IpAddress",feeModel.IpAddress),
Config.Conn().CreateDbParameter("@AddTime",feeModel.AddTime),
Config.Conn().CreateDbParameter("@IsDeal",feeModel.IsDeal),
Config.Conn().CreateDbParameter("@DealMeno",feeModel.DealMeno)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(FeedbackModel feeModel, string strFeedbackID)
        {
            StringBuilder sql = new StringBuilder("update t_Feedback set ");
            sql.Append(" DictionaryID=@DictionaryID,");
            sql.Append(" Title=@Title,");
            sql.Append(" FeedbackContent=@FeedbackContent,");
            sql.Append(" IpAddress=@IpAddress,");
            sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsDeal=@IsDeal,");
            sql.Append(" DealMeno=@DealMeno");
            sql.Append(" where  FeedbackID=@FeedbackID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@DictionaryID",feeModel.DictionaryID),
Config.Conn().CreateDbParameter("@Title",feeModel.Title),
Config.Conn().CreateDbParameter("@FeedbackContent",feeModel.FeedbackContent),
Config.Conn().CreateDbParameter("@IpAddress",feeModel.IpAddress),
Config.Conn().CreateDbParameter("@AddTime",feeModel.AddTime),
Config.Conn().CreateDbParameter("@IsDeal",feeModel.IsDeal),
Config.Conn().CreateDbParameter("@DealMeno",feeModel.DealMeno),
Config.Conn().CreateDbParameter("@FeedbackID",strFeedbackID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strFeedbackID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Feedback where FeedbackID=@FeedbackID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@FeedbackID",strFeedbackID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 处理信息
        /// <summary>
        /// 处理信息
        /// </summary>
        public void DealInfo(FeedbackModel feeModel, string strFeedbackID)
        {
            StringBuilder sql = new StringBuilder("update t_Feedback set ");
            sql.Append(" IsDeal=@IsDeal,");
            sql.Append(" DealMeno=@DealMeno");
            sql.Append(" where  FeedbackID=@FeedbackID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@IsDeal",feeModel.IsDeal),
Config.Conn().CreateDbParameter("@DealMeno",feeModel.DealMeno),
Config.Conn().CreateDbParameter("@FeedbackID",strFeedbackID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strFeedbackID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Feedback where FeedbackID=@FeedbackID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@FeedbackID",strFeedbackID)};
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
