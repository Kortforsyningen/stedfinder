using Rhino.Mocks;

namespace PlaceFinderTest.Builder
{
    public class BaseBuilder<TBuild> where TBuild : class
    {
        public TBuild Build { get; set; }

        public BaseBuilder()
        {
            Build = MockRepository.GenerateMock<TBuild>();
        }
    }
}