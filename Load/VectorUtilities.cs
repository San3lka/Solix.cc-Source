// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.VectorUtilities
// <3

using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace kaka
{
  public static class VectorUtilities
  {
    private static readonly GUIStyle textureStyle = new GUIStyle()
    {
      normal = new GUIStyleState()
      {
        background = Texture2D.whiteTexture
      }
    };

    public static void DrawColor(Rect position, Color color)
    {
      Color backgroundColor = GUI.backgroundColor;
      GUI.backgroundColor = color;
      GUI.Box(position, GUIContent.none, VectorUtilities.textureStyle);
      GUI.backgroundColor = backgroundColor;
    }

    public static void DrawColorBox(Color color, Rect Pos, int thinkness = 1)
    {
      GUI.skin = Asset.Skin;
      Color backgroundColor = GUI.backgroundColor;
      GUI.backgroundColor = color;
      GUI.Button(new Rect(((Rect) ref Pos).x, ((Rect) ref Pos).y, ((Rect) ref Pos).width, (float) thinkness), " ", VectorUtilities.textureStyle);
      GUI.Button(new Rect(((Rect) ref Pos).x + ((Rect) ref Pos).width, ((Rect) ref Pos).y, (float) thinkness, ((Rect) ref Pos).height), " ", VectorUtilities.textureStyle);
      GUI.Button(new Rect(((Rect) ref Pos).x, ((Rect) ref Pos).y, (float) thinkness, ((Rect) ref Pos).height), " ", VectorUtilities.textureStyle);
      GUI.Button(new Rect(((Rect) ref Pos).x, ((Rect) ref Pos).y + ((Rect) ref Pos).height, ((Rect) ref Pos).width, (float) thinkness), " ", VectorUtilities.textureStyle);
      GUI.backgroundColor = backgroundColor;
    }

    public static bool ShouldRun()
    {
      return Provider.isConnected && !Provider.isLoading && !LoadingUI.isBlocked && Object.op_Inequality((Object) Player.player, (Object) null) && Object.op_Inequality((Object) Camera.main, (Object) null);
    }

    public static float? GetGunDistance()
    {
      return new float?(Player.player?.equipment?.asset is ItemGunAsset asset ? ((ItemWeaponAsset) asset).range : 15.5f);
    }

    public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
    {
      return source.Skip<T>(Math.Max(0, source.Count<T>() - N));
    }

    public static double GetDistance(Vector3 point)
    {
      return VectorUtilities.GetDistance(((Component) Camera.main).transform.position, point);
    }

    public static float GetDistance2(Vector3 endpos)
    {
      return (float) Math.Round((double) Vector3.Distance(Player.player.look.aim.position, endpos));
    }

    public static double GetDistance(Vector3 start, Vector3 point)
    {
      Vector3 vector3;
      vector3.x = start.x - point.x;
      vector3.y = start.y - point.y;
      vector3.z = start.z - point.z;
      return Math.Sqrt((double) vector3.x * (double) vector3.x + (double) vector3.y * (double) vector3.y + (double) vector3.z * (double) vector3.z);
    }

    public static float FOVRadius(float fov)
    {
      float fieldOfView = Camera.main.fieldOfView;
      if (GraphicsSettings.scopeQuality != null)
      {
        UseableGun useable = Player.player.equipment.useable as UseableGun;
        if (Object.op_Implicit((Object) useable) && useable.isAiming && ((Behaviour) Player.player.look.scopeCamera).enabled)
          fieldOfView = Player.player.look.scopeCamera.fieldOfView;
      }
      return (float) (Math.Tan((double) fov * (Math.PI / 180.0) / 2.0) / Math.Tan((double) fieldOfView * (Math.PI / 180.0) / 2.0)) * (float) Screen.height;
    }

    public static double GetMagnitude(Vector3 vector)
    {
      return Math.Sqrt((double) vector.x * (double) vector.x + (double) vector.y * (double) vector.y + (double) vector.z * (double) vector.z);
    }

    public static Vector3 Normalize(Vector3 vector)
    {
      return Vector3.op_Division(vector, (float) VectorUtilities.GetMagnitude(vector));
    }

    public static double GetAngleDelta(Vector3 mainPos, Vector3 forward, Vector3 target)
    {
      Vector3 vector3 = VectorUtilities.Normalize(Vector3.op_Subtraction(target, mainPos));
      return Math.Atan2(VectorUtilities.GetMagnitude(Vector3.Cross(vector3, forward)), (double) Vector3.Dot(vector3, forward)) * (180.0 / Math.PI);
    }
  }
}
