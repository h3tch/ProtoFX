using System.Collections.Generic;
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
            var margin = Margins[BREAKPOINT_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = BREAKPOINT_MARKER_MASK | EXE_LINE_MARKER_MASK;
            margin.Cursor = MarginCursor.Arrow;

            // initialize breakpoint marker
            var marker = Markers[BREAKPOINT_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(Color.IndianRed);
            marker.SetForeColor(Color.IndianRed);

            // initialize current execution line marker
            marker = Markers[EXE_LINE_MARKER];
            marker.Symbol = MarkerSymbol.ShortArrow;
            marker.SetBackColor(Color.LightYellow);
            marker.SetForeColor(Color.LightYellow);
        }

        private void HandleMarginClick(object sender, MarginClickEventArgs e)
        {
            switch (e.Margin)
            {
                /// ON BOOKMARK MARGIN CLICK
                case BREAKPOINT_MARGIN:
                    var line = Lines[LineFromPosition(e.Position)];
                    // Do we have a marker for this line?
                    if ((line.MarkerGet() & BREAKPOINT_MARKER_MASK) > 0)
                        // remove existing bookmark
                        line.MarkerDelete(BREAKPOINT_MARKER);
                    else
                        // add bookmark
                        line.MarkerAdd(BREAKPOINT_MARKER);
                    break;
            }
        }

        public void RemoveInvalidBreakpoints(IEnumerable<int[]> shaderLines)
        {
            var iter = shaderLines.GetEnumerator();
            iter.MoveNext();
            for (int l = 0; l < Lines.Count; l++)
            {
                if (HasBreakpoint(l))
                {
                    while (iter.Current != null && iter.Current[0] + iter.Current[1] < l)
                        iter.MoveNext();
                    if (iter.Current != null && (l < iter.Current[0] || iter.Current[0] + iter.Current[1] <= l))
                        RemoveBreakpoint(l);
                }
            }
        }

        public IEnumerable<int> GetBreakpoints()
        {
            for (int l = 0; l < Lines.Count; l++)
                if (HasBreakpoint(l))
                    yield return l;
        }

        public void AddExecutionMarker(int line)
            => Lines[line].MarkerAdd(EXE_LINE_MARKER);

        public void RemoveExecutionMarker(int line)
            => Lines[line].MarkerDelete(EXE_LINE_MARKER);

        public void AddBreakpoint(int line)
            => Lines[line].MarkerAdd(BREAKPOINT_MARKER);

        public void RemoveBreakpoint(int line)
            => Lines[line].MarkerDelete(BREAKPOINT_MARKER);

        public bool HasBreakpoint(int line)
            => (Lines[line].MarkerGet() & BREAKPOINT_MARKER_MASK) > 0;
        
        public int PrevBreakpoint(int line)
            => Lines[line].MarkerPrevious(1 << BREAKPOINT_MARKER);

        public int NextBreakpoint(int line)
            => Lines[line].MarkerNext(1 << BREAKPOINT_MARKER);
    }
}
