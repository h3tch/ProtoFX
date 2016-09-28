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
        Dark,
    }

    [System.ComponentModel.ToolboxItem(false)]
    public abstract class TabStyleProvider : Component
    {
        #region Constructor
        
        protected TabStyleProvider(FXTabControl tabControl){
            _TabControl = tabControl;
            
            _BorderColor = Color.Empty;
            _BorderColorSelected = Color.Empty;
            _FocusColor = Color.Orange;
            
            _ImageAlign = _TabControl.RightToLeftLayout
                ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft;
            
            HotTrack = true;
            
            //	Must set after the _Overlap as this is used in the calculations of the actual padding
            Padding = new Drawing.Point(6 + (ShowTabCloser ? 6 : 0), 3);
        }
        
        #endregion

        #region Factory Methods
        
        public static TabStyleProvider CreateProvider(FXTabControl tabControl){
            TabStyleProvider provider;
            
            //	Depending on the display style of the tabControl generate an appropriate provider.
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
                case TabStyle.Dark:
                    provider = new FXTabStyleProvider(tabControl);
                    break;
                default:
                    provider = new TabStyleDefaultProvider(tabControl);
                    break;
            }
            
            provider._Style = tabControl.DisplayStyle;
            return provider;
        }
        
        #endregion
        
        #region	Protected variables
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected FXTabControl _TabControl;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Drawing.Point _Padding;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected bool _HotTrack;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected TabStyle _Style = TabStyle.Default;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected ContentAlignment _ImageAlign;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected int _Radius = 1;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected int _Overlap;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected bool _FocusTrack;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected float _Opacity = 1;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected bool _ShowTabCloser;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color _BorderColorSelected = Color.Empty;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color _BorderColor = Color.Empty;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color _BorderColorHot = Color.Empty;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        
        protected Color _CloserColorActive = Color.Black;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color _CloserColor = Color.DarkGray;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        
        protected Color _FocusColor = Color.Empty;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        
        protected Color _TextColor = Color.Empty;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color _TextColorSelected = Color.Empty;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color _TextColorDisabled = Color.Empty;

        protected Color _BackColor = SystemColors.ButtonFace;
        protected Color _BackColorSelected = SystemColors.Control;
        protected Color _BackColorHot = SystemColors.ControlDarkDark;
        #endregion

        #region overridable Methods

        public abstract void AddTabBorder(GraphicsPath path, Rectangle tabBounds);

        public virtual Rectangle GetTabRect(int index)
        {
            
            if (index < 0)
                return new Rectangle();

            var tabBounds = _TabControl.GetTabRect(index);
            if (_TabControl.RightToLeftLayout)
                tabBounds.X = _TabControl.Width - tabBounds.Right;

            bool firstTabinRow = _TabControl.IsFirstTabInRow(index);
            
            //	Expand to overlap the tabpage
            switch (_TabControl.Alignment)
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
            

            //	Greate Overlap unless first tab in the row to align with tabpage
            if ((!firstTabinRow || _TabControl.RightToLeftLayout) && _Overlap > 0)
            {
                if (_TabControl.Alignment <= TabAlignment.Bottom)
                {
                    tabBounds.X -= _Overlap;
                    tabBounds.Width += _Overlap;
                }
                else
                {
                    tabBounds.Y -= _Overlap;
                    tabBounds.Height += _Overlap;
                }
            }

            //	Adjust first tab in the row to align with tabpage
            EnsureFirstTabIsInView(ref tabBounds, index);

            return tabBounds;
        }
        
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
        protected virtual void EnsureFirstTabIsInView(ref Rectangle tabBounds, int index)
        {
            //	Adjust first tab in the row to align with tabpage
            //	Make sure we only reposition visible tabs, as we may have scrolled out of view.
            
            bool firstTabinRow = _TabControl.IsFirstTabInRow(index);

            if (firstTabinRow)
            {
                if (_TabControl.Alignment <= TabAlignment.Bottom)
                {
                    if (_TabControl.RightToLeftLayout)
                    {
                        if (tabBounds.Left < _TabControl.Right)
                        {
                            int tabPageRight = _TabControl.GetPageBounds(index).Right;
                            if (tabBounds.Right > tabPageRight)
                                tabBounds.Width -= (tabBounds.Right - tabPageRight);
                        }
                    }
                    else
                    {
                        if (tabBounds.Right > 0)
                        {
                            int tabPageX = _TabControl.GetPageBounds(index).X;
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
                    if (_TabControl.RightToLeftLayout)
                    {
                        if (tabBounds.Top < _TabControl.Bottom)
                        {
                            int tabPageBottom = _TabControl.GetPageBounds(index).Bottom;
                            if (tabBounds.Bottom > tabPageBottom)
                                tabBounds.Height -= (tabBounds.Bottom - tabPageBottom);
                        }
                    }
                    else
                    {
                        if (tabBounds.Bottom > 0)
                        {
                            int tabPageY = _TabControl.GetPageBounds(index).Location.Y;
                            if (tabBounds.Y < tabPageY)
                            {
                                tabBounds.Height -= (tabPageY - tabBounds.Y);
                                tabBounds.Y = tabPageY;
                            }
                        }
                    }
                }
            }
        }

        protected virtual Brush GetTabBackgroundBrush(int index)
        {
            LinearGradientBrush fillBrush = null;

            //	Capture the colours dependant on selection state of the tab
            var dark = Color.FromArgb(207, 207, 207);
            var light = Color.FromArgb(242, 242, 242);
            
            if (_TabControl.SelectedIndex == index)
            {
                dark = SystemColors.ControlLight;
                light = SystemColors.Window;
            }
            else if (!_TabControl.TabPages[index].Enabled)
            {
                light = dark;
            }
            else if (_HotTrack && index == _TabControl.ActiveIndex)
            {
                //	Enable hot tracking
                light = Color.FromArgb(234, 246, 253);
                dark = Color.FromArgb(167, 217, 245);
            }
            
            //	Get the correctly aligned gradient
            var tabBounds = GetTabRect(index);
            tabBounds.Inflate(3,3);
            tabBounds.X -= 1;
            tabBounds.Y -= 1;
            switch (_TabControl.Alignment)
            {
                case TabAlignment.Top:
                    if (_TabControl.SelectedIndex == index)
                        dark = light;
                    fillBrush = new LinearGradientBrush(tabBounds, light, dark, LinearGradientMode.Vertical);
                    break;
                case TabAlignment.Bottom:
                    fillBrush = new LinearGradientBrush(tabBounds, light, dark, LinearGradientMode.Vertical);
                    break;
                case TabAlignment.Left:
                    fillBrush = new LinearGradientBrush(tabBounds, dark, light, LinearGradientMode.Horizontal);
                    break;
                case TabAlignment.Right:
                    fillBrush = new LinearGradientBrush(tabBounds, light, dark, LinearGradientMode.Horizontal);
                    break;
            }
            
            //	Add the blend
            fillBrush.Blend = GetBackgroundBlend();
            
            return fillBrush;
        }

        #endregion
        
        #region	Base Properties

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TabStyle DisplayStyle
        {
            get { return _Style; }
            set { _Style = value; }
        }

        [Category("Appearance")]
        public ContentAlignment ImageAlign
        {
            get { return _ImageAlign; }
            set {
                _ImageAlign = value;
                _TabControl.Invalidate();
            }
        }
        
        [Category("Appearance")]
        public Drawing.Point Padding
        {
            get { return _Padding; }
            set {
                _Padding = value;
                //	This line will trigger the handle to recreate, therefore invalidating the control
                if (_ShowTabCloser)
                {
                    if (value.X + _Radius / 2 < -6)
                        ((TabControl)_TabControl).Padding = new Drawing.Point(0, value.Y);
                    else
                        ((TabControl)_TabControl).Padding = new Drawing.Point(value.X + _Radius / 2 + 6, value.Y);
                }
                else
                {
                    if (value.X + _Radius / 2 < 1)
                        ((TabControl)_TabControl).Padding = new Drawing.Point(0, value.Y);
                    else
                        ((TabControl)_TabControl).Padding = new Drawing.Point(value.X + _Radius / 2 - 1, value.Y);
                }
            }
        }
        
        [Category("Appearance"), DefaultValue(1), Browsable(true)]
        public int Radius
        {
            get { return _Radius; }
            set {
                _Radius = Math.Max(1, value);
                Padding = _Padding;
            }
        }

        [Category("Appearance")]
        public int Overlap
        {
            get { return _Overlap; }
            set { _Overlap = Math.Max(0, value); }
        }
        
        [Category("Appearance")]
        public bool FocusTrack
        {
            get { return _FocusTrack; }
            set {
                _FocusTrack = value;
                _TabControl.Invalidate();
            }
        }
        
        [Category("Appearance")]
        public bool HotTrack
        {
            get { return _HotTrack; }
            set {
                _HotTrack = value;
                ((TabControl)_TabControl).HotTrack = value;
            }
        }

        [Category("Appearance")]
        public bool ShowTabCloser
        {
            get { return _ShowTabCloser; }
            set {
                _ShowTabCloser = value;
                Padding = _Padding;
            }
        }

        [Category("Appearance")]
        public float Opacity
        {
            get { return _Opacity; }
            set {
                _Opacity = Math.Min(1, Math.Max(0, value));
                _TabControl.Invalidate();
            }
        }
        
        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BorderColorSelected
        {
            get {
                return _BorderColorSelected.IsEmpty ? SystemColors.ActiveBorder : _BorderColorSelected;
            }
            set {
                _BorderColorSelected = value.Equals(SystemColors.ActiveBorder) ? Color.Empty : value;
                _TabControl.Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BorderColorHot
        {
            get { return _BorderColorHot.IsEmpty ? SystemColors.ControlDark : _BorderColorHot; }
            set {
                _BorderColorHot = value.Equals(SystemColors.ControlDark) ? Color.Empty : value;
                _TabControl.Invalidate();
            }
        }
        
        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BorderColor
        {
            get { return _BorderColor.IsEmpty ? SystemColors.ControlDark : _BorderColor; }
            set {
                _BorderColor = value.Equals(SystemColors.ControlDark) ? Color.Empty : value;
                _TabControl.Invalidate();
            }
        }
        
        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColor
        {
            get { return _TextColor.IsEmpty ? SystemColors.ControlText : _TextColor; }
            set {
                _TextColor = value.Equals(SystemColors.ControlText) ? Color.Empty : value;
                _TabControl.Invalidate();
            }
        }
        
        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColorSelected
        {
            get {
                return _TextColorSelected.IsEmpty ? SystemColors.ControlText : _TextColorSelected;
            }
            set {
                _TextColorSelected = value.Equals(SystemColors.ControlText) ? Color.Empty : value;
                _TabControl.Invalidate();
            }
        }
        
        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColorDisabled
        {
            get { return _TextColor.IsEmpty ? SystemColors.ControlDark : _TextColor; }
            set {
                _TextColorDisabled = value.Equals(SystemColors.ControlDark) ? Color.Empty : value;
                _TabControl.Invalidate();
            }
        }
        
        [Category("Appearance"), DefaultValue(typeof(Color), "Orange")]
        public Color FocusColor
        {
            get { return _FocusColor; }
            set { _FocusColor = value; _TabControl.Invalidate(); }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "Black")]
        public Color CloserColorActive
        {
            get { return _CloserColorActive; }
            set { _CloserColorActive = value; _TabControl.Invalidate(); }
        }
        
        [Category("Appearance"), DefaultValue(typeof(Color), "DarkGrey")]
        public Color CloserColor
        {
            get { return _CloserColor; }
            set { _CloserColor = value; _TabControl.Invalidate(); }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BackColor
        {
            get { return _BackColor.IsEmpty ? SystemColors.ControlDark : _BackColor; }
            set
            {
                _BackColor = value.Equals(SystemColors.ControlDark) ? Color.Empty : value;
                _TabControl.Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BackColorSelected
        {
            get { return _BackColorSelected.IsEmpty ? SystemColors.ControlDark : _BackColorSelected; }
            set
            {
                _BackColorSelected = value.Equals(SystemColors.ControlDark) ? Color.Empty : value;
                _TabControl.Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BackColorHot
        {
            get { return _BackColorHot.IsEmpty ? SystemColors.ControlDark : _BackColorHot; }
            set
            {
                _BackColorHot = value.Equals(SystemColors.ControlDark) ? Color.Empty : value;
                _TabControl.Invalidate();
            }
        }

        #endregion

        #region Painting

        public void PaintTab(int index, Graphics graphics)
        {
            using (GraphicsPath tabpath = GetTabBorder(index))
            {
                using (Brush fillBrush = GetTabBackgroundBrush(index))
                {
                    //	Paint the background
                    graphics.FillPath(fillBrush, tabpath);

                    //	Paint a focus indication
                    if (_TabControl.Focused)
                        DrawTabFocusIndicator(tabpath, index, graphics);

                    //	Paint the closer
                    DrawTabCloser(index, graphics);
                }
            }
        }
        
        protected virtual void DrawTabCloser(int index, Graphics graphics)
        {
            if (!_ShowTabCloser)
                return;

            var closerRect = _TabControl.GetTabCloserRect(index);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var closerPath = GetCloserPath(closerRect))
            {
                if (closerRect.Contains(_TabControl.MousePosition))
                {
                    using (var closerPen = new Pen(_CloserColorActive))
                    {
                        graphics.DrawPath(closerPen, closerPath);
                    }
                }
                else
                {
                    using (var closerPen = new Pen(_CloserColor))
                    {
                        graphics.DrawPath(closerPen, closerPath);
                    }
                }
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
            if (_FocusTrack && _TabControl.Focused && index == _TabControl.SelectedIndex)
            {
                Brush focusBrush = null;
                RectangleF pathRect = tabpath.GetBounds();
                Rectangle focusRect = Rectangle.Empty;
                switch (_TabControl.Alignment)
                {
                    case TabAlignment.Top:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Y, (int)pathRect.Width, 4);
                        focusBrush = new LinearGradientBrush(focusRect, _FocusColor, SystemColors.Window, LinearGradientMode.Vertical);
                        break;
                    case TabAlignment.Bottom:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Bottom - 4, (int)pathRect.Width, 4);
                        focusBrush = new LinearGradientBrush(focusRect, SystemColors.ControlLight, _FocusColor, LinearGradientMode.Vertical);
                        break;
                    case TabAlignment.Left:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Y, 4, (int)pathRect.Height);
                        focusBrush = new LinearGradientBrush(focusRect, _FocusColor, SystemColors.ControlLight, LinearGradientMode.Horizontal);
                        break;
                    case TabAlignment.Right:
                        focusRect = new Rectangle((int)pathRect.Right - 4, (int)pathRect.Y, 4, (int)pathRect.Height);
                        focusBrush = new LinearGradientBrush(focusRect, SystemColors.ControlLight, _FocusColor, LinearGradientMode.Horizontal);
                        break;
                }
                
                //	Ensure the focus stip does not go outside the tab
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

            //	Glass look to top aligned tabs
            if (_TabControl.Alignment == TabAlignment.Top)
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
        {
            //	Capture the colours dependant on selection state of the tab
            var light = Color.FromArgb(242, 242, 242);

            if (_TabControl.Alignment == TabAlignment.Top)
                light = Color.FromArgb(207, 207, 207);
            
            if (_TabControl.SelectedIndex == index)
                light = SystemColors.Window;
            else if (!_TabControl.TabPages[index].Enabled)
                light = Color.FromArgb(207, 207, 207);
            else if (_HotTrack && index == _TabControl.ActiveIndex)
                //	Enable hot tracking
                light = Color.FromArgb(234, 246, 253);
            
            return new SolidBrush(light);
        }
        
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
