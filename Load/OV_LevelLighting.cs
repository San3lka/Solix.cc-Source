// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OV_LevelLighting
// <3

using SDG.Unturned;
using System.Reflection;

#nullable disable
namespace kaka
{
  public static class OV_LevelLighting
  {
    public static FieldInfo Time;
    public static bool WasEnabled;

    [Initializer]
    public static void Init()
    {
      OV_LevelLighting.Time = typeof (LevelLighting).GetField("_time", BindingFlags.Static | BindingFlags.NonPublic);
    }

    [Override(typeof (LevelLighting), "updateLighting", BindingFlags.Static | BindingFlags.Public, 0)]
    public static void OV_updateLighting()
    {
      float time = LevelLighting.time;
      if ((!VectorUtilities.ShouldRun() || !G.Settings.MiscOptions.SetTimeEnabled ? 1 : (SpyUtilities.BeingSpied ? 1 : 0)) != 0)
      {
        OverrideUtilities.CallOriginal();
      }
      else
      {
        OV_LevelLighting.Time.SetValue((object) null, (object) G.Settings.MiscOptions.Time);
        OverrideUtilities.CallOriginal();
        OV_LevelLighting.Time.SetValue((object) null, (object) time);
      }
    }
  }
}
