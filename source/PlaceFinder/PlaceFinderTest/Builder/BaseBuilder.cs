using Rhino.Mocks;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder
{
    public class BaseBuilder<TBuild> where TBuild : class
    {
        private TBuild _build;

        public TBuild Build
        {
            get
            {
                {
                    return _build;
                }
            }
            set { _build = value; }
        }

        public BaseBuilder()
        {
            Build = MockRepository.GenerateMock<TBuild>();
        }
    }
}