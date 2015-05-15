using Moq;
using PlaceFinder;
using PlaceFinder.Interface;

namespace PlaceFinderTest.Builder
{
    public class FactoryBuilder : BaseBuilder<IFactory>
    {
        public FactoryBuilder()
        {
            Mock.SetupProperty(factory => factory.GeosearchService, Make.GeosearchService.WithResult.Build);
            Mock.SetupProperty(factory => factory.MxDocument, Make.Esri.MxDocument.Build);
            Mock.SetupProperty(factory => factory.PlaceFinderDockableWindow, Make.PlaceFinderDockableWindow.Build);
            Mock.SetupProperty(factory => factory.SpatialReferenceFactory, Make.Esri.SpatialReferenceFactory.Build);
            Mock.Setup(factory => factory.ConvertWKTToGeometry(It.IsAny<string>())).Returns(Make.Esri.Geometry.Build);
        }
    }
}