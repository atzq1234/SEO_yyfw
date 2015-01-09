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
    ///在线调查-数据访问类
    /// 创建人:杨小明
    /// 日期:2011-12-26
    /// </summary>
    public class SurveyDAL
    {
        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Survey where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strSurveyID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Survey where " + strFieldName + "=@" + strFieldName + " and SurveyID<>@SurveyID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@SurveyID",strSurveyID)};
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
        public string GetValueByField(string strFieldName, string strSurveyID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Survey where SurveyID=@SurveyID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@SurveyID",strSurveyID)};
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
        public SurveyModel GetInfo(string strSurveyID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Survey where SurveyID=@SurveyID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@SurveyID",strSurveyID)};
            SurveyModel surModel = new SurveyModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    surModel.ClassID = dr["ClassID"].ToString();
                    surModel.Subject = dr["Subject"].ToString();
                    surModel.IntrContent = dr["IntrContent"].ToString();
                    surModel.IsRecommend = dr["IsRecommend"].ToString();
                    surModel.ClickNum = dr["ClickNum"].ToString();
                    surModel.ListID = dr["ListID"].ToString();
                    surModel.AdminID = dr["AdminID"].ToString();
                    surModel.AddTime = dr["AddTime"].ToString();
                    surModel.IsClose = dr["IsClose"].ToString();
                    return surModel;
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
        public SurveyModel GetInfo2(string strSurveyID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Survey where IsClose=0 and SurveyID=@SurveyID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@SurveyID",strSurveyID)};
            SurveyModel surModel = new SurveyModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    surModel.ClassID = dr["ClassID"].ToString();
                    surModel.Subject = dr["Subject"].ToString();
                    surModel.IntrContent = dr["IntrContent"].ToString();
                    surModel.IsRecommend = dr["IsRecommend"].ToString();
                    surModel.ClickNum = dr["ClickNum"].ToString();
                    surModel.ListID = dr["ListID"].ToString();
                    surModel.AdminID = dr["AdminID"].ToString();
                    surModel.AddTime = dr["AddTime"].ToString();
                    surModel.IsClose = dr["IsClose"].ToString();
                    return surModel;
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
        public void InsertInfo(SurveyModel surModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Survey(ClassID,Subject,IntrContent,IsRecommend,ListID,ClickNum,AdminID,AddTime,IsClose)");
            sql.Append(" values(@ClassID,@Subject,@IntrContent,@IsRecommend,@ListID,@ClickNum,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",surModel.ClassID),
            Config.Conn().CreateDbParameter("@Subject",surModel.Subject),
            Config.Conn().CreateDbParameter("@IntrContent",surModel.IntrContent),
            Config.Conn().CreateDbParameter("@IsRecommend",surModel.IsRecommend),
            Config.Conn().CreateDbParameter("@ListID",surModel.ListID),
            Config.Conn().CreateDbParameter("@ClickNum",surModel.ClickNum),
            Config.Conn().CreateDbParameter("@AdminID",surModel.AdminID),
            Config.Conn().CreateDbParameter("@AddTime",surModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",surModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(SurveyModel surModel, string strSurveyID)
        {
            StringBuilder sql = new StringBuilder("update t_Survey set ");
            sql.Append(" ClassID=@ClassID,");
            sql.Append(" Subject=@Subject,");
            sql.Append(" IntrContent=@IntrContent,");
            sql.Append(" IsRecommend=@IsRecommend,");
            sql.Append(" ListID=@ListID,");
            sql.Append(" ClickNum=@ClickNum,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where  SurveyID=@SurveyID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassID",surModel.ClassID),
Config.Conn().CreateDbParameter("@Subject",surModel.Subject),
Config.Conn().CreateDbParameter("@IntrContent",surModel.IntrContent),
Config.Conn().CreateDbParameter("@IsRecommend",surModel.IsRecommend),
Config.Conn().CreateDbParameter("@ListID",surModel.ListID),
Config.Conn().CreateDbParameter("@ClickNum",surModel.ClickNum),
//Config.Conn().CreateDbParameter("@AdminID",surModel.AdminID),
//Config.Conn().CreateDbParameter("@AddTime",surModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",surModel.IsClose),
Config.Conn().CreateDbParameter("@SurveyID",strSurveyID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strSurveyID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Survey where SurveyID=@SurveyID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@SurveyID",strSurveyID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strSurveyID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Survey set IsClose=@IsClose where SurveyID=@SurveyID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@SurveyID",strSurveyID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 转移信息
        /// <summary>
        /// 转移信息
        /// </summary>
        public void TransferInfo(string strSurveyID, string strClassID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Survey set ClassID=@ClassID where SurveyID=@SurveyID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID),
            Config.Conn().CreateDbParameter("@SurveyID",strSurveyID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 访问数加1
        /// <summary>
        /// 访问数加1
        /// </summary>
        public void Click(string strSurveyID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Survey set ClickNum=ClickNum+1 where SurveyID=@SurveyID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@SurveyID",strSurveyID)};
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
                sql.Append("select ListID from t_Survey order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_Survey order by ListID desc");
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
                    sql.Append("create table tmp as select SurveyID from t_Survey where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_Survey set ListID=ListID-1 where SurveyID in(select SurveyID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Survey set ListID=ListID-1 where SurveyID in(select SurveyID from t_Survey where  ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select SurveyID from t_Survey where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_Survey set ListID=ListID+1 where SurveyID in(select SurveyID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Survey set ListID=ListID+1 where SurveyID in(select SurveyID from t_Survey where  ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion

    }
}
