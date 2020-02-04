using System;
using System.Collections.Generic;
using System.Text;

namespace LiveCoding.Core
{
    public class Class
    {
        public string Name { get; set; }
        public ClassType ClassType { get; set; }

        public override string ToString() => $"{Name,7}{ClassType,15}";
    }
}
