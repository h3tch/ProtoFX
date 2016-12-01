using System.Drawing;

namespace ScintillaNET
{
    public partial class CodeEditor : Scintilla
    {
        private void InitializeBookmarks()
        {
            // add marker event
            MarginClick += HandleMarginClick;

            // add breakpoint margin
            var margin = Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = BOOKMARK_MARKER_MASK;
            margin.Cursor = MarginCursor.Arrow;

            // initialize breakpoint marker
            var marker = Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(Color.IndianRed);
            marker.SetForeColor(Color.Black);
        }

        private void HandleMarginClick(object sender, MarginClickEventArgs e)
        {
            switch (e.Margin)
            {
                /// ON BOOKMARK MARGIN CLICK
                case BOOKMARK_MARGIN:
                    var line = Lines[LineFromPosition(e.Position)];
                    // Do we have a marker for this line?
                    if ((line.MarkerGet() & BOOKMARK_MARKER_MASK) > 0)
                        // remove existing bookmark
                        line.MarkerDelete(BOOKMARK_MARKER);
                    else
                        // add bookmark
                        line.MarkerAdd(BOOKMARK_MARKER);
                    break;
            }
        }
    }
}
