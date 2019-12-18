using ESRI.ArcGIS.Geometry;
using Rhino.Mocks;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder.Esri
{
    public class EnvelopeBuilder : BaseBuilder<IEnvelope>
    {
        private IPoint _centroid;
        private ISpatialReference _spatialReference;

        public EnvelopeBuilder()
        {
            Build = MockRepository.GenerateMock<IEnvelope, IGeometry5>();
            Build.Stub(m => m.XMax).PropertyBehavior();
            Build.Stub(m => m.YMax).PropertyBehavior();
            Build.Stub(m => m.XMin).PropertyBehavior();
            Build.Stub(m => m.YMin).PropertyBehavior();

            IPoint point = Make.Esri.Point.Build;

            // Make the envelope extent bigger than the minimum zoom sizes from the settings
            XMax(point.X + 130);
            YMax(point.Y + 80);
            XMin(point.X - 130);
            YMin(point.Y - 80);

            WithCentroid(point);
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

        public EnvelopeBuilder WithSpatialReference(ISpatialReference spatialReference)
        {
            _spatialReference = spatialReference;
            ((IGeometry5)Build).Stub(m => m.SpatialReference).Return(_spatialReference).WhenCalled(x => x.ReturnValue = _spatialReference);
            return this;
        }
    }
}