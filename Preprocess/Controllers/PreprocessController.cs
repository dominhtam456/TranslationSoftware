using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using NltkNet;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Preprocess.Controllers
{
    public class PreprocessController : ApiController
    {
        [HttpGet]
        [ActionName("readfile")]
        public async System.Threading.Tasks.Task<string> ReadFile(string projectName, string fileName)
        {
            byte[] byteArray = await GetFileAsync(fileName, projectName);
            MemoryStream memoryStream = new MemoryStream(byteArray,0,byteArray.Length,false,true);
            byte[] allBytes = memoryStream.GetBuffer();
            string data = Encoding.UTF8.GetString(allBytes);

            // string readText = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/" + "App_Data/") + projectName
            //     +"\\"+fileName);
            return data;
        }

        [HttpGet]
        [ActionName("getfile")]
        public async System.Threading.Tasks.Task<byte[]> GetFileAsync(string fileName, string projectName)
        {
            using (var client = new HttpClient())
            {
                // New code:
                client.BaseAddress = new Uri("https://localhost:44335/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

                HttpResponseMessage response = await client.GetAsync("api/project/getfile?fileName=" + fileName + "&projectName=" + projectName);
                if (response.IsSuccessStatusCode)
                {
                    byte[] byteArray = await response.Content.ReadAsByteArrayAsync();
                    return byteArray;
                }

            }
            return null;


        }

        [HttpGet]
        [ActionName("wordextract")]
        public async System.Threading.Tasks.Task<string> WordExtract(string projectName, string fileName)
        {
            string data = await ReadFile(projectName, fileName);

            Util.nltk.Init();

            JavaScriptSerializer jss = new JavaScriptSerializer();

            string output = jss.Serialize(Util.nltk.WordSeg(data));

            return output;
            
        }

    }
}