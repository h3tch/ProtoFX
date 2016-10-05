namespace System.Windows.Forms
{
    class TabPageEx : TabPage
    {
        public string FilePath { get; set; }

        public TabPageEx(string filepath) : base()
        {
            FilePath = filepath;
        }
    }
    
    public static class TabPageExExtensions
    {
        /// <summary>
        /// Find first tab that references a file with the specified path.
        /// </summary>
        /// <param name="tab">Tab collection.</param>
        /// <param name="path">Path to search for.</param>
        /// <returns>Returns the index of the specified tab or -1 if no tab could be found.</returns>
        public static int IndexOf(this TabControl.TabPageCollection tab, string path)
        {
            for (int i = 0; i < tab.Count; i++)
            {
                var t = (TabPageEx)tab[i];
                if (t.FilePath != null && t.FilePath.Equals(path, StringComparison.CurrentCultureIgnoreCase))
                    return i;
            }
            return -1;
        }
    }
}
