using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemKadr.Common
{
    public class Menu
    {
        private List<MenuItem> _items;
        private int _maximumWidth;
        private int _selectedItem = 0;

        public struct ItemColor
        {
            public ConsoleColor Foreground;
            public ConsoleColor Background;

            public void Set()
            {
                Console.ForegroundColor = Foreground;
                Console.BackgroundColor = Background;
            }
        }

        public ItemColor NotSelected { get; set; } = new() { Background = ConsoleColor.Gray, Foreground = ConsoleColor.Black };
        public ItemColor Selected { get; set; } = new() { Background = ConsoleColor.Red, Foreground = ConsoleColor.White };

        public Menu()
        {
            using var log = Loger.Create<Menu>("Menu");

            _items = new();
            _maximumWidth = 0;
        }

        public Menu(MenuItem[] items) : this()
        {
            using var log = Loger.Create<Menu>("Menu(MenuItem[] items)");

            this._items.AddRange(items);
            this._maximumWidth = this._items.Max(x => x.Name.Length) + MenuItem.KeyItemWidth;
        }

        public void Draw((int x, int y) position)
        {
            Console.CursorVisible = false;

            foreach (var item in _items)
            {
                int itemIndex = _items.IndexOf(item);

                if (itemIndex == _selectedItem)
                    Selected.Set();
                else
                    NotSelected.Set();

                Console.SetCursorPosition(position.x, position.y + itemIndex);
                Console.WriteLine(item.ToString().PadRight(_maximumWidth,' '));
            }

            Console.ResetColor();
            Console.CursorVisible = true;
        }

        public int Select((int x, int y) position, bool autoEnter = false)
        {
            do
            {
                Draw(position);

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.Enter:
                        return _items[_selectedItem].Id;
                    case ConsoleKey.DownArrow:
                        _selectedItem = Math.Min(++_selectedItem, _items.Count - 1);
                        break;
                    case ConsoleKey.UpArrow:
                        _selectedItem = Math.Max(0, --_selectedItem);
                        break;
                    default:
                        var index = _items.FindIndex(x => x.Key.Equals(key));
                        if (index >= 0)
                        {
                            _selectedItem = index;
                            if (autoEnter)
                                return _items[_selectedItem].Id;
                        }
                        break;
                }
            }
            while (true);
        }
    }
}
