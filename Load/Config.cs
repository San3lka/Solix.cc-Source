// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.Config
// <3

using System.Collections.Generic;

#nullable disable
namespace kaka
{
  public class Config
  {
    public VisualOptions BedOptions = new VisualOptions();
    public VisualOptions PlayerOptions = new VisualOptions();
    public VisualOptions ItemOptions = new VisualOptions();
    public VisualOptions StorageOptions = new VisualOptions();
    public VisualOptions VehicleOptions = new VisualOptions();
    public VisualOptions AirdropOptions = new VisualOptions();
    public VisualOptions SentryOptions = new VisualOptions();
    public VisualOptions ZombieOptions = new VisualOptions();
    public VisualOptions FlagOptions = new VisualOptions();
    public VisualOptions NPCOptions = new VisualOptions();
    public VisualOptions FarmOptions = new VisualOptions();
    public VisualOptions ResourcesOptions = new VisualOptions();
    public OtherOptions GlobalOptions = new OtherOptions();
    public SilentOptions SilentOptions = new SilentOptions();
    public AimbotOptions AimbotOptions = new AimbotOptions();
    public MiscOptions MiscOptions = new MiscOptions();
    public SkinOptions SkinOptions = new SkinOptions();
    public Dictionary<ulong, Visual.Priority> Priority = new Dictionary<ulong, Visual.Priority>();
  }
}
