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
    ///����Ա���������-���ݷ�����
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>
    public class AdminInGroupDAL
    {

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strAdminID,string strAdminGroupID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_AdminInGroup where AdminID=@AdminID and AdminGroupID=@AdminGroupID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminID",strAdminID),
            Config.Conn().CreateDbParameter("@AdminGroupID",strAdminGroupID)};
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

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(AdminInGroupModel admInGrModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_AdminInGroup(AdminID,AdminGroupID)");
            sql.Append(" values(@AdminID,@AdminGroupID)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminID",admInGrModel.AdminID),
            Config.Conn().CreateDbParameter("@AdminGroupID",admInGrModel.AdminGroupID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strAdminID, string strAdminGroupID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_AdminInGroup where AdminID=@AdminID and AdminGroupID=@AdminGroupID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminID",strAdminID),
            Config.Conn().CreateDbParameter("@AdminGroupID",strAdminGroupID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }

        /// <summary>
        /// ����AdminIDɾ����Ϣ
        /// </summary>
        public void DeleteInfoByAdminID(string strAdminID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_AdminInGroup where AdminID=@AdminID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminID",strAdminID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }

        /// <summary>
        /// ����AdminGroupIDɾ����Ϣ
        /// </summary>
        public void DeleteInfoByAdminGroupID(string strAdminGroupID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_AdminInGroup where AdminGroupID=@AdminGroupID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminGroupID",strAdminGroupID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ����������еĹ���ԱȨ�޻���
        /// <summary>
        /// ����������еĹ���ԱȨ�޻���
        /// </summary>
        /// <param name="strAdminGroupID"></param>
        /// <returns></returns>
        public void RemoveLimitCache(string strAdminGroupID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select AdminID from t_AdminInGroup where AdminGroupID=@AdminGroupID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminGroupID",strAdminGroupID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                while (dr.Read())
                {
                    string key = "Cache_AdminGroup_LimitValues_" + dr[0].ToString();
                    CacheHelper.RemoveCache(key);
                }
            }
        }
        #endregion


    }
}
