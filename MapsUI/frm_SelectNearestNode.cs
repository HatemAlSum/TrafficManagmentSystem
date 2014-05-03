using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Objects;

namespace MapsUI
{
    public partial class frm_SelectNearestNode : Form
    {
        public long NodeID { get; set; }
        public frm_SelectNearestNode()
        {
            InitializeComponent();
            mapViewer.ClickMap += new MapViewer.Classes.ClickHandler(mapViewer_ClickMap);
        }

        void mapViewer_ClickMap(MapViewer.Classes.ClickArgs args)
        {
            if (args.Node != null && args.Node.Name != string.Empty)
            {
                this.txt_longitude.Text = args.Node.Longitude.ToString();
                this.txt_latitude.Text = args.Node.Latitude.ToString();
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            mapViewer.UnHighlightAllNodes();
            string command = 
                string.Format("declare @NodeID NodeID;"+
                "select @NodeID = NodeID from dbo.GetNodeByCords({0},{1});"+
                "select * from GetNearestFromNode(@NodeID,NULL,'{2}',0.0001,0.001,1);",txt_longitude.Text,txt_latitude.Text, cb_category.Text);



            SqlDataAdapter da = new SqlDataAdapter( command,Helper.connectionstring);
            DataSet result = new DataSet();
            da.Fill(result);
            dgv_results.DataSource = null;
                if(result.Tables.Count>0)
                    dgv_results.DataSource= result.Tables[0];
            dgv_results.Refresh();
            dgv_results_CellClick(null, null);
            mapViewer.HighlightNode(Convert.ToDecimal(txt_longitude.Text), Convert.ToDecimal(txt_latitude.Text), new Pen(Color.Blue, 3));
            foreach (DataGridViewRow row in dgv_results.Rows)
            {
                mapViewer.HighlightNode(Convert.ToDecimal(row.Cells["NodeID"].Value), new Pen(Color.Red, 4));
            }

            this.Cursor = Cursors.Default;

        }


        private void dgv_results_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            mapViewer.UnHighlightAllNodes();
            string command=string.Empty;
            DataSet result = new DataSet();
            if (dgv_results.SelectedRows.Count > 0)
            {
                this.NodeID = Convert.ToInt64(dgv_results.SelectedRows[0].Cells["NodeID"].Value);
                command = string.Format("Select * from NodeTags where NodeID={0}", dgv_results.SelectedRows[0].Cells["NodeID"].Value);
            
            SqlDataAdapter da = new SqlDataAdapter(command, Helper.connectionstring);
            
            da.Fill(result);
            dgv_profile.DataSource = null;
            if (result.Tables.Count > 0)
                dgv_profile.DataSource = result.Tables[0];
            mapViewer.HighlightNode(NodeID, new Pen(Color.Red, 3));
            }
            dgv_profile.Refresh();
            
        }

        private void btnPick_Click(object sender, EventArgs e)
        {
            mapViewer.Pick();
        }

       

    }
}
