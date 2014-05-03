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
    public partial class frm_SelectNode : Form
    {
        public long NodeID { get; set; }
        public frm_SelectNode()
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
                rb_cordinates.Checked ?
                string.Format("select distinct Convert(numeric, N.NodeID) as NodeID,N.Longitude,N.Latitude ,NT.TagValue from Nodes N  inner join NodeTags NT on N.NodeID=NT.NodeID and NT.TagName = 'name' Where N.Longitude={0} and N.Latitude={1}", decimal.Parse(txt_longitude.Text), decimal.Parse(txt_latitude.Text)) :
                rb_name.Checked ?
                string.Format("select distinct Convert(numeric, N.NodeID)as NodeID,N.Longitude,N.Latitude , NT.TagValue from Nodes N  inner join NodeTags NT on N.NodeID=NT.NodeID and NT.TagName = 'name' Where NT.TagValue like N'%{0}%'", txt_Name.Text) :
                string.Format("select distinct Convert(numeric, N.NodeID)as NodeID,N.Longitude,N.Latitude, NT.TagValue from Nodes N "
                +"inner join NodeTags NT on N.NodeID=NT.NodeID and NT.TagName = 'name'"
                +"inner join NodeTags NT2 on N.NodeID=NT2.NodeID "
                +"where NT2.TagName = 'amenity' and NT2.TagValue='{0}'", cb_category.Text);



            SqlDataAdapter da = new SqlDataAdapter( command,Helper.connectionstring);
            DataSet result = new DataSet();
            da.Fill(result);
            dgv_results.DataSource = null;
                if(result.Tables.Count>0)
                    dgv_results.DataSource= result.Tables[0];
            dgv_results.Refresh();
            dgv_results_CellClick(null, null);

            foreach (DataGridViewRow row in dgv_results.Rows)
            {
                mapViewer.HighlightNode(Convert.ToInt32(row.Cells["NodeID"].Value), new Pen(Color.Red, 4));
            }

            this.Cursor = Cursors.Default;

        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            txt_longitude.Enabled = rb_cordinates.Checked;
            txt_latitude.Enabled = rb_cordinates.Checked;
            txt_Name.Enabled = rb_name.Checked;
            cb_category.Enabled = rb_category.Checked;
            cb_category.SelectedIndex = cb_category.Enabled && cb_category.Items.Count > 0 ? 0 : -1;
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
