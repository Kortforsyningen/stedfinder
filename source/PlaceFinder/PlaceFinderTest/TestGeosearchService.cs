using System;
using System.Text.RegularExpressions;
using GeodataStyrelsen.ArcMap.PlaceFinder;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;
using NUnit.Framework;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest
{
    [TestFixture]
    public class TestGeosearchService
    {
        [Test]
        public void TestRegX()
        {
            var pattern = new Regex("([\\\\#/])|(^[/&/./;])");
            Assert.That(pattern.Replace("\\", "_"), Is.EqualTo("_"));
            Assert.That(pattern.Replace("#", "_"), Is.EqualTo("_"));
            Assert.That(pattern.Replace("/", "_"), Is.EqualTo("_"));
            Assert.That(pattern.Replace("&", "_"), Is.EqualTo("_"));
            Assert.That(pattern.Replace(".", "_"), Is.EqualTo("_"));
            Assert.That(pattern.Replace(";", "_"), Is.EqualTo("_"));

            Assert.That(pattern.Replace("A\\", "_"), Is.EqualTo("A_"));
            Assert.That(pattern.Replace("A#", "_"), Is.EqualTo("A_"));
            Assert.That(pattern.Replace("A/", "_"), Is.EqualTo("A_"));
            Assert.That(pattern.Replace("A&", "_"), Is.EqualTo("A&"));
            Assert.That(pattern.Replace("A.", "_"), Is.EqualTo("A."));
            Assert.That(pattern.Replace("A;", "_"), Is.EqualTo("A;"));
        }

        [Test]
        public void TestEncoding()
        {
            //Arange
            var text = @"Københavns Universitet (Universitet/Faghøjskole - København)";
            //Act
            var escapeString = Uri.EscapeDataString(text);
            //Asset
            Assert.That(escapeString, Is.EqualTo("K%C3%B8benhavns%20Universitet%20%28Universitet%2FFagh%C3%B8jskole%20-%20K%C3%B8benhavn%29"));
        }

        [Test]
        [Ignore("Integration test")]
        public void TestSpecialCharactersArrayWithAndAInfront()
        {
            //Arange
            var searchRequestParam = new SearchRequestParams();
            //for (int i = 32; i <= 165; i++)
            for (int i = 1; i <= 255; i++)
            {
                var text = "A" + (char)i;
                searchRequestParam.SearchText = text;
                var geosearchService = new GeosearchService();
                try
                {
                    //Act;
                    var geoSearchAddressData = geosearchService.Request(searchRequestParam);
                    //Asset
                    var message = geoSearchAddressData.message;
                    if (message != "OK")
                    {
                        Console.WriteLine(string.Format("\"{0}\"={1}", (char)i, message));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("\"{0}\"={1}", (char)i, e.Message));
                }
            }
        }

        [Test]
        [Ignore("Integration test")]
        public void TestSpecialCharactersArrayJustOne()
        {
            //Arange
            var searchRequestParam = new SearchRequestParams();
            //for (int i = 32; i <= 165; i++)
            for (int i = 1; i <= 255; i++)
            {
                var text = ((char)i).ToString();
                searchRequestParam.SearchText = text;
                var geosearchService = new GeosearchService();
                try
                {
                    //Act;
                    var geoSearchAddressData = geosearchService.Request(searchRequestParam);
                    //Asset
                    var message = geoSearchAddressData.message;
                    if (message != "OK")
                    {
                        Console.WriteLine(string.Format("\"{0}\"={1}", (char)i, message));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("\"{0}\"={1}", (char)i, e.Message));
                }
            }
        }
    }
}
