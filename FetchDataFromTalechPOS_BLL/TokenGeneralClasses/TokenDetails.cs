using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL.TokenGeneralClasses
{
    public class TokenDetails
    {

        //public string securityToken = string.Empty;
        public async Task<AuthenticationMethodResultModel> GetTalechAPI_Token()
        {
            AuthenticationMethodResultModel obj = new AuthenticationMethodResultModel();

            TalechAccountDetails objdetails = new TalechAccountDetails
            {
                refresh_token = "191072574/5kHP9zQJ8mADp8oSG15bw6x2",
                client_secret = "49B9GDPU",
                client_id = "rserasiya@gmail.com",
                grant_type = "refresh_token"
            };

            string jsonString = JsonConvert.SerializeObject(objdetails);
            JObject objInputParameters = JObject.Parse(jsonString);


            using (HttpClient client = new HttpClient())
            {
                SetHTTPClientObjectValues(client);
                HttpResponseMessage response = await ExecuteClientPostMethod("o/oauth2/token", objInputParameters, client);

                LogHelper.Log("Refresh Token Method Response code: " + response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    obj = await response.Content.ReadAsAsync<AuthenticationMethodResultModel>();
                }
            }

            return obj;
        }

        public bool SetRefreshToken(HttpResponseMessage response)
        {
            HttpHeaders headers = response.Headers;
            IEnumerable<string> values;
            if (headers.TryGetValues("X-POS-SecurityToken", out values))
            {
                Token.securityToken = values.First();
            }

            return true;
        }

        public async Task<HttpResponseMessage> ExecuteClientPostMethod(string uri, JObject inputParameters, HttpClient client, string securityToken = default(string),
            string merchantID = default(string))
        {

            if (!string.IsNullOrEmpty(securityToken))
                client.DefaultRequestHeaders.Add("securityToken", securityToken);
            if (!string.IsNullOrEmpty(merchantID))
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-POS-MerchantId", merchantID.ToString());

            HttpResponseMessage response = await client.PostAsJsonAsync(uri, inputParameters);

            return response;
        }

        public bool SetHTTPClientObjectValues(HttpClient client)
        {
            //client = new HttpClient();
            client.BaseAddress = new Uri("https://mapi.talech.com​/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return true;
        }

        public FileInfo GetFileInfo(string dirPath, string file, bool deleteIfExists = true)
        {
            var fi = new FileInfo(dirPath + Path.DirectorySeparatorChar + file);
            if (deleteIfExists && fi.Exists)
            {
                fi.Delete();  // ensures we create a new workbook
            }
            return fi;
        }
    }
}
