// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.SkinsUtilities
// <3

using SDG.Provider;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unturned.SystemEx;
using Unturned.UnityEx;

#nullable disable
namespace kaka
{
  public class SkinsUtilities
  {
    public static Vector2 ScrollPos;
    public static string SearchString = "";
    private static Vector2 scrollPosition1 = new Vector2(0.0f, 0.0f);

    public static HumanClothes CharacterClothes => Player.player.clothing.characterClothes;

    public static HumanClothes FirstClothes => Player.player.clothing.firstClothes;

    public static HumanClothes ThirdClothes => Player.player.clothing.thirdClothes;

    public static void Apply(Skin skin, SkinsTab.SkinType skinType)
    {
      if (skinType == SkinsTab.SkinType.Weapons)
      {
        Dictionary<ushort, int> itemSkins = Player.player.channel.owner.itemSkins;
        if (itemSkins == null)
          return;
        ushort id = ((Asset) Player.player.equipment.asset).id;
        G.Settings.SkinOptions.SkinConfig.WeaponSkins.Clear();
        if (itemSkins.TryGetValue(id, out int _))
          itemSkins[id] = skin.ID;
        else
          itemSkins.Add(id, skin.ID);
        Player.player.equipment.applySkinVisual();
        Player.player.equipment.applyMythicVisual();
        foreach (KeyValuePair<ushort, int> keyValuePair in itemSkins)
          G.Settings.SkinOptions.SkinConfig.WeaponSkins.Add(new WeaponSave(keyValuePair.Key, keyValuePair.Value));
      }
      else
        SkinsUtilities.ApplyClothing(skin, skinType);
    }

    public static void ApplyClothing(Skin skin, SkinsTab.SkinType type)
    {
      if (SpyUtilities.BeingSpied)
        return;
      switch (type)
      {
        case SkinsTab.SkinType.Shirts:
          SkinsUtilities.CharacterClothes.visualShirt = skin.ID;
          SkinsUtilities.FirstClothes.visualShirt = skin.ID;
          SkinsUtilities.ThirdClothes.visualShirt = skin.ID;
          G.Settings.SkinOptions.SkinConfig.ShirtID = skin.ID;
          break;
        case SkinsTab.SkinType.Pants:
          SkinsUtilities.CharacterClothes.visualPants = skin.ID;
          SkinsUtilities.FirstClothes.visualPants = skin.ID;
          SkinsUtilities.ThirdClothes.visualPants = skin.ID;
          G.Settings.SkinOptions.SkinConfig.PantsID = skin.ID;
          break;
        case SkinsTab.SkinType.Bags:
          SkinsUtilities.CharacterClothes.visualBackpack = skin.ID;
          SkinsUtilities.FirstClothes.visualBackpack = skin.ID;
          SkinsUtilities.ThirdClothes.visualBackpack = skin.ID;
          G.Settings.SkinOptions.SkinConfig.BackpackID = skin.ID;
          break;
        case SkinsTab.SkinType.Vests:
          SkinsUtilities.CharacterClothes.visualVest = skin.ID;
          SkinsUtilities.FirstClothes.visualVest = skin.ID;
          SkinsUtilities.ThirdClothes.visualVest = skin.ID;
          G.Settings.SkinOptions.SkinConfig.VestID = skin.ID;
          break;
        case SkinsTab.SkinType.Hats:
          SkinsUtilities.CharacterClothes.visualHat = skin.ID;
          SkinsUtilities.FirstClothes.visualHat = skin.ID;
          SkinsUtilities.ThirdClothes.visualHat = skin.ID;
          G.Settings.SkinOptions.SkinConfig.HatID = skin.ID;
          break;
        case SkinsTab.SkinType.Masks:
          SkinsUtilities.CharacterClothes.visualMask = skin.ID;
          SkinsUtilities.FirstClothes.visualMask = skin.ID;
          SkinsUtilities.ThirdClothes.visualMask = skin.ID;
          G.Settings.SkinOptions.SkinConfig.MaskID = skin.ID;
          break;
        case SkinsTab.SkinType.Glasses:
          SkinsUtilities.CharacterClothes.visualGlasses = skin.ID;
          SkinsUtilities.FirstClothes.visualGlasses = skin.ID;
          SkinsUtilities.ThirdClothes.visualGlasses = skin.ID;
          G.Settings.SkinOptions.SkinConfig.GlassesID = skin.ID;
          break;
      }
      SkinsUtilities.CharacterClothes.apply();
      SkinsUtilities.FirstClothes.apply();
      SkinsUtilities.ThirdClothes.apply();
    }

