using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapViewer.Classes
{
    public class Edge: Way
    {
        public int EdgeID { get; set; }

        public override bool Equals(object obj)
        {
            return EdgeID.Equals(((Edge)obj).EdgeID);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
