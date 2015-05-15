using ESRI.ArcGIS.Geometry;
using Moq;

namespace PlaceFinderTest.Builder.Esri
{
    public class SpatialReferenceFactoryBuilder : BaseBuilder<ISpatialReferenceFactory3>
    {
        public SpatialReferenceFactoryBuilder()
        {
            Mock.Setup(m => m.CreateSpatialReference(It.IsAny<int>())).Returns(Make.Esri.SpatialReference.Build);
        }
    }
}