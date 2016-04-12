using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;
using GeodataStyrelsen.ArcMap.PlaceFinder.Properties;

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
            searchRequestResources = new SearchRequestResources();
            searchRequestResources.Addresses = true;
            searchRequestResources.PlaceNames = true;
        }

        public void SearchTextChange(string searchString)
        {
            if (searchString.Length > 2)
            {
                var geoSearchAddresses = GetAddressData(searchString);
                currentSearch = geoSearchAddresses;
                var list = geoSearchAddresses.Select(geoSearchAddress => geoSearchAddress.presentationString).ToList();
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


        public void ZoomTo(string selectedAddress)
        {
            if (string.IsNullOrEmpty(selectedAddress))
            {
                //TODO move message to a resource file
                throw new PlaceFinderException("Der er ikke udfyldt et sted");
            }

            //get the last search address
            var geoSearchAddress = currentSearch.FirstOrDefault(x => x.presentationString.Equals(selectedAddress));
            if (geoSearchAddress == null)
            {
                //TODO move message to a resource file
                throw new PlaceFinderException("Stedet blev ikke fundet");
            }
            //create a polygon of the address
            var geometry = CreatePolyFromAddress(geoSearchAddress);
            var envelope = geometry.Envelope;

            var activeView = ((IActiveView)_factory.MxDocument.FocusMap);
            //verify that the map has e spatial reference
            if (activeView.FocusMap.SpatialReference == null || activeView.FocusMap.SpatialReference.FactoryCode == 0)
                //TODO move message to a resource file
                throw new PlaceFinderException("Spatial reference of map is not set");

            //get the smallest allowed zoom ratio and calculate it to degrees
            var smallestAllowedZoomToInMetersOnX = Settings.Default.smallestAllowedZoomToInMetersOnX / (1000 * 60);
            var smallestAllowedZoomToInMetersOnY = Settings.Default.smallestAllowedZoomToInMetersOnY / (1000 * 60 * 2);
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
                if (!searchRequestResources.Addresses)
                { searchRequestResources.Addresses = s.Equals("Adresser"); }
                if (!searchRequestResources.Street)
                { searchRequestResources.Street = s.Equals("Veje"); }
                if (!searchRequestResources.HouseNumber)
                { searchRequestResources.HouseNumber = s.Equals("Husnumre"); }
                if (!searchRequestResources.Municipalities)
                {searchRequestResources.Municipalities = s.Equals("Kommuner");}
                if (!searchRequestResources.CadastralNumber)
                { searchRequestResources.CadastralNumber = s.Equals("Matrikelnumre"); }
                if (!searchRequestResources.PlaceNames)
                { searchRequestResources.PlaceNames = s.Equals("Stednavne"); }
                if (!searchRequestResources.ElectoralDistrict)
                { searchRequestResources.ElectoralDistrict = s.Equals("Opstillingskredse"); }
                if (!searchRequestResources.PoliceDistrict)
                { searchRequestResources.PoliceDistrict = s.Equals("Politikredse"); }
                if (!searchRequestResources.PostDistricts)
                { searchRequestResources.PostDistricts = s.Equals("Postdistrikter"); }
                if (!searchRequestResources.Regions)
                { searchRequestResources.Regions = s.Equals("Regioner"); }
                if (!searchRequestResources.JurisdictionsDistrict)
                { searchRequestResources.JurisdictionsDistrict = s.Equals("Retskredse"); }
            }
        }

        private IGeometry CreatePolyFromAddress(GeoSearchAddress geoAddress)
        {
            if (geoAddress == null)
                return null;

            //convert the address WKT geometry to esri geometry
            var convertWktToGeometry = _factory.ConvertWKTToGeometry(geoAddress.geometryWkt);

            //create the spatial reference of wgs1984 reflection that of the request services
            var coordinateSystem = esriSRGeoCSType.esriSRGeoCS_WGS1984;
            var spatialReferenceFactory = _factory.SpatialReferenceFactory;
            var spatialReference = spatialReferenceFactory.CreateSpatialReference((int)coordinateSystem);
            convertWktToGeometry.SpatialReference = spatialReference;

            return convertWktToGeometry;
        }

    }
}
