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
            ValidataCordinateValue(((IActiveView)Mock).Extent.XMax, envelope.XMax, "Envelope.XMax");
            ValidataCordinateValue(((IActiveView)Mock).Extent.XMin, envelope.XMin, "Envelope.XMin");
            ValidataCordinateValue(((IActiveView)Mock).Extent.YMax, envelope.YMax, "Envelope.YMax");
            ValidataCordinateValue(((IActiveView)Mock).Extent.YMin, envelope.YMin, "Envelope.YMin");

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