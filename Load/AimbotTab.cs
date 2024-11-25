// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.AimbotTab
// <3

using SDG.Unturned;
using System;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class AimbotTab
  {
    private static Vector2 SilentScroll;
    private static Vector2 AimbotScroll;

    public static void Tab()
    {
      GUILayout.BeginArea(new Rect(340f, 20f, 350f, 400f), "Silent Aimbot", GUIStyle.op_Implicit("box"));
      GUILayout.Space(5f);
      AimbotTab.SilentScroll = GUILayout.BeginScrollView(AimbotTab.SilentScroll, Array.Empty<GUILayoutOption>());
      G.Settings.SilentOptions.Silent = GUILayout.Toggle(G.Settings.SilentOptions.Silent, "Silent Aim", Array.Empty<GUILayoutOption>());
      if (G.Settings.SilentOptions.Silent)
      {
        G.Settings.SilentOptions.PunchSilentAim = true;
        G.Settings.SilentOptions.ExtendMeleeRange = true;
        G.Settings.SilentOptions.SilentAimUseFOV = GUILayout.Toggle(G.Settings.SilentOptions.SilentAimUseFOV, "Use FOV", Array.Empty<GUILayoutOption>());
        if (G.Settings.SilentOptions.SilentAimUseFOV)
        {
          GUILayout.Label("FOV: " + G.Settings.SilentOptions.SilentAimFOV.ToString(), Array.Empty<GUILayoutOption>());
          G.Settings.SilentOptions.SilentAimFOV = (int) GUILayout.HorizontalSlider((float) G.Settings.SilentOptions.SilentAimFOV, 1f, 180f, Array.Empty<GUILayoutOption>());
          G.Settings.SilentOptions.ShowSilentFOV = GUILayout.Toggle(G.Settings.SilentOptions.ShowSilentFOV, "Draw FOV", Array.Empty<GUILayoutOption>());
        }
        G.Settings.SilentOptions.SafeZone = GUILayout.Toggle(G.Settings.SilentOptions.SafeZone, "Dont Hit If In SafeZone", Array.Empty<GUILayoutOption>());
        G.Settings.SilentOptions.Admin = GUILayout.Toggle(G.Settings.SilentOptions.Admin, "Dont Hit If Admin", Array.Empty<GUILayoutOption>());
      }
      else
      {
        G.Settings.SilentOptions.PunchSilentAim = false;
        G.Settings.SilentOptions.ExtendMeleeRange = false;
      }
      GUILayout.EndScrollView();
      GUILayout.EndArea();
      GUILayout.BeginArea(new Rect(700f, 20f, 290f, 400f), "Silent Aim Targets", GUIStyle.op_Implicit("box"));
      GUILayout.Space(5f);
      if (G.Settings.SilentOptions.Silent)
      {
        G.Settings.SilentOptions.TargetPlayers = GUILayout.Toggle(G.Settings.SilentOptions.TargetPlayers, "Target: Players", Array.Empty<GUILayoutOption>());
        G.Settings.SilentOptions.TargetBeds = GUILayout.Toggle(G.Settings.SilentOptions.TargetBeds, "Target: Beds", Array.Empty<GUILayoutOption>());
        G.Settings.SilentOptions.TargetZombies = GUILayout.Toggle(G.Settings.SilentOptions.TargetZombies, "Target: Zombies", Array.Empty<GUILayoutOption>());
        G.Settings.SilentOptions.TargetAnimal = GUILayout.Toggle(G.Settings.SilentOptions.TargetAnimal, "Target: Animals", Array.Empty<GUILayoutOption>());
        G.Settings.SilentOptions.TargetGenerators = GUILayout.Toggle(G.Settings.SilentOptions.TargetGenerators, "Target: Generators", Array.Empty<GUILayoutOption>());
        G.Settings.SilentOptions.TargetClaimFlags = GUILayout.Toggle(G.Settings.SilentOptions.TargetClaimFlags, "Target: Claim Flag", Array.Empty<GUILayoutOption>());
        G.Settings.SilentOptions.TargetVehicles = GUILayout.Toggle(G.Settings.SilentOptions.TargetVehicles, "Target: Vehicle", Array.Empty<GUILayoutOption>());
        G.Settings.SilentOptions.TargetStorage = GUILayout.Toggle(G.Settings.SilentOptions.TargetStorage, "Target: Storage", Array.Empty<GUILayoutOption>());
        G.Settings.SilentOptions.TargetSentries = GUILayout.Toggle(G.Settings.SilentOptions.TargetSentries, "Target: Sentries", Array.Empty<GUILayoutOption>());
        G.Settings.SilentOptions.TargetTrees = GUILayout.Toggle(G.Settings.SilentOptions.TargetTrees, "Target: Trees", Array.Empty<GUILayoutOption>());
        G.Settings.SilentOptions.TargetFarm = GUILayout.Toggle(G.Settings.SilentOptions.TargetFarm, "Target: Farm", Array.Empty<GUILayoutOption>());
        G.Settings.SilentOptions.TargetYourSelf = GUILayout.Toggle(G.Settings.SilentOptions.TargetYourSelf, "Target: YourSelf", Array.Empty<GUILayoutOption>());
      }
      GUILayout.EndArea();
      GUILayout.BeginArea(new Rect(340f, 430f, 350f, 200f), "Silent Aim Settings / Fov", GUIStyle.op_Implicit("box"));
      GUILayout.Space(5f);
      if (G.Settings.SilentOptions.FovMethod == 0 && GUILayout.Button("Fov Type: Circle", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
      {
        G.Settings.SilentOptions.FovMethod = 1;
        G.Settings.SilentOptions.FovKalınlık = 2.098787f;
      }
      if (G.Settings.SilentOptions.FovMethod == 1 && GUILayout.Button("Fov Type: Trigon", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
      {
        G.Settings.SilentOptions.FovMethod = 2;
        G.Settings.SilentOptions.FovKalınlık = 1.569352f;
      }
      if (G.Settings.SilentOptions.FovMethod == 2 && GUILayout.Button("Fov Type: Square", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
      {
        G.Settings.SilentOptions.FovMethod = 3;
        G.Settings.SilentOptions.FovKalınlık = 1.048189f;
      }
      if (G.Settings.SilentOptions.FovMethod == 3 && GUILayout.Button("Fov Type: Hexagon", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
      {
        G.Settings.SilentOptions.FovMethod = 4;
        G.Settings.SilentOptions.FovKalınlık = 0.7899502f;
      }
      if (G.Settings.SilentOptions.FovMethod == 4 && GUILayout.Button("Fov Type: Octagon", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
      {
        G.Settings.SilentOptions.FovMethod = 5;
        G.Settings.SilentOptions.FovKalınlık = 0.3946678f;
      }
      if (G.Settings.SilentOptions.FovMethod == 5 && GUILayout.Button("Fov Type: Hexadecimal", GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
      {
        G.Settings.SilentOptions.FovMethod = 0;
        G.Settings.SilentOptions.FovKalınlık = 0.01f;
      }
      if (G.Settings.SilentOptions.Silent)
      {
        if (GUILayout.Button("Silent Aim Limb: " + Enum.GetName(typeof (ELimb), (object) G.Settings.SilentOptions.TargetLimb), GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
          G.Settings.SilentOptions.TargetLimb = G.Settings.SilentOptions.TargetLimb.Next<ELimb>();
        G.Settings.SilentOptions.UseRandomLimb = GUILayout.Toggle(G.Settings.SilentOptions.UseRandomLimb, "Random Limb", Array.Empty<GUILayoutOption>());
        GUILayout.Label(Math.Round((double) G.Settings.SilentOptions.SphereRadius, 2).ToString() + "m", Array.Empty<GUILayoutOption>());
        G.Settings.SilentOptions.SphereRadius = (float) (int) GUILayout.HorizontalSlider(G.Settings.SilentOptions.SphereRadius, 1f, 14f, Array.Empty<GUILayoutOption>());
      }
      GUILayout.EndArea();
      GUILayout.BeginArea(new Rect(700f, 430f, 290f, 200f), "Aimbot", GUIStyle.op_Implicit("box"));
      GUILayout.Space(5f);
      AimbotTab.AimbotScroll = GUILayout.BeginScrollView(AimbotTab.AimbotScroll, Array.Empty<GUILayoutOption>());
      G.Settings.AimbotOptions.Aimbot = GUILayout.Toggle(G.Settings.AimbotOptions.Aimbot, "Aimbot", Array.Empty<GUILayoutOption>());
      if (G.Settings.AimbotOptions.Aimbot)
      {
        G.Settings.AimbotOptions.Smooth = GUILayout.Toggle(G.Settings.AimbotOptions.Smooth, "Smooth", Array.Empty<GUILayoutOption>());
        if (G.Settings.AimbotOptions.Smooth)
        {
          GUILayout.Label("Speed: " + G.Settings.AimbotOptions.AimSpeed.ToString(), Array.Empty<GUILayoutOption>());
          G.Settings.AimbotOptions.AimSpeed = (float) (int) GUILayout.HorizontalSlider(G.Settings.AimbotOptions.AimSpeed, 1f, 10f, Array.Empty<GUILayoutOption>());
        }
        G.Settings.AimbotOptions.OnKey = GUILayout.Toggle(G.Settings.AimbotOptions.OnKey, "On Key (F)", Array.Empty<GUILayoutOption>());
        G.Settings.AimbotOptions.AimbotUseFov = GUILayout.Toggle(G.Settings.AimbotOptions.AimbotUseFov, "Use FOV", Array.Empty<GUILayoutOption>());
        if (G.Settings.AimbotOptions.AimbotUseFov)
        {
          GUILayout.Label("FOV: " + G.Settings.AimbotOptions.AimbotFOV.ToString(), Array.Empty<GUILayoutOption>());
          G.Settings.AimbotOptions.AimbotFOV = (float) (int) GUILayout.HorizontalSlider(G.Settings.AimbotOptions.AimbotFOV, 1f, 180f, Array.Empty<GUILayoutOption>());
          G.Settings.AimbotOptions.AimbotShowFOV = GUILayout.Toggle(G.Settings.AimbotOptions.AimbotShowFOV, "Draw FOV", Array.Empty<GUILayoutOption>());
        }
        if (GUILayout.Button("Aimbot Aim Limb: " + Enum.GetName(typeof (AimbotLimb), (object) G.Settings.AimbotOptions.TargetLimb), GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
          G.Settings.AimbotOptions.TargetLimb = G.Settings.AimbotOptions.TargetLimb.Next<AimbotLimb>();
      }
      GUILayout.EndScrollView();
      GUILayout.EndArea();
    }
  }
}
