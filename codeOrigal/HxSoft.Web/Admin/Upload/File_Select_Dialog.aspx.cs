using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.Admin.Upload
{
    public partial class File_Select_Dialog : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        public string strFolderPath
        {
            get
            {
                return Config.FolderNameReplace(Config.Request(Request["FolderPath"], Config.FileUploadPath));
            }
        }
        public string strObjName
        {
            get
            {
                return Config.Request(Request["ObjName"], "");
            }
        }
        public string strFileNameKey
        {
            get
            {
                return Config.Request(Request["txtFileNameKey"], "");
            }
        }
        public string IsSearch
        {
            get
            {
                return Config.Request(Request["hidIsSearch"], "0");
            }
        }

        //ҳ���ʼ��
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            lbtnCreateFolder.Visible = GetData.LimitChk("FolderCreate");
            lbtnDelFolder.Visible = GetData.LimitChk("FolderDel");
            lbtnUploadFile.Visible = GetData.LimitChk("FileUpload");
            lbtnDelFile.Visible = GetData.LimitChk("FileDel");
            if (!Page.IsPostBack)
            {
                Config.CheckFolder(Server.MapPath(Config.FileUploadPath));
                Config.CheckFolder(Server.MapPath(strFolderPath));

                FolderPath_Bind();
                Repeater_Bind();

                drpFolderPath.Attributes.Add("onchange", "javascript:SelectFolder('" + strObjName + "')");
            }
        }
        //������
        protected void Repeater_Bind()
        {
            if (Directory.Exists(Server.MapPath(strFolderPath)))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("CheckBox", typeof(string)));
                dt.Columns.Add(new DataColumn("Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Link", typeof(string)));
                dt.Columns.Add(new DataColumn("CountOrSize", typeof(string)));
                dt.Columns.Add(new DataColumn("Author", typeof(string)));
                dt.Columns.Add(new DataColumn("AddTime", typeof(string)));

                DirectoryInfo dir = new DirectoryInfo(Server.MapPath(strFolderPath));
                DirectoryInfo[] arrDir = dir.GetDirectories();
                if (IsSearch == "0")
                {
                    for (int i = 0; i < arrDir.Length; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["CheckBox"] = "<input name=\"FolderName\" type=\"checkbox\" id=\"FolderName\" value=\"" + arrDir[i].Name + "\" " + GetData.ShowPathDisabledStatus(strFolderPath + arrDir[i].Name + "/", "FolderAll") + " />";
                        dr["Type"] = "folder";
                        dr["Name"] = arrDir[i].Name;
                        dr["Link"] = "?ObjName=" + strObjName + "&FolderPath=" + strFolderPath + arrDir[i].Name + "/";
                        dr["CountOrSize"] = Config.FolderAndFileTotal(Server.MapPath(strFolderPath + arrDir[i].Name + "/")).ToString();
                        dr["Author"] = GetData.GetPathAdmin(strFolderPath + arrDir[i].Name + "/");
                        dr["AddTime"] = arrDir[i].CreationTime;
                        dt.Rows.Add(dr);
                    }
                }

                FileInfo[] arrFile = dir.GetFiles();
                if (IsSearch == "0")
                {
                    for (int i = 0; i < arrFile.Length; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["CheckBox"] = "<input name=\"FileName\" type=\"checkbox\" id=\"FileName\" value=\"" + arrFile[i].Name + "\" " + GetData.ShowPathDisabledStatus(strFolderPath + arrFile[i].Name, "FileAll") + " />";
                        dr["Type"] = arrFile[i].Extension.Replace(".", "");
                        dr["Name"] = arrFile[i].Name;
                        dr["Link"] = "javascript:SelectFile('" + strObjName + "','" + strFolderPath + arrFile[i].Name + "');HiddenDialog();";
                        dr["CountOrSize"] = Math.Round(Convert.ToDouble(arrFile[i].Length) / 1024, 2).ToString("N") + "KB";
                        dr["Author"] = GetData.GetPathAdmin(strFolderPath + arrFile[i].Name);
                        dr["AddTime"] = arrFile[i].CreationTime;
                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    for (int i = 0; i < arrFile.Length; i++)
                    {
                        if (arrFile[i].Name.ToLower().IndexOf(strFileNameKey.ToLower()) > -1)
                        {
                            DataRow dr = dt.NewRow();
                            dr["CheckBox"] = "<input name=\"FileName\" type=\"checkbox\" id=\"FileName\" value=\"" + arrFile[i].Name + "\" " + GetData.ShowPathDisabledStatus(strFolderPath + arrFile[i].Name, "FileAll") + " />";
                            dr["Type"] = arrFile[i].Extension.Replace(".", "");
                            dr["Name"] = arrFile[i].Name;
                            dr["Link"] = "javascript:SelectFile('" + strObjName + "','" + strFolderPath + arrFile[i].Name + "');HiddenDialog();";
                            dr["CountOrSize"] = Math.Round(Convert.ToDouble(arrFile[i].Length) / 1024, 2).ToString("N") + "KB";
                            dr["Author"] = GetData.GetPathAdmin(strFolderPath + arrFile[i].Name);
                            dr["AddTime"] = arrFile[i].CreationTime;
                            dt.Rows.Add(dr);
                        }
                    }
                }
                pager.InnerHtml = BindHelper.DataPageBind(dt, Config.DataBindObjTypeCollection.Repeater.ToString(), repList, 12, page, "?ObjName=" + strObjName + "&FolderPath=" + strFolderPath + "&txtFileNameKey=" + strFileNameKey + "&hidIsSearch=" + IsSearch + "&").ToString();
            }
        }

        //��·�������б�
        protected void FolderPath_Bind()
        {
            string[] arrCurrentFolderPath = strFolderPath.Split(new char[] { '/' });
            //StringBuilder strFullPath = new StringBuilder();//��0��ʼ����ʾ����վ����ļ�Ŀ¼
            //for (int i = 0; i < arrCurrentFolderPath.Length - 1; i++)
            StringBuilder strFullPath = new StringBuilder("/");//��1��ʼʱ��Ҫʼ��strFullPathΪ"/"
            for (int i = 1; i < arrCurrentFolderPath.Length - 1; i++)
            {
                strFullPath.Append(arrCurrentFolderPath[i] + "/");
                drpFolderPath.Items.Add(new ListItem(strFullPath.ToString(), strFullPath.ToString()));
            }
            Config.setDefaultSelected(drpFolderPath, strFolderPath);
        }

        //��ѯ
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Repeater_Bind();
        }

        //ɾ���ļ���
        protected void lbtnDelFolder_Click(object sender, EventArgs e)
        {
            string strFolderName = Config.Request(Request.Form["FolderName"], string.Empty);
            if (strFolderName != string.Empty)
            {
                string[] arrFolderName = strFolderName.Split(new char[] { ',' });
                for (int i = 0; i < arrFolderName.Length; i++)
                {
                    string strTempFolderPath = strFolderPath + arrFolderName[i] + "/";
                    PathModel pathModel = new PathModel();
                    pathModel = Factory.Path().GetInfo(strTempFolderPath.ToLower());
                    if (pathModel != null)
                    {
                        if (GetData.CheckAdminID(pathModel.AdminID, "FolderAll"))//��鴴����
                        {
                            if (Directory.Exists(Server.MapPath(strTempFolderPath)))
                            {
                                DirectoryInfo dir = new DirectoryInfo(Server.MapPath(strTempFolderPath));
                                FileSystemInfo[] arrDir = dir.GetFileSystemInfos();
                                if (arrDir.Length == 0)
                                {
                                    dir.Delete(false);
                                    Factory.Path().DeleteInfo(strTempFolderPath.ToLower());
                                    Factory.AdminLog().InsertLog("ɾ���ļ���\"" + strTempFolderPath + "\"��", Session["AdminID"].ToString());
                                }
                            }
                        }
                    }
                }
            }
            Response.Redirect("File_Select_Dialog.aspx?FolderPath=" + strFolderPath + "&ObjName=" + strObjName);
        }

        //ɾ���ļ�
        protected void lbtnDelFile_Click(object sender, EventArgs e)
        {
            string strFileName = Config.Request(Request.Form["FileName"], string.Empty);
            if (strFileName != string.Empty)
            {
                string[] arrFileName = strFileName.Split(new char[] { ',' });
                for (int i = 0; i < arrFileName.Length; i++)
                {
                    string strTempFilePath = strFolderPath + arrFileName[i];
                    PathModel pathModel = new PathModel();
                    pathModel = Factory.Path().GetInfo(strTempFilePath.ToLower());
                    if (pathModel != null)
                    {
                        if (GetData.CheckAdminID(pathModel.AdminID, "FileAll"))//��鴴����
                        {
                            if (File.Exists(Server.MapPath(strTempFilePath)))
                            {
                                File.Delete(Server.MapPath(strTempFilePath));
                                Factory.Path().DeleteInfo(strTempFilePath.ToLower());
                                Factory.AdminLog().InsertLog("ɾ���ļ�\"" + strTempFilePath + "\"��", Session["AdminID"].ToString());
                            }
                        }
                    }
                }
            }
            Response.Redirect("File_Select_Dialog.aspx?FolderPath=" + strFolderPath + "&ObjName=" + strObjName);
        }

        //Ԥ��ͼƬ
        public string ShowPreview(string strFileName)
        {
            if (Config.IsPicture(strFileName))
            {
                return "<a href=\"#\" onclick=\"javascript:dialog('ͼƬԤ��','iframe:File_Preview.aspx?FilePath=" + strFolderPath + strFileName + "','500px','400px','');return false;\">Ԥ��</a>";
            }
            else
            {
                return "";
            }
        }

    }
}
