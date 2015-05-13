using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using Moq;
using NUnit.Framework;
using PlaceFinder;
using PlaceFinderTest.Builder;

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
            var doc = Make.Esri.MxDocument.Build;
            var placeFinderWindow = Make.PlaceFinderDockableWindow.Build;
            var geosearchService = Make.GeosearchService.Build;
            var placeFinderController = new PlaceFinderController(placeFinderWindow, doc, geosearchService);

            //Act
            placeFinderController.ZoomTo(place);

            //Assert
        }

        [Test]
        public void TestSearchTextChange()
        {
            //Arrange
            var doc = Make.Esri.MxDocument.Build;
            var placeFinderWindow = Make.PlaceFinderDockableWindow.Build;
            var geosearchService = Make.GeosearchService.Build;
            var placeFinderController = new PlaceFinderController(placeFinderWindow, doc, geosearchService);

            //Act
            var place = "SomePlace";
            placeFinderController.SearchTextChange(place);

            //Assert
            Validator.PlaceFinderWindow(placeFinderWindow).SearchResultAdded.Validate();
        }

    }

}
