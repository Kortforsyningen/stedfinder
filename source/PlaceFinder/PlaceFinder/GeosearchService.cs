using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using ESRI.ArcGIS.Geometry;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;

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
                //TODO move resource to file
                var url =
                    string.Format(
                        Interface.Properties.Settings.Default.Url,
                        searchRequestParams.Resources, searchRequestParams.Token, searchText, searchRequestParams.ReturnLimit
                        );

                var webClient = new WebClient {Encoding = Encoding.UTF8};

                System.Diagnostics.Debug.WriteLine("Url: " + url);

                var jsonContent = webClient.DownloadString(url);
                if (jsonContent.Equals("No workhorse available\r\nAll workhorses, including quarantined, are exhausted.\r\n"))
                {
                    throw new PlaceFinderException("Kan ikke søge på: '" + searchRequestParams.SearchText + "'");
                }
                var serializer = new DataContractJsonSerializer(typeof (List<GeoSearchAddress>));

                GeoSearchAddressData response = new GeoSearchAddressData();
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonContent)))
                {
                    List<GeoSearchAddress> hits = (List<GeoSearchAddress>) serializer.ReadObject(ms);
                    response.message = "OK";
                    response.status = "OK";
                    response.data = hits;
                }
                return response;
            }
            catch (WebException e)
            {
                throw new PlaceFinderException("Ikke muligt at få kontakt med Geosearch service", e);
            }
        }

    }
}