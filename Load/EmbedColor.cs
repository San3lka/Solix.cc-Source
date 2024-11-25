// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.EmbedColor
// <3

#nullable disable
namespace kaka
{
  public struct EmbedColor
  {
    public byte R;
    public byte G;
    public byte B;

    public EmbedColor(byte r, byte g, byte b)
    {
      this.R = r;
      this.G = g;
      this.B = b;
    }

    public override bool Equals(object obj)
    {
      return obj is EmbedColor embedColor && (int) embedColor.R == (int) this.R && (int) embedColor.G == (int) this.G && (int) embedColor.B == (int) this.B;
    }

    public override int GetHashCode() => base.GetHashCode();

    public static EmbedColor Transparent
    {
      get => new EmbedColor(byte.MaxValue, byte.MaxValue, byte.MaxValue);
    }

    public static EmbedColor AliceBlue => new EmbedColor((byte) 240, (byte) 248, byte.MaxValue);

    public static EmbedColor AntiqueWhite => new EmbedColor((byte) 250, (byte) 235, (byte) 215);

    public static EmbedColor Aqua => new EmbedColor((byte) 0, byte.MaxValue, byte.MaxValue);

    public static EmbedColor Aquamarine => new EmbedColor((byte) 127, byte.MaxValue, (byte) 212);

    public static EmbedColor Azure => new EmbedColor((byte) 240, byte.MaxValue, byte.MaxValue);

    public static EmbedColor Beige => new EmbedColor((byte) 245, (byte) 245, (byte) 220);

    public static EmbedColor Bisque => new EmbedColor(byte.MaxValue, (byte) 228, (byte) 196);

    public static EmbedColor Black => new EmbedColor((byte) 0, (byte) 0, (byte) 0);

    public static EmbedColor BlanchedAlmond
    {
      get => new EmbedColor(byte.MaxValue, (byte) 235, (byte) 205);
    }

    public static EmbedColor Blue => new EmbedColor((byte) 0, (byte) 0, byte.MaxValue);

    public static EmbedColor BlueViolet => new EmbedColor((byte) 138, (byte) 43, (byte) 226);

    public static EmbedColor Brown => new EmbedColor((byte) 165, (byte) 42, (byte) 42);

    public static EmbedColor BurlyWood => new EmbedColor((byte) 222, (byte) 184, (byte) 135);

    public static EmbedColor CadetBlue => new EmbedColor((byte) 95, (byte) 158, (byte) 160);

    public static EmbedColor Chartreuse => new EmbedColor((byte) 127, byte.MaxValue, (byte) 0);

    public static EmbedColor Chocolate => new EmbedColor((byte) 210, (byte) 105, (byte) 30);

    public static EmbedColor Coral => new EmbedColor(byte.MaxValue, (byte) 127, (byte) 80);

    public static EmbedColor CornflowerBlue => new EmbedColor((byte) 100, (byte) 149, (byte) 237);

    public static EmbedColor Cornsilk => new EmbedColor(byte.MaxValue, (byte) 248, (byte) 220);

    public static EmbedColor Crimson => new EmbedColor((byte) 220, (byte) 20, (byte) 60);

    public static EmbedColor Cyan => new EmbedColor((byte) 0, byte.MaxValue, byte.MaxValue);

    public static EmbedColor DarkBlue => new EmbedColor((byte) 0, (byte) 0, (byte) 139);

    public static EmbedColor DarkCyan => new EmbedColor((byte) 0, (byte) 139, (byte) 139);

    public static EmbedColor DarkGoldenrod => new EmbedColor((byte) 184, (byte) 134, (byte) 11);

    public static EmbedColor DarkGray => new EmbedColor((byte) 169, (byte) 169, (byte) 169);

    public static EmbedColor DarkGreen => new EmbedColor((byte) 0, (byte) 100, (byte) 0);

    public static EmbedColor DarkKhaki => new EmbedColor((byte) 189, (byte) 183, (byte) 107);

    public static EmbedColor DarkMagenta => new EmbedColor((byte) 139, (byte) 0, (byte) 139);

    public static EmbedColor DarkOliveGreen => new EmbedColor((byte) 85, (byte) 107, (byte) 47);

    public static EmbedColor DarkOrange => new EmbedColor(byte.MaxValue, (byte) 140, (byte) 0);

