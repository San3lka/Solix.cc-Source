// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.SilentUtilities
// <3

using SDG.Unturned;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace kaka
{
  public static class SilentUtilities
  {
    public static SafezoneNode isSafeInfo;
    public static HashSet<GameObject> Objects = new HashSet<GameObject>();

    public static RaycastInfo GenerateOriginalRaycast(
      Ray ray,
      float range,
      int mask,
      Player ignorePlayer = null)
    {
      RaycastHit raycastHit;
      Physics.Raycast(ray, ref raycastHit, range, mask);
      RaycastInfo originalRaycast = new RaycastInfo(raycastHit);
      originalRaycast.direction = ((Ray) ref ray).direction;
      originalRaycast.limb = (ELimb) 12;
      if (Object.op_Inequality((Object) originalRaycast.transform, (Object) null))
      {
        if (((Component) originalRaycast.transform).CompareTag("Barricade"))
          originalRaycast.transform = DamageTool.getBarricadeRootTransform(originalRaycast.transform);
        else if (((Component) originalRaycast.transform).CompareTag("Structure"))
          originalRaycast.transform = DamageTool.getBarricadeRootTransform(originalRaycast.transform);
        else if (((Component) originalRaycast.transform).CompareTag("Resource"))
          originalRaycast.transform = DamageTool.getBarricadeRootTransform(originalRaycast.transform);
        else if (((Component) originalRaycast.transform).CompareTag("Enemy"))
        {
          originalRaycast.player = DamageTool.getPlayer(originalRaycast.transform);
          if (Object.op_Equality((Object) originalRaycast.player, (Object) ignorePlayer))
            originalRaycast.player = (Player) null;
          originalRaycast.limb = DamageTool.getLimb(originalRaycast.transform);
        }
        else if (((Component) originalRaycast.transform).CompareTag("Zombie"))
        {
          originalRaycast.zombie = DamageTool.getZombie(originalRaycast.transform);
          originalRaycast.limb = DamageTool.getLimb(originalRaycast.transform);
        }
        else if (((Component) originalRaycast.transform).CompareTag("Animal"))
        {
          originalRaycast.animal = DamageTool.getAnimal(originalRaycast.transform);
          originalRaycast.limb = DamageTool.getLimb(originalRaycast.transform);
        }
        else if (((Component) originalRaycast.transform).CompareTag("Vehicle"))
          originalRaycast.vehicle = DamageTool.getVehicle(originalRaycast.transform);
        originalRaycast.materialName = !Object.op_Inequality((Object) originalRaycast.zombie, (Object) null) || !originalRaycast.zombie.isRadioactive ? PhysicsTool.GetMaterialName(((RaycastHit) ref raycastHit).point, originalRaycast.transform, originalRaycast.collider) : "ALIEN_DYNAMIC";
      }
      return originalRaycast;
    }

    public static bool GenerateRaycast(out RaycastInfo info)
    {
      float num = Player.player.equipment.asset is ItemGunAsset asset ? ((ItemWeaponAsset) asset).range : 16f;
      info = SilentUtilities.GenerateOriginalRaycast(new Ray(Player.player.look.aim.position, Player.player.look.aim.forward), num, RayMasks.DAMAGE_CLIENT);
      GameObject Object;
      Vector3 Point;
      if (!SilentUtilities.GetTargetObject(SilentUtilities.Objects, out Object, out Point, num))
        return false;
      info = SilentUtilities.GenerateRaycast(Object, Point, info.collider);
      return true;
    }

    public static RaycastInfo GenerateRaycast(GameObject Object, Vector3 Point, Collider col)
    {
      ELimb targetLimb = G.Settings.SilentOptions.TargetLimb;
      if (G.Settings.SilentOptions.UseRandomLimb)
      {
        ELimb[] values = (ELimb[]) Enum.GetValues(typeof (ELimb));
        targetLimb = (ELimb) (int) values[new Random().Next(0, values.Length)];
      }
      return new RaycastInfo(Object.transform)
      {
        point = Point,
        direction = Player.player.look.aim.forward,
        limb = targetLimb,
        collider = col,
        transform = Object.transform,
        materialName = PhysicsTool.GetMaterialName(Point, Object.transform, col),
        player = Object.GetComponent<Player>(),
        zombie = Object.GetComponentInParent<Zombie>(),
        vehicle = Object.GetComponent<InteractableVehicle>(),
        animal = Object.GetComponentInParent<Animal>()
      };
    }

    public static bool GetTargetObject(
      HashSet<GameObject> Objects,
      out GameObject Object,
      out Vector3 Point,
      float Range)
    {
      double num1 = (double) Range + 1.0;
      double num2 = 180.0;
      Object = (GameObject) null;
      Point = Vector3.zero;
      Vector3 position1 = Player.player.look.aim.position;
      Vector3 forward = Player.player.look.aim.forward;
      foreach (GameObject Target in Objects)
      {
        if (Object.op_Equality((Object) Target, (Object) null))
          return false;
        if (Object.op_Equality((Object) Target.GetComponent<SilentComponent>(), (Object) null))
        {
          Target.AddComponent<SilentComponent>();
        }
        else
        {
          Vector3 position2 = Target.transform.position;
          Player component1 = Target.GetComponent<Player>();
          if ((!Object.op_Implicit((Object) component1) || !component1.life.isDead && !PlayersTab.IsFriendly(component1)) && (!G.Settings.SilentOptions.SafeZone || !LevelNodes.isPointInsideSafezone(((Component) component1).transform.position, ref SilentUtilities.isSafeInfo)) && (!G.Settings.SilentOptions.Admin || !component1.channel.owner.isAdmin))
          {
            Zombie component2 = Target.GetComponent<Zombie>();
            if (!Object.op_Implicit((Object) component2) || !component2.isDead)
            {
              double distance = VectorUtilities.GetDistance(position1, position2);
              if (distance <= (double) Range)
              {
                if (G.Settings.SilentOptions.SilentAimUseFOV)
                {
                  double angleDelta = VectorUtilities.GetAngleDelta(position1, forward, position2);
                  if (angleDelta <= (double) G.Settings.SilentOptions.SilentAimFOV && angleDelta <= num2)
                    num2 = angleDelta;
                  else
                    continue;
                }
                else if (distance > num1)
                  continue;
                Vector3 Point1;
                if (SilentSphere.GetRaycast(Target, position1, out Point1))
                {
                  Object = Target;
                  num1 = distance;
                  Point = Point1;
                }
              }
            }
          }
        }
      }
      return Object.op_Inequality((Object) Object, (Object) null);
    }

    public static T Next<T>(this T src) where T : struct
    {
      if (!typeof (T).IsEnum)
        throw new ArgumentException(string.Format("Argument {0} is not an Enum", (object) typeof (T).FullName));
      T[] values = (T[]) Enum.GetValues(src.GetType());
      int index = Array.IndexOf<T>(values, src) + 1;
      return values.Length != index ? values[index] : values[0];
    }

    public static void AddRange<T>(this HashSet<T> source, IEnumerable<T> target)
    {
      foreach (T obj in target)
        source.Add(obj);
    }
  }
}
