using ESRI.ArcGIS.Geometry;

namespace PlaceFinderTest.Builder.Esri
{
    public class GeometryBuilder : BaseBuilder<IGeometry>
    {
        public GeometryBuilder()
        {
            Mock.SetupProperty(m => m.SpatialReference);
            Mock.Setup(m => m.Envelope).Returns(Make.Esri.Envelope.Build);
        }
    }
}