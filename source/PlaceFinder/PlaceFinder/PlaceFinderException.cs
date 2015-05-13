using System;

namespace PlaceFinder
{
    public class PlaceFinderException : Exception
    {
        public PlaceFinderException(string message) : base(message)
        {
        }
    }
}