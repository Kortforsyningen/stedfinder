using System;

namespace GeodataStyrelsen.ArcMap.PlaceFinder
{
    public class PlaceFinderException : Exception
    {
        public PlaceFinderException(string message) : base(message)
        {
        }

        public PlaceFinderException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}