using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NUnit.Framework;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest
{
    [TestFixture]
    public class TestGeosearchService
    {
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
    }
}
