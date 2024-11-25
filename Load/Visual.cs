// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.Visual
// <3

using HighlightingSystem;
using SDG.Unturned;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace kaka
{
  [Component]
  public class Visual : MonoBehaviour
  {
    public static Queue<Visual.ESPBox2> DrawBuffer2 = new Queue<Visual.ESPBox2>();
    public static SteamPlayer[] ConnectedPlayers;
    public static readonly GUIStyle textureStyle = new GUIStyle()
    {
      normal = new GUIStyleState()
      {
        background = Texture2D.whiteTexture
      }
    };
    public static List<Visual.ESPObj> EObjects = new List<Visual.ESPObj>();

    public static void çizmk(Color Col, Vector2 Center, float Radius)
    {
      GL.PushMatrix();
      Manager.DrawMaterial.SetPass(0);
      GL.Begin(1);
      GL.Color(Col);
      for (float num = 0.0f; (double) num < 6.2831854820251465; num += G.Settings.SilentOptions.FovKalınlık)
      {
        GL.Vertex(new Vector3(Mathf.Cos(num) * Radius + Center.x, Mathf.Sin(num) * Radius + Center.y));
        GL.Vertex(new Vector3(Mathf.Cos(num + G.Settings.SilentOptions.FovKalınlık) * Radius + Center.x, Mathf.Sin(num + G.Settings.SilentOptions.FovKalınlık) * Radius + Center.y));
      }
      GL.End();
      GL.PopMatrix();
    }

    public static float radyo(float fov)
    {
      float fieldOfView = Camera.main.fieldOfView;
      return (float) (Math.Tan((double) fov * (Math.PI / 180.0) / 2.0) / Math.Tan((double) fieldOfView * (Math.PI / 180.0) / 2.0)) * (float) Screen.height;
    }

    public void OnGUI()
    {
      if (!VectorUtilities.ShouldRun())
        return;
      if (!SpyUtilities.BeingSpied)
      {
        if (G.Settings.SilentOptions.SilentAimUseFOV && G.Settings.SilentOptions.ShowSilentFOV && G.Settings.SilentOptions.Silent)
          Visual.çizmk(Color32.op_Implicit(C.GetColor("Silent_Aim_FOV_Circle")), new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), Visual.radyo((float) G.Settings.SilentOptions.SilentAimFOV));
        if (G.Settings.AimbotOptions.AimbotShowFOV && G.Settings.AimbotOptions.AimbotUseFov && G.Settings.AimbotOptions.Aimbot)
          Visual.çizmk(Color32.op_Implicit(C.GetColor("Aimlock_FOV_Circle")), new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), Visual.radyo(G.Settings.AimbotOptions.AimbotFOV));
      }
      for (int index1 = 0; index1 < Visual.EObjects.Count; ++index1)
      {
        Visual.ESPObj eobject = Visual.EObjects[index1];
        if (Object.op_Inequality((Object) eobject.GObject, (Object) null) && (!eobject.Options.Enabled || (double) Visual.GetDistance(eobject.GObject.transform.position) > (double) eobject.Options.MaxDistance))
        {
          Highlighter component = eobject.GObject.GetComponent<Highlighter>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.ConstantOffImmediate();
        }
        if (!Object.op_Equality((Object) eobject.GObject, (Object) null) && Visual.InScreenView(Camera.main.WorldToViewportPoint(eobject.GObject.transform.position)) && eobject.Options.Enabled && (double) Visual.GetDistance(eobject.GObject.transform.position) <= (double) eobject.Options.MaxDistance)
        {
          if (SpyUtilities.BeingSpied)
          {
            Highlighter component = eobject.GObject.GetComponent<Highlighter>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.ConstantOffImmediate();
            Visual.RemoveShaders(eobject.GObject);
          }
          else if ((eobject.Target != Visual.ESPObject.Player || !((SteamPlayer) eobject.Object).player.life.isDead) && (eobject.Target != Visual.ESPObject.Zombie || !((Zombie) eobject.Object).isDead) && (eobject.Target != Visual.ESPObject.Vehicle || !((InteractableVehicle) eobject.Object).isDead) && (eobject.Target != Visual.ESPObject.Vehicle || !G.Settings.GlobalOptions.OnlyUnlocked || !((InteractableVehicle) eobject.Object).isLocked) && (eobject.Target != Visual.ESPObject.Bed || !G.Settings.GlobalOptions.OnlyUnclaimed || !((InteractableBed) eobject.Object).isClaimed) && (eobject.Target != Visual.ESPObject.Item || Visual.IsItemWhitelisted((InteractableItem) eobject.Object, G.Settings.GlobalOptions)))
          {
            if (SpyUtilities.BeingSpied)
            {
              Highlighter component = eobject.GObject.GetComponent<Highlighter>();
              if (Object.op_Inequality((Object) component, (Object) null))
                component.ConstantOffImmediate();
              Visual.RemoveShaders(eobject.GObject);
            }
            else
            {
              string str1 = string.Format("<size={0}>", (object) eobject.Options.FontSize);
              string str2 = string.Format("<size={0}>", (object) eobject.Options.FontSize);
              Color32 color1 = C.GetColor("Box_Color");
              Color32 color2 = C.GetColor(Enum.GetName(typeof (Visual.ESPObject), (object) eobject.Target) + "_ESP");
              if (eobject.Options.Distance)
              {
                if ((double) Visual.GetDistance(eobject.GObject.transform.position) >= 0.0 && (double) Visual.GetDistance(eobject.GObject.transform.position) < 50.0)
                {
                  string htmlStringRgb = ColorUtility.ToHtmlStringRGB(Color.Lerp(Color.red, Color.red, Mathf.InverseLerp(0.0f, 50f, Visual.GetDistance(eobject.GObject.transform.position))));
                  str1 += string.Format("<color=white>[</color><color=#{0}>{1}</color><color=white>]</color>", (object) htmlStringRgb, (object) Visual.GetDistance(eobject.GObject.transform.position));
                  str2 += string.Format("[{0}]", (object) Visual.GetDistance(eobject.GObject.transform.position));
                }
                if ((double) Visual.GetDistance(eobject.GObject.transform.position) >= 50.0 && (double) Visual.GetDistance(eobject.GObject.transform.position) < 300.0)
                {
                  string htmlStringRgb = ColorUtility.ToHtmlStringRGB(Color.Lerp(Color.red, Color.yellow, Mathf.InverseLerp(50f, 300f, Visual.GetDistance(eobject.GObject.transform.position))));
                  str1 += string.Format("<color=white>[</color><color=#{0}>{1}</color><color=white>]</color>", (object) htmlStringRgb, (object) Visual.GetDistance(eobject.GObject.transform.position));
                  str2 += string.Format("[{0}]", (object) Visual.GetDistance(eobject.GObject.transform.position));
                }
                if ((double) Visual.GetDistance(eobject.GObject.transform.position) >= 300.0)
                {
                  string htmlStringRgb = ColorUtility.ToHtmlStringRGB(Color.Lerp(Color.yellow, Color.green, Mathf.InverseLerp(300f, 1000f, Visual.GetDistance(eobject.GObject.transform.position))));
                  str1 += string.Format("<color=white>[</color><color=#{0}>{1}</color><color=white>]</color>", (object) htmlStringRgb, (object) Visual.GetDistance(eobject.GObject.transform.position));
                  str2 += string.Format("[{0}]", (object) Visual.GetDistance(eobject.GObject.transform.position));
                }
              }
              switch (eobject.Target)
              {
                case Visual.ESPObject.Player:
                  Player player = ((SteamPlayer) eobject.Object).player;
                  if (Visual.GetPriority(((SteamPlayer) eobject.Object).playerID.steamID.m_SteamID) == Visual.Priority.Friendly)
                    color2 = C.GetColor("Friendly_Player_ESP");
                  if (PlayersTab.IsFriendly(player))
                    color2 = C.GetColor("Friendly_Player_ESP");
                  if (eobject.Options.Name)
                  {
                    str1 += ((SteamPlayer) eobject.Object).playerID.characterName;
                    str2 += ((SteamPlayer) eobject.Object).playerID.characterName;
                  }
                  if (G.Settings.GlobalOptions.Weapon)
                  {
                    string str3 = player.equipment.asset != null ? ((SteamPlayer) eobject.Object).player.equipment.asset.itemName : "None";
                    str1 = str1 + "<color=white>\n" + str3 + "</color>";
                    str2 = str2 + "\n" + str3;
                  }
                  if (G.Settings.GlobalOptions.Skeleton)
                  {
                    Visual.DrawSkeleton(eobject.GObject.transform, Color.yellow);
                    break;
                  }
                  break;
                case Visual.ESPObject.Item:
                  if (eobject.Options.Name)
                  {
                    str1 += ((InteractableItem) eobject.Object).asset.itemName;
                    str2 += ((InteractableItem) eobject.Object).asset.itemName;
                    break;
                  }
                  break;
                case Visual.ESPObject.Bed:
                  if (eobject.Options.Name)
                  {
                    str1 += Enum.GetName(typeof (Visual.ESPObject), (object) eobject.Target);
                    str2 += Enum.GetName(typeof (Visual.ESPObject), (object) eobject.Target);
                  }
                  if (G.Settings.GlobalOptions.Claimed)
                  {
                    if (((InteractableBed) eobject.Object).isClaimed)
                    {
                      str1 += "<color=white>\nClaimed</color>";
                      str2 += "\nClaimed";
                      break;
                    }
                    str1 += "<color=white>\n</color><color=ff5a00>Unclaimed</color>";
                    str2 += "\nUnclaimed";
                    break;
                  }
                  break;
                case Visual.ESPObject.Vehicle:
                  if (eobject.Options.Name)
                  {
                    str1 += ((InteractableVehicle) eobject.Object).asset.vehicleName;
                    str2 += ((InteractableVehicle) eobject.Object).asset.vehicleName;
                  }
                  if (G.Settings.GlobalOptions.VehicleLocked)
                  {
                    if (((InteractableVehicle) eobject.Object).isLocked)
                    {
                      str1 += "<color=white>\nLocked</color>";
                      str2 += "\nLocked";
                      break;
                    }
                    str1 += "<color=white>\n</color><color=ff5a00>Unlocked</color>";
                    str2 += "\nUnlocked";
                    break;
                  }
                  break;
                case Visual.ESPObject.Storage:
                  BarricadeData barricadeData = (BarricadeData) null;
                  if (eobject.Options.Name || G.Settings.GlobalOptions.ShowLocked)
                  {
                    try
                    {
                      byte num1;
                      byte num2;
                      ushort num3;
                      ushort index2;
                      BarricadeRegion barricadeRegion;
                      if (BarricadeManager.tryGetInfo(((Component) eobject.Object).transform, ref num1, ref num2, ref num3, ref index2, ref barricadeRegion))
                        barricadeData = barricadeRegion.barricades[(int) index2];
                    }
                    catch
                    {
                    }
                  }
                  if (eobject.Options.Name)
                  {
                    string str4 = "Storage";
                    if (barricadeData != null)
                      str4 = ((Asset) barricadeData.barricade.asset).name.Replace("_", " ");
                    str1 += str4;
                    str2 += str4;
                  }
                  if (G.Settings.GlobalOptions.ShowLocked)
                  {
                    if (barricadeData != null)
                    {
                      if (barricadeData.barricade.asset.isLocked)
                      {
                        str1 += "<color=white>\nLocked</color>";
                        str2 += "\nLocked";
                        break;
                      }
                      str1 += "<color=white>\n</color><color=ff5a00>Unlocked</color>";
                      str2 += "\nUnlocked";
                      break;
                    }
                    str1 += "<color=white>\nUnknown</color>";
                    str2 += "\nUnknown";
                    break;
                  }
                  break;
                default:
                  if (eobject.Options.Name)
                  {
                    str1 += Enum.GetName(typeof (Visual.ESPObject), (object) eobject.Target);
                    str2 += Enum.GetName(typeof (Visual.ESPObject), (object) eobject.Target);
                    break;
                  }
                  break;
              }
              string text = str1 + "</size>";
              string outlinetext = str2 + "</size>";
              if (!string.IsNullOrEmpty(text))
                Visual.DrawESPLabel(eobject.GObject.transform.position, Color32.op_Implicit(color2), Color.black, text, outlinetext);
              switch (eobject.Options.BoxType)
              {
                case Visual.BoxType.Corners:
                  if (eobject.Target == Visual.ESPObject.Player)
                  {
                    Vector3 position = eobject.GObject.transform.position;
                    Vector3 localScale = eobject.GObject.transform.localScale;
                    if ((1 & 1) != 0)
                    {
                      Visual.PrepareRectangleCorners(Visual.GetRectangleLines(Camera.main, new Bounds(Vector3.op_Addition(eobject.GObject.transform.position, new Vector3(0.0f, 1.1f, 0.0f)), Vector3.op_Addition(eobject.GObject.transform.localScale, new Vector3(0.0f, 0.95f, 0.0f)))), Color32.op_Implicit(color1), Visual.GetDistance(eobject.GObject.transform.position));
                      break;
                    }
                    break;
                  }
                  Visual.PrepareRectangleCorners(Visual.GetRectangleLines(Camera.main, eobject.GObject.GetComponent<Collider>().bounds), Color32.op_Implicit(color1), Visual.GetDistance(eobject.GObject.transform.position));
                  break;
                case Visual.BoxType.Box2D:
                  if (eobject.Target == Visual.ESPObject.Player)
                  {
                    Vector3 position = eobject.GObject.transform.position;
                    Vector3 localScale = eobject.GObject.transform.localScale;
                    if ((1 & 1) != 0)
                    {
                      Visual.PrepareRectangleLines(Visual.GetRectangleLines(Camera.main, new Bounds(Vector3.op_Addition(eobject.GObject.transform.position, new Vector3(0.0f, 1.1f, 0.0f)), Vector3.op_Addition(eobject.GObject.transform.localScale, new Vector3(0.0f, 0.95f, 0.0f)))), Color32.op_Implicit(color1));
                      break;
                    }
                    break;
                  }
                  Visual.PrepareRectangleLines(Visual.GetRectangleLines(Camera.main, eobject.GObject.GetComponent<Collider>().bounds), Color32.op_Implicit(color1));
                  break;
                case Visual.BoxType.Box3D:
                  if (eobject.Target == Visual.ESPObject.Player)
                  {
                    Vector3 position = eobject.GObject.transform.position;
                    Vector3 localScale = eobject.GObject.transform.localScale;
                    Visual.Draw3DBox(new Bounds(Vector3.op_Addition(position, new Vector3(0.0f, 1.1f, 0.0f)), Vector3.op_Addition(localScale, new Vector3(0.0f, 0.95f, 0.0f))), Color32.op_Implicit(color1));
                    break;
                  }
                  Visual.Draw3DBox(eobject.GObject.GetComponent<Collider>().bounds, Color32.op_Implicit(color1));
                  break;
              }
              if (eobject.Options.Glow)
              {
                Highlighter highlighter = eobject.GObject.GetComponent<Highlighter>() ?? eobject.GObject.AddComponent<Highlighter>();
                highlighter.occluder = true;
                highlighter.overlay = true;
                highlighter.ConstantOnImmediate(Color32.op_Implicit(color2));
              }
              else
              {
                Highlighter component = eobject.GObject.GetComponent<Highlighter>();
                if (Object.op_Inequality((Object) component, (Object) null))
                  component.ConstantOffImmediate();
              }
            }
          }
        }
      }
      GL.PushMatrix();
      GL.Begin(1);
      for (int index3 = 0; index3 < Visual.DrawBuffer2.Count; ++index3)
      {
        Visual.ESPBox2 espBox2 = Visual.DrawBuffer2.Dequeue();
        GL.Color(espBox2.Color);
        Vector2[] vertices = espBox2.Vertices;
        bool flag = true;
        for (int index4 = 0; index4 < vertices.Length; ++index4)
        {
          if (index4 < vertices.Length - 1)
          {
            Vector2 vector2 = vertices[index4];
            if ((double) Vector2.Distance(vertices[index4 + 1], vector2) > (double) (Screen.width / 2))
            {
              flag = false;
              break;
            }
          }
        }
        if (flag)
        {
          for (int index5 = 0; index5 < vertices.Length; ++index5)
            GL.Vertex3(vertices[index5].x, vertices[index5].y, 0.0f);
        }
      }
      GL.End();
      GL.PopMatrix();
      Highlighter component1 = ((Component) Player.player)?.gameObject?.GetComponent<Highlighter>();
      if (!Object.op_Inequality((Object) component1, (Object) null))
        return;
      component1.ConstantOffImmediate();
    }

    public static bool IsItemWhitelisted(InteractableItem item, OtherOptions itemWhitelistObject)
    {
      return !itemWhitelistObject.filterItems || itemWhitelistObject.allowGun && item.asset is ItemGunAsset || itemWhitelistObject.allowBackpack && item.asset is ItemBackpackAsset || itemWhitelistObject.allowAmmo && (item.asset is ItemMagazineAsset || item.asset is ItemCaliberAsset) || itemWhitelistObject.allowAttachments && (item.asset is ItemBarrelAsset || item.asset is ItemOpticAsset) || itemWhitelistObject.allowClothing && item.asset is ItemClothingAsset || itemWhitelistObject.allowFuel && item.asset is ItemFuelAsset || itemWhitelistObject.allowMedical && item.asset is ItemMedicalAsset || itemWhitelistObject.allowMelee && item.asset is ItemMeleeAsset || itemWhitelistObject.allowThrowable && item.asset is ItemThrowableAsset || itemWhitelistObject.allowFoodWater && (item.asset is ItemFoodAsset || item.asset is ItemWaterAsset);
    }

    public static Vector2[] GetRectangleLines(Camera cam, Bounds b)
    {
      Vector3[] vector3Array = new Vector3[8]
      {
        cam.WorldToScreenPoint(new Vector3(((Bounds) ref b).center.x + ((Bounds) ref b).extents.x, ((Bounds) ref b).center.y + ((Bounds) ref b).extents.y, ((Bounds) ref b).center.z + ((Bounds) ref b).extents.z)),
        cam.WorldToScreenPoint(new Vector3(((Bounds) ref b).center.x + ((Bounds) ref b).extents.x, ((Bounds) ref b).center.y + ((Bounds) ref b).extents.y, ((Bounds) ref b).center.z - ((Bounds) ref b).extents.z)),
        cam.WorldToScreenPoint(new Vector3(((Bounds) ref b).center.x + ((Bounds) ref b).extents.x, ((Bounds) ref b).center.y - ((Bounds) ref b).extents.y, ((Bounds) ref b).center.z + ((Bounds) ref b).extents.z)),
        cam.WorldToScreenPoint(new Vector3(((Bounds) ref b).center.x + ((Bounds) ref b).extents.x, ((Bounds) ref b).center.y - ((Bounds) ref b).extents.y, ((Bounds) ref b).center.z - ((Bounds) ref b).extents.z)),
        cam.WorldToScreenPoint(new Vector3(((Bounds) ref b).center.x - ((Bounds) ref b).extents.x, ((Bounds) ref b).center.y + ((Bounds) ref b).extents.y, ((Bounds) ref b).center.z + ((Bounds) ref b).extents.z)),
        cam.WorldToScreenPoint(new Vector3(((Bounds) ref b).center.x - ((Bounds) ref b).extents.x, ((Bounds) ref b).center.y + ((Bounds) ref b).extents.y, ((Bounds) ref b).center.z - ((Bounds) ref b).extents.z)),
        cam.WorldToScreenPoint(new Vector3(((Bounds) ref b).center.x - ((Bounds) ref b).extents.x, ((Bounds) ref b).center.y - ((Bounds) ref b).extents.y, ((Bounds) ref b).center.z + ((Bounds) ref b).extents.z)),
        cam.WorldToScreenPoint(new Vector3(((Bounds) ref b).center.x - ((Bounds) ref b).extents.x, ((Bounds) ref b).center.y - ((Bounds) ref b).extents.y, ((Bounds) ref b).center.z - ((Bounds) ref b).extents.z))
      };
      for (int index = 0; index < vector3Array.Length; ++index)
        vector3Array[index].y = (float) Screen.height - vector3Array[index].y;
      Vector3 vector3_1 = vector3Array[0];
      Vector3 vector3_2 = vector3Array[0];
      for (int index = 1; index < vector3Array.Length; ++index)
      {
        vector3_1 = Vector3.Min(vector3_1, vector3Array[index]);
        vector3_2 = Vector3.Max(vector3_2, vector3Array[index]);
      }
      return new Vector2[4]
      {
        new Vector2(vector3_1.x, vector3_1.y),
        new Vector2(vector3_2.x, vector3_1.y),
        new Vector2(vector3_1.x, vector3_2.y),
        new Vector2(vector3_2.x, vector3_2.y)
      };
    }

    public static void PrepareRectangleLines(Vector2[] nvectors, Color c)
    {
      Visual.DrawBuffer2.Enqueue(new Visual.ESPBox2()
      {
        Color = c,
        Vertices = new Vector2[8]
        {
          nvectors[0],
          nvectors[1],
          nvectors[1],
          nvectors[3],
          nvectors[3],
          nvectors[2],
          nvectors[2],
          nvectors[0]
        }
      });
    }

    public static void PrepareRectangleCorners(Vector2[] nvectors, Color c, float distance)
    {
      float num = (float) (15.0 / ((double) distance * 0.10000000149011612));
      Visual.DrawBuffer2.Enqueue(new Visual.ESPBox2()
      {
        Color = c,
        Vertices = new Vector2[16]
        {
          nvectors[0],
          new Vector2(nvectors[0].x + num, nvectors[0].y),
          nvectors[0],
          new Vector2(nvectors[0].x, nvectors[0].y + num),
          nvectors[1],
          new Vector2(nvectors[1].x - num, nvectors[1].y),
          nvectors[1],
          new Vector2(nvectors[1].x, nvectors[1].y + num),
          nvectors[3],
          new Vector2(nvectors[3].x - num, nvectors[3].y),
          nvectors[3],
          new Vector2(nvectors[3].x, nvectors[3].y - num),
          nvectors[2],
          new Vector2(nvectors[2].x + num, nvectors[2].y),
          nvectors[2],
          new Vector2(nvectors[2].x, nvectors[2].y - num)
        }
      });
    }

    public static void DrawColorCorners(Rect rect, Color c)
    {
      GUI.skin = Asset.Skin;
      Color backgroundColor = GUI.backgroundColor;
      GUI.backgroundColor = c;
      float num = 15f;
      GUI.Button(new Rect(((Rect) ref rect).x, ((Rect) ref rect).y, num, 1f), " ", Visual.textureStyle);
      GUI.Button(new Rect(((Rect) ref rect).x, ((Rect) ref rect).y, 1f, num), " ", Visual.textureStyle);
      GUI.Button(new Rect(((Rect) ref rect).xMax - num, ((Rect) ref rect).y, num, 1f), " ", Visual.textureStyle);
      GUI.Button(new Rect(((Rect) ref rect).xMax, ((Rect) ref rect).y, 1f, num), " ", Visual.textureStyle);
      GUI.Button(new Rect(((Rect) ref rect).xMax - num, ((Rect) ref rect).yMax, num, 1f), " ", Visual.textureStyle);
      GUI.Button(new Rect(((Rect) ref rect).xMax, ((Rect) ref rect).yMax - num, 1f, num), " ", Visual.textureStyle);
      GUI.Button(new Rect(((Rect) ref rect).x, ((Rect) ref rect).yMax, num, 1f), " ", Visual.textureStyle);
      GUI.Button(new Rect(((Rect) ref rect).x, ((Rect) ref rect).yMax - num, 1f, num), " ", Visual.textureStyle);
      GUI.backgroundColor = backgroundColor;
    }

    public static void DrawColorBox(Color color, Rect Pos, int thinkness = 1)
    {
      GUI.skin = Asset.Skin;
      Color backgroundColor = GUI.backgroundColor;
      GUI.backgroundColor = color;
      GUI.Button(new Rect(((Rect) ref Pos).x, ((Rect) ref Pos).y, ((Rect) ref Pos).width, (float) thinkness), " ", Visual.textureStyle);
      GUI.Button(new Rect(((Rect) ref Pos).x + ((Rect) ref Pos).width, ((Rect) ref Pos).y, (float) thinkness, ((Rect) ref Pos).height), " ", Visual.textureStyle);
      GUI.Button(new Rect(((Rect) ref Pos).x, ((Rect) ref Pos).y, (float) thinkness, ((Rect) ref Pos).height), " ", Visual.textureStyle);
      GUI.Button(new Rect(((Rect) ref Pos).x, ((Rect) ref Pos).y + ((Rect) ref Pos).height, ((Rect) ref Pos).width, (float) thinkness), " ", Visual.textureStyle);
      GUI.backgroundColor = backgroundColor;
    }

    public static bool InScreenView(Vector3 scrnpt)
    {
      return (double) scrnpt.z > 0.0 && (double) scrnpt.x > 0.0 && (double) scrnpt.x < 1.0 && (double) scrnpt.y > 0.0 && (double) scrnpt.y < 1.0;
    }

    public static float GetDistance(Vector3 endpos)
    {
      return (float) Math.Round((double) Vector3.Distance(Player.player.look.aim.position, endpos));
    }

    public static void DrawESPLabel2(
      Vector3 worldpos,
      Color textcolor,
      Color outlinecolor,
      string text,
      Texture texture,
      string outlinetext = null)
    {
      GUIContent guiContent1 = new GUIContent(text);
      if (outlinetext == null)
        outlinetext = text;
      GUIContent guiContent2 = new GUIContent(outlinetext);
      GUIStyle label = GUI.skin.label;
      label.alignment = (TextAnchor) 4;
      Vector2 vector2 = label.CalcSize(guiContent1);
      Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldpos);
      screenPoint.y = (float) Screen.height - screenPoint.y;
      if ((double) screenPoint.z < 0.0)
        return;
      GUI.color = Color.black;
      GUI.Label(new Rect((float) ((double) screenPoint.x - (double) vector2.x / 2.0 - 1.0), screenPoint.y - 1f, vector2.x, vector2.y), guiContent2);
      GUI.Label(new Rect((float) ((double) screenPoint.x - (double) vector2.x / 2.0 + 1.0), screenPoint.y + 1f, vector2.x, vector2.y), guiContent2);
      GUI.Label(new Rect((float) ((double) screenPoint.x - (double) vector2.x / 2.0 - 1.0), screenPoint.y - 1f, vector2.x, vector2.y), guiContent2);
      GUI.Label(new Rect((float) ((double) screenPoint.x - (double) vector2.x / 2.0 + 1.0), screenPoint.y - 1f, vector2.x, vector2.y), guiContent2);
      GUI.Label(new Rect((float) ((double) screenPoint.x - (double) vector2.x / 2.0 - 1.0), screenPoint.y + 1f, vector2.x, vector2.y), guiContent2);
      GUI.color = textcolor;
      GUI.Label(new Rect(screenPoint.x - vector2.x / 2f, screenPoint.y, vector2.x, vector2.y), guiContent1);
      GUI.color = Color.white;
      GUI.DrawTexture(new Rect((float) ((double) screenPoint.x - (double) vector2.x / 2.0 - 20.0 - 5.0), screenPoint.y - 2f, 20f, 20f), texture);
      GUI.color = Main.GUIColor;
    }

    public static void DrawESPLabel(
      Vector3 worldpos,
      Color textcolor,
      Color outlinecolor,
      string text,
      string outlinetext = null)
    {
      GUIContent guiContent1 = new GUIContent(text);
      if (outlinetext == null)
        outlinetext = text;
      GUIContent guiContent2 = new GUIContent(outlinetext);
      GUIStyle label = GUI.skin.label;
      label.alignment = (TextAnchor) 4;
      Vector2 vector2 = label.CalcSize(guiContent1);
      Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldpos);
      screenPoint.y = (float) Screen.height - screenPoint.y;
      if ((double) screenPoint.z < 0.0)
        return;
      GUI.color = Color.black;
      GUI.Label(new Rect((float) ((double) screenPoint.x - (double) vector2.x / 2.0 - 1.0), screenPoint.y - 1f, vector2.x, vector2.y), guiContent2);
      GUI.Label(new Rect((float) ((double) screenPoint.x - (double) vector2.x / 2.0 + 1.0), screenPoint.y + 1f, vector2.x, vector2.y), guiContent2);
      GUI.Label(new Rect((float) ((double) screenPoint.x - (double) vector2.x / 2.0 - 1.0), screenPoint.y - 1f, vector2.x, vector2.y), guiContent2);
      GUI.Label(new Rect((float) ((double) screenPoint.x - (double) vector2.x / 2.0 + 1.0), screenPoint.y - 1f, vector2.x, vector2.y), guiContent2);
      GUI.Label(new Rect((float) ((double) screenPoint.x - (double) vector2.x / 2.0 - 1.0), screenPoint.y + 1f, vector2.x, vector2.y), guiContent2);
      GUI.color = textcolor;
      GUI.Label(new Rect(screenPoint.x - vector2.x / 2f, screenPoint.y, vector2.x, vector2.y), guiContent1);
      GUI.color = Color.white;
      GUI.color = Main.GUIColor;
    }

    public static Vector3 GetLimbPosition(Transform target, string objName)
    {
      Transform[] componentsInChildren = ((Component) ((Component) target).transform).GetComponentsInChildren<Transform>();
      Vector3 limbPosition = Vector3.zero;
      if (componentsInChildren == null)
        return limbPosition;
      foreach (Transform transform in componentsInChildren)
      {
        if (!(((Object) transform).name.Trim() != objName))
        {
          limbPosition = Vector3.op_Addition(transform.position, new Vector3(0.0f, 0.4f, 0.0f));
          break;
        }
      }
      return limbPosition;
    }

    public static void DrawSkeleton(Transform transform, Color color)
    {
      Vector3 screenPoint1 = Camera.main.WorldToScreenPoint(Vector3.op_Subtraction(Visual.GetLimbPosition(transform, "Skull"), new Vector3(0.0f, 0.4f, 0.0f)));
      screenPoint1.y = (float) Screen.height - screenPoint1.y;
      Vector3 screenPoint2 = Camera.main.WorldToScreenPoint(Vector3.op_Subtraction(Visual.GetLimbPosition(transform, "Spine"), new Vector3(0.0f, 0.4f, 0.0f)));
      screenPoint2.y = (float) Screen.height - screenPoint2.y;
      Vector3 screenPoint3 = Camera.main.WorldToScreenPoint(Vector3.op_Subtraction(Visual.GetLimbPosition(transform, "Right_Shoulder"), new Vector3(0.0f, 0.4f, 0.0f)));
      screenPoint3.y = (float) Screen.height - screenPoint3.y;
      Vector3 screenPoint4 = Camera.main.WorldToScreenPoint(Vector3.op_Subtraction(Visual.GetLimbPosition(transform, "Left_Shoulder"), new Vector3(0.0f, 0.4f, 0.0f)));
      screenPoint4.y = (float) Screen.height - screenPoint4.y;
      Vector3 screenPoint5 = Camera.main.WorldToScreenPoint(Vector3.op_Subtraction(Visual.GetLimbPosition(transform, "Right_Arm"), new Vector3(0.0f, 0.4f, 0.0f)));
      screenPoint5.y = (float) Screen.height - screenPoint5.y;
      Vector3 screenPoint6 = Camera.main.WorldToScreenPoint(Vector3.op_Subtraction(Visual.GetLimbPosition(transform, "Left_Arm"), new Vector3(0.0f, 0.4f, 0.0f)));
      screenPoint6.y = (float) Screen.height - screenPoint6.y;
      Vector3 screenPoint7 = Camera.main.WorldToScreenPoint(Vector3.op_Subtraction(Visual.GetLimbPosition(transform, "Right_Hand"), new Vector3(0.0f, 0.4f, 0.0f)));
      screenPoint7.y = (float) Screen.height - screenPoint7.y;
      Vector3 screenPoint8 = Camera.main.WorldToScreenPoint(Vector3.op_Subtraction(Visual.GetLimbPosition(transform, "Left_Hand"), new Vector3(0.0f, 0.4f, 0.0f)));
      screenPoint8.y = (float) Screen.height - screenPoint8.y;
      Vector3 screenPoint9 = Camera.main.WorldToScreenPoint(Vector3.op_Subtraction(Visual.GetLimbPosition(transform, "Left_Hip"), new Vector3(0.0f, 0.4f, 0.0f)));
      screenPoint9.y = (float) Screen.height - screenPoint9.y;
      Vector3 screenPoint10 = Camera.main.WorldToScreenPoint(Vector3.op_Subtraction(Visual.GetLimbPosition(transform, "Right_Hip"), new Vector3(0.0f, 0.4f, 0.0f)));
      screenPoint10.y = (float) Screen.height - screenPoint10.y;
      Vector3 screenPoint11 = Camera.main.WorldToScreenPoint(Vector3.op_Subtraction(Visual.GetLimbPosition(transform, "Left_Foot"), new Vector3(0.0f, 0.4f, 0.0f)));
      screenPoint11.y = (float) Screen.height - screenPoint11.y;
      Vector3 screenPoint12 = Camera.main.WorldToScreenPoint(Vector3.op_Subtraction(Visual.GetLimbPosition(transform, "Right_Foot"), new Vector3(0.0f, 0.4f, 0.0f)));
      screenPoint12.y = (float) Screen.height - screenPoint12.y;
      Vector3 screenPoint13 = Camera.main.WorldToScreenPoint(Vector3.op_Subtraction(Visual.GetLimbPosition(transform, "Left_Leg"), new Vector3(0.0f, 0.4f, 0.0f)));
      screenPoint13.y = (float) Screen.height - screenPoint13.y;
      Vector3 screenPoint14 = Camera.main.WorldToScreenPoint(Vector3.op_Subtraction(Visual.GetLimbPosition(transform, "Right_Leg"), new Vector3(0.0f, 0.4f, 0.0f)));
      screenPoint14.y = (float) Screen.height - screenPoint14.y;
      GL.PushMatrix();
      GL.Begin(1);
      Manager.DrawMaterial.SetPass(0);
      GL.Color(color);
      GL.Vertex3(screenPoint1.x, screenPoint1.y, 0.0f);
      GL.Vertex3(screenPoint2.x, screenPoint2.y, 0.0f);
      GL.Vertex3(screenPoint2.x, screenPoint2.y, 0.0f);
      GL.Vertex3(screenPoint4.x, screenPoint4.y, 0.0f);
      GL.Vertex3(screenPoint4.x, screenPoint4.y, 0.0f);
      GL.Vertex3(screenPoint6.x, screenPoint6.y, 0.0f);
      GL.Vertex3(screenPoint6.x, screenPoint6.y, 0.0f);
      GL.Vertex3(screenPoint8.x, screenPoint8.y, 0.0f);
      GL.Vertex3(screenPoint2.x, screenPoint2.y, 0.0f);
      GL.Vertex3(screenPoint3.x, screenPoint3.y, 0.0f);
      GL.Vertex3(screenPoint3.x, screenPoint3.y, 0.0f);
      GL.Vertex3(screenPoint5.x, screenPoint5.y, 0.0f);
      GL.Vertex3(screenPoint5.x, screenPoint5.y, 0.0f);
      GL.Vertex3(screenPoint7.x, screenPoint7.y, 0.0f);
      GL.Vertex3(screenPoint2.x, screenPoint2.y, 0.0f);
      GL.Vertex3(screenPoint9.x, screenPoint9.y, 0.0f);
      GL.Vertex3(screenPoint9.x, screenPoint9.y, 0.0f);
      GL.Vertex3(screenPoint13.x, screenPoint13.y, 0.0f);
      GL.Vertex3(screenPoint13.x, screenPoint13.y, 0.0f);
      GL.Vertex3(screenPoint11.x, screenPoint11.y, 0.0f);
      GL.Vertex3(screenPoint2.x, screenPoint2.y, 0.0f);
      GL.Vertex3(screenPoint10.x, screenPoint10.y, 0.0f);
      GL.Vertex3(screenPoint10.x, screenPoint10.y, 0.0f);
      GL.Vertex3(screenPoint14.x, screenPoint14.y, 0.0f);
      GL.Vertex3(screenPoint14.x, screenPoint14.y, 0.0f);
      GL.Vertex3(screenPoint12.x, screenPoint12.y, 0.0f);
      GL.End();
      GL.PopMatrix();
    }

    public static void Draw3DBox(Bounds b, Color color)
    {
      Vector3[] vector3Array = new Vector3[8]
      {
        Camera.main.WorldToScreenPoint(new Vector3(((Bounds) ref b).center.x + ((Bounds) ref b).extents.x, ((Bounds) ref b).center.y + ((Bounds) ref b).extents.y, ((Bounds) ref b).center.z + ((Bounds) ref b).extents.z)),
        Camera.main.WorldToScreenPoint(new Vector3(((Bounds) ref b).center.x + ((Bounds) ref b).extents.x, ((Bounds) ref b).center.y + ((Bounds) ref b).extents.y, ((Bounds) ref b).center.z - ((Bounds) ref b).extents.z)),
        Camera.main.WorldToScreenPoint(new Vector3(((Bounds) ref b).center.x + ((Bounds) ref b).extents.x, ((Bounds) ref b).center.y - ((Bounds) ref b).extents.y, ((Bounds) ref b).center.z + ((Bounds) ref b).extents.z)),
        Camera.main.WorldToScreenPoint(new Vector3(((Bounds) ref b).center.x + ((Bounds) ref b).extents.x, ((Bounds) ref b).center.y - ((Bounds) ref b).extents.y, ((Bounds) ref b).center.z - ((Bounds) ref b).extents.z)),
        Camera.main.WorldToScreenPoint(new Vector3(((Bounds) ref b).center.x - ((Bounds) ref b).extents.x, ((Bounds) ref b).center.y + ((Bounds) ref b).extents.y, ((Bounds) ref b).center.z + ((Bounds) ref b).extents.z)),
        Camera.main.WorldToScreenPoint(new Vector3(((Bounds) ref b).center.x - ((Bounds) ref b).extents.x, ((Bounds) ref b).center.y + ((Bounds) ref b).extents.y, ((Bounds) ref b).center.z - ((Bounds) ref b).extents.z)),
        Camera.main.WorldToScreenPoint(new Vector3(((Bounds) ref b).center.x - ((Bounds) ref b).extents.x, ((Bounds) ref b).center.y - ((Bounds) ref b).extents.y, ((Bounds) ref b).center.z + ((Bounds) ref b).extents.z)),
        Camera.main.WorldToScreenPoint(new Vector3(((Bounds) ref b).center.x - ((Bounds) ref b).extents.x, ((Bounds) ref b).center.y - ((Bounds) ref b).extents.y, ((Bounds) ref b).center.z - ((Bounds) ref b).extents.z))
      };
      for (int index = 0; index < vector3Array.Length; ++index)
        vector3Array[index].y = (float) Screen.height - vector3Array[index].y;
      GL.PushMatrix();
      GL.Begin(1);
      Manager.DrawMaterial.SetPass(0);
      GL.End();
      GL.PopMatrix();
      GL.PushMatrix();
      GL.Begin(1);
      Manager.DrawMaterial.SetPass(0);
      GL.Color(color);
      GL.Vertex3(vector3Array[0].x, vector3Array[0].y, 0.0f);
      GL.Vertex3(vector3Array[1].x, vector3Array[1].y, 0.0f);
      GL.Vertex3(vector3Array[1].x, vector3Array[1].y, 0.0f);
      GL.Vertex3(vector3Array[5].x, vector3Array[5].y, 0.0f);
      GL.Vertex3(vector3Array[5].x, vector3Array[5].y, 0.0f);
      GL.Vertex3(vector3Array[4].x, vector3Array[4].y, 0.0f);
      GL.Vertex3(vector3Array[4].x, vector3Array[4].y, 0.0f);
      GL.Vertex3(vector3Array[0].x, vector3Array[0].y, 0.0f);
      GL.Vertex3(vector3Array[2].x, vector3Array[2].y, 0.0f);
      GL.Vertex3(vector3Array[3].x, vector3Array[3].y, 0.0f);
      GL.Vertex3(vector3Array[3].x, vector3Array[3].y, 0.0f);
      GL.Vertex3(vector3Array[7].x, vector3Array[7].y, 0.0f);
      GL.Vertex3(vector3Array[7].x, vector3Array[7].y, 0.0f);
      GL.Vertex3(vector3Array[6].x, vector3Array[6].y, 0.0f);
      GL.Vertex3(vector3Array[6].x, vector3Array[6].y, 0.0f);
      GL.Vertex3(vector3Array[2].x, vector3Array[2].y, 0.0f);
      GL.Vertex3(vector3Array[2].x, vector3Array[2].y, 0.0f);
      GL.Vertex3(vector3Array[0].x, vector3Array[0].y, 0.0f);
      GL.Vertex3(vector3Array[3].x, vector3Array[3].y, 0.0f);
      GL.Vertex3(vector3Array[1].x, vector3Array[1].y, 0.0f);
      GL.Vertex3(vector3Array[7].x, vector3Array[7].y, 0.0f);
      GL.Vertex3(vector3Array[5].x, vector3Array[5].y, 0.0f);
      GL.Vertex3(vector3Array[6].x, vector3Array[6].y, 0.0f);
      GL.Vertex3(vector3Array[4].x, vector3Array[4].y, 0.0f);
      GL.End();
      GL.PopMatrix();
    }

    public static Visual.Priority GetPriority(ulong id)
    {
      Visual.Priority priority;
      G.Settings.Priority.TryGetValue(id, out priority);
      return priority;
    }

    public static void ApplyChams(Visual.ESPObj gameObject, Color vis, Color invis)
    {
      switch (gameObject.Options.ChamType)
      {
        case Visual.ShaderType.Lightening:
          Visual.Lightening(gameObject.GObject);
          break;
        case Visual.ShaderType.WireFrame:
          Visual.WireFrame(gameObject.GObject);
          break;
        case Visual.ShaderType.Xray:
          Visual.XRay(gameObject.GObject);
          break;
        case Visual.ShaderType.Flat:
          Visual.Chams(gameObject.GObject, Color32.op_Implicit(vis), Color32.op_Implicit(invis));
          break;
        default:
          Visual.RemoveShaders(gameObject.GObject);
          break;
      }
    }

    public static void Chams(GameObject pgo, Color32 VisibleColor, Color32 OccludedColor)
    {
      if (Object.op_Equality((Object) Asset.Shaders[nameof (Chams)], (Object) null))
        return;
      foreach (Renderer componentsInChild in pgo.GetComponentsInChildren<Renderer>())
      {
        Material[] materials = componentsInChild.materials;
        for (int index = 0; index < materials.Length; ++index)
        {
          materials[index].shader = Asset.Shaders[nameof (Chams)];
          materials[index].SetColor("_ColorVisible", Color32.op_Implicit(VisibleColor));
          materials[index].SetColor("_ColorBehind", Color32.op_Implicit(OccludedColor));
        }
      }
    }

    public static void WireFrame(GameObject pgo)
    {
      if (Object.op_Equality((Object) Asset.Shaders[nameof (WireFrame)], (Object) null))
        return;
      foreach (Renderer componentsInChild in pgo.GetComponentsInChildren<Renderer>())
      {
        Material[] materials = componentsInChild.materials;
        for (int index = 0; index < materials.Length; ++index)
        {
          materials[index].shader = Asset.Shaders[nameof (WireFrame)];
          materials[index].SetFloat("_WireThickness", 100f);
          materials[index].SetVector("_WireColor", new Vector4(0.0f, 1f, 0.0f, 1f));
          materials[index].SetVector("_BaseColor", new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
        }
      }
    }

    public static void Lightening(GameObject pgo)
    {
      if (Object.op_Equality((Object) Asset.Shaders[nameof (Lightening)], (Object) null))
        return;
      foreach (Renderer componentsInChild in pgo.GetComponentsInChildren<Renderer>())
      {
        Material[] materials = componentsInChild.materials;
        for (int index = 0; index < materials.Length; ++index)
        {
          materials[index].shader = Asset.Shaders[nameof (Lightening)];
          materials[index].SetTexture("_LightningTex", (Texture) Asset.Lightening2);
          materials[index].SetFloat("_Intensity", 0.5f);
          materials[index].SetFloat("_Speed", 0.5f);
        }
      }
    }

    public static void XRay(GameObject pgo)
    {
      if (Object.op_Equality((Object) Asset.Shaders[nameof (XRay)], (Object) null))
        return;
      Renderer[] componentsInChildren = pgo.GetComponentsInChildren<Renderer>();
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if (Object.op_Inequality((Object) componentsInChildren[index].material.shader, (Object) Asset.Shaders[nameof (XRay)]))
        {
          foreach (Material material in componentsInChildren[index].materials)
            material.shader = Asset.Shaders[nameof (XRay)];
        }
      }
    }

    public static void RemoveShaders(GameObject pgo)
    {
      if (Object.op_Equality((Object) Shader.Find("Standard/Clothes"), (Object) null))
        return;
      Renderer[] componentsInChildren = pgo.GetComponentsInChildren<Renderer>();
      for (int index1 = 0; index1 < componentsInChildren.Length; ++index1)
      {
        if (Object.op_Inequality((Object) componentsInChildren[index1].material.shader, (Object) Shader.Find("Standard/Clothes")))
        {
          Material[] materials = componentsInChildren[index1].materials;
          for (int index2 = 0; index2 < materials.Length; ++index2)
          {
            if (Object.op_Inequality((Object) materials[index2].shader, (Object) Shader.Find("Standard/Clothes")))
              materials[index2].shader = !((Object) materials[index2]).name.Contains("Clothes") ? Shader.Find("Standard") : Shader.Find("Standard/Clothes");
          }
        }
      }
    }

    public static IEnumerator ESPUp()
    {
      while (true)
      {
        if (VectorUtilities.ShouldRun())
        {
          List<SteamPlayer> steamPlayerList = new List<SteamPlayer>();
          List<Visual.ESPObj> espObjList = new List<Visual.ESPObj>();
          foreach (SteamPlayer client in Provider.clients)
          {
            if (client != Player.player.channel.owner)
            {
              Visual.ESPObj gameObject = new Visual.ESPObj(Visual.ESPObject.Player, (object) client, ((Component) client.player).gameObject, G.Settings.PlayerOptions);
              espObjList.Add(gameObject);
              Color invis = Color32.op_Implicit(C.GetColor("Player_Chams_Occluded_Color"));
              Color vis = Color32.op_Implicit(C.GetColor("Player_Chams_Visible_Color"));
              if (Visual.GetPriority(client.playerID.steamID.m_SteamID) == Visual.Priority.Friendly)
              {
                invis = Color32.op_Implicit(C.GetColor("Friendly_Chams_Occluded_Color"));
                vis = Color32.op_Implicit(C.GetColor("Friendly_Chams_Visible_Color"));
              }
              if (!SpyUtilities.BeingSpied)
                Visual.ApplyChams(gameObject, vis, invis);
              steamPlayerList.Add(client);
            }
          }
          if (G.Settings.ItemOptions.Enabled)
          {
            foreach (InteractableItem o in Object.FindObjectsOfType<InteractableItem>())
            {
              Visual.ESPObj espObj = new Visual.ESPObj(Visual.ESPObject.Item, (object) o, ((Component) o).gameObject, G.Settings.ItemOptions);
              espObjList.Add(espObj);
            }
          }
          if (G.Settings.FlagOptions.Enabled)
          {
            foreach (InteractableClaim o in Object.FindObjectsOfType<InteractableClaim>())
            {
              Visual.ESPObj espObj = new Visual.ESPObj(Visual.ESPObject.Flag, (object) o, ((Component) o).gameObject, G.Settings.FlagOptions);
              espObjList.Add(espObj);
            }
          }
          if (G.Settings.StorageOptions.Enabled)
          {
            foreach (InteractableStorage o in Object.FindObjectsOfType<InteractableStorage>())
            {
              Visual.ESPObj espObj = new Visual.ESPObj(Visual.ESPObject.Storage, (object) o, ((Component) o).gameObject, G.Settings.StorageOptions);
              espObjList.Add(espObj);
            }
          }
          if (G.Settings.ZombieOptions.Enabled)
          {
            foreach (Zombie o in Object.FindObjectsOfType<Zombie>())
            {
              Visual.ESPObj espObj = new Visual.ESPObj(Visual.ESPObject.Zombie, (object) o, ((Component) o).gameObject, G.Settings.ZombieOptions);
              espObjList.Add(espObj);
            }
          }
          if (G.Settings.BedOptions.Enabled)
          {
            foreach (InteractableBed o in Object.FindObjectsOfType<InteractableBed>())
            {
              Visual.ESPObj espObj = new Visual.ESPObj(Visual.ESPObject.Bed, (object) o, ((Component) o).gameObject, G.Settings.BedOptions);
              espObjList.Add(espObj);
            }
          }
          if (G.Settings.VehicleOptions.Enabled)
          {
            foreach (InteractableVehicle o in Object.FindObjectsOfType<InteractableVehicle>())
            {
              if (!G.Settings.GlobalOptions.OnlyUnlocked || !o.isLocked)
              {
                Visual.ESPObj espObj = new Visual.ESPObj(Visual.ESPObject.Vehicle, (object) o, ((Component) o).gameObject, G.Settings.VehicleOptions);
                espObjList.Add(espObj);
              }
            }
          }
          if (G.Settings.AirdropOptions.Enabled)
          {
            foreach (Carepackage o in Object.FindObjectsOfType<Carepackage>())
            {
              Visual.ESPObj espObj = new Visual.ESPObj(Visual.ESPObject.Airdrop, (object) o, ((Component) o).gameObject, G.Settings.AirdropOptions);
              espObjList.Add(espObj);
            }
          }
          if (G.Settings.SentryOptions.Enabled)
          {
            foreach (InteractableSentry o in Object.FindObjectsOfType<InteractableSentry>())
            {
              Visual.ESPObj espObj = new Visual.ESPObj(Visual.ESPObject.Sentry, (object) o, ((Component) o).gameObject, G.Settings.SentryOptions);
              espObjList.Add(espObj);
            }
          }
          if (G.Settings.NPCOptions.Enabled)
          {
            foreach (InteractableObjectNPC o in Object.FindObjectsOfType<InteractableObjectNPC>())
            {
              Visual.ESPObj espObj = new Visual.ESPObj(Visual.ESPObject.NPC, (object) o, ((Component) o).gameObject, G.Settings.NPCOptions);
              espObjList.Add(espObj);
            }
          }
          if (G.Settings.ResourcesOptions.Enabled)
          {
            for (byte index1 = 0; (int) index1 < (int) Regions.WORLD_SIZE; ++index1)
            {
              for (byte index2 = 0; (int) index2 < (int) Regions.WORLD_SIZE; ++index2)
              {
                foreach (ResourceSpawnpoint o in LevelGround.trees[(int) index1, (int) index2])
                {
                  Visual.ESPObj espObj = new Visual.ESPObj(Visual.ESPObject.Resources, (object) o, ((Component) o.model).gameObject, G.Settings.ResourcesOptions);
                  espObjList.Add(espObj);
                }
              }
            }
          }
          if (G.Settings.FarmOptions.Enabled)
          {
            foreach (InteractableFarm o in Object.FindObjectsOfType<InteractableFarm>())
            {
              Visual.ESPObj espObj = new Visual.ESPObj(Visual.ESPObject.Farm, (object) o, ((Component) o).gameObject, G.Settings.FarmOptions);
              espObjList.Add(espObj);
            }
          }
          Visual.ConnectedPlayers = steamPlayerList.ToArray();
          Visual.EObjects = espObjList;
        }
        yield return (object) new WaitForSeconds(0.5f);
      }
    }

    public enum BoxType
    {
      None,
      Corners,
      Box2D,
      Box3D,
    }

    public class ESPBox2
    {
      public Color Color;
      public Vector2[] Vertices;
    }

    public class ESPObj
    {
      public Visual.ESPObject Target;
      public object Object;
      public GameObject GObject;
      public VisualOptions Options;

      public ESPObj(Visual.ESPObject t, object o, GameObject go, VisualOptions opt)
      {
        this.Target = t;
        this.Object = o;
        this.GObject = go;
        this.Options = opt;
      }
    }

    public enum ESPObject
    {
      Player,
      Zombie,
      Item,
      Sentry,
      Bed,
      Flag,
      Vehicle,
      Storage,
      Airdrop,
      NPC,
      Farm,
      Resources,
    }

    public enum Priority
    {
      None = 1,
      Friendly = 2,
    }

    public enum ShaderType
    {
      Lightening,
      WireFrame,
      Xray,
      Flat,
      None,
    }
  }
}
