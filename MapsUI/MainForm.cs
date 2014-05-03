using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapsUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_SelectRoad_Click(object sender, EventArgs e)
        {
            frm_SelectRoad frm = new frm_SelectRoad();
            frm.ShowDialog();
        }

        private void btn_SelectNode_Click(object sender, EventArgs e)
        {
            frm_SelectNode frm = new frm_SelectNode();
            frm.ShowDialog();
          
        }

        private void btn_BFS_Click(object sender, EventArgs e)
        {
            frm_BFScs frm = new frm_BFScs();
            frm.ShowDialog();

        }

        private void btn_Dijkstra_Click(object sender, EventArgs e)
        {
            frm_Dijkstra frm = new frm_Dijkstra();
            frm.ShowDialog();

        }

        private void btnNearestNode_Click(object sender, EventArgs e)
        {
            frm_SelectNearestNode frm = new frm_SelectNearestNode();
            frm.ShowDialog();
        }
    }
}
