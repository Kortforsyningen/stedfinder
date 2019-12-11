namespace GeodataStyrelsen.ArcMap.PlaceFinder
{
    partial class ConfigurationsForm
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
            this.SearchRequestResourcesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.okButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchRequestResourcesCheckedListBox
            // 
            this.SearchRequestResourcesCheckedListBox.CheckOnClick = true;
            this.SearchRequestResourcesCheckedListBox.FormattingEnabled = true;
            this.SearchRequestResourcesCheckedListBox.Items.AddRange(new object[] {
            "Adresser",
            "Veje",
            "Husnumre",
            "Kommuner",
            "Matrikelnumre",
            "Stednavne",
            "Stednavne v2",
            "Stednavne v3",
            "Opstillingskredse",
            "Politikredse",
            "Postdistrikter",
            "Regioner",
            "Retskredse",
            "Sogne"});
            this.SearchRequestResourcesCheckedListBox.Location = new System.Drawing.Point(3, 26);
            this.SearchRequestResourcesCheckedListBox.Name = "SearchRequestResourcesCheckedListBox";
            this.SearchRequestResourcesCheckedListBox.Size = new System.Drawing.Size(108, 214);
            this.SearchRequestResourcesCheckedListBox.TabIndex = 0;
            this.SearchRequestResourcesCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.SearchRequestResourcesCheckedListBox_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.okButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.SearchRequestResourcesCheckedListBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(114, 278);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // okButton
            // 
            this.okButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.okButton.Location = new System.Drawing.Point(3, 246);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(108, 29);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Versionsinfo";
            // 
            // ConfigurationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(114, 278);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ConfigurationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Søgeregistre";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigurationsForm_FormClosing);
            this.Load += new System.EventHandler(this.ConfigurationsForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox SearchRequestResourcesCheckedListBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label1;
    }
}