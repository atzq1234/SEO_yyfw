using System;
using System.Collections.Generic;
using System.Text;
//
using System.Drawing;


namespace HxSoft.Common
{
/// <summary>
/// ��ɫ��֤��
/// </summary>
    public class VryImgGen
    {
        public static string ChineseChars = String.Empty;

        /// <summary>
        /// Ӣ�������ִ�
        /// </summary>
        protected static readonly string EnglishOrNumChars = "1234567890";

        public VryImgGen()
        {
            rnd = new Random(unchecked((int)DateTime.Now.Ticks));
        }

        /// <summary>
        /// ȫ�������������
        /// </summary>
        private Random rnd;

        int length = 4;
        /// <summary>
        /// ��֤�볤��(Ĭ��4����֤��ĳ���)
        /// </summary>
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        int fontSize = 15;
        /// <summary>
        /// ��֤�������С(Ϊ����ʾŤ��Ч����Ĭ��15���أ����������޸�)
        /// </summary>
        public int FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }

        int padding = 2;
        /// <summary>
        /// �߿�(Ĭ��2����)
        /// </summary>
        public int Padding
        {
            get { return padding; }
            set { padding = value; }
        }

        bool chaos = true;
        /// <summary>
        /// �Ƿ�������(Ĭ�����)
        /// </summary>
        public bool Chaos
        {
            get { return chaos; }
            set { chaos = value; }
        }

        Color chaosColor = Color.LightGray;
        /// <summary>
        /// ���������ɫ(Ĭ�ϻ�ɫ)
        /// </summary>
        public Color ChaosColor
        {
            get { return chaosColor; }
            set { chaosColor = value; }
        }

        Color backgroundColor = Color.White;
        /// <summary>
        /// �Զ��屳��ɫ(Ĭ�ϰ�ɫ)
        /// </summary>
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        Color[] colors = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        /// <summary>
        /// �Զ��������ɫ����
        /// </summary>
        public Color[] Colors
        {
            get { return colors; }
            set { colors = value; }
        }

        string[] fonts = { "Arial", "Verdana" };
        /// <summary>
        /// �Զ�����������
        /// </summary>
        public string[] Fonts
        {
            get { return fonts; }
            set { fonts = value; }
        }

        #region ���������˾�Ч��TwistImage

        private const double PI = 3.1415926535897932384626433832795;
        private const double PI2 = 6.283185307179586476925286766559;

        /// <summary>
        /// ��������WaveŤ��ͼƬ��Edit By 51aspx.com��
        /// </summary>
        /// <param name="srcBmp">ͼƬ·��</param>
        /// <param name="bXDir">���Ť����ѡ��ΪTrue</param>
        /// <param name="nMultValue">���εķ��ȱ�����Խ��Ť���ĳ̶�Խ�ߣ�һ��Ϊ3</param>
        /// <param name="dPhase">���ε���ʼ��λ��ȡֵ����[0-2*PI)</param>
        /// <returns></returns>
        public System.Drawing.Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            System.Drawing.Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // ��λͼ�������Ϊ��ɫ
            System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(System.Drawing.Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();

            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;

            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI2 * (double)j) / dBaseAxisLen : (PI2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // ȡ�õ�ǰ�����ɫ
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    System.Drawing.Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
        }



        #endregion

        /// <summary>
        /// ����У����ͼƬ
        /// </summary>
        /// <param name="code">��֤��</param>
        /// <returns></returns>
        public Bitmap CreateImage(string code)
        {
            int fSize = FontSize;
            int fWidth = fSize + Padding;

            int imageWidth = (int)(code.Length * fWidth) + 4 + Padding * 2;
            int imageHeight = fSize + Padding * 4;

            System.Drawing.Bitmap image = new System.Drawing.Bitmap(imageWidth, imageHeight);

            Graphics g = Graphics.FromImage(image);

            g.Clear(BackgroundColor);

            //���������������ɵ����
            if (this.Chaos)
            {

                Pen pen = new Pen(ChaosColor, 0);
                int c = Length * 10;

                for (int i = 0; i < c; i++)
                {
                    int x = rnd.Next(image.Width);
                    int y = rnd.Next(image.Height);

                    g.DrawRectangle(pen, x, y, 1, 1);
                }
            }

            int left = 0, top = 0, top1 = 1, top2 = 1;

            int n1 = (imageHeight - FontSize - Padding * 2);
            int n2 = n1 / 4;
            top1 = n2;
            top2 = n2 * 2;

            Font f;
            Brush b;

            int cindex, findex;

            //����������ɫ����֤���ַ�
            for (int i = 0; i < code.Length; i++)
            {
                cindex = rnd.Next(Colors.Length - 1);
                findex = rnd.Next(Fonts.Length - 1);

                f = new System.Drawing.Font(Fonts[findex], fSize, System.Drawing.FontStyle.Bold);
                b = new System.Drawing.SolidBrush(Colors[cindex]);

                if (i % 2 == 1)
                {
                    top = top2;
                }
                else
                {
                    top = top1;
                }

                left = i * fWidth;

                g.DrawString(code.Substring(i, 1), f, b, left, top);
            }

            //��һ���߿� �߿���ɫΪColor.Gainsboro
            g.DrawRectangle(new Pen(Color.Gainsboro, 0), 0, 0, image.Width - 1, image.Height - 1);
            g.Dispose();

            //�������Σ�Add By 51aspx.com��
            image = TwistImage(image, true, 2, 4);

            return image;
        }

        #region ��������ַ���CreateVerifyCode
        /// <summary>
        /// ��������ַ���
        /// </summary>
        /// <param name="codeLen">�ַ�������</param>
        /// <param name="zhCharsCount">�����ַ���</param>
        /// <returns></returns>
        public string CreateVerifyCode(int codeLen, int zhCharsCount)
        {
            char[] chs = new char[codeLen];

            int index;
            for (int i = 0; i < zhCharsCount; i++)
            {
                index = rnd.Next(0, codeLen);
                if (chs[index] == '\0')
                    chs[index] = CreateZhChar();
                else
                    --i;
            }
            for (int i = 0; i < codeLen; i++)
            {
                if (chs[i] == '\0')
                    chs[i] = CreateEnOrNumChar();
            }

            return new string(chs, 0, chs.Length);
        }

        /// <summary>
        /// ����Ӣ�Ļ������ַ�
        /// </summary>
        /// <returns></returns>
        protected char CreateEnOrNumChar()
        {
            return EnglishOrNumChars[rnd.Next(0, EnglishOrNumChars.Length)];
        }

        /// <summary>
        /// ���ɺ����ַ�
        /// </summary>
        /// <returns></returns>
        protected char CreateZhChar()
        {
            //���ṩ�˺��ּ�����ѯ���ּ�ѡȡ����
            if (ChineseChars.Length > 0)
            {
                return ChineseChars[rnd.Next(0, ChineseChars.Length)];
            }
            //��û���ṩ���ּ�������ݡ�GB2312�������ı������������캺��
            else
            {
                byte[] bytes = new byte[2];

                //��һ���ֽ�ֵ��0xb0, 0xf7֮��
                bytes[0] = (byte)rnd.Next(0xb0, 0xf8);
                //�ڶ����ֽ�ֵ��0xa1, 0xfe֮��
                bytes[1] = (byte)rnd.Next(0xa1, 0xff);

                //���ݺ��ֱ�����ֽ������������ĺ���
                string str1 = Encoding.GetEncoding("gb2312").GetString(bytes);

                return str1[0];
            }
        }
        #endregion
    }
}
