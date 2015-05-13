using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using NetTopologySuite.IO;
using PlaceFinder.GeoSearch;

namespace PlaceFinder
{
    public class PlaceFinderController : IPlaceFinderController
    {
        private readonly IPlaceFinderDockableWindow _placeFinderDockableWindow;
        private readonly IMxDocument _document;
        private readonly IGeosearchService _geosearchService;
        private List<GeoSearchAddress> currentSearch;
        private List<GeoSearchAddress> lastSearch;

        public PlaceFinderController(IPlaceFinderDockableWindow placeFinderDockableWindow, IMxDocument document, IGeosearchService geosearchService)
        {
            _placeFinderDockableWindow = placeFinderDockableWindow;
            _document = document;
            _geosearchService = geosearchService;
        }

        public void SearchTextChange(string searchString)
        {
            var geoSearchAddresses = GetAddressData(searchString);
            lastSearch = currentSearch;
            currentSearch = geoSearchAddresses;
            var list = geoSearchAddresses.Select(geoSearchAddress => geoSearchAddress.presentationString).ToList();
            _placeFinderDockableWindow.AddSearchResult(list);
        }

        private List<GeoSearchAddress> GetAddressData(string inputParamSearch)
        {
            var retValue = new List<GeoSearchAddress>();

            if (!String.IsNullOrEmpty(inputParamSearch))
            {
                //TODO create configuration for search parameter
                //var inputParamResources = "Adresser,Veje,Husnumre,Kommuner,Matrikelnumre,Stednavne,Opstillingskredse,Politikredse,Postdistrikter,Regioner,Retskredse";
                var inputParamResources = "Adresser,Stednavne";
                var inputParamLimit = "20";
                var inputParamLogin = "PlaceFinder";
                var inputParamPassword = "PlaceFinder!1";
                var inputParamCRS = "epsg:4326";

                var response = _geosearchService.Request(inputParamSearch, inputParamResources, inputParamLimit, inputParamLogin, inputParamPassword, inputParamCRS);

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
            var activeView = ((IActiveView)_document.FocusMap);
            if (activeView.FocusMap.SpatialReference == null || activeView.FocusMap.SpatialReference.FactoryCode == 0)
                //TODO move resource to file
                throw new PlaceFinderException("Spatial reference of map is not set");
            extent.Project(activeView.FocusMap.SpatialReference);
            activeView.Extent = extent;
            activeView.Refresh();
        }

        private IGeometry CreatePolyFromAddress(GeoSearchAddress geoAddress)
        {
            if (geoAddress == null)
                return null;
            var coordinateSystem = esriSRGeoCSType.esriSRGeoCS_WGS1984;
            var spatialReferenceFactory = (ISpatialReferenceFactory3) Activator.CreateInstance(Type.GetTypeFromProgID("esriGeometry.SpatialReferenceEnvironment"));
            var spatialReference = spatialReferenceFactory.CreateSpatialReference((int)coordinateSystem);

            var convertWktToGeometry = ConvertWKTToGeometry(geoAddress.geometryWkt);
            convertWktToGeometry.SpatialReference = spatialReference;
            return convertWktToGeometry;
        }

        private static IGeometry ConvertWKTToGeometry(string wkt)
        {
            byte[] wkb = ConvertWKTToWKB(wkt);
            return ConvertWKBToGeometry(wkb);
        }

        private static byte[] ConvertWKTToWKB(string wkt)
        {
            var writer = new WKBWriter();
            var reader = new WKTReader();
            return writer.Write(reader.Read(wkt));
        }

        private static IGeometry ConvertWKBToGeometry(byte[] wkb)
        {
            IGeometry geom;
            int countin = wkb.GetLength(0);
            var factory = (IGeometryFactory3)new GeometryEnvironment();
            factory.CreateGeometryFromWkbVariant(wkb, out geom, out countin);
            return geom;
        }
    }
}
