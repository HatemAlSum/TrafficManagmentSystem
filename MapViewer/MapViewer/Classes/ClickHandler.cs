using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapViewer.Classes
{
    public delegate void ClickHandler(ClickArgs args);
    public class ClickArgs
    {
        public Node Node;
        public List<Way> Ways;
        public List<Edge> Edges;
        public decimal Longitude;
        public decimal Latitude;
        public ClickArgs()
        {
            Ways = new List<Way>();
            Edges = new List<Edge>();
        }
    }
}
