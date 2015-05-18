namespace PlaceFinder.Interface
{
    public class SearchRequestParams
    {

        public SearchRequestParams()
        {
            SearchText = "";
            Resources = "Adresser,Stednavne";
            ReturnLimit = "20";
            LoginName = "PlaceFinder";
            Password = "PlaceFinder!1";
            EPSGCode = "epsg:4326";
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
        public bool ElectoralDistrict { get; set; }
        public bool PoliceDistrict { get; set; }
        public bool PostDistricts { get; set; }
        public bool Regions { get; set; }
        public bool JurisdictionsDistrict { get; set; }

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
                if (PlaceNames)
                {
                    resourcesString = AppendToResource(resourcesString, "Stednavne");
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