    public static void ApplyFromConfig()
    {
      Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
      foreach (WeaponSave weaponSkin in G.Settings.SkinOptions.SkinConfig.WeaponSkins)
        dictionary[weaponSkin.WeaponID] = weaponSkin.SkinID;
      if (Object.op_Equality((Object) Player.player, (Object) null))
        return;
      Player.player.channel.owner.itemSkins = dictionary;
      if (G.Settings.SkinOptions.SkinConfig.ShirtID != 0)
      {
        SkinsUtilities.CharacterClothes.visualShirt = G.Settings.SkinOptions.SkinConfig.ShirtID;
        SkinsUtilities.FirstClothes.visualShirt = G.Settings.SkinOptions.SkinConfig.ShirtID;
        SkinsUtilities.ThirdClothes.visualShirt = G.Settings.SkinOptions.SkinConfig.ShirtID;
      }
      if (G.Settings.SkinOptions.SkinConfig.PantsID != 0)
      {
        SkinsUtilities.CharacterClothes.visualPants = G.Settings.SkinOptions.SkinConfig.PantsID;
        SkinsUtilities.FirstClothes.visualPants = G.Settings.SkinOptions.SkinConfig.PantsID;
        SkinsUtilities.ThirdClothes.visualPants = G.Settings.SkinOptions.SkinConfig.PantsID;
      }
      if (G.Settings.SkinOptions.SkinConfig.BackpackID != 0)
      {
        SkinsUtilities.CharacterClothes.visualBackpack = G.Settings.SkinOptions.SkinConfig.BackpackID;
        SkinsUtilities.FirstClothes.visualBackpack = G.Settings.SkinOptions.SkinConfig.BackpackID;
        SkinsUtilities.ThirdClothes.visualBackpack = G.Settings.SkinOptions.SkinConfig.BackpackID;
      }
      if (G.Settings.SkinOptions.SkinConfig.VestID != 0)
      {
        SkinsUtilities.CharacterClothes.visualVest = G.Settings.SkinOptions.SkinConfig.VestID;
        SkinsUtilities.FirstClothes.visualVest = G.Settings.SkinOptions.SkinConfig.VestID;
        SkinsUtilities.ThirdClothes.visualVest = G.Settings.SkinOptions.SkinConfig.VestID;
      }
      if (G.Settings.SkinOptions.SkinConfig.HatID != 0)
      {
        SkinsUtilities.CharacterClothes.visualHat = G.Settings.SkinOptions.SkinConfig.HatID;
        SkinsUtilities.FirstClothes.visualHat = G.Settings.SkinOptions.SkinConfig.HatID;
        SkinsUtilities.ThirdClothes.visualHat = G.Settings.SkinOptions.SkinConfig.HatID;
      }
      if (G.Settings.SkinOptions.SkinConfig.MaskID != 0)
      {
        SkinsUtilities.CharacterClothes.visualMask = G.Settings.SkinOptions.SkinConfig.MaskID;
        SkinsUtilities.FirstClothes.visualMask = G.Settings.SkinOptions.SkinConfig.MaskID;
        SkinsUtilities.ThirdClothes.visualMask = G.Settings.SkinOptions.SkinConfig.MaskID;
      }
      if (G.Settings.SkinOptions.SkinConfig.GlassesID != 0)
      {
        SkinsUtilities.CharacterClothes.visualGlasses = G.Settings.SkinOptions.SkinConfig.GlassesID;
        SkinsUtilities.FirstClothes.visualGlasses = G.Settings.SkinOptions.SkinConfig.GlassesID;
        SkinsUtilities.ThirdClothes.visualGlasses = G.Settings.SkinOptions.SkinConfig.GlassesID;
      }
      SkinsUtilities.CharacterClothes.apply();
      SkinsUtilities.FirstClothes.apply();
      SkinsUtilities.ThirdClothes.apply();
    }

