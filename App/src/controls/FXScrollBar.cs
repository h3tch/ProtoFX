using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Diagnostics;


namespace CustomControls
{

    [Designer(typeof(ScrollbarControlDesigner))]
    public class CustomScrollbar : UserControl {

        protected Color moChannelColor = Color.Empty;
        protected int UpH = 15;
        protected int UpW = 15;
        protected int DownH = 15;
        protected int DownW = 15;
        protected int ThmpW = 15;
        protected int ThmpMidH = 15;
        protected int ThmpTopH = 5;
        protected int ThmpBtmH = 5;

        protected int moLargeChange = 10;
        protected int moSmallChange = 1;
        protected int moMinimum = 0;
        protected int moMaximum = 100;
        protected int moValue = 0;
        private int nClickPoint;

        protected int moThumbTop = 0;

        protected bool moAutoSize = false;

        private bool moThumbDown = false;
        private bool moThumbDragging = false;

        public new event EventHandler Scroll = null;
        public event EventHandler ValueChanged = null;

        private int GetThumbHeight()
        {
            int nTrackHeight = (this.Height - (UpH + DownH));
            float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
            int nThumbHeight = (int)fThumbHeight;

            if (nThumbHeight > nTrackHeight)
            {
                nThumbHeight = nTrackHeight;
                fThumbHeight = nTrackHeight;
            }
            if (nThumbHeight < 56)
            {
                nThumbHeight = 56;
                fThumbHeight = 56;
            }

            return nThumbHeight;
        }

