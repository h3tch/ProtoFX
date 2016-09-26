using System.Drawing;
using System.Reflection;

namespace System.Windows.Forms
{
    public static class Theme
    {
        static public Color BackColor = Color.FromArgb(255, 51, 51, 51);
        static public Color ForeColor = Color.FromArgb(255, 180, 180, 180);
        static public Color HighlightBackColor = Color.FromArgb(255, 100, 100, 100);
        static public Color HighlightForeColor = Color.FromArgb(255, 220, 220, 220);
        static public Color SelectForeColor = Color.FromArgb(255, 130, 80, 180);
        static public Color Workspace = Color.FromArgb(255, 30, 30, 30);
        static public Color WorkspaceHighlight = Color.FromArgb(255, 60, 60, 60);
        const BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic;

        public static void Apply(Control control)
        {
            var type = control.GetType();
            var method = GetMethod(type, true) ?? GetMethod(type, false);
            bool applyToChildren = (bool)method?.Invoke(null, new object[] { control });

            if (applyToChildren)
                foreach (Control c in control.Controls)
                    Apply(c);
        }

        internal static MethodInfo GetMethod(Type type, bool exact)
        {
            var methods = typeof(Theme).GetMethods(flags);
            foreach (var method in methods)
            {
                if (method.Name == "ApplyTo")
                {
                    var @params = method.GetParameters();
                    if (@params.Length == 1 &&
                        exact ? type.IsEquivalentTo(@params[0].ParameterType)
                              : type.IsSubclassOf(@params[0].ParameterType))
                        return method;
                }
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
            c.BackColor = Workspace;
            c.ForeColor = ForeColor;
            c.BackgroundColor = Workspace;
            c.ColumnHeadersDefaultCellStyle.BackColor = BackColor;
            c.ColumnHeadersDefaultCellStyle.ForeColor = ForeColor;
            c.ColumnHeadersDefaultCellStyle.SelectionBackColor = HighlightBackColor;
            c.ColumnHeadersDefaultCellStyle.SelectionForeColor = HighlightForeColor;
            var style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
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
}
