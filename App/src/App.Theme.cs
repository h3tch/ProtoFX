using System.Windows.Forms;
using System.Xml;

namespace App
{
    partial class App
    {
        private FormSettings LoadSettings()
        {
            // load settings
            return System.IO.File.Exists(Properties.Resources.WINDOW_SETTINGS_FILE)
                ? XmlSerializer.Load<FormSettings>(Properties.Resources.WINDOW_SETTINGS_FILE)
                : FormSettings.CreateCentered();
        }

        private void ApplyLayout(FormSettings settings)
        {
            // make layout DPI aware
            MakeDPIAware();
            // place form completely inside a screen
            settings.PlaceOnScreen(this);
            // place splitters
            settings.AdjustGUI(this);
        }

        private void ApplyTheme(FormSettings settings)
        {
            // LOAD PREVIOUS THEME

            if (!Theme.Load(settings.ThemeXml))
                Theme.Save(settings.ThemeXml);
            Theme.Apply(this);
        }
    }
}
