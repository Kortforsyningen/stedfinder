using ESRI.ArcGIS.Geometry;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;
using Rhino.Mocks;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder
{
    public class FactoryBuilder : BaseBuilder<IFactory>
    {

        IGeometry _geometry = Make.Esri.Geometry.Build;

        public FactoryBuilder(GeoSearchAddress resultAddress)
        {
            var geosearchService = Make.GeosearchService.WithResult(resultAddress).Build;
            Build.Stub(factory => factory.GeosearchService).Return(geosearchService);
            var mxDocument = Make.Esri.MxDocument.Build;
            Build.Stub(factory => factory.MxDocument).Return(mxDocument);
            var placeFinderDockableWindow = Make.PlaceFinderDockableWindow.Build;
            Build.Stub(factory => factory.PlaceFinderDockableWindow).Return(placeFinderDockableWindow);
            var spatialReferenceFactory3 = Make.Esri.SpatialReferenceFactory.Build;
            Build.Stub(factory => factory.SpatialReferenceFactory).Return(spatialReferenceFactory3);
            var geometry = Make.Esri.Geometry.Build;
            ConvertWKTToGeometryReturns(geometry);
        }

        public FactoryBuilder ConvertWKTToGeometryReturns(IGeometry geometry)
        {
            _geometry = geometry;
            //Build.Stub(factory => factory.ConvertWKTToGeometry(Arg<string>.Is.Anything)).IgnoreArguments().Do(new Func<IGeometry>(() => geometry));
            Build.Stub(factory => factory.ConvertWKTToGeometry(Arg<string>.Is.Anything))
                .Return(_geometry)
                .WhenCalled(x => x.ReturnValue = _geometry);
            return this;
        }
    }
}