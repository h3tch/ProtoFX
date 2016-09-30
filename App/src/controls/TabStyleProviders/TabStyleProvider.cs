using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms
{
    public enum TabStyle
    {
        None,
        Default,
        Rounded,
        FX,
    }

    [ToolboxItem(false)]
    public abstract class TabStyleProvider : Component
    {
        #region Constructor
        
        protected TabStyleProvider(FXTabControl tabControl)
        {
            this.tabControl = tabControl;
            
            borderColor = Color.Empty;
            borderColorSelected = Color.Empty;
            focusColor = Color.Orange;

            imageAlign = tabControl.RightToLeftLayout
                ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft;
            
            HotTrack = true;
            
            // Must set after the _Overlap as this is used
            // in the calculations of the actual padding
            Padding = new Drawing.Point(6 + (ShowTabCloser ? 6 : 0), 3);
        }
        
        #endregion

        #region Factory Methods
        
        public static TabStyleProvider CreateProvider(FXTabControl tabControl)
        {
            TabStyleProvider provider;
            
            // Depending on the display style of the tabControl generate an appropriate provider.
            switch (tabControl.DisplayStyle) {
                case TabStyle.None:
                    provider = new TabStyleNoneProvider(tabControl);
                    break;
                case TabStyle.Default:
                    provider = new TabStyleDefaultProvider(tabControl);
                    break;
                case TabStyle.Rounded:
                    provider = new TabStyleRoundedProvider(tabControl);
                    break;
                case TabStyle.FX:
                    provider = new FXTabStyleProvider(tabControl);
                    break;
                default:
                    provider = new TabStyleDefaultProvider(tabControl);
                    break;
            }
            
            provider.style = tabControl.DisplayStyle;
            return provider;
        }
        
        #endregion
        
        #region	Protected Variables
        
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected FXTabControl tabControl;

        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Drawing.Point padding;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected bool hotTrack;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected TabStyle style = TabStyle.Default;
        
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected ContentAlignment imageAlign;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected int radius = 1;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected int overlap;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected bool focusTrack;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected float opacity = 1;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected bool showTabCloser;
        
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color borderColorSelected = SystemColors.ActiveBorder;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color borderColor = SystemColors.ControlDark;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color borderColorHot = SystemColors.ControlDark;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        
        protected Color closerColorActive = SystemColors.ActiveBorder;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color closerColor = SystemColors.ControlDark;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        
        protected Color focusColor = Color.Empty;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        
        protected Color textColor = SystemColors.ControlText;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color textColorSelected = SystemColors.HighlightText;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color textColorDisabled = SystemColors.GrayText;

        protected Color backColor = SystemColors.ButtonFace;
        protected Color backColorSelected = SystemColors.Control;
        protected Color backColorHot = SystemColors.ControlDarkDark;

        #endregion

        #region Overridable Methods

        public abstract void AddTabBorder(GraphicsPath path, Rectangle tabBounds);

        public virtual Rectangle GetTabRect(int index)
        {
            if (index < 0)
                return Rectangle.Empty;

            var tabBounds = tabControl.GetTabRect(index);
            if (tabControl.RightToLeftLayout)
                tabBounds.X = tabControl.Width - tabBounds.Right;

            bool firstTabinRow = tabControl.IsFirstTabInRow(index);
            
            // Expand to overlap the tabpage
            switch (tabControl.Alignment)
            {
                case TabAlignment.Top:
                    tabBounds.Height += 2;
                    break;
                case TabAlignment.Bottom:
                    tabBounds.Height += 2;
                    tabBounds.Y -= 2;
                    break;
                case TabAlignment.Left:
                    tabBounds.Width += 2;
                    break;
                case TabAlignment.Right:
                    tabBounds.X -= 2;
                    tabBounds.Width += 2;
                    break;
            }
            
            // Greate Overlap unless first tab in the row to align with tabpage
            if ((!firstTabinRow || tabControl.RightToLeftLayout) && overlap > 0)
            {
                if (tabControl.Alignment <= TabAlignment.Bottom)
                {
                    tabBounds.X -= overlap;
                    tabBounds.Width += overlap;
                }
                else
                {
                    tabBounds.Y -= overlap;
                    tabBounds.Height += overlap;
                }
            }

            // Adjust first tab in the row to align with tabpage
            EnsureFirstTabIsInView(ref tabBounds, index);

            return tabBounds;
        }
        
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
        protected virtual void EnsureFirstTabIsInView(ref Rectangle tabBounds, int index)
        {
            // Adjust first tab in the row to align with tabpage
            // Make sure we only reposition visible tabs, as we may have scrolled out of view.

            if (!tabControl.IsFirstTabInRow(index))
                return;
            
            if (tabControl.Alignment <= TabAlignment.Bottom)
            {
                if (tabControl.RightToLeftLayout)
                {
                    if (tabBounds.Left < tabControl.Right)
                    {
                        int tabPageRight = tabControl.GetPageBounds(index).Right;
                        if (tabBounds.Right > tabPageRight)
                            tabBounds.Width -= (tabBounds.Right - tabPageRight);
                    }
                }
                else
                {
                    if (tabBounds.Right > 0)
                    {
                        int tabPageX = tabControl.GetPageBounds(index).X;
                        if (tabBounds.X < tabPageX)
                        {
                            tabBounds.Width -= (tabPageX - tabBounds.X);
                            tabBounds.X = tabPageX;
                        }
                    }
                }
            }
            else
            {
                if (tabControl.RightToLeftLayout)
                {
                    if (tabBounds.Top < tabControl.Bottom)
                    {
                        int tabPageBottom = tabControl.GetPageBounds(index).Bottom;
                        if (tabBounds.Bottom > tabPageBottom)
                            tabBounds.Height -= (tabBounds.Bottom - tabPageBottom);
                    }
                }
                else
                {
                    if (tabBounds.Bottom > 0)
                    {
                        int tabPageY = tabControl.GetPageBounds(index).Location.Y;
                        if (tabBounds.Y < tabPageY)
                        {
                            tabBounds.Height -= (tabPageY - tabBounds.Y);
                            tabBounds.Y = tabPageY;
                        }
                    }
                }
            }
        }

        protected virtual Brush GetTabBackgroundBrush(int index)
        {
            LinearGradientBrush brush = null;

            // Capture the colours dependant on selection state of the tab
            var start = Color.Transparent;
            var end = Color.Transparent;

            if (tabControl.SelectedIndex == index)
            {
                start = backColorSelected;
                end = backColor;
            }
            else if (HotTrack && index == tabControl.ActiveIndex)
                end = start = backColorHot;

            // Get the correctly aligned gradient
            var tabBounds = GetTabRect(index);
            tabBounds.Inflate(1, 1);
            switch (tabControl.Alignment)
            {
                case TabAlignment.Top:
                    brush = new LinearGradientBrush(tabBounds, start, end, LinearGradientMode.Vertical);
                    break;
                case TabAlignment.Bottom:
                    brush = new LinearGradientBrush(tabBounds, end, start, LinearGradientMode.Vertical);
                    break;
                case TabAlignment.Left:
                    brush = new LinearGradientBrush(tabBounds, start, end, LinearGradientMode.Horizontal);
                    break;
                case TabAlignment.Right:
                    brush = new LinearGradientBrush(tabBounds, end, start, LinearGradientMode.Horizontal);
                    break;
            }

            return brush;
        }

        #endregion

        #region	Base Properties

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TabStyle DisplayStyle
        {
            get { return style; }
            set { style = value; }
        }

        [Category("Appearance")]
        public ContentAlignment ImageAlign
        {
            get { return imageAlign; }
            set {
                imageAlign = value;
                tabControl.Invalidate();
            }
        }
        
        [Category("Appearance")]
        public Drawing.Point Padding
        {
            get { return padding; }
            set {
                padding = value;
                // This line will trigger the handle to recreate, therefore invalidating the control
                if (showTabCloser)
                {
                    if (value.X + radius / 2 < -6)
                        ((TabControl)tabControl).Padding = new Drawing.Point(0, value.Y);
                    else
                        ((TabControl)tabControl).Padding = new Drawing.Point(value.X + radius / 2 + 6, value.Y);
                }
                else
                {
                    if (value.X + radius / 2 < 1)
                        ((TabControl)tabControl).Padding = new Drawing.Point(0, value.Y);
                    else
                        ((TabControl)tabControl).Padding = new Drawing.Point(value.X + radius / 2 - 1, value.Y);
                }
            }
        }
        
        [Category("Appearance"), DefaultValue(1), Browsable(true)]
        public int Radius
        {
            get { return radius; }
            set {
                radius = Math.Max(1, value);
                Padding = padding;
            }
        }

        [Category("Appearance")]
        public int Overlap
        {
            get { return overlap; }
            set { overlap = Math.Max(0, value); }
        }
        
        [Category("Appearance")]
        public bool FocusTrack
        {
            get { return focusTrack; }
            set {
                focusTrack = value;
                tabControl.Invalidate();
            }
        }
        
        [Category("Appearance")]
        public bool HotTrack
        {
            get { return hotTrack; }
            set {
                hotTrack = value;
                ((TabControl)tabControl).HotTrack = value;
            }
        }

        [Category("Appearance")]
        public bool ShowTabCloser
        {
            get { return showTabCloser; }
            set {
                showTabCloser = value;
                Padding = padding;
            }
        }

        [Category("Appearance")]
        public float Opacity
        {
            get { return opacity; }
            set {
                opacity = Math.Min(1, Math.Max(0, value));
                tabControl.Invalidate();
            }
        }
        
        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BorderColorSelected
        {
            get {
                return borderColorSelected;
            }
            set {
                borderColorSelected = value;
                BorderColorSelectedPen?.Dispose();
                BorderColorSelectedPen = new Pen(borderColorSelected);
                tabControl.Invalidate();
            }
        }

        public Pen BorderColorSelectedPen { get; set; } = new Pen(SystemColors.ActiveBorder);

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BorderColorHot
        {
            get { return borderColorHot; }
            set {
                borderColorHot = value;
                BorderColorHotPen?.Dispose();
                BorderColorHotPen = new Pen(borderColorHot);
                tabControl.Invalidate();
            }
        }

        public Pen BorderColorHotPen { get; set; } = new Pen(SystemColors.ControlDark);

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BorderColor
        {
            get { return borderColor; }
            set {
                borderColor = value;
                BorderColorPen?.Dispose();
                BorderColorPen = new Pen(borderColor);
                tabControl.Invalidate();
            }
        }

        public Pen BorderColorPen { get; set; } = new Pen(SystemColors.ControlDark);

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColor
        {
            get { return textColor; }
            set {
                textColor = value;
                TextColorBrush?.Dispose();
                TextColorBrush = new SolidBrush(textColor);
                tabControl.Invalidate();
            }
        }

        public Brush TextColorBrush { get; set; } = new SolidBrush(SystemColors.ControlText);

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColorSelected
        {
            get { return textColorSelected; }
            set {
                textColorSelected = value;
                TextColorSelectedBrush?.Dispose();
                TextColorSelectedBrush = new SolidBrush(textColorSelected);
                tabControl.Invalidate();
            }
        }

        public Brush TextColorSelectedBrush { get; set; } = new SolidBrush(SystemColors.HighlightText);

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColorDisabled
        {
            get { return textColor; }
            set {
                textColorDisabled = value;
                TextColorDisabledBrush?.Dispose();
                TextColorDisabledBrush = new SolidBrush(textColorDisabled);
                tabControl.Invalidate();
            }
        }

        public Brush TextColorDisabledBrush { get; set; } = new SolidBrush(SystemColors.InactiveCaptionText);

        [Category("Appearance"), DefaultValue(typeof(Color), "Orange")]
        public Color FocusColor
        {
            get { return focusColor; }
            set {
                focusColor = value;
                FocusColorBrush?.Dispose();
                FocusColorBrush = new SolidBrush(focusColor);
                tabControl.Invalidate();
            }
        }

        protected Brush FocusColorBrush { get; set; }

        [Category("Appearance"), DefaultValue(typeof(Color), "Black")]
        public Color CloserColorActive
        {
            get { return closerColorActive; }
            set {
                closerColorActive = value;
                CloserColorActivePen?.Dispose();
                CloserColorActivePen = new Pen(closerColorActive);
                tabControl.Invalidate();
            }
        }

        public Pen CloserColorActivePen { get; set; }

        [Category("Appearance"), DefaultValue(typeof(Color), "DarkGrey")]
        public Color CloserColor
        {
            get { return closerColor; }
            set {
                closerColor = value;
                CloserColorPen?.Dispose();
                CloserColorPen = new Pen(closerColor);
                tabControl.Invalidate();
            }
        }

        public Pen CloserColorPen { get; set; }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BackColor
        {
            get { return backColor; }
            set
            {
                backColor = value;
                BackColorBrush?.Dispose();
                BackColorBrush = new SolidBrush(backColor);
                tabControl.Invalidate();
            }
        }

        protected Brush BackColorBrush { get; set; }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BackColorSelected
        {
            get { return backColorSelected; }
            set
            {
                backColorSelected = value;
                BackColorSelectedBrush?.Dispose();
                BackColorSelectedBrush = new SolidBrush(backColorSelected);
                tabControl.Invalidate();
            }
        }

        protected Brush BackColorSelectedBrush { get; set; }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BackColorHot
        {
            get { return backColorHot; }
            set
            {
                backColorHot = value;
                BackColorHotBrush?.Dispose();
                BackColorHotBrush = new SolidBrush(backColorHot);
                tabControl.Invalidate();
            }
        }

        protected Brush BackColorHotBrush { get; set; }

        #endregion

        #region Painting

        public void PaintTab(int index, Graphics graphics)
        {
            using (var tabpath = GetTabBorder(index))
            {
                using (var fillBrush = GetTabBackgroundBrush(index))
                {
                    // Paint the background
                    graphics.FillPath(fillBrush, tabpath);

                    // Paint a focus indication
                    if (tabControl.Focused)
                        DrawTabFocusIndicator(tabpath, index, graphics);

                    // Paint the closer
                    DrawTabCloser(index, graphics);
                }
            }
        }
        
        protected virtual void DrawTabCloser(int index, Graphics graphics)
        {
            if (!showTabCloser)
                return;

            var closerRect = tabControl.GetTabCloserRect(index);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (var closerPath = GetCloserPath(closerRect))
            {
                var pen = closerRect.Contains(tabControl.MousePosition)
                    ? CloserColorActivePen : CloserColorPen;
                graphics.DrawPath(pen, closerPath);
            }

        }
        
        protected static GraphicsPath GetCloserPath(Rectangle closerRect)
        {
            var closerPath = new GraphicsPath();
            closerPath.AddLine(closerRect.X, closerRect.Y, closerRect.Right, closerRect.Bottom);
            closerPath.CloseFigure();
            closerPath.AddLine(closerRect.Right, closerRect.Y, closerRect.X, closerRect.Bottom);
            closerPath.CloseFigure();
            return closerPath;
        }
        
        private void DrawTabFocusIndicator(GraphicsPath tabpath, int index, Graphics graphics)
        {
            if (focusTrack && tabControl.Focused && index == tabControl.SelectedIndex)
            {
                Brush focusBrush = null;
                RectangleF pathRect = tabpath.GetBounds();
                Rectangle focusRect = Rectangle.Empty;
                switch (tabControl.Alignment)
                {
                    case TabAlignment.Top:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Y, (int)pathRect.Width, 4);
                        focusBrush = new LinearGradientBrush(focusRect, focusColor, SystemColors.Window, LinearGradientMode.Vertical);
                        break;
                    case TabAlignment.Bottom:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Bottom - 4, (int)pathRect.Width, 4);
                        focusBrush = new LinearGradientBrush(focusRect, SystemColors.ControlLight, focusColor, LinearGradientMode.Vertical);
                        break;
                    case TabAlignment.Left:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Y, 4, (int)pathRect.Height);
                        focusBrush = new LinearGradientBrush(focusRect, focusColor, SystemColors.ControlLight, LinearGradientMode.Horizontal);
                        break;
                    case TabAlignment.Right:
                        focusRect = new Rectangle((int)pathRect.Right - 4, (int)pathRect.Y, 4, (int)pathRect.Height);
                        focusBrush = new LinearGradientBrush(focusRect, SystemColors.ControlLight, focusColor, LinearGradientMode.Horizontal);
                        break;
                }
                
                // Ensure the focus stip does not go outside the tab
                Region focusRegion = new Region(focusRect);
                focusRegion.Intersect(tabpath);
                graphics.FillRegion(focusBrush, focusRegion);
                focusRegion.Dispose();
                focusBrush.Dispose();
            }
        }

        #endregion
        
        #region Background brushes

        private Blend GetBackgroundBlend()
        {
            float[] relativeIntensities = new float[]{0f, 0.7f, 1f};
            float[] relativePositions = new float[]{0f, 0.6f, 1f};

            // Glass look to top aligned tabs
            if (tabControl.Alignment == TabAlignment.Top)
            {
                relativeIntensities = new float[]{0f, 0.5f, 1f, 1f};
                relativePositions = new float[]{0f, 0.5f, 0.51f, 1f};
            }
            
            Blend blend = new Blend();
            blend.Factors = relativeIntensities;
            blend.Positions = relativePositions;
            
            return blend;
        }
        
        public virtual Brush GetPageBackgroundBrush(int index)
            => tabControl.SelectedIndex == index ? BackColorBrush : Brushes.Transparent;

        #endregion

        #region Tab border and rect

        public GraphicsPath GetTabBorder(int index)
        {
            var path = new GraphicsPath();
            var tabBounds = GetTabRect(index);
            
            AddTabBorder(path, tabBounds);
            
            path.CloseFigure();
            return path;
        }

        #endregion
        
    }
}
