using System.Collections.Generic;

namespace GeodataStyrelsen.ArcMap.PlaceFinder.Interface
{
    public interface IPlaceFinderDockableWindow
    {
        void AddSearchResult(List<GeoSearchAddress> list);
    }
}