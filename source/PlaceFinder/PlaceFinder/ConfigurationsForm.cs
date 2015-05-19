using System.Windows.Forms;
using PlaceFinder.Interface;

namespace PlaceFinder
{
    public partial class ConfigurationsForm : Form
    {
        private readonly IPlaceFinderController _placeFinderController;

        public ConfigurationsForm(IPlaceFinderController placeFinderController)
        {
            _placeFinderController = placeFinderController;
            InitializeComponent();
        }

        private void ConfigurationsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var checkedItemCollection = SearchRequestResourcesCheckedListBox.CheckedItems;
            _placeFinderController.SearchResourcesChange(checkedItemCollection);
        }
    }
}
