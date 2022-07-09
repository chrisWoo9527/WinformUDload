using Masuit.Tools.Database;
using Masuit.Tools.Files;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WinformUDload.Services
{
    public class FileLoadServices
    {
        public async Task<ResultMessage> DeleteFile(string fileName)
        {
            using (var client = new HttpClient())
            {
                var url = ConfigurationManager.AppSettings["DelInfo"].ToString();
                url += $"?fileName={fileName}";
                var response = client.DeleteAsync(url).Result;
                string json = await response.Content.ReadAsStringAsync();
                ResultMessage resultMessage = JsonConvert.DeserializeObject<ResultMessage>(json);
                return resultMessage;
            }
        }

        /// <summary>
        /// 获取可以更新文件信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<FileInformation>> GetNeedDownFiles()
        {
            List<FileDownloadDto> list = new List<FileDownloadDto>();
            string location = Assembly.GetEntryAssembly().Location;
            List<FileInformation> fileInformations = await GetWebFiles();
            string[] localPathArray = (from a in Directory.GetFiles(Path.GetDirectoryName(location))
                                       join b in fileInformations on Path.GetFileName(a) equals b.FileName
                                       select a).ToArray();

            for (int i = 0; i < localPathArray.Length; i++)
            {
                string itemPath = localPathArray[i];
                using (var fs = new FileStream(itemPath, FileMode.Open))
                {
                    list.Add(new FileDownloadDto
                    {
                        FileName = Path.GetFileName(itemPath),
                        FileMd5 = fs.GetFileMD5()
                    });
                }
            }

            var needDownInfos = (from a in fileInformations
                                 join b in list on a.FileName equals b.FileName
                                 where a.FileName == b.FileName && a.FileMd5 != b.FileMd5
                                 select a).
                                 Concat(
                                 from a in fileInformations
                                 where !(from b in list select b).Any(b => a.FileName == b.FileName)
                                 select a).ToList();
            return needDownInfos;
        }


        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public async Task<ResultMessage> DownFile(string fileName)
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

        /// <summary>
        ///   文件上传
        /// </summary>
        /// <param name="filePath">本地需要上传的文件路径</param>
        /// <returns></returns>
        public async Task<FileUpLoadDto> FileUpload(string filePath)
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

        public async Task<DataTable> GetWebFilesDataTable()
        {
            List<FileInformation> fileInformations = await GetWebFiles();
            if (!fileInformations.Any())
            {
                return null;
            }
            return fileInformations.ToDataTable("fileInformations");

        }

        public async Task<List<FileInformation>> GetWebFiles()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var url = ConfigurationManager.AppSettings["GetInfo"].ToString();
                var response = await httpClient.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                var fileInformations = JsonConvert.DeserializeObject<List<FileInformation>>(json);
                return fileInformations;
            }
        }
    }
}
