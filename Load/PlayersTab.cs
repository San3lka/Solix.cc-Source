// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.PlayersTab
// <3

using SDG.Unturned;
using System;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class PlayersTab
  {
    public static SteamPlayer selectedplayer = (SteamPlayer) null;
    public static string SearchString = "";
    private static Vector2 PlayerScroll = new Vector2(0.0f, 0.0f);

    public static bool IsFriendly(Player player)
    {
      return player.quests.isMemberOfGroup(Player.player.quests.groupID) || Visual.GetPriority(player.channel.owner.playerID.steamID.m_SteamID) == Visual.Priority.Friendly;
    }

    public static void Tab()
    {
      GUILayout.Space(0.0f);
      GUILayout.BeginArea(new Rect(335f, 10f, 660f, 90f), "<b>Search Tab</b>", GUIStyle.op_Implicit("box"));
      PlayersTab.SearchString = GUI.TextField(new Rect(15f, 40f, 630f, 80f), PlayersTab.SearchString);
      GUILayout.EndArea();
      GUILayout.BeginArea(new Rect(335f, 105f, 350f, 530f), "<b>Players</b>", GUIStyle.op_Implicit("box"));
      PlayersTab.PlayerScroll = GUILayout.BeginScrollView(PlayersTab.PlayerScroll, Array.Empty<GUILayoutOption>());
      for (int index = 0; index < Provider.clients.Count; ++index)
      {
        SteamPlayer client = Provider.clients[index];
        if (!Object.op_Equality((Object) client.player, (Object) Player.player))
        {
          if (PlayersTab.selectedplayer == client)
          {
            if (!G.Settings.Priority.ContainsKey(client.playerID.steamID.m_SteamID))
              G.Settings.Priority.Add(client.playerID.steamID.m_SteamID, Visual.Priority.None);
            Visual.Priority priority;
            G.Settings.Priority.TryGetValue(client.playerID.steamID.m_SteamID, out priority);
            string str = "";
            if (priority == Visual.Priority.Friendly || PlayersTab.IsFriendly(client.player))
              str = str + "<color=#" + C.ColorToHex(C.GetColor("Friendly_Player_ESP")) + ">[FRIENDLY] </color>";
            if (GUILayout.Button(str + client.playerID.characterName, GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
              PlayersTab.selectedplayer = (SteamPlayer) null;
          }
          else
          {
            if (!G.Settings.Priority.ContainsKey(client.playerID.steamID.m_SteamID))
              G.Settings.Priority.Add(client.playerID.steamID.m_SteamID, Visual.Priority.None);
            Visual.Priority priority;
            G.Settings.Priority.TryGetValue(client.playerID.steamID.m_SteamID, out priority);
            string str = "";
            if (priority == Visual.Priority.Friendly || PlayersTab.IsFriendly(client.player))
              str = str + "<color=#" + C.ColorToHex(C.GetColor("Friendly_Player_ESP")) + ">[FRIENDLY] </color>";
            if (client.playerID.characterName.ToLower().Contains(PlayersTab.SearchString.ToLower()) && GUILayout.Button(str + client.playerID.characterName, GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
              PlayersTab.selectedplayer = client;
          }
        }
      }
      GUILayout.EndScrollView();
      GUILayout.EndArea();
      GUILayout.BeginArea(new Rect(695f, 105f, 300f, 530f), "<b>INFORMATION</b>", GUIStyle.op_Implicit("box"));
      for (int index = 0; index < Provider.clients.Count; ++index)
      {
        SteamPlayer client = Provider.clients[index];
        if (!Object.op_Equality((Object) client.player, (Object) Player.player) && PlayersTab.selectedplayer == client)
        {
          if (!G.Settings.Priority.ContainsKey(client.playerID.steamID.m_SteamID))
            G.Settings.Priority.Add(client.playerID.steamID.m_SteamID, Visual.Priority.None);
          Visual.Priority src;
          G.Settings.Priority.TryGetValue(client.playerID.steamID.m_SteamID, out src);
          string str1 = "";
          if (src == Visual.Priority.Friendly || PlayersTab.IsFriendly(client.player))
          {
            string str2 = str1 + "<color=#" + C.ColorToHex(C.GetColor("Friendly_Player_ESP")) + ">[FRIENDLY] </color>";
          }
          GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
          GUILayout.Label((Texture) Provider.provider.communityService.getIcon(client.playerID.steamID, false), Array.Empty<GUILayoutOption>());
          GUILayout.TextField(client.playerID.characterName + " - " + client.playerID.steamID.m_SteamID.ToString(), GUIStyle.op_Implicit("label"), Array.Empty<GUILayoutOption>());
          if (GUILayout.Button("Status: " + Enum.GetName(typeof (Visual.Priority), (object) src), GUIStyle.op_Implicit("NavBox"), Array.Empty<GUILayoutOption>()))
            G.Settings.Priority[client.playerID.steamID.m_SteamID] = src.Next<Visual.Priority>();
          GUILayout.EndVertical();
        }
      }
      GUILayout.EndArea();
    }
  }
}
