// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.C
// <3

using System;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class C
  {
    public static Visual.ESPObject selected;

    public static void AddColors()
    {
      foreach (Visual.ESPObject espObject in Enum.GetValues(typeof (Visual.ESPObject)))
      {
        string name = Enum.GetName(typeof (Visual.ESPObject), (object) espObject);
        switch (espObject)
        {
          case Visual.ESPObject.Player:
            C.AddColor(name + "_ESP", Color32.op_Implicit(Color.red));
            break;
          case Visual.ESPObject.Zombie:
            C.AddColor(name + "_ESP", Color32.op_Implicit(Color.green));
            break;
          case Visual.ESPObject.Item:
            C.AddColor(name + "_ESP", Color32.op_Implicit(Color.green));
            break;
          case Visual.ESPObject.Sentry:
            C.AddColor(name + "_ESP", Color32.op_Implicit(Color.blue));
            break;
          case Visual.ESPObject.Bed:
            C.AddColor(name + "_ESP", Color32.op_Implicit(Color.magenta));
            break;
          case Visual.ESPObject.Flag:
            C.AddColor(name + "_ESP", Color32.op_Implicit(Color.yellow));
            break;
          case Visual.ESPObject.Vehicle:
            C.AddColor(name + "_ESP", Color32.op_Implicit(Color.magenta));
            break;
          case Visual.ESPObject.Storage:
            C.AddColor(name + "_ESP", Color32.op_Implicit(Color.cyan));
            break;
          case Visual.ESPObject.Airdrop:
            C.AddColor(name + "_ESP", Color32.op_Implicit(Color.grey));
            break;
          case Visual.ESPObject.NPC:
            C.AddColor(name + "_ESP", Color32.op_Implicit(Color.white));
            break;
          case Visual.ESPObject.Farm:
            C.AddColor(name + "_ESP", Color32.op_Implicit(Color.yellow));
            break;
          case Visual.ESPObject.Resources:
            C.AddColor(name + "_ESP", Color32.op_Implicit(Color.blue));
            break;
        }
        C.AddColor("Player_Chams_Visible_Color", Color32.op_Implicit(Color.yellow));
        C.AddColor("Player_Chams_Occluded_Color", Color32.op_Implicit(Color.red));
        C.AddColor("Box_Color", Color32.op_Implicit(Color.yellow));
      }
      C.AddColor("Friendly_Player_ESP", Color32.op_Implicit(Color.cyan));
      C.AddColor("Friendly_Chams_Visible_Color", Color32.op_Implicit(Color.cyan));
      C.AddColor("Friendly_Chams_Occluded_Color", new Color32((byte) 0, (byte) 128, byte.MaxValue, byte.MaxValue));
      C.AddColor("Silent_Aim_FOV_Circle", Color32.op_Implicit(Color.red));
      C.AddColor("Aimlock_FOV_Circle", Color32.op_Implicit(Color.green));
    }

    public static Color32 GetColor(string identifier)
    {
      Color32 color32;
      return G.Settings.GlobalOptions.GlobalColors.TryGetValue(identifier, out color32) ? color32 : Color32.op_Implicit(Color.magenta);
    }

    public static void AddColor(string id, Color32 c)
    {
      if (G.Settings.GlobalOptions.GlobalColors.ContainsKey(id))
        return;
      G.Settings.GlobalOptions.GlobalColors.Add(id, c);
    }

    public static void SetColor(string id, Color32 c)
    {
      G.Settings.GlobalOptions.GlobalColors[id] = c;
    }

    public static string ColorToHex(Color32 color)
    {
      return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
    }
  }
}
