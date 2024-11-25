// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.SkinOptionList
// <3

using System.Collections.Generic;

#nullable disable
namespace kaka
{
  public class SkinOptionList
  {
    public SkinsTab.SkinType Type;
    public HashSet<Skin> Skins = new HashSet<Skin>();

    public SkinOptionList(SkinsTab.SkinType Type) => this.Type = Type;
  }
}
