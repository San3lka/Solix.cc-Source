// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.SkinsTab
// <3

using SDG.Unturned;
using System;
using System.Linq;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class SkinsTab
  {
    public static string SelectedColorIdentifier = "";
    public static SkinsTab.SkinType Select = SkinsTab.SkinType.Weapons;

    public static void Tab()
    {
      GUILayout.Space(0.0f);
      GUILayout.BeginArea(new Rect(315f, 1f, 880f, 60f));
      int num1 = SkinsTab.Select < SkinsTab.SkinType.Hats ? (int) SkinsTab.Select : -1;
      int num2 = GUI.Toolbar(new Rect(25f, 5f, 650f, 25f), num1, Main.SkinButtons.Take<GUIContent>(5).ToArray<GUIContent>(), GUIStyle.op_Implicit("NavBox"));
      int num3 = SkinsTab.Select >= SkinsTab.SkinType.Hats ? (int) (SkinsTab.Select - 5) : -1;
      int num4 = GUI.Toolbar(new Rect(25f, 35f, 650f, 30f), num3, Main.SkinButtons.Skip<GUIContent>(5).ToArray<GUIContent>(), GUIStyle.op_Implicit("NavBox"));
      if (num2 != num1 && num2 != -1)
        SkinsTab.Select = (SkinsTab.SkinType) num2;
      else if (num4 != num3 && num4 != -1)
        SkinsTab.Select = (SkinsTab.SkinType) (num4 + 5);
      GUILayout.EndArea();
      Rect rect = new Rect(340f, 70f, 650f, 550f);
      GUIStyle guiStyle1 = GUIStyle.op_Implicit("box");
      string str = string.Format("<size=15>{0}</size>", (object) SkinsTab.Select);
      GUIStyle guiStyle2 = guiStyle1;
      GUILayout.BeginArea(rect, str, guiStyle2);
      switch (SkinsTab.Select)
      {
        case SkinsTab.SkinType.Weapons:
          if (Object.op_Inequality((Object) Player.player, (Object) null) && Player.player.equipment.asset != null)
          {
            SkinsUtilities.DrawSkins2(G.Settings.SkinOptions.SkinWeapons);
            break;
          }
          GUILayout.Label("Use For Join Any Server And Equip GUN", Array.Empty<GUILayoutOption>());
          break;
        case SkinsTab.SkinType.Shirts:
          SkinsUtilities.DrawSkins(G.Settings.SkinOptions.SkinClothesShirts);
          break;
        case SkinsTab.SkinType.Pants:
          SkinsUtilities.DrawSkins(G.Settings.SkinOptions.SkinClothesPants);
          break;
        case SkinsTab.SkinType.Bags:
          SkinsUtilities.DrawSkins(G.Settings.SkinOptions.SkinClothesBackpack);
          break;
        case SkinsTab.SkinType.Vests:
          SkinsUtilities.DrawSkins(G.Settings.SkinOptions.SkinClothesVest);
          break;
        case SkinsTab.SkinType.Hats:
          SkinsUtilities.DrawSkins(G.Settings.SkinOptions.SkinClothesHats);
          break;
        case SkinsTab.SkinType.Masks:
          SkinsUtilities.DrawSkins(G.Settings.SkinOptions.SkinClothesMask);
          break;
        case SkinsTab.SkinType.Glasses:
          SkinsUtilities.DrawSkins(G.Settings.SkinOptions.SkinClothesGlasses);
          break;
      }
      GUILayout.EndArea();
    }

    public enum SkinType
    {
      Weapons,
      Shirts,
      Pants,
      Bags,
      Vests,
      Hats,
      Masks,
      Glasses,
    }
  }
}
