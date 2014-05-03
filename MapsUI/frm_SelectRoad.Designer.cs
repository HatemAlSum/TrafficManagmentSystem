namespace MapsUI
{
    partial class frm_SelectRoad
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_results = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPick = new System.Windows.Forms.Button();
            this.btn_Decrease = new System.Windows.Forms.Button();
            this.btn_Increase = new System.Windows.Forms.Button();
            this.rb_cordinates = new System.Windows.Forms.RadioButton();
            this.btn_Search = new System.Windows.Forms.Button();
            this.rb_name = new System.Windows.Forms.RadioButton();
            this.txt_longitude = new System.Windows.Forms.TextBox();
            this.txt_latitude = new System.Windows.Forms.TextBox();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_profile = new System.Windows.Forms.DataGridView();
            this.mapViewer = new MapViewer.MapViewer();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_results)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_profile)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_results);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 562);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Criteria";
            // 
            // dgv_results
            // 
            this.dgv_results.AllowUserToAddRows = false;
            this.dgv_results.AllowUserToDeleteRows = false;
            this.dgv_results.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgv_results.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_results.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_results.Location = new System.Drawing.Point(3, 95);
            this.dgv_results.MultiSelect = false;
            this.dgv_results.Name = "dgv_results";
            this.dgv_results.ReadOnly = true;
            this.dgv_results.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_results.Size = new System.Drawing.Size(462, 345);
            this.dgv_results.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPick);
            this.panel1.Controls.Add(this.btn_Decrease);
            this.panel1.Controls.Add(this.btn_Increase);
            this.panel1.Controls.Add(this.rb_cordinates);
            this.panel1.Controls.Add(this.btn_Search);
            this.panel1.Controls.Add(this.rb_name);
            this.panel1.Controls.Add(this.txt_longitude);
            this.panel1.Controls.Add(this.txt_latitude);
            this.panel1.Controls.Add(this.txt_Name);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 79);
            this.panel1.TabIndex = 6;
            // 
            // btnPick
            // 
            this.btnPick.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnPick.Location = new System.Drawing.Point(373, 1);
            this.btnPick.Name = "btnPick";
            this.btnPick.Size = new System.Drawing.Size(24, 23);
            this.btnPick.TabIndex = 8;
            this.btnPick.Text = "P";
            this.btnPick.UseVisualStyleBackColor = true;
            this.btnPick.Click += new System.EventHandler(this.btnPick_Click);
            // 
            // btn_Decrease
            // 
            this.btn_Decrease.Location = new System.Drawing.Point(404, 56);
            this.btn_Decrease.Name = "btn_Decrease";
            this.btn_Decrease.Size = new System.Drawing.Size(24, 23);
            this.btn_Decrease.TabIndex = 7;
            this.btn_Decrease.Text = "-";
            this.btn_Decrease.UseVisualStyleBackColor = true;
            this.btn_Decrease.Click += new System.EventHandler(this.btn_Decrease_Click);
            // 
            // btn_Increase
            // 
            this.btn_Increase.Location = new System.Drawing.Point(374, 56);
            this.btn_Increase.Name = "btn_Increase";
            this.btn_Increase.Size = new System.Drawing.Size(24, 23);
            this.btn_Increase.TabIndex = 6;
            this.btn_Increase.Text = "+";
            this.btn_Increase.UseVisualStyleBackColor = true;
            this.btn_Increase.Click += new System.EventHandler(this.btn_Increase_Click);
            // 
            // rb_cordinates
            // 
            this.rb_cordinates.AutoSize = true;
            this.rb_cordinates.Checked = true;
            this.rb_cordinates.Location = new System.Drawing.Point(9, 3);
            this.rb_cordinates.Name = "rb_cordinates";
            this.rb_cordinates.Size = new System.Drawing.Size(96, 17);
            this.rb_cordinates.TabIndex = 0;
            this.rb_cordinates.TabStop = true;
            this.rb_cordinates.Text = "By Coordinates";
            this.rb_cordinates.UseVisualStyleBackColor = true;
            this.rb_cordinates.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(373, 27);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(75, 23);
            this.btn_Search.TabIndex = 5;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // rb_name
            // 
            this.rb_name.AutoSize = true;
            this.rb_name.Location = new System.Drawing.Point(9, 27);
            this.rb_name.Name = "rb_name";
            this.rb_name.Size = new System.Drawing.Size(68, 17);
            this.rb_name.TabIndex = 3;
            this.rb_name.Text = "By Name";
            this.rb_name.UseVisualStyleBackColor = true;
            this.rb_name.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // txt_longitude
            // 
            this.txt_longitude.Location = new System.Drawing.Point(151, 3);
            this.txt_longitude.Name = "txt_longitude";
            this.txt_longitude.Size = new System.Drawing.Size(100, 20);
            this.txt_longitude.TabIndex = 1;
            this.txt_longitude.Text = "31.287644";
            // 
            // txt_latitude
            // 
            this.txt_latitude.Location = new System.Drawing.Point(267, 3);
            this.txt_latitude.Name = "txt_latitude";
            this.txt_latitude.Size = new System.Drawing.Size(100, 20);
            this.txt_latitude.TabIndex = 2;
            this.txt_latitude.Text = "30.050719";
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(151, 27);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(216, 20);
            this.txt_Name.TabIndex = 4;
            this.txt_Name.Text = "صلاح سالم";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_profile);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 440);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(462, 119);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WayProfile";
            // 
            // dgv_profile
            // 
            this.dgv_profile.AllowUserToAddRows = false;
            this.dgv_profile.AllowUserToDeleteRows = false;
            this.dgv_profile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_profile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_profile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_profile.Location = new System.Drawing.Point(3, 16);
            this.dgv_profile.Name = "dgv_profile";
            this.dgv_profile.ReadOnly = true;
            this.dgv_profile.Size = new System.Drawing.Size(456, 100);
            this.dgv_profile.TabIndex = 3;
            // 
            // mapViewer
            // 
            this.mapViewer.DefaultZoom = 2048F;
            this.mapViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapViewer.Location = new System.Drawing.Point(468, 0);
            this.mapViewer.Name = "mapViewer";
            this.mapViewer.Size = new System.Drawing.Size(316, 562);
            this.mapViewer.TabIndex = 3;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(468, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 562);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // frm_SelectRoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.mapViewer);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_SelectRoad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Road";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.rb_CheckedChanged);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_results)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_profile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.TextBox txt_latitude;
        private System.Windows.Forms.TextBox txt_longitude;
        private System.Windows.Forms.RadioButton rb_name;
        private System.Windows.Forms.RadioButton rb_cordinates;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv_profile;
        private MapViewer.MapViewer mapViewer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv_results;
        private System.Windows.Forms.Button btn_Decrease;
        private System.Windows.Forms.Button btn_Increase;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button btnPick;
    }
}