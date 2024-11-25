// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OV_Input
// <3

using SDG.Unturned;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace kaka
{
  public static class OV_Input
  {
    public static bool InputEnabled = true;

    [Override(typeof (Input), "GetKey", BindingFlags.Static | BindingFlags.Public, 0)]
    public static bool OV_GetKey(KeyCode key)
    {
      bool key1;
      if ((!VectorUtilities.ShouldRun() ? 1 : (!OV_Input.InputEnabled ? 1 : 0)) != 0)
        key1 = (bool) OverrideUtilities.CallOriginal((object) null, (object) key);
      else if (key == ControlsSettings.up && G.Settings.GlobalOptions.AutoWalk)
      {
        key1 = true;
      }
      else
      {
        int num;
        if ((key == ControlsSettings.left || key == ControlsSettings.right || key == ControlsSettings.up || key == ControlsSettings.down ? (Object.op_Inequality((Object) PlayerFinder.SpP, (Object) null) ? 1 : 0) : 0) == 0)
          num = (bool) OverrideUtilities.CallOriginal((object) null, (object) key) ? 1 : 0;
        else
          num = 0;
        key1 = num != 0;
      }
      return key1;
    }
  }
}