    public static EmbedColor DarkOrchid => new EmbedColor((byte) 153, (byte) 50, (byte) 204);

    public static EmbedColor DarkRed => new EmbedColor((byte) 139, (byte) 0, (byte) 0);

    public static EmbedColor DarkSalmon => new EmbedColor((byte) 233, (byte) 150, (byte) 122);

    public static EmbedColor DarkSeaGreen => new EmbedColor((byte) 143, (byte) 188, (byte) 143);

    public static EmbedColor DarkSlateBlue => new EmbedColor((byte) 72, (byte) 61, (byte) 139);

    public static EmbedColor DarkSlateGray => new EmbedColor((byte) 47, (byte) 79, (byte) 79);

    public static EmbedColor DarkTurquoise => new EmbedColor((byte) 0, (byte) 206, (byte) 209);

    public static EmbedColor DarkViolet => new EmbedColor((byte) 148, (byte) 0, (byte) 211);

    public static EmbedColor DeepPink => new EmbedColor(byte.MaxValue, (byte) 20, (byte) 147);

    public static EmbedColor DeepSkyBlue => new EmbedColor((byte) 0, (byte) 191, byte.MaxValue);

    public static EmbedColor DimGray => new EmbedColor((byte) 105, (byte) 105, (byte) 105);

    public static EmbedColor DodgerBlue => new EmbedColor((byte) 30, (byte) 144, byte.MaxValue);

    public static EmbedColor Firebrick => new EmbedColor((byte) 178, (byte) 34, (byte) 34);

    public static EmbedColor FloralWhite => new EmbedColor(byte.MaxValue, (byte) 250, (byte) 240);

    public static EmbedColor ForestGreen => new EmbedColor((byte) 34, (byte) 139, (byte) 34);

    public static EmbedColor Fuchsia => new EmbedColor(byte.MaxValue, (byte) 0, byte.MaxValue);

    public static EmbedColor Gainsboro => new EmbedColor((byte) 220, (byte) 220, (byte) 220);

    public static EmbedColor GhostWhite => new EmbedColor((byte) 248, (byte) 248, byte.MaxValue);

    public static EmbedColor Gold => new EmbedColor(byte.MaxValue, (byte) 215, (byte) 0);

    public static EmbedColor Goldenrod => new EmbedColor((byte) 218, (byte) 165, (byte) 32);

    public static EmbedColor Gray => new EmbedColor((byte) 128, (byte) 128, (byte) 128);

    public static EmbedColor Green => new EmbedColor((byte) 0, (byte) 128, (byte) 0);

    public static EmbedColor GreenYellow => new EmbedColor((byte) 173, byte.MaxValue, (byte) 47);

    public static EmbedColor Honeydew => new EmbedColor((byte) 240, byte.MaxValue, (byte) 240);

    public static EmbedColor HotPink => new EmbedColor(byte.MaxValue, (byte) 105, (byte) 180);

    public static EmbedColor IndianRed => new EmbedColor((byte) 205, (byte) 92, (byte) 92);

    public static EmbedColor Indigo => new EmbedColor((byte) 75, (byte) 0, (byte) 130);

    public static EmbedColor Ivory => new EmbedColor(byte.MaxValue, byte.MaxValue, (byte) 240);

    public static EmbedColor Khaki => new EmbedColor((byte) 240, (byte) 230, (byte) 140);

    public static EmbedColor Lavender => new EmbedColor((byte) 230, (byte) 230, (byte) 250);

    public static EmbedColor LavenderBlush => new EmbedColor(byte.MaxValue, (byte) 240, (byte) 245);

    public static EmbedColor LawnGreen => new EmbedColor((byte) 124, (byte) 252, (byte) 0);

    public static EmbedColor LemonChiffon => new EmbedColor(byte.MaxValue, (byte) 250, (byte) 205);

    public static EmbedColor LightBlue => new EmbedColor((byte) 173, (byte) 216, (byte) 230);

    public static EmbedColor LightCoral => new EmbedColor((byte) 240, (byte) 128, (byte) 128);

    public static EmbedColor LightCyan => new EmbedColor((byte) 224, byte.MaxValue, byte.MaxValue);

    public static EmbedColor LightGoldenrodYellow
    {
      get => new EmbedColor((byte) 250, (byte) 250, (byte) 210);
    }

