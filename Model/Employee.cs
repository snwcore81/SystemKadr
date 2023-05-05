using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SystemKadr.Common;
using SystemKadr.Common.Logging;

namespace SystemKadr.Model
{
    public class Employee : XmlSerializer<Employee>
    {
        [DataMember]
        public string? Name { get; set; }
        [DataMember]
        public string? SecondName { get; set; }
        [DataMember]
        public int? Age { get; set; }

        protected override void Initialize(Employee? obj)
        {
            using var log = Loger.Create<Employee>("Initialize");

            base.Initialize(obj);

            log.Hint("wiadomość testowa");
        }

    }
}
