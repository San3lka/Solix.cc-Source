// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OV_PlayerInteract
// <3

using SDG.Unturned;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class OV_PlayerInteract
  {
    public static FieldInfo FocusField;
    public static FieldInfo TargetField;
    public static FieldInfo InteractableField;
    public static FieldInfo Interactable2Field;
    public static FieldInfo PurchaseAssetField;
    public static bool isHoldingKey;
    public static float lastInteract;
    public static float lastKeyDown;
    public static RaycastHit hit;

    [Initializer]
    public static void Init()
    {
      OV_PlayerInteract.FocusField = typeof (PlayerInteract).GetField("focus", BindingFlags.Static | BindingFlags.NonPublic);
      OV_PlayerInteract.TargetField = typeof (PlayerInteract).GetField("target", BindingFlags.Static | BindingFlags.NonPublic);
      OV_PlayerInteract.InteractableField = typeof (PlayerInteract).GetField("_interactable", BindingFlags.Static | BindingFlags.NonPublic);
      OV_PlayerInteract.Interactable2Field = typeof (PlayerInteract).GetField("_interactable2", BindingFlags.Static | BindingFlags.NonPublic);
      OV_PlayerInteract.PurchaseAssetField = typeof (PlayerInteract).GetField("purchaseAsset", BindingFlags.Static | BindingFlags.NonPublic);
    }

    public static Transform focus
    {
      get => (Transform) OV_PlayerInteract.FocusField.GetValue((object) null);
      set => OV_PlayerInteract.FocusField.SetValue((object) null, (object) value);
    }

    public static Transform target
    {
      get => (Transform) OV_PlayerInteract.TargetField.GetValue((object) null);
      set => OV_PlayerInteract.TargetField.SetValue((object) null, (object) value);
    }

    public static Interactable interactable
    {
      get => (Interactable) OV_PlayerInteract.InteractableField.GetValue((object) null);
      set => OV_PlayerInteract.InteractableField.SetValue((object) null, (object) value);
    }

    public static Interactable2 interactable2
    {
      get => (Interactable2) OV_PlayerInteract.Interactable2Field.GetValue((object) null);
      set => OV_PlayerInteract.Interactable2Field.SetValue((object) null, (object) value);
    }

    public static ItemAsset purchaseAsset
    {
      get => (ItemAsset) OV_PlayerInteract.PurchaseAssetField.GetValue((object) null);
      set => OV_PlayerInteract.PurchaseAssetField.SetValue((object) null, (object) value);
    }

    public float salvageTime
    {
      get
      {
        if (G.Settings.MiscOptions.CustomSalvageTime)
          return G.Settings.MiscOptions.SalvageTime;
        return !Player.player.channel.owner.isAdmin ? 8f : 1f;
      }
    }

    [Override(typeof (PlayerInteract), "Update", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
    public void OV_Update()
    {
      if (!VectorUtilities.ShouldRun())
        return;
      if (SpyUtilities.BeingSpied)
      {
        Transform transform = ((Component) Camera.main).transform;
        if (Object.op_Inequality((Object) transform, (Object) null))
          Physics.Raycast(new Ray(transform.position, transform.forward), ref OV_PlayerInteract.hit, Player.player.look.perspective == 1 ? 6f : 4f, RayMasks.PLAYER_INTERACT, (QueryTriggerInteraction) 0);
      }
      if (Player.player.stance.stance != 6 && Player.player.stance.stance != 7 && !Player.player.life.isDead && !Player.player.workzone.isBuilding)
      {
        if ((double) Time.realtimeSinceStartup - (double) OV_PlayerInteract.lastInteract > 0.10000000149011612)
        {
          OV_PlayerInteract.lastInteract = Time.realtimeSinceStartup;
          float num = !G.Settings.MiscOptions.InteractThroughWalls || SpyUtilities.BeingSpied ? 4f : 20f;
          Physics.Raycast(new Ray(((Component) Camera.main).transform.position, ((Component) Camera.main).transform.forward), ref OV_PlayerInteract.hit, Player.player.look.perspective == 1 ? num + 2f : num, RayMasks.PLAYER_INTERACT, (QueryTriggerInteraction) 0);
        }
        Transform transform = !Object.op_Inequality((Object) ((RaycastHit) ref OV_PlayerInteract.hit).collider, (Object) null) ? (Transform) null : ((Component) ((RaycastHit) ref OV_PlayerInteract.hit).collider).transform;
        if (Object.op_Inequality((Object) transform, (Object) OV_PlayerInteract.focus))
        {
          if (Object.op_Inequality((Object) OV_PlayerInteract.focus, (Object) null) && Object.op_Inequality((Object) PlayerInteract.interactable, (Object) null))
            HighlighterTool.unhighlight(((Component) PlayerInteract.interactable).transform);
          OV_PlayerInteract.focus = (Transform) null;
          OV_PlayerInteract.target = (Transform) null;
          OV_PlayerInteract.interactable = (Interactable) null;
          OV_PlayerInteract.interactable2 = (Interactable2) null;
          if (Object.op_Inequality((Object) transform, (Object) null))
          {
            OV_PlayerInteract.focus = transform;
            OV_PlayerInteract.interactable = ((Component) OV_PlayerInteract.focus).GetComponentInParent<Interactable>();
            OV_PlayerInteract.interactable2 = ((Component) OV_PlayerInteract.focus).GetComponentInParent<Interactable2>();
            if (Object.op_Inequality((Object) PlayerInteract.interactable, (Object) null))
            {
              OV_PlayerInteract.target = TransformRecursiveFind.FindChildRecursive(((Component) PlayerInteract.interactable).transform, "Target");
              if (PlayerInteract.interactable.checkInteractable())
              {
                if (PlayerUI.window.isEnabled)
                {
                  Color color;
                  if (PlayerInteract.interactable.checkUseable())
                  {
                    if (!PlayerInteract.interactable.checkHighlight(ref color))
                      color = Color.green;
                  }
                  else
                    color = Color.red;
                  HighlighterTool.highlight(((Component) PlayerInteract.interactable).transform, color);
                }
              }
              else
              {
                OV_PlayerInteract.target = (Transform) null;
                OV_PlayerInteract.interactable = (Interactable) null;
              }
            }
          }
        }
      }
      else
      {
        if (Object.op_Inequality((Object) OV_PlayerInteract.focus, (Object) null) && Object.op_Inequality((Object) PlayerInteract.interactable, (Object) null))
          HighlighterTool.unhighlight(((Component) PlayerInteract.interactable).transform);
        OV_PlayerInteract.focus = (Transform) null;
        OV_PlayerInteract.target = (Transform) null;
        OV_PlayerInteract.interactable = (Interactable) null;
        OV_PlayerInteract.interactable2 = (Interactable2) null;
      }
      if (Player.player.life.isDead)
        return;
      if (Object.op_Inequality((Object) PlayerInteract.interactable, (Object) null))
      {
        EPlayerMessage eplayerMessage;
        string str;
        Color color;
        if (PlayerInteract.interactable.checkHint(ref eplayerMessage, ref str, ref color) && !PlayerUI.window.showCursor)
        {
          if (((Component) PlayerInteract.interactable).CompareTag("Item"))
            PlayerUI.hint(!Object.op_Inequality((Object) OV_PlayerInteract.target, (Object) null) ? OV_PlayerInteract.focus : OV_PlayerInteract.target, eplayerMessage, str, color, new object[2]
            {
              (object) ((InteractableItem) PlayerInteract.interactable).item,
              (object) ((InteractableItem) PlayerInteract.interactable).asset
            });
          else
            PlayerUI.hint(!Object.op_Inequality((Object) OV_PlayerInteract.target, (Object) null) ? OV_PlayerInteract.focus : OV_PlayerInteract.target, eplayerMessage, str, color, new object[0]);
        }
      }
      else if (OV_PlayerInteract.purchaseAsset != null && Object.op_Inequality((Object) Player.player.movement.purchaseNode, (Object) null) && !PlayerUI.window.showCursor)
        PlayerUI.hint((Transform) null, (EPlayerMessage) 47, string.Empty, Color.white, new object[2]
        {
          (object) OV_PlayerInteract.purchaseAsset.itemName,
          (object) Player.player.movement.purchaseNode.cost
        });
      else if (Object.op_Inequality((Object) OV_PlayerInteract.focus, (Object) null) && ((Component) OV_PlayerInteract.focus).CompareTag("Enemy"))
      {
        Player player = DamageTool.getPlayer(OV_PlayerInteract.focus);
        if (Object.op_Inequality((Object) player, (Object) null) && Object.op_Inequality((Object) player, (Object) Player.player) && !PlayerUI.window.showCursor)
          PlayerUI.hint((Transform) null, (EPlayerMessage) 11, string.Empty, Color.white, new object[1]
          {
            (object) player.channel.owner
          });
      }
      EPlayerMessage eplayerMessage1;
      float num1;
      if (Object.op_Inequality((Object) PlayerInteract.interactable2, (Object) null) && PlayerInteract.interactable2.checkHint(ref eplayerMessage1, ref num1) && !PlayerUI.window.showCursor)
        PlayerUI.hint2(eplayerMessage1, !OV_PlayerInteract.isHoldingKey ? 0.0f : (Time.realtimeSinceStartup - OV_PlayerInteract.lastKeyDown) / this.salvageTime, num1);
      if (Input.GetKeyDown(ControlsSettings.interact))
      {
        OV_PlayerInteract.lastKeyDown = Time.realtimeSinceStartup;
        OV_PlayerInteract.isHoldingKey = true;
      }
      if (Input.GetKeyDown(ControlsSettings.inspect) && ControlsSettings.inspect != ControlsSettings.interact && Player.player.equipment.canInspect)
        Player.player.channel.send("askInspect", (ESteamCall) 0, (ESteamPacket) 1, new object[0]);
      if (!OV_PlayerInteract.isHoldingKey)
        return;
      if (Input.GetKeyUp(ControlsSettings.interact))
      {
        OV_PlayerInteract.isHoldingKey = false;
        if (PlayerUI.window.showCursor)
        {
          if (Player.player.inventory.isStoring && Player.player.inventory.shouldInteractCloseStorage)
          {
            PlayerDashboardUI.close();
            PlayerLifeUI.open();
          }
          else if (PlayerBarricadeSignUI.active)
          {
            PlayerBarricadeSignUI.close();
            PlayerLifeUI.open();
          }
          else if (PlayerBarricadeLibraryUI.active)
          {
            PlayerBarricadeLibraryUI.close();
            PlayerLifeUI.open();
          }
          else if (PlayerNPCDialogueUI.active)
          {
            if (PlayerNPCDialogueUI.IsDialogueAnimating)
              PlayerNPCDialogueUI.SkipAnimation();
            else if (PlayerNPCDialogueUI.CanAdvanceToNextPage)
            {
              PlayerNPCDialogueUI.AdvancePage();
            }
            else
            {
              PlayerNPCDialogueUI.close();
              PlayerLifeUI.open();
            }
          }
          else if (PlayerNPCQuestUI.active)
          {
            PlayerNPCQuestUI.closeNicely();
          }
          else
          {
            if (!PlayerNPCVendorUI.active)
              return;
            PlayerNPCVendorUI.closeNicely();
          }
        }
        else if (Player.player.stance.stance == 6 || Player.player.stance.stance == 7)
          VehicleManager.exitVehicle();
        else if (Object.op_Inequality((Object) OV_PlayerInteract.focus, (Object) null) && Object.op_Inequality((Object) PlayerInteract.interactable, (Object) null))
        {
          if (!PlayerInteract.interactable.checkUseable())
            return;
          PlayerInteract.interactable.use();
        }
        else if (OV_PlayerInteract.purchaseAsset != null)
        {
          if (Player.player.skills.experience < Player.player.movement.purchaseNode.cost)
            return;
          Player.player.skills.sendPurchase(Player.player.movement.purchaseNode);
        }
        else
        {
          if (ControlsSettings.inspect != ControlsSettings.interact || !Player.player.equipment.canInspect)
            return;
          Player.player.channel.send("askInspect", (ESteamCall) 0, (ESteamPacket) 1, new object[0]);
        }
      }
      else
      {
        if ((double) Time.realtimeSinceStartup - (double) OV_PlayerInteract.lastKeyDown <= (double) this.salvageTime)
          return;
        OV_PlayerInteract.isHoldingKey = false;
        if (PlayerUI.window.showCursor || !Object.op_Inequality((Object) PlayerInteract.interactable2, (Object) null))
          return;
        PlayerInteract.interactable2.use();
      }
    }
  }
}
