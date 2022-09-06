using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicalObjectInfo.Domain
{
    public class RealParameter : Parameter
    {
        public double Value { get; set; }

        /*public override string ValueAsString
        {
            get { return Value.ToString(); }
        }*/
    }
}
