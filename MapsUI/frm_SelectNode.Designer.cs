namespace MapsUI
{
    partial class frm_SelectNode
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_profile = new System.Windows.Forms.DataGridView();
            this.dgv_results = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rb_category = new System.Windows.Forms.RadioButton();
            this.btn_Search = new System.Windows.Forms.Button();
            this.cb_category = new System.Windows.Forms.ComboBox();
            this.rb_cordinates = new System.Windows.Forms.RadioButton();
            this.rb_name = new System.Windows.Forms.RadioButton();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.txt_longitude = new System.Windows.Forms.TextBox();
            this.txt_latitude = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.mapViewer = new MapViewer.MapViewer();
            this.btnPick = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_profile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_results)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.dgv_results);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(506, 562);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Criteria";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_profile);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 442);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 117);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Node Tags";
            // 
            // dgv_profile
            // 
            this.dgv_profile.AllowUserToAddRows = false;
            this.dgv_profile.AllowUserToDeleteRows = false;
            this.dgv_profile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgv_profile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_profile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_profile.Location = new System.Drawing.Point(3, 16);
            this.dgv_profile.Name = "dgv_profile";
            this.dgv_profile.ReadOnly = true;
            this.dgv_profile.Size = new System.Drawing.Size(494, 98);
            this.dgv_profile.TabIndex = 3;
            // 
            // dgv_results
            // 
            this.dgv_results.AllowUserToAddRows = false;
            this.dgv_results.AllowUserToDeleteRows = false;
            this.dgv_results.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgv_results.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_results.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_results.Location = new System.Drawing.Point(3, 121);
            this.dgv_results.MultiSelect = false;
            this.dgv_results.Name = "dgv_results";
            this.dgv_results.ReadOnly = true;
            this.dgv_results.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_results.Size = new System.Drawing.Size(500, 438);
            this.dgv_results.TabIndex = 1;
            this.dgv_results.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_results_CellClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPick);
            this.panel1.Controls.Add(this.rb_category);
            this.panel1.Controls.Add(this.btn_Search);
            this.panel1.Controls.Add(this.cb_category);
            this.panel1.Controls.Add(this.rb_cordinates);
            this.panel1.Controls.Add(this.rb_name);
            this.panel1.Controls.Add(this.txt_Name);
            this.panel1.Controls.Add(this.txt_longitude);
            this.panel1.Controls.Add(this.txt_latitude);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 105);
            this.panel1.TabIndex = 5;
            // 
            // rb_category
            // 
            this.rb_category.AutoSize = true;
            this.rb_category.Location = new System.Drawing.Point(13, 54);
            this.rb_category.Name = "rb_category";
            this.rb_category.Size = new System.Drawing.Size(85, 17);
            this.rb_category.TabIndex = 5;
            this.rb_category.Text = "By Category";
            this.rb_category.UseVisualStyleBackColor = true;
            this.rb_category.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(406, 51);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(75, 23);
            this.btn_Search.TabIndex = 7;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // cb_category
            // 
            this.cb_category.FormattingEnabled = true;
            this.cb_category.Items.AddRange(new object[] {
            "arts_centre",
            "atm",
            "bank",
            "binoculars",
            "bus_station",
            "cafe",
            "cinema",
            "courthouse",
            "embassy",
            "fast_food",
            "ferry_terminal",
            "fire_station",
            "fountain",
            "fuel",
            "helipad",
            "hospital",
            "library",
            "marketplace",
            "nightclub",
            "parking",
            "pharmacy",
            "place_of_worship",
            "police",
            "post_box",
            "post_office",
            "public_building",
            "restaurant",
            "school",
            "theatre",
            "toilets",
            "training_centre",
            "university",
            "waste_basket"});
            this.cb_category.Location = new System.Drawing.Point(155, 53);
            this.cb_category.Name = "cb_category";
            this.cb_category.Size = new System.Drawing.Size(216, 21);
            this.cb_category.TabIndex = 6;
            // 
            // rb_cordinates
            // 
            this.rb_cordinates.AutoSize = true;
            this.rb_cordinates.Checked = true;
            this.rb_cordinates.Location = new System.Drawing.Point(13, 7);
            this.rb_cordinates.Name = "rb_cordinates";
            this.rb_cordinates.Size = new System.Drawing.Size(98, 17);
            this.rb_cordinates.TabIndex = 0;
            this.rb_cordinates.TabStop = true;
            this.rb_cordinates.Text = "By Coordinates";
            this.rb_cordinates.UseVisualStyleBackColor = true;
            this.rb_cordinates.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rb_name
            // 
            this.rb_name.AutoSize = true;
            this.rb_name.Location = new System.Drawing.Point(13, 31);
            this.rb_name.Name = "rb_name";
            this.rb_name.Size = new System.Drawing.Size(67, 17);
            this.rb_name.TabIndex = 3;
            this.rb_name.Text = "By Name";
            this.rb_name.UseVisualStyleBackColor = true;
            this.rb_name.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(155, 30);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(216, 20);
            this.txt_Name.TabIndex = 4;
            this.txt_Name.Text = "سفارة اليونان";
            // 
            // txt_longitude
            // 
            this.txt_longitude.Location = new System.Drawing.Point(155, 7);
            this.txt_longitude.Name = "txt_longitude";
            this.txt_longitude.Size = new System.Drawing.Size(100, 20);
            this.txt_longitude.TabIndex = 1;
            this.txt_longitude.Text = "31.2182596";
            // 
            // txt_latitude
            // 
            this.txt_latitude.Location = new System.Drawing.Point(271, 7);
            this.txt_latitude.Name = "txt_latitude";
            this.txt_latitude.Size = new System.Drawing.Size(100, 20);
            this.txt_latitude.TabIndex = 2;
            this.txt_latitude.Text = "30.0599323";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(506, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 562);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // mapViewer
            // 
            this.mapViewer.DefaultZoom = 2048F;
            this.mapViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapViewer.Location = new System.Drawing.Point(506, 0);
            this.mapViewer.Name = "mapViewer";
            this.mapViewer.Size = new System.Drawing.Size(278, 562);
            this.mapViewer.TabIndex = 4;
            // 
            // btnPick
            // 
            this.btnPick.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnPick.Location = new System.Drawing.Point(377, 7);
            this.btnPick.Name = "btnPick";
            this.btnPick.Size = new System.Drawing.Size(24, 23);
            this.btnPick.TabIndex = 9;
            this.btnPick.Text = "P";
            this.btnPick.UseVisualStyleBackColor = true;
            this.btnPick.Click += new System.EventHandler(this.btnPick_Click);
            // 
            // frm_SelectNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.mapViewer);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_SelectNode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Node";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.rb_CheckedChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_profile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_results)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.TextBox txt_latitude;
        private System.Windows.Forms.TextBox txt_longitude;
        private System.Windows.Forms.RadioButton rb_name;
        private System.Windows.Forms.RadioButton rb_cordinates;
        private System.Windows.Forms.DataGridView dgv_results;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv_profile;
        private System.Windows.Forms.RadioButton rb_category;
        private System.Windows.Forms.ComboBox cb_category;
        private System.Windows.Forms.Panel panel1;
        private MapViewer.MapViewer mapViewer;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button btnPick;
    }
}