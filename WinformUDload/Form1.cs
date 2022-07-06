using Masuit.Tools.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
            superGridControl1.PrimaryGrid.DataSource = await GetWebFiles();
        }

        private async Task<DataTable> GetWebFiles()
        {
            DataTable data = null;
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("https://localhost:7238/api/UpDown/SelectFiles");
                string json = await httpResponseMessage.Content.ReadAsStringAsync();
                List<FileInformation> fileInformations = JsonConvert.DeserializeObject<List<FileInformation>>(json);
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
                    MessageBox.Show("上传文件成功");
                else
                    MessageBox.Show("上传文件失败：" + fileUpLoadDto.Message);

                btnRefresh_ClickAsync(sender, e);
            }

        }


        private async Task<FileUpLoadDto> FileUpload(string fileName)
        {
            FileUpLoadDto fileUpLoadDto = null;
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                byte[] bytes = new byte[fs.Length];
                BinaryWriter binaryWriter = new BinaryWriter(fs);
                binaryWriter.Write(bytes);  // 写入byte数组当中
                using (HttpClient httpClient = new HttpClient())
                {
                    MultipartFormDataContent form = new MultipartFormDataContent();

                    ByteArrayContent byteArrayContent = new ByteArrayContent(bytes);
                    string v = Path.GetFileName(fileName);
                    form.Add(byteArrayContent,Path.GetFileNameWithoutExtension(fileName), Path.GetFileName(fileName));
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("https://localhost:7238/api/UpDown/UploadFile",
                       form);
                    string json = await httpResponseMessage.Content.ReadAsStringAsync();
                    fileUpLoadDto = JsonConvert.DeserializeObject<FileUpLoadDto>(json);
                }
            }
            return fileUpLoadDto;
        }
    }
}
