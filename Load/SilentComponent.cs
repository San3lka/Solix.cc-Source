// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.SilentComponent
// <3

using SDG.Unturned;
using System.Collections;
using UnityEngine;

#nullable disable
namespace kaka
{
  [DisallowMultipleComponent]
  public class SilentComponent : MonoBehaviour
  {
    public GameObject Sphere;

    public void Awake()
    {
      this.StartCoroutine(this.RedoSphere());
      this.StartCoroutine(this.CalcSphere());
    }

    public IEnumerator CalcSphere()
    {
      SilentComponent silentComponent = this;
      while (true)
      {
        do
        {
          yield return (object) new WaitForSeconds(0.5f);
        }
        while (!Object.op_Implicit((Object) silentComponent.Sphere));
        Rigidbody component = ((Component) silentComponent).gameObject.GetComponent<Rigidbody>();
        if (Object.op_Implicit((Object) component))
        {
          double ping = (double) Provider.ping;
          Vector3 velocity = component.velocity;
          double magnitude = (double) ((Vector3) ref velocity).magnitude;
          float num = (float) (1.0 - ping * magnitude * 2.0);
          silentComponent.Sphere.transform.localScale = new Vector3(num, num, num);
        }
      }
    }

    public IEnumerator RedoSphere()
    {
      SilentComponent silentComponent = this;
      while (true)
      {
        GameObject sphere = silentComponent.Sphere;
        silentComponent.Sphere = SilentSphere.Create("HitSphere", G.Settings.SilentOptions.SpherePrediction ? 15.5f : G.Settings.SilentOptions.SphereRadius, 2f);
        silentComponent.Sphere.layer = 24;
        silentComponent.Sphere.transform.parent = ((Component) silentComponent).transform;
        silentComponent.Sphere.transform.localPosition = Vector3.zero;
        Object.Destroy((Object) sphere);
        yield return (object) new WaitForSeconds(0.5f);
      }
    }
  }
}
