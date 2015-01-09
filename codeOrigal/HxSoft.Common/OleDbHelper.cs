using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;

namespace HxSoft.Common
{
    /// <summary>
    /// Access���ݿ�ӿ�ʵ����
    /// </summary>
    public class OleDbHelper : IDbHelper
   {
       #region ���ݿ������ַ���
        private string _connstr;
        /// <summary>
        /// SQL���ݿ������ַ���
        /// </summary>
        public string ConnStr
        {
            get { return _connstr; }
            set { _connstr = value; }
        }
       #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public OleDbHelper()
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="OleDbConnStr"></param>
        public OleDbHelper(string OleDbConnStr)
        {
            _connstr = OleDbConnStr;
        }


       #region ִ�в�ѯ������DataSet
        /// <summary>
        /// ִ�в�ѯ������DataSet
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>    
        public DataSet GetDataSet(CommandType cmdType, string cmdText, DbParameter[] cmdParams)
       {
           using (OleDbConnection conn = new OleDbConnection(ConnStr))
           {
               using (OleDbCommand cmd = new OleDbCommand())
               {
                   PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParams);
                   using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
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

       #region ��������ִ�в�ѯ������DataSet
       /// <summary>
        /// ��������ִ�в�ѯ������DataSet
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public DataSet GetDataSet(DbTransaction trans, CommandType cmdType, string cmdText, params  DbParameter[] cmdParams)
       {
           OleDbCommand cmd = new OleDbCommand();
           PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParams);
           OleDbDataAdapter da = new OleDbDataAdapter(cmd);
           DataSet ds = new DataSet();
           da.Fill(ds, "ds");
           cmd.Parameters.Clear();
           return ds;
       }
       #endregion

       #region ִ�� Transact-SQL ��䲢������Ӱ���������
        /// <summary>
        /// ִ�� Transact-SQL ��䲢������Ӱ���������
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
       public int ExecuteSql(CommandType cmdType, string cmdText,DbParameter[] cmdParams)
       {
           using (OleDbConnection conn = new OleDbConnection(ConnStr))
           {
               OleDbCommand cmd = new OleDbCommand();
               PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParams);
               int val = cmd.ExecuteNonQuery();
               cmd.Parameters.Clear();
               conn.Close();
               return val;
           }
       }
       #endregion

       #region ��������ִ�� Transact-SQL ��䲢������Ӱ���������
        /// <summary>
        /// ��������ִ�� Transact-SQL ��䲢������Ӱ���������
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public int ExecuteSql(DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParams)
       {
           OleDbCommand cmd = new OleDbCommand();
           PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParams);
           int val = cmd.ExecuteNonQuery();
           cmd.Parameters.Clear();
           return val;
       }
       #endregion

       #region ִ�в�ѯ������DataReader
        /// <summary>
        /// ִ�в�ѯ������DataReader
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public DbDataReader GetDataReader(CommandType cmdType, string cmdText, DbParameter[] cmdParams)
       {
           OleDbCommand cmd = new OleDbCommand();
           OleDbConnection conn = new OleDbConnection(ConnStr);

           try
           {
               PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParams);
               DbDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

       #region ��������ִ�в�ѯ������DataReader
        /// <summary>
        /// ��������ִ�в�ѯ������DataReader
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public DbDataReader GetDataReader(DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParams)
       {
           OleDbCommand cmd = new OleDbCommand();
           PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParams);
           DbDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           cmd.Parameters.Clear();
           return dr;
       }
       #endregion

       #region ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С�
        /// <summary>
        /// ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С�
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
       public object GetScalar(CommandType cmdType, string cmdText,DbParameter[] cmdParams)
       {
           using (OleDbConnection conn = new OleDbConnection(ConnStr))
           {
               OleDbCommand cmd = new OleDbCommand();
               PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParams);
               object val = cmd.ExecuteScalar();
               cmd.Parameters.Clear();
               conn.Close();
               return val;
           }
       }
       #endregion

       #region ��������ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С�
       /// <summary>
       /// ��������ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С�
       /// </summary>
       /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
       public object GetScalar(DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParams)
       {
           OleDbCommand cmd = new OleDbCommand();
           PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParams);
           object val = cmd.ExecuteScalar();
           cmd.Parameters.Clear();
           return val;
       }
       #endregion

       #region ׼��Ҫִ�е�����
        /// <summary>
        /// ׼��Ҫִ�е�����
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParams)
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
                foreach (OleDbParameter parm in cmdParams)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }
       #endregion

    }
}