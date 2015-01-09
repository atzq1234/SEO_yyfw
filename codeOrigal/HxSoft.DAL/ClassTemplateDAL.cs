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
    ///栏目模板-数据访问类
    /// 创建人:杨小明
    /// 日期:2012-4-16
    /// </summary>
    public class ClassTemplateDAL
    {
        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_ClassTemplate where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strClassTemplateID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_ClassTemplate where " + strFieldName + "=@" + strFieldName + " and ClassTemplateID<>@ClassTemplateID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@ClassTemplateID",strClassTemplateID)};
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
        public string GetValueByField(string strFieldName, string strClassTemplateID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_ClassTemplate where ClassTemplateID=@ClassTemplateID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassTemplateID",strClassTemplateID)};
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
        public ClassTemplateModel GetInfo(string strClassTemplateID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_ClassTemplate where ClassTemplateID=@ClassTemplateID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassTemplateID",strClassTemplateID)};
            ClassTemplateModel claTempModel = new ClassTemplateModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    claTempModel.ClassPropertyID = dr["ClassPropertyID"].ToString();
                    claTempModel.TemplateName = dr["TemplateName"].ToString();
                    claTempModel.TemplatePath = dr["TemplatePath"].ToString();
                    claTempModel.ListID = dr["ListID"].ToString();
                    claTempModel.AdminID = dr["AdminID"].ToString();
                    claTempModel.AddTime = dr["AddTime"].ToString();
                    claTempModel.IsClose = dr["IsClose"].ToString();
                    return claTempModel;
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
        public void InsertInfo(ClassTemplateModel claTempModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_ClassTemplate(ClassPropertyID,TemplateName,TemplatePath,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@ClassPropertyID,@TemplateName,@TemplatePath,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassPropertyID",claTempModel.ClassPropertyID),
Config.Conn().CreateDbParameter("@TemplateName",claTempModel.TemplateName),
Config.Conn().CreateDbParameter("@TemplatePath",claTempModel.TemplatePath),
Config.Conn().CreateDbParameter("@ListID",claTempModel.ListID),
Config.Conn().CreateDbParameter("@AdminID",claTempModel.AdminID),
Config.Conn().CreateDbParameter("@AddTime",claTempModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",claTempModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(ClassTemplateModel claTempModel, string strClassTemplateID)
        {
            StringBuilder sql = new StringBuilder("update t_ClassTemplate set ");
            sql.Append(" ClassPropertyID=@ClassPropertyID,");
            sql.Append(" TemplateName=@TemplateName,");
            sql.Append(" TemplatePath=@TemplatePath,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where  ClassTemplateID=@ClassTemplateID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassPropertyID",claTempModel.ClassPropertyID),
Config.Conn().CreateDbParameter("@TemplateName",claTempModel.TemplateName),
Config.Conn().CreateDbParameter("@TemplatePath",claTempModel.TemplatePath),
Config.Conn().CreateDbParameter("@ListID",claTempModel.ListID),
//Config.Conn().CreateDbParameter("@AdminID",claTempModel.AdminID),
//Config.Conn().CreateDbParameter("@AddTime",claTempModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",claTempModel.IsClose),
Config.Conn().CreateDbParameter("@ClassTemplateID",strClassTemplateID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strClassTemplateID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_ClassTemplate where ClassTemplateID=@ClassTemplateID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassTemplateID",strClassTemplateID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strClassTemplateID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_ClassTemplate set IsClose=@IsClose where ClassTemplateID=@ClassTemplateID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@IsClose",strIsClose),
Config.Conn().CreateDbParameter("@ClassTemplateID",strClassTemplateID)};
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
                sql.Append("select ListID from t_ClassTemplate order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_ClassTemplate order by ListID desc");
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
                    sql.Append("create table tmp as select ClassTemplateID from t_ClassTemplate where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_ClassTemplate set ListID=ListID-1 where ClassTemplateID in(select ClassTemplateID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_ClassTemplate set ListID=ListID-1 where ClassTemplateID in(select ClassTemplateID from t_ClassTemplate where  ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select ClassTemplateID from t_ClassTemplate where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_ClassTemplate set ListID=ListID+1 where ClassTemplateID in(select ClassTemplateID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_ClassTemplate set ListID=ListID+1 where ClassTemplateID in(select ClassTemplateID from t_ClassTemplate where  ListID>=@ListID and ListID<@OldListID)");
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
