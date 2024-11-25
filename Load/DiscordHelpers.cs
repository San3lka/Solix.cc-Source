// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.DiscordHelpers
// <3

using System;
using System.Globalization;

#nullable disable
namespace kaka
{
  public static class DiscordHelpers
  {
    public static string DateTimeToISO(DateTime dateTime)
    {
      return DiscordHelpers.DateTimeToISO(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
    }

    public static string DateTimeToISO(
      int year,
      int month,
      int day,
      int hour,
      int minute,
      int second)
    {
      return new DateTime(year, month, day, hour, minute, second, 0, DateTimeKind.Local).ToString("yyyy-MM-dd'T'HH:mm:ss.fffK", (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }
}
