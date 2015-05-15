using Moq;

namespace PlaceFinderTest.Builder
{
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