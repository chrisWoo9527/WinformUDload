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
            superGridControl1.PrimaryGrid.DataSource = await GetWebFiles();
        }

        private async Task<DataTable> GetWebFiles()
        {
            DataTable data = null;

            using (HttpClient httpClient = new HttpClient())
            {
                var url = ConfigurationManager.AppSettings["GetInfo"].ToString();
                var response = await httpClient.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                var fileInformations = JsonConvert.DeserializeObject<List<FileInformation>>(json);
                data = fileInformations.ToDataTable("fileInformations");
            }
            return data;
        }

        private async void btnRefresh_ClickAsync(object sender, EventArgs e)
        {
            superGridControl1.PrimaryGrid.DataSource = await GetWebFiles();
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
        protected void WriteMsg(string msg)
        {
            var timeMsg = $"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}   :    ";
            RTxtMessage.AppendText(timeMsg + msg);
            RTxtMessage.AppendText("\r\n");
        }

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
            ResultMessage resultMessage = await DownFile(fileName);
            if (resultMessage.Status)
                WriteMsg($"  【{fileName}】 下载成功！ ");
            else
                WriteMsg($"  【{fileName}】 下载失败:  " + resultMessage.Message);
        }


        protected string GetDgvValue(string key)
        {
            var elements = superGridControl1.PrimaryGrid.GetSelectedRows();
            GridRow gridrow = elements[0] as GridRow;
            return gridrow.Cells[key].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            RTxtMessage.Clear();
        }
    }
}
