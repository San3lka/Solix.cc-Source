// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.OtherOptions
// <3

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class OtherOptions
  {
    public bool Claimed = true;
    public bool OnlyUnclaimed;
    public bool filterItems;
    public bool allowGun;
    public bool allowMelee;
    public bool allowBackpack;
    public bool allowClothing;
    public bool allowFuel;
    public bool allowFoodWater;
    public bool allowAmmo;
    public bool allowMedical;
    public bool allowThrowable;
    public bool allowAttachments;
    public bool Weapon = true;
    public bool Skeleton;
    public bool ViewHitboxes = true;
    public bool VehicleLocked = true;
    public bool OnlyUnlocked;
    public bool ShowLocked = true;
    public Dictionary<string, Color32> GlobalColors = new Dictionary<string, Color32>();
    public bool NoCloud;
    public bool NoGrass;
    public bool GPS;
    public bool NoSnow;
    public bool NoRain;
    public bool NoTree;
    public bool NoFog;
    public bool NoFlinch;
    public bool NoGrayscale;
    public bool ShowPlayerOnMap;
    public bool NightVision;
    public bool NightVision2;
    public bool Compass;
    public bool AutoWalk;
    public Dictionary<string, KeyCode> Hotkeyd = new Dictionary<string, KeyCode>();
  }
}
