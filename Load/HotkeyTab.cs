// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.HotkeyTab
// <3

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace kaka
{
  public static class HotkeyTab
  {
    private static bool waitingForKey = false;
    private static string currentHotkey = string.Empty;
    private static Vector2 scrollPosition1 = new Vector2(0.0f, 0.0f);

    public static void Tab()
    {
      GUILayout.BeginArea(new Rect(340f, 20f, 650f, 610f), "Hotkeys", GUIStyle.op_Implicit("box"));
      HotkeyTab.scrollPosition1 = GUILayout.BeginScrollView(HotkeyTab.scrollPosition1, Array.Empty<GUILayoutOption>());
      foreach (KeyValuePair<string, KeyCode> keyValuePair in G.Settings.GlobalOptions.Hotkeyd.ToList<KeyValuePair<string, KeyCode>>())
      {
        string str = !HotkeyTab.waitingForKey || !(HotkeyTab.currentHotkey == keyValuePair.Key) ? G.Settings.GlobalOptions.Hotkeyd[keyValuePair.Key].ToString() : "None";
        if (GUILayout.Button(keyValuePair.Key + " - <size=20>[" + str + "]</size>", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
        {
          HotkeyTab.waitingForKey = true;
          HotkeyTab.currentHotkey = keyValuePair.Key;
        }
      }
      if (HotkeyTab.waitingForKey)
      {
        Event current = Event.current;
        if (current.type == 4)
        {
          G.Settings.GlobalOptions.Hotkeyd[HotkeyTab.currentHotkey] = current.keyCode;
          HotkeyTab.waitingForKey = false;
        }
      }
      GUILayout.EndScrollView();
      GUILayout.EndArea();
    }
  }
}
