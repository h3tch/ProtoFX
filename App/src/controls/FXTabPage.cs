namespace System.Windows.Forms
{
    public static class FXTabPageExtensions
    {
        /// <summary>
        /// Find first tab that references a file with the specified path.
        /// </summary>
        /// <param name="tab">Tab collection</param>
        /// <param name="userdata">User data to search for.</param>
        /// <param name="comparison">String comparison mode</param>
        /// <returns>Returns the index of the specified tab or -1 if no tab could be found.</returns>
        public static int IndexOf(this TabControl.TabPageCollection tab, string filename,
            StringComparison comparison = StringComparison.CurrentCultureIgnoreCase)
        {
            // seach all tabs
            for (int i = 0; i < tab.Count; i++)
            {
                // if is not an FXTabPage then there can be no user data
                if (tab[i].Controls.Count > 0 && !(tab[i].Controls[0] is ScintillaNET.CodeEditor))
                    continue;

                var editor = tab[i].Controls[0] as ScintillaNET.CodeEditor;

                // if references are matching
                if (editor.Filename?.Equals(filename, comparison) ?? false)
                    return i;
            }

            // search was unsuccessful
            return -1;
        }
    }
}
