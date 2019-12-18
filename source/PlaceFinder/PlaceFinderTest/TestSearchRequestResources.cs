using NUnit.Framework;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest
{
    [TestFixture]
    public class TestSearchRequestResources
    {
        [Test]
        public void TestGetResourceString_AddressAndPlaceName()
        {
            //Arrange
            var searchRequestResources = new SearchRequestResources();
            searchRequestResources.Addresses = true;
            searchRequestResources.PlaceNames = true;
            
            //Act
            string resourceString = searchRequestResources.GetResourceString;

            //Assert
            Assert.AreEqual("Adresser,Stednavne", resourceString);
        }

        [Test]
        public void TestGetResourceString_All()
        {
            //Arrange
            var searchRequestResources = new SearchRequestResources();
            searchRequestResources.Addresses = true;
            searchRequestResources.CadastralNumber = true;
            searchRequestResources.ElectoralDistrict = true;
            searchRequestResources.HouseNumber = true;
            searchRequestResources.JurisdictionsDistrict = true;
            searchRequestResources.Municipalities = true;
            searchRequestResources.PlaceNames = true;
            searchRequestResources.PlaceNames_v2 = true;
            searchRequestResources.PlaceNames_v3 = true;
            searchRequestResources.PoliceDistrict = true;
            searchRequestResources.PostDistricts = true;
            searchRequestResources.Regions = true;
            searchRequestResources.Sogne = true;
            searchRequestResources.Street = true;

            //Act
            string resourceString = searchRequestResources.GetResourceString;

            //Assert
            Assert.AreEqual("Adresser,Veje,Husnumre,Kommuner,Matrikelnumre,Stednavne,Stednavne_v2,Stednavne_v3,Opstillingskredse,Politikredse,Postdistrikter,Regioner,Retskredse,Sogne", resourceString);
        }
    }
}
