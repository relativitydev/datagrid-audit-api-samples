using System;
using System.Net.Http;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;

namespace DataGridConsole
{
    /// <summary>
    /// This class authorizes and performs requests. Wraps HttpClient to create generic HTTP methods.
    /// </summary>
    public class RelativityHttpClient
    {
        #region Members

        /// <summary>
        /// Base URL of the instance
        /// e.g. http://relativity-instance.com
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Username = email address
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Returns the timeout value
        /// </summary>
        public TimeSpan Timeout => _httpClient.Timeout;


        /// <summary>
        /// Password
        /// </summary>
        private string _password;

        /// <summary>
        /// Our instance of HttpClient that performs the actual requests
        /// </summary>
        private readonly HttpClient _httpClient;

        #endregion


        /// <summary>
        /// Creates a new instance of the DataGridClient object
        /// </summary>
        /// <param name="url">The base URL of our Relativity instance (e.g. https://relativity0=-instance.com)</param>
        /// <param name="username">The email address of our user</param>
        /// <param name="password">The user's password</param>
        /// <param name="timeoutInSeconds">Number of seconds before request should time out. 60 seconds by default</param>
        public RelativityHttpClient(string url, string username, string password, int timeoutInSeconds = 60)
        {
            Url = url;
            Username = username;
            _password = password;

            // set up HttpClient
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(Url),
                Timeout = new TimeSpan(0, 0, 0, timeoutInSeconds)
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
        /// Wraps HttpClient.DeleteAsync()
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
            throw new HttpException($"Error occured. Status Code: {(int)response.StatusCode} ({response.StatusCode})");
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
