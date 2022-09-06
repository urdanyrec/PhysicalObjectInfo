using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicalObjectInfo.Domain
{
    public class SampleValue
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public string Dimension { get; set; }
        public DateTime PollingTime { get; set; }
        public Guid ObjectId { get; set; }
        public Parameter Parameter { get; set; } = null!;
    }
}
