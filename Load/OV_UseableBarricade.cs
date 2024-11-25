// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OV_UseableBarricade
// <3

using SDG.Unturned;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class OV_UseableBarricade
  {
    public static Vector3 BuildPos;
    public static FieldInfo pointField = typeof (UseableBarricade).GetField("point", BindingFlags.Instance | BindingFlags.NonPublic);

    [Override(typeof (UseableBarricade), "checkSpace", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
    public bool OV_checkSpace()
    {
      if (!G.Settings.MiscOptions.BuildToAnywhere || SpyUtilities.BeingSpied)
        return (bool) OverrideUtilities.CallOriginal((object) this);
      OverrideUtilities.CallOriginal((object) this);
      if (Vector3.op_Inequality((Vector3) OV_UseableBarricade.pointField.GetValue((object) this), Vector3.zero) && !G.Settings.MiscOptions.Freecam)
      {
        if (G.Settings.MiscOptions.BuildPosSet)
          OV_UseableBarricade.pointField.SetValue((object) this, (object) Vector3.op_Addition((Vector3) OV_UseableBarricade.pointField.GetValue((object) this), OV_UseableBarricade.BuildPos));
        return true;
      }
      RaycastHit raycastHit;
      if (Physics.Raycast(new Ray(((Component) Camera.main).transform.position, ((Component) Camera.main).transform.forward), ref raycastHit, 20f, RayMasks.DAMAGE_CLIENT))
      {
        Vector3 vector3 = ((RaycastHit) ref raycastHit).point;
        if (G.Settings.MiscOptions.BuildPosSet)
          vector3 = Vector3.op_Addition(vector3, OV_UseableBarricade.BuildPos);
        OV_UseableBarricade.pointField.SetValue((object) this, (object) vector3);
        return true;
      }
      Vector3 vector3_1 = Vector3.op_Addition(((Component) Camera.main).transform.position, Vector3.op_Multiply(((Component) Camera.main).transform.forward, 7f));
      if (G.Settings.MiscOptions.BuildPosSet)
        vector3_1 = Vector3.op_Addition(vector3_1, OV_UseableBarricade.BuildPos);
      OV_UseableBarricade.pointField.SetValue((object) this, (object) vector3_1);
      return true;
    }
  }
}
