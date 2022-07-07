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

namespace WinformUDload
{
    public partial class Form1 : FormBase
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<string> list = new List<string>();

        private async void Form1_Load(object sender, EventArgs e)
        {
            // 显示行号
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
            // 允许调整行头宽度
            superGridControl1.PrimaryGrid.AllowRowHeaderResize = true;
            //superGridControl1.PrimaryGrid.Filter.Visible = true;
            superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            superGridControl1.PrimaryGrid.DataSource = await GetWebFilesDataTable();
        }

        private async Task<DataTable> GetWebFilesDataTable()
        {
            List<FileInformation> fileInformations = await GetWebFiles();
            return fileInformations.ToDataTable("fileInformations");

        }

        private async Task<List<FileInformation>> GetWebFiles()
        {
            DataTable data = null;

            using (HttpClient httpClient = new HttpClient())
            {
                var url = ConfigurationManager.AppSettings["GetInfo"].ToString();
                var response = await httpClient.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                var fileInformations = JsonConvert.DeserializeObject<List<FileInformation>>(json);
                return fileInformations;
            }
        }


        private async void btnRefresh_ClickAsync(object sender, EventArgs e)
        {
            superGridControl1.PrimaryGrid.DataSource = await GetWebFilesDataTable();
        }

        private async void btnUpload_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                FileUpLoadDto fileUpLoadDto = await FileUpload(openFileDialog1.FileName);

                if (fileUpLoadDto.Status)
                {
                    btnRefresh_ClickAsync(sender, e);
                    WriteMsg($"上传文件成功,文件MD5值：{fileUpLoadDto.fileInfo.FileMd5}");
                }
                else
                    WriteMsg($"上传文件失败：{fileUpLoadDto.Message}");
            }
        }


        /// <summary>
        ///   文件上传
        /// </summary>
        /// <param name="filePath">本地需要上传的文件路径</param>
        /// <returns></returns>
        private async Task<FileUpLoadDto> FileUpload(string filePath)
        {
            FileUpLoadDto fileUpLoadDto = null;
            using (var httpClient = new HttpClient())
            {
                var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var fileName = Path.GetFileName(filePath);
                var content = new MultipartFormDataContent();
                //  Add 方法 第一个参数是content 
                content.Add(new StreamContent(fs), "file", fileName);
                var url = ConfigurationManager.AppSettings["Upload"].ToString();
                var response = await httpClient.PostAsync(url, content);
                var json = await response.Content.ReadAsStringAsync();
                fileUpLoadDto = JsonConvert.DeserializeObject<FileUpLoadDto>(json);
            }
            return fileUpLoadDto;
        }

        /// <summary>
        ///  写日志到textbox当中
        /// </summary>
        /// <param name="msg"></param>
        protected void WriteMsg(string msg)
        {
            var timeMsg = $"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}   :    ";
            RTxtMessage.AppendText(timeMsg + msg);
            RTxtMessage.AppendText("\r\n");
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        private async Task<ResultMessage> DownFile(string fileName)
        {
            ResultMessage resultMessage = null;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = ConfigurationManager.AppSettings["DownFile"].ToString();
                    url += "?fileName=" + fileName;
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        using (var stream = await response.Content.ReadAsStreamAsync())
                        {
                            string winPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), fileName);
                            await stream.CopyToFileAsync(winPath);
                            resultMessage = new ResultMessage { Status = true, Message = "成功" };
                        }
                    }
                    else
                    {
                        resultMessage = new ResultMessage { Status = false, Message = response.ToString() };
                    }
                }
            }
            catch (Exception ex)
            {
                resultMessage = new ResultMessage { Status = false, Message = ex.Message };
            }

            return resultMessage;
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            var fileName = GetDgvValue("FileName");
            if (fileName == null)
                return;
            ResultMessage resultMessage = await DownFile(fileName);
            if (resultMessage.Status)
                WriteMsg($"  【{fileName}】 下载成功！ ");
            else
                WriteMsg($"  【{fileName}】 下载失败:  " + resultMessage.Message);
        }


        /// <summary>
        /// 获取当前选中的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetDgvValue(string key)
        {
            var elements = superGridControl1.PrimaryGrid.GetSelectedRows();
            if (elements == null)
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

        private async void GetNeedDownFiles()
        {
            string location = Assembly.GetEntryAssembly().Location;
            List<FileInformation> fileInformations = await GetWebFiles();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var fileNames = Directory.GetFiles(Path.GetDirectoryName(location)).ToList();
            fileNames.ForEach(s =>
            {
                dict.Add(key: Path.GetFileName(s), value: s);
            });
            var fileList = (from a in dict
                            join b in fileInformations on a.Key equals b.FileName
                            select a).ToList();

            List<FileInformation> localfileInformations = await GetWebFiles();

            fileList.ForEach(async f =>
            {
                using (var fs = new FileStream(f.Value, FileMode.Open, FileAccess.Read))
                {

                    FileInformation localFileInformation = new FileInformation
                    {
                        FileName = Path.GetFileName(f.Value),
                        FileMd5 = fs.GetFileMD5(),
                    };

                    var NeedDownfileInformation = fileInformations.Where(w => w.FileName == localFileInformation.FileName && w.FileMd5 != localFileInformation.FileMd5).FirstOrDefault();

                    if (NeedDownfileInformation != null)
                    {
                        ResultMessage resultMessage = await DownFile(NeedDownfileInformation.FileName);
                        if (resultMessage.Status)
                            WriteMsg($"  【{NeedDownfileInformation.FileName}】 下载成功！ ");
                        else
                            WriteMsg($"  【{NeedDownfileInformation.FileName}】 下载失败:  " + resultMessage.Message);
                    }

                }
            });


        }

        private void btnDownlaodMore_Click(object sender, EventArgs e)
        {
            GetNeedDownFiles();
        }
    }
}
