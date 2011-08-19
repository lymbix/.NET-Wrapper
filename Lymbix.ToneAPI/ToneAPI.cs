using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Lymbix.ToneAPI.Models;
using Newtonsoft.Json;

namespace Lymbix.ToneAPI
{
    public class ToneAPI
    {
        private const string API_BASE = "https://gyrus.lymbix.com/";
        private const string TONALIZE_MULTIPLE = "tonalize_multiple";
        private const string TONALIZE_DETAILED = "tonalize_detailed";
        private const string TONALIZE = "tonalize";
        private const string FLAG_RESPONSE = "flag_response";

        private string authenticationKey = null;

        #region Constructors

        /// <summary>
        /// Constructor for the ToneAPI object
        /// </summary>
        /// <param name="authenticationKey">Your provided authentication key. You can get it at http://www.lymbix.com/account </param>
        public ToneAPI(string authenticationKey)
        {
            if (String.IsNullOrEmpty(authenticationKey))
            {
                throw new ArgumentException("You must include your authentication key", "authenticationKey");
            }

            this.authenticationKey = authenticationKey;
        }

        #endregion

        #region Utility Functions

        private static string Post(string url, string data, List<string> headers)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/x-www-form-urlencoded";

            if (headers != null)
            {
                foreach (string header in headers)
                {
                    httpRequest.Headers.Add(header);
                }
            }

            // write request
            byte[] postData = Encoding.UTF8.GetBytes(data.ToString());
            httpRequest.ContentLength = postData.Length;
            httpRequest.GetRequestStream().Write(postData, 0, postData.Length);

            // read response
            WebResponse webResponse = (WebResponse)httpRequest.GetResponse();
            StreamReader webResponseStream = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            return webResponseStream.ReadToEnd();
        }

        private List<string> getHeaders()
        {
            List<string> headers = new List<string>();
            headers.Add("Authentication: " + this.authenticationKey);
            headers.Add("Version: 2.2");
            return headers;
        }

        #endregion

        #region API Methods

        /// <summary>
        /// Tonalize multiple articles
        /// </summary>
        /// <param name="articles">Articles to tonalize</param>
        /// <param name="options">Additional parameters (reference_ids and return_fields)</param>
        /// <returns>See the api documentation for the format of this object</returns>
        public ArticleInfo[] TonalizeMultiple(List<string> articles, Dictionary<string, string> options = null)
        {
            if (articles == null)
            {
                throw new ArgumentException("You must include articles to tonalize", "articles");
            }

            string url = API_BASE + TONALIZE_MULTIPLE;
            string data = "articles=" + Uri.EscapeDataString(JsonConvert.SerializeObject(articles));

            if (options != null)
            {
                // return_fields & reference_ids
                foreach (KeyValuePair<string, string> option in options)
                {
                    data += "&" + Uri.EscapeDataString(option.Key) + "=" + Uri.EscapeDataString(option.Value);
                }
            }

            List<string> headers = getHeaders();
            string response = Post(url, data, headers);
            return JsonConvert.DeserializeObject<ArticleInfo[]>(response);
        }

        /// <summary>
        /// Tonalize an article
        /// </summary>
        /// <param name="articles">Article to tonalize</param>
        /// <param name="options">Additional parameters (reference_id and return_fields)</param>
        /// <returns>See the api documentation for the format of this object</returns>
        public ArticleInfo TonalizeDetailed(string article, Dictionary<string, string> options = null)
        {
            if (article == null)
            {
                throw new ArgumentException("You must include an article to tonalize", "article");
            }

            string url = API_BASE + TONALIZE_DETAILED;
            string data = "article=" + Uri.EscapeDataString(article);

            if (options != null)
            {
                foreach (KeyValuePair<string, string> option in options)
                {
                    data += "&" + Uri.EscapeDataString(option.Key) + "=" + Uri.EscapeDataString(option.Value);
                }
            }

            List<string> headers = getHeaders();
            string response = Post(url, data, headers);
            return JsonConvert.DeserializeObject<ArticleInfo>(response);
        }

        /// <summary>
        /// Tonalize an article
        /// </summary>
        /// <param name="articles">Article to tonalize</param>
        /// <param name="options">Additional parameters (reference_id and return_fields)</param>
        /// <returns>See the api documentation for the format of this object</returns>
        public ArticleInfo Tonalize(string article, Dictionary<string, string> options = null)
        {
            if (article == null)
            {
                throw new ArgumentException("You must include an article to tonalize", "article");
            }

            string url = API_BASE + TONALIZE;
            string data = "article=" + Uri.EscapeDataString(article);

            if (options != null)
            {
                foreach (KeyValuePair<string, string> option in options)
                {
                    data += "&" + Uri.EscapeDataString(option.Key) + "=" + Uri.EscapeDataString(option.Value);
                }
            }

            List<string> headers = getHeaders();
            string response = Post(url, data, headers);
            return JsonConvert.DeserializeObject<ArticleInfo>(response);
        }

        /// <summary>
        /// Flag a response as inaccurate
        /// </summary>
        /// <param name="phrase">The phrase that returns an inaccurate response</param>
        /// <param name="apiMethod">The method that returns an inaccurate response</param>
        /// <param name="apiVersion">The version that returns an inaccurate response</param>
        /// <param name="callbackUrl">A url to call when the phrase has been re-rated</param>
        /// <param name="options">Additional parameters (reference_id)</param>
        /// <returns>See the api documentation for this method's response</returns>
        public string FlagResponse(string phrase, string apiMethod = null,
            string apiVersion = "2.2", string callbackUrl = null, Dictionary<string, string> options = null)
        {
            if (String.IsNullOrEmpty(phrase))
            {
                throw new ArgumentException("You must include a phrase to flag", "phrase");
            }

            string url = API_BASE + FLAG_RESPONSE;
            string data = "phrase=" + Uri.EscapeDataString(phrase);

            if (!String.IsNullOrEmpty(apiMethod)) data += "&apiMethod=" + Uri.EscapeDataString(apiMethod);
            if (!String.IsNullOrEmpty(apiVersion)) data += "&apiVersion=" + Uri.EscapeDataString(apiVersion);
            if (!String.IsNullOrEmpty(callbackUrl)) data += "&callbackUrl=" + Uri.EscapeDataString(callbackUrl);

            if (options != null)
            {
                foreach (KeyValuePair<string, string> option in options)
                {
                    data += "&" + Uri.EscapeDataString(option.Key) + "=" + Uri.EscapeDataString(option.Value);
                }
            }

            List<string> headers = getHeaders();
            string response = Post(url, data, headers);
            return response;
        }

        #endregion

    }
}
