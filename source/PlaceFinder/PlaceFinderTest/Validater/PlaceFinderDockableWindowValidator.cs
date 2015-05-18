using System.Collections.Generic;
using Moq;
using PlaceFinder.Interface;

namespace PlaceFinderTest.Validater
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
                Mock.Verify(m => m.AddSearchResult(It.Is<List<string>>(l => l.Count > 0)), Times.AtLeastOnce);
                return this;
            }
        }

    }
}