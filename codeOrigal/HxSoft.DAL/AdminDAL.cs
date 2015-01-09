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
    ///����Ա����-���ݷ�����
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>
    public class AdminDAL
    {

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Admin where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strAdminID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Admin where " + strFieldName + "=@" + strFieldName + " and AdminID<>@AdminID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@AdminID",strAdminID)};
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

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public AdminModel GetInfo(string strAdminID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Admin where AdminID=@AdminID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminID",strAdminID)};
            AdminModel admModel = new AdminModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    admModel.AdminName = dr["AdminName"].ToString();
                    admModel.AdminPass = dr["AdminPass"].ToString();
                    admModel.RealName = dr["RealName"].ToString();
                    admModel.Email = dr["Email"].ToString();
                    admModel.Department = dr["Department"].ToString();
                    admModel.Comment = dr["Comment"].ToString();
                    admModel.LoginNum = dr["LoginNum"].ToString();
                    admModel.LastLoginTime = dr["LastLoginTime"].ToString();
                    admModel.ThisLoginTime = dr["ThisLoginTime"].ToString();
                    admModel.ManageAdminID = dr["ManageAdminID"].ToString();
                    admModel.AddTime = dr["AddTime"].ToString();
                    admModel.IsClose = dr["IsClose"].ToString();
                    return admModel;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(AdminModel admModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Admin(AdminName,AdminPass,RealName,Email,Department,Comment,LoginNum,LastLoginTime,ThisLoginTime,ManageAdminID,AddTime,IsClose)");
            sql.Append(" values(@AdminName,@AdminPass,@RealName,@Email,@Department,@Comment,@LoginNum,@LastLoginTime,@ThisLoginTime,@ManageAdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminName",admModel.AdminName),
            Config.Conn().CreateDbParameter("@AdminPass",admModel.AdminPass),
            Config.Conn().CreateDbParameter("@RealName",admModel.RealName),
            Config.Conn().CreateDbParameter("@Email",admModel.Email),
            Config.Conn().CreateDbParameter("@Department",admModel.Department),
            Config.Conn().CreateDbParameter("@Comment",admModel.Comment),
            Config.Conn().CreateDbParameter("@LoginNum",admModel.LoginNum),
            Config.Conn().CreateDbParameter("@LastLoginTime",admModel.LastLoginTime),
            Config.Conn().CreateDbParameter("@ThisLoginTime",admModel.ThisLoginTime),
            Config.Conn().CreateDbParameter("@ManageAdminID",admModel.ManageAdminID),
            Config.Conn().CreateDbParameter("@AddTime",admModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",admModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(AdminModel admModel, string strAdminID)
        {
            StringBuilder sql = new StringBuilder("update t_Admin set ");
            sql.Append(" AdminName=@AdminName,");
            sql.Append(" AdminPass=@AdminPass,");
            sql.Append(" RealName=@RealName,");
            sql.Append(" Email=@Email,");
            sql.Append(" Department=@Department,");
            sql.Append(" Comment=@Comment,");
            //sql.Append(" LoginNum=@LoginNum,");
            //sql.Append(" LastLoginTime=@LastLoginTime,");
            //sql.Append(" ThisLoginTime=@ThisLoginTime,");
            //sql.Append(" ManageAdminID=@ManageAdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where AdminID=@AdminID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminName",admModel.AdminName),
            Config.Conn().CreateDbParameter("@AdminPass",admModel.AdminPass),
            Config.Conn().CreateDbParameter("@RealName",admModel.RealName),
            Config.Conn().CreateDbParameter("@Email",admModel.Email),
            Config.Conn().CreateDbParameter("@Department",admModel.Department),
            Config.Conn().CreateDbParameter("@Comment",admModel.Comment),
            //Config.Conn().CreateDbParameter("@LoginNum",admModel.LoginNum),
            //Config.Conn().CreateDbParameter("@LastLoginTime",admModel.LastLoginTime),
            //Config.Conn().CreateDbParameter("@ThisLoginTime",admModel.ThisLoginTime),
            //Config.Conn().CreateDbParameter("@ManageAdminID",admModel.ManageAdminID),
            //Config.Conn().CreateDbParameter("@AddTime",admModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",admModel.IsClose),
            Config.Conn().CreateDbParameter("@AdminID",strAdminID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strAdminID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Admin where AdminID=@AdminID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminID",strAdminID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ��¼
        /// <summary>
        /// ��¼
        /// </summary>
        public bool Login(string strAdminName, string strAdminPass)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Admin where AdminName=@AdminName and AdminPass=@AdminPass and IsClose=0");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminName",strAdminName),
            Config.Conn().CreateDbParameter("@AdminPass",strAdminPass)};
            AdminModel admModel = new AdminModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    Config.Conn().ExecuteSql(CommandType.Text, "update t_Admin set LoginNum=LoginNum+1,LastLoginTime=ThisLoginTime,ThisLoginTime='"+DateTime.Now.ToString()+"' where AdminID=" + dr["AdminID"].ToString(), null);
                    HttpCookie UserCookie = new HttpCookie("AdminLoginInfo");
                    UserCookie["AdminID"] = Convert.ToString(Convert.ToInt32(dr["AdminID"].ToString()) + DateTime.Now.Year);
                    UserCookie["AdminName"] = Config.md5(dr["AdminName"].ToString());
                    UserCookie["AdminPass"] = Config.md5(dr["AdminPass"].ToString());
                    UserCookie.Expires = DateTime.Now.AddDays(1d);
                    HttpContext.Current.Response.Cookies.Add(UserCookie);
                    HttpContext.Current.Session["AdminID"] = dr["AdminID"].ToString();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region ��½���
        /// <summary>
        /// ��½���
        /// </summary>
        public bool IsLogin()
        {
            if (HttpContext.Current.Request.Cookies["AdminLoginInfo"] == null)
            {
                return false;
            }
            else
            {
                string strAdminID = Convert.ToString(Convert.ToInt32(HttpContext.Current.Request.Cookies["AdminLoginInfo"].Values["AdminID"]) - DateTime.Now.Year);
                string strAdminName = HttpContext.Current.Request.Cookies["AdminLoginInfo"].Values["AdminName"];
                string strAdminPass = HttpContext.Current.Request.Cookies["AdminLoginInfo"].Values["AdminPass"];
                string sql = "select * from t_Admin where  AdminID=@AdminID and IsClose=0";
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@AdminID",strAdminID)};
                AdminModel admModel = new AdminModel();
                using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
                {
                    if (dr.Read())
                    {
                        if (Config.md5(dr["AdminName"].ToString()) != strAdminName || Config.md5(dr["AdminPass"].ToString()) != strAdminPass)
                        {
                            return false;
                        }
                        else
                        {

                            HttpContext.Current.Session["AdminID"] = dr["AdminID"].ToString();
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strAdminID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Admin set IsClose=@IsClose where AdminID=@AdminID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@AdminID",strAdminID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region �޸�����
        /// <summary>
        /// �޸�����
        /// </summary>
        public void ResetPassword(string strAdminID, string strAdminPass)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Admin set AdminPass=@AdminPass where AdminID=@AdminID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminPass",strAdminPass),
            Config.Conn().CreateDbParameter("@AdminID",strAdminID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strAdminID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Admin where AdminID=@AdminID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@AdminID",strAdminID)};
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
