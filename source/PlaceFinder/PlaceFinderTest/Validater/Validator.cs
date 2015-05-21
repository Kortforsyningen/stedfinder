using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;

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
    }
}