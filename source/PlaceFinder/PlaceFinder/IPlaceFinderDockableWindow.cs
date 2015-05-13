using System.Collections.Generic;

namespace PlaceFinder
{
    public interface IPlaceFinderDockableWindow
    {
        void AddSearchResult(List<string> list);
    }
}