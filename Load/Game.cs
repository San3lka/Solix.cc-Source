// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.Game
// <3

using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace kaka
{
  public class Game
  {
    public Uri WebhookURL;

    public Game(string webhookURL) => this.WebhookURL = new Uri(webhookURL);

    public void PostMessage(WM message)
    {
      HttpWebRequest http = WebRequest.CreateHttp(this.WebhookURL);
      http.Method = "POST";
      http.ContentType = "application/json";
      byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object) message));
      http.ContentLength = (long) bytes.Length;
      using (Stream requestStream = http.GetRequestStream())
      {
        requestStream.Write(bytes, 0, bytes.Length);
        requestStream.Flush();
      }
      HttpWebResponse response = (HttpWebResponse) http.GetResponse();
    }

    public async Task PMA(WM message)
    {
      HttpWebRequest request = WebRequest.CreateHttp(this.WebhookURL);
      request.Method = "POST";
      request.ContentType = "application/json";
      byte[] Buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object) message));
      request.ContentLength = (long) Buffer.Length;
      using (Stream write = await request.GetRequestStreamAsync())
      {
        await write.WriteAsync(Buffer, 0, Buffer.Length);
        await write.FlushAsync();
      }
      HttpWebResponse responseAsync = (HttpWebResponse) await request.GetResponseAsync();
      request = (HttpWebRequest) null;
      Buffer = (byte[]) null;
    }
  }
}
