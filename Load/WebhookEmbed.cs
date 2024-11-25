// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.WebhookEmbed
// <3

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable
namespace kaka
{
  public class WebhookEmbed
  {
    [JsonIgnore]
    private WM parent;
    public int color;
    public WebhookAuthor author;
    public string title;
    public string url;
    public string description;
    public List<WebhookField> fields = new List<WebhookField>();
    public WebhookImage image;
    public WebhookImage thumbnail;
    public WebhookFooter footer;
    public string timestamp;

    internal WebhookEmbed(WM parent) => this.parent = parent;

    public WebhookEmbed()
    {
    }

    public WM Finalize()
    {
      if (this.parent == null)
        this.parent = new WM()
        {
          embeds = new List<WebhookEmbed>() { this }
        };
      return this.parent;
    }

    public WebhookEmbed WithTitle(string title)
    {
      this.title = title;
      return this;
    }

    public WebhookEmbed WithURL(string value)
    {
      this.url = value;
      return this;
    }

    public WebhookEmbed WithDescription(string value)
    {
      this.description = value;
      return this;
    }

    public WebhookEmbed WithTimestamp(DateTime value)
    {
      this.timestamp = DiscordHelpers.DateTimeToISO(value.ToLocalTime());
      return this;
    }

    public WebhookEmbed WithField(string name, string value, bool inline = true)
    {
      this.fields.Add(new WebhookField()
      {
        value = value,
        inline = inline,
        name = name
      });
      return this;
    }

    public WebhookEmbed WithImage(string value)
    {
      this.image = new WebhookImage() { url = value };
      return this;
    }

    public WebhookEmbed WithThumbnail(string value)
    {
      this.thumbnail = new WebhookImage() { url = value };
      return this;
    }

    public WebhookEmbed WithAuthor(string name, string url = null, string icon = null)
    {
      this.author = new WebhookAuthor()
      {
        name = name,
        icon_url = icon,
        url = url
      };
      return this;
    }

    public WebhookEmbed WithColor(EmbedColor color)
    {
      this.color = BitConverter.ToInt32(new byte[4]
      {
        color.B,
        color.G,
        color.R,
        (byte) 0
      }, 0);
      return this;
    }

    private byte Clamp(float a) => (byte) Math.Round((double) a * (double) byte.MaxValue, 0);
  }
}
