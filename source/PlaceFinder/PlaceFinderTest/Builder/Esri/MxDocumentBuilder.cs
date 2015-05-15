using ESRI.ArcGIS.ArcMapUI;

namespace PlaceFinderTest.Builder.Esri
{
    public class MxDocumentBuilder : BaseBuilder<IMxDocument>
    {
        public MxDocumentBuilder()
        {
            Mock.Setup(m => m.FocusMap).Returns(Make.Esri.Map.Build);
        }
    }
}