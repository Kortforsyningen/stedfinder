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
        private List<GeoSearchAddress> lastSearch;
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
            var geoSearchAddresses = GetAddressData(searchString);
            lastSearch = currentSearch ?? geoSearchAddresses;
            currentSearch = geoSearchAddresses;
            var list = geoSearchAddresses.Select(geoSearchAddress => geoSearchAddress.presentationString).ToList();
            _factory.PlaceFinderDockableWindow.AddSearchResult(list);
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
            if (string.IsNullOrWhiteSpace(selectedAddress)) 
                throw new PlaceFinderException("Der er ikke udfyldt et sted");

            var geoSearchAddress = lastSearch.FirstOrDefault(x => x.presentationString.Equals(selectedAddress));
            if (geoSearchAddress == null)
            {
                throw new PlaceFinderException("Stedet blev ikke fundet");
            }
            var geometry = CreatePolyFromAddress(geoSearchAddress);
            var envelope = geometry.Envelope;
            var activeView = ((IActiveView)_factory.MxDocument.FocusMap);
            if (activeView.FocusMap.SpatialReference == null || activeView.FocusMap.SpatialReference.FactoryCode == 0)
                //TODO move resource to file
                throw new PlaceFinderException("Spatial reference of map is not set");

            var smallestAllowedExtentToZoomTo = Settings.Default.smallestAllowedExtentToZoomTo;
            if ((envelope.XMax - envelope.XMin < smallestAllowedExtentToZoomTo) || (envelope.YMax - envelope.YMin < smallestAllowedExtentToZoomTo))
            {
                var halfOfSmallestExtentToZoomTo = smallestAllowedExtentToZoomTo / 2;
                envelope.XMax = envelope.XMax + halfOfSmallestExtentToZoomTo;
                envelope.YMax = envelope.YMax + halfOfSmallestExtentToZoomTo;
                envelope.XMin = envelope.XMin - halfOfSmallestExtentToZoomTo;
                envelope.YMin = envelope.YMin - halfOfSmallestExtentToZoomTo;
            }

            envelope.Project(activeView.FocusMap.SpatialReference);
            activeView.Extent = envelope;

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
            var coordinateSystem = esriSRGeoCSType.esriSRGeoCS_WGS1984;
            var spatialReferenceFactory = _factory.SpatialReferenceFactory;
            var spatialReference = spatialReferenceFactory.CreateSpatialReference((int)coordinateSystem);

            var convertWktToGeometry = _factory.ConvertWKTToGeometry(geoAddress.geometryWkt);
            convertWktToGeometry.SpatialReference = spatialReference;
            return convertWktToGeometry;
        }

    }
}
