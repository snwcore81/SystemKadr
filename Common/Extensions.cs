using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemKadr.Common
{
    public static class Extensions
    {
        public static Stream AsStream(this string input) => new MemoryStream(Encoding.UTF8.GetBytes(input));
        public static string AsString(this MemoryStream input) => Encoding.UTF8.GetString(input.ToArray());
    }
}
