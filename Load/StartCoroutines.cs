// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.StartCoroutines
// <3

using UnityEngine;

#nullable disable
namespace kaka
{
  [Component]
  public class StartCoroutines : MonoBehaviour
  {
    public void Start() => this.StartCoroutine(SilentCoroutines.UpdateObjects());
  }
}
