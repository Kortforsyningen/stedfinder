using ESRI.ArcGIS.Geometry;
using Rhino.Mocks;

namespace PlaceFinderTest.Builder.Esri
{
    public class SpatialReferenceFactoryBuilder : BaseBuilder<ISpatialReferenceFactory3>
    {
        public SpatialReferenceFactoryBuilder()
        {
            Build.Stub(m => m.CreateSpatialReference(Arg<int>.Is.Anything)).Return(Make.Esri.SpatialReference.Build);
        }
    }
}