// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.SkinOptions
// <3

#nullable disable
namespace kaka
{
  public class SkinOptions
  {
    public SkinConfig SkinConfig = new SkinConfig();
    public SkinOptionList SkinWeapons = new SkinOptionList(SkinsTab.SkinType.Weapons);
    public SkinOptionList SkinClothesShirts = new SkinOptionList(SkinsTab.SkinType.Shirts);
    public SkinOptionList SkinClothesPants = new SkinOptionList(SkinsTab.SkinType.Pants);
    public SkinOptionList SkinClothesBackpack = new SkinOptionList(SkinsTab.SkinType.Bags);
    public SkinOptionList SkinClothesVest = new SkinOptionList(SkinsTab.SkinType.Vests);
    public SkinOptionList SkinClothesHats = new SkinOptionList(SkinsTab.SkinType.Hats);
    public SkinOptionList SkinClothesMask = new SkinOptionList(SkinsTab.SkinType.Masks);
    public SkinOptionList SkinClothesGlasses = new SkinOptionList(SkinsTab.SkinType.Glasses);
  }
}
