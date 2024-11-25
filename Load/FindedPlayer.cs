// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.FindedPlayer
// <3

#nullable disable
namespace kaka
{
  public struct FindedPlayer
  {
    public uint ip;
    public ushort port;
    public string nickname;
    public string name;
    public string time;

    public FindedPlayer(uint ip, ushort port, string name, string nickname, string time)
    {
      this.ip = ip;
      this.port = port;
      this.nickname = nickname;
      this.name = name;
      this.time = time;
    }
  }
}