    public static void DrawSkins(SkinOptionList OptionList)
    {
      SkinsUtilities.SearchString = GUILayout.TextField(SkinsUtilities.SearchString, Array.Empty<GUILayoutOption>());
      SkinsUtilities.scrollPosition1 = GUILayout.BeginScrollView(SkinsUtilities.scrollPosition1, Array.Empty<GUILayoutOption>());
      foreach (Skin skin in OptionList.Skins)
      {
        if (skin.Name.ToLower().Contains(SkinsUtilities.SearchString.ToLower()))
        {
          if (GUILayout.Button(skin.Name, GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
            SkinsUtilities.Apply(skin, OptionList.Type);
          GUILayout.Space(5f);
        }
      }
      GUILayout.EndScrollView();
    }

    public static void DrawSkins2(SkinOptionList OptionList)
    {
      SkinsUtilities.SearchString = GUILayout.TextField(SkinsUtilities.SearchString, Array.Empty<GUILayoutOption>());
      SkinsUtilities.scrollPosition1 = GUILayout.BeginScrollView(SkinsUtilities.scrollPosition1, Array.Empty<GUILayoutOption>());
      foreach (Skin skin in OptionList.Skins)
      {
        if (skin.Name.ToLower().Contains(Player.player.equipment.asset.itemName.ToLower()))
        {
          if (GUILayout.Button(skin.Name, GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
            SkinsUtilities.Apply(skin, OptionList.Type);
          GUILayout.Space(5f);
        }
      }
      GUILayout.EndScrollView();
    }

    public static Dictionary<int, UnturnedEconInfo> econInfo { get; private set; }

    [Initializer]
    public static void anan2()
    {
      string path = UnityPaths.ProjectDirectory == null ? PathEx.Join(UnturnedPaths.RootDirectory, "EconInfo.bin") : PathEx.Join(UnityPaths.ProjectDirectory, "Builds", "Shared_Release", "EconInfo.bin");
      SkinsUtilities.econInfo = new Dictionary<int, UnturnedEconInfo>();
      FileStream input = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
      SHA1Stream shA1Stream = new SHA1Stream((Stream) input);
      BinaryReader binaryReader = new BinaryReader((Stream) input);
      binaryReader.ReadInt32();
      int num = binaryReader.ReadInt32();
      for (int index = 0; index < num; ++index)
      {
        UnturnedEconInfo unturnedEconInfo = new UnturnedEconInfo();
        unturnedEconInfo.name = binaryReader.ReadString();
        unturnedEconInfo.display_type = binaryReader.ReadString();
        unturnedEconInfo.description = binaryReader.ReadString();
        unturnedEconInfo.name_color = binaryReader.ReadString();
        unturnedEconInfo.itemdefid = binaryReader.ReadInt32();
        unturnedEconInfo.marketable = binaryReader.ReadBoolean();
        unturnedEconInfo.scraps = binaryReader.ReadInt32();
        unturnedEconInfo.target_game_asset_guid = new Guid(binaryReader.ReadBytes(16));
        unturnedEconInfo.item_skin = binaryReader.ReadInt32();
        unturnedEconInfo.item_effect = binaryReader.ReadInt32();
        unturnedEconInfo.quality = (UnturnedEconInfo.EQuality) binaryReader.ReadInt32();
        unturnedEconInfo.econ_type = binaryReader.ReadInt32();
        SkinsUtilities.econInfo.Add(unturnedEconInfo.itemdefid, unturnedEconInfo);
      }
      SkinsUtilities.RefreshSkinIcons();
    }

    public static void RefreshSkinIcons()
    {
      if (G.Settings.SkinOptions.SkinWeapons.Skins.Count > 5)
        return;
      foreach (KeyValuePair<int, UnturnedEconInfo> keyValuePair in SkinsUtilities.econInfo)
      {
        UnturnedEconInfo unturnedEconInfo = keyValuePair.Value;
        if (unturnedEconInfo.display_type.Contains("Skin"))
          G.Settings.SkinOptions.SkinWeapons.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
        if (unturnedEconInfo.display_type.Contains("Shirt"))
          G.Settings.SkinOptions.SkinClothesShirts.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
        if (unturnedEconInfo.display_type.Contains("Pants"))
          G.Settings.SkinOptions.SkinClothesPants.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
        if (unturnedEconInfo.display_type.Contains("Backpack"))
          G.Settings.SkinOptions.SkinClothesBackpack.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
        if (unturnedEconInfo.display_type.Contains("Vest"))
          G.Settings.SkinOptions.SkinClothesVest.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
        if (unturnedEconInfo.display_type.Contains("Hat"))
          G.Settings.SkinOptions.SkinClothesHats.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
        if (unturnedEconInfo.display_type.Contains("Mask"))
          G.Settings.SkinOptions.SkinClothesMask.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
        if (unturnedEconInfo.display_type.Contains("Glass"))
          G.Settings.SkinOptions.SkinClothesGlasses.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
      }
    }
  }
}
