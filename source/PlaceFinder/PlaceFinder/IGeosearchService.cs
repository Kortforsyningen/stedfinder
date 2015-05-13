using PlaceFinder.GeoSearch;

namespace PlaceFinder
{
    public interface IGeosearchService
    {
        GeoSearchAddressData Request(string inputParamSearch, string inputParamResources, string inputParamLimit, string inputParamLogin, string inputParamPassword, string inputParamCrs);
    }
}