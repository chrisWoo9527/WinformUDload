using DevComponents.DotNetBar.SuperGrid;
using Masuit.Tools.Database;
using Masuit.Tools.Files;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformUDload.Services;

namespace WinformUDload
{
    public partial class Form1 : FormBase
    {
        private readonly FileLoadServices _fls = null;

        public Form1()
        {
            InitializeComponent();
            _fls = new FileLoadServices();
        }

        private List<string> list = new List<string>();

        private async void Form1_Load(object sender, EventArgs e)
        {
            // 显示行号
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
            // 允许调整行头宽度
            superGridControl1.PrimaryGrid.AllowRowHeaderResize = true;
            superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            superGridControl1.PrimaryGrid.DataSource = await _fls.GetWebFilesDataTable();
            openFileDialog1.Multiselect = true;  // 允许多选
        }


        private async void btnRefresh_ClickAsync(object sender, EventArgs e)
        {
            superGridControl1.PrimaryGrid.DataSource = await _fls.GetWebFilesDataTable();
        }

        private async void btnUpload_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                string[] fileNames = openFileDialog1.FileNames;
                WriteMsg($"当前上传文件个数 {fileNames.Count()} ,开始上传~");

                for (int i = 0; i < fileNames.Length; i++)
                {
                    string fileName = fileNames[i];
                    FileUpLoadDto fileUpLoadDto = await _fls.FileUpload(fileName);

                    if (fileUpLoadDto.Status)
                    {
                        btnRefresh_ClickAsync(sender, e);
                        WriteMsg($"上传【{fileName}】成功,文件MD5值：{fileUpLoadDto.fileInfo.FileMd5}~");
                    }
                    else
                        WriteMsg($"上传【{fileName}】失败：{fileUpLoadDto.Message}~");
                }
                WriteMsg($"上传完成~");
            }
        }

        /// <summary>
        ///  写日志到textbox当中
        /// </summary>
        /// <param name="msg"></param>
        protected void WriteMsg(string msg)
        {
            var timeMsg = $"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} : ";
            RTxtMessage.AppendText(timeMsg + msg);
            RTxtMessage.AppendText("\r\n");
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            var fileName = GetDgvValue("FileName");
            if (fileName == null)
                return;
            ResultMessage resultMessage = await _fls.DownFile(fileName);
            if (resultMessage.Status)
                WriteMsg($"【{fileName}】 下载成功！ ");
            else
                WriteMsg($"【{fileName}】 下载失败:  " + resultMessage.Message);
        }

        /// <summary>
        /// 获取当前选中的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetDgvValue(string key)
        {
            var elements = superGridControl1.PrimaryGrid.GetSelectedRows();

            if (elements.Count() == 0)
            {
                WriteMsg("当前无选中行");
                return null;
            }
            else
            {
                GridRow gridrow = elements[0] as GridRow;
                return gridrow.Cells[key].Value.ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            RTxtMessage.Clear();
        }

        /// <summary>
        /// 下载文件循环
        /// </summary>
        /// <param name="input"></param>
        private void DownNeedFile(List<FileInformation> input)
        {
            if (!input.Any())
            {
                WriteMsg($"当前没有需要下载的内容 ");
                return;
            }

            WriteMsg($"开始下载 文件个数：{input.Count()}");

            input.ForEach(async f =>
            {
                ResultMessage resultMessage = await _fls.DownFile(f.FileName);
                if (resultMessage.Status)
                    WriteMsg($"【{f.FileName}】 下载成功！ ");
                else
                    WriteMsg($"【{f.FileName}】 下载失败:  " + resultMessage.Message);

            });
        }

        private async void btnDownlaodMore_Click(object sender, EventArgs e)
        {
            var fileInformations = await _fls.GetNeedDownFiles();
            DownNeedFile(fileInformations);
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var fileName = GetDgvValue("FileName");
            if (fileName == null)
                return;

            if (MessageBox.Show($"确定要删除文明名【{fileName}】? 当前操作不可逆~", "",
                                                           MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ResultMessage resultMessage = await _fls.DeleteFile(fileName);
                if (resultMessage.Status)
                {
                    WriteMsg($"【{fileName}】删除成功~");
                }
                else
                {
                    WriteMsg($"【{fileName}】删除失败:{resultMessage.Message}~");
                }
                btnRefresh_ClickAsync(sender, e);
            }
        }
    }
}
