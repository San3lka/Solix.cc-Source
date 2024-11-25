// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OverrideManager
// <3

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace kaka
{
  public class OverrideManager
  {
    public static Dictionary<OverrideAttribute, OverrideWrapper> _overrides = new Dictionary<OverrideAttribute, OverrideWrapper>();

    public Dictionary<OverrideAttribute, OverrideWrapper> Overrides => OverrideManager._overrides;

    public void OffHook()
    {
      foreach (OverrideWrapper overrideWrapper in this.Overrides.Values)
        overrideWrapper.Revert();
    }

    public void LoadOverride(MethodInfo method)
    {
      try
      {
        OverrideAttribute attribute = (OverrideAttribute) Attribute.GetCustomAttribute((MemberInfo) method, typeof (OverrideAttribute));
        if (this.Overrides.Count<KeyValuePair<OverrideAttribute, OverrideWrapper>>((Func<KeyValuePair<OverrideAttribute, OverrideWrapper>, bool>) (a => a.Key.Method == attribute.Method)) > 0)
          return;
        OverrideWrapper overrideWrapper = new OverrideWrapper(attribute.Method, method, attribute);
        overrideWrapper.Override();
        this.Overrides.Add(attribute, overrideWrapper);
      }
      catch
      {
      }
    }
  }
}
