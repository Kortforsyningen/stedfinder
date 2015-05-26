using ESRI.ArcGIS.Geometry;
using Rhino.Mocks;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder.Esri
{
    public class EnvelopeBuilder : BaseBuilder<IEnvelope>
    {
        private IPoint _centroid;

        public EnvelopeBuilder()
        {
            Build = MockRepository.GenerateMock<IEnvelope, IGeometry5>();
            Build.Stub(m => m.XMax).PropertyBehavior();
            Build.Stub(m => m.YMax).PropertyBehavior();
            Build.Stub(m => m.XMin).PropertyBehavior();
            Build.Stub(m => m.YMin).PropertyBehavior();

            XMax(2);
            YMax(2);
            XMin(1);
            YMin(1);

            WithCentroid(Make.Esri.Point.Build);
        }

        public EnvelopeBuilder XMax(double x)
        {
            Build.XMax = x;
            return this;
        }
        public EnvelopeBuilder YMax(double y)
        {
            Build.YMax = y;
            return this;
        }
        public EnvelopeBuilder XMin(double x)
        {
            Build.XMin = x;
            return this;
        }
        public EnvelopeBuilder YMin(double y)
        {
            Build.YMin = y;
            return this;
        }

        public EnvelopeBuilder WithCentroid(IPoint centroid)
        {
            _centroid = centroid;
            ((IGeometry5)Build).Stub(m => m.CentroidEx).Return(_centroid).WhenCalled(x => x.ReturnValue = _centroid);
            return this;
        }
    }
}