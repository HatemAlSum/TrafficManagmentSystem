using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapViewer.Classes
{
    public class Path
    {
        public Node StartNode { get; set; }
        public Node EndNode { get; set; }
        public List<Edge> Edges { get; private set; }

        public Path()
        {
            Edges = new List<Edge>();
        }
        public void AddEdge(Edge e)
        {
            Edges.Add(e);
        }
        public void RemoveEdge(int EdgeID)
        {
            Edge SelectedEdge = null;
            foreach (Edge e in Edges)
            {
                if (e.EdgeID == EdgeID)
                    SelectedEdge = e;
            }
            if (SelectedEdge != null)
                Edges.Remove(SelectedEdge);
        }
        public void ClearEdges()
        {
            Edges.Clear();
        }
    }
}
