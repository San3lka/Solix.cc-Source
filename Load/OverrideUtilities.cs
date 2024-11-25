// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OverrideUtilities
// <3

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

#nullable disable
namespace kaka
{
  public static class OverrideUtilities
  {
    public static object CallOriginal(object instance = null, params object[] args)
    {
      OverrideManager overrideManager = new OverrideManager();
      StackTrace stackTrace = new StackTrace(false);
      MethodBase element = stackTrace.FrameCount >= 1 ? stackTrace.GetFrame(1).GetMethod() : throw new Exception("Invalid trace back to the original method! Please provide the methodinfo instead!");
      MethodInfo original = (MethodInfo) null;
      if (!Attribute.IsDefined((MemberInfo) element, typeof (OverrideAttribute)))
        element = stackTrace.GetFrame(2).GetMethod();
      OverrideAttribute customAttribute = (OverrideAttribute) Attribute.GetCustomAttribute((MemberInfo) element, typeof (OverrideAttribute));
      if (customAttribute == null)
        throw new Exception("This method can only be called from an overwritten method!");
      original = customAttribute.MethodFound ? customAttribute.Method : throw new Exception("The original method was never found!");
      if (overrideManager.Overrides.All<KeyValuePair<OverrideAttribute, OverrideWrapper>>((Func<KeyValuePair<OverrideAttribute, OverrideWrapper>, bool>) (o => o.Value.Original != original)))
        throw new Exception("The Override specified was not found!");
      return overrideManager.Overrides.First<KeyValuePair<OverrideAttribute, OverrideWrapper>>((Func<KeyValuePair<OverrideAttribute, OverrideWrapper>, bool>) (a => a.Value.Original == original)).Value.CallOriginal(args, instance);
    }

    public static bool EnableOverride(MethodInfo method)
    {
      OverrideWrapper overrideWrapper = new OverrideManager().Overrides.First<KeyValuePair<OverrideAttribute, OverrideWrapper>>((Func<KeyValuePair<OverrideAttribute, OverrideWrapper>, bool>) (a => a.Value.Original == method)).Value;
      return overrideWrapper != null && overrideWrapper.Override();
    }

    public static bool DisableOverride(MethodInfo method)
    {
      OverrideWrapper overrideWrapper = new OverrideManager().Overrides.First<KeyValuePair<OverrideAttribute, OverrideWrapper>>((Func<KeyValuePair<OverrideAttribute, OverrideWrapper>, bool>) (a => a.Value.Original == method)).Value;
      return overrideWrapper != null && overrideWrapper.Revert();
    }

    public static unsafe bool OverrideFunction(IntPtr ptrOriginal, IntPtr ptrModified)
    {
      try
      {
        switch (IntPtr.Size)
        {
          case 4:
            byte* pointer1 = (byte*) ptrOriginal.ToPointer();
            *pointer1 = (byte) 104;
            *(int*) (pointer1 + 1) = ptrModified.ToInt32();
            pointer1[5] = (byte) 195;
            break;
          case 8:
            byte* pointer2 = (byte*) ptrOriginal.ToPointer();
            *pointer2 = (byte) 72;
            pointer2[1] = (byte) 184;
            *(long*) (pointer2 + 2) = ptrModified.ToInt64();
            pointer2[10] = byte.MaxValue;
            pointer2[11] = (byte) 224;
            break;
          default:
            return false;
        }
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public static unsafe bool RevertOverride(OverrideUtilities.OffsetBackup backup)
    {
      try
      {
        byte* pointer = (byte*) backup.Method.ToPointer();
        *pointer = backup.A;
        pointer[1] = backup.B;
        pointer[10] = backup.C;
        pointer[11] = backup.D;
        pointer[12] = backup.E;
        if (IntPtr.Size == 4)
        {
          *(int*) (pointer + 1) = (int) backup.F32;
          pointer[5] = backup.G;
        }
        else
          *(long*) (pointer + 2) = (long) backup.F64;
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public class OffsetBackup
    {
      public IntPtr Method;
      public byte A;
      public byte B;
      public byte C;
      public byte D;
      public byte E;
      public byte G;
      public ulong F64;
      public uint F32;

      public unsafe OffsetBackup(IntPtr method)
      {
        this.Method = method;
        byte* pointer = (byte*) method.ToPointer();
        this.A = *pointer;
        this.B = pointer[1];
        this.C = pointer[10];
        this.D = pointer[11];
        this.E = pointer[12];
        if (IntPtr.Size == 4)
        {
          this.F32 = *(uint*) (pointer + 1);
          this.G = pointer[5];
        }
        else
          this.F64 = (ulong) *(long*) (pointer + 2);
      }
    }
  }
}
