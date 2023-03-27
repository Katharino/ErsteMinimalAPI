using MeineErsteAPI.Models;
using System.Collections.Concurrent;

namespace MeineErsteAPI
{
    public class FakeDb
    {
        public int NaechsterCharId { get; set; } = 0;
        public ConcurrentDictionary<int, Charakter> CharDict { get; set; } = new();

    }
}
