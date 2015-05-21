using ESRI.ArcGIS.Geometry;
using Rhino.Mocks;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder.Esri
{
    public class SpatialReferenceFactoryBuilder : BaseBuilder<ISpatialReferenceFactory3>
    {
        public SpatialReferenceFactoryBuilder()
        {
            Build.Stub(m => m.CreateSpatialReference(Arg<int>.Is.Anything)).Return(Make.Esri.SpatialReference.Build);
        }
    }
}