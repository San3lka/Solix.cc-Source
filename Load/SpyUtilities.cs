// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.SpyUtilities
// <3

using Load;
using SDG.NetPak;
using SDG.NetTransport;
using SDG.Unturned;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace kaka
{
  [Component]
  public class SpyUtilities : MonoBehaviour
  {
    public static byte[] data;
    public static float LastSpy;
    public static bool BeingSpied;

    [Override(typeof (Player), "ReceiveTakeScreenshot", BindingFlags.Instance | BindingFlags.Public, 0)]
    public void OV_ReceiveTakeScreenshot() => this.StartCoroutine(SpyUtilities.TakeScreenshot());

    public static IEnumerator TakeScreenshot()
    {
      SteamChannel channel = Player.player.channel;
      switch (G.Settings.MiscOptions.AntiSpyMethod)
      {
        case 0:
          if (((double) Time.realtimeSinceStartup - (double) SpyUtilities.LastSpy < 0.5 ? 1 : (SpyUtilities.BeingSpied ? 1 : 0)) != 0)
          {
            yield break;
          }
          else
          {
            SpyUtilities.BeingSpied = true;
            SpyUtilities.LastSpy = Time.realtimeSinceStartup;
            yield return (object) new WaitForFixedUpdate();
            yield return (object) new WaitForEndOfFrame();
            G.Settings.MiscOptions.SpyNofity = true;
            G.Settings.MiscOptions.SpyPic = true;
            Texture2D texture2D1 = new Texture2D(Screen.width, Screen.height, (TextureFormat) 3, false);
            ((Object) texture2D1).name = "Screenshot_Raw";
            ((Object) texture2D1).hideFlags = (HideFlags) 61;
            Texture2D texture2D2 = texture2D1;
            texture2D2.ReadPixels(new Rect(0.0f, 0.0f, (float) Screen.width, (float) Screen.height), 0, 0, false);
            Texture2D texture2D3 = new Texture2D(640, 480, (TextureFormat) 3, false);
            ((Object) texture2D3).name = "Screenshot_Final";
            ((Object) texture2D3).hideFlags = (HideFlags) 61;
            Texture2D texture2D4 = texture2D3;
            Color[] pixels = texture2D2.GetPixels();
            Color[] colorArray = new Color[((Texture) texture2D4).width * ((Texture) texture2D4).height];
            float num1 = (float) ((Texture) texture2D2).width / (float) ((Texture) texture2D4).width;
            float num2 = (float) ((Texture) texture2D2).height / (float) ((Texture) texture2D4).height;
            for (int index1 = 0; index1 < ((Texture) texture2D4).height; ++index1)
            {
              int num3 = (int) ((double) index1 * (double) num2) * ((Texture) texture2D2).width;
              int num4 = index1 * ((Texture) texture2D4).width;
              for (int index2 = 0; index2 < ((Texture) texture2D4).width; ++index2)
              {
                int num5 = (int) ((double) index2 * (double) num1);
                colorArray[num4 + index2] = pixels[num3 + num5];
              }
            }
            texture2D4.SetPixels(colorArray);
            SpyUtilities.data = ImageConversion.EncodeToJPG(texture2D4, 33);
            if (SpyUtilities.data.Length < 30000)
            {
              if (Provider.isServer)
                SpyUtilities._HandleScreenshotData(SpyUtilities.data, channel);
              else
                ServerInstanceMethod.Get(typeof (Player), "ReceiveScreenshotRelay").Invoke(Player.player.GetNetId(), (ENetReliability) 0, (Action<NetPakWriter>) (writer =>
                {
                  ushort length = (ushort) SpyUtilities.data.Length;
                  SystemNetPakWriterEx.WriteUInt16(writer, length);
                  writer.WriteBytes(SpyUtilities.data, (int) length);
                }));
            }
            yield return (object) new WaitForFixedUpdate();
            yield return (object) new WaitForEndOfFrame();
            SpyUtilities.BeingSpied = false;
            break;
          }
        case 1:
          if (((double) Time.realtimeSinceStartup - (double) SpyUtilities.LastSpy < 0.5 ? 1 : (SpyUtilities.BeingSpied ? 1 : 0)) != 0)
          {
            yield break;
          }
          else
          {
            SpyUtilities.BeingSpied = true;
            SpyUtilities.LastSpy = Time.realtimeSinceStartup;
            yield return (object) new WaitForFixedUpdate();
            yield return (object) new WaitForEndOfFrame();
            G.Settings.MiscOptions.SpyNofity = true;
            G.Settings.MiscOptions.SpyPic = true;
            Random random = new Random();
            string[] files = Directory.GetFiles(Loadc.AppdatYol + "/Solix//CustomScreenShot/");
            byte[] numArray = File.ReadAllBytes(files[random.Next(files.Length)]);
            Texture2D texture2D5 = new Texture2D(2, 2);
            ImageConversion.LoadImage(texture2D5, numArray);
            Texture2D texture2D6 = new Texture2D(640, 480, (TextureFormat) 3, false);
            ((Object) texture2D6).name = "Screenshot_Final";
            ((Object) texture2D6).hideFlags = (HideFlags) 61;
            Texture2D texture2D7 = texture2D6;
            Color[] pixels = texture2D5.GetPixels();
            Color[] colorArray = new Color[((Texture) texture2D7).width * ((Texture) texture2D7).height];
            float num6 = (float) ((Texture) texture2D5).width / (float) ((Texture) texture2D7).width;
            float num7 = (float) ((Texture) texture2D5).height / (float) ((Texture) texture2D7).height;
            for (int index3 = 0; index3 < ((Texture) texture2D7).height; ++index3)
            {
              int num8 = (int) ((double) index3 * (double) num7) * ((Texture) texture2D5).width;
              int num9 = index3 * ((Texture) texture2D7).width;
              for (int index4 = 0; index4 < ((Texture) texture2D7).width; ++index4)
              {
                int num10 = (int) ((double) index4 * (double) num6);
                colorArray[num9 + index4] = pixels[num8 + num10];
              }
            }
            texture2D7.SetPixels(colorArray);
            SpyUtilities.data = ImageConversion.EncodeToJPG(texture2D7, 33);
            if (SpyUtilities.data.Length < 30000)
            {
              if (Provider.isServer)
                SpyUtilities._HandleScreenshotData(SpyUtilities.data, channel);
              else
                ServerInstanceMethod.Get(typeof (Player), "ReceiveScreenshotRelay").Invoke(Player.player.GetNetId(), (ENetReliability) 0, (Action<NetPakWriter>) (writer =>
                {
                  ushort length = (ushort) SpyUtilities.data.Length;
                  SystemNetPakWriterEx.WriteUInt16(writer, length);
                  writer.WriteBytes(SpyUtilities.data, (int) length);
                }));
            }
            yield return (object) new WaitForFixedUpdate();
            yield return (object) new WaitForEndOfFrame();
            SpyUtilities.BeingSpied = false;
            break;
          }
        case 3:
          if (((double) Time.realtimeSinceStartup - (double) SpyUtilities.LastSpy < 0.5 ? 1 : (SpyUtilities.BeingSpied ? 1 : 0)) != 0)
          {
            yield break;
          }
          else
          {
            SpyUtilities.LastSpy = Time.realtimeSinceStartup;
            yield return (object) new WaitForFixedUpdate();
            yield return (object) new WaitForEndOfFrame();
            G.Settings.MiscOptions.SpyNofity = true;
            G.Settings.MiscOptions.SpyPic = true;
            Texture2D texture2D8 = new Texture2D(Screen.width, Screen.height, (TextureFormat) 3, false);
            ((Object) texture2D8).name = "Screenshot_Raw";
            ((Object) texture2D8).hideFlags = (HideFlags) 61;
            Texture2D texture2D9 = texture2D8;
            texture2D9.ReadPixels(new Rect(0.0f, 0.0f, (float) Screen.width, (float) Screen.height), 0, 0, false);
            Texture2D texture2D10 = new Texture2D(640, 480, (TextureFormat) 3, false);
            ((Object) texture2D10).name = "Screenshot_Final";
            ((Object) texture2D10).hideFlags = (HideFlags) 61;
            Texture2D texture2D11 = texture2D10;
            Color[] pixels = texture2D9.GetPixels();
            Color[] colorArray = new Color[((Texture) texture2D11).width * ((Texture) texture2D11).height];
            float num11 = (float) ((Texture) texture2D9).width / (float) ((Texture) texture2D11).width;
            float num12 = (float) ((Texture) texture2D9).height / (float) ((Texture) texture2D11).height;
            for (int index5 = 0; index5 < ((Texture) texture2D11).height; ++index5)
            {
              int num13 = (int) ((double) index5 * (double) num12) * ((Texture) texture2D9).width;
              int num14 = index5 * ((Texture) texture2D11).width;
              for (int index6 = 0; index6 < ((Texture) texture2D11).width; ++index6)
              {
                int num15 = (int) ((double) index6 * (double) num11);
                colorArray[num14 + index6] = pixels[num13 + num15];
              }
            }
            texture2D11.SetPixels(colorArray);
            SpyUtilities.data = ImageConversion.EncodeToJPG(texture2D11, 33);
            if (SpyUtilities.data.Length < 30000)
            {
              if (Provider.isServer)
                SpyUtilities._HandleScreenshotData(SpyUtilities.data, channel);
              else
                ServerInstanceMethod.Get(typeof (Player), "ReceiveScreenshotRelay").Invoke(Player.player.GetNetId(), (ENetReliability) 0, (Action<NetPakWriter>) (writer =>
                {
                  ushort length = (ushort) SpyUtilities.data.Length;
                  SystemNetPakWriterEx.WriteUInt16(writer, length);
                  writer.WriteBytes(SpyUtilities.data, (int) length);
                }));
            }
            yield return (object) new WaitForFixedUpdate();
            yield return (object) new WaitForEndOfFrame();
            break;
          }
      }
      if (G.Settings.MiscOptions.AlertOnSpy)
        ((MonoBehaviour) Player.player).StartCoroutine(SpyUtilities.SpyIE());
      if (G.Settings.MiscOptions.ShowSpyPic)
        ((MonoBehaviour) Player.player).StartCoroutine(SpyUtilities.SpyPicIE());
    }

    public static void _HandleScreenshotData(byte[] data, SteamChannel channel)
    {
      if (Dedicator.isDedicated)
      {
        ReadWrite.writeBytes(ReadWrite.PATH + ServerSavedata.directory + "/" + Provider.serverID + "/Spy.jpg", false, false, data);
        ReadWrite.writeBytes(ReadWrite.PATH + ServerSavedata.directory + "/" + Provider.serverID + "/Spy/" + (object) channel.owner.playerID.steamID.m_SteamID + ".jpg", false, false, data);
        if (Player.player.onPlayerSpyReady != null)
          Player.player.onPlayerSpyReady.Invoke(channel.owner.playerID.steamID, data);
        new Queue<PlayerSpyReady>().Dequeue()?.Invoke(channel.owner.playerID.steamID, data);
      }
      else
      {
        ReadWrite.writeBytes("/Spy.jpg", false, true, data);
        if (Player.onSpyReady == null)
          return;
        Player.onSpyReady.Invoke(channel.owner.playerID.steamID, data);
      }
    }

    public static IEnumerator SpyPicIE()
    {
      float started = Time.realtimeSinceStartup;
      do
      {
        yield return (object) new WaitForEndOfFrame();
        if (G.Settings.MiscOptions.SpyPic && !SpyUtilities.BeingSpied)
        {
          Spy.AddNotification();
          G.Settings.MiscOptions.SpyPic = false;
        }
      }
      while ((double) Time.realtimeSinceStartup - (double) started <= 3.0);
    }

    public static IEnumerator SpyIE()
    {
      float started = Time.realtimeSinceStartup;
      do
      {
        yield return (object) new WaitForEndOfFrame();
        if (G.Settings.MiscOptions.SpyNofity && !SpyUtilities.BeingSpied)
        {
          NotificationWindow.AddNotification("<b>[!]</b> You got Spyed");
          G.Settings.MiscOptions.SpyNofity = false;
        }
      }
      while ((double) Time.realtimeSinceStartup - (double) started <= 3.0);
    }
  }
}
