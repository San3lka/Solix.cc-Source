// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.Asset
// <3

using Load;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class Asset
  {
    public static Dictionary<string, Shader> Shaders = new Dictionary<string, Shader>();
    public static GUISkin Skin;
    public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
    public static Texture2D Lightening2;
    public static Texture2D Lightening;
    public static Texture2D WireFrame;
    public static Texture2D Farm;
    public static Texture2D Resources;
    public static Texture2D NPC;
    public static Texture2D AimbotIcon;
    public static Texture2D VisualIcon;
    public static Texture2D MiscIcon;
    public static Texture2D SettingsIcon;
    public static Texture2D SkinIcon;
    public static Texture2D KeyboardIcon;
    public static Texture2D DNLogo;
    public static Texture2D Battleye;
    public static Texture2D Player;
    public static Texture2D ChamsPlayer;
    public static Texture2D XrayPlayer;
    public static Texture2D Zombie;
    public static Texture2D Sentry;
    public static Texture2D Locker;
    public static Texture2D Yatak;
    public static Texture2D Item;
    public static Texture2D Airdrop;
    public static Texture2D Vehicle;
    public static Texture2D ClaimFlag;
    public static Texture2D PlayerIcon;

    public static void Get()
    {
      if (!Directory.Exists(Loadc.AppdatYol + "/Solix/CustomScreenShot"))
        Directory.CreateDirectory(Loadc.AppdatYol + "/Solix/CustomScreenShot");
      AssetBundle assetBundle = AssetBundle.LoadFromMemory(new WebClient().DownloadData(Loadc.AssetLink));
      if (Object.op_Implicit((Object) assetBundle))
        NotificationWindow.AddNotification("<b>[!]</b>    Assets Loaded");
      foreach (Shader loadAllAsset in assetBundle.LoadAllAssets<Shader>())
        Asset.Shaders.Add(((Object) loadAllAsset).name, loadAllAsset);
      foreach (Texture2D loadAllAsset in assetBundle.LoadAllAssets<Texture2D>())
      {
        if (((Object) loadAllAsset).name != "Font Texture")
          Asset.Textures.Add(((Object) loadAllAsset).name, loadAllAsset);
      }
      Asset.Lightening2 = Asset.Textures["shadertext"];
      Asset.Lightening = Asset.Textures["Lightening"];
      Asset.WireFrame = Asset.Textures["WireFrame"];
      Asset.Farm = Asset.Textures["Farm"];
      Asset.Resources = Asset.Textures["Resources"];
      Asset.NPC = Asset.Textures["NPC"];
      Asset.DNLogo = Asset.Textures["DNLogo"];
      Asset.AimbotIcon = Asset.Textures["AimbotIcon"];
      Asset.MiscIcon = Asset.Textures["Misc"];
      Asset.SettingsIcon = Asset.Textures["SettingIcon"];
      Asset.PlayerIcon = Asset.Textures["PlayerIcon"];
      Asset.VisualIcon = Asset.Textures["VisualIcon"];
      Asset.SkinIcon = Asset.Textures["SkinIcon"];
      Asset.KeyboardIcon = Asset.Textures["Keyboard"];
      Asset.DNLogo = Asset.Textures["DNLogo"];
      Asset.Battleye = Asset.Textures["Battleye"];
      Asset.Player = Asset.Textures["NormalPlayer"];
      Asset.ChamsPlayer = Asset.Textures["ChamsPlayer"];
      Asset.Zombie = Asset.Textures["Zombie"];
      Asset.Sentry = Asset.Textures["Sentry"];
      Asset.Yatak = Asset.Textures["Yatak"];
      Asset.Vehicle = Asset.Textures["Vehicle"];
      Asset.ClaimFlag = Asset.Textures["ClaimFlag"];
      Asset.Locker = Asset.Textures["Locker"];
      Asset.Item = Asset.Textures["Item"];
      Asset.Airdrop = Asset.Textures["Airdrop"];
      Asset.XrayPlayer = Asset.Textures["XrayPlayer"];
      Asset.Skin = ((IEnumerable<GUISkin>) assetBundle.LoadAllAssets<GUISkin>()).First<GUISkin>();
    }
  }
}
