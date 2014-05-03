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
    public partial class frm_SelectRoad : Form
    {
        public frm_SelectRoad()
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
            this.mapViewer.UnHighlightAllWays();
            string command = rb_cordinates.Checked ?
                string.Format("select  WayID,WayName,WayLength,geog4326  from GetWayByCords({0},{1})", decimal.Parse(txt_longitude.Text), decimal.Parse(txt_latitude.Text)) :
                string.Format("select  WayID,WayName,WayLength,geog4326  from GetWayByName(N'{0}')", txt_Name.Text);

            SqlDataAdapter da = new SqlDataAdapter(command, Helper.connectionstring);
            DataSet result = new DataSet();
            da.Fill(result);
            dgv_results.DataSource = null;
            if (result.Tables.Count > 0)
                dgv_results.DataSource = result.Tables[0];
            dgv_results.Refresh();
            dgv_results_CellClick(null, null);
            this.Cursor = Cursors.Default;

            foreach (DataGridViewRow row in dgv_results.Rows)
            {
                mapViewer.HighlightWay(Convert.ToInt32(row.Cells["WayID"].Value), new Pen(Color.Red, 4));
            }


        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            txt_longitude.Enabled = rb_cordinates.Checked;
            txt_latitude.Enabled = rb_cordinates.Checked;
            txt_Name.Enabled = rb_name.Checked;
        }

        private void dgv_results_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataSet result = new DataSet();
            
            string command=string.Empty;
            if (dgv_results.SelectedRows.Count > 0)
            {
                command = string.Format("select WC.WayClass,WC.Distance,WC.NoOfCars, WW.EdgesCount,WW.TotalWeight,WW.AverageWeight from WayCapacity WC inner join WayWeights WW on WC.WayID=WW.WayID and WC.WayID ={0}", dgv_results.SelectedRows[0].Cells["WayID"].Value);
                SqlDataAdapter da = new SqlDataAdapter(command, Helper.connectionstring);
                
                da.Fill(result);
               
            }
            dgv_profile.DataSource = null;
            if (result.Tables.Count > 0)
                dgv_profile.DataSource = result.Tables[0];
            dgv_profile.Refresh();
        }

        private void btn_Increase_Click(object sender, EventArgs e)
        {   
            if (dgv_results.SelectedRows.Count > 0)
            {
                using (SqlCommand command = new SqlCommand("IncreaseWeight", new SqlConnection(Helper.connectionstring)))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("type", 'W');
                    command.Parameters.AddWithValue("id", dgv_results.SelectedRows[0].Cells["WayID"].Value);
                    command.Parameters.AddWithValue("weight", 10);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
            }
            dgv_results_CellClick(null, null);
        }

        private void btn_Decrease_Click(object sender, EventArgs e)
        {
            if (dgv_results.SelectedRows.Count > 0)
            {
                using (SqlCommand command = new SqlCommand("DecreaseWeight", new SqlConnection(Helper.connectionstring)))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("type", 'W');
                    command.Parameters.AddWithValue("id", dgv_results.SelectedRows[0].Cells["WayID"].Value);
                    command.Parameters.AddWithValue("weight", 10);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
            }
            dgv_results_CellClick(null, null);
        }

        private void btnPick_Click(object sender, EventArgs e)
        {
            mapViewer.Pick();
        }

    }
}
