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
        private const string API_BASE = "https://api.lymbix.com/";
        private const string TONALIZE_MULTIPLE = "tonalize_multiple";
        private const string TONALIZE_DETAILED = "tonalize_detailed";
        private const string TONALIZE = "tonalize";
        private const string FLAG_RESPONSE = "flag_response";
        private const string DEFAULT_VERSION = "2.3";

        private string authenticationKey = null;

        /// <param name="authenticationKey">your Lymbix authentication key</param>
        public ToneAPI(string authenticationKey)
        {
            if (String.IsNullOrEmpty(authenticationKey))
            {
                throw new ArgumentException("You must include your authentication key", "authenticationKey");
            }

            this.authenticationKey = authenticationKey;
        }

        /* utility functions */

        private static string post(string url, string data, List<string> headers)
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
            headers.Add(String.Format("Version: {0}", DEFAULT_VERSION));
            return headers;
        }

        /* api methods */

        public ArticleInfo[] TonalizeMultiple(List<string> articles)
        {
            return TonalizeMultiple(articles, null);
        }

        /// <summary>
        /// tonalize multiple articles
        /// </summary>
        /// <param name="articles">articles to tonalize</param>
        /// <param name="options">additional parameters (reference_ids and return_fields)</param>
        /// <returns>see the api documentation for the format of this object</returns>
        public ArticleInfo[] TonalizeMultiple(List<string> articles, Dictionary<string, string> options)
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
            string response = post(url, data, headers);
            return JsonConvert.DeserializeObject<ArticleInfo[]>(response);
        }

        public ArticleInfo TonalizeDetailed(string article)
        {
            return TonalizeDetailed(article, null);
        }

        /// <summary>
        /// tonalize an article
        /// </summary>
        /// <param name="articles">article to tonalize</param>
        /// <param name="options">additional parameters (reference_id and return_fields)</param>
        /// <returns>see the api documentation for the format of this object</returns>
        public ArticleInfo TonalizeDetailed(string article, Dictionary<string, string> options )
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
            string response = post(url, data, headers);
            return JsonConvert.DeserializeObject<ArticleInfo>(response);
        }

        public ArticleInfo Tonalize(string article)
        {
            return Tonalize(article, null);
        }

        /// <summary>
        /// tonalize an article
        /// </summary>
        /// <param name="articles">article to tonalize</param>
        /// <param name="options">additional parameters (reference_id and return_fields)</param>
        /// <returns>see the api documentation for the format of this object</returns>
        public ArticleInfo Tonalize(string article, Dictionary<string, string> options)
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
            string response = post(url, data, headers);
            return JsonConvert.DeserializeObject<ArticleInfo>(response);
        }

        public string FlagResponse(string phrase, string apiMethod)
        {
            return FlagResponse(phrase, apiMethod, DEFAULT_VERSION, "", null);
        }

        /// <summary>
        /// flag a response as inaccurate
        /// </summary>
        /// <param name="phrase">the phrase that returns an inaccurate response</param>
        /// <param name="apiMethod">the method that returns an inaccurate response</param>
        /// <param name="apiVersion">the version that returns an inaccurate response</param>
        /// <param name="callbackUrl">a url to call when the phrase has been re-rated</param>
        /// <param name="options">additional parameters (reference_id)</param>
        /// <returns>see the api documentation for this method's response</returns>
        public string FlagResponse(string phrase, string apiMethod,
            string apiVersion, string callbackUrl, Dictionary<string, string> options)
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
            string response = post(url, data, headers);
            return response;
        }
    }
}
