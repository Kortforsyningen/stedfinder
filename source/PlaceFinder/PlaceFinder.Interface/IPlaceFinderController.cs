using System.Collections;

namespace GeodataStyrelsen.ArcMap.PlaceFinder.Interface
{
    public interface IPlaceFinderController
    {
        void SearchTextChange(string searchString);
        void ZoomTo(GeoSearchAddress selectedAddress);
        void SearchResourcesChange(IEnumerable checkedItemCollection);
    }
}