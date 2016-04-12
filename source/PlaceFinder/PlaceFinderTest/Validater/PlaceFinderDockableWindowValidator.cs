using System.Collections.Generic;
using Rhino.Mocks;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Validater
{
    public class PlaceFinderDockableWindowValidator : BaseValidator<IPlaceFinderDockableWindow>
    {
        public PlaceFinderDockableWindowValidator(IPlaceFinderDockableWindow mock)
            : base(mock)
        {

        }

        public PlaceFinderDockableWindowValidator SearchResultAdded
        {
            get
            {
                Mock.AssertWasCalled(
                    m => m.AddSearchResult(Arg<List<GeoSearchAddress>>.Matches(l => l.Count > 0)), 
                    options => options.Repeat.AtLeastOnce());
                return this;
            }
        }
    }
}