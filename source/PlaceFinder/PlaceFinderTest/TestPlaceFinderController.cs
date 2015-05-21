using System.Collections;
using System.Collections.Generic;
using GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder;
using GeodataStyrelsen.ArcMap.PlaceFinderTest.Validater;
using NUnit.Framework;
using GeodataStyrelsen.ArcMap.PlaceFinder;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest
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

        [Test]
        public void TestResourceChange()
        {
            //Arrange
            var place = "SomePlace";
            var resourceList = new List<object> { "Husnumre", "Adresser" };
            var factory = Make.Factory.Build;
            var placeFinderController = new PlaceFinderController(factory);

            //Act
            placeFinderController.SearchResourcesChange(resourceList);
            placeFinderController.SearchTextChange(place);

            //Assert
            Validator.PlaceFinderWindow(factory.PlaceFinderDockableWindow).Validate();
            Validator.GeosearchService(factory.GeosearchService).RequestCallWithSearchResources("Adresser,Husnumre").Validate();
        }
    }
}
