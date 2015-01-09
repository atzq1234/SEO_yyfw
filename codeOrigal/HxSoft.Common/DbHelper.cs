using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace HxSoft.Common
{
    /// <summary>
    /// 数据库接口调用类
    /// </summary>
    public class DbHelper
    {
        private IDbHelper _idbhelper;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DbHelper()
        {
            if (Config.DatabaseType == Config.DatabaseTypeCollection.Sql.ToString())
            {
                _idbhelper = new SqlHelper(Config.SqlConnStr);
            }
            else if (Config.DatabaseType == Config.DatabaseTypeCollection.OleDb.ToString())
            {
                _idbhelper = new OleDbHelper(Config.AccessConnStr);
            }
            else if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                _idbhelper = new MySqlHelper(Config.MySqlConnStr);
            }
            else
            {
                _idbhelper = new OleDbHelper(Config.AccessConnStr);
            }
        }
        #endregion

        #region 接口调用
        /// <summary>
        /// 执行查询，返回DataSet
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public DataSet GetDataSet(CommandType cmdType, string cmdText, DbParameter[] cmdParams)
        {
            return _idbhelper.GetDataSet(cmdType, cmdText, cmdParams);
        }

        /// <summary>
        /// 在事务中执行查询，返回DataSet
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public DataSet GetDataSet(DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParams)
        {
            return _idbhelper.GetDataSet(trans, cmdType, cmdText, cmdParams);
        }

        /// <summary>
        /// 执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public int ExecuteSql(CommandType cmdType, string cmdText, DbParameter[] cmdParams)
        {
            return _idbhelper.ExecuteSql(cmdType, cmdText, cmdParams);
        }

        /// <summary>
        /// 在事务中执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public int ExecuteSql(DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParams)
        {
            return _idbhelper.ExecuteSql(trans, cmdType, cmdText, cmdParams);
        }

        /// <summary>
        /// 执行查询，返回DataReader
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public DbDataReader GetDataReader(CommandType cmdType, string cmdText, DbParameter[] cmdParams)
        {
            return _idbhelper.GetDataReader(cmdType, cmdText, cmdParams);
        }

        /// <summary>
        /// 在事务中执行查询，返回DataReader
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public DbDataReader GetDataReader(DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParams)
        {
            return _idbhelper.GetDataReader(trans, cmdType, cmdText, cmdParams);
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public object GetScalar(CommandType cmdType, string cmdText, DbParameter[] cmdParams)
        {
            return _idbhelper.GetScalar(cmdType, cmdText, cmdParams);
        }

        /// <summary>
        /// 在事务中执行查询，并返回查询所返回的结果集中第一行的第一列。
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public object GetScalar(DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParams)
        {
            return _idbhelper.GetScalar(trans, cmdType, cmdText, cmdParams);
        }
        #endregion

        /// <summary>
        /// 创造DbParameter的实例
        /// </summary>
        /// <param name="paraName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DbParameter CreateDbParameter(string paraName, object value)
        {
            DbParameter para;
            if (Config.DatabaseType == Config.DatabaseTypeCollection.Sql.ToString())
            {
                para = new SqlParameter();
            }
            else if (Config.DatabaseType == Config.DatabaseTypeCollection.OleDb.ToString())
            {
                para = new OleDbParameter();
            }
            else if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                para = new MySqlParameter();
            }
            else
            {
                para = new OleDbParameter();
            }

            para.ParameterName = paraName;

            if (value != null)
                para.Value = value;

            para.Direction = ParameterDirection.Input;

            return para;
        }

    }


}
