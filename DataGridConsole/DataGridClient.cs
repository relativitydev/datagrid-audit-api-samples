using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataGridConsole
{
    /// <summary>
    /// This class authorizes and performs requests. Wraps HttpClient.
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

        private HttpClient _httpClient;

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
        /// <param name="url"></param>
        public void Get(string url)
        {
            HttpResponseMessage response = _httpClient.GetAsync(url).Result;
            string resultAsStr = response.Content.ReadAsStringAsync().Result;
            
        }
        

        #endregion
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
    }
}
