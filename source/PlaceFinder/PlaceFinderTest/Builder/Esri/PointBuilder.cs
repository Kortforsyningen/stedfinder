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
            // Somewhere in Gladsaxe in EPSG:25832 - ETRS89 UTM32N
            Coords(718826, 6183122);
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