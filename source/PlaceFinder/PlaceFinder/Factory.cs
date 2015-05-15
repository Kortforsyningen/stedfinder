using System;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geometry;
using NetTopologySuite.IO;
using PlaceFinder.Interface;

namespace PlaceFinder
{
    public class Factory : IFactory
    {
        private IMxDocument _mxDocument;
        private IGeosearchService _geosearchService;
        private ISpatialReferenceFactory3 _spatialReferenceFactory;

        public IMxDocument MxDocument
        {
            get { return _mxDocument ?? (_mxDocument = ArcMap.Document); }
            set { _mxDocument = value; }
        }

        public IGeosearchService GeosearchService
        {
            get { return _geosearchService ?? (_geosearchService = new GeosearchService()); ; }
            set { _geosearchService = value; }
        }

        public IPlaceFinderDockableWindow PlaceFinderDockableWindow { get; set; }

        public ISpatialReferenceFactory3 SpatialReferenceFactory
        {
            get { return _spatialReferenceFactory ?? (_spatialReferenceFactory = (ISpatialReferenceFactory3)Activator.CreateInstance(Type.GetTypeFromProgID("esriGeometry.SpatialReferenceEnvironment"))); }
            set { _spatialReferenceFactory = value; }
        }
        
        public IGeometry ConvertWKTToGeometry(string wkt)
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