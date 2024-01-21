using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using ESRI.ArcGIS.Geometry;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;
using NetTopologySuite.Features;

namespace GeodataStyrelsen.ArcMap.PlaceFinder
{
    public class GeosearchService : IGeosearchService
    {
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
                    dynamic d = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonContent);

                    List<GeoSearchAddress> hits = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GeoSearchAddress>>(jsonContent);

                    // Add the hits (if any) to the concurrent collection
                    hits?.ForEach(x => hitsCollection.Add(x));
                });

                // Convert concurrent collection to list (for type conformance)
                List<GeoSearchAddress> hitList = new List<GeoSearchAddress>();
                hitList.AddRange(hitsCollection);

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