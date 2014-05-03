using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MapViewer.Classes;

namespace MapsUI
{
    public partial class frm_Dijkstra : Form
    {
        public frm_Dijkstra()
        {
            InitializeComponent();
        }

        private void btn_selectNode1_Click(object sender, EventArgs e)
        {
            frm_SelectNode frm = new frm_SelectNode();
            frm.ShowDialog();
            txt_Src.Text = frm.NodeID.ToString();

        }

        private void btn_selectNode2_Click(object sender, EventArgs e)
        {
            frm_SelectNode frm = new frm_SelectNode();
            frm.ShowDialog();
            txt_Dst.Text = frm.NodeID.ToString();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            
            this.Cursor = Cursors.WaitCursor;
            mapViewer.UnHighlightAllEdges();
            string profile = "DECLARE @TblWightFactors dbo.TblWightFactors;" +
                                "insert into @TblWightFactors values('Distance',100,0);" +
                                "insert into @TblWightFactors values('maxspeed',100,60);" +
                                "insert into @TblWightFactors values('lanes',-10,2);" +
                                "insert into @TblWightFactors values('Preferred',-1,1);" +
                                "insert into @TblWightFactors values('Weight',100,0);" +
                                "insert into @TblWightFactors values('Flow',-1,1);";
            string command = profile+
                string.Format("exec Dijkstra {0},{1},0,@TblWightFactors;", txt_Src.Text, txt_Dst.Text);
                

            SqlDataAdapter da = new SqlDataAdapter(command, Helper.connectionstring);
            DataSet result = new DataSet();
            da.Fill(result);
            dgv_results.DataSource = null;
            if (result.Tables.Count > 0)
                dgv_results.DataSource = result.Tables[0];
            dgv_results.Refresh();

            MapViewer.Classes.Path P = new MapViewer.Classes.Path();
            P.StartNode = new Node { NodeID = decimal.Parse(txt_Src.Text) };
            P.EndNode = new Node { NodeID = decimal.Parse(txt_Dst.Text) };
            foreach (DataGridViewRow row in dgv_results.Rows)
            {
                    if (row.Cells["EdgeID"].Value != null && Convert.ToInt32(row.Cells["EdgeID"].Value) > 0)
                        //mapViewer.HighlightEdge(Convert.ToInt32(row.Cells["EdgeID"].Value), new Pen(Color.Red, 4));
                        P.AddEdge(new Edge { EdgeID = Convert.ToInt32(row.Cells["EdgeID"].Value) }); 
            }
            mapViewer.HighlightPath(P, new Pen(Color.Red, 4));
            this.Cursor = Cursors.Default;
        }

        private void frm_Dijkstra_Load(object sender, EventArgs e)
        {

        }


    }
}
