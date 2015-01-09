using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace HxSoft.Common
{
    /// <summary>
    /// SQL���ݿ������
    /// </summary>
    public class DataMySql
    {
        #region ���ݿ������ַ���
        private string _connstr;
        /// <summary>
        /// ���ݿ������ַ���
        /// </summary>
        public string ConnStr
        {
            get { return _connstr; }
            set { _connstr = value; }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public DataMySql()
        {
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="SqlConnStr"></param>
        public DataMySql(string MySqlConnStr)
        {
            _connstr = MySqlConnStr;
        }

        #endregion

        #region ִ�в�ѯ������DataSet
        /// <summary>
        /// ִ�в�ѯ������DataSet
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>    
        public DataSet GetDataSet(CommandType cmdType, string cmdText, MySqlParameter[] cmdParams)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParams);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
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
        public DataSet GetDataSet(MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParams)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParams);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
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
        public int ExecuteSql(CommandType cmdType, string cmdText, MySqlParameter[] cmdParams)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                MySqlCommand cmd = new MySqlCommand();
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
        public int ExecuteSql(MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParams)
        {
            MySqlCommand cmd = new MySqlCommand();
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
        public MySqlDataReader GetDataReader(CommandType cmdType, string cmdText, MySqlParameter[] cmdParams)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(ConnStr);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParams);
                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
        public MySqlDataReader GetDataReader(MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParams)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParams);
            MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
        public object GetScalar(CommandType cmdType, string cmdText, MySqlParameter[] cmdParams)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                MySqlCommand cmd = new MySqlCommand();
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
        public object GetScalar(MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParams)
        {
            MySqlCommand cmd = new MySqlCommand();
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
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParams)
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
                foreach (MySqlParameter parm in cmdParams)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }
        #endregion

    }
}
