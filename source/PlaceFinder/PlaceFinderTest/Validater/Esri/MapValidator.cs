using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using NUnit.Framework;
using Rhino.Mocks;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Validater.Esri
{
    public class MapValidator : BaseValidator<IMap>
    {
        public MapValidator(IMap map) 
            : base(map)
        {
        }

        public MapValidator MapIsRefresh
        {
            get
            {
                ((IActiveView)Mock).AssertWasCalled(m => m.Refresh(), options => options.Repeat.AtLeastOnce());
                return this;
            }
        }

        public MapValidator NewExtentIsSet(IEnvelope envelope)
        {
            ValidataCordinateValue(Math.Round(((IActiveView)Mock).Extent.XMax,4), Math.Round(envelope.XMax, 4), "Envelope.XMax");
            ValidataCordinateValue(Math.Round(((IActiveView)Mock).Extent.XMin,4), Math.Round(envelope.XMin,4), "Envelope.XMin");
            ValidataCordinateValue(Math.Round(((IActiveView)Mock).Extent.YMax,4), Math.Round(envelope.YMax,4), "Envelope.YMax");
            ValidataCordinateValue(Math.Round(((IActiveView)Mock).Extent.YMin, 4), Math.Round(envelope.YMin, 4), "Envelope.YMin");

            return this;
        }

        private static void ValidataCordinateValue(double actual, double expectedValue, string message)
        {
            Assert.That(actual, Is.Not.Null, message);
            Assert.That(actual, Is.Not.EqualTo(0), message);
            Assert.That(actual, Is.EqualTo(expectedValue), message);
        }

        public MapValidator NewExtentIsSet()
        {
            Assert.That(((IActiveView)Mock).Extent, Is.Not.Null);

            return this;
        }
    }
}