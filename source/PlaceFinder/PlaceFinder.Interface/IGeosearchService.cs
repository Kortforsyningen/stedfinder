namespace PlaceFinder.Interface
{
    public interface IGeosearchService
    {
        GeoSearchAddressData Request(SearchRequestParams searchRequestParams);
    }
}