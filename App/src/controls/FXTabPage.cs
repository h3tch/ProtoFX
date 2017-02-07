namespace System.Windows.Forms
{
    /// <summary>
    /// Extends the default TabPage with a UserData property.
    /// </summary>
    class FXTabPage : TabPage
    {
        public object UserData { get; set; }

        public FXTabPage() : base() { }
    }
    
    public static class FXTabPageExtensions
    {
        /// <summary>
        /// Find first tab that references a file with the specified path.
        /// </summary>
        /// <param name="tab">Tab collection</param>
        /// <param name="userdata">User data to search for.</param>
        /// <param name="comparison">String comparison mode</param>
        /// <returns>Returns the index of the specified tab or -1 if no tab could be found.</returns>
        public static int IndexOf(this TabControl.TabPageCollection tab, string userdata,
            StringComparison comparison = StringComparison.CurrentCultureIgnoreCase)
        {
            // seach all tabs
            for (int i = 0; i < tab.Count; i++)
            {
                // if is not an FXTabPage then there can be no user data
                if (!(tab[i] is FXTabPage))
                    continue;

                var t = tab[i] as FXTabPage;

                // if references are matching
                if (t.UserData == (object)userdata)
                    return i;

                // if user data is not a string
                if (t.UserData == null || !(t.UserData is string))
                    continue;

                var s = t.UserData as string;

                // compare user data
                if (s.Equals(userdata, comparison))
                    return i;
            }

            // search was unsuccessful
            return -1;
        }
    }
}
