using System.Collections.Generic;
using Rhino.Mocks;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder
{
    public class GeosearchServiceBuilder : BaseBuilder<IGeosearchService>
    {
        public GeosearchServiceBuilder WithResult(GeoSearchAddress resultAddress)
        {
                var geoSearchAddressData = new GeoSearchAddressData();
                geoSearchAddressData.data = new List<GeoSearchAddress> { resultAddress, new GeoSearchAddress { presentationString = "AnotherPlace" } };
                Build.Stub(m => m.Request(Arg<SearchRequestParams>.Is.Anything)).Return(geoSearchAddressData);
                return this;
        }
    }
}