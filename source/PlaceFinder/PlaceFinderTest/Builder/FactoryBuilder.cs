using Rhino.Mocks;
using PlaceFinder;
using PlaceFinder.Interface;

namespace PlaceFinderTest.Builder
{
    public class FactoryBuilder : BaseBuilder<IFactory>
    {
        public FactoryBuilder()
        {
            var geosearchService = Make.GeosearchService.WithResult.Build;
            Build.Stub(factory => factory.GeosearchService).Return(geosearchService);
            var mxDocument = Make.Esri.MxDocument.Build;
            Build.Stub(factory => factory.MxDocument).Return(mxDocument);
            var placeFinderDockableWindow = Make.PlaceFinderDockableWindow.Build;
            Build.Stub(factory => factory.PlaceFinderDockableWindow).Return(placeFinderDockableWindow);
            var spatialReferenceFactory3 = Make.Esri.SpatialReferenceFactory.Build;
            Build.Stub(factory => factory.SpatialReferenceFactory).Return(spatialReferenceFactory3);
            var geometry = Make.Esri.Geometry.Build;
            Build.Stub(factory => factory.ConvertWKTToGeometry(Arg<string>.Is.Anything)).Return(geometry);
        }
    }
}