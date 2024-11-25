// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.WM
// <3

using System.Collections.Generic;

#nullable disable
namespace kaka
{
  public class WM
  {
    public string username;
    public string avatar_url;
    public string content = "";
    public List<WebhookEmbed> embeds = new List<WebhookEmbed>();

    public bool tts { get; set; }

    public WM WithEmbed(WebhookEmbed embed)
    {
      this.embeds.Add(embed);
      return this;
    }

    public WebhookEmbed PassEmbed()
    {
      WebhookEmbed webhookEmbed = new WebhookEmbed(this);
      this.embeds.Add(webhookEmbed);
      return webhookEmbed;
    }

    public WM WithUsername(string un)
    {
      this.username = un;
      return this;
    }

    public WM WithAvatar(string avatar)
    {
      this.avatar_url = avatar;
      return this;
    }

    public WM WithContent(string c)
    {
      this.content = c;
      return this;
    }

    public WM WithTTS()
    {
      this.tts = true;
      return this;
    }
  }
}
