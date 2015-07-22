/*
 * author:  Jayson Ragasa
 * Date:    July 22, 2015
 */

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CloudEmployee.BAL.Net
{
    public class HttpClientEx
    {
        public static async Task<string> GetResponseByBasicAuth(string uri, string user, string pass)
        {
            string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(user + ":" + pass));

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            HttpResponseMessage response = await client.GetAsync(uri);
            HttpContent content = response.Content;

            // ... Check Status Code                                
            Debug.WriteLine("Response StatusCode: " + (int)response.StatusCode);

            // ... Read the string.
            string result = await content.ReadAsStringAsync();

            // ... Display the result.
            if (result != null &&
            result.Length >= 50)
            {
                Debug.WriteLine(result.Substring(0, 50) + "...");
            }

            return result;
        }
    }
}
