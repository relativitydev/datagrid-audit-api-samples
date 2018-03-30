using System;
using System.Collections.Generic;
using System.IO;
using kCura.AuditUI2.Services.AuditLog;
using Newtonsoft.Json.Linq;
using Relativity.Services.ServiceProxy;


namespace DataGridAuditAPISamples
{
    class ConnectionHelper
    {
        /// <summary>
        /// Credentials read from a file
        /// </summary>
        private string _baseUrl;
        private string _user;
        private string _password;

        private Constants.Enums.AuthType _authType;

        /// <summary>
        /// Default working workspace artifact ID
        /// </summary>
        public static int DefaultWorkspaceId { get; } = 1035745;

        public string BaseRelativityUrl
        {
            get
            {
                string retVal = null;
                if (!String.IsNullOrEmpty(_baseUrl))
                {
                    // check if URL ends with a slash
                    if (_baseUrl[_baseUrl.Length - 1].Equals('/'))
                    {
                        retVal = _baseUrl + "Relativity";
                    }
                    else
                    {
                        retVal = _baseUrl + "/Relativity";
                    }
                }
                return retVal;
            }
        }

        public Uri ServicesUri => new Uri(BaseRelativityUrl + ".Services");

        public Uri RestUri => new Uri(BaseRelativityUrl + ".REST/api");


        /// <summary>
        /// Instantiate with path to credentials file in the format:
        /// InstanceUrl
        /// Relativity username
        /// Relativity password
        /// </summary>
        /// <param name="filePath"></param>
        public ConnectionHelper(string filePath)
        {
            try
            {
                string[] _creds = File.ReadAllLines(filePath);

                if (_creds.Length == 1)
                {
                    Console.WriteLine("Using IntegratedAuth");
                    _baseUrl = _creds[0];
                    _authType = Constants.Enums.AuthType.Integrated;
                }
                else if (_creds.Length == 3)
                {
                    _baseUrl = _creds[0];
                    _user = _creds[1];
                    _password = _creds[2];
                    Console.WriteLine("Using UsernamePassword");
                    _authType = Constants.Enums.AuthType.UsernamePassword;
                }

                else
                {
                    throw new ApplicationException($"File at {filePath} does not contain the correct number of lines.");
                }
            }

            catch (IOException)
            {
                Console.WriteLine("Specified credentials file not found.");
                throw;
            }
        }


        public IRSAPIClient GetRsapiClient(int workspaceId = -1)
        {
            switch (_authType)
            {
                case Constants.Enums.AuthType.Integrated:
                    return GetRsapiIntegrated(workspaceId);

                case Constants.Enums.AuthType.UsernamePassword:
                    IRSAPIClient proxy = new RSAPIClient(
                        ServicesUri,
                        new UsernamePasswordCredentials(_user, _password));
                    //proxy.APIOptions.WorkspaceID = DefaultWorkspaceId;
                    proxy.APIOptions.WorkspaceID = workspaceId;
                    proxy.Login();
                    return proxy;

                default:
                    return GetRsapiIntegrated(workspaceId);
            }
        }


        /// <summary>
        /// Gets the RSAPIClient with IntegratedAuth
        /// </summary>
        /// <param name="workspaceId"></param>
        /// <returns></returns>
        private IRSAPIClient GetRsapiIntegrated(int workspaceId = -1)
        {
            var creds = new kCura.Relativity.Client.IntegratedAuthCredentials();
            IRSAPIClient proxy = new RSAPIClient(
                ServicesUri,
                creds);
            proxy.APIOptions.WorkspaceID = workspaceId;
            proxy.Login();
            return proxy;
        }


        /// <summary>
        /// Gets the service factory with username/password
        /// </summary>
        /// <returns></returns>
        public ServiceFactory GetServiceFactory()
        {
            Credentials credentials;
            switch (_authType)
            {
                case Constants.Enums.AuthType.UsernamePassword:
                    credentials = new Relativity.Services.ServiceProxy.UsernamePasswordCredentials(_user, _password);
                    break;
                case Constants.Enums.AuthType.Integrated:
                    credentials = new Relativity.Services.ServiceProxy.IntegratedAuthCredentials();
                    break;
                default:
                    credentials = new Relativity.Services.ServiceProxy.IntegratedAuthCredentials();
                    break;
            }

            ServiceFactory factory
                = new ServiceFactory(new ServiceFactorySettings(ServicesUri, RestUri, credentials));
            return factory;
        }
    }
}
