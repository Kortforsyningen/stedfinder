namespace GeodataStyrelsen.ArcMap.PlaceFinder.Interface
{
    public class GeoSearchAddress
    {
        // Primary display text for a hit (is available for all search results)
        public string visningstekst { get; set; }
        // Unique id available for all search results
        public string id { get; set; }
        // Generate WKT built from other properties to ensure single access to geometry of hit (for zooming) 
        public string geometryWkt { get { return "Test"; } }
        // Geometric properties for the geometryWkt method
        public GeoSearchGeometry geometri {  get; set; }
        public GeoSearchGeometry vejpunkt_geometri { get; set; }
        // Generic properties that may be available for different result sets

        public string type { get; set; }
        public string streetName { get; set; }
        public string postCodeIdentifier { get; set; }
        public string districtName { get; set; }
        public bool validCoordinates { get; set; }
        public double xMax { get; set; }
        public double yMax { get; set; }
        public double xMin { get; set; }
        public double yMin { get; set; }
        public string municipalityCodes { get; set; }
        public string streetCodes { get; set; }
        public string id_lokalid { get; set; }
        public string name { get; set; }
        public string elavsnavn { get; set; }
        public string elavskode { get; set; }
        public string matrnr { get; set; }
        public string regionKoder { get; set; }
        public string featKode { get; set; }
        public object stednavnType { get; set; }
        public string valgkredsNr { get; set; }
        public string storkredsNr { get; set; }
        public string storkredsNavn { get; set; }
        public string landsdelsNr { get; set; }
        public string landsdelsNavn { get; set; }
   }
}