    public static EmbedColor LightGreen => new EmbedColor((byte) 144, (byte) 238, (byte) 144);

    public static EmbedColor LightGray => new EmbedColor((byte) 211, (byte) 211, (byte) 211);

    public static EmbedColor LightPink => new EmbedColor(byte.MaxValue, (byte) 182, (byte) 193);

    public static EmbedColor LightSalmon => new EmbedColor(byte.MaxValue, (byte) 160, (byte) 122);

    public static EmbedColor LightSeaGreen => new EmbedColor((byte) 32, (byte) 178, (byte) 170);

    public static EmbedColor LightSkyBlue => new EmbedColor((byte) 135, (byte) 206, (byte) 250);

    public static EmbedColor LightSlateGray => new EmbedColor((byte) 119, (byte) 136, (byte) 153);

    public static EmbedColor LightSteelBlue => new EmbedColor((byte) 176, (byte) 196, (byte) 222);

    public static EmbedColor LightYellow
    {
      get => new EmbedColor(byte.MaxValue, byte.MaxValue, (byte) 224);
    }

    public static EmbedColor Lime => new EmbedColor((byte) 0, byte.MaxValue, (byte) 0);

    public static EmbedColor LimeGreen => new EmbedColor((byte) 50, (byte) 205, (byte) 50);

    public static EmbedColor Linen => new EmbedColor((byte) 250, (byte) 240, (byte) 230);

    public static EmbedColor Magenta => new EmbedColor(byte.MaxValue, (byte) 0, byte.MaxValue);

    public static EmbedColor Maroon => new EmbedColor((byte) 128, (byte) 0, (byte) 0);

    public static EmbedColor MediumAquamarine => new EmbedColor((byte) 102, (byte) 205, (byte) 170);

    public static EmbedColor MediumBlue => new EmbedColor((byte) 0, (byte) 0, (byte) 205);

    public static EmbedColor MediumOrchid => new EmbedColor((byte) 186, (byte) 85, (byte) 211);

    public static EmbedColor MediumPurple => new EmbedColor((byte) 147, (byte) 112, (byte) 219);

    public static EmbedColor MediumSeaGreen => new EmbedColor((byte) 60, (byte) 179, (byte) 113);

    public static EmbedColor MediumSlateBlue => new EmbedColor((byte) 123, (byte) 104, (byte) 238);

    public static EmbedColor MediumSpringGreen => new EmbedColor((byte) 0, (byte) 250, (byte) 154);

    public static EmbedColor MediumTurquoise => new EmbedColor((byte) 72, (byte) 209, (byte) 204);

    public static EmbedColor MediumVioletRed => new EmbedColor((byte) 199, (byte) 21, (byte) 133);

    public static EmbedColor MidnightBlue => new EmbedColor((byte) 25, (byte) 25, (byte) 112);

    public static EmbedColor MintCream => new EmbedColor((byte) 245, byte.MaxValue, (byte) 250);

    public static EmbedColor MistyRose => new EmbedColor(byte.MaxValue, (byte) 228, (byte) 225);

    public static EmbedColor Moccasin => new EmbedColor(byte.MaxValue, (byte) 228, (byte) 181);

    public static EmbedColor NavajoWhite => new EmbedColor(byte.MaxValue, (byte) 222, (byte) 173);

    public static EmbedColor Navy => new EmbedColor((byte) 0, (byte) 0, (byte) 128);

    public static EmbedColor OldLace => new EmbedColor((byte) 253, (byte) 245, (byte) 230);

    public static EmbedColor Olive => new EmbedColor((byte) 128, (byte) 128, (byte) 0);

    public static EmbedColor OliveDrab => new EmbedColor((byte) 107, (byte) 142, (byte) 35);

    public static EmbedColor Orange => new EmbedColor(byte.MaxValue, (byte) 165, (byte) 0);

    public static EmbedColor OrangeRed => new EmbedColor(byte.MaxValue, (byte) 69, (byte) 0);

    public static EmbedColor Orchid => new EmbedColor((byte) 218, (byte) 112, (byte) 214);

    public static EmbedColor PaleGoldenrod => new EmbedColor((byte) 238, (byte) 232, (byte) 170);

