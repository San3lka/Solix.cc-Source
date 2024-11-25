// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.NotificationWindow
// <3

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class NotificationWindow
  {
    public string info;
    private long ExpireTime;
    public static List<NotificationWindow> Notifications = new List<NotificationWindow>();
    public int NotificationNum = NotificationWindow.Notifications.Count + 1;

    public void Run()
    {
      GUI.skin = Asset.Skin;
      if (DateTimeOffset.Now.ToUnixTimeMilliseconds() > this.ExpireTime)
        NotificationWindow.Notifications.Remove(this);
      else if (100L > this.ExpireTime - DateTimeOffset.Now.ToUnixTimeMilliseconds())
      {
        long num = (this.ExpireTime - DateTimeOffset.Now.ToUnixTimeMilliseconds()) * 3L;
        // ISSUE: method pointer
        GUI.Window(this.NotificationNum + 10, new Rect((float) ((long) Screen.width - num), (float) (250 + 70 * this.NotificationNum), (float) num, 60f), new GUI.WindowFunction((object) this, __methodptr(Draw)), "", GUIStyle.op_Implicit("verticalsliderthumb"));
      }
      else
      {
        // ISSUE: method pointer
        GUI.Window(this.NotificationNum + 10, new Rect((float) (Screen.width - 250), (float) (250 + 70 * this.NotificationNum), 300f, 60f), new GUI.WindowFunction((object) this, __methodptr(Draw)), "", GUIStyle.op_Implicit("verticalsliderthumb"));
      }
    }

    private void Draw(int windowID)
    {
      GUI.Label(new Rect(25f, 25f, 260f, 70f), "<size=20>" + this.info + "</size>");
      GUI.DrawTexture(new Rect((float) (300.0 - (double) (this.ExpireTime - DateTimeOffset.Now.ToUnixTimeMilliseconds() - 100L) / 9900.0 * 300.0), 50f, 300f, 10f), (Texture) Asset.Skin.verticalScrollbar.normal.background, (ScaleMode) 0);
    }

    public NotificationWindow()
    {
      this.ExpireTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 3000L;
    }

    public static void AddNotification(string text)
    {
      NotificationWindow.Notifications.Add(new NotificationWindow()
      {
        info = text
      });
    }
  }
}
