namespace MapsUI
{
    partial class MainForm
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
            this.btn_SelectRoad = new System.Windows.Forms.Button();
            this.btn_SelectNode = new System.Windows.Forms.Button();
            this.btn_BFS = new System.Windows.Forms.Button();
            this.btn_Dijkstra = new System.Windows.Forms.Button();
            this.btnNearestNode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_SelectRoad
            // 
            this.btn_SelectRoad.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_SelectRoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SelectRoad.Location = new System.Drawing.Point(0, 0);
            this.btn_SelectRoad.Name = "btn_SelectRoad";
            this.btn_SelectRoad.Size = new System.Drawing.Size(784, 84);
            this.btn_SelectRoad.TabIndex = 0;
            this.btn_SelectRoad.Text = "SelectRoad";
            this.btn_SelectRoad.UseVisualStyleBackColor = true;
            this.btn_SelectRoad.Click += new System.EventHandler(this.btn_SelectRoad_Click);
            // 
            // btn_SelectNode
            // 
            this.btn_SelectNode.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_SelectNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SelectNode.Location = new System.Drawing.Point(0, 84);
            this.btn_SelectNode.Name = "btn_SelectNode";
            this.btn_SelectNode.Size = new System.Drawing.Size(784, 84);
            this.btn_SelectNode.TabIndex = 1;
            this.btn_SelectNode.Text = "Select Node";
            this.btn_SelectNode.UseVisualStyleBackColor = true;
            this.btn_SelectNode.Click += new System.EventHandler(this.btn_SelectNode_Click);
            // 
            // btn_BFS
            // 
            this.btn_BFS.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_BFS.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_BFS.Location = new System.Drawing.Point(0, 168);
            this.btn_BFS.Name = "btn_BFS";
            this.btn_BFS.Size = new System.Drawing.Size(784, 84);
            this.btn_BFS.TabIndex = 2;
            this.btn_BFS.Text = "Breadth First Search";
            this.btn_BFS.UseVisualStyleBackColor = true;
            this.btn_BFS.Click += new System.EventHandler(this.btn_BFS_Click);
            // 
            // btn_Dijkstra
            // 
            this.btn_Dijkstra.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Dijkstra.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Dijkstra.Location = new System.Drawing.Point(0, 252);
            this.btn_Dijkstra.Name = "btn_Dijkstra";
            this.btn_Dijkstra.Size = new System.Drawing.Size(784, 84);
            this.btn_Dijkstra.TabIndex = 3;
            this.btn_Dijkstra.Text = "Dijkstra";
            this.btn_Dijkstra.UseVisualStyleBackColor = true;
            this.btn_Dijkstra.Click += new System.EventHandler(this.btn_Dijkstra_Click);
            // 
            // btnNearestNode
            // 
            this.btnNearestNode.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNearestNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNearestNode.Location = new System.Drawing.Point(0, 336);
            this.btnNearestNode.Name = "btnNearestNode";
            this.btnNearestNode.Size = new System.Drawing.Size(784, 84);
            this.btnNearestNode.TabIndex = 4;
            this.btnNearestNode.Text = "Get Nearest From Node";
            this.btnNearestNode.UseVisualStyleBackColor = true;
            this.btnNearestNode.Click += new System.EventHandler(this.btnNearestNode_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.btnNearestNode);
            this.Controls.Add(this.btn_Dijkstra);
            this.Controls.Add(this.btn_BFS);
            this.Controls.Add(this.btn_SelectNode);
            this.Controls.Add(this.btn_SelectRoad);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_SelectRoad;
        private System.Windows.Forms.Button btn_SelectNode;
        private System.Windows.Forms.Button btn_BFS;
        private System.Windows.Forms.Button btn_Dijkstra;
        private System.Windows.Forms.Button btnNearestNode;
    }
}

