using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;

namespace GeodataStyrelsen.ArcMap.PlaceFinder
{
    /// <summary>
    /// This specific implementation uses regular expressions to parse the JSON even though a NewtonSoft/GeoJSON.net
    /// solution was also implemented. That is available in the version history as checkin
    /// c5e74fb gsearch: Default search resource bug fixed
    /// The ms serialization branch following this tried using System.Text.Json for the parsing, but due to dll loading
    /// challenges both the implementations did not work under ArcMap 10.8.1 and were abandoned for this solution
    /// 
    /// Jørgen Wanscher (x009068sdfi.dk/jbw@hermestraffic.com)
    /// </summary>
    public class GeosearchService : IGeosearchService
    {
        // Regex for visningstekst
        private Regex rgxVisningstekst = new Regex("\"visningstekst\":\"(?<visningstekst>[^\"]+)\"");
        private Regex rgxId = new Regex("\"id\":\"(?<id>[^\"]+)\"");
        private Regex rgxOfficiel = new Regex("\"skrivemaade_officiel\":\"(?<officiel>[^\"]+)\"");
        private Regex rgxUOfficiel = new Regex("\"skrivemaade_uofficiel\":\"(?<uofficiel>[^\"]+)\"");

        public GeoSearchAddressData Request(SearchRequestParams searchRequestParams)
        {
            try
            {
                Regex pattern = new Regex("([\\\\#/])|(^[/&/./;])");
                var searchText = searchRequestParams.SearchText;
                //Uri.EscapeDataString(searchRequestParams.SearchText.Replace('/', '_').Replace('\\', '_').Replace('\0', '_').Replace('.', '_').Replace(';', '_'));
                searchText = pattern.Replace(searchText, "_");

                // Split the resources into individual resources and search each of them
                string[] resources = searchRequestParams.Resources.Split(new char[] { ',' });

                ConcurrentBag<GeoSearchAddress> hitsCollection = new ConcurrentBag<GeoSearchAddress>();

                System.Threading.Tasks.Parallel.ForEach(resources, resource =>
                {
                    var url =
                        string.Format(
                            Interface.Properties.Settings.Default.Url,
                            resource, searchRequestParams.Token, searchText, searchRequestParams.ReturnLimit
                            );

                    var webClient = new WebClient { Encoding = Encoding.UTF8 };

                    System.Diagnostics.Debug.WriteLine("Url: " + url);

                    var jsonContent = webClient.DownloadString(url);
                    if (jsonContent.Equals("No workhorse available\r\nAll workhorses, including quarantined, are exhausted.\r\n"))
                    {
                        throw new PlaceFinderException("Kan ikke søge på: '" + searchRequestParams.SearchText + "'");
                    }

                    string[] hitStrings = jsonContent.Split(new[] { "},{" }, System.StringSplitOptions.None);

                    List<GeoSearchAddress> hits = new List<GeoSearchAddress>(hitStrings.Length);
                   
                    foreach(string str in hitStrings)
                    {
                        // Skip empty (no hit entries
                        if (string.IsNullOrEmpty(str)) continue;
                        str.Trim();
                        if (string.IsNullOrEmpty(str)) continue;
                        // Parse the rest
                        string visningstekst = null;
                        Match matchVisningstekst = rgxVisningstekst.Match(str);
                        if (matchVisningstekst.Success) visningstekst = matchVisningstekst.Groups["visningstekst"].Value;
                        string idtekst = null;
                        Match matchId = rgxId.Match(str);
                        if (matchId.Success) idtekst = matchVisningstekst.Groups["id"].Value;
                        if (visningstekst != null)
                        {
                            // Specific workaround to add skrivemaade_uofficiel og skrivemaade_officiel if needed to hits from the stednavn resource
                            if (resource.CompareTo("stednavn") == 0)
                            {
                                // Check for the official naming
                                string officiel = null;
                                Match matchOfficiel = rgxOfficiel.Match(str);
                                if (matchOfficiel.Success) officiel = matchOfficiel.Groups["officiel"].Value;
                                // Reset this if visningstekst already contains "officiel" name
                                if (officiel != null && visningstekst.Contains(officiel)) officiel = null;

                                // Check for the unofficial naming
                                Match matchUofficiel = rgxUOfficiel.Match(str);
                                string uofficiel = null;
                                if (matchUofficiel.Success) uofficiel = matchUofficiel.Groups["uofficiel"].Value;
                                // Reset this if visningstekst already contains "officiel" name
                                if (uofficiel != null && visningstekst.Contains(uofficiel)) uofficiel = null;

                                string append = null;
                                if (officiel != null && uofficiel != null) append = " [" + officiel + "/" + uofficiel + "]";
                                else
                                {
                                    if (officiel != null) append = " [" + officiel + "]";
                                    if (uofficiel != null) append = " [" + uofficiel + "]";
                                }
                                // Append the values to visningstekst
                                if (append != null) visningstekst += append;
                            }

                            GeoSearchAddress geoAddress = new GeoSearchAddress()
                            {
                                Visningstekst = visningstekst,
                                Id = idtekst,
                                Ressource = resource,
                                Blob = str
                            };
                            hits.Add(geoAddress);
                        }
                    }

                    // Add the hits (if any) to the concurrent collection
                    hits?.ForEach(x => hitsCollection.Add(x));
                });

                // Convert concurrent collection to list (for type conformance)
                List<GeoSearchAddress> hitList = new List<GeoSearchAddress>();
                hitList.AddRange(hitsCollection);

                hitList.Sort((a, b) => a.Ressource.CompareTo(b.Ressource));

                GeoSearchAddressData response = new GeoSearchAddressData
                {
                    message = "OK",
                    status = "OK",
                    data = hitList
                };

                return response;
            }
            catch (WebException e)
            {
                throw new PlaceFinderException("Ikke muligt at få kontakt med Geosearch service", e);
            }
        }

    }
}