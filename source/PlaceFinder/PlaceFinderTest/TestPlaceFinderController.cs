using NUnit.Framework;
using PlaceFinder;
using PlaceFinder.Interface;
using PlaceFinderTest.Builder;
using PlaceFinderTest.Validater;

namespace PlaceFinderTest
{
    [TestFixture]
    public class TestPlaceFinderController
    {
        [Test]
        public void TestZoomTo()
        {
            //Arrange
            var place = "SomePlace";
            IFactory factory = Make.Factory.Build;
            var placeFinderController = new PlaceFinderController(factory);

            //Act
            placeFinderController.SearchTextChange(place);
            placeFinderController.ZoomTo(place);

            //Assert
        }

        [Test]
        public void TestSearchTextChange()
        {
            //Arrange
            var place = "SomePlace";
            IFactory factory = Make.Factory.Build;
            var placeFinderController = new PlaceFinderController(factory);

            //Act
            placeFinderController.SearchTextChange(place);

            //Assert
            Validator.PlaceFinderWindow(factory.PlaceFinderDockableWindow).SearchResultAdded.Validate();
        }

    }

}
