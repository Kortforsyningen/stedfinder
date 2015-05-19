using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using NetTopologySuite.IO;
using PlaceFinder.Interface;

namespace PlaceFinder
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
            var extent = geometry.Envelope;
            var activeView = ((IActiveView)_factory.MxDocument.FocusMap);
            if (activeView.FocusMap.SpatialReference == null || activeView.FocusMap.SpatialReference.FactoryCode == 0)
                //TODO move resource to file
                throw new PlaceFinderException("Spatial reference of map is not set");
            extent.Project(activeView.FocusMap.SpatialReference);
            activeView.Extent = extent;
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
