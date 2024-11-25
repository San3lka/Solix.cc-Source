// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OVRandomEx
// <3

using SDG.Unturned;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace kaka
{
  public static class OVRandomEx
  {
    [Override(typeof (RandomEx), "GetRandomForwardVectorInCone", BindingFlags.Static | BindingFlags.Public, 0)]
    public static Vector3 OV_GetRandomForwardVectorInCone(float halfAngleRadians)
    {
      halfAngleRadians = G.Settings.MiscOptions.NoSway ? G.Settings.MiscOptions.NoSway1 : Mathf.Min(halfAngleRadians, 1.56979632f);
      float num1 = Mathf.Sin(halfAngleRadians * Mathf.Sqrt(Random.value));
      double num2 = 6.2831854820251465 * (double) Random.value;
      float num3 = Mathf.Cos((float) num2);
      double num4 = (double) Mathf.Sin((float) num2);
      float num5 = num3 * num1;
      double num6 = (double) num1;
      float num7 = (float) (num4 * num6);
      float num8 = Mathf.Sqrt((float) (1.0 - (double) num5 * (double) num5 - (double) num7 * (double) num7));
      return new Vector3(num5, num7, num8);
    }
  }
}
