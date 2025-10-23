using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanta.Core.Domain
{
    public class PingEvents
    {
        public int Id { get; set; }
        public DateTime PingDateTime { get; set; }
        public string Status { get; set; }
        public int RoundTripTime { get; set; }
    }
}
