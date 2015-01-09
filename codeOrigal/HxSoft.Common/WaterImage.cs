using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace HxSoft.Common
{
    public class WaterImage
    {
        public WaterImage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
        }


        #region 生成缩略图
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形） 
                    if (ow > width || oh > height)
                    {
                        if ((double)originalImage.Width / (double)originalImage.Height > (double)width / (double)height)
                        {
                            oh = originalImage.Height;
                            ow = originalImage.Height * width / height;
                            y = 0;
                            x = (originalImage.Width - ow) / 2;
                        }
                        else
                        {
                            ow = originalImage.Width;
                            oh = originalImage.Width * height / width;
                            x = 0;
                            y = (originalImage.Height - oh) / 2;
                        }
                    }
                    else
                    {
                        //原图保存
                        towidth = ow;
                        toheight = oh;
                    }
                    break;
                case "CutScale"://指定高宽裁减按比例
                    int TW = originalImage.Width * height / originalImage.Height;
                    int TH = originalImage.Height * width / originalImage.Width;
                    if (ow > width)
                    {
                        if (TH > height)
                        {
                            towidth = TW;
                            toheight = height;
                        }
                        else
                        {
                            towidth = width;
                            toheight = TH;
                        }
                    }
                    else
                    {
                        if (oh > height)
                        {
                            towidth = TW;
                            toheight = height;
                        }
                        else
                        {
                            towidth = ow;
                            toheight = oh;
                        }
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        #endregion

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
        public static void ZoomAuto(byte[] originalData, string savePath, Double targetWidth, Double targetHeight, string watermarkText, string watermarkImage)
        {
            //创建一个图片对象用来装载要被添加水印的图片
            MemoryStream ms = new MemoryStream(originalData);
            Image initImage = Image.FromStream(ms);
            //原图宽高均小于模版，不作处理，直接保存 
            if (initImage.Width <= targetWidth && initImage.Height <= targetHeight)
            {
                //文字水印 
                if (watermarkText != "")
                {
                    using (System.Drawing.Graphics gWater = System.Drawing.Graphics.FromImage(initImage))
                    {
                        System.Drawing.Font fontWater = new Font("黑体", 10, FontStyle.Underline);
                        System.Drawing.Brush brushWater = new SolidBrush(Color.Gray);
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
                                gWater.DrawImage(wrImage, new Rectangle(initImage.Width - wrImage.Width, initImage.Height - wrImage.Height, wrImage.Width, wrImage.Height), 0, 0, wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);

                                gWater.Dispose();
                            }

                            wrImage.Dispose();
                        }
                    }
                }

                //保存 
                initImage.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                //缩略图宽、高计算 
                double newWidth = initImage.Width;
                double newHeight = initImage.Height;

                //宽大于高或宽等于高（横图或正方） 
                if (initImage.Width > initImage.Height || initImage.Width == initImage.Height)
                {
                    //如果宽大于模版 
                    if (initImage.Width > targetWidth)
                    {
                        //宽按模版，高按比例缩放 
                        newWidth = targetWidth;
                        newHeight = initImage.Height * (targetWidth / initImage.Width);
                    }
                }
                //高大于宽（竖图） 
                else
                {
                    //如果高大于模版 
                    if (initImage.Height > targetHeight)
                    {
                        //高按模版，宽按比例缩放 
                        newHeight = targetHeight;
                        newWidth = initImage.Width * (targetHeight / initImage.Height);
                    }
                }

                //生成新图 
                //新建一个bmp图片 
                System.Drawing.Image newImage = new System.Drawing.Bitmap((int)newWidth, (int)newHeight);
                //新建一个画板 
                System.Drawing.Graphics newG = System.Drawing.Graphics.FromImage(newImage);

                //设置质量 
                newG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                newG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //置背景色 
                newG.Clear(Color.White);
                //画图 
                newG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, newImage.Width, newImage.Height), new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);

                //文字水印 
                if (watermarkText != "")
                {
                    using (System.Drawing.Graphics gWater = System.Drawing.Graphics.FromImage(newImage))
                    {
                        System.Drawing.Font fontWater = new Font("宋体", 10);
                        System.Drawing.Brush brushWater = new SolidBrush(Color.White);
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
                            if (newImage.Width >= wrImage.Width && newImage.Height >= wrImage.Height)
                            {
                                Graphics gWater = Graphics.FromImage(newImage);

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
                                gWater.DrawImage(wrImage, new Rectangle(newImage.Width - wrImage.Width, newImage.Height - wrImage.Height, wrImage.Width, wrImage.Height), 0, 0, wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);
                                gWater.Dispose();
                            }
                            wrImage.Dispose();
                        }
                    }
                }

                //保存缩略图 
                newImage.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                //释放资源 
                newG.Dispose();
                newImage.Dispose();
                initImage.Dispose();
            }
        }

        #endregion


    }

}
