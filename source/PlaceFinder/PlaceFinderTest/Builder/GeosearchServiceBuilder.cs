using System.Collections.Generic;
using Rhino.Mocks;
using GeodataStyrelsen.ArcMap.PlaceFinder;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder
{
    public class GeosearchServiceBuilder : BaseBuilder<IGeosearchService>
    {
        public GeosearchServiceBuilder WithResult
        {
            get
            {
                var geoSearchAddressData = new GeoSearchAddressData();
                geoSearchAddressData.data = new List<GeoSearchAddress> { new GeoSearchAddress { presentationString = "SomePlace" }, new GeoSearchAddress { presentationString = "Anotherlace" } };
                Build.Stub(m => m.Request(Arg<SearchRequestParams>.Is.Anything)).Return(geoSearchAddressData);
                return this;
            }
        }
    }
}