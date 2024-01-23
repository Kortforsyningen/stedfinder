using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;
using GeodataStyrelsen.ArcMap.PlaceFinder.Properties;
using System.Diagnostics;

namespace GeodataStyrelsen.ArcMap.PlaceFinder
{
    public class PlaceFinderController : IPlaceFinderController
    {
        private readonly IFactory _factory;
        private List<GeoSearchAddress> currentSearch;
        private SearchRequestResources searchRequestResources;

        public PlaceFinderController(IFactory factory)
        {
            _factory = factory;
            // This is initialized for testing purposes only as the application will construct this as part of the 
            // PlaceFinderDockableWindow that will envokes the SearchResourcesChange to update the internal
            // searchRequestResources properly
            searchRequestResources = new SearchRequestResources();
            searchRequestResources.Addresses = true;
            searchRequestResources.PlaceNames = true;
        }

        public void SearchTextChange(string searchString)
        {
            //
            // TODO: Add small extension time if multiple characters are entered in sequence to improve user experience
            //
            if (searchString.Length > 2)
            {
                var geoSearchAddresses = GetAddressData(searchString);
                currentSearch = geoSearchAddresses;
                _factory.PlaceFinderDockableWindow.AddSearchResult(currentSearch);
            }
        }

        private List<GeoSearchAddress> GetAddressData(string inputParamSearch)
        {
            var retValue = new List<GeoSearchAddress>();

            if (!String.IsNullOrEmpty(inputParamSearch))
            {
                var searchRequestParams = new SearchRequestParams
                    {
                        SearchText = inputParamSearch,
                        Resources = searchRequestResources.GetResourceString
                    };
                var response = _factory.GeosearchService.Request(searchRequestParams);

                if (response != null && response.data != null)
                {
                    return response.data;
                }
            }

            return retValue;
        }

        public void ZoomTo(GeoSearchAddress selectedAddress)
        {
            Debug.WriteLine("ZoomTo entry: " + selectedAddress);
            if (selectedAddress == null || string.IsNullOrEmpty(selectedAddress.Visningstekst))
            {
                throw new PlaceFinderException(Properties.Resources.noPlaceSelected);
            }

            //get the last search address
            var geoSearchAddress = currentSearch.FirstOrDefault(x => x.Equals(selectedAddress));
            if (geoSearchAddress == null)
            {
                throw new PlaceFinderException(Properties.Resources.placeNotRelocatedInList);
            }
            //create a polygon of the address
            var geometry = CreatePolyFromAddress(geoSearchAddress);
            var envelope = geometry.Envelope;

            // This check is performed to avoid nullpointer exception when testing with Rhino mocks
            // (Envelope for the geometry is not available at this point)
            if (envelope != null && envelope.LowerLeft != null)
            {
                Debug.WriteLine("Envelope for zoom");
                Debug.Indent();
                Debug.WriteLine("Geometry LowerLeft: " + envelope.LowerLeft.X + ", " + envelope.LowerLeft.Y);
                Debug.WriteLine("Geometry Extent: " + envelope.Width + ", " + envelope.Height);
            }

            var activeView = ((IActiveView)_factory.MxDocument.FocusMap);
            //verify that the map has e spatial reference
            if (activeView.FocusMap.SpatialReference == null || activeView.FocusMap.SpatialReference.FactoryCode == 0)
                throw new PlaceFinderException(Properties.Resources.noSpatialReference);

            //get the smallest allowed zoom ratio and calculate it to degrees
            var smallestAllowedZoomToInMetersOnX = Settings.Default.smallestAllowedZoomToInMetersOnX;
            var smallestAllowedZoomToInMetersOnY = Settings.Default.smallestAllowedZoomToInMetersOnY;
            if (Math.Abs(envelope.XMax - envelope.XMin) < smallestAllowedZoomToInMetersOnX || Math.Abs(envelope.YMax - envelope.YMin) < smallestAllowedZoomToInMetersOnY)
            {
                //get the center of the current envelope
                var centroidEx = ((IGeometry5)envelope).CentroidEx;
                //resize the envelope to the minimum size
                envelope.XMax = centroidEx.X + smallestAllowedZoomToInMetersOnX / 2;
                envelope.YMax = centroidEx.Y + smallestAllowedZoomToInMetersOnY / 2;
                envelope.XMin = centroidEx.X - smallestAllowedZoomToInMetersOnX / 2;
                envelope.YMin = centroidEx.Y - smallestAllowedZoomToInMetersOnY / 2;
            }

            // This check is performed to avoid nullpointer exception when testing with Rhino mocks
            // (Envelope for the geometry is not available at this point)
            if (envelope != null && envelope.LowerLeft != null)
            {
                Debug.WriteLine("Adapted LowerLeft: " + envelope.LowerLeft.X + ", " + envelope.LowerLeft.Y);
                Debug.WriteLine("Adapted Extent: " + envelope.Width + ", " + envelope.Height);
                Debug.Unindent();
            }

            //project the envelope to the spatial reference of the map
            envelope.Project(activeView.FocusMap.SpatialReference);
            //add the envelpe as extent on the map
            activeView.Extent = envelope;
            //refres the map to zoom
            activeView.Refresh();
        }

