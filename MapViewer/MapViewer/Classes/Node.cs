using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MapViewer.Classes
{
    public class Node
    {
        public Node()
        {
        }
        public decimal NodeID { get; set; }
        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string PointString { get; set; }
        public Pen AttachedPen { get; set; }

        public override bool Equals(object obj)
        {
            return NodeID.Equals(((Node)obj).NodeID) || (Longitude == ((Node)obj).Longitude && Latitude == ((Node)obj).Latitude);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
