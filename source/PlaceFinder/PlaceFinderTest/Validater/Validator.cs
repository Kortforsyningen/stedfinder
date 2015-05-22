using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;
using GeodataStyrelsen.ArcMap.PlaceFinderTest.Validater.Esri;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Validater
{
    public class Validator
    {
        public static PlaceFinderDockableWindowValidator PlaceFinderWindow(IPlaceFinderDockableWindow placeFinderWindow)
        {
            return new PlaceFinderDockableWindowValidator(placeFinderWindow);
        }

        public static GeosearchServiceValidator GeosearchService(IGeosearchService geosearchService)
        {
            return new GeosearchServiceValidator(geosearchService);
        }

        public static MapValidator Map(ESRI.ArcGIS.Carto.IMap map)
        {
            return new MapValidator(map);
        }
    }
}