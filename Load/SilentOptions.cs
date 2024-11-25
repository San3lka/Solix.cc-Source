// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.SilentOptions
// <3

using SDG.Unturned;

#nullable disable
namespace kaka
{
  public class SilentOptions
  {
    public bool Silent;
    public bool PunchSilentAim;
    public bool ExtendMeleeRange;
    public bool SilentAimUseFOV = true;
    public bool ShowSilentFOV = true;
    public bool SafeZone;
    public bool Admin;
    public bool UseRandomLimb;
    public float SphereRadius = 13f;
    public bool SpherePrediction;
    public bool TargetPlayers = true;
    public bool TargetBeds;
    public bool TargetZombies;
    public bool TargetAnimal;
    public bool TargetGenerators;
    public bool TargetClaimFlags;
    public bool TargetVehicles;
    public bool TargetStorage;
    public bool TargetSentries;
    public bool TargetTrees;
    public bool TargetFarm;
    public bool TargetYourSelf;
    public int FovMethod;
    public float FovKalınlık = 0.01f;
    public int SilentAimFOV = 20;
    public ELimb TargetLimb = (ELimb) 13;
  }
}
