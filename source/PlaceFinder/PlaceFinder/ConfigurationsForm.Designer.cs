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
            this.SuspendLayout();
            // 
            // SearchRequestResourcesCheckedListBox
            // 
            this.SearchRequestResourcesCheckedListBox.CheckOnClick = true;
            this.SearchRequestResourcesCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchRequestResourcesCheckedListBox.FormattingEnabled = true;
            this.SearchRequestResourcesCheckedListBox.Items.AddRange(new object[] {
            "Adresser",
            "Veje",
            "Husnumre",
            "Kommuner",
            "Matrikelnumre",
            "Stednavne",
            "Opstillingskredse",
            "Politikredse",
            "Postdistrikter",
            "Regioner",
            "Retskredse"});
            this.SearchRequestResourcesCheckedListBox.Location = new System.Drawing.Point(0, 0);
            this.SearchRequestResourcesCheckedListBox.Name = "SearchRequestResourcesCheckedListBox";
            this.SearchRequestResourcesCheckedListBox.Size = new System.Drawing.Size(131, 175);
            this.SearchRequestResourcesCheckedListBox.TabIndex = 0;
            // 
            // ConfigurationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(131, 175);
            this.Controls.Add(this.SearchRequestResourcesCheckedListBox);
            this.Name = "ConfigurationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ConfigurationsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigurationsForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox SearchRequestResourcesCheckedListBox;
    }
}