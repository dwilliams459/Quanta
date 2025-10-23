using System;

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