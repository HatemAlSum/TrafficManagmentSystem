using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using MapViewer.Classes;

namespace MapViewer
{
    public partial class MapViewer : UserControl
    {
        public MapViewer()
        {
            Connection = new SqlConnection(Properties.Resources.ConnectionString);
            InitializeComponent();
        }

        #region Variables
        double xMin, yMin, xMax, yMax;
        int x1, y1, x2, y2;
        int offsetX = 0;
        int offsetY = 0;
        bool down = false;
        bool move = false;
        float zoom = 256;
        public decimal Longitude;
        public decimal Latitude;
        public int BufferSpace = 600000;
        private bool PickMode = false;
        private List<Way> highlight_Ways;
        private List<Edge> highlight_Edges;
        private List<Node> highlight_Nodes;
        private List<Classes.Path> highlight_Paths;
        public event ClickHandler ClickMap;

        SqlConnection Connection;
        SqlCommand Command;
        private Hashtable htWays;
        private Hashtable htEdges;
        private Hashtable htNodes;
        public float DefaultZoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;
            }
        }
        #endregion

        #region Events
        private void MapViewer_Load(object sender, EventArgs e)
        {
            htWays = new Hashtable();
            htEdges = new Hashtable();
            htNodes = new Hashtable();
            highlight_Ways = new List<Way>();
            highlight_Edges = new List<Edge>();
            highlight_Nodes = new List<Node>();
            highlight_Paths = new List<Classes.Path>();
            LoadMaxMin();
            LoadAllWays();
            LoadAllNodes();
        }
        private void pictureMap_Paint(object sender, PaintEventArgs e)
        {
            PointF pt_string;
            double MaxLen;
            e.Graphics.TranslateTransform(offsetX, offsetY);
            foreach (Way way in htWays.Values)
            {
                int n = way.Points.Length;
                PointF[] pts = new PointF[n];
                double sumX = 0, sumY = 0;
                for (int j = 0; j < n; j++)
                {
                    pts[j].X = (way.Points[j].X - (float)xMin) * zoom;
                    pts[j].Y = (way.Points[j].Y - (float)yMax) * zoom;
                    sumX += pts[j].X; sumY += pts[j].Y;
                }
                if (pts.Length > 1)
                {
                    if (sumX < 0) sumX = sumX * -1;
                    if (sumY < 0) sumY = sumY * -1;
                    if (way.Length*zoom > 1000000)
                        e.Graphics.DrawLines(new Pen(Color.FromArgb(((int)(n * 7 * Math.Sqrt(sumX)) % 200), (int)((n * 11 * sumY) % 200)
                        , (int)((n * 13 * ((sumX + sumY))) % 200))), pts);
                    if (way.Length*zoom > 10000000)
                    {
                        pt_string = GetLongestLine(pts, out MaxLen);
                        e.Graphics.DrawString(way.Name, this.Font, Brushes.Black, pt_string);
                    }
                }
            }
            if (zoom > 5000)
            {
                foreach (Node node in htNodes.Values)
                {
                    decimal x = (node.Longitude - (decimal)xMin) * (decimal)zoom;
                    decimal y = (node.Latitude - (decimal)yMax) * (decimal)zoom;
                    float size1;
                    size1 = 2;
                    e.Graphics.DrawEllipse(new Pen(new SolidBrush(Color.Black),2), (float)x - (size1 / 2), (float)y - (size1 / 2), size1, size1);
                }
            }
            for (int i = 0; i < highlight_Ways.Count; i++)
            {
                int n = highlight_Ways[i].Points.Length;
                PointF[] pts = new PointF[n];
                double sumX = 0, sumY = 0;
                for (int j = 0; j < n; j++)
                {
                    pts[j].X = (highlight_Ways[i].Points[j].X - (float)xMin) * zoom;
                    pts[j].Y = (highlight_Ways[i].Points[j].Y - (float)yMax) * zoom;
                    sumX += pts[j].X; sumY += pts[j].Y;
                }
                if (pts.Length > 1)
                {
                    if (sumX < 0) sumX = sumX * -1;
                    if (sumY < 0) sumY = sumY * -1;
                    e.Graphics.DrawLines(highlight_Ways[i].AttachedPen, pts);
                }
            }
            for (int i = 0; i < highlight_Edges.Count; i++)
            {
                int n = highlight_Edges[i].Points.Length;
                PointF[] pts = new PointF[n];
                double sumX = 0, sumY = 0;
                for (int j = 0; j < n; j++)
                {
                    pts[j].X = (highlight_Edges[i].Points[j].X - (float)xMin) * zoom;
                    pts[j].Y = (highlight_Edges[i].Points[j].Y - (float)yMax) * zoom;
                    sumX += pts[j].X; sumY += pts[j].Y;
                }
                if (pts.Length > 1)
                {
                    if (sumX < 0) sumX = sumX * -1;
                    if (sumY < 0) sumY = sumY * -1;
                    e.Graphics.DrawLines(highlight_Edges[i].AttachedPen, pts);
                }
            }
            for (int i = 0; i < highlight_Nodes.Count; i++)
            {
                decimal x = (highlight_Nodes[i].Longitude - (decimal)xMin) * (decimal)zoom;
                decimal y = (highlight_Nodes[i].Latitude - (decimal)yMax) * (decimal)zoom;
                float size1, size2;
                size1 = 3;
                size2 = 10 * (zoom / 512);
                e.Graphics.DrawEllipse(highlight_Nodes[i].AttachedPen, (float)x-(size1/2), (float)y-(size1/2), size1, size1);
                e.Graphics.DrawEllipse(highlight_Nodes[i].AttachedPen, (float)x-(size2/2), (float)y-(size2/2), size2, size2);
            }
        }
        private void pictureMap_Click(object sender, EventArgs e)
        {
            if (!move)
            {
                ClickArgs Args = new ClickArgs();
                Node node = LoadNodeByCords(Longitude, Latitude, new decimal(0.001));
                List<Way> ways = LoadWayByCords(Longitude, Latitude);
                List<Edge> edges = LoadEdgeByCords(Longitude, Latitude);
                Args.Node = node;
                Args.Ways = ways;
                Args.Edges = edges;
                Args.Longitude = Longitude; 
                Args.Latitude = Latitude;
                if (!PickMode)
                {
                    string Info = string.Empty;
                    if (node != null)
                    {
                        Info += "Nodes:-" + Environment.NewLine;
                        Info += node.ToString();
                        Info += Environment.NewLine + Environment.NewLine;
                    }
                    if (ways.Count > 0)
                    {
                        Info += "Ways:-" + Environment.NewLine;
                        foreach (Way way in ways)
                            Info += way.ToString();
                        Info += Environment.NewLine + Environment.NewLine;
                    }
                    if (edges.Count > 0)
                    {
                        Info += "Edges:-" + Environment.NewLine;
                        foreach (Edge edge in edges)
                            Info += edge.ToString();
                    }
                    if (Info != string.Empty)
                        MessageBox.Show(Info);
                }
                else if (PickMode)
                {
                    ClickMap(Args);
                    PickMode = false;
                }
            }
        }
        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            zoom = zoom / 2;
            pictureMap.Invalidate();
        }
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            zoom = zoom * 2;
            pictureMap.Invalidate();
        }
        private void pictureMap_MouseDown(object sender, MouseEventArgs e)
        {
            x1 = e.X;
            y1 = e.Y;
            x2 = offsetX;
            y2 = offsetY;
            down = true;
        }
        private void pictureMap_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolTip.SetToolTip(this.pictureMap, string.Empty);
            if (down == true)
            {
                move = true;
                offsetX = x2 - (x1 - e.X);
                offsetY = y2 - (y1 - e.Y);
                pictureMap.Invalidate();
            }
            else
            {
                Longitude = decimal.Parse(((((double)e.X - offsetX) / zoom) + xMin).ToString());
                Latitude = -1*decimal.Parse(((((double)e.Y - offsetY) / zoom) + yMax).ToString());

                this.lblPosition.Text ="Longitude= " + Longitude.ToString() +
                    ", Latitude= " + Latitude.ToString();
            }
        }
        private void pictureMap_MouseUp(object sender, MouseEventArgs e)
        {
            down = false;
            move = false;
        }
        private void pictureMap_MouseHover(object sender, EventArgs e)
        {
            //string Label = string.Empty;
            //List<Way> Ways = LoadWayByCords(Longitude, Latitude);
            //foreach (Way way in Ways)
            //{
            //    Label += way.ToString() + Environment.NewLine;
            //}
            //if(Label!=string.Empty)
            //this.toolTip.SetToolTip(pictureMap, Label);
        }
        #endregion

        #region Methods
        private void LoadMaxMin()
        {
            SqlDataReader Reader= null;
            Command = new SqlCommand("select * from GetNodeMaxMin()", Connection);
            Command.CommandType = CommandType.Text;
            try
            {
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    xMin = Reader.GetDouble(0);
                    xMax = Reader.GetDouble(1);
                    yMin = -1*Reader.GetDouble(2);
                    yMax = -1*Reader.GetDouble(3);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (!Reader.IsClosed)
                    Reader.Close();
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }

        }
        private void LoadAllWays()
        {
            htWays = new Hashtable();
            SqlDataReader Reader= null;
            Command = new SqlCommand("select * from GetAllWays()", Connection);
            Command.CommandType = CommandType.Text;
            try
            {
                Way way;
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    way = new Way
                    {
                        WayID = Reader.GetInt32(0),
                        Name = Reader.GetValue(2).ToString(),
                        Length = Reader.GetDouble(3),
                        LineString = Reader.GetValue(1).ToString()
                    };
                    way.Parse();
                    htWays.Add(way.WayID, way);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (!Reader.IsClosed)
                    Reader.Close();
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
        }
        private void LoadAllNodes()
        {
            htNodes = new Hashtable();
            SqlDataReader Reader = null;
            Command = new SqlCommand("select * from GetAllNodes()", Connection);
            Command.CommandType = CommandType.Text;
            try
            {
                Node node;
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    node = new Node
                    {
                        NodeID = Reader.GetDecimal(0),
                        Longitude = decimal.Parse(Reader.GetValue(1).ToString()),
                        Latitude = -1 * decimal.Parse(Reader.GetValue(2).ToString()),
                        PointString = Reader.GetValue(3).ToString(),
                        Name = Reader.GetValue(4).ToString(),
                    };
                    htNodes.Add(node.NodeID, node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (!Reader.IsClosed)
                    Reader.Close();
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
        }
        private List<Way> LoadWayByCords(decimal lon,decimal lat)
        {
            List<Way> Ways = new List<Way>();
            SqlDataReader Reader = null;
            Command = new SqlCommand("select * from GetNearstWay(@Longitude,@Latitude,@BufferSpace)", Connection);
            Command.Parameters.AddWithValue("Longitude", lon);
            Command.Parameters.AddWithValue("Latitude", lat);
            Command.Parameters.AddWithValue("BufferSpace", BufferSpace/zoom);
            Command.CommandType = CommandType.Text;
            try
            {
                Way way;
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    way = new Way
                    {
                        WayID = Reader.GetInt32(0),
                        Name = Reader.GetValue(2).ToString(),
                        Length = Reader.GetDouble(3),
                        LineString = Reader.GetValue(1).ToString()
                    };
                    Ways.Add(way);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (!Reader.IsClosed)
                    Reader.Close();
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
            return Ways;
        }
        private List<Edge> LoadEdgeByCords(decimal lon, decimal lat)
        {
            List<Edge> Edges = new List<Edge>();
            SqlDataReader Reader = null;
            Command = new SqlCommand("GetEdgeByCords", Connection);
            Command.Parameters.AddWithValue("Longitude", lon);
            Command.Parameters.AddWithValue("Latitude", lat);
            Command.CommandType = CommandType.StoredProcedure;
            try
            {
                Edge Edge;
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Edge = new Edge
                    {
                        EdgeID = Reader.GetInt32(0),
                        WayID = Reader.GetInt32(1),
                        Name = Reader.GetValue(3).ToString(),
                        Length = Reader.GetDouble(4),
                        LineString = Reader.GetValue(2).ToString()
                    };
                    Edges.Add(Edge);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (!Reader.IsClosed)
                    Reader.Close();
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
            return Edges;
        }
        private Node LoadNodeByCords(decimal lon, decimal lat,decimal Radius=0)
        {
            Node node = null;
            SqlDataReader Reader = null;
            if(Radius==0)
                Command = new SqlCommand("select * from dbo.GetNodeByCords(@Longitude,@Latitude)", Connection);
            else
                Command = new SqlCommand("select * from dbo.GetNearestNodes(@Longitude,@Latitude,@Radius)", Connection);
            Command.Parameters.AddWithValue("Longitude", lon);
            Command.Parameters.AddWithValue("Latitude", lat);
            if(Radius>0)
                Command.Parameters.AddWithValue("Radius", Radius);
            Command.CommandType = CommandType.Text;
            try
            {
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    node = new Node
                    {
                        NodeID = Reader.GetDecimal(0),
                        Name = Reader.GetValue(6).ToString(),
                        Longitude = decimal.Parse(Reader.GetValue(7).ToString()),
                        Latitude = decimal.Parse(Reader.GetValue(8).ToString()),
                        PointString = Reader.GetValue(1).ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (!Reader.IsClosed)
                    Reader.Close();
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
            return node;
        }
        private void LoadEdgeByID(int EdgeID)
        {
            htEdges = new Hashtable();
            SqlDataReader Reader = null;
            Command = new SqlCommand("select * from GetEdgeByID(@EdgeID)", Connection);
            Command.Parameters.AddWithValue("EdgeID", EdgeID);
            Command.CommandType = CommandType.Text;
            try
            {
                Edge edge;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    edge = new Edge
                    {
                        EdgeID = EdgeID,
                        WayID = Reader.GetInt32(0),
                        Name = Reader.GetValue(2).ToString(),
                        Length = Reader.GetDouble(3),
                        LineString = Reader.GetValue(1).ToString()
                    };
                    edge.Parse();
                    htEdges.Add(edge.EdgeID, edge);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (!Reader.IsClosed)
                    Reader.Close();
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
        }
        private Node LoadNodeByID(decimal NodeID)
        {
            SqlDataReader Reader = null;
            Node node = null;
            Command = new SqlCommand("select * from GetNodeByID(@NodeID)", Connection);
            Command.Parameters.AddWithValue("NodeID", NodeID);
            Command.CommandType = CommandType.Text;
            try
            {
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    node = new Node
                    {
                        NodeID = NodeID,
                        Longitude = decimal.Parse(Reader.GetValue(0).ToString()),
                        Latitude = -1*decimal.Parse(Reader.GetValue(1).ToString()),
                        PointString = Reader.GetValue(2).ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (!Reader.IsClosed)
                    Reader.Close();
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
            return node;
        }
        public void HighlightWay(int WayID, Pen attachedPen)
        {
            if (htWays.Contains(WayID))
            {
                Way way = (Way)htWays[WayID];
                if (way.Parse())
                {
                    way.AttachedPen = attachedPen;
                    highlight_Ways.Add(way);
                    pictureMap.Invalidate();
                }
            }
        }
        public void UnHighlightWay(int WayID)
        {
            highlight_Ways.Remove(new Way { WayID = WayID });
            pictureMap.Invalidate();
        }
        public void UnHighlightAllWays()
        {
            highlight_Ways.Clear();
            pictureMap.Invalidate();
        }
        public void HighlightEdge(int EdgeID, Pen attachedPen)
        {
            if(!htEdges.Contains(EdgeID))
            {
                LoadEdgeByID(EdgeID);
            }

            if (htEdges.Contains(EdgeID))
            {
                Edge edge = (Edge)htEdges[EdgeID];
                if (edge.Parse())
                {
                    edge.AttachedPen = attachedPen;
                    highlight_Edges.Add(edge);
                    pictureMap.Invalidate();
                }
            }
        }
        public void UnHighlightEdge(int EdgeID)
        {
            highlight_Edges.Remove(new Edge { EdgeID = EdgeID });
            pictureMap.Invalidate();
        }
        public void UnHighlightAllEdges()
        {
            highlight_Edges.Clear();
            pictureMap.Invalidate();
        }
        public void HighlightNode(decimal NodeID, Pen attachedPen)
        {
            Node node = (Node)htNodes[NodeID];
            if (node == null)
            {
                node = LoadNodeByID(NodeID);
            }
            if (node != null)
            {
                node.AttachedPen = attachedPen;
                highlight_Nodes.Add(node);
                pictureMap.Invalidate();
            }
        }
        public void HighlightNode(decimal Longitude,decimal Latitude, Pen attachedPen)
        {
            Node node = new Node
            {
                Longitude = Longitude,
                Latitude = -1*Latitude,
                PointString = "POINT (" + Longitude.ToString() + " " + Latitude.ToString() + ")"
            };
            node.AttachedPen = attachedPen;
            highlight_Nodes.Add(node);
            pictureMap.Invalidate();
        }
        public void UnHighlightNode(decimal NodeID)
        {
            highlight_Nodes.Remove(new Node { NodeID = NodeID });
            pictureMap.Invalidate();
        }
        public void UnHighlightAllNodes()
        {
            highlight_Nodes.Clear();
            pictureMap.Invalidate();
        }
        public void HighlightPath(Classes.Path path, Pen attachedPen)
        {
            highlight_Paths.Add(path);
            HighlightNode(path.StartNode.NodeID, (path.StartNode.AttachedPen==null)?attachedPen:path.StartNode.AttachedPen);
            HighlightNode(path.EndNode.NodeID, (path.EndNode.AttachedPen == null) ? attachedPen : path.EndNode.AttachedPen);
            foreach (Edge edge in path.Edges)
            {
                HighlightEdge(edge.EdgeID, attachedPen);
            }
            pictureMap.Invalidate();
        }
        public void UnHighlightAllPaths()
        {
            foreach (Classes.Path path in highlight_Paths)
            {
                UnHighlightNode(path.StartNode.NodeID);
                UnHighlightNode(path.EndNode.NodeID);
                foreach (Edge edge in path.Edges)
                {
                    UnHighlightEdge(edge.EdgeID);
                }
            }
            highlight_Paths.Clear();
            pictureMap.Invalidate();
        }
        private PointF GetLongestLine(PointF[] pts, out double MaxLen)
        {
            double MaxLength = -1,Length = 0;
            PointF pt = pts[0];
            for (int i = 0; i < pts.Length - 1; i++)
            {
                Length = GetLineLength(pts[i],pts[i+1]);
                if (MaxLength < Length)
                {
                    MaxLength = Length;
                    pt = pts[i];
                }
            }
            MaxLen = MaxLength;
            return pt;
        }
        public void Pick()
        {
            PickMode = true;
        }
        private double GetLineLength(PointF x1, PointF x2)
        {
            return Math.Sqrt(Math.Pow(x1.X - x2.X, 2) + Math.Pow(x1.Y - x2.Y,2));
        }
        #endregion
    }
}
