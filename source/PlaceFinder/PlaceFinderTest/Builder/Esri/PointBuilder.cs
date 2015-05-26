using ESRI.ArcGIS.Geometry;
using Rhino.Mocks;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder.Esri
{
    public class PointBuilder : BaseBuilder<IPoint>
    {
        private double _y;
        private double _x;

        public PointBuilder()
        {
            Coords(42, 42);
        }

        public PointBuilder Coords(double x, double y)
        {
            _x = x;
            _y = y;
            Build.Stub(m => m.Y).Return(_y).WhenCalled(i => i.ReturnValue = _y);
            Build.Stub(m => m.X).Return(_x).WhenCalled(i => i.ReturnValue = _x);
            return this;
        }
    }
}