using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using PlaceFinder.Interface;

namespace PlaceFinder
{
    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
    public partial class PlaceFinderDockableWindow : UserControl, IPlaceFinderDockableWindow
    {
        public IPlaceFinderController PlaceFinderController { get; private set; }

        public PlaceFinderDockableWindow()
        {
        }

        public PlaceFinderDockableWindow(object hook)
        {
            InitializeComponent();
            var factory = new Factory();
            factory.PlaceFinderDockableWindow = this;
            PlaceFinderController = new PlaceFinderController(factory);
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
                    MessageBox.Show("Error: " + e.Exception.Message);
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

        private void onZoomTo_Click(object sender, EventArgs e)
        {
            PlaceFinderController.ZoomTo(searchTextBox.SelectedText);
        }

        private void onSearchTextChanged(object sender, EventArgs e)
        {
            PlaceFinderController.SearchTextChange(searchTextBox.Text);
        }

        public void AddSearchResult(List<string> geoSearchAddresses)
        {
            var autoCompleteStringCollection = new AutoCompleteStringCollection();
            autoCompleteStringCollection.AddRange(geoSearchAddresses.ToArray());
            searchTextBox.AutoCompleteCustomSource = autoCompleteStringCollection;
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            var configurationsForm = new ConfigurationsForm(PlaceFinderController);
            configurationsForm.Location = configButton.PointToScreen(Point.Empty);
            configurationsForm.ShowDialog();
        }
    }
}
