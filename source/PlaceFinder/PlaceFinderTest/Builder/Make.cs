using ESRI.ArcGIS.ArcMapUI;
using Moq;
using PlaceFinder;

namespace PlaceFinderTest.Builder
{


    public class Make
    {
        public class Esri
        {
            public static MxDocumentBuilder MxDocument
            {
                get { return new MxDocumentBuilder(); }
            }
        }

        public static PlaceFinderDockableWindowBuilder PlaceFinderDockableWindow
        {
            get { return new PlaceFinderDockableWindowBuilder(); }
        }

        public static GeosearchServiceBuilder GeosearchService
        {
            get { return new GeosearchServiceBuilder(); }
        }
    }

    public class GeosearchServiceBuilder : BaseBuilder<IGeosearchService>
    {
    }


    public class PlaceFinderDockableWindowBuilder : BaseBuilder<IPlaceFinderDockableWindow>
    {
    }

    public class MxDocumentBuilder : BaseBuilder<IMxDocument>
    {
    }

    public class BaseBuilder<TBuild> where TBuild : class
    {
        public Mock<TBuild> Mock { get; set; }
        public TBuild Build { get { return Mock.Object; } }

        public BaseBuilder()
        {
            Mock = new Mock<TBuild>();
        }
    }
}
