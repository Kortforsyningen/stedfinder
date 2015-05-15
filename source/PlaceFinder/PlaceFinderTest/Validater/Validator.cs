using PlaceFinder;
using PlaceFinder.Interface;
using PlaceFinderTest.Builder;

namespace PlaceFinderTest.Validater
{
    public class Validator
    {
        public static PlaceFinderDockableWindowValidator PlaceFinderWindow(IPlaceFinderDockableWindow placeFinderWindow)
        {
            return new PlaceFinderDockableWindowValidator(placeFinderWindow);
        }
    }
}