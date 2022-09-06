using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicalObjectInfo.Domain
{
    public class PhysicalObject
    {

        public Guid Id { get; set; }
        public Guid ObjectId { get; set; }
        public string Series { get; set; }
        public string State { get; set; }
        public string URL { get; set; }
        public Guid ObjectTechId { get; set; }
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}