        public CustomScrollbar() {

            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            moChannelColor = Color.FromArgb(51, 166, 3);

            this.Width = UpW;
            base.MinimumSize = new Size(UpW, UpH + UpH + GetThumbHeight());
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("LargeChange")]
        public int LargeChange {
            get { return moLargeChange; }
            set { moLargeChange = value;
            Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("SmallChange")]
        public int SmallChange {
            get { return moSmallChange; }
            set { moSmallChange = value;
            Invalidate();    
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("Minimum")]
        public int Minimum {
            get { return moMinimum; }
            set { moMinimum = value;
            Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("Maximum")]
        public int Maximum {
            get { return moMaximum; }
            set { moMaximum = value;
            Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("Value")]
        public int Value {
            get { return moValue; }
            set { moValue = value;

            int nTrackHeight = (this.Height - (UpH + DownH));
            float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
            int nThumbHeight = (int)fThumbHeight;

            if (nThumbHeight > nTrackHeight)
            {
                nThumbHeight = nTrackHeight;
                fThumbHeight = nTrackHeight;
            }
            if (nThumbHeight < 56)
            {
                nThumbHeight = 56;
                fThumbHeight = 56;
            }

            //figure out value
            int nPixelRange = nTrackHeight - nThumbHeight;
            int nRealRange = (Maximum - Minimum)-LargeChange;
            float fPerc = 0.0f;
            if (nRealRange != 0)
            {
                fPerc = (float)moValue / (float)nRealRange;
                
            }
            
            float fTop = fPerc * nPixelRange;
            moThumbTop = (int)fTop;
            

            Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Channel Color")]
        public Color ChannelColor
        {
            get { return moChannelColor; }
            set { moChannelColor = value; }
        }
        
        protected override void OnPaint(PaintEventArgs e) {

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            var s = Width;
            e.Graphics.FillRectangle(Brushes.Red, new Rectangle(new Point(0, 0), new Size(s, s)));
            
            Brush oBrush = new SolidBrush(moChannelColor);
            Brush oWhiteBrush = new SolidBrush(Color.FromArgb(255,255,255));
            
            //draw channel left and right border colors
            e.Graphics.FillRectangle(oWhiteBrush, new Rectangle(0, Width, 1, (this.Height-s)));
            e.Graphics.FillRectangle(oWhiteBrush, new Rectangle(this.Width-1, s, 1, (this.Height - s)));
            
            //draw channel
            e.Graphics.FillRectangle(oBrush, new Rectangle(1, s, this.Width-2, (this.Height-s)));

            //draw thumb
            int nTrackHeight = (this.Height - (s + s));
            float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
            int nThumbHeight = (int)fThumbHeight;

            if (nThumbHeight > nTrackHeight) {
                nThumbHeight = nTrackHeight;
                fThumbHeight = nTrackHeight;
            }
            if (nThumbHeight < 56) {
                nThumbHeight = 56;
                fThumbHeight = 56;
            }

            //Debug.WriteLine(nThumbHeight.ToString());
            float fSpanHeight = (fThumbHeight - (ThmpMidH + ThmpTopH + ThmpBtmH)) / 2.0f;
            int nSpanHeight = (int)fSpanHeight;

            int nTop = moThumbTop;
            nTop += s;

            //draw top
            e.Graphics.FillRectangle(Brushes.Red, new Rectangle(1, nTop, this.Width - 2, ThmpTopH));

            nTop += ThmpTopH;
            //draw top span
            Rectangle rect = new Rectangle(1, nTop, this.Width - 2, nSpanHeight);


            e.Graphics.FillRectangle(Brushes.Red, 1.0f,(float)nTop, (float)this.Width-2.0f, (float) fSpanHeight*2);

            nTop += nSpanHeight;
            //draw middle
            e.Graphics.FillRectangle(Brushes.Red, new Rectangle(1, nTop, this.Width - 2, ThmpMidH));


            nTop += ThmpMidH;
            //draw top span
            rect = new Rectangle(1, nTop, this.Width - 2, nSpanHeight*2);
            e.Graphics.FillRectangle(Brushes.Red, rect);

            nTop += nSpanHeight;
            //draw bottom
            e.Graphics.FillRectangle(Brushes.Red, new Rectangle(1, nTop, this.Width - 2, nSpanHeight));
            
            e.Graphics.FillRectangle(Brushes.Red, new Rectangle(new Point(0, (this.Height - s)), new Size(this.Width, s)));
            
        }

        public override bool AutoSize {
            get {
                return base.AutoSize;
            }
            set {
                base.AutoSize = value;
                if (base.AutoSize) {
                    this.Width = 15;
                }
            }
        }

        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // CustomScrollbar
            // 
            this.Name = "CustomScrollbar";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CustomScrollbar_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CustomScrollbar_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CustomScrollbar_MouseUp);
            this.ResumeLayout(false);

        }

        private void CustomScrollbar_MouseDown(object sender, MouseEventArgs e) {
            Point ptPoint = this.PointToClient(Cursor.Position);
            int nTrackHeight = (this.Height - (UpH + DownH));
            float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
            int nThumbHeight = (int)fThumbHeight;

            if (nThumbHeight > nTrackHeight) {
                nThumbHeight = nTrackHeight;
                fThumbHeight = nTrackHeight;
            }
            if (nThumbHeight < 56) {
                nThumbHeight = 56;
                fThumbHeight = 56;
            }

            int nTop = moThumbTop;
            nTop += UpH;


            Rectangle thumbrect = new Rectangle(new Point(1, nTop), new Size(ThmpW, nThumbHeight));
            if (thumbrect.Contains(ptPoint))
            {
                
                //hit the thumb
                nClickPoint = (ptPoint.Y - nTop);
                //MessageBox.Show(Convert.ToString((ptPoint.Y - nTop)));
                this.moThumbDown = true;
            }

            Rectangle uparrowrect = new Rectangle(new Point(1, 0), new Size(UpW, UpH));
            if (uparrowrect.Contains(ptPoint))
            {

                int nRealRange = (Maximum - Minimum)-LargeChange;
                int nPixelRange = (nTrackHeight - nThumbHeight);
                if (nRealRange > 0)
                {
                    if (nPixelRange > 0)
                    {
                        if ((moThumbTop - SmallChange) < 0)
                            moThumbTop = 0;
                        else
                            moThumbTop -= SmallChange;

                        //figure out value
                        float fPerc = (float)moThumbTop / (float)nPixelRange;
                        float fValue = fPerc * (Maximum - LargeChange);
                        
                            moValue = (int)fValue;
                        Debug.WriteLine(moValue.ToString());

                        if (ValueChanged != null)
                            ValueChanged(this, new EventArgs());

                        if (Scroll != null)
                            Scroll(this, new EventArgs());

                        Invalidate();
                    }
                }
            }

            Rectangle downarrowrect = new Rectangle(new Point(1, UpH + nTrackHeight), new Size(UpW, UpH));
            if (downarrowrect.Contains(ptPoint))
            {
                int nRealRange = (Maximum - Minimum) - LargeChange;
                int nPixelRange = (nTrackHeight - nThumbHeight);
                if (nRealRange > 0)
                {
                    if (nPixelRange > 0)
                    {
                        if ((moThumbTop + SmallChange) > nPixelRange)
                            moThumbTop = nPixelRange;
                        else
                            moThumbTop += SmallChange;

                        //figure out value
                        float fPerc = (float)moThumbTop / (float)nPixelRange;
                        float fValue = fPerc * (Maximum-LargeChange);
                       
                            moValue = (int)fValue;
                        Debug.WriteLine(moValue.ToString());

                        if (ValueChanged != null)
                            ValueChanged(this, new EventArgs());

                        if (Scroll != null)
                            Scroll(this, new EventArgs());

                        Invalidate();
                    }
                }
            }
        }

        private void CustomScrollbar_MouseUp(object sender, MouseEventArgs e) {
            this.moThumbDown = false;
            this.moThumbDragging = false;
        }

        private void MoveThumb(int y) {
            int nRealRange = Maximum - Minimum;
            int nTrackHeight = (this.Height - (UpH + DownH));
            float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
            int nThumbHeight = (int)fThumbHeight;

            if (nThumbHeight > nTrackHeight) {
                nThumbHeight = nTrackHeight;
                fThumbHeight = nTrackHeight;
            }
            if (nThumbHeight < 56) {
                nThumbHeight = 56;
                fThumbHeight = 56;
            }

            int nSpot = nClickPoint;

            int nPixelRange = (nTrackHeight - nThumbHeight);
            if (moThumbDown && nRealRange > 0) {
                if (nPixelRange > 0) {
                    int nNewThumbTop = y - (UpH + nSpot);
                    
                    if(nNewThumbTop<0)
                    {
                        moThumbTop = nNewThumbTop = 0;
                    }
                    else if(nNewThumbTop > nPixelRange)
                    {
                        moThumbTop = nNewThumbTop = nPixelRange;
                    }
                    else {
                        moThumbTop = y - (UpH + nSpot);
                    }
                   
                    //figure out value
                    float fPerc = (float)moThumbTop / (float)nPixelRange;
                    float fValue = fPerc * (Maximum-LargeChange);
                    moValue = (int)fValue;
                    Debug.WriteLine(moValue.ToString());

                    Application.DoEvents();

                    Invalidate();
                }
            }
        }

        private void CustomScrollbar_MouseMove(object sender, MouseEventArgs e) {
            if(moThumbDown == true)
            {
                this.moThumbDragging = true;
            }

            if (this.moThumbDragging) {

                MoveThumb(e.Y);
            }

            if(ValueChanged != null)
                ValueChanged(this, new EventArgs());

            if(Scroll != null)
                Scroll(this, new EventArgs());
        }

    }

    internal class ScrollbarControlDesigner : System.Windows.Forms.Design.ControlDesigner {

        

        public override SelectionRules SelectionRules {
            get {
                SelectionRules selectionRules = base.SelectionRules;
                PropertyDescriptor propDescriptor = TypeDescriptor.GetProperties(this.Component)["AutoSize"];
                if (propDescriptor != null) {
                    bool autoSize = (bool)propDescriptor.GetValue(this.Component);
                    if (autoSize) {
                        selectionRules = SelectionRules.Visible | SelectionRules.Moveable | SelectionRules.BottomSizeable | SelectionRules.TopSizeable;
                    }
                    else {
                        selectionRules = SelectionRules.Visible | SelectionRules.AllSizeable | SelectionRules.Moveable;
                    }
                }
                return selectionRules;
            }
        } 
    }
}