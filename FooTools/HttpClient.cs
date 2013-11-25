using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace FooTools
{
    public class HttpClient
    {
        public string UserId = "";
        public string UserPass = "";

        public HttpClientResponse Post(string url, params object[] PostParams)
        {
            HttpClientResponse resp = new HttpClientResponse();

            try
            {
                Log.Debug("HTTP POST: " + url);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                if (UserId != "")
                    request.Credentials = new NetworkCredential(UserId, UserPass);
                byte[] data = null;
                // prepare the POST data
                if (PostParams.Length == 1)
                {
                    request.ContentType = "text/xml";
                    data = Encoding.UTF8.GetBytes((string)PostParams[0]);
                }
                else
                {
                    if (PostParams.Length % 2 != 0)
                        throw new Exception("Expected even number of entries in PostParams");

                    request.ContentType = "application/x-www-form-urlencoded";
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < PostParams.Length; i += 2)
                    {
                        builder.Append((string)PostParams[i] + "=");
                        builder.Append(Uri.EscapeDataString(PostParams[i + 1].ToString()));
                        if (i < PostParams.Length - 3)
                            builder.Append("&");
                    }
                    data = Encoding.UTF8.GetBytes(builder.ToString());
                }
                request.ContentLength = data.Length;

                // send out the data
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();


                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream InStream = response.GetResponseStream())
                    {
                        using (StreamReader InReader = new StreamReader(InStream, Encoding.UTF8))
                        {
                            resp.Content = InReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                resp.IsSuccess = false;
                resp.ErrorMessage = e.Message;
            }
            return resp;
        }

        public HttpClientResponse Get(string url)
        {
            HttpClientResponse resp = new HttpClientResponse();

            try
            {
                Log.Debug("HTTP GET: " + url);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream InStream = response.GetResponseStream())
                    {
                        using (StreamReader InReader = new StreamReader(InStream, Encoding.UTF8))
                        {
                            resp.Content = InReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                resp.IsSuccess = false;
                resp.ErrorMessage = e.Message;
            }
            return resp;
        }
    }
}
