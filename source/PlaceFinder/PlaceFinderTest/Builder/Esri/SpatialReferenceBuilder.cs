using ESRI.ArcGIS.Geometry;
using Rhino.Mocks;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder.Esri
{
    public class SpatialReferenceBuilder : BaseBuilder<ISpatialReference>
    {
        public SpatialReferenceBuilder()
        {
            Build.Stub(m => m.FactoryCode).Return(4326);
        }
    }
}