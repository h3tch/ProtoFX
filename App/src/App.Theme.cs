using System.Windows.Forms;
using System.Xml;

namespace protofx
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
            // load previous theme
            if (!Theme.Load(settings.ThemeXml))
            {
                // save themes to xml
                Theme.LightTheme();
                Theme.Save($"{Theme.Name}.xml", false);
                Theme.DarkTheme();
                Theme.Save($"{Theme.Name}.xml", false);
            }

            Theme.Apply(this);
        }
    }
}
