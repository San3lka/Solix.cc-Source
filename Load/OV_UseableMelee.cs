// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OV_UseableMelee
// <3

using SDG.Unturned;
using System.Reflection;

#nullable disable
namespace kaka
{
  public class OV_UseableMelee
  {
    [Override(typeof (UseableMelee), "fire", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
    public static void OV_fire()
    {
      OV_DamageTool.OVType = OverrideType.None;
      if ((!G.Settings.SilentOptions.Silent ? 0 : (G.Settings.SilentOptions.ExtendMeleeRange ? 1 : 0)) != 0)
        OV_DamageTool.OVType = OverrideType.SilentAimMelee;
      else if (G.Settings.SilentOptions.Silent)
        OV_DamageTool.OVType = OverrideType.SilentAim;
      else if (G.Settings.SilentOptions.ExtendMeleeRange)
        OV_DamageTool.OVType = OverrideType.Extended;
      OverrideUtilities.CallOriginal((object) Player.player.equipment.useable);
      OV_DamageTool.OVType = OverrideType.None;
    }
  }
}
