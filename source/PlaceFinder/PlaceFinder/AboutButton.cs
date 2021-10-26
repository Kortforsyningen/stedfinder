using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace GeodataStyrelsen.ArcMap.PlaceFinder
{
    public class AboutButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        protected override void OnClick()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Danske stednavnes stedfinder.");
            stringBuilder.AppendLine();

            // Version
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            stringBuilder.AppendLine("Version:");
            stringBuilder.AppendLine(fileVersionInfo.FileVersion);
            stringBuilder.AppendLine();

            stringBuilder.AppendLine("Tidligere udvikling:");
            stringBuilder.AppendLine("Steen Hulthin Rasmussen");
            stringBuilder.AppendLine("Bjørn Elo Petersen");
            stringBuilder.AppendLine();
            // kontakt
            stringBuilder.AppendLine("Udvikling og kontaktperson (Contact):");
            stringBuilder.AppendLine("Jørgen Wanscher, x009068@sdfe.dk");
            stringBuilder.AppendLine("(eller jbw@hermestraffic.com)");

            stringBuilder.AppendLine();

            // attribution
            stringBuilder.AppendLine("Anvendte resourcer:");
            stringBuilder.AppendLine("Jack Cai's info_black button"); //http://findicons.com/icon/175921/info_black?id=362845

            MessageBox.Show(stringBuilder.ToString(), "Om danske stednavne stedfinder", MessageBoxButtons.OK);
        }
    }
}
