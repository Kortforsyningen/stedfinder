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
        public void TestSpecialCaratersSingel()
        {
            //Arange
            var searchRequestParam = new SearchRequestParams();
            var text = "D";
            Console.Write(text);
            searchRequestParam.SearchText = text;
            var geosearchService = new GeosearchService();
            //Act;
            var geoSearchAddressData = geosearchService.Request(searchRequestParam);
            //Asset
            //Assert.That(geoSearchAddressData.message, Is.EqualTo("OK"), "Fail on text: " + text);
            //Assert.That(geoSearchAddressData.status, Is.EqualTo("OK"), "Fail on text: " + text);
            Assert.That(geoSearchAddressData.data.Count, Is.EqualTo(0), "Fail on text: " + text);
        }

        [Test]
        [Ignore("Integration test")]
        public void TestSpecialCaratersArrayWithAndAInfront()
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
        public void TestSpecialCaratersArrayJustOne()
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
