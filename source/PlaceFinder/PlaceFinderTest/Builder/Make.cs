﻿using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;
using GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder.Esri;

namespace GeodataStyrelsen.ArcMap.PlaceFinderTest.Builder
{
    public class Make
    {
        public class Esri
        {
            public static MxDocumentBuilder MxDocument
            {
                get { return new MxDocumentBuilder(); }
            }

            public static SpatialReferenceFactoryBuilder SpatialReferenceFactory
            {
                get { return new SpatialReferenceFactoryBuilder(); }
            }

            public static SpatialReferenceBuilder SpatialReference
            {
                get { return new SpatialReferenceBuilder(); }
            }

            public static GeometryBuilder Geometry
            {
                get { return new GeometryBuilder();}
            }

            public static EnvelopeBuilder Envelope
            {
                get { return new EnvelopeBuilder();}

            }

            public static MapBuilder Map
            {
                get { return new MapBuilder(); }
            }

            public static PointBuilder Point
            { get{ return  new PointBuilder();}}
        }

        public static PlaceFinderDockableWindowBuilder PlaceFinderDockableWindow
        {
            get { return new PlaceFinderDockableWindowBuilder(); }
        }

        public static GeosearchServiceBuilder GeosearchService
        {
            get { return new GeosearchServiceBuilder(); }
        }

        public static FactoryBuilder Factory(GeoSearchAddress resultAddress)
        {
            return new FactoryBuilder(resultAddress); 
        }

    }
}
