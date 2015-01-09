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
    ///调查结果-数据访问类
    /// 创建人:杨小明
    /// 日期:2011-12-27
    /// </summary>
    public class SurveyResultDAL
    {
        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_SurveyResult where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strSurveyResultID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_SurveyResult where " + strFieldName + "=@" + strFieldName + " and SurveyResultID<>@SurveyResultID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@SurveyResultID",strSurveyResultID)};
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
        public string GetValueByField(string strFieldName, string strSurveyResultID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_SurveyResult where SurveyResultID=@SurveyResultID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@SurveyResultID",strSurveyResultID)};
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
        public SurveyResultModel GetInfo(string strSurveyResultID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_SurveyResult where SurveyResultID=@SurveyResultID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@SurveyResultID",strSurveyResultID)};
            SurveyResultModel surResModel = new SurveyResultModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    surResModel.SurveyContent = dr["SurveyContent"].ToString();
                    surResModel.IP = dr["IP"].ToString();
                    surResModel.SurveyID = dr["SurveyID"].ToString();
                    surResModel.AddTime = dr["AddTime"].ToString();
                    return surResModel;
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
        public void InsertInfo(SurveyResultModel surResModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_SurveyResult(SurveyContent,IP,SurveyID,AddTime)");
            sql.Append(" values(@SurveyContent,@IP,@SurveyID,@AddTime)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@SurveyContent",surResModel.SurveyContent),
Config.Conn().CreateDbParameter("@IP",surResModel.IP),
Config.Conn().CreateDbParameter("@SurveyID",surResModel.SurveyID),
Config.Conn().CreateDbParameter("@AddTime",surResModel.AddTime)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(SurveyResultModel surResModel, string strSurveyResultID)
        {
            StringBuilder sql = new StringBuilder("update t_SurveyResult set ");
            sql.Append(" SurveyContent=@SurveyContent,");
            sql.Append(" IP=@IP,");
            sql.Append(" SurveyID=@SurveyID,");
            sql.Append(" AddTime=@AddTime");
            sql.Append(" where  SurveyResultID=@SurveyResultID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@SurveyContent",surResModel.SurveyContent),
Config.Conn().CreateDbParameter("@IP",surResModel.IP),
Config.Conn().CreateDbParameter("@SurveyID",surResModel.SurveyID),
Config.Conn().CreateDbParameter("@AddTime",surResModel.AddTime),
Config.Conn().CreateDbParameter("@SurveyResultID",strSurveyResultID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strSurveyResultID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_SurveyResult where SurveyResultID=@SurveyResultID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@SurveyResultID",strSurveyResultID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

    }
}
