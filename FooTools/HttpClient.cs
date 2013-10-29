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
