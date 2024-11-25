// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OverrideAttribute
// <3

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace kaka
{
  [AttributeUsage(AttributeTargets.Method)]
  public class OverrideAttribute : Attribute
  {
    public Type Class { get; private set; }

    public string MethodName { get; private set; }

    public MethodInfo Method { get; private set; }

    public BindingFlags Flags { get; private set; }

    public bool MethodFound { get; private set; }

    public OverrideAttribute(Type tClass, string method, BindingFlags flags, int index = 0)
    {
      this.Class = tClass;
      this.MethodName = method;
      this.Flags = flags;
      try
      {
        this.Method = ((IEnumerable<MethodInfo>) this.Class.GetMethods(flags)).Where<MethodInfo>((Func<MethodInfo, bool>) (a => a.Name == method)).ToArray<MethodInfo>()[index];
        this.MethodFound = true;
      }
      catch (Exception ex)
      {
        this.MethodFound = false;
      }
    }
  }
}
