using System.Collections.Generic;
using Moq;
using PlaceFinder;

namespace PlaceFinderTest.Builder
{
    public class Validator
    {
        public static PlaceFinderDockableWindowValidator PlaceFinderWindow(IPlaceFinderDockableWindow placeFinderWindow)
        {
            return new PlaceFinderDockableWindowValidator(placeFinderWindow);
        }
    }

    public class BaseValidator<TBuild> where TBuild : class
    {
        protected BaseValidator(TBuild instance)
        {
            Mock = Moq.Mock.Get(instance);
        }

        public Mock<TBuild> Mock { get; set; }
        public void Validate()
        {
        }

    }

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