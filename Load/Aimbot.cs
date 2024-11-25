// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.Aimbot
// <3

using SDG.Unturned;
using System.Collections;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace kaka
{
  public static class Aimbot
  {
    public static Vector3 PiVector = new Vector3(0.0f, 3.14159274f, 0.0f);
    public static GameObject LockedObject;
    public static bool IsAiming = false;
    public static FieldInfo PitchInfo;
    public static FieldInfo YawInfo;

    public static float Pitch
    {
      get => Player.player.look.pitch;
      set => Aimbot.PitchInfo.SetValue((object) Player.player.look, (object) value);
    }

    public static float Yaw
    {
      get => Player.player.look.yaw;
      set => Aimbot.YawInfo.SetValue((object) Player.player.look, (object) value);
    }

    [Initializer]
    public static void Init()
    {
      Aimbot.PitchInfo = typeof (PlayerLook).GetField("_pitch", BindingFlags.Instance | BindingFlags.NonPublic);
      Aimbot.YawInfo = typeof (PlayerLook).GetField("_yaw", BindingFlags.Instance | BindingFlags.NonPublic);
    }

    public static IEnumerator SetLockedObject()
    {
      while (true)
      {
        while ((!VectorUtilities.ShouldRun() ? 1 : (!G.Settings.AimbotOptions.Aimbot ? 1 : 0)) == 0)
        {
          Player player = (Player) null;
          Vector3 position = Player.player.look.aim.position;
          Vector3 forward = Player.player.look.aim.forward;
          SteamPlayer[] array = Provider.clients.ToArray();
          for (int index = 0; index < array.Length; ++index)
          {
            SteamPlayer steamPlayer = array[index];
            Visual.Priority priority;
            G.Settings.Priority.TryGetValue(steamPlayer.playerID.steamID.m_SteamID, out priority);
            if ((steamPlayer == null || Object.op_Equality((Object) steamPlayer.player, (Object) Player.player) || Object.op_Equality((Object) steamPlayer.player.life, (Object) null) || steamPlayer.player.life.isDead ? 1 : (priority == Visual.Priority.Friendly ? 1 : 0)) == 0)
            {
              if (VectorUtilities.GetAngleDelta(position, forward, ((Component) array[index].player).transform.position) < (double) G.Settings.AimbotOptions.AimbotFOV)
              {
                double distance = (double) (int) VectorUtilities.GetDistance(((Component) array[index].player).transform.position, ((Component) Player.player).transform.position);
                float? gunDistance = VectorUtilities.GetGunDistance();
                double valueOrDefault = (double) gunDistance.GetValueOrDefault();
                if (distance < valueOrDefault & gunDistance.HasValue)
                {
                  if (Object.op_Equality((Object) player, (Object) null))
                    player = array[index].player;
                  else if (VectorUtilities.GetAngleDelta(position, forward, ((Component) array[index].player).transform.position) < VectorUtilities.GetAngleDelta(position, forward, ((Component) player).transform.position))
                    player = array[index].player;
                }
              }
            }
          }
          if (!Aimbot.IsAiming)
            Aimbot.LockedObject = Object.op_Inequality((Object) player, (Object) null) ? ((Component) player).gameObject : (GameObject) null;
          yield return (object) new WaitForEndOfFrame();
          Vector3 vector3_1 = new Vector3();
          Vector3 vector3_2 = new Vector3();
        }
        yield return (object) new WaitForSeconds(0.1f);
      }
    }

    public static IEnumerator AimToObject()
    {
      while (true)
      {
        while ((!VectorUtilities.ShouldRun() ? 1 : (!G.Settings.AimbotOptions.Aimbot ? 1 : 0)) == 0)
        {
          if ((!Object.op_Inequality((Object) Aimbot.LockedObject, (Object) null) ? 0 : (Object.op_Inequality((Object) Aimbot.LockedObject.transform, (Object) null) ? 1 : 0)) != 0)
          {
            if ((Input.GetKey(Hotkeys.GetKey(nameof (Aimbot))) ? 1 : (!G.Settings.AimbotOptions.OnKey ? 1 : 0)) != 0)
            {
              Aimbot.IsAiming = true;
              if (G.Settings.AimbotOptions.Smooth)
                Aimbot.SmoothAim(Aimbot.LockedObject);
              else
                Aimbot.Aim(Aimbot.LockedObject);
            }
            else
              Aimbot.IsAiming = false;
          }
          else
            Aimbot.IsAiming = false;
          yield return (object) new WaitForEndOfFrame();
        }
        yield return (object) new WaitForSeconds(0.1f);
      }
    }

    public static void Aim(GameObject obj)
    {
      Camera main = Camera.main;
      Vector3 aimPosition = Aimbot.GetAimPosition(obj.transform, G.Settings.AimbotOptions.TargetLimb.ToString());
      if (!Vector3.op_Inequality(aimPosition, Aimbot.PiVector))
        return;
      ((Component) Player.player).transform.LookAt(aimPosition);
      Transform transform = ((Component) Player.player).transform;
      Quaternion rotation1 = ((Component) Player.player).transform.rotation;
      Vector3 vector3 = new Vector3(0.0f, ((Quaternion) ref rotation1).eulerAngles.y, 0.0f);
      transform.eulerAngles = vector3;
      ((Component) main).transform.LookAt(aimPosition);
      Quaternion localRotation1 = ((Component) main).transform.localRotation;
      float num = ((Quaternion) ref localRotation1).eulerAngles.x;
      if (((double) num > 90.0 ? 0 : ((double) num <= 270.0 ? 1 : 0)) != 0)
      {
        Quaternion localRotation2 = ((Component) main).transform.localRotation;
        num = ((Quaternion) ref localRotation2).eulerAngles.x + 90f;
      }
      else if (((double) num < 270.0 ? 0 : ((double) num <= 360.0 ? 1 : 0)) != 0)
      {
        Quaternion localRotation3 = ((Component) main).transform.localRotation;
        num = ((Quaternion) ref localRotation3).eulerAngles.x - 270f;
      }
      Aimbot.Pitch = num;
      Quaternion rotation2 = ((Component) Player.player).transform.rotation;
      Aimbot.Yaw = ((Quaternion) ref rotation2).eulerAngles.y;
    }

    public static void SmoothAim(GameObject obj)
    {
      Camera main = Camera.main;
      Vector3 aimPosition = Aimbot.GetAimPosition(obj.transform, G.Settings.AimbotOptions.TargetLimb.ToString());
      if (!Vector3.op_Inequality(aimPosition, Aimbot.PiVector))
        return;
      ((Component) Player.player).transform.rotation = Quaternion.Slerp(((Component) Player.player).transform.rotation, Quaternion.LookRotation(Vector3.op_Subtraction(aimPosition, ((Component) Player.player).transform.position)), Time.deltaTime * G.Settings.AimbotOptions.AimSpeed);
      Transform transform = ((Component) Player.player).transform;
      Quaternion rotation1 = ((Component) Player.player).transform.rotation;
      Vector3 vector3 = new Vector3(0.0f, ((Quaternion) ref rotation1).eulerAngles.y, 0.0f);
      transform.eulerAngles = vector3;
      ((Component) main).transform.localRotation = Quaternion.Slerp(((Component) main).transform.localRotation, Quaternion.LookRotation(Vector3.op_Subtraction(aimPosition, ((Component) main).transform.position)), Time.deltaTime * G.Settings.AimbotOptions.AimSpeed);
      Quaternion localRotation1 = ((Component) main).transform.localRotation;
      float num = ((Quaternion) ref localRotation1).eulerAngles.x;
      if (((double) num > 90.0 ? 0 : ((double) num <= 270.0 ? 1 : 0)) != 0)
      {
        Quaternion localRotation2 = ((Component) main).transform.localRotation;
        num = ((Quaternion) ref localRotation2).eulerAngles.x + 90f;
      }
      else if (((double) num < 270.0 ? 0 : ((double) num <= 360.0 ? 1 : 0)) != 0)
      {
        Quaternion localRotation3 = ((Component) main).transform.localRotation;
        num = ((Quaternion) ref localRotation3).eulerAngles.x - 270f;
      }
      Aimbot.Pitch = num;
      Quaternion rotation2 = ((Component) Player.player).transform.rotation;
      Aimbot.Yaw = ((Quaternion) ref rotation2).eulerAngles.y;
    }

    public static Vector3 GetAimPosition(Transform parent, string name)
    {
      Transform[] componentsInChildren = ((Component) parent).GetComponentsInChildren<Transform>();
      Vector3 piVector;
      if (componentsInChildren == null)
      {
        piVector = Aimbot.PiVector;
      }
      else
      {
        foreach (Transform transform in componentsInChildren)
        {
          if (((Object) transform).name.Trim() == name)
            return Vector3.op_Addition(transform.position, new Vector3(0.0f, 0.4f, 0.0f));
        }
        piVector = Aimbot.PiVector;
      }
      return piVector;
    }
  }
}
