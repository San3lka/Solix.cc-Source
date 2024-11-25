// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.Main
// <3

using Load;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace kaka
{
  [Component]
  public class Main : MonoBehaviour
  {
    public static bool MenuOpen = true;
    public static Main.MenuTab SelectedTab = Main.MenuTab.Aimbot;
    public static Rect WindowRect = new Rect(80f, 80f, 1010f, 645f);
    public static List<GUIContent> SettingsButtons = new List<GUIContent>();
    public static List<GUIContent> SkinButtons = new List<GUIContent>();
    public static List<GUIContent> VisualButtons = new List<GUIContent>();
    public static List<GUIContent> MenuButtons = new List<GUIContent>();
    public static Rect CursorPos = new Rect(0.0f, 0.0f, 20f, 20f);
    public static Texture _cursorTexture;
    public static Color GUIColor;

    public void OnGUI()
    {
      if (SpyUtilities.BeingSpied)
        return;
      GUI.skin = Asset.Skin;
      foreach (NotificationWindow notification in NotificationWindow.Notifications)
        notification.Run();
      if (!Main.MenuOpen)
        return;
      if (Object.op_Equality((Object) Main._cursorTexture, (Object) null))
        Main._cursorTexture = (Texture) Asset.Textures["Imleç"];
      GUI.Label(new Rect(((Rect) ref Main.WindowRect).x + 80f, ((Rect) ref Main.WindowRect).y + 122f, 32f, 32f), (Texture) Asset.AimbotIcon);
      GUI.Label(new Rect(((Rect) ref Main.WindowRect).x + 80f, ((Rect) ref Main.WindowRect).y + 183f, 32f, 32f), (Texture) Asset.VisualIcon);
      GUI.Label(new Rect(((Rect) ref Main.WindowRect).x + 80f, ((Rect) ref Main.WindowRect).y + 245f, 32f, 32f), (Texture) Asset.MiscIcon);
      GUI.Label(new Rect(((Rect) ref Main.WindowRect).x + 80f, ((Rect) ref Main.WindowRect).y + 305f, 32f, 32f), (Texture) Asset.PlayerIcon);
      GUI.Label(new Rect(((Rect) ref Main.WindowRect).x + 80f, ((Rect) ref Main.WindowRect).y + 370f, 40f, 40f), (Texture) Asset.SkinIcon);
      GUI.Label(new Rect(((Rect) ref Main.WindowRect).x + 80f, ((Rect) ref Main.WindowRect).y + 430f, 32f, 32f), (Texture) Asset.SettingsIcon);
      GUI.Label(new Rect(((Rect) ref Main.WindowRect).x + 80f, ((Rect) ref Main.WindowRect).y + 493f, 32f, 32f), (Texture) Asset.KeyboardIcon);
      GUI.Label(new Rect(((Rect) ref Main.WindowRect).x + 130f, ((Rect) ref Main.WindowRect).y + 30f, 500f, 70f), "<size=30><b>Solix.cc</b></size>");
      GUI.Label(new Rect(((Rect) ref Main.WindowRect).x + 17f, ((Rect) ref Main.WindowRect).y - 11f, 120f, 120f), (Texture) Asset.DNLogo);
      GUI.Label(new Rect(((Rect) ref Main.WindowRect).x + 100f, ((Rect) ref Main.WindowRect).y + 572f, 150f, 100f), "<size=15><b>" + Loadc.Name + "</b></size>");
      if (Loadc.Gün > 200)
        GUI.Label(new Rect(((Rect) ref Main.WindowRect).x + 100f, ((Rect) ref Main.WindowRect).y + 598f, 150f, 100f), "<size=15><b>Unlimited</b></size>");
      else
        GUI.Label(new Rect(((Rect) ref Main.WindowRect).x + 100f, ((Rect) ref Main.WindowRect).y + 598f, 150f, 100f), string.Format("<size=15><b>{0} Day Left</b></size>", (object) Loadc.Gün));
      GUI.Label(new Rect(((Rect) ref Main.WindowRect).x + 37f, ((Rect) ref Main.WindowRect).y + 570f, 50f, 50f), (Texture) Asset.Battleye);
      GUI.depth = -1;
      GUIStyle guiStyle = new GUIStyle(GUIStyle.op_Implicit("label"))
      {
        margin = new RectOffset(10, 10, 5, 5),
        fontSize = 22
      };
      // ISSUE: method pointer
      Main.WindowRect = GUILayout.Window(1, Main.WindowRect, new GUI.WindowFunction((object) null, __methodptr(MW)), "", Array.Empty<GUILayoutOption>());
      GUI.depth = -2;
      ((Rect) ref Main.CursorPos).x = Input.mousePosition.x;
      ((Rect) ref Main.CursorPos).y = (float) Screen.height - Input.mousePosition.y;
      GUI.DrawTexture(Main.CursorPos, Main._cursorTexture);
      Cursor.lockState = (CursorLockMode) 0;
      if (PlayerUI.window != null)
        PlayerUI.window.showCursor = true;
      GUI.skin = (GUISkin) null;
    }

    public void Update()
    {
      if (Input.GetKeyDown(Hotkeys.GetKey("Menu")))
        Main.MenuOpen = !Main.MenuOpen;
      if (Input.GetKeyDown(Hotkeys.GetKey("Freecam")))
      {
        if (G.Settings.MiscOptions.Freecam = !G.Settings.MiscOptions.Freecam)
          NotificationWindow.AddNotification("FreeCam <b> ON</b>");
        else
          NotificationWindow.AddNotification("FreeCam <b> OFF</b>");
      }
      if (Input.GetKeyDown(Hotkeys.GetKey("VehicleFly")))
      {
        if (G.Settings.MiscOptions.VehicleFly = !G.Settings.MiscOptions.VehicleFly)
          NotificationWindow.AddNotification("VehicleFly <b> ON</b>");
        else
          NotificationWindow.AddNotification("VehicleFly <b> OFF</b>");
      }
      if (!Input.GetKeyDown(Hotkeys.GetKey("Disconnect")))
        return;
      Provider.disconnect();
      NotificationWindow.AddNotification("Disconnect");
    }

    public void Start()
    {
      foreach (Main.MenuTab menuTab in Enum.GetValues(typeof (Main.MenuTab)))
        Main.MenuButtons.Add(new GUIContent(Enum.GetName(typeof (Main.MenuTab), (object) menuTab)));
      foreach (Visual.ESPObject espObject in Enum.GetValues(typeof (Visual.ESPObject)))
        Main.VisualButtons.Add(new GUIContent(Enum.GetName(typeof (Visual.ESPObject), (object) espObject)));
      foreach (SettingsOptions settingsOptions in Enum.GetValues(typeof (SettingsOptions)))
        Main.SettingsButtons.Add(new GUIContent(Enum.GetName(typeof (SettingsOptions), (object) settingsOptions)));
      foreach (SkinsTab.SkinType skinType in Enum.GetValues(typeof (SkinsTab.SkinType)))
        Main.SkinButtons.Add(new GUIContent(Enum.GetName(typeof (SkinsTab.SkinType), (object) skinType)));
    }

    public static void MW(int windowID)
    {
      GUILayout.Space(0.0f);
      switch (Main.SelectedTab)
      {
        case Main.MenuTab.Aimbot:
          AimbotTab.Tab();
          break;
        case Main.MenuTab.Visuals:
          VisualsTab.Tab();
          break;
        case Main.MenuTab.Misc:
          MiscTab.Tab();
          break;
        case Main.MenuTab.Players:
          PlayersTab.Tab();
          break;
        case Main.MenuTab.Skins:
          SkinsTab.Tab();
          break;
        case Main.MenuTab.Settings:
          SettingsTab.Tab();
          break;
        case Main.MenuTab.Keybınds:
          HotkeyTab.Tab();
          break;
      }
      GUILayout.BeginArea(new Rect(35f, 108f, 260f, 500f));
      int fontSize = GUI.skin.button.fontSize;
      GUI.skin.button.fixedHeight = 58f;
      GUI.skin.button.fontSize = 20;
      Main.SelectedTab = (Main.MenuTab) GUILayout.SelectionGrid((int) Main.SelectedTab, Main.MenuButtons.ToArray(), 1, Array.Empty<GUILayoutOption>());
      GUI.skin.button.fixedHeight = 40f;
      GUI.skin.button.fontSize = fontSize;
      GUILayout.EndArea();
      GUI.DragWindow();
    }

    public enum MenuTab
    {
      Aimbot,
      Visuals,
      Misc,
      Players,
      Skins,
      Settings,
      Keybınds,
    }
  }
}
