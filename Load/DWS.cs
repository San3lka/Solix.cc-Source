// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.DWS
// <3

using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

#nullable disable
namespace kaka
{
  public static class DWS
  {
    public static async void PMA(string WebhookURL, WM message)
    {
      HttpWebRequest request = WebRequest.CreateHttp(WebhookURL);
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
