// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.VisualsTab
// <3

using SDG.Unturned;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class VisualsTab
  {
    public static Visual.ESPObject SelectedObject = Visual.ESPObject.Player;
    private static Vector2 scrollPosition;
    private static Vector2 scrollPosition2;
    private static Vector2 scrollPosition3;
    public static VisualOptions SelectedOptions = G.Settings.PlayerOptions;

    public static void Tab()
    {
      GUILayout.Space(0.0f);
      GUILayout.BeginArea(new Rect(315f, 1f, 880f, 60f));
      int num1 = VisualsTab.SelectedObject < Visual.ESPObject.Vehicle ? (int) VisualsTab.SelectedObject : -1;
      int num2 = GUI.Toolbar(new Rect(25f, 5f, 650f, 25f), num1, Main.VisualButtons.Take<GUIContent>(6).ToArray<GUIContent>(), GUIStyle.op_Implicit("NavBox"));
      int num3 = VisualsTab.SelectedObject >= Visual.ESPObject.Vehicle ? (int) (VisualsTab.SelectedObject - 6) : -1;
      int num4 = GUI.Toolbar(new Rect(25f, 35f, 650f, 30f), num3, Main.VisualButtons.Skip<GUIContent>(6).ToArray<GUIContent>(), GUIStyle.op_Implicit("NavBox"));
      if (num2 != num1 && num2 != -1)
        VisualsTab.SelectedObject = (Visual.ESPObject) num2;
      else if (num4 != num3 && num4 != -1)
        VisualsTab.SelectedObject = (Visual.ESPObject) (num4 + 6);
      GUILayout.EndArea();
      GUILayout.BeginArea(new Rect(340f, 330f, 350f, 300f), "Options", GUIStyle.op_Implicit("box"));
      VisualsTab.scrollPosition = GUILayout.BeginScrollView(VisualsTab.scrollPosition, Array.Empty<GUILayoutOption>());
      switch (VisualsTab.SelectedObject)
      {
        case Visual.ESPObject.Player:
          VisualsTab.SelectedOptions = G.Settings.PlayerOptions;
          G.Settings.GlobalOptions.Weapon = GUILayout.Toggle(G.Settings.GlobalOptions.Weapon, "Show Weapon", Array.Empty<GUILayoutOption>());
          VisualsTab.DrawGlobals2(G.Settings.PlayerOptions);
          break;
        case Visual.ESPObject.Zombie:
          VisualsTab.SelectedOptions = G.Settings.ZombieOptions;
          VisualsTab.DrawGlobals2(G.Settings.ZombieOptions);
          break;
        case Visual.ESPObject.Item:
          VisualsTab.SelectedOptions = G.Settings.ItemOptions;
          OtherOptions globalOptions = G.Settings.GlobalOptions;
          globalOptions.filterItems = GUILayout.Toggle(globalOptions.filterItems, "Filter Item Whitelist", Array.Empty<GUILayoutOption>());
          if (globalOptions.filterItems)
          {
            globalOptions.allowGun = GUILayout.Toggle(globalOptions.allowGun, " Guns", Array.Empty<GUILayoutOption>());
            globalOptions.allowMelee = GUILayout.Toggle(globalOptions.allowMelee, " Melees", Array.Empty<GUILayoutOption>());
            globalOptions.allowBackpack = GUILayout.Toggle(globalOptions.allowBackpack, " Backpacks", Array.Empty<GUILayoutOption>());
            globalOptions.allowClothing = GUILayout.Toggle(globalOptions.allowClothing, " Clothing", Array.Empty<GUILayoutOption>());
            globalOptions.allowFuel = GUILayout.Toggle(globalOptions.allowFuel, " Fuel", Array.Empty<GUILayoutOption>());
            globalOptions.allowFoodWater = GUILayout.Toggle(globalOptions.allowFoodWater, " Food/Water", Array.Empty<GUILayoutOption>());
            globalOptions.allowAmmo = GUILayout.Toggle(globalOptions.allowAmmo, " Ammo", Array.Empty<GUILayoutOption>());
            globalOptions.allowMedical = GUILayout.Toggle(globalOptions.allowMedical, " Medical", Array.Empty<GUILayoutOption>());
            globalOptions.allowThrowable = GUILayout.Toggle(globalOptions.allowThrowable, " Throwables", Array.Empty<GUILayoutOption>());
            globalOptions.allowAttachments = GUILayout.Toggle(globalOptions.allowAttachments, " Attachments", Array.Empty<GUILayoutOption>());
          }
          VisualsTab.DrawGlobals2(G.Settings.ItemOptions);
          break;
        case Visual.ESPObject.Sentry:
          VisualsTab.SelectedOptions = G.Settings.SentryOptions;
          VisualsTab.DrawGlobals2(G.Settings.SentryOptions);
          break;
        case Visual.ESPObject.Bed:
          VisualsTab.SelectedOptions = G.Settings.BedOptions;
          G.Settings.GlobalOptions.Claimed = GUILayout.Toggle(G.Settings.GlobalOptions.Claimed, "Show Claimed State", Array.Empty<GUILayoutOption>());
          G.Settings.GlobalOptions.OnlyUnclaimed = GUILayout.Toggle(G.Settings.GlobalOptions.OnlyUnclaimed, "Only Display Unclaimed Beds", Array.Empty<GUILayoutOption>());
          VisualsTab.DrawGlobals2(G.Settings.BedOptions);
          break;
        case Visual.ESPObject.Flag:
          VisualsTab.SelectedOptions = G.Settings.FlagOptions;
          VisualsTab.DrawGlobals2(G.Settings.FlagOptions);
          break;
        case Visual.ESPObject.Vehicle:
          VisualsTab.SelectedOptions = G.Settings.VehicleOptions;
          G.Settings.GlobalOptions.VehicleLocked = GUILayout.Toggle(G.Settings.GlobalOptions.VehicleLocked, "Show Lock State", Array.Empty<GUILayoutOption>());
          G.Settings.GlobalOptions.OnlyUnlocked = GUILayout.Toggle(G.Settings.GlobalOptions.OnlyUnlocked, "Only Display Unlocked Vehicles", Array.Empty<GUILayoutOption>());
          VisualsTab.DrawGlobals2(G.Settings.VehicleOptions);
          break;
        case Visual.ESPObject.Storage:
          VisualsTab.SelectedOptions = G.Settings.StorageOptions;
          G.Settings.GlobalOptions.ShowLocked = GUILayout.Toggle(G.Settings.GlobalOptions.ShowLocked, "Show Lock State", Array.Empty<GUILayoutOption>());
          VisualsTab.DrawGlobals2(G.Settings.StorageOptions);
          break;
        case Visual.ESPObject.Airdrop:
          VisualsTab.SelectedOptions = G.Settings.AirdropOptions;
          VisualsTab.DrawGlobals2(G.Settings.AirdropOptions);
          break;
        case Visual.ESPObject.NPC:
          VisualsTab.SelectedOptions = G.Settings.NPCOptions;
          VisualsTab.DrawGlobals2(G.Settings.NPCOptions);
          break;
        case Visual.ESPObject.Farm:
          VisualsTab.SelectedOptions = G.Settings.FarmOptions;
          VisualsTab.DrawGlobals2(G.Settings.FarmOptions);
          break;
        case Visual.ESPObject.Resources:
          VisualsTab.SelectedOptions = G.Settings.ResourcesOptions;
          VisualsTab.DrawGlobals2(G.Settings.ResourcesOptions);
          break;
      }
      GUILayout.EndScrollView();
      GUILayout.EndArea();
      GUILayout.BeginArea(new Rect(700f, 430f, 290f, 200f), "Other", GUIStyle.op_Implicit("box"));
      GUILayout.Space(5f);
      VisualsTab.scrollPosition2 = GUILayout.BeginScrollView(VisualsTab.scrollPosition2, Array.Empty<GUILayoutOption>());
      if (GUILayout.Button("Set Graphic Settings(LOW)", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
      {
        GraphicsSettings.NormalizedFarClipDistance = 200f;
        GraphicsSettings.normalizedDrawDistance = 100f;
        GraphicsSettings.landmarkQuality = (EGraphicQuality) 0;
        GraphicsSettings.ragdolls = false;
        GraphicsSettings.debris = false;
        GraphicsSettings.isAmbientOcclusionEnabled = false;
        GraphicsSettings.bloom = false;
        GraphicsSettings.filmGrain = false;
        GraphicsSettings.blend = false;
        GraphicsSettings.grassDisplacement = false;
        GraphicsSettings.IsWindEnabled = false;
        GraphicsSettings.foliageFocus = false;
        GraphicsSettings.blast = false;
        GraphicsSettings.puddle = false;
        GraphicsSettings.glitter = false;
        GraphicsSettings.triplanar = false;
        GraphicsSettings.skyboxReflection = false;
        GraphicsSettings.IsItemIconAntiAliasingEnabled = false;
        GraphicsSettings.chromaticAberration = false;
        GraphicsSettings.antiAliasingType = (EAntiAliasingType) 0;
        GraphicsSettings.effectQuality = (EGraphicQuality) 1;
        GraphicsSettings.foliageQuality = (EGraphicQuality) 1;
        GraphicsSettings.sunShaftsQuality = (EGraphicQuality) 0;
        GraphicsSettings.lightingQuality = (EGraphicQuality) 0;
        GraphicsSettings.waterQuality = (EGraphicQuality) 1;
        GraphicsSettings.scopeQuality = (EGraphicQuality) 0;
        GraphicsSettings.outlineQuality = (EGraphicQuality) 1;
        GraphicsSettings.terrainQuality = (EGraphicQuality) 1;
        typeof (MenuConfigurationGraphicsUI).GetMethod("updateAll", BindingFlags.Static | BindingFlags.NonPublic).Invoke((object) null, (object[]) null);
      }
      if (GUILayout.Button("Set Options Settings", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
      {
        OptionsSettings.debug = true;
        OptionsSettings.chatVoiceIn = true;
        OptionsSettings.chatVoiceOut = true;
        OptionsSettings.VoiceAlwaysRecording = true;
        OptionsSettings.useStaticCrosshair = true;
        OptionsSettings.staticCrosshairSize = 0.0f;
        OptionsSettings.vehicleThirdPersonCameraMode = (EVehicleThirdPersonCameraMode) 1;
        typeof (MenuConfigurationOptionsUI).GetMethod("updateAll", BindingFlags.Static | BindingFlags.NonPublic).Invoke((object) null, (object[]) null);
      }
      G.Settings.GlobalOptions.GPS = GUILayout.Toggle(G.Settings.GlobalOptions.GPS, "Force GPS", Array.Empty<GUILayoutOption>());
      G.Settings.GlobalOptions.ShowPlayerOnMap = GUILayout.Toggle(G.Settings.GlobalOptions.ShowPlayerOnMap, "Show Player On Map", Array.Empty<GUILayoutOption>());
      GUILayout.EndScrollView();
      GUILayout.EndArea();
      Rect rect = new Rect(340f, 65f, 350f, 250f);
      GUIStyle guiStyle1 = GUIStyle.op_Implicit("box");
      string name = Enum.GetName(typeof (Visual.ESPObject), (object) VisualsTab.SelectedObject);
      GUIStyle guiStyle2 = guiStyle1;
      GUILayout.BeginArea(rect, name, guiStyle2);
      GUILayout.Space(5f);
      VisualsTab.scrollPosition3 = GUILayout.BeginScrollView(VisualsTab.scrollPosition3, Array.Empty<GUILayoutOption>());
      switch (VisualsTab.SelectedObject)
      {
        case Visual.ESPObject.Player:
          VisualsTab.SelectedOptions = G.Settings.PlayerOptions;
          VisualsTab.DrawGlobals(G.Settings.PlayerOptions, "Players");
          break;
        case Visual.ESPObject.Zombie:
          VisualsTab.SelectedOptions = G.Settings.ZombieOptions;
          VisualsTab.DrawGlobals(G.Settings.ZombieOptions, "Zombies");
          break;
        case Visual.ESPObject.Item:
          VisualsTab.DrawGlobals(G.Settings.ItemOptions, "Items");
          break;
        case Visual.ESPObject.Sentry:
          VisualsTab.SelectedOptions = G.Settings.SentryOptions;
          VisualsTab.DrawGlobals(G.Settings.SentryOptions, "Sentry");
          break;
        case Visual.ESPObject.Bed:
          VisualsTab.SelectedOptions = G.Settings.BedOptions;
          VisualsTab.DrawGlobals(G.Settings.BedOptions, "Beds");
          break;
        case Visual.ESPObject.Flag:
          VisualsTab.SelectedOptions = G.Settings.FlagOptions;
          VisualsTab.DrawGlobals(G.Settings.FlagOptions, "Claim Flags");
          break;
        case Visual.ESPObject.Vehicle:
          VisualsTab.SelectedOptions = G.Settings.VehicleOptions;
          VisualsTab.DrawGlobals(G.Settings.VehicleOptions, "Vehicles");
          break;
        case Visual.ESPObject.Storage:
          VisualsTab.SelectedOptions = G.Settings.StorageOptions;
          VisualsTab.DrawGlobals(G.Settings.StorageOptions, "Storages");
          break;
        case Visual.ESPObject.Airdrop:
          VisualsTab.SelectedOptions = G.Settings.AirdropOptions;
          VisualsTab.DrawGlobals(G.Settings.AirdropOptions, "Airdrop");
          break;
        case Visual.ESPObject.NPC:
          VisualsTab.SelectedOptions = G.Settings.NPCOptions;
          VisualsTab.DrawGlobals(G.Settings.NPCOptions, "NPC");
          break;
        case Visual.ESPObject.Farm:
          VisualsTab.SelectedOptions = G.Settings.FarmOptions;
          VisualsTab.DrawGlobals(G.Settings.FarmOptions, "Farm");
          break;
        case Visual.ESPObject.Resources:
          VisualsTab.SelectedOptions = G.Settings.ResourcesOptions;
          VisualsTab.DrawGlobals(G.Settings.ResourcesOptions, "Resources");
          break;
      }
      GUILayout.EndScrollView();
      GUILayout.EndArea();
      GUILayout.BeginArea(new Rect(700f, 65f, 290f, 350f), GUIStyle.op_Implicit("box"));
      switch (VisualsTab.SelectedObject)
      {
        case Visual.ESPObject.Player:
          VisualsTab.SelectedOptions = G.Settings.PlayerOptions;
          VisualsTab.DrawPreview(G.Settings.PlayerOptions);
          break;
        case Visual.ESPObject.Zombie:
          VisualsTab.SelectedOptions = G.Settings.ZombieOptions;
          VisualsTab.DrawPreviewZombie(G.Settings.ZombieOptions);
          break;
        case Visual.ESPObject.Item:
          VisualsTab.DrawPreviewİtem(G.Settings.ItemOptions);
          break;
        case Visual.ESPObject.Sentry:
          VisualsTab.SelectedOptions = G.Settings.SentryOptions;
          VisualsTab.DrawPreviewSentry(G.Settings.SentryOptions);
          break;
        case Visual.ESPObject.Bed:
          VisualsTab.SelectedOptions = G.Settings.BedOptions;
          VisualsTab.DrawPreviewBed(G.Settings.BedOptions);
          break;
        case Visual.ESPObject.Flag:
          VisualsTab.SelectedOptions = G.Settings.FlagOptions;
          VisualsTab.DrawPreviewFlag(G.Settings.FlagOptions);
          break;
        case Visual.ESPObject.Vehicle:
          VisualsTab.SelectedOptions = G.Settings.VehicleOptions;
          VisualsTab.DrawPreviewVehicle(G.Settings.VehicleOptions);
          break;
        case Visual.ESPObject.Storage:
          VisualsTab.SelectedOptions = G.Settings.StorageOptions;
          VisualsTab.DrawPreviewLocker(G.Settings.StorageOptions);
          break;
        case Visual.ESPObject.Airdrop:
          VisualsTab.SelectedOptions = G.Settings.AirdropOptions;
          VisualsTab.DrawPreviewAirdrop(G.Settings.AirdropOptions);
          break;
        case Visual.ESPObject.NPC:
          VisualsTab.SelectedOptions = G.Settings.NPCOptions;
          VisualsTab.DrawPreviewNPC(G.Settings.NPCOptions);
          break;
        case Visual.ESPObject.Farm:
          VisualsTab.SelectedOptions = G.Settings.FarmOptions;
          VisualsTab.DrawPreviewFarm(G.Settings.FarmOptions);
          break;
        case Visual.ESPObject.Resources:
          VisualsTab.SelectedOptions = G.Settings.ResourcesOptions;
          VisualsTab.DrawPreviewResources(G.Settings.ResourcesOptions);
          break;
      }
      GUILayout.EndArea();
    }

    private static void DrawPreview(VisualOptions options)
    {
      switch (options.ChamType)
      {
        case Visual.ShaderType.Lightening:
          GUI.Label(new Rect(25f, 20f, 250f, 400f), (Texture) Asset.Lightening);
          break;
        case Visual.ShaderType.WireFrame:
          GUI.Label(new Rect(25f, 20f, 250f, 400f), (Texture) Asset.WireFrame);
          break;
        case Visual.ShaderType.Xray:
          GUI.Label(new Rect(25f, 20f, 250f, 400f), (Texture) Asset.XrayPlayer);
          break;
        case Visual.ShaderType.Flat:
          GUI.Label(new Rect(25f, 20f, 250f, 400f), (Texture) Asset.ChamsPlayer);
          break;
        case Visual.ShaderType.None:
          GUI.Label(new Rect(25f, 20f, 250f, 400f), (Texture) Asset.Player);
          break;
      }
      switch (options.BoxType)
      {
        case Visual.BoxType.Corners:
          Visual.DrawColorCorners(new Rect(15f, 10f, 260f, 280f), Color.cyan);
          break;
        case Visual.BoxType.Box2D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
        case Visual.BoxType.Box3D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
      }
      if (!options.Distance && !options.Name && !G.Settings.GlobalOptions.Weapon)
        return;
      GUI.Label(new Rect(110f, 300f, 335f, 22f), "<size=" + options.FontSize.ToString() + ">" + (options.Distance ? "[50]" : "") + (options.Name ? " 31" : "") + (G.Settings.GlobalOptions.Weapon ? (options.Distance || options.Name ? " - Maplestrike" : "Maplestrike") : "") + "</size>");
    }

    private static void DrawPreviewZombie(VisualOptions options)
    {
      GUI.Label(new Rect(25f, 20f, 250f, 400f), (Texture) Asset.Zombie);
      switch (options.BoxType)
      {
        case Visual.BoxType.Corners:
          Visual.DrawColorCorners(new Rect(15f, 10f, 260f, 280f), Color.cyan);
          break;
        case Visual.BoxType.Box2D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
        case Visual.BoxType.Box3D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
      }
      if (!options.Distance && !options.Name)
        return;
      GUI.Label(new Rect(110f, 300f, 335f, 22f), "<size=" + options.FontSize.ToString() + ">" + (options.Distance ? "[50]" : "") + (options.Name ? " Zombie" : "") + (G.Settings.GlobalOptions.Weapon ? (options.Distance || options.Name ? "" : "") : "") + "</size>");
    }

    private static void DrawPreviewSentry(VisualOptions options)
    {
      GUI.Label(new Rect(25f, 20f, 250f, 400f), (Texture) Asset.Sentry);
      switch (options.BoxType)
      {
        case Visual.BoxType.Corners:
          Visual.DrawColorCorners(new Rect(15f, 10f, 260f, 280f), Color.cyan);
          break;
        case Visual.BoxType.Box2D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
        case Visual.BoxType.Box3D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
      }
      if (!options.Distance && !options.Name)
        return;
      GUI.Label(new Rect(110f, 300f, 335f, 22f), "<size=" + options.FontSize.ToString() + ">" + (options.Distance ? "[50]" : "") + (options.Name ? " Sentry" : "") + (G.Settings.GlobalOptions.Weapon ? (options.Distance || options.Name ? "" : "") : "") + "</size>");
    }

    private static void DrawPreviewİtem(VisualOptions options)
    {
      GUI.Label(new Rect(25f, 20f, 250f, 400f), (Texture) Asset.Item);
      switch (options.BoxType)
      {
        case Visual.BoxType.Corners:
          Visual.DrawColorCorners(new Rect(15f, 10f, 260f, 280f), Color.cyan);
          break;
        case Visual.BoxType.Box2D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
        case Visual.BoxType.Box3D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
      }
      if (!options.Distance && !options.Name)
        return;
      GUI.Label(new Rect(110f, 300f, 335f, 22f), "<size=" + options.FontSize.ToString() + ">" + (options.Distance ? "[50]" : "") + (options.Name ? " Item" : "") + (G.Settings.GlobalOptions.Weapon ? (options.Distance || options.Name ? "" : "") : "") + "</size>");
    }

    private static void DrawPreviewBed(VisualOptions options)
    {
      GUI.Label(new Rect(25f, 20f, 250f, 400f), (Texture) Asset.Yatak);
      switch (options.BoxType)
      {
        case Visual.BoxType.Corners:
          Visual.DrawColorCorners(new Rect(15f, 10f, 260f, 280f), Color.cyan);
          break;
        case Visual.BoxType.Box2D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
        case Visual.BoxType.Box3D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
      }
      if (!options.Distance && !options.Name)
        return;
      GUI.Label(new Rect(110f, 300f, 335f, 22f), "<size=" + options.FontSize.ToString() + ">" + (options.Distance ? "[50]" : "") + (options.Name ? " Bed" : "") + (G.Settings.GlobalOptions.Weapon ? (options.Distance || options.Name ? "" : "") : "") + "</size>");
    }

    private static void DrawPreviewFlag(VisualOptions options)
    {
      GUI.Label(new Rect(25f, 20f, 250f, 400f), (Texture) Asset.ClaimFlag);
      switch (options.BoxType)
      {
        case Visual.BoxType.Corners:
          Visual.DrawColorCorners(new Rect(15f, 10f, 260f, 280f), Color.cyan);
          break;
        case Visual.BoxType.Box2D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
        case Visual.BoxType.Box3D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
      }
      if (!options.Distance && !options.Name)
        return;
      GUI.Label(new Rect(110f, 300f, 335f, 22f), "<size=" + options.FontSize.ToString() + ">" + (options.Distance ? "[50]" : "") + (options.Name ? " Claim Flag" : "") + (G.Settings.GlobalOptions.Weapon ? (options.Distance || options.Name ? "" : "") : "") + "</size>");
    }

    private static void DrawPreviewVehicle(VisualOptions options)
    {
      GUI.Label(new Rect(25f, 20f, 250f, 400f), (Texture) Asset.Vehicle);
      switch (options.BoxType)
      {
        case Visual.BoxType.Corners:
          Visual.DrawColorCorners(new Rect(15f, 10f, 260f, 280f), Color.cyan);
          break;
        case Visual.BoxType.Box2D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
        case Visual.BoxType.Box3D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
      }
      if (!options.Distance && !options.Name)
        return;
      GUI.Label(new Rect(110f, 300f, 335f, 22f), "<size=" + options.FontSize.ToString() + ">" + (options.Distance ? "[50]" : "") + (options.Name ? " Vehicle" : "") + (G.Settings.GlobalOptions.Weapon ? (options.Distance || options.Name ? "" : "") : "") + "</size>");
    }

    private static void DrawPreviewLocker(VisualOptions options)
    {
      GUI.Label(new Rect(25f, 20f, 250f, 400f), (Texture) Asset.Locker);
      switch (options.BoxType)
      {
        case Visual.BoxType.Corners:
          Visual.DrawColorCorners(new Rect(15f, 10f, 260f, 280f), Color.cyan);
          break;
        case Visual.BoxType.Box2D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
        case Visual.BoxType.Box3D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
      }
      if (!options.Distance && !options.Name)
        return;
      GUI.Label(new Rect(110f, 300f, 335f, 22f), "<size=" + options.FontSize.ToString() + ">" + (options.Distance ? "[50]" : "") + (options.Name ? " Locker" : "") + (G.Settings.GlobalOptions.Weapon ? (options.Distance || options.Name ? "" : "") : "") + "</size>");
    }

    private static void DrawPreviewAirdrop(VisualOptions options)
    {
      GUI.Label(new Rect(25f, 20f, 250f, 400f), (Texture) Asset.Airdrop);
      switch (options.BoxType)
      {
        case Visual.BoxType.Corners:
          Visual.DrawColorCorners(new Rect(15f, 10f, 260f, 280f), Color.cyan);
          break;
        case Visual.BoxType.Box2D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
        case Visual.BoxType.Box3D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
      }
      if (!options.Distance && !options.Name)
        return;
      GUI.Label(new Rect(110f, 300f, 335f, 22f), "<size=" + options.FontSize.ToString() + ">" + (options.Distance ? "[50]" : "") + (options.Name ? " AIRDROP" : "") + (G.Settings.GlobalOptions.Weapon ? (options.Distance || options.Name ? "" : "") : "") + "</size>");
    }

    private static void DrawPreviewNPC(VisualOptions options)
    {
      GUI.Label(new Rect(25f, 20f, 250f, 400f), (Texture) Asset.NPC);
      switch (options.BoxType)
      {
        case Visual.BoxType.Corners:
          Visual.DrawColorCorners(new Rect(15f, 10f, 260f, 280f), Color.cyan);
          break;
        case Visual.BoxType.Box2D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
        case Visual.BoxType.Box3D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
      }
      if (!options.Distance && !options.Name)
        return;
      GUI.Label(new Rect(110f, 300f, 335f, 22f), "<size=" + options.FontSize.ToString() + ">" + (options.Distance ? "[50]" : "") + (options.Name ? " NPC" : "") + (G.Settings.GlobalOptions.Weapon ? (options.Distance || options.Name ? "" : "") : "") + "</size>");
    }

    private static void DrawPreviewResources(VisualOptions options)
    {
      GUI.Label(new Rect(25f, 20f, 250f, 400f), (Texture) Asset.Resources);
      switch (options.BoxType)
      {
        case Visual.BoxType.Corners:
          Visual.DrawColorCorners(new Rect(15f, 10f, 260f, 280f), Color.cyan);
          break;
        case Visual.BoxType.Box2D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
        case Visual.BoxType.Box3D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
      }
      if (!options.Distance && !options.Name)
        return;
      GUI.Label(new Rect(110f, 300f, 335f, 22f), "<size=" + options.FontSize.ToString() + ">" + (options.Distance ? "[50]" : "") + (options.Name ? " Resources" : "") + (G.Settings.GlobalOptions.Weapon ? (options.Distance || options.Name ? "" : "") : "") + "</size>");
    }

    private static void DrawPreviewFarm(VisualOptions options)
    {
      GUI.Label(new Rect(25f, 20f, 250f, 400f), (Texture) Asset.Farm);
      switch (options.BoxType)
      {
        case Visual.BoxType.Corners:
          Visual.DrawColorCorners(new Rect(15f, 10f, 260f, 280f), Color.cyan);
          break;
        case Visual.BoxType.Box2D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
        case Visual.BoxType.Box3D:
          Visual.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f));
          break;
      }
      if (!options.Distance && !options.Name)
        return;
      GUI.Label(new Rect(110f, 300f, 335f, 22f), "<size=" + options.FontSize.ToString() + ">" + (options.Distance ? "[50]" : "") + (options.Name ? " Farm" : "") + (G.Settings.GlobalOptions.Weapon ? (options.Distance || options.Name ? "" : "") : "") + "</size>");
    }

    private static void DrawGlobals(VisualOptions options, string objname)
    {
      GUILayout.Space(2f);
      options.Enabled = GUILayout.Toggle(options.Enabled, objname + " - Enabled", Array.Empty<GUILayoutOption>());
      options.Name = GUILayout.Toggle(options.Name, "Name", Array.Empty<GUILayoutOption>());
      options.Distance = GUILayout.Toggle(options.Distance, "Distance", Array.Empty<GUILayoutOption>());
    }

    private static void DrawGlobals2(VisualOptions options)
    {
      if (VisualsTab.SelectedObject == Visual.ESPObject.Player && GUILayout.Button("Cham Type: " + Enum.GetName(typeof (Visual.ShaderType), (object) options.ChamType), GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
        options.ChamType = options.ChamType.Next<Visual.ShaderType>();
      if (GUILayout.Button("Box Type: " + Enum.GetName(typeof (Visual.BoxType), (object) options.BoxType), GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
        options.BoxType = options.BoxType.Next<Visual.BoxType>();
      GUILayout.Space(2f);
      GUILayout.Label("Max Render Distance: " + options.MaxDistance.ToString(), Array.Empty<GUILayoutOption>());
      options.MaxDistance = (int) GUILayout.HorizontalSlider((float) options.MaxDistance, 0.0f, 3000f, Array.Empty<GUILayoutOption>());
      GUILayout.Space(2f);
      GUILayout.Label("Font Size: " + options.FontSize.ToString(), Array.Empty<GUILayoutOption>());
      options.FontSize = (int) GUILayout.HorizontalSlider((float) options.FontSize, 0.0f, 24f, Array.Empty<GUILayoutOption>());
    }
  }
}
