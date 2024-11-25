// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.SpinBot
// <3

using SDG.NetPak;
using SDG.Unturned;
using System;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class SpinBot : PlayerInputPacket
  {
    public static FieldInfo analogF;
    public static FieldInfo clientPositionF;
    public static FieldInfo yawF;
    public static FieldInfo pitchF;
    public static bool walkSpin;
    public static float spinbotYaw;
    public static float spinbotPitch;
    public static FieldInfo inputXField = typeof (PlayerMovement).GetField("input_x", BindingFlags.Instance | BindingFlags.NonPublic);
    public static FieldInfo inputYField = typeof (PlayerMovement).GetField("input_y", BindingFlags.Instance | BindingFlags.NonPublic);
    public static byte step;

    public static int GetInputX(PlayerMovement movement)
    {
      return (int) SpinBot.inputXField.GetValue((object) movement);
    }

    public static int GetInputY(PlayerMovement movement)
    {
      return (int) SpinBot.inputYField.GetValue((object) movement);
    }

    public float NextSpinbotPitch(float increment)
    {
      SpinBot.spinbotPitch += increment;
      if ((double) SpinBot.spinbotPitch > 180.0)
        SpinBot.spinbotPitch -= 180f;
      return SpinBot.spinbotPitch;
    }

    public static float NextSpinbotYaw(float increment)
    {
      SpinBot.spinbotYaw += increment;
      if ((double) SpinBot.spinbotYaw >= 360.0)
        SpinBot.spinbotYaw -= 360f;
      return SpinBot.spinbotYaw;
    }

    public static float ReverseAngle180(float angle)
    {
      angle += 180f;
      if ((double) angle >= 360.0)
        angle -= 360f;
      return angle;
    }

    [Initializer]
    public static void a()
    {
      SpinBot.analogF = typeof (WalkingPlayerInputPacket).GetField("analog", BindingFlags.Instance | BindingFlags.Public);
      SpinBot.clientPositionF = typeof (WalkingPlayerInputPacket).GetField("clientPosition", BindingFlags.Instance | BindingFlags.Public);
      SpinBot.yawF = typeof (WalkingPlayerInputPacket).GetField("yaw", BindingFlags.Instance | BindingFlags.Public);
      SpinBot.pitchF = typeof (WalkingPlayerInputPacket).GetField("pitch", BindingFlags.Instance | BindingFlags.Public);
    }

    public static T GetPrivateVar<T>(Type script, string varName, object obj)
    {
      return (T) SpinBot.GetPrivateVar(script, varName, obj);
    }

    public static object GetPrivateVar(Type script, string varName, object obj)
    {
      BindingFlags bindingFlags = obj == null ? BindingFlags.Static : BindingFlags.Instance;
      return script.GetField(varName, BindingFlags.NonPublic | bindingFlags).GetValue(obj);
    }

    [Override(typeof (WalkingPlayerInputPacket), "write", BindingFlags.Instance | BindingFlags.Public, 0)]
    public void OV_write(NetPakWriter writer)
    {
      this.write(writer);
      if (G.Settings.MiscOptions.Spinbot)
      {
        int inputX = SpinBot.GetInputX(Player.player.movement);
        int inputY = SpinBot.GetInputY(Player.player.movement);
        if (inputX == 0 && inputY == 0)
        {
          SystemNetPakWriterEx.WriteUInt8(writer, (byte) SpinBot.analogF.GetValue((object) this));
          UnityNetPakWriterEx.WriteClampedVector3(writer, (Vector3) SpinBot.clientPositionF.GetValue((object) this), 13, 7);
          SystemNetPakWriterEx.WriteFloat(writer, SpinBot.NextSpinbotYaw(180f));
          SystemNetPakWriterEx.WriteFloat(writer, 0.0f);
        }
        else if (!SpinBot.walkSpin)
        {
          int num1 = inputY * -1;
          int num2 = inputX * -1;
          SystemNetPakWriterEx.WriteUInt8(writer, (byte) ((uint) (byte) (num2 + 1) << 4 | (uint) (byte) (num1 + 1)));
          UnityNetPakWriterEx.WriteClampedVector3(writer, (Vector3) SpinBot.clientPositionF.GetValue((object) this), 13, 7);
          SystemNetPakWriterEx.WriteFloat(writer, SpinBot.ReverseAngle180(Player.player.look.yaw));
          SystemNetPakWriterEx.WriteFloat(writer, 0.0f);
          SpinBot.walkSpin = true;
        }
        else
        {
          SystemNetPakWriterEx.WriteUInt8(writer, (byte) SpinBot.analogF.GetValue((object) this));
          UnityNetPakWriterEx.WriteClampedVector3(writer, (Vector3) SpinBot.clientPositionF.GetValue((object) this), 13, 7);
          SystemNetPakWriterEx.WriteFloat(writer, (float) SpinBot.yawF.GetValue((object) this));
          SystemNetPakWriterEx.WriteFloat(writer, (float) SpinBot.pitchF.GetValue((object) this));
          SpinBot.walkSpin = false;
        }
      }
      else
      {
        SystemNetPakWriterEx.WriteUInt8(writer, (byte) SpinBot.analogF.GetValue((object) this));
        UnityNetPakWriterEx.WriteClampedVector3(writer, (Vector3) SpinBot.clientPositionF.GetValue((object) this), 13, 7);
        SystemNetPakWriterEx.WriteFloat(writer, (float) SpinBot.yawF.GetValue((object) this));
        SystemNetPakWriterEx.WriteFloat(writer, (float) SpinBot.pitchF.GetValue((object) this));
      }
    }
  }
}
