using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;

namespace GeodataStyrelsen.ArcMap.PlaceFinder
{
    public class GeosearchService : IGeosearchService
    {
        public GeoSearchAddressData Request(SearchRequestParams searchRequestParams)
        {
            try
            {
                //TODO move resource to file
                var url =
                    String.Format(
                        "https://kortforsyningen.kms.dk/Geosearch?type=json&search={0}&resources={1}&limit={2}&login={3}&password={4}&crs={5}",
                        searchRequestParams.SearchText, searchRequestParams.Resources, searchRequestParams.ReturnLimit, searchRequestParams.LoginName, searchRequestParams.Password,
                        searchRequestParams.EPSGCode);

                var webClient = new WebClient { Encoding = Encoding.UTF8 };
                var jsonContent = webClient.DownloadString(url);
                var serializer = new DataContractJsonSerializer(typeof(GeoSearchAddressData));

                GeoSearchAddressData response;
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonContent)))
                {
                    response = (GeoSearchAddressData)serializer.ReadObject(ms);
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