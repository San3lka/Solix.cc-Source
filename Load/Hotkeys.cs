// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.Hotkeys
// <3

using System;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class Hotkeys
  {
    public static void AddKey()
    {
      Hotkeys.AddKey("Menu", (KeyCode) 282);
      Hotkeys.AddKey("Freecam", (KeyCode) 283);
      Hotkeys.AddKey("Aimbot", (KeyCode) 102);
      Hotkeys.AddKey("Zoom", (KeyCode) 98);
      Hotkeys.AddKey("Disconnect", (KeyCode) 286);
      Hotkeys.AddKey("VehicleFly", (KeyCode) 301);
    }

    public static KeyCode GetKey(string identifier)
    {
      KeyCode keyCode;
      return G.Settings.GlobalOptions.Hotkeyd.TryGetValue(identifier, out keyCode) ? keyCode : (KeyCode) 0;
    }

    public static string HotkeyToHex(KeyCode color) => ((Enum) (object) color).ToString("X2");

    public static void AddKey(string id, KeyCode c)
    {
      if (G.Settings.GlobalOptions.Hotkeyd.ContainsKey(id))
        return;
      G.Settings.GlobalOptions.Hotkeyd.Add(id, c);
    }
  }
}
