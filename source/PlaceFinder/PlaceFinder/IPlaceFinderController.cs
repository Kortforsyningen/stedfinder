namespace PlaceFinder
{
    public interface IPlaceFinderController
    {
        void SearchTextChange(string searchString);
        void ZoomTo(string selectedAddress);
    }
}