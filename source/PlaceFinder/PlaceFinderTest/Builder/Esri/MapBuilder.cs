using ESRI.ArcGIS.Carto;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder.Esri
{
    public class MapBuilder : BaseBuilder<IMap>
    {
        public MapBuilder()
        {
            Build = MockRepository.GenerateMock<IMap, IActiveView>();
            Build.Stub(m => m.SpatialReference).Return(Make.Esri.SpatialReference.Build);

            ((IActiveView)Build).Stub(m => m.FocusMap).Return(Build);
            ((IActiveView)Build).Stub(m => m.Extent).PropertyBehavior();

        }
    }
}