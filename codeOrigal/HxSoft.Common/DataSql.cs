using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace HxSoft.Common
{
    /// <summary>
    /// SQL数据库操作类
    /// </summary>
    public class DataSql
   {
       #region 数据库连接字符串
        private string _connstr;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnStr
        {
            get { return _connstr; }
            set { _connstr = value; }
        }
       #endregion

       #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataSql()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="SqlConnStr"></param>
        public DataSql(string SqlConnStr)
        {
            _connstr = SqlConnStr;
        }

        #endregion

       #region 执行查询，返回DataSet
        /// <summary>
        /// 执行查询，返回DataSet
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>    
        public DataSet GetDataSet(CommandType cmdType, string cmdText, SqlParameter[] cmdParams)
       {
           using (SqlConnection conn = new SqlConnection(ConnStr))
           {
               using (SqlCommand cmd = new SqlCommand())
               {
                   PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParams);
                   using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                   {
                       DataSet ds = new DataSet();
                       da.Fill(ds, "ds");
                       cmd.Parameters.Clear();
                       conn.Close();
                       return ds;
                   }
               }
           }
       }
       #endregion

       #region 在事务中执行查询，返回DataSet
       /// <summary>
        /// 在事务中执行查询，返回DataSet
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public DataSet GetDataSet(SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParams)
       {
           SqlCommand cmd = new SqlCommand();
           PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParams);
           SqlDataAdapter da = new SqlDataAdapter(cmd);
           DataSet ds = new DataSet();
           da.Fill(ds, "ds");
           cmd.Parameters.Clear();
           return ds;
       }
       #endregion

       #region 执行 Transact-SQL 语句并返回受影响的行数。
        /// <summary>
        /// 执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
       public int ExecuteSql(CommandType cmdType, string cmdText,SqlParameter[] cmdParams)
       {
           using (SqlConnection conn = new SqlConnection(ConnStr))
           {
               SqlCommand cmd = new SqlCommand();
               PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParams);
               int val = cmd.ExecuteNonQuery();
               cmd.Parameters.Clear();
               conn.Close();
               return val;
           }
       }
       #endregion

       #region 在事务中执行 Transact-SQL 语句并返回受影响的行数。
       /// <summary>
        /// 在事务中执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public int ExecuteSql(SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParams)
       {
           SqlCommand cmd = new SqlCommand();
           PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParams);
           int val = cmd.ExecuteNonQuery();
           cmd.Parameters.Clear();
           return val;
       }
       #endregion

       #region 执行查询，返回DataReader
        /// <summary>
        /// 执行查询，返回DataReader
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public SqlDataReader GetDataReader(CommandType cmdType, string cmdText, SqlParameter[] cmdParams)
       {
           SqlCommand cmd = new SqlCommand();
           SqlConnection conn = new SqlConnection(ConnStr);

           try
           {
               PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParams);
               SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
               cmd.Parameters.Clear();
               return dr;
           }
           catch (Exception e)
           {
               conn.Close();
               throw e;
           }
           
       }
       #endregion

       #region 在事务中执行查询，返回DataReader
        /// <summary>
        /// 在事务中执行查询，返回DataReader
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public SqlDataReader GetDataReader(SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParams)
       {
           SqlCommand cmd = new SqlCommand();
           PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParams);
           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           cmd.Parameters.Clear();
           return dr;
       }
       #endregion

       #region 执行查询，并返回查询所返回的结果集中第一行的第一列。
        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
       public object GetScalar(CommandType cmdType, string cmdText,SqlParameter[] cmdParams)
       {
           using (SqlConnection conn = new SqlConnection(ConnStr))
           {
               SqlCommand cmd = new SqlCommand();
               PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParams);
               object val = cmd.ExecuteScalar();
               cmd.Parameters.Clear();
               conn.Close();
               return val;
           }
       }
       #endregion

       #region 在事务中执行查询，并返回查询所返回的结果集中第一行的第一列。
       /// <summary>
       /// 在事务中执行查询，并返回查询所返回的结果集中第一行的第一列。
       /// </summary>
       /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public object GetScalar(SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParams)
       {
           SqlCommand cmd = new SqlCommand();
           PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParams);
           object val = cmd.ExecuteScalar();
           cmd.Parameters.Clear();
           return val;
       }
       #endregion

       #region 准备要执行的命令
        /// <summary>
        /// 准备要执行的命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParams)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
            {
                cmd.Transaction = trans;
            }

            cmd.CommandType = cmdType;

            if (cmdParams != null)
            {
                foreach (SqlParameter parm in cmdParams)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }
       #endregion

    }
}
