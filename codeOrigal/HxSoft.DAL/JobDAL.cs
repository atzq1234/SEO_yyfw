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
    ///招聘表-数据访问类
    /// 创建人:杨小明
    /// 日期:2010/11/2
    /// </summary>
    public class JobDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Job where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strJobID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Job where " + strFieldName + "=@" + strFieldName + " and JobID<>@JobID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@JobID",strJobID)};
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
        public JobModel GetInfo(string strJobID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Job where JobID=@JobID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@JobID",strJobID)};
            JobModel jobModel = new JobModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    jobModel.ClassID = dr["ClassID"].ToString();
                    jobModel.JobName = dr["JobName"].ToString();
                    jobModel.Department = dr["Department"].ToString();
                    jobModel.JobNum = dr["JobNum"].ToString();
                    jobModel.Salary = dr["Salary"].ToString();
                    jobModel.WorkPlace = dr["WorkPlace"].ToString();
                    jobModel.EndTime = dr["EndTime"].ToString();
                    jobModel.Tags = dr["Tags"].ToString();
                    jobModel.Keywords = dr["Keywords"].ToString();
                    jobModel.Description = dr["Description"].ToString();
                    jobModel.Demand = dr["Demand"].ToString();
                    jobModel.ClickNum = dr["ClickNum"].ToString();
                    jobModel.ListID = dr["ListID"].ToString();
                    jobModel.AdminID = dr["AdminID"].ToString();
                    jobModel.AddTime = dr["AddTime"].ToString();
                    jobModel.IsClose = dr["IsClose"].ToString();
                    jobModel.IsRecommend = dr["IsRecommend"].ToString();
                    return jobModel;
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
        public JobModel GetInfo2(string strJobID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Job where JobID=@JobID and IsClose=0");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@JobID",strJobID)};
            JobModel jobModel = new JobModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    jobModel.ClassID = dr["ClassID"].ToString();
                    jobModel.JobName = dr["JobName"].ToString();
                    jobModel.Department = dr["Department"].ToString();
                    jobModel.JobNum = dr["JobNum"].ToString();
                    jobModel.Salary = dr["Salary"].ToString();
                    jobModel.WorkPlace = dr["WorkPlace"].ToString();
                    jobModel.EndTime = dr["EndTime"].ToString();
                    jobModel.Tags = dr["Tags"].ToString();
                    jobModel.Keywords = dr["Keywords"].ToString();
                    jobModel.Description = dr["Description"].ToString();
                    jobModel.Demand = dr["Demand"].ToString();
                    jobModel.ClickNum = dr["ClickNum"].ToString();
                    jobModel.ListID = dr["ListID"].ToString();
                    jobModel.AdminID = dr["AdminID"].ToString();
                    jobModel.AddTime = dr["AddTime"].ToString();
                    jobModel.IsClose = dr["IsClose"].ToString();
                    jobModel.IsRecommend = dr["IsRecommend"].ToString();
                    return jobModel;
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
        public void InsertInfo(JobModel jobModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Job(ClassID,JobName,Department,JobNum,Salary,WorkPlace,EndTime,Tags,Keywords,Description,Demand,ClickNum,ListID,AdminID,AddTime,IsClose,IsRecommend)");
            sql.Append(" values(@ClassID,@JobName,@Department,@JobNum,@Salary,@WorkPlace,@EndTime,@Tags,@Keywords,@Description,@Demand,@ClickNum,@ListID,@AdminID,@AddTime,@IsClose,@IsRecommend)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassID",jobModel.ClassID),
Config.Conn().CreateDbParameter("@JobName",jobModel.JobName),
Config.Conn().CreateDbParameter("@Department",jobModel.Department),
Config.Conn().CreateDbParameter("@JobNum",jobModel.JobNum),
Config.Conn().CreateDbParameter("@Salary",jobModel.Salary),
Config.Conn().CreateDbParameter("@WorkPlace",jobModel.WorkPlace),
Config.Conn().CreateDbParameter("@EndTime",jobModel.EndTime),
Config.Conn().CreateDbParameter("@Tags",jobModel.Tags),
Config.Conn().CreateDbParameter("@Keywords",jobModel.Keywords),
Config.Conn().CreateDbParameter("@Description",jobModel.Description),
Config.Conn().CreateDbParameter("@Demand",jobModel.Demand),
Config.Conn().CreateDbParameter("@ClickNum",jobModel.ClickNum),
Config.Conn().CreateDbParameter("@ListID",jobModel.ListID),
Config.Conn().CreateDbParameter("@AdminID",jobModel.AdminID),
Config.Conn().CreateDbParameter("@AddTime",jobModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",jobModel.IsClose),
Config.Conn().CreateDbParameter("@IsRecommend",jobModel.IsRecommend)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(JobModel jobModel, string strJobID)
        {
            StringBuilder sql = new StringBuilder("update t_Job set ");
            sql.Append(" ClassID=@ClassID,");
            sql.Append(" JobName=@JobName,");
            sql.Append(" Department=@Department,");
            sql.Append(" JobNum=@JobNum,");
            sql.Append(" Salary=@Salary,");
            sql.Append(" WorkPlace=@WorkPlace,");
            sql.Append(" EndTime=@EndTime,");
            sql.Append(" Tags=@Tags,");
            sql.Append(" Keywords=@Keywords,");
            sql.Append(" Description=@Description,");
            sql.Append(" Demand=@Demand,");
            sql.Append(" ClickNum=@ClickNum,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose,");
            sql.Append(" IsRecommend=@IsRecommend");
            sql.Append(" where  JobID=@JobID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassID",jobModel.ClassID),
Config.Conn().CreateDbParameter("@JobName",jobModel.JobName),
Config.Conn().CreateDbParameter("@Department",jobModel.Department),
Config.Conn().CreateDbParameter("@JobNum",jobModel.JobNum),
Config.Conn().CreateDbParameter("@Salary",jobModel.Salary),
Config.Conn().CreateDbParameter("@WorkPlace",jobModel.WorkPlace),
Config.Conn().CreateDbParameter("@EndTime",jobModel.EndTime),
Config.Conn().CreateDbParameter("@Tags",jobModel.Tags),
Config.Conn().CreateDbParameter("@Keywords",jobModel.Keywords),
Config.Conn().CreateDbParameter("@Description",jobModel.Description),
Config.Conn().CreateDbParameter("@Demand",jobModel.Demand),
Config.Conn().CreateDbParameter("@ClickNum",jobModel.ClickNum),
Config.Conn().CreateDbParameter("@ListID",jobModel.ListID),
//Config.Conn().CreateDbParameter("@AdminID",jobModel.AdminID),
//Config.Conn().CreateDbParameter("@AddTime",jobModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",jobModel.IsClose),
Config.Conn().CreateDbParameter("@IsRecommend",jobModel.IsRecommend),
Config.Conn().CreateDbParameter("@JobID",strJobID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strJobID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Job where JobID=@JobID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@JobID",strJobID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strJobID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Job set IsClose=@IsClose where JobID=@JobID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@JobID",strJobID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 转移信息
        /// <summary>
        /// 转移信息
        /// </summary>
        public void TransferInfo(string strJobID, string strClassID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Job set ClassID=@ClassID where JobID=@JobID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID),
            Config.Conn().CreateDbParameter("@JobID",strJobID)};
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
                sql.Append("select ListID from t_Job order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_Job order by ListID desc");
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
                    sql.Append("create table tmp as select JobID from t_Job where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_Job set ListID=ListID-1 where JobID in(select JobID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Job set ListID=ListID-1 where JobID in(select JobID from t_Job where  ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select JobID from t_Job where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_Job set ListID=ListID+1 where JobID in(select JobID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Job set ListID=ListID+1 where JobID in(select JobID from t_Job where  ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion

        #region 访问数加1
        /// <summary>
        /// 访问数加1
        /// </summary>
        public void Click(string strJobID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Job set ClickNum=ClickNum+1 where JobID=@JobID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@JobID",strJobID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strJobID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Job where JobID=@JobID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@JobID",strJobID)};
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
