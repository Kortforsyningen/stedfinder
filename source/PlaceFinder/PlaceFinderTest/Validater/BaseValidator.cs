using Moq;

namespace PlaceFinderTest.Validater
{
    public class BaseValidator<TBuild> where TBuild : class
    {
        protected BaseValidator(TBuild instance)
        {
            Mock = Moq.Mock.Get(instance);
        }

        public Mock<TBuild> Mock { get; set; }
        public void Validate()
        {
        }

    }
}