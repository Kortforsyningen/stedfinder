using ESRI.ArcGIS.ArcMapUI;
using Rhino.Mocks;

namespace PlaceFinderTest.Builder.Esri
{
    public class MxDocumentBuilder : BaseBuilder<IMxDocument>
    {
        public MxDocumentBuilder()
        {
            Build.Stub(m => m.FocusMap).Return(Make.Esri.Map.Build);
        }
    }
}