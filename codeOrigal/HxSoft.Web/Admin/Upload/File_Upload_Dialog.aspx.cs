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
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
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
        //页面初始化
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

        //保存文件
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
                            if (!GetData.CheckAdminID(pathModel.AdminID, "FileAll"))//检查创建者
                            {
                                //文件不属于当前用户,则生成随机文件名
                                Random rnd = new Random();
                                strRnd = "_" + rnd.Next(1000, 9999).ToString();
                            }
                        }
                        else//文件存在,但在数据库中找不到,则生成随机文件名
                        {
                            Random rnd = new Random();
                            strRnd = "_" + rnd.Next(1000, 9999).ToString();
                        }
                    }
                }

                string strTempDir = Config.FileUploadPath + "temp/";//临时目录
                Config.CheckFolder(Server.MapPath(strTempDir));
                string strTempFile = strTempDir + strFileName + strRnd + strFileExt;//临时文件   
                string strFilePath = strFolderPath + strFileName + strRnd + strFileExt;//实际保存路径
                if (strFileName.IndexOf(".asp") > -1 || strFileName.IndexOf(".asa") > -1 || strFileName.IndexOf(".aspx") > -1 || strFileName.IndexOf(".ashx") > -1 || strFileName.IndexOf(".jsp") > -1 || strFileName.IndexOf(".php") > -1)
                {
                    errMsg.Text = "不允许上传可执行文件!";
                }
                else if (strFileExt.IndexOf(".asp") > -1 || strFileExt.IndexOf(".asa") > -1 || strFileExt.IndexOf(".aspx") > -1 || strFileExt.IndexOf(".ashx") > -1 || strFileExt.IndexOf(".jsp") > -1 || strFileExt.IndexOf(".php") > -1)
                {
                    errMsg.Text = "不允许上传可执行文件!";
                }
                else
                {
                    if (!Config.IsPicture3(strFileExt))//不是图片
                    {
                        InputFile1.MoveTo(Server.MapPath(strFilePath), MoveToOptions.Overwrite);
                    }
                    else
                    {
                        //生成文字水印图片开始
                        SetModel seModel = new SetModel();
                        seModel = Factory.Set().GetInfo();
                        if (seModel != null)
                        {
                            if (seModel.WaterTypeID == "1")//文字水印
                            {
                                InputFile1.MoveTo(Server.MapPath(strTempFile), MoveToOptions.Overwrite);

                                ZoomAuto(Server.MapPath(strTempFile), Server.MapPath(strFilePath), seModel.WaterText, "", seModel);
                                File.Delete(Server.MapPath(strTempFile));
                            }
                            else if (seModel.WaterTypeID == "2")//图片水印
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
                    //将上传的文件路径保存到数据库中
                    PathModel pathModel = new PathModel();
                    pathModel.Path = strFilePath;
                    pathModel.AdminID = Session["AdminID"].ToString();
                    Factory.Path().InsertInfo(pathModel);
                    Factory.AdminLog().InsertLog("上传文件\"" + strFilePath + "\"。", Session["AdminID"].ToString());

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

        #region 在图片上生成文字、图片水印
        /// <summary> 
        /// 图片等比缩放 
        /// </summary> 
        /// <param name="postedFile">原图存放地址</param> 
        /// <param name="savePath">缩略图存放地址</param> 
        /// <param name="targetWidth">指定的最大宽度</param> 
        /// <param name="targetHeight">指定的最大高度</param> 
        /// <param name="watermarkText">水印文字(为""表示不使用水印)</param> 
        /// <param name="watermarkImage">水印图片路径(为""表示不使用水印)</param> 
        public void ZoomAuto(string postedPath, string savePath, string watermarkText, string watermarkImage, SetModel seModel)
        {
            //创建一个图片对象用来装载要被添加水印的图片
            System.Drawing.Image initImage = System.Drawing.Image.FromFile(postedPath);

            string Style = seModel.Font; //字体样式
            float FontSize = (float)int.Parse(seModel.FontSize);//字体大小
            string FontColor = seModel.FontColor;//字体颜色
            string Position = seModel.WaterPosition;//图片位置
            int WidthX = 0;
            int HeightY = 0;

            //文字水印 
            if (watermarkText != "")
            {
                using (System.Drawing.Graphics gWater = System.Drawing.Graphics.FromImage(initImage))
                {
                    System.Drawing.Font fontWater = null;
                    System.Drawing.Brush brushWater = null;

                    //字体样式和大小
                    switch (Style)
                    {
                        case "Bold": fontWater = new Font("宋体", FontSize, FontStyle.Bold);
                            break;
                        case "Italic": fontWater = new Font("宋体", FontSize, FontStyle.Regular);
                            break;
                        case "Regular": fontWater = new Font("宋体", FontSize, FontStyle.Regular);
                            break;
                        case "Strikeout": fontWater = new Font("宋体", FontSize, FontStyle.Strikeout);
                            break;
                        case "Underline": fontWater = new Font("宋体", FontSize, FontStyle.Underline);
                            break;
                        default: break;
                    }

                    //字体颜色
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

            //透明图片水印 
            if (watermarkImage != "")
            {
                if (File.Exists(watermarkImage))
                {
                    //获取水印图片 
                    using (System.Drawing.Image wrImage = System.Drawing.Image.FromFile(watermarkImage))
                    {
                        //水印绘制条件：原始图片宽高均大于或等于水印图片 
                        if (initImage.Width >= wrImage.Width && initImage.Height >= wrImage.Height)
                        {
                            Graphics gWater = Graphics.FromImage(initImage);

                            //透明属性 
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
                                   new float[] {0.0f,  0.0f,  0.0f,  0.5f, 0.0f},//透明度:0.5 
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

            //保存 
            initImage.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            initImage.Dispose();

        }

        #endregion
    }
}
