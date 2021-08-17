namespace Q4_gui
{
    partial class SpreadsheetApp
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.LoadButton = new System.Windows.Forms.Button();
            this.SetCellButton = new System.Windows.Forms.Button();
            this.SearchStringButton = new System.Windows.Forms.Button();
            this.AddRowButton = new System.Windows.Forms.Button();
            this.AddColumnButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.GridColor = System.Drawing.Color.MidnightBlue;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(891, 573);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // LoadButton
            // 
            this.LoadButton.AccessibleName = "";
            this.LoadButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.LoadButton.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadButton.Location = new System.Drawing.Point(901, 3);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(230, 80);
            this.LoadButton.TabIndex = 1;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = false;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // SetCellButton
            // 
            this.SetCellButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.SetCellButton.Font = new System.Drawing.Font("Verdana", 14F);
            this.SetCellButton.Location = new System.Drawing.Point(901, 205);
            this.SetCellButton.Name = "SetCellButton";
            this.SetCellButton.Size = new System.Drawing.Size(230, 80);
            this.SetCellButton.TabIndex = 2;
            this.SetCellButton.Text = "Set Cell";
            this.SetCellButton.UseVisualStyleBackColor = false;
            this.SetCellButton.Click += new System.EventHandler(this.SetCellButton_Click);
            // 
            // SearchStringButton
            // 
            this.SearchStringButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.SearchStringButton.Font = new System.Drawing.Font("Verdana", 14F);
            this.SearchStringButton.Location = new System.Drawing.Point(900, 305);
            this.SearchStringButton.Name = "SearchStringButton";
            this.SearchStringButton.Size = new System.Drawing.Size(230, 80);
            this.SearchStringButton.TabIndex = 3;
            this.SearchStringButton.Text = "Search String";
            this.SearchStringButton.UseVisualStyleBackColor = false;
            this.SearchStringButton.Click += new System.EventHandler(this.SearchStringButton_Click);
            // 
            // AddRowButton
            // 
            this.AddRowButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.AddRowButton.Font = new System.Drawing.Font("Verdana", 14F);
            this.AddRowButton.Location = new System.Drawing.Point(901, 402);
            this.AddRowButton.Name = "AddRowButton";
            this.AddRowButton.Size = new System.Drawing.Size(230, 80);
            this.AddRowButton.TabIndex = 4;
            this.AddRowButton.Text = "Add Row";
            this.AddRowButton.UseVisualStyleBackColor = false;
            this.AddRowButton.Click += new System.EventHandler(this.AddRowButton_Click);
            // 
            // AddColumnButton
            // 
            this.AddColumnButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.AddColumnButton.Font = new System.Drawing.Font("Verdana", 14F);
            this.AddColumnButton.Location = new System.Drawing.Point(902, 498);
            this.AddColumnButton.Name = "AddColumnButton";
            this.AddColumnButton.Size = new System.Drawing.Size(230, 80);
            this.AddColumnButton.TabIndex = 5;
            this.AddColumnButton.Text = "Add Column";
            this.AddColumnButton.UseVisualStyleBackColor = false;
            this.AddColumnButton.Click += new System.EventHandler(this.AddColumnButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.SaveButton.Font = new System.Drawing.Font("Verdana", 14F);
            this.SaveButton.Location = new System.Drawing.Point(901, 105);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(230, 80);
            this.SaveButton.TabIndex = 6;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 583);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.AddColumnButton);
            this.Controls.Add(this.AddRowButton);
            this.Controls.Add(this.SearchStringButton);
            this.Controls.Add(this.SetCellButton);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "SpreadsheetApp";
            this.Text = "SpreadsheetApp";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button SetCellButton;
        private System.Windows.Forms.Button SearchStringButton;
        private System.Windows.Forms.Button AddRowButton;
        private System.Windows.Forms.Button AddColumnButton;
        private System.Windows.Forms.Button SaveButton;
    }
}

