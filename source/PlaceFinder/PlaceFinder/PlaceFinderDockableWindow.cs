﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;

namespace GeodataStyrelsen.ArcMap.PlaceFinder
{
    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
    public partial class PlaceFinderDockableWindow : UserControl, IPlaceFinderDockableWindow
    {
        public IPlaceFinderController PlaceFinderController { get; private set; }
        private readonly ConfigurationsForm configurationsForm;
        
        public PlaceFinderDockableWindow(object hook)
        {
            InitializeComponent();
            var factory = new Factory();
            factory.PlaceFinderDockableWindow = this;
            PlaceFinderController = new PlaceFinderController(factory);

            // This string array defines the default search areas. The strings must match the elements in the 
            // Configurations form exactly
            string[] defaultConfig = new string[] { "Stednavn" };

            // Create the configurations form
            configurationsForm = new ConfigurationsForm(PlaceFinderController, defaultConfig);

            this.Hook = hook;
        }

        /// <summary>
        /// Host object of the dockable window
        /// </summary>
        private object Hook
        {
            get;
            set;
        }

        /// <summary>
        /// Implementation class of the dockable window add-in. It is responsible for 
        /// creating and disposing the user interface class of the dockable window.
        /// </summary>
        public class AddinImpl : ESRI.ArcGIS.Desktop.AddIns.DockableWindow
        {
            private PlaceFinderDockableWindow m_windowUI;

            public AddinImpl()
            {
                Application.ThreadException += MYThreadHandler;
            }

            private void MYThreadHandler(object sender, ThreadExceptionEventArgs e)
            {
                if (e.Exception is PlaceFinderException)
                {
                    MessageBox.Show("Fejl: " + e.Exception.Message);
                }
                else
                {
                    MessageBox.Show("Unhandled error in thread: " + Environment.NewLine + e.Exception);
                }
            }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new PlaceFinderDockableWindow(Hook);
                return m_windowUI.Handle;
            }

            protected override void Dispose(bool disposing)
            {
                if (m_windowUI != null)
                    m_windowUI.Dispose(disposing);

                base.Dispose(disposing);
            }

        }

        private void onSearchTextChanged(object sender, EventArgs e)
        {
            if (searchTextBox.Focused)
            {
                PlaceFinderController.SearchTextChange(searchTextBox.Text);
            }
        }

        public void AddSearchResult(List<GeoSearchAddress> geoSearchAddresses)
        {
            searchTextBox.BringToFront();
            searchResultComboBox.DroppedDown = geoSearchAddresses.Count > 0;
            Cursor.Current = Cursors.Default;
            searchResultComboBox.Items.Clear();
            searchResultComboBox.DisplayMember = "visningstekst";
            if (geoSearchAddresses.Count > 0)
            {
                searchResultComboBox.Items.AddRange(geoSearchAddresses.ToArray());
            }
            searchTextBox.Focus();
            searchTextBox.BringToFront();
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            configurationsForm.Location = configButton.PointToScreen(Point.Empty);
            configurationsForm.ShowDialog();
        }

        private void onSearchTextKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    searchResultComboBox.Focus();
                    break;
            }

        }

        private void onSearchResultKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    var selected = (GeoSearchAddress)searchResultComboBox.SelectedItem;
                    if (selected != null) ZoomTo(selected);
                    else
                        Debug.WriteLine("Zoom to null ignored. No selected item");
                    break;
                case  Keys.Up:
                    if (searchResultComboBox.SelectedIndex == 1)
                    {
                        searchTextBox.Focus();
                    }
                    break;
            }
        }
        
        private void ZoomTo(GeoSearchAddress selected)
        {
            if (PlaceFinderController != null)
            {
                searchResultComboBox.Focus();
                PlaceFinderController.ZoomTo(selected);
                searchTextBox.Text = selected.Visningstekst;
            }
        }

        private void searchResultComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = searchResultComboBox.SelectedItem;
            ZoomTo((GeoSearchAddress)selected);
        }
    }
}
