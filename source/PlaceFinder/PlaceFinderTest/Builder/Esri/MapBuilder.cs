using ESRI.ArcGIS.Carto;
using Rhino.Mocks;

namespace PlaceFinderTest.Builder.Esri
{
    public class MapBuilder : BaseBuilder<IMap>
    {
        public MapBuilder()
        {
            Build.Stub(m => m.SpatialReference).Return(Make.Esri.SpatialReference.Build);

            var tempMock = MockRepository.GenerateMock<IActiveView>();
            tempMock.Stub(m => m.FocusMap).Return(Build);

        }
    }
}