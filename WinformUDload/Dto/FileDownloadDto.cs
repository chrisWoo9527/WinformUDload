using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformUDload
{
    public class FileDownloadDto
    {
        /// <summary>
        /// 文件名称(含后缀)
        /// </summary>
        public string FileName { get; set; }
    
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FileNamePath { get; set; }

        /// <summary>
        /// 文件Md5值
        /// </summary>
        public string FileMd5 { get; set; }
    }
}