        public void SearchResourcesChange(IEnumerable checkedItemCollection)
        {
            searchRequestResources = new SearchRequestResources();
            foreach (var checkListItem in checkedItemCollection)
            {
                var s = checkListItem.ToString();

                /// Check each setting individually against the configured text lines
                if (!searchRequestResources.Addresses)
                { searchRequestResources.Addresses = s.Equals("Adresse"); }
                if (!searchRequestResources.Street)
                { searchRequestResources.Street = s.Equals("Navngiven vej"); }
                if (!searchRequestResources.HouseNumber)
                { searchRequestResources.HouseNumber = s.Equals("Husnummer"); }
                if (!searchRequestResources.Municipalities)
                {searchRequestResources.Municipalities = s.Equals("Kommune");}
                if (!searchRequestResources.Cadastre)
                { searchRequestResources.Cadastre = s.Equals("Matrikel"); }
                if (!searchRequestResources.CadastreDeprecated)
                { searchRequestResources.CadastreDeprecated = s.Equals("Matrikel udgået"); }
                if (!searchRequestResources.PlaceNames)
                { searchRequestResources.PlaceNames = s.Equals("Stednavn"); }
                if (!searchRequestResources.ElectoralDistrict)
                { searchRequestResources.ElectoralDistrict = s.Equals("Opstillingskreds"); }
                if (!searchRequestResources.PoliceDistrict)
                { searchRequestResources.PoliceDistrict = s.Equals("Politikreds"); }
                if (!searchRequestResources.Parish)
                { searchRequestResources.Parish = s.Equals("Sogn"); }
                if (!searchRequestResources.PostDistricts)
                { searchRequestResources.PostDistricts = s.Equals("Postnummer"); }
                if (!searchRequestResources.Regions)
                { searchRequestResources.Regions = s.Equals("Region"); }
                if (!searchRequestResources.JurisdictionsDistrict)
                { searchRequestResources.JurisdictionsDistrict = s.Equals("Retskreds"); }
            }
        }

        private IGeometry CreatePolyFromAddress(GeoSearchAddress geoAddress)
        {
            if (geoAddress == null)
                return null;

            //convert the address WKT geometry to esri geometry
            var convertWktToGeometry = _factory.ConvertWKTToGeometry(geoAddress.GeometryWkt);

            //create the spatial reference of etrs89 (the default of the service)
            int epsgId = Interface.Properties.Settings.Default.ESPGCode;

            var spatialReferenceFactory = _factory.SpatialReferenceFactory;
            var spatialReference = spatialReferenceFactory.CreateSpatialReference(epsgId);
            convertWktToGeometry.SpatialReference = spatialReference;

            return convertWktToGeometry;
        }

    }
}
