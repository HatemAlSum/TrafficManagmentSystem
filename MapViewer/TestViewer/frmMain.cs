using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapViewer.Classes;

namespace TestViewer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //mapViewer.HighlightWay(174647078, new Pen(Color.Red, 4));

            //mapViewer.HighlightEdge(10828, new Pen(Color.Blue, 4));

            //mapViewer.HighlightNode(607250058, new Pen(Color.Red, 3));

            //mapViewer.HighlightNode(31.2432513f, 30.0199735f, new Pen(Color.Red, 3));
            mapViewer.DefaultZoom = 1024;
            Path path = new Path();
            path.StartNode = new Node { NodeID = 919760233, AttachedPen = new Pen(Color.Red, 3) };
            path.EndNode = new Node { NodeID = 919761784, AttachedPen = new Pen(Color.Red, 3) };
            path.AddEdge(new Edge { EdgeID = 6331 });
            path.AddEdge(new Edge { EdgeID = 6332 });
            path.AddEdge(new Edge { EdgeID = 5 });
            mapViewer.HighlightPath(path,new Pen(Color.LightBlue,4));
        }
    }
}