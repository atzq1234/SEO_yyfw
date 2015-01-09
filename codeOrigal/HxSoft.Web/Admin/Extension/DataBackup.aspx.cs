using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data.OleDb;
using HxSoft.Common;
using System.IO;
using System.Diagnostics;

namespace HxSoft.Web.Admin.Extension
{
    public partial class DataBackup : System.Web.UI.Page
    {
        DataSql dat = new DataSql(Config.SqlConnStr);

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void BackUp_Click(object sender, EventArgs e)
        {
            if (Config.DatabaseType == "OleDb")
            {
                BackupAccess();
            }
            else if (Config.DatabaseType == "Sql")
            {
                BackupMssql();
            }
            else if (Config.DatabaseType == "MySql")
            {
                BackupMysql();
            }
        }

        //备份access数据库
        private void BackupAccess()
        {
            try
            {
                string OldPath = Server.MapPath(Config.AccessStr);
                string NewPath = Server.MapPath("/App_Data/back_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".mdb");
                File.Copy(OldPath, NewPath, true);
                Config.ShowEnd("备份成功!");
            }
            catch (Exception ex)
            {
                Config.ShowEnd(ex.Message);
            }

        }

        //备份mssql数据库
        private void BackupMssql()
        {
            string sql_name = "Select Name From Master..SysDataBases Where DbId=(Select Dbid From Master..SysProcesses Where Spid = @@spid)";
            using (SqlDataReader dr = dat.GetDataReader(CommandType.Text, sql_name, null))
            {
                if (dr.Read())
                {
                    string strDatabaseName = dr[0].ToString();
                    string strBackupPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "App_Data\\back_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";
                    string strBackupSql = "BACKUP DATABASE " + strDatabaseName + " TO DISK ='" + strBackupPath + "'   WITH INIT, NOUNLOAD, NAME=N'" + strDatabaseName + " Full backup', SKIP, STATS = 10, NOFORMAT";
                    try
                    {
                        dat.ExecuteSql(CommandType.Text, strBackupSql, null);
                        Config.ShowEnd("备份成功!");
                    }
                    catch (Exception ex)
                    {
                        Config.ShowEnd(ex.Message);
                    }
                }
            }
        }

        //备份mysql数据库
        private void BackupMysql()
        {
            string strDumpPath = Server.MapPath("/App_Data/mysqldump.exe");
            string strBackPath = Server.MapPath("/App_Data/back_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString() + ".sql");
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            string strOutput = null;
            try
            {
                p.Start();
                p.StandardInput.WriteLine("@echo 开始对" + Config.MySql_Server + "服务器上" + Config.MySql_Database + "执行备份操作");
                p.StandardInput.WriteLine("@" + strDumpPath + " -h" + Config.MySql_Server + " -P" + Config.MySql_Port + " -u" + Config.MySql_Uid + " -p" + Config.MySql_Pwd + " --opt " + Config.MySql_Database + " > " + strBackPath);
                p.StandardInput.WriteLine("@echo 对" + Config.MySql_Server + "服务器MYSQL数据库文件" + Config.MySql_Database + "备份成功");
                p.StandardInput.WriteLine("@echo 备份文件所在路径 " + strBackPath);
                p.StandardInput.WriteLine("exit");
                strOutput = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Close();
            }
            catch (Exception e)
            {
                strOutput = e.Message;
            }
            errMsg.Visible = true;
            errMsg.Text = strOutput;
        }

    }
}
