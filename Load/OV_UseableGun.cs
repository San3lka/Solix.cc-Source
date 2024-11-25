// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OV_UseableGun
// <3

using SDG.Unturned;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class OV_UseableGun
  {
    public static FieldInfo BulletsField;

    [Initializer]
    public static void Load()
    {
      OV_UseableGun.BulletsField = typeof (UseableGun).GetField("bullets", BindingFlags.Instance | BindingFlags.NonPublic);
    }

    public static bool IsRaycastInvalid(RaycastInfo info)
    {
      return Object.op_Equality((Object) info.player, (Object) null) && Object.op_Equality((Object) info.zombie, (Object) null) && Object.op_Equality((Object) info.animal, (Object) null) && Object.op_Equality((Object) info.vehicle, (Object) null) && Object.op_Equality((Object) info.transform, (Object) null);
    }

    [Override(typeof (UseableGun), "ballistics", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
    public void OV_ballistics()
    {
      Useable useable = Player.player.equipment.useable;
      if (Provider.isServer)
      {
        OverrideUtilities.CallOriginal((object) useable);
      }
      else
      {
        ItemGunAsset asset = (ItemGunAsset) Player.player.equipment.asset;
        PlayerLook look = Player.player.look;
        if (Object.op_Inequality((Object) asset.projectile, (Object) null))
          return;
        List<BulletInfo> bulletInfoList = (List<BulletInfo>) OV_UseableGun.BulletsField.GetValue((object) useable);
        if (bulletInfoList.Count == 0)
          return;
        RaycastInfo info = (RaycastInfo) null;
        if (G.Settings.SilentOptions.Silent)
          SilentUtilities.GenerateRaycast(out info);
        if (Provider.modeConfigData.Gameplay.Ballistics)
        {
          if (info == null)
          {
            OverrideUtilities.CallOriginal((object) useable);
          }
          else
          {
            for (int index = 0; index < bulletInfoList.Count; ++index)
            {
              BulletInfo bulletInfo = bulletInfoList[index];
              double distance = VectorUtilities.GetDistance(((Component) Player.player).transform.position, info.point);
              if ((double) bulletInfo.steps * (double) asset.ballisticTravel >= distance)
              {
                PlayerUI.hitmark(Vector3.zero, false, OV_UseableGun.CalcHitMarker(asset, ref info));
                Player.player.input.sendRaycast(info, (ERaycastInfoUsage) 3);
                bulletInfo.steps = (byte) 254;
              }
            }
            for (int index = bulletInfoList.Count - 1; index >= 0; --index)
            {
              BulletInfo bulletInfo = bulletInfoList[index];
              ++bulletInfo.steps;
              if ((int) bulletInfo.steps >= (int) asset.ballisticSteps)
                bulletInfoList.RemoveAt(index);
            }
          }
        }
        else if (info != null)
        {
          for (int index = 0; index < bulletInfoList.Count; ++index)
          {
            PlayerUI.hitmark(Vector3.zero, false, OV_UseableGun.CalcHitMarker(asset, ref info));
            Player.player.input.sendRaycast(info, (ERaycastInfoUsage) 3);
          }
          bulletInfoList.Clear();
        }
        else
          OverrideUtilities.CallOriginal((object) useable);
      }
    }

    public static EPlayerHit CalcHitMarker(ItemGunAsset PAsset, ref RaycastInfo ri)
    {
      EPlayerHit eplayerHit1 = (EPlayerHit) 0;
      EPlayerHit eplayerHit2;
      if ((ri == null ? 1 : (PAsset == null ? 1 : 0)) != 0)
      {
        eplayerHit2 = eplayerHit1;
      }
      else
      {
        if ((Object.op_Implicit((Object) ri.animal) || Object.op_Implicit((Object) ri.player) ? 1 : (Object.op_Implicit((Object) ri.zombie) ? 1 : 0)) != 0)
        {
          eplayerHit1 = (EPlayerHit) 1;
          if (ri.limb == 13)
            eplayerHit1 = (EPlayerHit) 2;
        }
        else if (Object.op_Implicit((Object) ri.transform))
        {
          if ((!((Component) ri.transform).CompareTag("Barricade") ? 0 : ((double) ((ItemWeaponAsset) PAsset).barricadeDamage > 1.0 ? 1 : 0)) != 0)
          {
            ushort result;
            if (!ushort.TryParse(((Object) ri.transform).name, out result))
              return eplayerHit1;
            ItemBarricadeAsset itemBarricadeAsset = (ItemBarricadeAsset) Assets.find((EAssetType) 1, result);
            if ((itemBarricadeAsset == null ? 1 : (itemBarricadeAsset.isVulnerable ? 0 : (!((ItemWeaponAsset) PAsset).isInvulnerable ? 1 : 0))) != 0)
              return eplayerHit1;
            if (eplayerHit1 == 0)
              eplayerHit1 = (EPlayerHit) 3;
          }
          else if ((!((Component) ri.transform).CompareTag("Structure") ? 0 : ((double) ((ItemWeaponAsset) PAsset).structureDamage > 1.0 ? 1 : 0)) != 0)
          {
            ushort result;
            if (!ushort.TryParse(((Object) ri.transform).name, out result))
              return eplayerHit1;
            ItemStructureAsset itemStructureAsset = (ItemStructureAsset) Assets.find((EAssetType) 1, result);
            if ((itemStructureAsset == null ? 1 : (itemStructureAsset.isVulnerable ? 0 : (!((ItemWeaponAsset) PAsset).isInvulnerable ? 1 : 0))) != 0)
              return eplayerHit1;
            if (eplayerHit1 == 0)
              eplayerHit1 = (EPlayerHit) 3;
          }
          else if ((!((Component) ri.transform).CompareTag("Resource") ? 0 : ((double) ((ItemWeaponAsset) PAsset).resourceDamage > 1.0 ? 1 : 0)) != 0)
          {
            byte num1;
            byte num2;
            ushort num3;
            if (!ResourceManager.tryGetRegion(ri.transform, ref num1, ref num2, ref num3))
              return eplayerHit1;
            ResourceSpawnpoint resourceSpawnpoint = ResourceManager.getResourceSpawnpoint(num1, num2, num3);
            if ((resourceSpawnpoint == null || resourceSpawnpoint.isDead ? 1 : (!((ItemWeaponAsset) PAsset).hasBladeID(resourceSpawnpoint.asset.bladeID) ? 1 : 0)) != 0)
              return eplayerHit1;
            if (eplayerHit1 == 0)
              eplayerHit1 = (EPlayerHit) 3;
          }
          else if ((double) ((ItemWeaponAsset) PAsset).objectDamage > 1.0)
          {
            InteractableObjectRubble component = ((Component) ri.transform).GetComponent<InteractableObjectRubble>();
            if (Object.op_Equality((Object) component, (Object) null))
              return eplayerHit1;
            ri.section = component.getSection(((Component) ri.collider).transform);
            if ((component.isSectionDead(ri.section) ? 1 : (component.asset.rubbleIsVulnerable ? 0 : (!((ItemWeaponAsset) PAsset).isInvulnerable ? 1 : 0))) != 0)
              return eplayerHit1;
            if (eplayerHit1 == 0)
              eplayerHit1 = (EPlayerHit) 3;
          }
        }
        else if ((!Object.op_Implicit((Object) ri.vehicle) || ri.vehicle.isDead ? 0 : ((double) ((ItemWeaponAsset) PAsset).vehicleDamage > 1.0 ? 1 : 0)) != 0 && (ri.vehicle.asset == null ? 0 : (ri.vehicle.asset.isVulnerable ? 1 : (((ItemWeaponAsset) PAsset).isInvulnerable ? 1 : 0))) != 0 && eplayerHit1 == 0)
          eplayerHit1 = (EPlayerHit) 3;
        eplayerHit2 = eplayerHit1;
      }
      return eplayerHit2;
    }
  }
}
