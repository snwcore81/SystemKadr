using System.Collections;
using System.Linq;
using System.Text;
using SystemKadr.Common;
using SystemKadr.Common.Log;
using SystemKadr.Common.Menu;
using SystemKadr.Model;

namespace SystemKadr
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Loger.SetOutput(new FileLogWriter("logapp.txt"));

            using var log = Loger.Create<Program>("Main");

            MenuComponent m = new(new MenuItem[] 
            { 
                new(1, "Opcja testowa 1", ConsoleKey.F1), 
                new(2, "Opcja test"),
                new(3, "Opcja testowa 2", ConsoleKey.F2),
                new(4, "Opcja testowa 3 albo coś", ConsoleKey.Escape)
            });

            Console.WriteLine(m.Select((1, 4), true));

            Employee e1 = new();
            e1.Name = "Jacek";
            e1.SecondName = "Kuźmicz";
            e1.Age = 42;

            var data = e1.WriteObject().AsString();

            Employee e = new();
            e.ReadObject(data.AsStream());

            Console.WriteLine(e);

            var output = DataInput.Get<int>("Wprowadz int:","Podales niepoprawna wartosc", (x) =>
            {
                if (x < 0 || x > 10)
                    throw new DataInput.DataInputException("Niepoprawna wartosc! Ma byc z przedzialu 1-10");
            });

            var output1 = DataInput.Get<float>("Wprowadz float:", "Podales niepoprawna wartosc", (x) =>
            {
                if (x < 100.0f)
                    throw new DataInput.DataInputException("Niepoprawna wartosc! Ma byc wieksze od 100!");
            });

            Console.WriteLine($"o1={output} o2={output1}");
        }
    }
}