namespace MapsUI
{
    partial class frm_BFScs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BFScs));
            this.lbl_Source = new System.Windows.Forms.Label();
            this.btn_selectNode2 = new System.Windows.Forms.PictureBox();
            this.txt_Src = new System.Windows.Forms.TextBox();
            this.btn_selectNode1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Dst = new System.Windows.Forms.TextBox();
            this.mapViewer = new MapViewer.MapViewer();
            this.dgv_results = new System.Windows.Forms.DataGridView();
            this.btn_Search = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.btn_selectNode2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_selectNode1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_results)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Source
            // 
            this.lbl_Source.AutoSize = true;
            this.lbl_Source.Location = new System.Drawing.Point(14, 11);
            this.lbl_Source.Name = "lbl_Source";
            this.lbl_Source.Size = new System.Drawing.Size(47, 13);
            this.lbl_Source.TabIndex = 9;
            this.lbl_Source.Text = "Source :";
            // 
            // btn_selectNode2
            // 
            this.btn_selectNode2.Image = ((System.Drawing.Image)(resources.GetObject("btn_selectNode2.Image")));
            this.btn_selectNode2.Location = new System.Drawing.Point(276, 33);
            this.btn_selectNode2.Name = "btn_selectNode2";
            this.btn_selectNode2.Size = new System.Drawing.Size(19, 21);
            this.btn_selectNode2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_selectNode2.TabIndex = 13;
            this.btn_selectNode2.TabStop = false;
            this.btn_selectNode2.Click += new System.EventHandler(this.btn_selectNode2_Click);
            // 
            // txt_Src
            // 
            this.txt_Src.Location = new System.Drawing.Point(170, 11);
            this.txt_Src.Name = "txt_Src";
            this.txt_Src.Size = new System.Drawing.Size(100, 20);
            this.txt_Src.TabIndex = 1;
            this.txt_Src.Text = "96621052";
            // 
            // btn_selectNode1
            // 
            this.btn_selectNode1.Image = ((System.Drawing.Image)(resources.GetObject("btn_selectNode1.Image")));
            this.btn_selectNode1.Location = new System.Drawing.Point(276, 10);
            this.btn_selectNode1.Name = "btn_selectNode1";
            this.btn_selectNode1.Size = new System.Drawing.Size(19, 21);
            this.btn_selectNode1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_selectNode1.TabIndex = 12;
            this.btn_selectNode1.TabStop = false;
            this.btn_selectNode1.Click += new System.EventHandler(this.btn_selectNode1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Destination :";
            // 
            // txt_Dst
            // 
            this.txt_Dst.Location = new System.Drawing.Point(170, 34);
            this.txt_Dst.Name = "txt_Dst";
            this.txt_Dst.Size = new System.Drawing.Size(100, 20);
            this.txt_Dst.TabIndex = 2;
            this.txt_Dst.Text = "339744928";
            // 
            // mapViewer
            // 
            this.mapViewer.DefaultZoom = 2048F;
            this.mapViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapViewer.Location = new System.Drawing.Point(450, 0);
            this.mapViewer.Name = "mapViewer";
            this.mapViewer.Size = new System.Drawing.Size(334, 562);
            this.mapViewer.TabIndex = 7;
            // 
            // dgv_results
            // 
            this.dgv_results.AllowUserToAddRows = false;
            this.dgv_results.AllowUserToDeleteRows = false;
            this.dgv_results.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_results.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_results.Location = new System.Drawing.Point(3, 115);
            this.dgv_results.MultiSelect = false;
            this.dgv_results.Name = "dgv_results";
            this.dgv_results.ReadOnly = true;
            this.dgv_results.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_results.Size = new System.Drawing.Size(444, 444);
            this.dgv_results.TabIndex = 15;
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(301, 31);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(75, 23);
            this.btn_Search.TabIndex = 7;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_results);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 562);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Criteria";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_Source);
            this.panel1.Controls.Add(this.btn_selectNode2);
            this.panel1.Controls.Add(this.txt_Src);
            this.panel1.Controls.Add(this.btn_selectNode1);
            this.panel1.Controls.Add(this.txt_Dst);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_Search);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 99);
            this.panel1.TabIndex = 14;
            // 
            // frm_BFScs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.mapViewer);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_BFScs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Breadth First Search";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.btn_selectNode2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_selectNode1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_results)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Source;
        private System.Windows.Forms.PictureBox btn_selectNode2;
        private System.Windows.Forms.TextBox txt_Src;
        private System.Windows.Forms.PictureBox btn_selectNode1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Dst;
        private MapViewer.MapViewer mapViewer;
        private System.Windows.Forms.DataGridView dgv_results;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;

    }
}