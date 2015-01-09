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
using System.IO;

namespace HxSoft.DAL
{
    /// <summary>
    ///会员管理-数据访问类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    public class UserDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_User where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strUserID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_User where " + strFieldName + "=@" + strFieldName + " and UserID<>@UserID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@UserID",strUserID)};
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

        #region 读取信息(根据用户ID)
        /// <summary>
        /// 读取信息(根据用户ID)
        /// </summary>
        public UserModel GetInfo(string strUserID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_User where UserID=@UserID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@UserID",strUserID)};
            UserModel userModel = new UserModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    userModel.UserName = dr["UserName"].ToString();
                    userModel.UserPass = dr["UserPass"].ToString();
                    userModel.PassQuestion = dr["PassQuestion"].ToString();
                    userModel.PassAnswer = dr["PassAnswer"].ToString();
                    userModel.RealName = dr["RealName"].ToString();
                    userModel.Sex = dr["Sex"].ToString();
                    userModel.Email = dr["Email"].ToString();
                    userModel.Mobile = dr["Mobile"].ToString();
                    userModel.Address = dr["Address"].ToString();
                    userModel.Company = dr["Company"].ToString();
                    userModel.Comment = dr["Comment"].ToString();
                    userModel.UserRankID = dr["UserRankID"].ToString();
                    userModel.IsAudit = dr["IsAudit"].ToString();
                    userModel.Point = dr["Point"].ToString();
                    userModel.LoginNum = dr["LoginNum"].ToString();
                    userModel.LastLoginTime = dr["LastLoginTime"].ToString();
                    userModel.ThisLoginTime = dr["ThisLoginTime"].ToString();
                    userModel.AddTime = dr["AddTime"].ToString();
                    userModel.IsClose = dr["IsClose"].ToString();
                    return userModel;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region 读取信息(根据用户名)
        /// <summary>
        /// 通过用户名查找信息
        /// </summary>
        public UserModel GetInfoByUserName(string strUserName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_User where UserName=@UserName");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@UserName",strUserName)};
            UserModel userModel = new UserModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    userModel.UserID = dr["UserID"].ToString();
                    userModel.UserName = dr["UserName"].ToString();
                    userModel.UserPass = dr["UserPass"].ToString();
                    userModel.PassQuestion = dr["PassQuestion"].ToString();
                    userModel.PassAnswer = dr["PassAnswer"].ToString();
                    userModel.RealName = dr["RealName"].ToString();
                    userModel.Sex = dr["Sex"].ToString();
                    userModel.Email = dr["Email"].ToString();
                    userModel.Mobile = dr["Mobile"].ToString();
                    userModel.Address = dr["Address"].ToString();
                    userModel.Company = dr["Company"].ToString();
                    userModel.Comment = dr["Comment"].ToString();
                    userModel.UserRankID = dr["UserRankID"].ToString();
                    userModel.IsAudit = dr["IsAudit"].ToString();
                    userModel.Point = dr["Point"].ToString();
                    userModel.LoginNum = dr["LoginNum"].ToString();
                    userModel.LastLoginTime = dr["LastLoginTime"].ToString();
                    userModel.ThisLoginTime = dr["ThisLoginTime"].ToString();
                    userModel.AddTime = dr["AddTime"].ToString();
                    userModel.IsClose = dr["IsClose"].ToString();
                    return userModel;
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
        public void InsertInfo(UserModel userModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_User(UserName,UserPass,PassQuestion,PassAnswer,RealName,Sex,Email,Mobile,Address,Company,Comment,UserRankID,IsAudit,Point,LoginNum,LastLoginTime,ThisLoginTime,AddTime,IsClose)");
            sql.Append(" values(@UserName,@UserPass,@PassQuestion,@PassAnswer,@RealName,@Sex,@Email,@Mobile,@Address,@Company,@Comment,@UserRankID,@IsAudit,@Point,@LoginNum,@LastLoginTime,@ThisLoginTime,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@UserName",userModel.UserName),
            Config.Conn().CreateDbParameter("@UserPass",userModel.UserPass),
            Config.Conn().CreateDbParameter("@PassQuestion",userModel.PassQuestion),
            Config.Conn().CreateDbParameter("@PassAnswer",userModel.PassAnswer),
            Config.Conn().CreateDbParameter("@RealName",userModel.RealName),
            Config.Conn().CreateDbParameter("@Sex",userModel.Sex),
            Config.Conn().CreateDbParameter("@Email",userModel.Email),
            Config.Conn().CreateDbParameter("@Mobile",userModel.Mobile),
            Config.Conn().CreateDbParameter("@Address",userModel.Address),
            Config.Conn().CreateDbParameter("@Company",userModel.Company),
            Config.Conn().CreateDbParameter("@Comment",userModel.Comment),
            Config.Conn().CreateDbParameter("@UserRankID",userModel.UserRankID),
            Config.Conn().CreateDbParameter("@IsAudit",userModel.IsAudit),
            Config.Conn().CreateDbParameter("@Point",userModel.Point),
            Config.Conn().CreateDbParameter("@LoginNum",userModel.LoginNum),
            Config.Conn().CreateDbParameter("@LastLoginTime",userModel.LastLoginTime),
            Config.Conn().CreateDbParameter("@ThisLoginTime",userModel.ThisLoginTime),
            Config.Conn().CreateDbParameter("@AddTime",userModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",userModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(UserModel userModel, string strUserID)
        {
            StringBuilder sql = new StringBuilder("update t_User set ");
            sql.Append(" UserName=@UserName,");
            sql.Append(" UserPass=@UserPass,");
            sql.Append(" PassQuestion=@PassQuestion,");
            sql.Append(" PassAnswer=@PassAnswer,");
            sql.Append(" RealName=@RealName,");
            sql.Append(" Sex=@Sex,");
            sql.Append(" Email=@Email,");
            sql.Append(" Mobile=@Mobile,");
            sql.Append(" Address=@Address,");
            sql.Append(" Company=@Company,");
            sql.Append(" Comment=@Comment,");
            sql.Append(" UserRankID=@UserRankID,");
            sql.Append(" IsAudit=@IsAudit,");
            sql.Append(" Point=@Point,");
            //sql.Append(" LoginNum=@LoginNum,");
            //sql.Append(" LastLoginTime=@LastLoginTime,");
            //sql.Append(" ThisLoginTime=@ThisLoginTime,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where  UserID=@UserID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@UserName",userModel.UserName),
            Config.Conn().CreateDbParameter("@UserPass",userModel.UserPass),
            Config.Conn().CreateDbParameter("@PassQuestion",userModel.PassQuestion),
            Config.Conn().CreateDbParameter("@PassAnswer",userModel.PassAnswer),
            Config.Conn().CreateDbParameter("@RealName",userModel.RealName),
            Config.Conn().CreateDbParameter("@Sex",userModel.Sex),
            Config.Conn().CreateDbParameter("@Email",userModel.Email),
            Config.Conn().CreateDbParameter("@Mobile",userModel.Mobile),
            Config.Conn().CreateDbParameter("@Address",userModel.Address),
            Config.Conn().CreateDbParameter("@Company",userModel.Company),
            Config.Conn().CreateDbParameter("@Comment",userModel.Comment),
            Config.Conn().CreateDbParameter("@UserRankID",userModel.UserRankID),
            Config.Conn().CreateDbParameter("@IsAudit",userModel.IsAudit),
            Config.Conn().CreateDbParameter("@Point",userModel.Point),
            //Config.Conn().CreateDbParameter("@LoginNum",userModel.LoginNum),
            //Config.Conn().CreateDbParameter("@LastLoginTime",userModel.LastLoginTime),
            //Config.Conn().CreateDbParameter("@ThisLoginTime",userModel.ThisLoginTime),
            //Config.Conn().CreateDbParameter("@AddTime",userModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",userModel.IsClose),
            Config.Conn().CreateDbParameter("@UserID",strUserID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strUserID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_User where UserID=@UserID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@UserID",strUserID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strUserID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_User set IsClose=@IsClose where UserID=@UserID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@UserID",strUserID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新审核状态
        /// <summary>
        /// 更新审核状态
        /// </summary>
        public void UpdateAuditStatus(string strUserID, string strIsAudit)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_User set IsAudit=@IsAudit where UserID=@UserID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsAudit",strIsAudit),
            Config.Conn().CreateDbParameter("@UserID",strUserID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新会员等级
        /// <summary>
        /// 更新会员等级
        /// </summary>
        public void UpdateUserRank(string strUserID, string strUserRankID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_User set UserRankID=@UserRankID where UserID=@UserID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@UserRankID",strUserRankID),
            Config.Conn().CreateDbParameter("@UserID",strUserID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        public UserModel Login(string strUserName, string strUserPass)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_User where UserName=@UserName and UserPass=@UserPass");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@UserName",strUserName),
            Config.Conn().CreateDbParameter("@UserPass",strUserPass)};
            UserModel userModel = new UserModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    if (dr["IsClose"].ToString() != "1" && dr["IsAudit"].ToString() != "0")
                    {
                        Config.Conn().ExecuteSql(CommandType.Text, "update t_User set LoginNum=LoginNum+1,LastLoginTime=ThisLoginTime,ThisLoginTime='" + DateTime.Now.ToString() + "' where UserID=" + dr["UserID"].ToString(), null);
                        HttpCookie UserCookie = new HttpCookie("UserLoginInfo");
                        UserCookie["UserID"] = Convert.ToString(Convert.ToInt32(dr["UserID"].ToString()) + DateTime.Now.Year);
                        UserCookie["UserName"] = Config.md5(dr["UserName"].ToString());
                        UserCookie["UserPass"] = Config.md5(dr["UserPass"].ToString());
                        UserCookie.Expires = DateTime.Now.AddDays(1d);
                        HttpContext.Current.Response.Cookies.Add(UserCookie);
                        HttpContext.Current.Session["UserID"] = dr["UserID"].ToString();
                    }
                    userModel.UserName = dr["UserName"].ToString();
                    userModel.UserPass = dr["UserPass"].ToString();
                    userModel.PassQuestion = dr["PassQuestion"].ToString();
                    userModel.PassAnswer = dr["PassAnswer"].ToString();
                    userModel.RealName = dr["RealName"].ToString();
                    userModel.Sex = dr["Sex"].ToString();
                    userModel.Email = dr["Email"].ToString();
                    userModel.Mobile = dr["Mobile"].ToString();
                    userModel.Address = dr["Address"].ToString();
                    userModel.Company = dr["Company"].ToString();
                    userModel.Comment = dr["Comment"].ToString();
                    userModel.UserRankID = dr["UserRankID"].ToString();
                    userModel.IsAudit = dr["IsAudit"].ToString();
                    userModel.LoginNum = dr["LoginNum"].ToString();
                    userModel.LastLoginTime = dr["LastLoginTime"].ToString();
                    userModel.ThisLoginTime = dr["ThisLoginTime"].ToString();
                    userModel.AddTime = dr["AddTime"].ToString();
                    userModel.IsClose = dr["IsClose"].ToString();
                    return userModel;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region 是否登录
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin()
        {
            if (HttpContext.Current.Request.Cookies["UserLoginInfo"] == null)
            {
                return false;
            }
            else
            {
                string strUserID = Convert.ToString(Convert.ToInt32(HttpContext.Current.Request.Cookies["UserLoginInfo"].Values["UserID"]) - DateTime.Now.Year);
                string strUserName = HttpContext.Current.Request.Cookies["UserLoginInfo"].Values["UserName"];
                string strUserPass = HttpContext.Current.Request.Cookies["UserLoginInfo"].Values["UserPass"];
                string sql = "select * from t_User where  UserID=@UserID and IsClose=0 and IsAudit=1";
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@UserID",strUserID)};
                UserModel admModel = new UserModel();
                using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
                {
                    if (dr.Read())
                    {
                        if (Config.md5(dr["UserName"].ToString()) != strUserName || Config.md5(dr["UserPass"].ToString()) != strUserPass)
                        {
                            return false;
                        }
                        else
                        {

                            HttpContext.Current.Session["UserID"] = dr["UserID"].ToString();
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

        #region 会员更新资料
        /// <summary>
        /// 会员更新资料
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="strUserID"></param>
        public void UpdateInfoByUser(UserModel userModel, string strUserID)
        {
            StringBuilder sql = new StringBuilder("update t_User set ");
            sql.Append(" RealName=@RealName,");
            sql.Append(" Sex=@Sex,");
            sql.Append(" Email=@Email,");
            sql.Append(" Mobile=@Mobile,");
            sql.Append(" Address=@Address,");
            sql.Append(" Company=@Company,");
            sql.Append(" Comment=@Comment");
            sql.Append(" where UserID=@UserID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@RealName",userModel.RealName),
            Config.Conn().CreateDbParameter("@Sex",userModel.Sex),
            Config.Conn().CreateDbParameter("@Email",userModel.Email),
            Config.Conn().CreateDbParameter("@Mobile",userModel.Mobile),
            Config.Conn().CreateDbParameter("@Address",userModel.Address),
            Config.Conn().CreateDbParameter("@Company",userModel.Company),
            Config.Conn().CreateDbParameter("@Comment",userModel.Comment),
            Config.Conn().CreateDbParameter("@UserID",strUserID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 会员更新密码保护
        /// <summary>
        /// 会员更新密码保护
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="strUserID"></param>
        public void UpdatePassQuestion(UserModel userModel, string strUserID)
        {
            StringBuilder sql = new StringBuilder("update t_User set ");
            sql.Append(" PassQuestion=@PassQuestion,");
            sql.Append(" PassAnswer=@PassAnswer");
            sql.Append(" where UserID=@UserID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@PassQuestion",userModel.PassQuestion),
            Config.Conn().CreateDbParameter("@PassAnswer",userModel.PassAnswer),
            Config.Conn().CreateDbParameter("@UserID",strUserID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 会员修改密码
        /// <summary>
        /// 会员修改密码
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="strUserID"></param>
        public void SetPass(UserModel userModel, string strUserID)
        {
            StringBuilder sql = new StringBuilder("update t_User set ");
            sql.Append(" UserPass=@UserPass");
            sql.Append(" where UserID=@UserID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@UserPass",userModel.UserPass),
            Config.Conn().CreateDbParameter("@UserID",strUserID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strUserID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_User where UserID=@UserID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@UserID",strUserID)};
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

        #region 导出邮件地址
        /// <summary>
        /// 导出邮件地址
        /// </summary>
        public void EmailExport(string strSql, string strFilePath, string strFileName)
        {
            using (StreamWriter sw = File.CreateText(strFilePath))
            {
                using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, strSql, null))
                {
                    while (dr.Read())
                    {
                        sw.WriteLine(dr["Email"].ToString());
                    }
                }
            }
            Config.DirectDownFile(strFilePath, strFileName);
        }
        #endregion
    }
}
