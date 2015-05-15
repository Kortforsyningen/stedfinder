using ESRI.ArcGIS.Geometry;

namespace PlaceFinderTest.Builder.Esri
{
    public class SpatialReferenceBuilder : BaseBuilder<ISpatialReference>
    {
        public SpatialReferenceBuilder()
        {
            Mock.SetupGet(m => m.FactoryCode).Returns(4326);
        }
    }
}