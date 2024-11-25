// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.StartCor
// <3

using UnityEngine;

#nullable disable
namespace kaka
{
  [Component]
  public class StartCor : MonoBehaviour
  {
    public void Start()
    {
      this.StartCoroutine(PlayerFinder.Game());
      this.StartCoroutine(Visual.ESPUp());
      this.StartCoroutine(HwidChanger.SetHwid());
      this.StartCoroutine(PlayerFinder.AutomaticCloseGenerator());
      this.StartCoroutine(PlayerFinder.AutomaticSitToCar());
      this.StartCoroutine(PlayerFinder.AutomaticForage());
      this.StartCoroutine(PlayerFinder.AutomaticHarvest());
      this.StartCoroutine(PlayerFinder.AutomaticStructures());
      this.StartCoroutine(PlayerFinder.AutomaticATM());
      this.StartCoroutine(PlayerFinder.AutoItemPickupFiltresiz());
      this.StartCoroutine(Aimbot.SetLockedObject());
      this.StartCoroutine(Aimbot.AimToObject());
    }
  }
}
