using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.VisualBasic;

namespace HxSoft.Common
{
    /// <summary>
    /// 繁体转简体
    /// </summary>
    public class CB2GFilter : Stream
    {
        // Fields
        private long position;
        private StringBuilder responseHtml;
        private Stream responseStream;

        // Properties
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public override long Length
        {
            get
            {
                return 0L;
            }
        }

        public override long Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        // Methods
        public CB2GFilter(Stream inputStream)
        {
            responseStream = inputStream;
            responseHtml = new StringBuilder();
        }
        public override void Close()
        {
            responseStream.Close();
        }

        public override void Flush()
        {
            responseStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return responseStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return responseStream.Seek(offset, origin);
        }

        public override void SetLength(long length)
        {
            responseStream.SetLength(length);
        }

        /// <summary>
        /// 简体转繁体
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Simplified2Traditional(string str)
        {
            return Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
        }

        /// <summary>
        /// 繁体转简体
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Traditional2Simplified(string str)
        {
            return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
        }

        /// <summary>
        /// 转换任务
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            string str = Encoding.UTF8.GetString(buffer, offset, count);
            string s = Traditional2Simplified(str);
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            responseStream.Write(bytes, 0, bytes.Length);
        }
    }
}
