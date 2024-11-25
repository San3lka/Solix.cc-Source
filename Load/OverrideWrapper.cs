// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OverrideWrapper
// <3

using System;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace kaka
{
  public class OverrideWrapper
  {
    public MethodInfo Original { get; set; }

    public MethodInfo Modified { get; set; }

    public IntPtr PtrOriginal { get; private set; }

    public IntPtr PtrModified { get; private set; }

    public OverrideUtilities.OffsetBackup OffsetBackup { get; private set; }

    public OverrideAttribute Attribute { get; set; }

    public bool Detoured { get; private set; }

    public object Instance { get; }

    public bool Local { get; private set; }

    public OverrideWrapper(
      MethodInfo original,
      MethodInfo modified,
      OverrideAttribute attribute,
      object instance = null)
    {
      try
      {
        this.Original = original;
        this.Modified = modified;
        this.Instance = instance;
        this.Attribute = attribute;
        this.Local = this.Modified.DeclaringType.Assembly == Assembly.GetExecutingAssembly();
        RuntimeHelpers.PrepareMethod(original.MethodHandle);
        RuntimeHelpers.PrepareMethod(modified.MethodHandle);
        this.PtrOriginal = this.Original.MethodHandle.GetFunctionPointer();
        this.PtrModified = this.Modified.MethodHandle.GetFunctionPointer();
        this.OffsetBackup = new OverrideUtilities.OffsetBackup(this.PtrOriginal);
        this.Detoured = false;
      }
      catch (Exception ex)
      {
      }
    }

    public bool Override()
    {
      bool flag;
      if (this.Detoured)
      {
        flag = true;
      }
      else
      {
        int num = OverrideUtilities.OverrideFunction(this.PtrOriginal, this.PtrModified) ? 1 : 0;
        if (num != 0)
          this.Detoured = true;
        flag = num != 0;
      }
      return flag;
    }

    public bool Revert()
    {
      bool flag;
      if (!this.Detoured)
      {
        flag = false;
      }
      else
      {
        int num = OverrideUtilities.RevertOverride(this.OffsetBackup) ? 1 : 0;
        if (num != 0)
          this.Detoured = false;
        flag = num != 0;
      }
      return flag;
    }

    public object CallOriginal(object[] args, object instance = null)
    {
      this.Revert();
      object obj = this.Original.Invoke(instance ?? this.Instance, args);
      this.Override();
      return obj;
    }
  }
}
