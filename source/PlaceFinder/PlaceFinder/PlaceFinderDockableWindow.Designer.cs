namespace PlaceFinder
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.zoomToButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.geoSearchAddressBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.geoSearchAddressBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.zoomToButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.searchTextBox, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(300, 25);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // zoomToButton
            // 
            this.zoomToButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoomToButton.Location = new System.Drawing.Point(222, 3);
            this.zoomToButton.Name = "zoomToButton";
            this.zoomToButton.Size = new System.Drawing.Size(75, 21);
            this.zoomToButton.TabIndex = 0;
            this.zoomToButton.Text = "Zoom til";
            this.zoomToButton.UseVisualStyleBackColor = true;
            this.zoomToButton.Click += new System.EventHandler(this.onZoomTo_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.searchTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchTextBox.Location = new System.Drawing.Point(3, 3);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(213, 20);
            this.searchTextBox.TabIndex = 1;
            this.searchTextBox.TextChanged += new System.EventHandler(this.onSearchTextChanged);
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onSearchTextKeyDown);
            this.searchTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onSearchTextKeyPress);
            // 
            // geoSearchAddressBindingSource
            // 
            this.geoSearchAddressBindingSource.DataSource = typeof(PlaceFinder.GeoSearch.GeoSearchAddress);
            // 
            // PlaceFinderDockableWindow
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PlaceFinderDockableWindow";
            this.Size = new System.Drawing.Size(300, 25);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.geoSearchAddressBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button zoomToButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.BindingSource geoSearchAddressBindingSource;

    }
}
