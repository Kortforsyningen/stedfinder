using System.Diagnostics;
using System.Windows.Forms;
using GeodataStyrelsen.ArcMap.PlaceFinder.Interface;

namespace GeodataStyrelsen.ArcMap.PlaceFinder
{
    public partial class ConfigurationsForm : Form
    {
        private readonly IPlaceFinderController _placeFinderController;

        public ConfigurationsForm(IPlaceFinderController placeFinderController, string[] selected)
        {
            _placeFinderController = placeFinderController;
            InitializeComponent();
            for (int i = 0; i < selected.Length; ++i)
                SearchRequestResourcesCheckedListBox.SetItemCheckState(SearchRequestResourcesCheckedListBox.Items.IndexOf(selected[i]), CheckState.Checked);

            // Force update of the place finder controller similar to closing the selection form
            ConfigurationsForm_FormClosing(null, null);
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

        private void SearchRequestResourcesCheckedListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }
    }
}
