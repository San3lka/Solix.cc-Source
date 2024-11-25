// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.SpinVehicle
// <3

using SDG.NetPak;
using SDG.Unturned;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class SpinVehicle : PlayerInputPacket
  {
    public static FieldInfo positionF;
    public static FieldInfo rotationF;
    public static FieldInfo speedF;
    public static FieldInfo forwardVelocityF;
    public static FieldInfo steeringInputF;
    public static FieldInfo velocityInputF;

    [Initializer]
    public static void a()
    {
      SpinVehicle.positionF = typeof (DrivingPlayerInputPacket).GetField("position", BindingFlags.Instance | BindingFlags.Public);
      SpinVehicle.rotationF = typeof (DrivingPlayerInputPacket).GetField("rotation", BindingFlags.Instance | BindingFlags.Public);
      SpinVehicle.speedF = typeof (DrivingPlayerInputPacket).GetField("speed", BindingFlags.Instance | BindingFlags.Public);
      SpinVehicle.forwardVelocityF = typeof (DrivingPlayerInputPacket).GetField("forwardVelocity", BindingFlags.Instance | BindingFlags.Public);
      SpinVehicle.steeringInputF = typeof (DrivingPlayerInputPacket).GetField("steeringInput", BindingFlags.Instance | BindingFlags.Public);
      SpinVehicle.velocityInputF = typeof (DrivingPlayerInputPacket).GetField("velocityInput", BindingFlags.Instance | BindingFlags.Public);
    }

    [Override(typeof (DrivingPlayerInputPacket), "write", BindingFlags.Instance | BindingFlags.Public, 0)]
    public void OV_write(NetPakWriter writer)
    {
      this.write(writer);
      if (G.Settings.MiscOptions.VehicleSpinbot)
      {
        UnityNetPakWriterEx.WriteClampedVector3(writer, (Vector3) SpinVehicle.positionF.GetValue((object) this), 13, 8);
        UnityNetPakWriterEx.WriteQuaternion(writer, new Quaternion((float) Random.Range(10, 280), (float) Random.Range(10, 280), (float) Random.Range(10, 280), (float) Random.Range(10, 280)), 11);
        SystemNetPakWriterEx.WriteUnsignedClampedFloat(writer, (float) SpinVehicle.speedF.GetValue((object) this), 8, 2);
        SystemNetPakWriterEx.WriteClampedFloat(writer, (float) SpinVehicle.forwardVelocityF.GetValue((object) this), 9, 2);
        SystemNetPakWriterEx.WriteSignedNormalizedFloat(writer, (float) SpinVehicle.steeringInputF.GetValue((object) this), 2);
        SystemNetPakWriterEx.WriteClampedFloat(writer, (float) SpinVehicle.velocityInputF.GetValue((object) this), 9, 2);
      }
      else
      {
        UnityNetPakWriterEx.WriteClampedVector3(writer, (Vector3) SpinVehicle.positionF.GetValue((object) this), 13, 8);
        UnityNetPakWriterEx.WriteQuaternion(writer, (Quaternion) SpinVehicle.rotationF.GetValue((object) this), 11);
        SystemNetPakWriterEx.WriteUnsignedClampedFloat(writer, (float) SpinVehicle.speedF.GetValue((object) this), 8, 2);
        SystemNetPakWriterEx.WriteClampedFloat(writer, (float) SpinVehicle.forwardVelocityF.GetValue((object) this), 9, 2);
        SystemNetPakWriterEx.WriteSignedNormalizedFloat(writer, (float) SpinVehicle.steeringInputF.GetValue((object) this), 2);
        SystemNetPakWriterEx.WriteClampedFloat(writer, (float) SpinVehicle.velocityInputF.GetValue((object) this), 9, 2);
      }
    }
  }
}
