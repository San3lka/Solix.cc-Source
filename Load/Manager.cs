// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.Manager
// <3

using Load;
using System;
using System.Reflection;
using System.Threading;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class Manager : MonoBehaviour
  {
    public static Material DrawMaterial;

    public void Start()
    {
      Material material = new Material(Shader.Find("Hidden/Internal-Colored"));
      ((Object) material).hideFlags = (HideFlags) 61;
      Manager.DrawMaterial = material;
      Manager.DrawMaterial.SetInt("_SrcBlend", 5);
      Manager.DrawMaterial.SetInt("_DstBlend", 10);
      Manager.DrawMaterial.SetInt("_Cull", 0);
      Manager.DrawMaterial.SetInt("_ZWrite", 0);
      Asset.Get();
      Hotkeys.AddKey();
      C.AddColors();
      foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
      {
        if (type.IsDefined(typeof (ComponentAttribute), false))
          Loadc.obj.AddComponent(type);
        foreach (MethodInfo method in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
        {
          MethodInfo M = method;
          if (M.IsDefined(typeof (InitializerAttribute), false))
            M.Invoke((object) null, (object[]) null);
          if (M.IsDefined(typeof (OverrideAttribute), false))
            new OverrideManager().LoadOverride(M);
          if (M.IsDefined(typeof (ThreadAttribute), false))
            new Thread((ThreadStart) (() => M.Invoke((object) null, (object[]) null))).Start();
        }
      }
      ConfigUtilities.CreateEnvironment();
    }
  }
}
