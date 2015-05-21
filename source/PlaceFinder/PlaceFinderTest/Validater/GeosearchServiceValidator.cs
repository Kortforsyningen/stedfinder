using System.Collections.Generic;
using Rhino.Mocks;
using NUnit.Framework;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Validater
{
    public class GeosearchServiceValidator : BaseValidator<IGeosearchService>
    {

        public GeosearchServiceValidator(IGeosearchService mock)
            : base(mock)
        {
        }

        public GeosearchServiceValidator RequestCallWithSearchResources(string resourcString)
        {
            Mock.AssertWasCalled(m => m.Request(Arg<SearchRequestParams>.Is.Anything), options => options.Repeat.AtLeastOnce());
            var argumentsForCallsMadeOn = (SearchRequestParams)Mock.GetArgumentsForCallsMadeOn(m => m.Request(null))[0][0];
            Assert.That(argumentsForCallsMadeOn.Resources, Is.EqualTo(resourcString), "Fail 'Request' call on 'GeosearchService' param 'SearchRequestParams.Resources'");

            return this;
        }
    }
}