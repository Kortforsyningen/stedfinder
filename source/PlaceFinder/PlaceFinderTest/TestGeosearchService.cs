using System;
using System.Text.RegularExpressions;
using ESRI.ArcGIS.Geometry;
using GeodataStyrelsen.ArcMap.PlaceFinder;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;
using GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder;
using GeodataStyrelsen.ArcMap.PlaceFinderTest.Validater;
using NUnit.Framework;
using GeoJSON.Net;
using GeoJSON.Net.Geometry;

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

        /// <summary>
        /// Parametrized test to allow individual type tests
        /// </summary>
        /// <param name="type">The type to test</param>
        [Test]
        [Sequential]
        public void TestWKTEncoding(
            [Values(GeoJSONObjectType.Point, GeoJSONObjectType.MultiPoint,
            GeoJSONObjectType.LineString, GeoJSONObjectType.MultiLineString,
            GeoJSONObjectType.Polygon, GeoJSONObjectType.MultiPolygon)]
            GeoJSON.Net.GeoJSONObjectType type)
        {
            // Arrange
            GeoJSONObject g = null;
            string expected = null;

            LineString linestring = new GeoJSON.Net.Geometry.LineString(new System.Collections.Generic.List<GeoJSON.Net.Geometry.IPosition>
            {
                new GeoJSON.Net.Geometry.Position(10, 20),
                new GeoJSON.Net.Geometry.Position(20, 30),
                new GeoJSON.Net.Geometry.Position(30, 30),
                new GeoJSON.Net.Geometry.Position(10, 20)
            });
            string linestringStr = "(20 10, 30 20, 30 30, 20 10)";
            GeoJSON.Net.Geometry.Polygon polygon = new GeoJSON.Net.Geometry.Polygon(new System.Collections.Generic.List<GeoJSON.Net.Geometry.LineString>() { linestring });


            switch (type)
            {
                case GeoJSONObjectType.Point:
                    g = new GeoJSON.Net.Geometry.Point(new GeoJSON.Net.Geometry.Position(10, 20));
                    expected = "POINT(20 10)";
                    break;
                case GeoJSONObjectType.MultiPoint:
                    {
                        System.Collections.Generic.List<GeoJSON.Net.Geometry.Point> positions = new System.Collections.Generic.List<GeoJSON.Net.Geometry.Point>();
                        positions.Add(new GeoJSON.Net.Geometry.Point(new GeoJSON.Net.Geometry.Position(10, 20)));
                        positions.Add(new GeoJSON.Net.Geometry.Point(new GeoJSON.Net.Geometry.Position(20, 30)));
                        g = new GeoJSON.Net.Geometry.MultiPoint(positions.ToArray());
                        expected = "MULTIPOINT(20 10, 30 20)";
                    }
                    break;
                case GeoJSONObjectType.LineString:
                    g = linestring;
                    expected = "LINESTRING" + linestringStr;
                    break;
                case GeoJSONObjectType.MultiLineString:
                    g = new MultiLineString(new System.Collections.Generic.List<GeoJSON.Net.Geometry.LineString>() { linestring,linestring }); 
                    expected = "MULTILINESTRING(" + linestringStr + ", " + linestringStr + ")";
                    break;
                case GeoJSONObjectType.Polygon:
                    g = polygon;
                    expected = "POLYGON(" + linestringStr + ")";
                    break;
                case GeoJSONObjectType.MultiPolygon:
                    g = new MultiPolygon(new System.Collections.Generic.List<GeoJSON.Net.Geometry.Polygon>() { polygon, polygon });
                    expected = "MULTIPOLYGON((" + linestringStr + "), (" + linestringStr + "))";
                    break;
            }
            Assert.That(g, Is.Not.Null, "Geometric object for testing available");
            GeoSearchAddress geoSearchAddress = new GeoSearchAddress
            {
                Geometry = g
            };
            Assert.That(geoSearchAddress.GeometryWkt, Is.EqualTo(expected), "Correct WKT transformation for " + type);
        }

        [Test]
        public void TestEncoding()
        {
            //Arrange
            var text = @"Københavns Universitet (Universitet/Faghøjskole - København)";
            //Act
            var escapeString = Uri.EscapeDataString(text);
            //Asset
            Assert.That(escapeString, Is.EqualTo("K%C3%B8benhavns%20Universitet%20%28Universitet%2FFagh%C3%B8jskole%20-%20K%C3%B8benhavn%29"));
        }

        [Test]
        [Category("Integration")]
        [Explicit]
        [Ignore("Integration test")]
        public void TestSpecialCharactersArrayWithAndAInfront()
        {
            //Arange
            var searchRequestParam = new SearchRequestParams();
            searchRequestParam.Resources = "Adresser";
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
        [Category("Long")]
        [Category("Integration")]
        [Explicit]
        [Ignore("Integration test (>1 min)")]
        public void TestSpecialCharactersArrayJustOne()
        {
            //Arange
            var searchRequestParam = new SearchRequestParams();
            searchRequestParam.Resources = "Adresser";
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

        // Designed test method to check the availability of specific results from specific search resources
        // Implemented to validate the transition to the gsearch api
        [Test]
        [Category("Integration")]
        [Explicit]
        //[Ignore("Integration test")]
        public void TestSearchResults([Values(
            "chokoladek@stednavn>Chokoladekrydset",
            "fredericia stadion@stednavn>Monjasa Park",
            "tøjhusmuseet@stednavn>Krigsmuseet (Museum i København K)"

            )] string t)
        {
            //Arrange
            var searchRequestParam = new SearchRequestParams();
            char[] splitChars = new char[] { '@', '>' };
            string[] parts = t.Split(splitChars);
            searchRequestParam.SearchText = parts[0];
            searchRequestParam.Resources = parts[1];
            string needle = parts[2];

            var geosearchService = new GeosearchService();
            GeoSearchAddressData geoSearchAddressData = null;
            //Act;
            geoSearchAddressData = geosearchService.Request(searchRequestParam);

            //Assess
            var message = geoSearchAddressData.message;
            if (message != "OK")
            {
                Console.WriteLine(string.Format("\"{0}\"={1}", searchRequestParam.SearchText, message));
            }
            System.Collections.Generic.List<GeoSearchAddress> hits = geoSearchAddressData.data;
            bool foundTheNeedle = false;
            foreach (GeoSearchAddress hit in hits)
            {
                if (hit.Visningstekst.Contains(needle)) { foundTheNeedle = true; break; }
            }
            Assert.That(foundTheNeedle, Is.True,
                "Did not find " + needle + " when searching for " + searchRequestParam.SearchText +
                " in " + searchRequestParam.Resources);
        }

        // Designed test method to check the coordinate reference frame for search result
        // Implemented to validate the transition to the gsearch api
        [Test]
        [Category("Integration")]
        [Explicit]
        //[Ignore("Integration test")]
        public void TestResultLocation()
        {
            //Arrange
            var searchRequestParam = new SearchRequestParams();
            searchRequestParam.SearchText = "chokoladek";
            searchRequestParam.Resources = "stednavn";
            string needle = "Chokoladekrydset";

            var geosearchService = new GeosearchService();
            //Act;
            var geoSearchAddressData = geosearchService.Request(searchRequestParam);
            //Asset
            var message = geoSearchAddressData.message;
            if (message != "OK")
            {
                Console.WriteLine(string.Format("\"{0}\"={1}", searchRequestParam.SearchText, message));
            }
            System.Collections.Generic.List<GeoSearchAddress> hits = geoSearchAddressData.data;
            bool foundTheNeedle = false;
            GeoSearchAddress testAddress = null;
            foreach (GeoSearchAddress hit in hits)
            {
                if (hit.Visningstekst.Contains(needle))
                {
                    testAddress = hit;
                    foundTheNeedle = true;
                    break;
                }
            }
            Assert.That(foundTheNeedle, Is.True,
                "Did not find " + needle + " when searching for " + searchRequestParam.SearchText +
                " in " + searchRequestParam.Resources);

            // Regular expression to extract the first coordinate of the test address
            Regex regex = new Regex("\\((?<x>[^\\.]+)\\S+\\s+(?<y>[0-9]+)");
            Match match = regex.Matches(testAddress.GeometryWkt)[0];
            double x = Double.Parse(match.Groups["x"].Value);
            double y = Double.Parse(match.Groups["y"].Value);

            // Check the raw answer location (with some number precision slack due to regex reduction and potential coordinate rounding MULTIPOINT(711872.0496 6181397.1604)
            Assert.That(x, Is.InRange(711850, 711900), "x coordinate in range");
            Assert.That(y, Is.InRange(6181350, 6181450), "y coordinate in range");

            // Check the view port location update
            var geometry = Make.Esri.Geometry.WithCentroid(Make.Esri.Point.Coords(x, y).Build).Build;
            var factory = Make.Factory(testAddress).ConvertWKTToGeometryReturns(geometry).Build;
            var placeFinderController = new PlaceFinderController(factory);
            var expectedEnvelope = Make.Esri.Envelope
                .XMax(718956)
                .XMin(718696)
                .YMax(6183202)
                .YMin(6183042).Build;

            //Act
            placeFinderController.SearchTextChange(testAddress.Visningstekst);
            placeFinderController.ZoomTo(testAddress);

            //Assert
            Validator.Map(factory.MxDocument.FocusMap)
                .NewExtentIsSet(expectedEnvelope)
                .MapIsRefresh
                .Validate();
        }
    }
}
