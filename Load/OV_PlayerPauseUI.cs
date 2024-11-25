// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OV_PlayerPauseUI
// <3

using SDG.Unturned;
using System.Reflection;

#nullable disable
namespace kaka
{
  public static class OV_PlayerPauseUI
  {
    [Override(typeof (PlayerPauseUI), "onClickedExitButton", BindingFlags.Static | BindingFlags.NonPublic, 0)]
    public static void OV_onClickedExitButton(ISleekElement button) => Provider.disconnect();

    [Override(typeof (PlayerPauseUI), "onClickedQuitButton", BindingFlags.Static | BindingFlags.NonPublic, 0)]
    public static void OV_onClickedQuitButton(ISleekElement button) => Provider.QuitGame("");

    [Override(typeof (PlayerPauseUI), "onClickedSuicideButton", BindingFlags.Static | BindingFlags.NonPublic, 0)]
    public static void OV_onClickedSuicideButton(ISleekElement button)
    {
      PlayerPauseUI.closeAndGotoAppropriateHUD();
      Player.player.life.sendSuicide();
    }
  }
}
