// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.MiscTab
// <3

using Load;
using SDG.Unturned;
using System;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class MiscTab
  {
    private static Vector2 Misc1Scroll = new Vector2(0.0f, 0.0f);
    private static Vector2 WeaponScroll = new Vector2(0.0f, 0.0f);
    private static Vector2 Misc2Scroll = new Vector2(0.0f, 0.0f);
    private static Vector2 Misc3Scroll = new Vector2(0.0f, 0.0f);

    public static float _steerMin
    {
      get => Player.player.movement.getVehicle().asset.steerMin;
      set
      {
        typeof (VehicleAsset).GetField(nameof (_steerMin), BindingFlags.Instance | BindingFlags.NonPublic).SetValue((object) Player.player.movement.getVehicle().asset, (object) value);
      }
    }

    public static float _steerMax
    {
      get => Player.player.movement.getVehicle().asset.steerMax;
      set
      {
        typeof (VehicleAsset).GetField(nameof (_steerMax), BindingFlags.Instance | BindingFlags.NonPublic).SetValue((object) Player.player.movement.getVehicle().asset, (object) value);
      }
    }

    public static void Tab()
    {
      GUILayout.BeginArea(new Rect(340f, 20f, 350f, 335f), "Misc 1", GUIStyle.op_Implicit("box"));
      GUILayout.Space(5f);
      MiscTab.Misc1Scroll = GUILayout.BeginScrollView(MiscTab.Misc1Scroll, Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.Freecam = GUILayout.Toggle(G.Settings.MiscOptions.Freecam, "FreeCam", Array.Empty<GUILayoutOption>());
      if (G.Settings.MiscOptions.Freecam && GUILayout.Button("Back To Player (FreeCam)", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
        Player.player.look.orbitPosition = Vector3.zero;
      G.Settings.MiscOptions.Spinbot = GUILayout.Toggle(G.Settings.MiscOptions.Spinbot, "Spinbot", Array.Empty<GUILayoutOption>());
      if (G.Settings.MiscOptions.Spinbot)
        G.Settings.MiscOptions.LocalSpinbot = GUILayout.Toggle(G.Settings.MiscOptions.LocalSpinbot, "Show Local Spinbot", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.VehicleSpinbot = GUILayout.Toggle(G.Settings.MiscOptions.VehicleSpinbot, "Vehicle Spinbot", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.ShowSpyPic = GUILayout.Toggle(G.Settings.MiscOptions.ShowSpyPic, "Show Spy Picture", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.AlertOnSpy = GUILayout.Toggle(G.Settings.MiscOptions.AlertOnSpy, "Spy Alert", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.DeleteCharacterAnimation = GUILayout.Toggle(G.Settings.MiscOptions.DeleteCharacterAnimation, "Delete Character Animation", Array.Empty<GUILayoutOption>());
      G.Settings.GlobalOptions.AutoWalk = GUILayout.Toggle(G.Settings.GlobalOptions.AutoWalk, "Auto Walk", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.ShowAdmin = GUILayout.Toggle(G.Settings.MiscOptions.ShowAdmin, "Show Admin Players", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.SpammerEnabled = GUILayout.Toggle(G.Settings.MiscOptions.SpammerEnabled, "Enable Spam", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.SpamText = GUILayout.TextField(G.Settings.MiscOptions.SpamText, Array.Empty<GUILayoutOption>());
      GUILayout.Label("Spam Delay: " + G.Settings.MiscOptions.SpammerDelay.ToString() + " MilliSecond", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.SpammerDelay = (int) GUILayout.HorizontalSlider((float) G.Settings.MiscOptions.SpammerDelay, 10f, 9999f, Array.Empty<GUILayoutOption>());
      GUILayout.EndScrollView();
      GUILayout.EndArea();
      GUILayout.BeginArea(new Rect(700f, 20f, 290f, 335f), "Weapon", GUIStyle.op_Implicit("box"));
      GUILayout.Space(5f);
      MiscTab.WeaponScroll = GUILayout.BeginScrollView(MiscTab.WeaponScroll, Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.NoSway = GUILayout.Toggle(G.Settings.MiscOptions.NoSway, "Remove Sway", Array.Empty<GUILayoutOption>());
      if (G.Settings.MiscOptions.NoSway)
      {
        GUILayout.Label("Sway: %" + G.Settings.MiscOptions.NoSway1.ToString(), Array.Empty<GUILayoutOption>());
        G.Settings.MiscOptions.NoSway1 = GUILayout.HorizontalSlider(G.Settings.MiscOptions.NoSway1, 0.0f, 1f, Array.Empty<GUILayoutOption>());
      }
      GUILayout.EndScrollView();
      GUILayout.EndArea();
      GUILayout.BeginArea(new Rect(340f, 365f, 350f, 265f), "Misc 2", GUIStyle.op_Implicit("box"));
      GUILayout.Space(5f);
      MiscTab.Misc2Scroll = GUILayout.BeginScrollView(MiscTab.Misc2Scroll, Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.BuildToAnywhere = GUILayout.Toggle(G.Settings.MiscOptions.BuildToAnywhere, "Build To Anywhere", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.BuildPosSet = GUILayout.Toggle(G.Settings.MiscOptions.BuildPosSet, "Change Build Locaion", Array.Empty<GUILayoutOption>());
      if (G.Settings.MiscOptions.BuildPosSet)
      {
        G.Settings.MiscOptions.BuildToAnywhere = true;
        GUILayout.Label("X: " + OV_UseableBarricade.BuildPos.x.ToString(), Array.Empty<GUILayoutOption>());
        OV_UseableBarricade.BuildPos.x = GUILayout.HorizontalSlider(OV_UseableBarricade.BuildPos.x, -5f, 5f, Array.Empty<GUILayoutOption>());
        GUILayout.Label("Y: " + OV_UseableBarricade.BuildPos.y.ToString(), Array.Empty<GUILayoutOption>());
        OV_UseableBarricade.BuildPos.y = GUILayout.HorizontalSlider(OV_UseableBarricade.BuildPos.y, -5f, 5f, Array.Empty<GUILayoutOption>());
        GUILayout.Label("Z: " + OV_UseableBarricade.BuildPos.z.ToString(), Array.Empty<GUILayoutOption>());
        OV_UseableBarricade.BuildPos.z = GUILayout.HorizontalSlider(OV_UseableBarricade.BuildPos.z, -5f, 5f, Array.Empty<GUILayoutOption>());
        GUILayout.Space(2f);
        if (GUILayout.Button("X,Y,Z Reset", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
        {
          OV_UseableBarricade.BuildPos.x = 0.0f;
          OV_UseableBarricade.BuildPos.y = 0.0f;
          OV_UseableBarricade.BuildPos.z = 0.0f;
        }
      }
      G.Settings.MiscOptions.CustomSalvageTime = GUILayout.Toggle(G.Settings.MiscOptions.CustomSalvageTime, "Custom Salvage Time", Array.Empty<GUILayoutOption>());
      if (G.Settings.MiscOptions.CustomSalvageTime)
      {
        GUILayout.Label("Time: " + G.Settings.MiscOptions.SalvageTime.ToString() + " Seconds", Array.Empty<GUILayoutOption>());
        G.Settings.MiscOptions.SalvageTime = GUILayout.HorizontalSlider(G.Settings.MiscOptions.SalvageTime, 0.2f, 10f, Array.Empty<GUILayoutOption>());
      }
      G.Settings.MiscOptions.AutoItemPickupFiltresiz = GUILayout.Toggle(G.Settings.MiscOptions.AutoItemPickupFiltresiz, "Auto Pickup (No Filter)", Array.Empty<GUILayoutOption>());
      if (G.Settings.MiscOptions.AutoItemPickupFiltresiz)
      {
        GUILayout.Label("Delay: " + G.Settings.MiscOptions.ItemPickupDelayFiltresizDelay.ToString() + " Second", Array.Empty<GUILayoutOption>());
        GUILayout.Space(2f);
        G.Settings.MiscOptions.ItemPickupDelayFiltresizDelay = GUILayout.HorizontalSlider(G.Settings.MiscOptions.ItemPickupDelayFiltresizDelay, 0.1f, 10f, Array.Empty<GUILayoutOption>());
        GUILayout.Space(2f);
        GUILayout.Label("Range: " + G.Settings.MiscOptions.ItemPickupDelayFiltresizRange.ToString() + "m", Array.Empty<GUILayoutOption>());
        GUILayout.Space(2f);
        G.Settings.MiscOptions.ItemPickupDelayFiltresizRange = (int) GUILayout.HorizontalSlider((float) G.Settings.MiscOptions.ItemPickupDelayFiltresizRange, 0.0f, 20f, Array.Empty<GUILayoutOption>());
      }
      G.Settings.MiscOptions.HwidChanger = GUILayout.Toggle(G.Settings.MiscOptions.HwidChanger, "HWID Changer", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.extendPlayerRegion = GUILayout.Toggle(G.Settings.MiscOptions.extendPlayerRegion, "Remote Pickup By(G)", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.AutomaticCloseGenerator = GUILayout.Toggle(G.Settings.MiscOptions.AutomaticCloseGenerator, "Automatic Close Generator", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.AutomaticSitToCar = GUILayout.Toggle(G.Settings.MiscOptions.AutomaticSitToCar, "Automatic Sit To Car", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.AutomaticForage = GUILayout.Toggle(G.Settings.MiscOptions.AutomaticForage, "Automatic Forage", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.AutomaticHarvest = GUILayout.Toggle(G.Settings.MiscOptions.AutomaticHarvest, "Automatic Harvest", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.AutomaticStructures = GUILayout.Toggle(G.Settings.MiscOptions.AutomaticStructures, "Automatic Take Your Structures", Array.Empty<GUILayoutOption>());
      if (G.Settings.MiscOptions.AutomaticStructures)
      {
        GUILayout.Label("Structures Time: " + G.Settings.MiscOptions.AutomaticStructuresZ.ToString(), Array.Empty<GUILayoutOption>());
        G.Settings.MiscOptions.AutomaticStructuresZ = GUILayout.HorizontalSlider(G.Settings.MiscOptions.AutomaticStructuresZ, 0.1f, 5f, Array.Empty<GUILayoutOption>());
        GUILayout.Label("Structures Distance: " + G.Settings.MiscOptions.AutomaticStructuresM.ToString(), Array.Empty<GUILayoutOption>());
        G.Settings.MiscOptions.AutomaticStructuresM = (int) GUILayout.HorizontalSlider((float) G.Settings.MiscOptions.AutomaticStructuresM, 0.0f, 1000f, Array.Empty<GUILayoutOption>());
      }
      G.Settings.MiscOptions.AutomaticATM = GUILayout.Toggle(G.Settings.MiscOptions.AutomaticATM, "Automatic ATM Use", Array.Empty<GUILayoutOption>());
      if (G.Settings.MiscOptions.AutomaticATM)
        G.Settings.MiscOptions.AutomaticATMPickup = GUILayout.Toggle(G.Settings.MiscOptions.AutomaticATMPickup, "Automatic ATM Pickup Moneys", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.InteractThroughWalls = GUILayout.Toggle(G.Settings.MiscOptions.InteractThroughWalls, "Pickup In Through Walls", Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.SetTimeEnabled = GUILayout.Toggle(G.Settings.MiscOptions.SetTimeEnabled, "Set Time", Array.Empty<GUILayoutOption>());
      if (G.Settings.MiscOptions.SetTimeEnabled)
      {
        GUILayout.Label("Time: " + G.Settings.MiscOptions.Time.ToString(), Array.Empty<GUILayoutOption>());
        G.Settings.MiscOptions.Time = GUILayout.HorizontalSlider(G.Settings.MiscOptions.Time, 0.0f, 0.9f, Array.Empty<GUILayoutOption>());
      }
      GUILayout.Space(5f);
      GUILayout.EndScrollView();
      GUILayout.EndArea();
      GUILayout.BeginArea(new Rect(700f, 365f, 290f, 265f), "Misc 3", GUIStyle.op_Implicit("box"));
      GUILayout.Space(5f);
      MiscTab.Misc3Scroll = GUILayout.BeginScrollView(MiscTab.Misc3Scroll, Array.Empty<GUILayoutOption>());
      G.Settings.MiscOptions.VehicleDrift = GUILayout.Toggle(G.Settings.MiscOptions.VehicleDrift, "Vehicle Drift", Array.Empty<GUILayoutOption>());
      if (G.Settings.MiscOptions.VehicleDrift && Object.op_Inequality((Object) Player.player.movement.getVehicle(), (Object) null) && VectorUtilities.ShouldRun())
      {
        GUILayout.Label("SteerMin: " + MiscTab._steerMin.ToString(), Array.Empty<GUILayoutOption>());
        MiscTab._steerMin = GUILayout.HorizontalSlider(MiscTab._steerMin, 0.0f, 100f, Array.Empty<GUILayoutOption>());
        GUILayout.Label("SteerMax: " + MiscTab._steerMax.ToString(), Array.Empty<GUILayoutOption>());
        MiscTab._steerMax = GUILayout.HorizontalSlider(MiscTab._steerMax, 0.0f, 100f, Array.Empty<GUILayoutOption>());
      }
      G.Settings.MiscOptions.VehicleFly = GUILayout.Toggle(G.Settings.MiscOptions.VehicleFly, "Vehicle Fly", Array.Empty<GUILayoutOption>());
      if (G.Settings.MiscOptions.VehicleFly)
      {
        G.Settings.MiscOptions.VehicleRigibody = GUILayout.Toggle(G.Settings.MiscOptions.VehicleRigibody, "Vehicle Rigibody", Array.Empty<GUILayoutOption>());
        G.Settings.MiscOptions.VehicleUseMaxSpeed = GUILayout.Toggle(G.Settings.MiscOptions.VehicleUseMaxSpeed, "Vehicle Max Speed", Array.Empty<GUILayoutOption>());
        if (!G.Settings.MiscOptions.VehicleUseMaxSpeed)
        {
          GUILayout.Label("Speed Multiplier: " + G.Settings.MiscOptions.VehicleSpeed.ToString() + "x", Array.Empty<GUILayoutOption>());
          G.Settings.MiscOptions.VehicleSpeed = GUILayout.HorizontalSlider(G.Settings.MiscOptions.VehicleSpeed, 0.1f, 50f, Array.Empty<GUILayoutOption>());
        }
        GUILayout.Space(2f);
      }
      if (GUILayout.Button("Fix Vehicle Status", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
      {
        InteractableVehicle vehicle = Player.player.movement.getVehicle();
        if (Object.op_Inequality((Object) vehicle, (Object) null))
        {
          vehicle.askFillFuel((ushort) 10000);
          vehicle.askRepair((ushort) 10000);
          vehicle.askChargeBattery((ushort) 10000);
        }
      }
      if (GUILayout.Button("Steal Battery", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
      {
        InteractableVehicle vehicle = Player.player.movement.getVehicle();
        if (Object.op_Inequality((Object) vehicle, (Object) null))
          vehicle.stealBattery(Player.player);
      }
      if (GUILayout.Button("Random Teleport With Vehicle", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
      {
        Player player = Provider.clients[new Random().Next(0, Provider.clients.Count)].player;
        if (!player.life.isDead && SteamPlayerID.op_Inequality(player.channel.owner.playerID, Player.player.channel.owner.playerID))
          ((Component) Player.player.movement.getVehicle()).transform.position = ((Component) player).transform.position;
      }
      G.Settings.MiscOptions.Fov = GUILayout.Toggle(G.Settings.MiscOptions.Fov, "Change FOV", Array.Empty<GUILayoutOption>());
      if (G.Settings.MiscOptions.Fov)
      {
        GUILayout.Label(string.Format("Hip: {0}", (object) Provider.preferenceData.Viewmodel.Field_Of_View_Hip), Array.Empty<GUILayoutOption>());
        Provider.preferenceData.Viewmodel.Field_Of_View_Hip = GUILayout.HorizontalSlider(Provider.preferenceData.Viewmodel.Field_Of_View_Hip, -5f, 300f, Array.Empty<GUILayoutOption>());
        GUILayout.Label(string.Format("Fov Aim: {0}", (object) Provider.preferenceData.Viewmodel.Field_Of_View_Aim), Array.Empty<GUILayoutOption>());
        Provider.preferenceData.Viewmodel.Field_Of_View_Aim = GUILayout.HorizontalSlider(Provider.preferenceData.Viewmodel.Field_Of_View_Aim, -5f, 300f, Array.Empty<GUILayoutOption>());
        GUILayout.Label(string.Format("Horizontal: {0}", (object) Provider.preferenceData.Viewmodel.Offset_Horizontal), Array.Empty<GUILayoutOption>());
        Provider.preferenceData.Viewmodel.Offset_Horizontal = GUILayout.HorizontalSlider(Provider.preferenceData.Viewmodel.Offset_Horizontal, -3f, 150f, Array.Empty<GUILayoutOption>());
        GUILayout.Label(string.Format("Vertical:  {0}", (object) Provider.preferenceData.Viewmodel.Offset_Vertical), Array.Empty<GUILayoutOption>());
        Provider.preferenceData.Viewmodel.Offset_Vertical = GUILayout.HorizontalSlider(Provider.preferenceData.Viewmodel.Offset_Vertical, -2f, 6f, Array.Empty<GUILayoutOption>());
        GUILayout.Label(string.Format("Depth: {0}", (object) Provider.preferenceData.Viewmodel.Offset_Depth), Array.Empty<GUILayoutOption>());
        Provider.preferenceData.Viewmodel.Offset_Depth = GUILayout.HorizontalSlider(Provider.preferenceData.Viewmodel.Offset_Depth, 0.0f, 150f, Array.Empty<GUILayoutOption>());
      }
      else
      {
        Provider.preferenceData.Viewmodel.Field_Of_View_Aim = 60f;
        Provider.preferenceData.Viewmodel.Field_Of_View_Hip = 60f;
        Provider.preferenceData.Viewmodel.Offset_Horizontal = 0.0f;
        Provider.preferenceData.Viewmodel.Offset_Vertical = 0.0f;
        Provider.preferenceData.Viewmodel.Offset_Depth = 0.0f;
      }
      GUILayout.Space(5f);
      GUILayout.Label("Anti Spy Method:", Array.Empty<GUILayoutOption>());
      if (G.Settings.MiscOptions.AntiSpyMethod == 0 && GUILayout.Button("Hide Hack", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
        G.Settings.MiscOptions.AntiSpyMethod = 1;
      if (G.Settings.MiscOptions.AntiSpyMethod == 1)
      {
        if (GUILayout.Button("Random Image in Folder", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
          G.Settings.MiscOptions.AntiSpyMethod = 2;
        GUILayout.Space(10f);
        if (GUILayout.Button("Open Directory The Image Folder", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
          Process.Start(Loadc.AppdatYol + "\\Solix\\CustomScreenShot");
      }
      if (G.Settings.MiscOptions.AntiSpyMethod == 2 && GUILayout.Button("Send No Image", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
        G.Settings.MiscOptions.AntiSpyMethod = 3;
      if (G.Settings.MiscOptions.AntiSpyMethod == 3 && GUILayout.Button("No Anti-Spy", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
        G.Settings.MiscOptions.AntiSpyMethod = 0;
      GUILayout.Space(10f);
      if (GUILayout.Button("Delete Water", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
      {
        if ((double) G.Settings.MiscOptions.Altitude == 0.0)
          G.Settings.MiscOptions.Altitude = LevelLighting.seaLevel;
        LevelLighting.seaLevel = (double) LevelLighting.seaLevel == 0.0 ? G.Settings.MiscOptions.Altitude : 0.0f;
      }
      GUILayout.Space(5f);
      if (Provider.cameraMode != 2 && GUILayout.Button("Open 3rd Camera", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
        Provider.cameraMode = (ECameraMode) 2;
      if (Provider.cameraMode == 2 && GUILayout.Button("Close 3rd Camera", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
        Provider.cameraMode = (ECameraMode) 3;
      GUILayout.Space(5f);
      if (GUILayout.Button("Learn All Achievements", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
      {
        Provider.provider.achievementsService.setAchievement("Frost_Visited");
        Provider.provider.achievementsService.setAchievement("Arid_Visited");
        Provider.provider.achievementsService.setAchievement("Buak_Visited");
        Provider.provider.achievementsService.setAchievement("Elver_Visited");
        Provider.provider.achievementsService.setAchievement("Peaks");
        Provider.provider.achievementsService.setAchievement("Hawaii");
        Provider.provider.achievementsService.setAchievement("Ireland_Visited");
        Provider.provider.achievementsService.setAchievement("Kuwait_Visited");
        Provider.provider.achievementsService.setAchievement("Educated");
        Provider.provider.achievementsService.setAchievement("Zweihander");
        Provider.provider.achievementsService.setAchievement("Boss_Magma");
        Provider.provider.achievementsService.setAchievement("Quest");
        Provider.provider.achievementsService.setAchievement("pei");
        Provider.provider.achievementsService.setAchievement("bridge");
        Provider.provider.achievementsService.setAchievement("mastermind");
        Provider.provider.achievementsService.setAchievement("offense");
        Provider.provider.achievementsService.setAchievement("defense");
        Provider.provider.achievementsService.setAchievement("support");
        Provider.provider.achievementsService.setAchievement("experienced");
        Provider.provider.achievementsService.setAchievement("hoarder");
        Provider.provider.achievementsService.setAchievement("outdoors");
        Provider.provider.achievementsService.setAchievement("psychopath");
        Provider.provider.achievementsService.setAchievement("survivor");
        Provider.provider.achievementsService.setAchievement("berries");
        Provider.provider.achievementsService.setAchievement("accident_prone");
        Provider.provider.achievementsService.setAchievement("wheel");
        Provider.provider.achievementsService.setAchievement("yukon");
        Provider.provider.achievementsService.setAchievement("fishing");
        Provider.provider.achievementsService.setAchievement("washington");
        Provider.provider.achievementsService.setAchievement("crafting");
        Provider.provider.achievementsService.setAchievement("farming");
        Provider.provider.achievementsService.setAchievement("headshot");
        Provider.provider.achievementsService.setAchievement("sharpshooter");
        Provider.provider.achievementsService.setAchievement("hiking");
        Provider.provider.achievementsService.setAchievement("roadtrip");
        Provider.provider.achievementsService.setAchievement("champion");
        Provider.provider.achievementsService.setAchievement("fortified");
        Provider.provider.achievementsService.setAchievement("russia");
        Provider.provider.achievementsService.setAchievement("villain");
        Provider.provider.achievementsService.setAchievement("unturned");
        Provider.provider.achievementsService.setAchievement("forged");
        Provider.provider.achievementsService.setAchievement("soulcrystal");
        Provider.provider.achievementsService.setAchievement("paragon");
        Provider.provider.achievementsService.setAchievement("mk2");
        Provider.provider.achievementsService.setAchievement("ensign");
        Provider.provider.achievementsService.setAchievement("major");
        Provider.provider.achievementsService.setAchievement("lieutenant");
        Provider.provider.achievementsService.setAchievement("hawaii");
        Provider.provider.achievementsService.setAchievement("Yukon");
        Provider.provider.achievementsService.setAchievement("Elver_Visited");
        Provider.provider.achievementsService.setAchievement("Kuwait_Visited");
        Provider.provider.achievementsService.setAchievement("PEI");
        Provider.provider.achievementsService.setAchievement("Washington");
        Provider.provider.achievementsService.setAchievement("Hawaii");
        Provider.provider.achievementsService.setAchievement("Arid_Visited");
        Provider.provider.achievementsService.setAchievement("Ireland_Visited");
        Provider.provider.achievementsService.setAchievement("Russia");
        Provider.provider.achievementsService.setAchievement("Peaks");
        Provider.provider.achievementsService.setAchievement("Wheel");
        Provider.provider.achievementsService.setAchievement("Ensign");
        Provider.provider.achievementsService.setAchievement("Lieutenant");
        Provider.provider.achievementsService.setAchievement("Major");
        Provider.provider.achievementsService.setAchievement("Quest");
        Provider.provider.achievementsService.setAchievement("Villain");
        Provider.provider.achievementsService.setAchievement("Paragon");
        Provider.provider.achievementsService.setAchievement("Offense");
        Provider.provider.achievementsService.setAchievement("Defense");
        Provider.provider.achievementsService.setAchievement("Support");
        Provider.provider.achievementsService.setAchievement("Mastermind");
        Provider.provider.achievementsService.setAchievement("Berries");
      }
      GUILayout.Space(10f);
      if (GUILayout.Button("Turn Off Cheat Completely!", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
      {
        new OverrideManager().OffHook();
        Object.DestroyImmediate((Object) Loadc.obj);
      }
      GUILayout.EndScrollView();
      GUILayout.EndArea();
    }
  }
}
