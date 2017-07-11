using RevStack.Payment;
using RevStack.Payment.Context;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System;
using System.Linq;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using System.IO;

namespace RevStack.QuickBooksOnline.Context
{
    public class QuickBooksOnlineContext : PaymentGatewayContext
    {
        private const string OAuthVersion = "1.0";
        private const string PRODUCTION_URL = "https://api.intuit.com";
        private const string SANDBOX_URL = "https://sandbox.api.intuit.com";

        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
        public string CallbackUrl { get; set; }
        public string RealmId { get; set; }
        public string MinorVersion { get; set; } = "4";
        public string Currency { get; set; }
        
        private QuickBooksOnlineContext() { }

        public QuickBooksOnlineContext(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret, string callbackUrl, ServiceMode serviceMode, string realmId)
            : this(consumerKey, consumerSecret, accessToken, accessTokenSecret, callbackUrl, serviceMode, "USD", realmId, "4") { }

        public QuickBooksOnlineContext(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret, string callbackUrl, ServiceMode serviceMode, string realmId, string minorVersion)
            : this(consumerKey, consumerSecret, accessToken, accessTokenSecret, callbackUrl, serviceMode, "USD", realmId, minorVersion) { }

        public QuickBooksOnlineContext(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret, string callbackUrl, ServiceMode serviceMode, string currency, string realmId, string minorVersion)
        {
            ServiceMode = serviceMode;
            Url = ServiceMode == ServiceMode.Test ? SANDBOX_URL : PRODUCTION_URL;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;
            CallbackUrl = callbackUrl;
            RealmId = realmId;
            MinorVersion = minorVersion;
            Currency = currency;
        }

        public string Url { get; set; }
        
        public RestResponse SendRequest(string url, string method, string body, Dictionary<string, string> additionalParams, string appToken)
        {
            RestResponse rv = new RestResponse
            {
                Headers = new Dictionary<string, string>(),
                Body = string.Empty
            };

            HttpWebRequest request = null;
            HttpWebResponse response = null;

            try
            {
                var queryParams = additionalParams ?? new Dictionary<string, string>();
                
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                request.KeepAlive = false;
                
                request.Headers.Add("Request-Id", Guid.NewGuid().ToString());

                if (!string.IsNullOrEmpty(body))
                {
                    request.Accept = "application/json";
                    request.ContentType = "application/json";

                    var bytes = Encoding.UTF8.GetBytes(body);
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }

                request.Headers.Add("Authorization", GetDevDefinedOAuthHeader(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret, request, body));
                
                string content = "";

                response = request.GetResponse() as HttpWebResponse;
                using (Stream data = response.GetResponseStream())
                {
                    content = new StreamReader(data).ReadToEnd();
                }

                rv.StatusCode = (int)((HttpWebResponse)response).StatusCode;
                rv.StatusString = ((HttpWebResponse)response).StatusDescription;
                rv.ContentLength = response.ContentLength;
                rv.ContentType = response.ContentType;
                rv.Body = content;
                response.Headers.AllKeys.ToList().ForEach(o => rv.Headers.Add(o, response.Headers[o]));

            }
            catch (WebException ex)
              {
                rv.Exception = ex;
                if (ex.Response != null)
                {
                    rv.StatusCode = (int)((HttpWebResponse)ex.Response).StatusCode;
                    rv.StatusString = ((HttpWebResponse)ex.Response).StatusDescription;
                    response = (HttpWebResponse)ex.Response;
                }
            }
            catch (Exception ex)
            {
                rv.Exception = ex;
            }

            return rv;
        }

        private string GetDevDefinedOAuthHeader(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret, HttpWebRequest webRequest, string requestBody)
        {

            OAuthConsumerContext consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = consumerKey,
                SignatureMethod = SignatureMethod.HmacSha1,
                ConsumerSecret = consumerSecret,
                UseHeaderForOAuthParameters = true
            };

            //We already have OAuth tokens, so OAuth URIs below are not used - set to example.com
            OAuthSession oSession = new OAuthSession(consumerContext, "https://www.example.com",
                                    "https://www.example.com",
                                    "https://www.example.com");

            oSession.AccessToken = new TokenBase
            {
                Token = accessToken,
                ConsumerKey = consumerKey,
                TokenSecret = accessTokenSecret
            };

            IConsumerRequest consumerRequest = oSession.Request();
            consumerRequest = ConsumerRequestExtensions.ForMethod(consumerRequest, webRequest.Method);
            if (!string.IsNullOrEmpty(requestBody))
            {
                consumerRequest = consumerRequest.Post().WithRawContentType(webRequest.ContentType).WithRawContent(System.Text.Encoding.ASCII.GetBytes(requestBody));
            }
            else
            {
                consumerRequest = consumerRequest.Get();
            }

            consumerRequest = ConsumerRequestExtensions.ForUri(consumerRequest, webRequest.RequestUri);
            consumerRequest = consumerRequest.SignWithToken();
            return consumerRequest.Context.GenerateOAuthParametersForHeader();
        }
    }
}

