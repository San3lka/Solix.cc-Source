// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.Spy
// <3

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace kaka
{
  [Component]
  public class Spy : MonoBehaviour
  {
    public static List<Spy> Notifications = new List<Spy>();
    private long ExpireTime;

    private void OnGUI()
    {
      if (SpyUtilities.BeingSpied)
        return;
      foreach (Spy notification in Spy.Notifications)
        notification.Run();
    }

    public void Run()
    {
      GUI.skin = Asset.Skin;
      if (DateTimeOffset.Now.ToUnixTimeMilliseconds() > this.ExpireTime)
      {
        Spy.Notifications.Remove(this);
      }
      else
      {
        GUI.color = new Color(1f, 1f, 1f, 0.0f);
        GUI.color = Color.white;
      }
    }

    private void Draw(int windowID)
    {
      GUILayout.Space(0.0f);
      ImageConversion.LoadImage(new Texture2D(345, 355, (TextureFormat) 3, false), SpyUtilities.data, true);
      GUI.DragWindow();
    }

    public Spy() => this.ExpireTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 2000L;

    public static void AddNotification()
    {
      Spy spy = new Spy();
      Spy.Notifications.Add(spy);
    }
  }
}
