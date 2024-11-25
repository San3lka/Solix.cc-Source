// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.ConfigUtilities
// <3

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace kaka
{
  public class ConfigUtilities
  {
    public static string AppdatYol = Environment.ExpandEnvironmentVariables("%appdata%");
    public static string SelectedConfig = "SolixDefault";
    private static string ConfigPath = ConfigUtilities.AppdatYol + "/Solix/Configs/";

    private static string GetConfigPath(string name = "SolixDefault")
    {
      return ConfigUtilities.ConfigPath + name + ".config";
    }

    public static void CreateEnvironment()
    {
      if (!Directory.Exists(ConfigUtilities.ConfigPath))
        Directory.CreateDirectory(ConfigUtilities.ConfigPath);
      if (!File.Exists(ConfigUtilities.GetConfigPath()))
        ConfigUtilities.SaveConfig();
      else
        ConfigUtilities.LoadConfig();
    }

    public static void SaveConfig(string name = "SolixDefault", bool setconfig = false)
    {
      File.WriteAllText(ConfigUtilities.GetConfigPath(name), JsonConvert.SerializeObject((object) G.Settings, (Formatting) 1));
      if (setconfig)
        ConfigUtilities.SelectedConfig = name;
      NotificationWindow.AddNotification("Save Configs - " + name);
    }

    public static void LoadConfig(string name = "SolixDefault")
    {
      G.Settings = JsonConvert.DeserializeObject<Config>(File.ReadAllText(ConfigUtilities.GetConfigPath(name)));
      ConfigUtilities.SelectedConfig = name;
      C.AddColors();
      Hotkeys.AddKey();
      NotificationWindow.AddNotification("Load Configs - " + name);
    }

    public static List<string> GetConfigs(bool Extensions = false)
    {
      List<string> configs = new List<string>();
      foreach (FileInfo file in new DirectoryInfo(ConfigUtilities.ConfigPath).GetFiles("*.config"))
      {
        if (Extensions)
          configs.Add(file.Name.Substring(0, file.Name.Length));
        else
          configs.Add(file.Name.Substring(0, file.Name.Length - 7));
      }
      return configs;
    }
  }
}
