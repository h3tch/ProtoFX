using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace System.Windows.Forms
{
    public class Theme
    {
        public struct Palette
        {
            public string Name;
            public XmlColor BackColor;
            public XmlColor ForeColor;
            public XmlColor HighlightBackColor;
            public XmlColor HighlightForeColor;
            public XmlColor SelectForeColor;
            public XmlColor Workspace;
            public XmlColor WorkspaceHighlight;
        }
        internal static Palette palette = new Palette();
        public static string Name => palette.Name;
        public static Color BackColor => palette.BackColor;
        public static Color ForeColor => palette.ForeColor;
        public static Color HighlightBackColor => palette.HighlightBackColor;
        public static Color HighlightForeColor => palette.HighlightForeColor;
        public static Color SelectForeColor => palette.SelectForeColor;
        public static Color Workspace => palette.Workspace;
        public static Color WorkspaceHighlight => palette.WorkspaceHighlight;

        static Theme() {
            palette.Name = "DarkTheme";
            palette.BackColor = Color.FromArgb(255, 51, 51, 51);
            palette.ForeColor = Color.FromArgb(255, 160, 160, 160);
            palette.HighlightBackColor = Color.FromArgb(255, 100, 100, 100);
            palette.HighlightForeColor = Color.FromArgb(255, 220, 220, 220);
            palette.SelectForeColor = Color.FromArgb(255, 130, 80, 180);
            palette.Workspace = Color.FromArgb(255, 30, 30, 30);
            palette.WorkspaceHighlight = Color.FromArgb(255, 60, 60, 60);
        }

        public static bool Load(string path)
        {
            if (!File.Exists(path))
                return false;
            palette = XmlSerializer.Load<Palette>(path);
            return true;
        }

        public static void Save(string path)
        {
            XmlSerializer.Save(palette, path);
        }

        /// <summary>
        /// Apply theme to the specified control and its children.
        /// </summary>
        /// <param name="control"></param>
        public static void Apply(Control control)
        {
            var type = control.GetType();
            var method = GetMethod(type, true) ?? GetMethod(type, false);
            if (!(bool)method?.Invoke(null, new object[] { control }))
                return;
            foreach (Control c in control.Controls)
                Apply(c);
        }

        internal static MethodInfo GetMethod(Type type, bool exact)
        {
            var methods = typeof(Theme).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                if (method.Name != "ApplyTo")
                    continue;
                var @params = method.GetParameters();
                if (@params.Length == 1 &&
                    exact ? type.IsEquivalentTo(@params[0].ParameterType)
                          : type.IsSubclassOf(@params[0].ParameterType))
                    return method;
            }
            return null;
        }

        private static bool ApplyTo(PropertyGrid c)
        {
            c.BackColor = BackColor;
            c.CategoryForeColor = ForeColor;
            c.CategorySplitterColor = BackColor;
            c.DisabledItemForeColor = HighlightBackColor;
            c.HelpBackColor = Workspace;
            c.HelpBorderColor = BackColor;
            c.HelpForeColor = ForeColor;
            c.LineColor = BackColor;
            c.SelectedItemWithFocusBackColor = WorkspaceHighlight;
            c.SelectedItemWithFocusForeColor = ForeColor;
            c.ViewBackColor = Workspace;
            c.ViewBorderColor = WorkspaceHighlight;
            c.ViewForeColor = ForeColor;
            return false;
        }

        private static bool ApplyTo(DataGridView c)
        {
            var style = new DataGridViewCellStyle();
            c.BackColor = Workspace;
            c.ForeColor = ForeColor;
            c.BackgroundColor = Workspace;
            c.ColumnHeadersDefaultCellStyle.BackColor = BackColor;
            c.ColumnHeadersDefaultCellStyle.ForeColor = ForeColor;
            c.ColumnHeadersDefaultCellStyle.SelectionBackColor = HighlightBackColor;
            c.ColumnHeadersDefaultCellStyle.SelectionForeColor = HighlightForeColor;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            style.WrapMode = DataGridViewTriState.False;
            style.BackColor = Workspace;
            style.ForeColor = ForeColor;
            style.SelectionBackColor = WorkspaceHighlight;
            style.SelectionForeColor = HighlightForeColor;
            c.DefaultCellStyle = style;
            c.RowHeadersDefaultCellStyle.BackColor = Workspace;
            c.RowHeadersDefaultCellStyle.ForeColor = ForeColor;
            c.RowHeadersDefaultCellStyle.SelectionBackColor = WorkspaceHighlight;
            c.RowHeadersDefaultCellStyle.SelectionForeColor = HighlightForeColor;
            c.GridColor = WorkspaceHighlight;
            return true;
        }

        private static bool ApplyTo(ComboBoxEx c)
        {
            c.ForeColor = ForeColor;
            c.BackColor = Workspace;
            c.BorderColor = ForeColor;
            return true;
        }

        private static bool ApplyTo(TabControlEx c)
        {
            c.BackColor = BackColor;
            c.ForeColor = ForeColor;
            c.HighlightForeColor = HighlightForeColor;
            c.WorkspaceColor = Workspace;
            return true;
        }

        private static bool ApplyTo(ListView c)
        {
            c.BackColor = Workspace;
            c.ForeColor = ForeColor;
            return true;
        }

        private static bool ApplyTo(NumericUpDown c)
        {
            c.BackColor = Workspace;
            c.ForeColor = ForeColor;
            return false;
        }

        private static bool ApplyTo(ImageViewer c)
        {
            c.BackColor = Workspace;
            c.ForeColor = ForeColor;
            return false;
        }

        private static bool ApplyTo(Control c)
        {
            c.BackColor = BackColor;
            c.ForeColor = ForeColor;
            return true;
        }
    }

    public class XmlColor
    {
        private Color color = Color.Black;

        public XmlColor() { }

        public XmlColor(Color c) { color = c; }
        
        public Color ToColor() => color;
        
        public static implicit operator Color(XmlColor x) => x.ToColor();

        public static implicit operator XmlColor(Color c) => new XmlColor(c);

        [Xml.Serialization.XmlAttribute]
        public string RgbString
        {
            get { return ColorTranslator.ToHtml(color); }
            set
            {
                try
                {
                    if (Alpha == 0xFF)
                        color = ColorTranslator.FromHtml(value);
                    else
                        color = Color.FromArgb(Alpha, ColorTranslator.FromHtml(value));
                }
                catch (Exception)
                {
                    color = Color.Black;
                }
            }
        }

        [Xml.Serialization.XmlAttribute]
        public byte Alpha
        {
            get { return color.A; }
            set
            {
                if (value != color.A)
                    color = Color.FromArgb(value, color);
            }
        }

        public bool ShouldSerializeAlpha() => Alpha < 0xFF;
    }
}
