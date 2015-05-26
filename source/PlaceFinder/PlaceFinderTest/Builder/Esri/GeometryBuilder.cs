using ESRI.ArcGIS.Geometry;
using Rhino.Mocks;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder.Esri
{
    public class GeometryBuilder : BaseBuilder<IGeometry>
    {
        private IEnvelope _envelope;
        private IPoint _centroid;

        public GeometryBuilder()
        {
            Build = MockRepository.GenerateMock<IGeometry, IGeometry5>();
            Build.Stub(m => m.SpatialReference).Return(null);
            WithEnvelope(Make.Esri.Envelope.Build);
        }

        public GeometryBuilder WithEnvelope(IEnvelope envelope)
        {
            _envelope = envelope;
            Build.Stub(m => m.Envelope).Return(_envelope).WhenCalled(x => x.ReturnValue = _envelope);
            return this;
        }

        public GeometryBuilder WithCentroid(IPoint centroid)
        {
            _centroid = centroid;
            ((IGeometry5)Build).Stub(m => m.CentroidEx).Return(_centroid).WhenCalled(x => x.ReturnValue = _centroid);
            return this;
        }
    }
}