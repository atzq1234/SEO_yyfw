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
using System.Data.SqlClient;

namespace HxSoft.DAL
{
    /// <summary>
    /// Acc类,返回DataTable,记录总数,数据绑定
    /// </summary>
    public class AccDAL
    {
        #region 返回DataTable
        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string strSql, DbParameter[] cmdParams)
        {
            DataSet ds = Config.Conn().GetDataSet(CommandType.Text, strSql, cmdParams);
            return ds.Tables[0];
        }

        /// <summary>
        /// 分页存储,返回DataTable
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="FieldKey"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <param name="FieldShow"></param>
        /// <param name="FieldOrder"></param>
        /// <param name="Where"></param>
        /// <param name="AllCount"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string TableName, string FieldKey, int CurrentPage, int PageSize, string FieldShow, string FieldOrder, string Where, ref int AllCount)
        {
            SqlParameter[] cmdParams = {
            new SqlParameter("@TableName",TableName),
            new SqlParameter("@FieldKey",FieldKey),
            new SqlParameter("@CurrentPage",CurrentPage),
            new SqlParameter("@PageSize",PageSize),
            new SqlParameter("@FieldShow",FieldShow),
            new SqlParameter("@FieldOrder",FieldOrder),
            new SqlParameter("@Where",Where),
            new SqlParameter("@AllCount",SqlDbType.Int)};
            cmdParams[7].Direction = ParameterDirection.Output;
            DataSql dat = new DataSql(Config.SqlConnStr);
            DataSet ds = dat.GetDataSet(CommandType.StoredProcedure, "sp_PageView", cmdParams);
            try
            {
                AllCount = Convert.ToInt32(cmdParams[7].Value);
            }
            catch
            {
                AllCount = 0;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// SQL分页,返回DataTable
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="FieldKey"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <param name="FieldShow"></param>
        /// <param name="FieldOrder"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string TableName, string FieldKey, int CurrentPage, int PageSize, string FieldShow, string FieldOrder, string Where, ref int AllCount, DbParameter[] cmdParams)
        {
            string strCountSql = "select count(0) from " + TableName + " where " + Where + "";
            //AllCount = GetAllCount(strCountSql, cmdParams);

            int intStartRow = (CurrentPage - 1) * PageSize + 1;
            int intEndRow = CurrentPage * PageSize;
            string strTableSql = "(select " + FieldShow + ",row_number() over(order by " + FieldOrder + ") as row from " + TableName + " where " + Where + ") as temp";
            string strPageSql = "select * from " + strTableSql + " where row between " + intStartRow + " and " + intEndRow;

            DataSet ds = Config.Conn().GetDataSet(CommandType.Text, strCountSql + ";" + strPageSql, cmdParams);
            AllCount = (int)ds.Tables[0].Rows[0][0];
            return ds.Tables[1];
        }

        #endregion

        #region 返回记录总数
        /// <summary>
        /// 返回记录总数
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public int GetAllCount(string strSql, DbParameter[] cmdParams)
        {
            object obj = Config.Conn().GetScalar(CommandType.Text, strSql, cmdParams);
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return 0;
            }
        }
        #endregion

    }
}
