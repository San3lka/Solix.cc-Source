// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OV_DamageTool
// <3

using SDG.Unturned;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace kaka
{
  public static class OV_DamageTool
  {
    public static OverrideType OVType;

    [Override(typeof (DamageTool), "raycast", BindingFlags.Static | BindingFlags.Public, 1)]
    public static RaycastInfo OV_raycast(Ray ray, float range, int mask, Player ignorePlayer = null)
    {
      return OV_DamageTool.SetupRaycast(ray, range, mask, ignorePlayer);
    }

    public static RaycastInfo SetupRaycast(Ray ray, float range, int mask, Player ignorePlayer = null)
    {
      switch (OV_DamageTool.OVType)
      {
        case OverrideType.Extended:
          return SilentUtilities.GenerateOriginalRaycast(ray, range, mask);
        case OverrideType.PlayerHit:
          for (int index = 0; index < Provider.clients.Count; ++index)
          {
            if (VectorUtilities.GetDistance(((Component) Player.player).transform.position, ((Component) Provider.clients[index].player).transform.position) <= 15.5)
            {
              RaycastInfo info;
              SilentUtilities.GenerateRaycast(out info);
              return info;
            }
          }
          break;
        case OverrideType.SilentAim:
          RaycastInfo info1;
          return !SilentUtilities.GenerateRaycast(out info1) ? SilentUtilities.GenerateOriginalRaycast(ray, range, mask) : info1;
        case OverrideType.SilentAimMelee:
          RaycastInfo info2;
          return !SilentUtilities.GenerateRaycast(out info2) ? SilentUtilities.GenerateOriginalRaycast(ray, Mathf.Max(5.5f, range), mask) : info2;
      }
      return SilentUtilities.GenerateOriginalRaycast(ray, range, mask, ignorePlayer);
    }
  }
}
