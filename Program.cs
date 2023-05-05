﻿using System.Collections;
using System.Linq;
using System.Text;
using SystemKadr.Common;
using SystemKadr.Model;

namespace SystemKadr
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu m = new(new MenuItem[] 
            { 
                new(1, "Opcja testowa 1", ConsoleKey.F1), 
                new(2, "Opcja test"),
                new(3, "Opcja testowa 2", ConsoleKey.F2),
                new(4, "Opcja testowa 3 albo coś", ConsoleKey.Escape)
            });

            Console.WriteLine(m.Select((1, 4), true));

            using var log = Loger.Create<Program>("Main");

            Employee e1 = new();
            e1.Name = "Jacek";
            e1.SecondName = "Kuźmicz";
            e1.Age = 42;

            var data = e1.WriteObject().AsString();

            Employee e = new();
            e.ReadObject(data.AsStream());

            Console.WriteLine(e);
        }
    }
}