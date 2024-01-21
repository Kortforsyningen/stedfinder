using System.Collections.Generic;
using GeodataStyrelsen.ArcMap.PlaceFinder;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;
using GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder;
using GeodataStyrelsen.ArcMap.PlaceFinderTest.Validater;
using NUnit.Framework;
using Rhino.Mocks;
using ESRI.ArcGIS.Geometry;
using System;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest
{
    [TestFixture]
    public class TestPlaceFinderController
    {
        [Test]
        public void TestZoomTo()
        {
            //Arrange
            var place = new GeoSearchAddress() {Visningstekst = "SomePlace"};
            IFactory factory = Make.Factory(place).Build;
            var placeFinderController = new PlaceFinderController(factory);
            var expectedEnvelope = Make.Esri.Envelope.Build;

            //Act
            placeFinderController.SearchTextChange(place.Visningstekst);
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
            var place = new GeoSearchAddress() { Visningstekst = "SomePlace" };
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
            placeFinderController.SearchTextChange(place.Visningstekst);
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
            var place = new GeoSearchAddress() { Visningstekst = "SomePlace" };
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
            placeFinderController.SearchTextChange(place.Visningstekst);
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
            var place = new GeoSearchAddress() { Visningstekst = "SomePlace" };
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
            placeFinderController.SearchTextChange(place.Visningstekst);
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
            var place = new GeoSearchAddress() { Visningstekst = "SomePlace" };
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
                .XMax(centroid.X + 125)
                .XMin(centroid.X - 125)
                .YMax(centroid.Y + 75)
                .YMin(centroid.Y - 75)
                .Build;

            //Act
            placeFinderController.SearchTextChange(place.Visningstekst);
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
            IFactory factory = Make.Factory(new GeoSearchAddress() { Visningstekst = "SomePlace" }).Build;
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
            var factory = Make.Factory(new GeoSearchAddress() { Visningstekst = "SomePlace" }).Build;
            var placeFinderController = new PlaceFinderController(factory);

            var resourceList = new List<object> { "Husnummer", "Adresse", "Sogn" };
            //Act
            placeFinderController.SearchResourcesChange(resourceList);
            placeFinderController.SearchTextChange(place);

            //Assert
            Validator.PlaceFinderWindow(factory.PlaceFinderDockableWindow).Validate();
            Validator.GeosearchService(factory.GeosearchService).RequestCallWithSearchResources("adresse,husnummer,sogn").Validate();
        }

        [Test]
        [Sequential]
        public void TestLabels(
            [Values("Adresse","Husnummer","Kommune")]//,"Matrikel","Matrikel udgået","matrikel_udgaaet","Navngiven vej",
                //"Opstillingskreds","Politikreds","Postnummer","Region","Retskreds","Sogn","Stednavn")] 
        string label, 
            [Values("adresse", "husnummer", "kommune")]//, "matrikel", "matikel_udgaaet", "navngivenvej",
                //"opstillingskreds", "politikreds", "postnummer", "region", "retskreds", "sogn", "stednavn")] 
        string resource)
        {
            var place = "SomePlace";
            //Arrange
            var factory = Make.Factory(new GeoSearchAddress() { Visningstekst = "SomePlace" }).Build;
            var resourceList = new List<object> { label };
            var placeFinderController = new PlaceFinderController(factory);
            //Act
            placeFinderController.SearchResourcesChange(resourceList);
            placeFinderController.SearchTextChange(place);
            //Assert
            Validator.PlaceFinderWindow(factory.PlaceFinderDockableWindow).Validate();
            Validator.GeosearchService(factory.GeosearchService).RequestCallWithSearchResources(resource).Validate();
        }
    }
}
