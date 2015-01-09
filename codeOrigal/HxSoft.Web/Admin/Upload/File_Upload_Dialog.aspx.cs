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
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;
using Brettle.Web.NeatUpload;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace HxSoft.Web.Admin.Upload
{
    public partial class File_Upload_Dialog : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
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
        public string strDialogType
        {
            get
            {
                return Config.Request(Request["DialogType"], "UploadDialog");
            }
        }
        public string strW
        {
            get
            {
                return Config.Request(Request["W"], "550");
            }
        }
        public string strH
        {
            get
            {
                return Config.Request(Request["H"], "350");
            }
        }
        //ҳ���ʼ��
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("FileUpload");
            if (!Page.IsPostBack)
            {
                Config.CheckFolder(Server.MapPath(Config.FileUploadPath));
                Config.CheckFolder(Server.MapPath(strFolderPath));
                InputFile1.Attributes.Add("onchange", "javascript:FilePreview(" + strW + "," + strH + ");FileExitsCheck();");
                btnSave.Attributes.Add("onclick", "javascript:return FileFormCheck()");
                hidFolderPath.Value = strFolderPath;
            }
        }

        //�����ļ�
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (InputFile1.HasFile)
            {
                string strFileExt = Config.FileNameReplace(Path.GetExtension(InputFile1.FileName)).ToLower();
                string strFileName = Config.FileNameReplace(Path.GetFileNameWithoutExtension(InputFile1.FileName)).ToLower();
                string strRnd = "";
                if (radNameType.Items[1].Selected)
                {
                    Random rnd = new Random();
                    strRnd = "_" + rnd.Next(1000, 9999).ToString();
                }
                else
                {
                    string strTempFilePath = strFolderPath + strFileName + strFileExt;
                    if (File.Exists(Server.MapPath(strTempFilePath)))
                    {
                        PathModel pathModel = new PathModel();
                        pathModel = Factory.Path().GetInfo(strTempFilePath.ToLower());
                        if (pathModel != null)
                        {
                            if (!GetData.CheckAdminID(pathModel.AdminID, "FileAll"))//��鴴����
                            {
                                //�ļ������ڵ�ǰ�û�,����������ļ���
                                Random rnd = new Random();
                                strRnd = "_" + rnd.Next(1000, 9999).ToString();
                            }
                        }
                        else//�ļ�����,�������ݿ����Ҳ���,����������ļ���
                        {
                            Random rnd = new Random();
                            strRnd = "_" + rnd.Next(1000, 9999).ToString();
                        }
                    }
                }

                string strTempDir = Config.FileUploadPath + "temp/";//��ʱĿ¼
                Config.CheckFolder(Server.MapPath(strTempDir));
                string strTempFile = strTempDir + strFileName + strRnd + strFileExt;//��ʱ�ļ�   
                string strFilePath = strFolderPath + strFileName + strRnd + strFileExt;//ʵ�ʱ���·��
                if (strFileName.IndexOf(".asp") > -1 || strFileName.IndexOf(".asa") > -1 || strFileName.IndexOf(".aspx") > -1 || strFileName.IndexOf(".ashx") > -1 || strFileName.IndexOf(".jsp") > -1 || strFileName.IndexOf(".php") > -1)
                {
                    errMsg.Text = "�������ϴ���ִ���ļ�!";
                }
                else if (strFileExt.IndexOf(".asp") > -1 || strFileExt.IndexOf(".asa") > -1 || strFileExt.IndexOf(".aspx") > -1 || strFileExt.IndexOf(".ashx") > -1 || strFileExt.IndexOf(".jsp") > -1 || strFileExt.IndexOf(".php") > -1)
                {
                    errMsg.Text = "�������ϴ���ִ���ļ�!";
                }
                else
                {
                    if (!Config.IsPicture3(strFileExt))//����ͼƬ
                    {
                        InputFile1.MoveTo(Server.MapPath(strFilePath), MoveToOptions.Overwrite);
                    }
                    else
                    {
                        //��������ˮӡͼƬ��ʼ
                        SetModel seModel = new SetModel();
                        seModel = Factory.Set().GetInfo();
                        if (seModel != null)
                        {
                            if (seModel.WaterTypeID == "1")//����ˮӡ
                            {
                                InputFile1.MoveTo(Server.MapPath(strTempFile), MoveToOptions.Overwrite);

                                ZoomAuto(Server.MapPath(strTempFile), Server.MapPath(strFilePath), seModel.WaterText, "", seModel);
                                File.Delete(Server.MapPath(strTempFile));
                            }
                            else if (seModel.WaterTypeID == "2")//ͼƬˮӡ
                            {
                                InputFile1.MoveTo(Server.MapPath(strTempFile), MoveToOptions.Overwrite);

                                ZoomAuto(Server.MapPath(strTempFile), Server.MapPath(strFilePath), "", Server.MapPath(seModel.WaterPic), seModel);
                                File.Delete(Server.MapPath(strTempFile));
                            }
                            else
                            {
                                InputFile1.MoveTo(Server.MapPath(strFilePath), MoveToOptions.Overwrite);
                            }
                        }
                        else
                        {
                            InputFile1.MoveTo(Server.MapPath(strFilePath), MoveToOptions.Overwrite);
                        }
                    }
                    //���ϴ����ļ�·�����浽���ݿ���
                    PathModel pathModel = new PathModel();
                    pathModel.Path = strFilePath;
                    pathModel.AdminID = Session["AdminID"].ToString();
                    Factory.Path().InsertInfo(pathModel);
                    Factory.AdminLog().InsertLog("�ϴ��ļ�\"" + strFilePath + "\"��", Session["AdminID"].ToString());

                    //
                    StringBuilder strJs = new StringBuilder();
                    strJs.Append("<script>");
                    if (strDialogType == "SelectDialog")
                    {
                        strJs.Append("parent.window.location.reload();");
                    }
                    else
                    {
                        strJs.Append("parent.document.getElementById('" + strObjName + "').value='" + strFilePath + "';");
                        strJs.Append("parent.document.getElementById('floatBoxBg').style.display='none';");
                        strJs.Append("parent.document.getElementById('floatBox').style.display='none';");
                    }
                    strJs.Append("</script>");
                    Config.ShowEnd(strJs.ToString());
                }
            }
        }

        #region ��ͼƬ���������֡�ͼƬˮӡ
        /// <summary> 
        /// ͼƬ�ȱ����� 
        /// </summary> 
        /// <param name="postedFile">ԭͼ��ŵ�ַ</param> 
        /// <param name="savePath">����ͼ��ŵ�ַ</param> 
        /// <param name="targetWidth">ָ���������</param> 
        /// <param name="targetHeight">ָ�������߶�</param> 
        /// <param name="watermarkText">ˮӡ����(Ϊ""��ʾ��ʹ��ˮӡ)</param> 
        /// <param name="watermarkImage">ˮӡͼƬ·��(Ϊ""��ʾ��ʹ��ˮӡ)</param> 
        public void ZoomAuto(string postedPath, string savePath, string watermarkText, string watermarkImage, SetModel seModel)
        {
            //����һ��ͼƬ��������װ��Ҫ�����ˮӡ��ͼƬ
            System.Drawing.Image initImage = System.Drawing.Image.FromFile(postedPath);

            string Style = seModel.Font; //������ʽ
            float FontSize = (float)int.Parse(seModel.FontSize);//�����С
            string FontColor = seModel.FontColor;//������ɫ
            string Position = seModel.WaterPosition;//ͼƬλ��
            int WidthX = 0;
            int HeightY = 0;

            //����ˮӡ 
            if (watermarkText != "")
            {
                using (System.Drawing.Graphics gWater = System.Drawing.Graphics.FromImage(initImage))
                {
                    System.Drawing.Font fontWater = null;
                    System.Drawing.Brush brushWater = null;

                    //������ʽ�ʹ�С
                    switch (Style)
                    {
                        case "Bold": fontWater = new Font("����", FontSize, FontStyle.Bold);
                            break;
                        case "Italic": fontWater = new Font("����", FontSize, FontStyle.Regular);
                            break;
                        case "Regular": fontWater = new Font("����", FontSize, FontStyle.Regular);
                            break;
                        case "Strikeout": fontWater = new Font("����", FontSize, FontStyle.Strikeout);
                            break;
                        case "Underline": fontWater = new Font("����", FontSize, FontStyle.Underline);
                            break;
                        default: break;
                    }

                    //������ɫ
                    switch (FontColor)
                    {
                        case "White": brushWater = new SolidBrush(Color.White);
                            break;
                        case "Red": brushWater = new SolidBrush(Color.Red);
                            break;
                        case "Green": brushWater = new SolidBrush(Color.Green);
                            break;
                        case "Gray": brushWater = new SolidBrush(Color.Gray);
                            break;
                        case "Blue": brushWater = new SolidBrush(Color.Blue);
                            break;
                        default: break;
                    }


                    gWater.DrawString(watermarkText, fontWater, brushWater, 10, 10);
                    gWater.Dispose();
                }
            }

            //͸��ͼƬˮӡ 
            if (watermarkImage != "")
            {
                if (File.Exists(watermarkImage))
                {
                    //��ȡˮӡͼƬ 
                    using (System.Drawing.Image wrImage = System.Drawing.Image.FromFile(watermarkImage))
                    {
                        //ˮӡ����������ԭʼͼƬ��߾����ڻ����ˮӡͼƬ 
                        if (initImage.Width >= wrImage.Width && initImage.Height >= wrImage.Height)
                        {
                            Graphics gWater = Graphics.FromImage(initImage);

                            //͸������ 
                            ImageAttributes imgAttributes = new ImageAttributes();
                            ColorMap colorMap = new ColorMap();
                            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                            ColorMap[] remapTable = { colorMap };
                            imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                            float[][] colorMatrixElements = {  
                                   new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f}, 
                                   new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f}, 
                                   new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f}, 
                                   new float[] {0.0f,  0.0f,  0.0f,  0.5f, 0.0f},//͸����:0.5 
                                   new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f} 
                                };

                            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                            imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);


                            switch (Position)
                            {
                                case "RightB":
                                    WidthX = initImage.Width - wrImage.Width;
                                    HeightY = initImage.Height - wrImage.Height;
                                    break;
                                case "RightT":
                                    WidthX = initImage.Width - wrImage.Width;
                                    HeightY = 0;
                                    break;
                                case "LeftB":
                                    WidthX = 0;
                                    HeightY = initImage.Height - wrImage.Height;
                                    break;
                                case "LeftT":
                                    WidthX = 0;
                                    HeightY = 0;
                                    break;
                                case "Center":
                                    WidthX = (initImage.Width - wrImage.Width) / 2;
                                    HeightY = (initImage.Height - wrImage.Height) / 2;
                                    break;
                                default: break;
                            }

                            gWater.DrawImage(wrImage, new Rectangle(WidthX, HeightY, wrImage.Width, wrImage.Height), 0, 0, wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);

                            gWater.Dispose();
                        }

                        wrImage.Dispose();
                    }
                }
            }

            //���� 
            initImage.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            initImage.Dispose();

        }

        #endregion
    }
}
