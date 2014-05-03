using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MapViewer.Classes
{
    public class Way
    {
        public Way()
        {
        }
        public int WayID { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public string LineString { get; set; }
        public PointF[] Points { get; set; }
        public Pen AttachedPen { get; set; }

        public bool Parse()
        {
            Points = LineStringParser.Parse(LineString);
            if (Points == null)
                return false;
            return true;
        }
        public override string ToString()
        {
            return "Name : " + Name + Environment.NewLine +
                    "Length : " + Length.ToString() + Environment.NewLine;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return WayID.Equals(((Way)obj).WayID);
        }
    }
}
