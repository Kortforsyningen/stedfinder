using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class PlaceFindeController
    {
        private readonly PlaceFinderDockableWindow _placeFinderDockableWindow;
        private readonly IMxDocument _document;
        private List<GeoSearchAddress> currentSearch;
        private List<GeoSearchAddress> lastSearch;

        public PlaceFindeController(PlaceFinderDockableWindow placeFinderDockableWindow, IMxDocument document)
        {
            _placeFinderDockableWindow = placeFinderDockableWindow;
            _document = document;
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
                //var inputParamResources = "Adresser,Veje,Husnumre,Kommuner,Matrikelnumre,Stednavne,Opstillingskredse,Politikredse,Postdistrikter,Regioner,Retskredse";
                var inputParamResources = "Adresser,Stednavne";
                var inputParamLimit = "20";
                var inputParamLogin = "PlaceFinder";
                var inputParamPassword = "PlaceFinder!1";
                var inputParamCRS = "epsg:4326";

                var url =
                    String.Format(
                        "https://kortforsyningen.kms.dk/Geosearch?type=json&search={0}&resources={1}&limit={2}&login={3}&password={4}&crs={5}",
                        inputParamSearch, inputParamResources, inputParamLimit, inputParamLogin, inputParamPassword,
                        inputParamCRS);

                var webClient = new WebClient { Encoding = Encoding.UTF8 };
                var jsonContent = webClient.DownloadString(url);
                var serializer = new DataContractJsonSerializer(typeof(GeoSearchAddressData));

                GeoSearchAddressData response;
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonContent)))
                {
                    response = (GeoSearchAddressData)serializer.ReadObject(ms);
                }
                if (response != null && response.data != null)
                {
                    return response.data;
                }
            }

            return retValue;
        }

        public void ZoomTo(string selectedAddress)
        {
            var geoSearchAddress = lastSearch.FirstOrDefault(x => x.presentationString.Equals(selectedAddress));
            if (geoSearchAddress == null) return;
            var geometry = CreatePolyFromAddress(geoSearchAddress);
            var extent = geometry.Envelope;
            var activeView = ((IActiveView)_document.FocusMap);
            Debug.Print("sp befor:" + extent.SpatialReference.FactoryCode);
            extent.Project(activeView.FocusMap.SpatialReference);
            Debug.Print("sp on map:" + activeView.FocusMap.SpatialReference.FactoryCode);
            Debug.Print("sp after:" + extent.SpatialReference.FactoryCode);
            Debug.Print("xmax:" + extent.XMax);
            Debug.Print("xMin:" + extent.XMin);
            Debug.Print("ymax:" + extent.YMax);
            Debug.Print("yMin:" + extent.YMin);
            activeView.Extent = extent;
            activeView.Refresh();
        }
        public IGeometry CreatePolyFromAddress(GeoSearchAddress geoAddress)
        {
            if (geoAddress == null)
                return null;
            var coordinateSystem = esriSRGeoCSType.esriSRGeoCS_WGS1984;
            var spatialReferenceFactory = (ISpatialReferenceFactory3) Activator.CreateInstance(Type.GetTypeFromProgID("esriGeometry.SpatialReferenceEnvironment"));
            var spatialReference = spatialReferenceFactory.CreateSpatialReference((int)coordinateSystem);

            IGeometry convertWktToGeometry = ConvertWKTToGeometry(geoAddress.geometryWkt);
            convertWktToGeometry.SpatialReference = spatialReference;
            return convertWktToGeometry;
        }

        public static IGeometry ConvertWKTToGeometry(string wkt)
        {
            byte[] wkb = ConvertWKTToWKB(wkt);
            return ConvertWKBToGeometry(wkb);
        }

        public static byte[] ConvertWKTToWKB(string wkt)
        {
            var writer = new WKBWriter();
            var reader = new WKTReader();
            return writer.Write(reader.Read(wkt));
        }

        public static IGeometry ConvertWKBToGeometry(byte[] wkb)
        {
            IGeometry geom;
            int countin = wkb.GetLength(0);
            var factory = (IGeometryFactory3)new GeometryEnvironment();
            factory.CreateGeometryFromWkbVariant(wkb, out geom, out countin);
            return geom;
        }
    }
}
