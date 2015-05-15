using System.Collections.Generic;
using Moq;
using PlaceFinder;
using PlaceFinder.Interface;

namespace PlaceFinderTest.Builder
{
    public class GeosearchServiceBuilder : BaseBuilder<IGeosearchService>
    {
        public GeosearchServiceBuilder WithResult
        {
            get
            {
                var geoSearchAddressData = new GeoSearchAddressData();
                geoSearchAddressData.data = new List<GeoSearchAddress> { new GeoSearchAddress { presentationString = "SomePlace" }, new GeoSearchAddress { presentationString = "Anotherlace" } };
                Mock.Setup(m => m.Request(It.IsAny<string>(),
                                          It.IsAny<string>(),
                                          It.IsAny<string>(),
                                          It.IsAny<string>(),
                                          It.IsAny<string>(),
                                          It.IsAny<string>())).Returns(geoSearchAddressData);
                return this;
            }
        }
    }
}