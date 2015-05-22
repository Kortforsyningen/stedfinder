using ESRI.ArcGIS.Geometry;
using Rhino.Mocks;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder.Esri
{
    public class GeometryBuilder : BaseBuilder<IGeometry>
    {
        private IEnvelope _envelope;
        public GeometryBuilder()
        {
            Build.Stub(m => m.SpatialReference).Return(null);
            WithEnvelope(Make.Esri.Envelope.Build);
        }

        public GeometryBuilder WithEnvelope(IEnvelope envelope)
        {
            _envelope = envelope;
            Build.Stub(m => m.Envelope).Return(_envelope).WhenCalled(x => x.ReturnValue = _envelope);
            return this;
        }
    }
}