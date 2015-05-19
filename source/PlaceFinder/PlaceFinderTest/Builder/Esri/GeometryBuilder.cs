using ESRI.ArcGIS.Geometry;
using Rhino.Mocks;

namespace PlaceFinderTest.Builder.Esri
{
    public class GeometryBuilder : BaseBuilder<IGeometry>
    {
        public GeometryBuilder()
        {
            Build.Stub(m => m.SpatialReference).Return(null);
            Build.Stub(m => m.Envelope).Return(Make.Esri.Envelope.Build);
        }
    }
}