// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OVInteractableItem
// <3

using SDG.Unturned;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class OVInteractableItem
  {
    public static FieldInfo WasResetField;
    public static List<InteractableItem> items = new List<InteractableItem>();
    [SerializeField]
    private static MonoBehaviour monoBehaviour;
    public static float lastCheckTime = 0.0f;

    [Initializer]
    public static void Initialize()
    {
      OVInteractableItem.WasResetField = typeof (InteractableItem).GetField("wasReset", BindingFlags.NonPublic);
    }

    [Override(typeof (InteractableItem), "OnEnable", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
    public static void OnEnable(InteractableItem instance)
    {
      ItemManager.clampedItems.Add(instance);
      OVInteractableItem.items.Add(instance);
    }

    [Override(typeof (InteractableItem), "OnDisable", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
    public static void OnDisable(InteractableItem instance)
    {
      if ((double) Time.realtimeSinceStartup - (double) OVInteractableItem.lastCheckTime > 2.5)
      {
        OVInteractableItem.lastCheckTime = Time.realtimeSinceStartup;
        for (int index = 0; index < OVInteractableItem.items.Count; ++index)
        {
          if (Object.op_Equality((Object) OVInteractableItem.items[index], (Object) null))
            OVInteractableItem.items.RemoveAt(index);
        }
      }
      if (!(bool) OVInteractableItem.WasResetField.GetValue((object) instance))
        return;
      ListExtension.RemoveFast<InteractableItem>(ItemManager.clampedItems, instance);
    }
  }
}
