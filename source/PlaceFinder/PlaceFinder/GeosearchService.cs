using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using PlaceFinder.GeoSearch;

namespace PlaceFinder
{
    public class GeosearchService : IGeosearchService
    {
        public GeoSearchAddressData Request(string inputParamSearch, string inputParamResources,
                                                                 string inputParamLimit, string inputParamLogin,
                                                                 string inputParamPassword, string inputParamCRS)
        {
            //TODO move resource to file
            var url =
                String.Format(
                    "https://kortforsyningen.kms.dk/Geosearch?type=json&search={0}&resources={1}&limit={2}&login={3}&password={4}&crs={5}",
                    inputParamSearch, inputParamResources, inputParamLimit, inputParamLogin, inputParamPassword,
                    inputParamCRS);

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

    }
}