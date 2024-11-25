// https://github.com/aerodakdev && https://github.com/San3lka
// Current=> kaka.ItemIcon
// <3

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace kaka
{
  public class ItemIcon
  {
    public ushort itemId;
    public Dictionary<byte[], Texture2D> states;

    public ItemIcon(ushort itemId)
    {
      this.itemId = itemId;
      this.states = new Dictionary<byte[], Texture2D>();
    }

    public bool Contains(byte[] state)
    {
      foreach (byte[] key in this.states.Keys)
      {
        bool flag = true;
        if (state.Length == key.Length)
        {
          for (int index = 0; index < key.Length; ++index)
          {
            if ((int) state[index] != (int) key[index])
            {
              flag = false;
              break;
            }
          }
          if (flag)
            return true;
        }
      }
      return false;
    }

    public Texture2D Get(byte[] state)
    {
      foreach (byte[] key in this.states.Keys)
      {
        bool flag = true;
        if (state.Length == key.Length)
        {
          for (int index = 0; index < key.Length; ++index)
          {
            if ((int) state[index] != (int) key[index])
            {
              flag = false;
              break;
            }
          }
          if (flag)
            return this.states[key];
        }
      }
      return (Texture2D) null;
    }
  }
}
