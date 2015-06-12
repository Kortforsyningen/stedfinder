using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;

namespace GeodataStyrelsen.ArcMap.PlaceFinder
{
    partial class PlaceFinderDockableWindow
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.configButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchResultComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Controls.Add(this.configButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(238, 29);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // configButton
            // 
            this.configButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configButton.Image = global::GeodataStyrelsen.ArcMap.PlaceFinder.Properties.Resources.TableShowAllRecords16;
            this.configButton.Location = new System.Drawing.Point(209, 3);
            this.configButton.MaximumSize = new System.Drawing.Size(22, 23);
            this.configButton.MinimumSize = new System.Drawing.Size(22, 23);
            this.configButton.Name = "configButton";
            this.configButton.Size = new System.Drawing.Size(22, 23);
            this.configButton.TabIndex = 2;
            this.configButton.Click += new System.EventHandler(this.configButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.searchTextBox);
            this.panel1.Controls.Add(this.searchResultComboBox);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 21);
            this.panel1.TabIndex = 4;
            // 
            // searchTextBox
            // 
            this.searchTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.searchTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchTextBox.Location = new System.Drawing.Point(0, 0);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(200, 20);
            this.searchTextBox.TabIndex = 0;
            this.searchTextBox.TextChanged += new System.EventHandler(this.onSearchTextChanged);
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onSearchTextKeyDown);
            // 
            // searchResultComboBox
            // 
            this.searchResultComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchResultComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchResultComboBox.FormattingEnabled = true;
            this.searchResultComboBox.Location = new System.Drawing.Point(0, 0);
            this.searchResultComboBox.Name = "searchResultComboBox";
            this.searchResultComboBox.Size = new System.Drawing.Size(200, 21);
            this.searchResultComboBox.TabIndex = 3;
            this.searchResultComboBox.SelectedIndexChanged += new System.EventHandler(this.searchResultComboBox_SelectedIndexChanged);
            this.searchResultComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onSearchResultKeyDown);
            // 
            // PlaceFinderDockableWindow
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PlaceFinderDockableWindow";
            this.Size = new System.Drawing.Size(238, 29);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button configButton;
        private System.Windows.Forms.ComboBox searchResultComboBox;
        private System.Windows.Forms.Panel panel1;

    }
}