    public static EmbedColor PaleGreen => new EmbedColor((byte) 152, (byte) 251, (byte) 152);

    public static EmbedColor PaleTurquoise => new EmbedColor((byte) 175, (byte) 238, (byte) 238);

    public static EmbedColor PaleVioletRed => new EmbedColor((byte) 219, (byte) 112, (byte) 147);

    public static EmbedColor PapayaWhip => new EmbedColor(byte.MaxValue, (byte) 239, (byte) 213);

    public static EmbedColor PeachPuff => new EmbedColor(byte.MaxValue, (byte) 218, (byte) 185);

    public static EmbedColor Peru => new EmbedColor((byte) 205, (byte) 133, (byte) 63);

    public static EmbedColor Pink => new EmbedColor(byte.MaxValue, (byte) 192, (byte) 203);

    public static EmbedColor Plum => new EmbedColor((byte) 221, (byte) 160, (byte) 221);

    public static EmbedColor PowderBlue => new EmbedColor((byte) 176, (byte) 224, (byte) 230);

    public static EmbedColor Purple => new EmbedColor((byte) 128, (byte) 0, (byte) 128);

    public static EmbedColor RebeccaPurple => new EmbedColor((byte) 102, (byte) 51, (byte) 153);

    public static EmbedColor Red => new EmbedColor(byte.MaxValue, (byte) 0, (byte) 0);

    public static EmbedColor RosyBrown => new EmbedColor((byte) 188, (byte) 143, (byte) 143);

    public static EmbedColor RoyalBlue => new EmbedColor((byte) 65, (byte) 105, (byte) 225);

    public static EmbedColor SaddleBrown => new EmbedColor((byte) 139, (byte) 69, (byte) 19);

    public static EmbedColor Salmon => new EmbedColor((byte) 250, (byte) 128, (byte) 114);

    public static EmbedColor SandyBrown => new EmbedColor((byte) 244, (byte) 164, (byte) 96);

    public static EmbedColor SeaGreen => new EmbedColor((byte) 46, (byte) 139, (byte) 87);

    public static EmbedColor SeaShell => new EmbedColor(byte.MaxValue, (byte) 245, (byte) 238);

    public static EmbedColor Sienna => new EmbedColor((byte) 160, (byte) 82, (byte) 45);

    public static EmbedColor Silver => new EmbedColor((byte) 192, (byte) 192, (byte) 192);

    public static EmbedColor SkyBlue => new EmbedColor((byte) 135, (byte) 206, (byte) 235);

    public static EmbedColor SlateBlue => new EmbedColor((byte) 106, (byte) 90, (byte) 205);

    public static EmbedColor SlateGray => new EmbedColor((byte) 112, (byte) 128, (byte) 144);

    public static EmbedColor Snow => new EmbedColor(byte.MaxValue, (byte) 250, (byte) 250);

    public static EmbedColor SpringGreen => new EmbedColor((byte) 0, byte.MaxValue, (byte) 127);

    public static EmbedColor SteelBlue => new EmbedColor((byte) 70, (byte) 130, (byte) 180);

    public static EmbedColor Tan => new EmbedColor((byte) 210, (byte) 180, (byte) 140);

    public static EmbedColor Teal => new EmbedColor((byte) 0, (byte) 128, (byte) 128);

    public static EmbedColor Thistle => new EmbedColor((byte) 216, (byte) 191, (byte) 216);

    public static EmbedColor Tomato => new EmbedColor(byte.MaxValue, (byte) 99, (byte) 71);

    public static EmbedColor Turquoise => new EmbedColor((byte) 64, (byte) 224, (byte) 208);

    public static EmbedColor Violet => new EmbedColor((byte) 238, (byte) 130, (byte) 238);

    public static EmbedColor Wheat => new EmbedColor((byte) 245, (byte) 222, (byte) 179);

    public static EmbedColor White => new EmbedColor(byte.MaxValue, byte.MaxValue, byte.MaxValue);

    public static EmbedColor WhiteSmoke => new EmbedColor((byte) 245, (byte) 245, (byte) 245);

    public static EmbedColor Yellow => new EmbedColor(byte.MaxValue, byte.MaxValue, (byte) 0);

    public static EmbedColor YellowGreen => new EmbedColor((byte) 154, (byte) 205, (byte) 50);
  }
}
