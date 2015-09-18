namespace App
{
    class TabPage : System.Windows.Forms.TabPage
    {
        public string filepath;

        public TabPage(string filepath) : base()
        {
            this.filepath = filepath;
        }
    }
}
