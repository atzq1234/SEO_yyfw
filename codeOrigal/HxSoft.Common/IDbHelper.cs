using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace HxSoft.Common
{
    /// <summary>
    /// 数据库接口类
    /// </summary>
    interface IDbHelper
    {
        /// <summary>
        /// 执行查询，返回DataSet
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        DataSet GetDataSet(CommandType cmdType, string cmdText, DbParameter[] cmdParams);

        /// <summary>
        /// 在事务中执行查询，返回DataSet
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        DataSet GetDataSet(DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParams);

        /// <summary>
        /// 执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        int ExecuteSql(CommandType cmdType, string cmdText, DbParameter[] cmdParams);

        /// <summary>
        /// 在事务中执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        int ExecuteSql(DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParams);

        /// <summary>
        /// 执行查询，返回DataReader
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        DbDataReader GetDataReader(CommandType cmdType, string cmdText, DbParameter[] cmdParams);

        /// <summary>
        /// 在事务中执行查询，返回DataReader
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        DbDataReader GetDataReader(DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParams);

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        object GetScalar(CommandType cmdType, string cmdText, DbParameter[] cmdParams);

        /// <summary>
        /// 在事务中执行查询，并返回查询所返回的结果集中第一行的第一列。
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        object GetScalar(DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParams);
    }
}
