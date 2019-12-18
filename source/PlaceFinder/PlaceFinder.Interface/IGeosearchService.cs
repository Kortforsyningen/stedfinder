namespace GeodataStyrelsen.ArcMap.PlaceFinder.Interface
{
    public interface IGeosearchService
    {

        GeoSearchAddressData Request(SearchRequestParams searchRequestParams);
    }
}