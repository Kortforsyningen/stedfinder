using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using PlaceFinder.GeoSearch;

namespace PlaceFinder
{
    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
    public partial class PlaceFinderDockableWindow : UserControl
    {
        public PlaceFindeController PlaceFindeController { get; private set; }

        public PlaceFinderDockableWindow(object hook)
        {
            InitializeComponent();
            PlaceFindeController = new PlaceFindeController(this, ArcMap.Document);
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
            }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new PlaceFinderDockableWindow(this.Hook);
                return m_windowUI.Handle;
            }

            protected override void Dispose(bool disposing)
            {
                if (m_windowUI != null)
                    m_windowUI.Dispose(disposing);

                base.Dispose(disposing);
            }

        }

        private void onZoomTo_Click(object sender, EventArgs e)
        {
            PlaceFindeController.ZoomTo(searchTextBox.SelectedText);
        }

        private void onSearchTextChanged(object sender, EventArgs e)
        {
            PlaceFindeController.SearchTextChange(searchTextBox.Text);
        }

        private void onSearchTextKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    break;
            }
        }

        private void onSearchTextKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)//(char) 13) //enter
            {
                zoomToButton.Focus();
                e.Handled = true;
            }
        }

        public void AddSearchResult(List<string> geoSearchAddresses)
        {
            var autoCompleteStringCollection = new AutoCompleteStringCollection();
            autoCompleteStringCollection.AddRange(geoSearchAddresses.ToArray());
            searchTextBox.AutoCompleteCustomSource = autoCompleteStringCollection;
        }
    }
}
