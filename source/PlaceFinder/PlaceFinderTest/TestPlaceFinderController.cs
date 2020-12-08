using System.Collections.Generic;
using GeodataStyrelsen.ArcMap.PlaceFinder;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;
using GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder;
using GeodataStyrelsen.ArcMap.PlaceFinderTest.Validater;
using NUnit.Framework;
using Rhino.Mocks;
using ESRI.ArcGIS.Geometry;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest
{
    [TestFixture]
    public class TestPlaceFinderController
    {
        [Test]
        public void TestZoomTo()
        {
            //Arrange
            var place = new GeoSearchAddress() {presentationString = "SomePlace"};
            IFactory factory = Make.Factory(place).Build;
            var placeFinderController = new PlaceFinderController(factory);
            var expectedEnvelope = Make.Esri.Envelope.Build;

            //Act
            placeFinderController.SearchTextChange(place.presentationString);
            placeFinderController.ZoomTo(place);

            //Assert
            Validator.Map(factory.MxDocument.FocusMap)
                .NewExtentIsSet(expectedEnvelope)
                .MapIsRefresh
                .Validate();
        }



        [Test]
        public void TestBuildStubOrdre()
        {
            var mock = MockRepository.GenerateMock<ITestBuildordre>();
            Assert.That(mock.Foobar(""), Is.Null, "call 0");

            mock.Stub(factory => factory.Foobar(Arg<string>.Is.Anything)).Return("set 1");
            Assert.That(mock.Foobar(""), Is.EqualTo("set 1"), "call 1");

            mock.BackToRecord(BackToRecordOptions.All);
            mock.Replay();
            mock.Stub(factory => factory.Foobar(Arg<string>.Is.Anything)).Return("set 2");
            Assert.That(mock.Foobar(""), Is.EqualTo("set 2"), "call 2");
           
        }

        public interface ITestBuildordre
        {
            string Foobar(string s);

        }


        [Test]
        public void TestZoomTo_ToSmallFeatureOnX()
        {
            //Arrange
            var place = new GeoSearchAddress() { presentationString = "SomePlace" };
            IPoint point = Make.Esri.Point.Build;
            var incommingEnvelope = Make.Esri.Envelope
                .XMax(point.X + 1)
                .XMin(point.X - 1)
                .YMax(point.Y + 150)
                .YMin(point.Y - 150)
                .Build;
            var geometry = Make.Esri.Geometry.WithEnvelope(incommingEnvelope).Build;
            var factory = Make.Factory(place).ConvertWKTToGeometryReturns(geometry).Build;
            var placeFinderController = new PlaceFinderController(factory);
            var expectedEnvelope = Make.Esri.Envelope
                .XMax(point.X + 125)
                .XMin(point.X - 125)
                .YMax(point.Y + 75)
                .YMin(point.Y - 75).Build;

            //Act
            placeFinderController.SearchTextChange(place.presentationString);
            placeFinderController.ZoomTo(place);

            //Assert
            Validator.Map(factory.MxDocument.FocusMap)
                .NewExtentIsSet(expectedEnvelope)
                .MapIsRefresh
                .Validate();
        }


        [Test]
        public void TestZoomTo_ToSmallFeatureOnXNegativeX()
        {
            //Arrange
            var place = new GeoSearchAddress() { presentationString = "SomePlace" };
            var centroid = Make.Esri.Point.Coords(-250.0, -150.0).Build;
            var incommingEnvelope = Make.Esri.Envelope
                .XMax(centroid.X + 1)
                .XMin(centroid.X - 1)
                .YMax(centroid.Y + 150)
                .YMin(centroid.Y - 150)
                .WithCentroid(centroid)
                .Build;
            var geometry = Make.Esri.Geometry.WithEnvelope(incommingEnvelope).Build;
            var factory = Make.Factory(place).ConvertWKTToGeometryReturns(geometry).Build;
            var placeFinderController = new PlaceFinderController(factory);
            var expectedEnvelope = Make.Esri.Envelope
                .XMax(centroid.X + 125)
                .XMin(centroid.X - 125)
                .YMax(centroid.Y + 75)
                .YMin(centroid.Y - 75)
                .Build;

            //Act
            placeFinderController.SearchTextChange(place.presentationString);
            placeFinderController.ZoomTo(place);

            //Assert
            Validator.Map(factory.MxDocument.FocusMap)
                .NewExtentIsSet(expectedEnvelope)
                .MapIsRefresh
                .Validate();
        }


        [Test]
        public void TestZoomTo_ToSmallFeatureOnY()
        {
            //Arrange
            var place = new GeoSearchAddress() { presentationString = "SomePlace" };
            IPoint point = Make.Esri.Point.Build;
            var incommingEnvelope = Make.Esri.Envelope
                .XMax(point.X + 150)
                .XMin(point.X - 150)
                .YMax(point.Y + 1)
                .YMin(point.Y - 1)
                .Build;
            var geometry = Make.Esri.Geometry.WithEnvelope(incommingEnvelope).Build;
            var factory = Make.Factory(place).ConvertWKTToGeometryReturns(geometry).Build;
            var placeFinderController = new PlaceFinderController(factory);
            var expetedEnvelope = Make.Esri.Envelope
                .XMax(point.X + 125)
                .XMin(point.X - 125)
                .YMax(point.Y + 75)
                .YMin(point.Y - 75).Build;

            //Act
            placeFinderController.SearchTextChange(place.presentationString);
            placeFinderController.ZoomTo(place);

            //Assert
            Validator.Map(factory.MxDocument.FocusMap)
                .NewExtentIsSet(expetedEnvelope)
                .MapIsRefresh
                .Validate();
        }
        
        [Test]
        public void TestZoomTo_ToSmallFeatureOnYWithNegativeY()
        {
            //Arrange
            var place = new GeoSearchAddress() { presentationString = "SomePlace" };
            var centroid = Make.Esri.Point.Coords(-250.0, -150.0).Build;
            var incommingEnvelope = Make.Esri.Envelope
                .XMax(centroid.X + 150)
                .XMin(centroid.X - 150)
                .YMax(centroid.Y + 1)
                .YMin(centroid.Y - 1)
                .WithCentroid(centroid)
                .Build;
            var geometry = Make.Esri.Geometry.WithEnvelope(incommingEnvelope).Build;
            var factory = Make.Factory(place).ConvertWKTToGeometryReturns(geometry).Build;
            var placeFinderController = new PlaceFinderController(factory);
            var expectedEnvelope = Make.Esri.Envelope
                .XMax(-41.9979)
                .XMin(-42.0021)
                .YMax(-41.9994)
                .YMin(-42.0006)
                .Build;

            //Act
            placeFinderController.SearchTextChange(place.presentationString);
            placeFinderController.ZoomTo(place);

            //Assert
            Validator.Map(factory.MxDocument.FocusMap)
                .NewExtentIsSet(expectedEnvelope)
                .MapIsRefresh
                .Validate();
        }

        [Test]
        public void TestSearchTextChange()
        {
            //Arrange
            var place = "SomePlace";
            IFactory factory = Make.Factory(new GeoSearchAddress() { presentationString = "SomePlace" }).Build;
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
            var factory = Make.Factory(new GeoSearchAddress() { presentationString = "SomePlace" }).Build;
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
