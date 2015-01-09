using System;
using System.Collections.Generic;
using System.Text;
//
using System.Drawing;
using http = System.Web.HttpContext;

namespace HxSoft.Common
{
    /// <summary>
    /// ����ַ���֤��,��ɫ����
    /// </summary>
    public class CheckCode
    {
        public CheckCode()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        /// <summary>
        /// ������֤��
        /// </summary>
        /// <param name="num">��֤��λ��</param>
        public void CreateImg(int num)
        {
            //��������ַ���
            string[] source ={ "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
        "a","b","c","d","e","f","g","h","i","j","k","l","m","n",
        "o","p","q","r","s","t","u","v","w","x","y","z"};
            string CheckCodeText = "";
            Random rd = new Random();
            for (int i = 0; i < num; i++)
            {
                CheckCodeText += source[rd.Next(0, source.Length)];
            }

            Bitmap myBmp = new Bitmap(num * 10 + 5, 20);
            Graphics myGra = Graphics.FromImage(myBmp);

            SolidBrush whitebrush = new SolidBrush(Color.White);
            SolidBrush bluebrush = new SolidBrush(Color.Blue);

            //����ͼƬ����
            myGra.FillRectangle(whitebrush, 0, 0, num * 10 + 5, 20);
            //��ʾ�ַ�
            myGra.DrawString(CheckCodeText, new Font("Verdana", 11), bluebrush, 1, 1);
            //ͼƬ����
            http.Current.Response.ContentType = "image/gif";
            //��ʾͼƬ
            myBmp.Save(http.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);

            http.Current.Session["CheckCode"] = CheckCodeText;
            myGra.Dispose();
            myBmp.Dispose();
        }
    }
}
