using ESRI.ArcGIS.Carto;

namespace PlaceFinderTest.Builder.Esri
{
    public class MapBuilder : BaseBuilder<IMap>
    {
        public MapBuilder()
        {
            Mock.SetupProperty(m => m.SpatialReference, Make.Esri.SpatialReference.Build);

            var tempMock = Mock.As<IActiveView>();
            tempMock.Setup(m => m.FocusMap).Returns(Build);

        }
    }
}