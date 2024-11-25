// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.SettingsTab
// <3

using Load;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace kaka
{
  public static class SettingsTab
  {
    public static string playerNickname = "";
    public static string SelectedColorIdentifier = "";
    private static Vector2 scrollPosition4;
    public static SettingsOptions Select = SettingsOptions.Information;
    private static string textfield = "New Config";
    private static Vector2 scrollPosition2 = new Vector2(0.0f, 0.0f);

    public static InteractableItem FirstNotNull(this List<InteractableItem> items)
    {
      foreach (InteractableItem interactableItem in items)
      {
        if (Object.op_Inequality((Object) interactableItem, (Object) null))
          return interactableItem;
      }
      return (InteractableItem) null;
    }

    public static void Tab()
    {
      GUILayout.Space(0.0f);
      Rect rect1 = new Rect(335f, 10f, 660f, 90f);
      GUIStyle guiStyle1 = GUIStyle.op_Implicit("box");
      string str1 = string.Format("<b>{0}</b>", (object) SettingsTab.Select);
      GUIStyle guiStyle2 = guiStyle1;
      GUILayout.BeginArea(rect1, str1, guiStyle2);
      SettingsTab.Select = (SettingsOptions) GUI.Toolbar(new Rect(15f, 40f, 630f, 80f), (int) SettingsTab.Select, Main.SettingsButtons.ToArray(), GUIStyle.op_Implicit("NavBox"));
      GUILayout.EndArea();
      Rect rect2 = new Rect(335f, 105f, 350f, 530f);
      GUIStyle guiStyle3 = GUIStyle.op_Implicit("box");
      string str2 = string.Format("<b>{0}</b>", (object) SettingsTab.Select);
      GUIStyle guiStyle4 = guiStyle3;
      GUILayout.BeginArea(rect2, str2, guiStyle4);
      switch (SettingsTab.Select)
      {
        case SettingsOptions.Config:
          SettingsTab.textfield = GUILayout.TextField(SettingsTab.textfield, Array.Empty<GUILayoutOption>());
          if (GUILayout.Button("Create Config", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()) && !string.IsNullOrEmpty(SettingsTab.textfield))
          {
            ConfigUtilities.SaveConfig(SettingsTab.textfield, true);
            SettingsTab.textfield = "";
          }
          if (GUILayout.Button("Save Current Config", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
          {
            ConfigUtilities.SaveConfig(ConfigUtilities.SelectedConfig);
            break;
          }
          break;
        case SettingsOptions.Colors:
          if (G.Settings.GlobalOptions.GlobalColors.Count > 0)
          {
            SettingsTab.scrollPosition4 = GUILayout.BeginScrollView(SettingsTab.scrollPosition4, Array.Empty<GUILayoutOption>());
            List<string> stringList = new List<string>((IEnumerable<string>) G.Settings.GlobalOptions.GlobalColors.Keys);
            for (int index = 0; index < stringList.Count; ++index)
            {
              string identifier = stringList[index];
              if (GUILayout.Button("<color=#" + C.ColorToHex(C.GetColor(identifier)) + ">" + identifier.Replace("_", " ") + "</color>", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
                SettingsTab.SelectedColorIdentifier = identifier;
            }
            GUILayout.EndScrollView();
            break;
          }
          break;
        case SettingsOptions.Information:
          GUILayout.Label("<b>Status: Undetected</b>", Array.Empty<GUILayoutOption>());
          GUILayout.Label("<b>Version: " + Loadc.Version + "</b>", Array.Empty<GUILayoutOption>());
          GUILayout.Label("<b>Owner: chuckyware</b>1", Array.Empty<GUILayoutOption>());
          GUILayout.Label("<b>Owner: holyware</b>", Array.Empty<GUILayoutOption>());
          GUILayout.Label("<b>Discord: .gg/Pgwg69hsCN</b>", Array.Empty<GUILayoutOption>());
          GUILayout.Label("<b>Youtube: .com/UnturnedHack</b>", Array.Empty<GUILayoutOption>());
          if (VectorUtilities.ShouldRun() && Provider.CurrentServerAdvertisement.ip.ToString() != null)
          {
            GUILayout.TextField("Ip&Port: " + Parser.getIPFromUInt32(Provider.CurrentServerAdvertisement.ip) + ":" + Provider.CurrentServerAdvertisement.queryPort.ToString(), new GUILayoutOption[0]);
            break;
          }
          break;
      }
      GUILayout.EndArea();
      Rect rect3 = new Rect(695f, 105f, 300f, 530f);
      GUIStyle guiStyle5 = GUIStyle.op_Implicit("box");
      string str3 = string.Format("<b>{0}</b>", (object) SettingsTab.Select);
      GUIStyle guiStyle6 = guiStyle5;
      GUILayout.BeginArea(rect3, str3, guiStyle6);
      switch (SettingsTab.Select)
      {
        case SettingsOptions.Config:
          GUILayout.Space(5f);
          SettingsTab.scrollPosition2 = GUILayout.BeginScrollView(SettingsTab.scrollPosition2, Array.Empty<GUILayoutOption>());
          foreach (string config in ConfigUtilities.GetConfigs())
          {
            string name = config;
            if (name == ConfigUtilities.SelectedConfig)
              name = "<b>" + name + "</b>";
            if (GUILayout.Button(name, GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
            {
              ConfigUtilities.LoadConfig(name);
              SkinsUtilities.ApplyFromConfig();
            }
          }
          GUILayout.EndScrollView();
          break;
        case SettingsOptions.Colors:
          List<string> stringList1 = new List<string>((IEnumerable<string>) G.Settings.GlobalOptions.GlobalColors.Keys);
          for (int index = 0; index < stringList1.Count; ++index)
          {
            string str4 = stringList1[index];
            Color32 color1 = C.GetColor(str4);
            "<color=#" + C.ColorToHex(color1) + ">" + str4.Replace("_", " ") + "</color>";
            if (SettingsTab.SelectedColorIdentifier == str4)
            {
              GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
              Color32 color32_1 = color1;
              Color32 color32_2 = new Color32()
              {
                a = byte.MaxValue
              };
              GUILayout.Label("Color Preview", Array.Empty<GUILayoutOption>());
              GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
              GUIStyle guiStyle7 = new GUIStyle(GUI.skin.box);
              guiStyle7.normal.background = Texture2D.whiteTexture;
              Color color2 = GUI.color;
              GUI.color = Color32.op_Implicit(new Color32(color32_1.r, color32_1.g, color32_1.b, byte.MaxValue));
              GUILayout.Box(GUIContent.none, guiStyle7, new GUILayoutOption[2]
              {
                GUILayout.Width(140f),
                GUILayout.Height(75f)
              });
              GUI.color = Color.white;
              GUILayout.Box(GUIContent.none, guiStyle7, new GUILayoutOption[2]
              {
                GUILayout.Width(140f),
                GUILayout.Height(75f)
              });
              GUI.color = color2;
              GUILayout.EndHorizontal();
              GUILayout.Space(5f);
              GUILayout.Label("R: " + color32_1.r.ToString(), Array.Empty<GUILayoutOption>());
              color32_2.r = (byte) GUILayout.HorizontalSlider((float) color32_1.r, 0.0f, (float) byte.MaxValue, Array.Empty<GUILayoutOption>());
              GUILayout.Space(2f);
              GUILayout.Label("G: " + color32_1.g.ToString(), Array.Empty<GUILayoutOption>());
              color32_2.g = (byte) GUILayout.HorizontalSlider((float) color32_1.g, 0.0f, (float) byte.MaxValue, Array.Empty<GUILayoutOption>());
              GUILayout.Space(2f);
              GUILayout.Label("B: " + color32_1.b.ToString(), Array.Empty<GUILayoutOption>());
              color32_2.b = (byte) GUILayout.HorizontalSlider((float) color32_1.b, 0.0f, (float) byte.MaxValue, Array.Empty<GUILayoutOption>());
              G.Settings.GlobalOptions.GlobalColors[str4] = color32_2;
              GUILayout.EndVertical();
            }
          }
          break;
        case SettingsOptions.Information:
          GUILayout.Label("<b>Licence Owner Name : " + Loadc.Name + " </b> ", Array.Empty<GUILayoutOption>());
          if (Loadc.Gün > 200)
          {
            GUILayout.Label("<b>Expiry : Lifetime</b>", Array.Empty<GUILayoutOption>());
            break;
          }
          GUILayout.Label("<b>Expiry : " + Loadc.Gün.ToString() + "</b>", Array.Empty<GUILayoutOption>());
          break;
      }
      GUILayout.EndArea();
    }
  }
}
