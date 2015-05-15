using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geometry;

namespace PlaceFinder.Interface
{
    public interface IFactory
    {
        IMxDocument MxDocument { get; set; }
        IGeosearchService GeosearchService { get; set; }
        IPlaceFinderDockableWindow PlaceFinderDockableWindow { get; set; }
        ISpatialReferenceFactory3 SpatialReferenceFactory { get; set; }
        IGeometry ConvertWKTToGeometry(string geometryWkt);
    }
}