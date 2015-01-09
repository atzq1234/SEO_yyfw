using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;
using System.Web.SessionState;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace HxSoft.Web.Admin.Ajax
{
    /// <summary>
    /// Ajax_FileHandle 的摘要说明
    /// </summary>
    public class Ajax_FileHandle : IHttpHandler, IRequiresSessionState
    {
        public string strFolderPath
        {
            get
            {
                return HttpContext.Current.Request["Path"].ToString();
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            PathModel pathModel = new PathModel();
            string strJson = string.Empty;

            //获取图片session值  
            if (ListThumbnail.ListThum.Count != 0)
            {

                foreach (Thumbnail t in ListThumbnail.ListThum)
                {
                    string sy_imgPath = strFolderPath + t.Name;//水印存储路径

                    if (File.Exists(context.Server.MapPath(sy_imgPath)))
                    {
                        Random rnd = new Random();
                        string strRnd = "_" + rnd.Next(1000, 9999).ToString();
                        sy_imgPath = strFolderPath + strRnd + t.Name;
                    }
                    //原始图片保存至服务器开始
                    FileStream fs = new FileStream(context.Server.MapPath(sy_imgPath), FileMode.Create);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(t.OriginalData);
                    bw.Close();
                    fs.Close();


                    //生成文字水印图片开始

                    SetModel seModel = new SetModel();
                    seModel = Factory.Set().GetInfo();
                    if (seModel != null)
                    {
                        if (seModel.WaterTypeID == "0" || strFolderPath == "/File/WaterPic/")
                        {
                        }
                        else if (seModel.WaterTypeID == "1")
                        {
                            ZoomAuto(t.OriginalData, context.Server.MapPath(sy_imgPath), seModel.WaterText, "", seModel);
                        }
                        //生成图片水印图片开始
                        else if (seModel.WaterTypeID == "2")
                        {
                            ZoomAuto(t.OriginalData, context.Server.MapPath(sy_imgPath), "", context.Server.MapPath(seModel.WaterPic), seModel);
                        }
                    }
                    pathModel.Path = sy_imgPath;
                    pathModel.AdminID = context.Session["AdminID"].ToString();
                    Factory.Path().InsertInfo(pathModel);
                    Factory.AdminLog().InsertLog("上传图片\"" + sy_imgPath + "\"。", context.Session["AdminID"].ToString());
                }

                strJson = "{\"msg\":\"成功上传" + ListThumbnail.ListThum.Count + "张图片\"}";
            }
            else
            {
                strJson = "{\"msg\":\"请选择需要上传的图片!\"}";

            }
            context.Response.Write(strJson);
            ListThumbnail.ListThum.Clear();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region 在图片上生成文字、图片水印
        /// <summary> 
        /// 图片等比缩放 
        /// </summary> 
        /// <param name="postedFile">原图流对象</param> 
        /// <param name="savePath">缩略图存放地址</param> 
        /// <param name="targetWidth">指定的最大宽度</param> 
        /// <param name="targetHeight">指定的最大高度</param> 
        /// <param name="watermarkText">水印文字(为""表示不使用水印)</param> 
        /// <param name="watermarkImage">水印图片路径(为""表示不使用水印)</param> 
        public void ZoomAuto(byte[] originalData, string savePath, string watermarkText, string watermarkImage, SetModel seModel)
        {
            //创建一个图片对象用来装载要被添加水印的图片
            MemoryStream ms = new MemoryStream(originalData);
            Image initImage = Image.FromStream(ms);

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