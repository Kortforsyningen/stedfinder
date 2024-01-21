using GeodataStyrelsen.ArcMap.PlaceFinder.Interface.Properties;

namespace GeodataStyrelsen.ArcMap.PlaceFinder.Interface
{
    public class SearchRequestParams
    {
        public SearchRequestParams()
        {
            // Set from the GUI
            SearchText = "";
            // Updated from the ConfigurationFrame close code
            Resources = "";
            // Get these settings from the settings file
            ReturnLimit = Settings.Default.ReturnLimit.ToString();
            Token = Settings.Default.Token;
        }

        public string SearchText { get; set; }

        public string Resources { get; set; }

        public string ReturnLimit { get; set; }

        public string Token { get; set; }
    }

    public class SearchRequestResources
    {
        public bool Addresses { get; set; }
        public bool Street { get; set; }
        public bool HouseNumber { get; set; }
        public bool Municipalities { get; set; }
        public bool Cadastre { get; set; }
        public bool CadastreDeprecated { get; set; }
        public bool PlaceNames { get; set; }
        public bool ElectoralDistrict { get; set; }
        public bool Parish { get; set; }
        public bool PoliceDistrict { get; set; }
        public bool PostDistricts { get; set; }
        public bool Regions { get; set; }
        public bool JurisdictionsDistrict { get; set; }

        public string GetResourceString
        {
            get
            {
                string resourcesString = "";
                if (Addresses)
                {
                    resourcesString = AppendToResource(resourcesString, "adresse");
                }
                if (HouseNumber)
                {
                    resourcesString = AppendToResource(resourcesString, "husnummer");
                }
                if (Municipalities)
                {
                    resourcesString = AppendToResource(resourcesString, "kommune");
                }
                if (Cadastre)
                {
                    resourcesString = AppendToResource(resourcesString, "matrikel");
                }
                if (CadastreDeprecated)
                {
                    resourcesString = AppendToResource(resourcesString, "matrikel_udgaaet");
                }
                if (Street)
                {
                    resourcesString = AppendToResource(resourcesString, "navngivenvej");
                }
                if (ElectoralDistrict)
                {
                    resourcesString = AppendToResource(resourcesString, "opstillingskreds");
                }
                if (PoliceDistrict)
                {
                    resourcesString = AppendToResource(resourcesString, "politikreds");
                }
                if (PostDistricts)
                {
                    resourcesString = AppendToResource(resourcesString, "postnummer");
                }
                if (Regions)
                {
                    resourcesString = AppendToResource(resourcesString, "region");
                }
                if (JurisdictionsDistrict)
                {
                    resourcesString = AppendToResource(resourcesString, "retskreds");
                }
                if (Parish)
                {
                    resourcesString = AppendToResource(resourcesString, "sogn");
                }
                if (PlaceNames)
                {
                    resourcesString = AppendToResource(resourcesString, "stednavn");
                }

                return resourcesString;
            }
        }

        private static string AppendToResource(string resourcesString, string resourceName)
        {
            if (!string.IsNullOrEmpty(resourcesString))
            {
                resourcesString += ",";
            }
            resourcesString += resourceName;
            return resourcesString;
        }
    }
}