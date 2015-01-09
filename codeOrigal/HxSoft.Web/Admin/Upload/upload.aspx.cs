using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HxSoft.Web.Admin.Upload
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Drawing.Image thumbnail_image = null;
            System.Drawing.Image original_image = null;
            System.Drawing.Bitmap final_image = null;
            System.Drawing.Graphics graphic = null;
            MemoryStream ms = null;
            MemoryStream original_ms = null;
            try
            {
                // 获取传过来的文件数据
                HttpPostedFile jpeg_image_upload = Request.Files["Filedata"];
                //保存文件名
                string filename = jpeg_image_upload.FileName;
                // 获取最原始的图片
                original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
                // 保存原图大小及缩略图片大小
                int width = original_image.Width;
                int height = original_image.Height;
                int target_width = 132;
                int target_height = 82;
                int new_width, new_height;
                //计算缩略图的大小
                float target_ratio = (float)target_width / (float)target_height;
                float image_ratio = (float)width / (float)height;
                if (target_ratio > image_ratio)
                {
                    new_height = target_height;
                    new_width = (int)Math.Floor(image_ratio * (float)target_height);
                }
                else
                {
                    new_height = (int)Math.Floor((float)target_width / image_ratio);
                    new_width = target_width;
                }
                new_width = new_width > target_width ? target_width : new_width;
                new_height = new_height > target_height ? target_height : new_height;
                //绘制最终的缩略图
                final_image = new System.Drawing.Bitmap(target_width, target_height);
                graphic = System.Drawing.Graphics.FromImage(final_image);
                graphic.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), new System.Drawing.Rectangle(0, 0, target_width, target_height));
                int paste_x = (target_width - new_width) / 2;
                int paste_y = (target_height - new_height) / 2;
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphic.DrawImage(original_image, paste_x, paste_y, new_width, new_height);

                //缩略图
                ms = new MemoryStream();
                final_image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                string thumbnail_id = filename;//图片名称            
                //原始图
                original_ms = new MemoryStream();
                original_image.Save(original_ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                //构造
                Thumbnail thum = new Thumbnail(thumbnail_id, ms.GetBuffer(), original_ms.GetBuffer());

                //保存缩略图及原始图（保存thumbnail类到Session(这样很不好，会占用大量的内存））
                //List<Thumbnail> thumbnails = Session["AmbryPartImageList"] as List<Thumbnail>;
                //if (thumbnails == null)
                //{
                //    thumbnails = new List<Thumbnail>();
                //    Session["AmbryPartImageList"] = thumbnails;
                //}
                //thumbnails.Add(thum);
                //避免占用大量内存，使用全局静态变量
                ListThumbnail.ListThum.Add(thum);
                Response.StatusCode = 200;
                Response.Write(thumbnail_id);
            }
            catch
            {
                //500错误
                Response.StatusCode = 500;
                Response.Write("An error occured");
                Response.End();
            }
            finally
            {
                //释放资源
                if (final_image != null) final_image.Dispose();
                if (graphic != null) graphic.Dispose();
                if (original_image != null) original_image.Dispose();
                if (thumbnail_image != null) thumbnail_image.Dispose();
                if (ms != null) ms.Close();
                if (original_ms != null) original_ms.Close();
                Response.End();
            }
        }
    }
}