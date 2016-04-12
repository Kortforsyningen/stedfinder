using System.Diagnostics;
using System.Windows.Forms;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;

namespace GeodataStyrelsen.ArcMap.PlaceFinder
{
    public partial class ConfigurationsForm : Form
    {
        private readonly IPlaceFinderController _placeFinderController;

        public ConfigurationsForm(IPlaceFinderController placeFinderController)
        {
            _placeFinderController = placeFinderController;
            InitializeComponent();
            SearchRequestResourcesCheckedListBox.SetItemCheckState(SearchRequestResourcesCheckedListBox.Items.IndexOf("Adresser"), CheckState.Checked);
            SearchRequestResourcesCheckedListBox.SetItemCheckState(SearchRequestResourcesCheckedListBox.Items.IndexOf("Stednavne"), CheckState.Checked);
        }

        private void ConfigurationsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var checkedItemCollection = SearchRequestResourcesCheckedListBox.CheckedItems;
            _placeFinderController.SearchResourcesChange(checkedItemCollection);
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ConfigurationsForm_Load(object sender, System.EventArgs e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            label1.Text = "Version " + fvi.FileVersion;
        }
    }
}
