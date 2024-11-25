// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OVItemManager
// <3

using SDG.Unturned;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace kaka
{
  public static class OVItemManager
  {
    [Override(typeof (ItemManager), "findSimulatedItemsInRadius", BindingFlags.Static | BindingFlags.Public, 0)]
    public static void OV_findSimulatedItemsInRadius(
      Vector3 center,
      float sqrRadius,
      List<InteractableItem> result)
    {
      sqrRadius = G.Settings.MiscOptions.extendPlayerRegion ? 400f : sqrRadius;
      if (ItemManager.clampedItems == null)
        return;
      foreach (InteractableItem clampedItem in ItemManager.clampedItems)
      {
        if (!Object.op_Equality((Object) clampedItem, (Object) null))
        {
          Vector3 vector3 = Vector3.op_Subtraction(((Component) clampedItem).transform.position, center);
          if ((double) ((Vector3) ref vector3).sqrMagnitude <= (double) sqrRadius)
            result.Add(clampedItem);
        }
      }
    }
  }
}
