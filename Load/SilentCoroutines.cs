// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.SilentCoroutines
// <3

using SDG.Unturned;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class SilentCoroutines
  {
    public static List<Player> CachedPlayers = new List<Player>();

    public static IEnumerator UpdateObjects()
    {
      while (true)
      {
        while (VectorUtilities.ShouldRun())
        {
          GameObject[] array = ((IEnumerable<Collider>) Physics.OverlapSphere(((Component) Player.player).transform.position, (Player.player.equipment.asset is ItemGunAsset asset ? ((ItemWeaponAsset) asset).range : 15.5f) + 10f)).Select<Collider, GameObject>((Func<Collider, GameObject>) (c => ((Component) c).gameObject)).ToArray<GameObject>();
          SilentUtilities.Objects.Clear();
          if (G.Settings.SilentOptions.Silent)
          {
            if (G.Settings.SilentOptions.TargetPlayers)
            {
              SilentCoroutines.CachedPlayers.Clear();
              foreach (GameObject gameObject in array)
              {
                Player player = DamageTool.getPlayer(gameObject.transform);
                if (!Object.op_Equality((Object) player, (Object) null) && !SilentCoroutines.CachedPlayers.Contains(player) && !Object.op_Equality((Object) player, (Object) Player.player) && !player.life.isDead)
                  SilentCoroutines.CachedPlayers.Add(player);
              }
              SilentUtilities.Objects.AddRange<GameObject>(SilentCoroutines.CachedPlayers.Select<Player, GameObject>((Func<Player, GameObject>) (c => ((Component) c).gameObject)));
            }
            if (G.Settings.SilentOptions.TargetZombies)
              SilentUtilities.Objects.AddRange<GameObject>(((IEnumerable<GameObject>) array).Where<GameObject>((Func<GameObject, bool>) (g => Object.op_Inequality((Object) g.GetComponent<Zombie>(), (Object) null))));
            if (G.Settings.SilentOptions.TargetSentries)
              SilentUtilities.Objects.AddRange<GameObject>(((IEnumerable<GameObject>) array).Where<GameObject>((Func<GameObject, bool>) (g => Object.op_Inequality((Object) g.GetComponent<InteractableSentry>(), (Object) null))));
            if (G.Settings.SilentOptions.TargetBeds)
              SilentUtilities.Objects.AddRange<GameObject>(((IEnumerable<GameObject>) array).Where<GameObject>((Func<GameObject, bool>) (g => Object.op_Inequality((Object) g.GetComponent<InteractableBed>(), (Object) null))));
            if (G.Settings.SilentOptions.TargetAnimal)
              SilentUtilities.Objects.AddRange<GameObject>(((IEnumerable<GameObject>) array).Where<GameObject>((Func<GameObject, bool>) (g => Object.op_Inequality((Object) g.GetComponent<Animal>(), (Object) null))));
            if (G.Settings.SilentOptions.TargetGenerators)
              SilentUtilities.Objects.AddRange<GameObject>(((IEnumerable<GameObject>) array).Where<GameObject>((Func<GameObject, bool>) (g => Object.op_Inequality((Object) g.GetComponent<InteractableGenerator>(), (Object) null))));
            if (G.Settings.SilentOptions.TargetClaimFlags)
              SilentUtilities.Objects.AddRange<GameObject>(((IEnumerable<GameObject>) array).Where<GameObject>((Func<GameObject, bool>) (g => Object.op_Inequality((Object) g.GetComponent<InteractableClaim>(), (Object) null))));
            if (G.Settings.SilentOptions.TargetVehicles)
              SilentUtilities.Objects.AddRange<GameObject>(((IEnumerable<GameObject>) array).Where<GameObject>((Func<GameObject, bool>) (g => Object.op_Inequality((Object) g.GetComponent<InteractableVehicle>(), (Object) null))));
            if (G.Settings.SilentOptions.TargetStorage)
              SilentUtilities.Objects.AddRange<GameObject>(((IEnumerable<GameObject>) array).Where<GameObject>((Func<GameObject, bool>) (g => Object.op_Inequality((Object) g.GetComponent<InteractableStorage>(), (Object) null))));
            if (G.Settings.SilentOptions.TargetTrees)
            {
              for (byte index1 = 0; (int) index1 < (int) Regions.WORLD_SIZE; ++index1)
              {
                for (byte index2 = 0; (int) index2 < (int) Regions.WORLD_SIZE; ++index2)
                {
                  foreach (ResourceSpawnpoint resourceSpawnpoint in LevelGround.trees[(int) index1, (int) index2])
                  {
                    if (!resourceSpawnpoint.isDead && resourceSpawnpoint != null && VectorUtilities.GetDistance(resourceSpawnpoint.model.position) < 15.5)
                      SilentUtilities.Objects.Add(((Component) resourceSpawnpoint.model).gameObject);
                  }
                }
              }
            }
            if (G.Settings.SilentOptions.TargetYourSelf)
              SilentUtilities.Objects.AddRange<GameObject>(((IEnumerable<GameObject>) array).Where<GameObject>((Func<GameObject, bool>) (g => Object.op_Inequality((Object) g.GetComponent<Player>(), (Object) null))));
          }
          yield return (object) new WaitForSeconds(0.1f);
        }
        SilentUtilities.Objects.Clear();
        yield return (object) new WaitForSeconds(1f);
      }
    }
  }
}
