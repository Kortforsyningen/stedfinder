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
            Validator.Map(factory.MxDocument.FocusMap)
                .NewExtentIsSet()
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
        [Ignore("To be finish missing right size of envelope")]
        public void TestZoomTo_ToSmallFeatureOnX()
        {
            //Arrange
            var place = "SomePlace";
            var incommingEnvelope = Make.Esri.Envelope.XMax(0.1).Build;
            var geometry = Make.Esri.Geometry.WithEnvelope(incommingEnvelope).Build;
            var factory = Make.Factory.ConvertWKTToGeometryReturns(geometry).Build;
            var placeFinderController = new PlaceFinderController(factory);
            var expetedEnvelope = Make.Esri.Envelope.XMax(0.001).Build;

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
        [Ignore("To be finish missing right size of envelope")]
        public void TestZoomTo_ToSmallFeatureOnY()
        {
            //Arrange
            var place = "SomePlace";
            var incommingEnvelope = Make.Esri.Envelope.YMax(0.1).Build;
            var geometry = Make.Esri.Geometry.WithEnvelope(incommingEnvelope).Build;
            var factory = Make.Factory.ConvertWKTToGeometryReturns(geometry).Build;
            var placeFinderController = new PlaceFinderController(factory);
            var expetedEnvelope = Make.Esri.Envelope.YMax(0.001).Build;

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
