using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.esriSystem;

namespace PlaceFinder
{
    public class PlaceFinderButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        private ESRI.ArcGIS.Framework.IDockableWindow _mDockableWindow;

        public PlaceFinderButton()
        {
            Enabled = _mDockableWindow != null;
        }

        public bool IsActiv
        {
            get { return Enabled; }
        }

        public void Activate()
        {
            OnClick();
        }

        protected override void OnClick()
        {
            if (DockableWindow == null)
                return;

            DockableWindow.Show(!DockableWindow.IsVisible());

            Checked = DockableWindow.IsVisible();

        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
            Checked = _mDockableWindow != null && _mDockableWindow.IsVisible();
        }

        private ESRI.ArcGIS.Framework.IDockableWindow DockableWindow
        {
            get
            {
                if (_mDockableWindow == null)
                {
                    var addinImpl = ArcMap.DockableWindowManager.GetDockableWindow(new UIDClass
                            {
                                Value = ThisAddIn.IDs.PlaceFinderDockableWindow
                            });
                    _mDockableWindow = addinImpl;
                }
                return _mDockableWindow;
            }
        }
    }

}
