using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicalObjectInfo.Domain
{
    public class Parameter
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }//virtual string ValueAsString { get { return "Empty"; } }
        public string Dimension { get; set; }
        public DateTime PollingTime { get; set; } = new DateTime();
        public Guid ObjectId { get; set; } = Guid.Empty;

        //public List<SampleValue> SampleValues { get; set; } = new List<SampleValue>();

        public Guid PhysicalObjectId { get; set; }
        //public PhysicalObject PhysicalObject { get; set; }// = null!;

    }
}
