using ESRI.ArcGIS.Geometry;
using Rhino.Mocks;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder.Esri
{
    public class EnvelopeBuilder : BaseBuilder<IEnvelope>
    {
        public EnvelopeBuilder()
        {
            Build.Stub(m => m.XMax).PropertyBehavior();
            Build.Stub(m => m.YMax).PropertyBehavior();
            Build.Stub(m => m.XMin).PropertyBehavior();
            Build.Stub(m => m.YMin).PropertyBehavior();

            XMax(2);
            YMax(2);
            XMin(1);
            YMin(1);
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
    }
}