using Rhino.Mocks;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Validater
{
    public class BaseValidator<TBuild> where TBuild : class
    {
        protected BaseValidator(TBuild instance)
        {
            Mock = instance;
        }

        public TBuild Mock { get; set; }
        public void Validate()
        {
        }

    }
}