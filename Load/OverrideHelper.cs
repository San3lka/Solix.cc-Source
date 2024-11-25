// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OverrideHelper
// <3

using System;
using System.Reflection;
using System.Runtime.InteropServices;

#nullable disable
namespace kaka
{
  public class OverrideHelper
  {
    [DllImport("mono.dll", CallingConvention = CallingConvention.FastCall)]
    private static extern IntPtr mono_domain_get();

    [DllImport("mono.dll", CallingConvention = CallingConvention.FastCall)]
    private static extern IntPtr mono_method_get_header(IntPtr method);

    public static void RedirectCalls(MethodInfo from, MethodInfo to)
    {
      RuntimeMethodHandle methodHandle = from.MethodHandle;
      IntPtr functionPointer1 = methodHandle.GetFunctionPointer();
      methodHandle = to.MethodHandle;
      IntPtr functionPointer2 = methodHandle.GetFunctionPointer();
      OverrideHelper.PatchJumpTo(functionPointer1, functionPointer2);
    }

    private static unsafe void RedirectCall(MethodInfo from, MethodInfo to)
    {
      IntPtr num1 = from.MethodHandle.Value;
      IntPtr num2 = to.MethodHandle.Value;
      from.MethodHandle.GetFunctionPointer();
      to.MethodHandle.GetFunctionPointer();
      IntPtr num3 = (IntPtr) OverrideHelper.mono_domain_get().ToPointer() + 232;
      long** numPtr1 = (long**) *(IntPtr*) (num3 + 32);
      uint num4 = *(uint*) (num3 + 24);
      void* voidPtr1 = (void*) null;
      void* voidPtr2 = (void*) null;
      long int64_1 = num1.ToInt64();
      uint num5 = (uint) int64_1 >> 3;
      for (long* numPtr2 = (long*) *(IntPtr*) ((IntPtr) numPtr1 + (IntPtr) ((long) (num5 % num4) * (long) sizeof (long*))); (IntPtr) numPtr2 != IntPtr.Zero; numPtr2 = (long*) *(IntPtr*) (numPtr2 + 1))
      {
        if (int64_1 == *numPtr2)
        {
          voidPtr1 = (void*) numPtr2;
          break;
        }
      }
      long int64_2 = num2.ToInt64();
      uint num6 = (uint) int64_2 >> 3;
      for (long* numPtr3 = (long*) *(IntPtr*) ((IntPtr) numPtr1 + (IntPtr) ((long) (num6 % num4) * (long) sizeof (long*))); (IntPtr) numPtr3 != IntPtr.Zero; numPtr3 = (long*) *(IntPtr*) (numPtr3 + 1))
      {
        if (int64_2 == *numPtr3)
        {
          voidPtr2 = (void*) numPtr3;
          break;
        }
      }
      if ((IntPtr) voidPtr1 == IntPtr.Zero || (IntPtr) voidPtr2 == IntPtr.Zero)
        return;
      ulong* numPtr4 = (ulong*) voidPtr1;
      ulong* numPtr5 = (ulong*) voidPtr2;
      numPtr4[2] = numPtr5[2];
      numPtr4[3] = numPtr5[3];
    }

    private static unsafe void PatchJumpTo(IntPtr site, IntPtr target)
    {
      byte* pointer = (byte*) site.ToPointer();
      *pointer = (byte) 73;
      pointer[1] = (byte) 187;
      *(long*) (pointer + 2) = target.ToInt64();
      pointer[10] = (byte) 65;
      pointer[11] = byte.MaxValue;
      pointer[12] = (byte) 227;
    }

    private static unsafe void RedirectCallIL(MethodInfo from, MethodInfo to)
    {
      IntPtr num = from.MethodHandle.Value;
      IntPtr method = to.MethodHandle.Value;
      OverrideHelper.mono_method_get_header(method);
      *(IntPtr*) ((IntPtr) num.ToPointer() + 40) = *(IntPtr*) ((IntPtr) method.ToPointer() + 40);
    }
  }
}
