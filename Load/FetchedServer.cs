// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.FetchedServer
// <3

using Steamworks;
using System;

#nullable disable
namespace kaka
{
  public class FetchedServer
  {
    public uint serverIP;
    public ushort serverPort;
    public ushort connectionPort;
    public string name;
    public int index;
    public Action<string, float, int> onPlayersRefreshed;
    public string fetchedPlayer;
    public float fetchedPlayerPlaytime;
    public HServerQuery cachedQuery;
    public ISteamMatchmakingPlayersResponse playersResponse;
    public int atempts;

    public FetchedServer(
      uint serverIP,
      ushort serverPort,
      ushort connectionPort,
      string name,
      int index,
      Action<string, float, int> onPlayersRefreshed)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      FetchedServer.\u003C\u003Ec__DisplayClass11_0 cDisplayClass110 = new FetchedServer.\u003C\u003Ec__DisplayClass11_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass110.onPlayersRefreshed = onPlayersRefreshed;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass110.index = index;
      // ISSUE: explicit constructor call
      base.\u002Ector();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass110.\u003C\u003E4__this = this;
      this.serverIP = serverIP;
      this.serverPort = serverPort;
      this.connectionPort = connectionPort;
      this.name = name;
      // ISSUE: reference to a compiler-generated field
      this.onPlayersRefreshed = cDisplayClass110.onPlayersRefreshed;
      // ISSUE: reference to a compiler-generated field
      this.index = cDisplayClass110.index;
      this.cachedQuery = HServerQuery.Invalid;
      this.fetchedPlayer = "";
      this.fetchedPlayerPlaytime = 0.0f;
      // ISSUE: method pointer
      // ISSUE: method pointer
      // ISSUE: method pointer
      this.playersResponse = new ISteamMatchmakingPlayersResponse(new ISteamMatchmakingPlayersResponse.AddPlayerToList((object) cDisplayClass110, __methodptr(\u003C\u002Ector\u003Eb__0)), new ISteamMatchmakingPlayersResponse.PlayersFailedToRespond((object) cDisplayClass110, __methodptr(\u003C\u002Ector\u003Eb__1)), new ISteamMatchmakingPlayersResponse.PlayersRefreshComplete((object) cDisplayClass110, __methodptr(\u003C\u002Ector\u003Eb__2)));
    }

    public void Refresh()
    {
      if (this.atempts > 4)
      {
        this.cachedQuery = HServerQuery.Invalid;
        this.onPlayersRefreshed("", 0.0f, this.index);
      }
      else
      {
        ++this.atempts;
        this.cachedQuery = SteamMatchmakingServers.PlayerDetails(this.serverIP, this.serverPort, this.playersResponse);
      }
    }

    public void ForceStop()
    {
      if (!HServerQuery.op_Inequality(this.cachedQuery, HServerQuery.Invalid))
        return;
      SteamMatchmakingServers.CancelServerQuery(this.cachedQuery);
      this.cachedQuery = HServerQuery.Invalid;
    }
  }
}
