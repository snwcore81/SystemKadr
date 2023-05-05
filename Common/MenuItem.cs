using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemKadr.Common
{
    public class MenuItem
    {
        public static int KeyItemWidth = 8;

        public int Id { get; private set; }
        public string Name { get; private set; }
        public ConsoleKey? Key { get; private set; } 
        public MenuItem(int Id,string Name, ConsoleKey? Key = null)
        {
            this.Id = Id;
            this.Name = Name;
            this.Key = Key;
        }
        public override string ToString() => $"{Key.ToString().PadRight(KeyItemWidth, ' ')}{Name}";
    }
}
