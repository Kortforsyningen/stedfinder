using System.Collections;
using System.Collections.Generic;
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
        [Ignore("unfinished test - should not have been checked in.")]
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
