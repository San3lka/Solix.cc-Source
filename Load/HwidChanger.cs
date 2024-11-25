// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.HwidChanger
// <3

using SDG.Unturned;
using System.Collections;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class HwidChanger
  {
    public static object methodhwid;
    public static byte[] Hwid1;
    public static byte[] Hwid2;
    public static byte[] Hwid3;
    public static byte[][] hwid;
    public static MethodBase method = (MethodBase) typeof (LocalHwid).GetMethod("InitHwids", BindingFlags.Static | BindingFlags.NonPublic);

    [Initializer]
    public static void a()
    {
      HwidChanger.methodhwid = (object) (byte[][]) HwidChanger.method.Invoke((object) null, (object[]) null);
      HwidChanger.hwid = (byte[][]) HwidChanger.methodhwid;
    }

    public static IEnumerator SetHwid()
    {
      if (G.Settings.MiscOptions.HwidChanger)
      {
        HwidChanger.CreateRandomHwid();
        HwidChanger.hwid.SetValue((object) HwidChanger.Hwid1, 0);
        HwidChanger.hwid.SetValue((object) HwidChanger.Hwid2, 1);
        HwidChanger.hwid.SetValue((object) HwidChanger.Hwid3, 2);
      }
      yield return (object) new WaitForSeconds(10f);
    }

    [Override(typeof (LocalHwid), "GetHwids", BindingFlags.Static | BindingFlags.Public, 0)]
    public static byte[][] OV_GetHwids() => HwidChanger.hwid;

    public static void CreateRandomHwid()
    {
      HwidChanger.Hwid1 = new byte[20];
      for (int index = 0; index < 20; ++index)
        HwidChanger.Hwid1[index] = (byte) Random.Range(0, 256);
      HwidChanger.Hwid2 = new byte[20];
      for (int index = 0; index < 20; ++index)
        HwidChanger.Hwid2[index] = (byte) Random.Range(0, 256);
      HwidChanger.Hwid3 = new byte[20];
      for (int index = 0; index < 20; ++index)
        HwidChanger.Hwid3[index] = (byte) Random.Range(0, 256);
    }
  }
}
