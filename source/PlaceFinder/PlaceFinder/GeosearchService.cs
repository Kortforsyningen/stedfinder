using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
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
                        "https://kortforsyningen.kms.dk/Geosearch?type=json&search={0}&resources={1}&limit={2}&login={3}&password={4}&crs={5}",
                        searchText, searchRequestParams.Resources, searchRequestParams.ReturnLimit,
                        searchRequestParams.LoginName, searchRequestParams.Password,
                        searchRequestParams.EPSGCode);

                var webClient = new WebClient {Encoding = Encoding.UTF8};
                var jsonContent = webClient.DownloadString(url);
                if (jsonContent.Equals("No workhorse available\r\nAll workhorses, including quarantined, are exhausted.\r\n"))
                {
                    throw new PlaceFinderException("Kan ikke søge på: '" + searchRequestParams.SearchText + "'");
                }
                var serializer = new DataContractJsonSerializer(typeof (GeoSearchAddressData));

                GeoSearchAddressData response;
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonContent)))
                {
                    response = (GeoSearchAddressData) serializer.ReadObject(ms);
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