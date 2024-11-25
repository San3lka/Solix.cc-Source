// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OV_PlayerEquipment
// <3

using SDG.Unturned;
using System.Reflection;

#nullable disable
namespace kaka
{
  public class OV_PlayerEquipment
  {
    public static bool WasPunching;
    public static uint CurrSim;

    [Override(typeof (PlayerEquipment), "punch", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
    public void OV_punch(EPlayerPunch p)
    {
      if (G.Settings.SilentOptions.PunchSilentAim)
        OV_DamageTool.OVType = OverrideType.PlayerHit;
      OverrideUtilities.CallOriginal((object) Player.player.equipment, (object) p);
      OV_DamageTool.OVType = OverrideType.None;
    }
  }
}
