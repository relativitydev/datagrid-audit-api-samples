using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace DataGridConsole
{
    /// <summary>
    /// This class authorizes and performs requests. Wraps HttpClient to create generic HTTP methods.
    /// </summary>
    public class DataGridClient
    {
        #region Members

        /// <summary>
        /// Username = email address
        /// </summary>
        public static string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        private static string _password;

        /// <summary>
        /// Base URL of the instance
        /// e.g. http://relativity-instance.com
        /// </summary>
        public static string Url { get; set; }

        private readonly HttpClient _httpClient;

        #endregion


        public DataGridClient(string url, string username, string password)
        {
            Url = url;
            Username = username;
            _password = password;

            // set up HttpClient
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(Url)
            };

            string basicAuthHeader = GenerateBasicAuthParam(Username, _password);
            _httpClient.DefaultRequestHeaders.Add("Authorization", basicAuthHeader);
            _httpClient.DefaultRequestHeaders.Add("X-CSRF-Header", "-");
        }

        #region Public methods
        /// <summary>
        /// Generic GET that wraps HttpClient.GetAsync()
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns>A JSON Object representing the response</returns>
        public JObject Get(string requestUri)
        {
            HttpResponseMessage response = _httpClient.GetAsync(requestUri).Result;
            return ReturnIfSuccess(response);
        }


        /// <summary>
        /// Wraps HttpClient.PostAsync()
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="payload"></param>
        /// <returns>A JSON Object representing the response</returns>
        public JObject Post(string requestUri, JObject payload)
        {
            StringContent content = ConvertToStringContent(payload);
            HttpResponseMessage response = _httpClient.PostAsync(requestUri, content).Result;
            return ReturnIfSuccess(response);
        }


        /// <summary>
        /// Wraps HttpClient.PutAsync()
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="payload"></param>
        /// <returns>A JSON Object representing the response</returns>
        public JObject Put(string requestUri, JObject payload)
        {
            StringContent content = ConvertToStringContent(payload);
            HttpResponseMessage response = _httpClient.PutAsync(requestUri, content).Result;
            return ReturnIfSuccess(response);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns>A JSON Object representing the response</returns>
        public JObject Delete(string requestUri)
        {
            HttpResponseMessage response = _httpClient.DeleteAsync(requestUri).Result;
            return ReturnIfSuccess(response);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Returns a JObject of the response if successful. Otherwise, throws an exception.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private JObject ReturnIfSuccess(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string resultAsStr = response.Content.ReadAsStringAsync().Result;
                JObject result = JObject.Parse(resultAsStr);
                return result;
            }
            throw new HttpException($"Error occured. Status Code: {response.StatusCode}");
        }

        /// <summary>
        /// Just converts the JObject into a form consumable by HttpClient
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        private static StringContent ConvertToStringContent(JObject payload)
        {
            string payloadAsStr = payload.ToString();
            StringContent content = new StringContent(payloadAsStr, Encoding.UTF8, "application/json");
            return content;
        }


        /// <summary>
        /// Generates the value for the Authentication header in a request in the form "Basic b64encode("username:password")".
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static string GenerateBasicAuthParam(string username, string password)
        {
            string unencoded = $"{username}:{password}";
            byte[] unencodedBytes = Encoding.ASCII.GetBytes((unencoded));
            string base64Creds = Convert.ToBase64String(unencodedBytes);

            return $"Basic {base64Creds}";
        }

        #endregion


    }
}
