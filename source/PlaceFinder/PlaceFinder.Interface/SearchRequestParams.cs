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
            LoginName = Settings.Default.Loginname;
            Password = Settings.Default.Password;
            EPSGCode = "epsg:" + Settings.Default.EPSGCode;
        }

        public string SearchText { get; set; }

        public string Resources { get; set; }

        public string ReturnLimit { get; set; }

        public string LoginName { get; set; }

        public string Password { get; set; }

        public string EPSGCode { get; set; }
    }

    public class SearchRequestResources
    {
        public bool Addresses { get; set; }
        public bool Street { get; set; }
        public bool HouseNumber { get; set; }
        public bool Municipalities { get; set; }
        public bool CadastralNumber { get; set; }
        public bool PlaceNames { get; set; }
        public bool PlaceNames_v2 { get; set; }
        public bool PlaceNames_v3 { get; set; }
        public bool ElectoralDistrict { get; set; }
        public bool PoliceDistrict { get; set; }
        public bool PostDistricts { get; set; }
        public bool Regions { get; set; }
        public bool JurisdictionsDistrict { get; set; }
        public bool Sogne { get; set; }

        //"Adresser,Veje,Husnumre,Kommuner,Matrikelnumre,Stednavne,Opstillingskredse,Politikredse,Postdistrikter,Regioner,Retskredse";
        public string GetResourceString
        {
            get
            {
                string resourcesString = "";
                if (Addresses)
                {
                    resourcesString = AppendToResource(resourcesString, "Adresser");
                }
                if (Street)
                {
                    resourcesString = AppendToResource(resourcesString, "Veje");
                }
                if (HouseNumber)
                {
                    resourcesString = AppendToResource(resourcesString, "Husnumre");
                }
                if (Municipalities)
                {
                    resourcesString = AppendToResource(resourcesString, "Kommuner");
                }
                if (CadastralNumber)
                {
                    resourcesString = AppendToResource(resourcesString, "Matrikelnumre");
                }
                if (PlaceNames) //used to be "Stednavne, Stednavne_v2" for some unknown reason. (x009068 has been expanded to explicit selection)
                {
                    resourcesString = AppendToResource(resourcesString, "Stednavne"); 
                }
                if (PlaceNames_v2)
                {
                    resourcesString = AppendToResource(resourcesString, "Stednavne_v2"); 
                }
                if (PlaceNames_v3)
                {
                    resourcesString = AppendToResource(resourcesString, "Stednavne_v3");
                }
                if (CadastralNumber)
                {
                    resourcesString = AppendToResource(resourcesString, "Opstillingskredse");
                }
                if (PoliceDistrict)
                {
                    resourcesString = AppendToResource(resourcesString, "Politikredse");
                }
                if (PostDistricts)
                {
                    resourcesString = AppendToResource(resourcesString, "Postdistrikter");
                }
                if (Regions)
                {
                    resourcesString = AppendToResource(resourcesString, "Regioner");
                }
                if (JurisdictionsDistrict)
                {
                    resourcesString = AppendToResource(resourcesString, "Retskredse");
                }
                if (Sogne)
                {
                    resourcesString = AppendToResource(resourcesString, "Sogne");
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