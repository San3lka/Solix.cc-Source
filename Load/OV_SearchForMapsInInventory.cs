// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OV_SearchForMapsInInventory
// <3

using SDG.Unturned;
using Steamworks;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace kaka
{
  public static class OV_SearchForMapsInInventory
  {
    public static FieldInfo fieldInfo_0;
    public static FieldInfo fieldInfo_1;
    public static FieldInfo fieldInfo_2;
    public static FieldInfo fieldInfo_3;
    public static FieldInfo fieldInfo_4;
    public static FieldInfo fieldInfo_5;
    public static MethodInfo methodInfo_0;
    public static FieldInfo RemotePlayerImagesField;
    public static FieldInfo MarkerImagesField;
    public static FieldInfo MapMarkersContainerField;
    public static FieldInfo MapRemotePlayersContainerField;
    public static FieldInfo ShowPlayerAvatarsToggleField;
    public static FieldInfo ShowPlayerNamesToggleField;
    public static MethodInfo ProjectWorldPositionToMapMethod;

    [Override(typeof (PlayerDashboardInformationUI), "searchForMapsInInventory", BindingFlags.Static | BindingFlags.NonPublic, 0)]
    public static void OV_searchForMapsInInventory(ref bool enableChart, ref bool enableMap)
    {
      if (G.Settings.GlobalOptions.GPS || !SpyUtilities.BeingSpied)
      {
        enableMap = true;
        enableChart = true;
      }
      else
        OverrideUtilities.CallOriginal((object) null, (object) true, (object) true);
    }

    public static List<ISleekImage> remotePlayerImages
    {
      get
      {
        return OV_SearchForMapsInInventory.RemotePlayerImagesField.GetValue((object) null) as List<ISleekImage>;
      }
    }

    [Thread]
    public static void Init()
    {
      OV_SearchForMapsInInventory.RemotePlayerImagesField = typeof (PlayerDashboardInformationUI).GetField("remotePlayerImages", BindingFlags.Static | BindingFlags.NonPublic);
      OV_SearchForMapsInInventory.MarkerImagesField = typeof (PlayerDashboardInformationUI).GetField("markerImages", BindingFlags.Static | BindingFlags.NonPublic);
      OV_SearchForMapsInInventory.MapMarkersContainerField = typeof (PlayerDashboardInformationUI).GetField("mapMarkersContainer", BindingFlags.Static | BindingFlags.NonPublic);
      OV_SearchForMapsInInventory.MapRemotePlayersContainerField = typeof (PlayerDashboardInformationUI).GetField("mapRemotePlayersContainer", BindingFlags.Static | BindingFlags.NonPublic);
      OV_SearchForMapsInInventory.ShowPlayerAvatarsToggleField = typeof (PlayerDashboardInformationUI).GetField("showPlayerAvatarsToggle", BindingFlags.Static | BindingFlags.NonPublic);
      OV_SearchForMapsInInventory.ShowPlayerNamesToggleField = typeof (PlayerDashboardInformationUI).GetField("showPlayerNamesToggle", BindingFlags.Static | BindingFlags.NonPublic);
      OV_SearchForMapsInInventory.ProjectWorldPositionToMapMethod = typeof (PlayerDashboardInformationUI).GetMethod("ProjectWorldPositionToMap", BindingFlags.Static | BindingFlags.NonPublic);
    }

    [Override(typeof (PlayerDashboardInformationUI), "updateRemotePlayerAvatars", BindingFlags.Static | BindingFlags.NonPublic, 0)]
    private static void OV_updateRemotePlayerAvatars()
    {
      int index1 = 0;
      bool specStatsVisible = Player.player.look.areSpecStatsVisible;
      foreach (SteamPlayer client in Provider.clients)
      {
        if (!Object.op_Equality((Object) client.model, (Object) null) && !CSteamID.op_Equality(client.playerID.steamID, Provider.client))
        {
          bool flag = client.player.quests.isMemberOfSameGroupAs(Player.player);
          if (specStatsVisible | flag || G.Settings.GlobalOptions.ShowPlayerOnMap && !SpyUtilities.BeingSpied)
          {
            ISleekImage isleekImage;
            if (index1 < OV_SearchForMapsInInventory.remotePlayerImages.Count)
            {
              isleekImage = OV_SearchForMapsInInventory.remotePlayerImages[index1];
              ((ISleekElement) isleekImage).IsVisible = true;
            }
            else
            {
              isleekImage = Glazier.Get().CreateImage();
              ((ISleekElement) isleekImage).PositionOffset_X = -10f;
              ((ISleekElement) isleekImage).PositionOffset_Y = -10f;
              ((ISleekElement) isleekImage).SizeOffset_X = 20f;
              ((ISleekElement) isleekImage).SizeOffset_Y = 20f;
              ((ISleekElement) isleekImage).AddLabel(string.Empty, (ESleekSide) 1);
              (OV_SearchForMapsInInventory.MapRemotePlayersContainerField.GetValue((object) null) as ISleekElement).AddChild((ISleekElement) isleekImage);
              OV_SearchForMapsInInventory.remotePlayerImages.Add(isleekImage);
            }
            ++index1;
            Vector2 vector2 = (Vector2) OV_SearchForMapsInInventory.ProjectWorldPositionToMapMethod.Invoke((object) null, new object[1]
            {
              (object) ((Component) client.player).transform.position
            });
            ((ISleekElement) isleekImage).PositionScale_X = vector2.x;
            ((ISleekElement) isleekImage).PositionScale_Y = vector2.y;
            isleekImage.Texture = OptionsSettings.streamer || !(OV_SearchForMapsInInventory.ShowPlayerAvatarsToggleField.GetValue((object) null) as ISleekToggle).Value ? (Texture) null : (Texture) Provider.provider.communityService.getIcon(client.playerID.steamID, true);
            if ((OV_SearchForMapsInInventory.ShowPlayerNamesToggleField.GetValue((object) null) as ISleekToggle).Value)
            {
              if (flag && !string.IsNullOrEmpty(client.playerID.nickName))
                ((ISleekElement) isleekImage).UpdateLabel(client.playerID.nickName);
              else
                ((ISleekElement) isleekImage).UpdateLabel(client.playerID.characterName);
            }
            else
              ((ISleekElement) isleekImage).UpdateLabel(string.Empty);
          }
        }
      }
      for (int index2 = OV_SearchForMapsInInventory.remotePlayerImages.Count - 1; index2 >= index1; --index2)
        ((ISleekElement) OV_SearchForMapsInInventory.remotePlayerImages[index2]).IsVisible = false;
    }
  }
}
