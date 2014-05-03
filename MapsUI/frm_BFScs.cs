using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MapsUI
{
    public partial class frm_BFScs : Form
    {
        public frm_BFScs()
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
            mapViewer.UnHighlightAllNodes();
            string command =
                string.Format("exec Breadth_First_Search {0},{1}", txt_Src.Text, txt_Dst.Text);
                

            SqlDataAdapter da = new SqlDataAdapter(command, Helper.connectionstring);
            DataSet result = new DataSet();
            da.Fill(result);
            dgv_results.DataSource = null;
            if (result.Tables.Count > 0)
                dgv_results.DataSource = result.Tables[0];
            dgv_results.Refresh();
            foreach (DataGridViewRow row in dgv_results.Rows)
            {
                try
                {
                    if (row.Cells["ID"].Value != null && Convert.ToInt32(row.Cells["ID"].Value) > 0)
                        mapViewer.HighlightNode(Convert.ToInt32(row.Cells["ID"].Value), new Pen(Color.Red, 2));
                }
                catch (Exception ex)
                {
                    
                }

            }
            this.Cursor = Cursors.Default;
        }

       


    }
}
