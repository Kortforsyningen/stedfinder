using System.Collections.Generic;
using ESRI.ArcGIS.Geometry;
using GeodataStyrelsen.ArcMap.PlaceFinder;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;
using GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder;
using GeodataStyrelsen.ArcMap.PlaceFinderTest.Validater;
using NUnit.Framework;
using Rhino.Mocks;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest
{
    [TestFixture]
    public class TestPlaceFinderController
    {
        //[Test]
        //public void TestZoomTo()
        //{
        //    //Arrange
        //    var place = "SomePlace";
        //    IFactory factory = Make.Factory.Build;
        //    var placeFinderController = new PlaceFinderController(factory);
        //    var expetedEnvelope = Make.Esri.Envelope.Build;

        //    //Act
        //    placeFinderController.SearchTextChange(place);
        //    placeFinderController.ZoomTo(place);

        //    //Assert
        //    Validator.Map(factory.MxDocument.FocusMap)
        //        .NewExtentIsSet(expetedEnvelope)
        //        .MapIsRefresh
        //        .Validate();
        //}



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
            //(y) nord south 150 m is 150/(1000*60*2) = 0,00125
            //(x) east vest 250 m is 200/(1000*60) = 0,00416
            //Arrange
            var place = "SomePlace";
            var incommingEnvelope = Make.Esri.Envelope
                .XMax(42.0021-0.00416)
                .XMin(41.9979)
                .YMax(42)
                .YMin(41)
                .Build;
            var geometry = Make.Esri.Geometry.WithEnvelope(incommingEnvelope).Build;
            var factory = Make.Factory.ConvertWKTToGeometryReturns(geometry).Build;
            var placeFinderController = new PlaceFinderController(factory);
            var expetedEnvelope = Make.Esri.Envelope
                .XMax(42.0021)
                .XMin(41.9979)
                .YMax(42.0006)
                .YMin(41.9994)
                .Build;

            //Act
            placeFinderController.SearchTextChange(place);
            placeFinderController.ZoomTo(place);

            //Assert
            Validator.Map(factory.MxDocument.FocusMap)
                .NewExtentIsSet(expetedEnvelope)
                .MapIsRefresh
                .Validate();
        }


        [Test]
        public void TestZoomTo_ToSmallFeatureOnXNegativX()
        {
            //(y) nord south 150 m is 150/(1000*60*2) = 0,00125
            //(x) east vest 250 m is 200/(1000*60) = 0,00416
            //Arrange
            var place = "SomePlace";
            var centroid = Make.Esri.Point.Coords(-42.0, -42.0).Build;
            var incommingEnvelope = Make.Esri.Envelope
                .XMax(-41.9979 - 0.00416)
                .XMin(-42.0021)
                .YMax(-41)
                .YMin(-42)
                .WithCentroid(centroid)
                .Build;
            var geometry = Make.Esri.Geometry.WithEnvelope(incommingEnvelope).Build;
            var factory = Make.Factory.ConvertWKTToGeometryReturns(geometry).Build;
            var placeFinderController = new PlaceFinderController(factory);
            var expetedEnvelope = Make.Esri.Envelope
                .XMax(-41.9979)
                .XMin(-42.0021)
                .YMax(-41.9994)
                .YMin(-42.0006)
                .Build;

            //Act
            placeFinderController.SearchTextChange(place);
            placeFinderController.ZoomTo(place);

            //Assert
            Validator.Map(factory.MxDocument.FocusMap)
                .NewExtentIsSet(expetedEnvelope)
                .MapIsRefresh
                .Validate();
        }


        [Test]
        public void TestZoomTo_ToSmallFeatureOnY()
        {
            //(y) nord south 150 m is 150/(1000*60*2) = 0,00125
            //(x) east vest 250 m is 200/(1000*60) = 0,00416
            //Arrange
            var place = "SomePlace";
            var incommingEnvelope = Make.Esri.Envelope
                .XMax(42)
                .XMin(41)
                .YMax(42.00063-0.00125)
                .YMin(41.9993)
                .Build;
            var geometry = Make.Esri.Geometry.WithEnvelope(incommingEnvelope).Build;
            var factory = Make.Factory.ConvertWKTToGeometryReturns(geometry).Build;
            var placeFinderController = new PlaceFinderController(factory);
            var expetedEnvelope = Make.Esri.Envelope
                .XMax(42.0021)
                .XMin(41.9979)
                .YMax(42.0006)
                .YMin(41.9994)
                .Build;

            //Act
            placeFinderController.SearchTextChange(place);
            placeFinderController.ZoomTo(place);

            //Assert
            Validator.Map(factory.MxDocument.FocusMap)
                .NewExtentIsSet(expetedEnvelope)
                .MapIsRefresh
                .Validate();
        }
        
        [Test]
        public void TestZoomTo_ToSmallFeatureOnYWithNegativY()
        {
            //(y) nord south 150 m is 150/(1000*60*2) = 0,00125
            //(x) east vest 250 m is 200/(1000*60) = 0,00416
            //Arrange
            var place = "SomePlace";
            var centroid = Make.Esri.Point.Coords(-42.0, -42.0).Build;
            var incommingEnvelope = Make.Esri.Envelope
                .XMax(-41)
                .XMin(-42)
                .YMax(-41.9993 - 0.00125)
                .YMin(-42.00063)
                .WithCentroid(centroid)
                .Build;
            var geometry = Make.Esri.Geometry
                .WithEnvelope(incommingEnvelope)
                .Build;
            var factory = Make.Factory.ConvertWKTToGeometryReturns(geometry).Build;
            var placeFinderController = new PlaceFinderController(factory);
            var expetedEnvelope = Make.Esri.Envelope
                .XMax(-41.9979)
                .XMin(-42.0021)
                .YMax(-41.9994)
                .YMin(-42.0006)
                .Build;

            //Act
            placeFinderController.SearchTextChange(place);
            placeFinderController.ZoomTo(place);

            //Assert
            Validator.Map(factory.MxDocument.FocusMap)
                .NewExtentIsSet(expetedEnvelope)
                .MapIsRefresh
                .Validate();
        }

        //[Test]
        //public void TestSearchTextChange()
        //{
        //    //Arrange
        //    var place = "SomePlace";
        //    IFactory factory = Make.Factory.Build;
        //    var placeFinderController = new PlaceFinderController(factory);

        //    //Act
        //    placeFinderController.SearchTextChange(place);

        //    //Assert
        //    Validator.PlaceFinderWindow(factory.PlaceFinderDockableWindow).SearchResultAdded.Validate();
        //}

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
