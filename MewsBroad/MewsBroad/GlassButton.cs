using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using PushButtonState = System.Windows.Forms.VisualStyles.PushButtonState;

namespace Mews.Svr.Broad
{
    /// <summary>
    /// Represents a glass button control.
    /// </summary>
    [ToolboxBitmap(typeof(GlassButton)), ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms"), Description("Raises an event when the user clicks it.")]
    public partial class GlassButton : Button
    {
        #region " Constructors "

        /// <summary>
        /// Initializes a new instance of the <see cref="Glass.GlassButton" /> class.
        /// </summary>
        public GlassButton()
        {
            InitializeComponent();
            timer.Interval = animationLength / framesCount;
            base.BackColor = Color.Transparent;
            BackColor = Color.Black;
            ForeColor = Color.White;            
            BorderColor = Color.Black;
            ShineColor = Color.White;
            GlowColor = Color.FromArgb(-7488001);//unchecked((int)(0xFF8DBDFF)));
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.Opaque, false);
        }

        #endregion

        #region " Fields and Properties "

        private Color backColor;
        /// <summary>
        /// Gets or sets the background color of the control.
        /// </summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> value representing the background color.</returns>
        [DefaultValue(typeof(Color), "Black")]
        public virtual new Color BackColor
        {
            get { return backColor; }
            set
            {
                if (!backColor.Equals(value))
                {
                    backColor = value;
                    UseVisualStyleBackColor = false;
                    CreateFrames();
                    OnBackColorChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control.</returns>
        [DefaultValue(typeof(Color), "White")]
        public virtual new Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
            }
        }

        private Color borderColor;
        /// <summary>
        /// Gets or sets the border color of the control.
        /// </summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> value representing the color of the border.</returns>
        [DefaultValue(typeof(Color), "Black"), Category("Appearance"), Description("The border color of the control.")]
        public virtual Color BorderColor
        {
            get { return borderColor; }
            set
            {
                if (borderColor != value)
                {
                    borderColor = value;
                    CreateFrames();
                    if (IsHandleCreated)
                    {
                        Invalidate();
                    }
                    OnBorderColorChanged(EventArgs.Empty);
                }
            }
        }        

        private Color shineColor;
        /// <summary>
        /// Gets or sets the shine color of the control.
        /// </summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> value representing the shine color.</returns>
        [DefaultValue(typeof(Color), "White"), Category("Appearance"), Description("The shine color of the control.")]
        public virtual Color ShineColor
        {
            get { return shineColor; }
            set
            {
                if (shineColor != value)
                {
                    shineColor = value;
                    CreateFrames();
                    if (IsHandleCreated)
                    {
                        Invalidate();
                    }
                    OnShineColorChanged(EventArgs.Empty);
                }
            }
        }

        private Color glowColor;
        /// <summary>
        /// Gets or sets the glow color of the control.
        /// </summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> value representing the glow color.</returns>
        [DefaultValue(typeof(Color), "255,141,189,255"), Category("Appearance"), Description("The glow color of the control.")]
        public virtual Color GlowColor
        {
            get { return glowColor; }
            set
            {
                if (glowColor != value)
                {
                    glowColor = value;
                    CreateFrames();
                    if (IsHandleCreated)
                    {
                        Invalidate();
                    }
                    OnGlowColorChanged(EventArgs.Empty);
                }
            }
        }

        private int radius = 4;
        /// <summary>
        /// Gets or set the radius for the button corner
        /// </summary>
        [DefaultValue(typeof(int), "4"), Category("Appearance"), Description("The radius for button corner")]
        public virtual int Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                CreateFrames();
                if (IsHandleCreated)
                {
                    Invalidate();
                }
            }
        }

        private bool isHovered;
        private bool isFocused;
        private bool isFocusedByKey;
        private bool isKeyDown;
        private bool isMouseDown;
        private bool isPressed { get { return isKeyDown || (isMouseDown && isHovered); } }

        /// <summary>
        /// Gets the state of the button control.
        /// </summary>
        /// <value>The state of the button control.</value>
        [Browsable(false)]
        public PushButtonState State
        {
            get
            {
                if (!Enabled)
                {
                    return PushButtonState.Disabled;
                }
                if (isPressed)
                {
                    return PushButtonState.Pressed;
                }
                if (isHovered)
                {
                    return PushButtonState.Hot;
                }
                if (isFocused || IsDefault)
                {
                    return PushButtonState.Default;
                }
                return PushButtonState.Normal;
            }
        }

        [Flags]
        public enum RoundCornerStyle
        {
            None = 0x00,
            LeftTop = 0x01,
            RightTop = 0x02,
            LeftBottom = 0x04,
            RightBottom = 0x08,
            All = 0x0F,
            LeftTop_RightBottom = 0x01 | 0x08,
            LeftBottom_RightTop = 0x04 | 0x02
        }

        private RoundCornerStyle rectRoundCS = RoundCornerStyle.All;

        /// <summary>
        /// Gets or set the corner style for text label
        /// </summary>
        [DefaultValue(typeof(RoundCornerStyle), "All"), Category("Appearance"), Description("The round corner style for the text label")]
        public virtual RoundCornerStyle RectRoundCS
        {
            get { return rectRoundCS; }
            set
            {
                rectRoundCS = value;
                CreateFrames();
                if (IsHandleCreated)
                {
                    Invalidate();
                }
            }
        }

        #endregion

        #region " Events "

        /// <summary>Occurs when the value of the <see cref="P:Glass.GlassButton.BorderColor" /> property changes.</summary>
        [Description("Event raised when the value of the BorderColor property is changed."), Category("Property Changed")]
        public event EventHandler BorderColorChanged;

        /// <summary>
        /// Raises the <see cref="E:Glass.GlassButton.BorderColorChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected virtual void OnBorderColorChanged(EventArgs e)
        {
            if (BorderColorChanged != null)
            {
                BorderColorChanged(this, e);
            }
        }        

        /// <summary>Occurs when the value of the <see cref="P:Glass.GlassButton.ShineColor" /> property changes.</summary>
        [Description("Event raised when the value of the ShineColor property is changed."), Category("Property Changed")]
        public event EventHandler ShineColorChanged;

        /// <summary>
        /// Raises the <see cref="E:Glass.GlassButton.ShineColorChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected virtual void OnShineColorChanged(EventArgs e)
        {
            if (ShineColorChanged != null)
            {
                ShineColorChanged(this, e);
            }
        }

        /// <summary>Occurs when the value of the <see cref="P:Glass.GlassButton.GlowColor" /> property changes.</summary>
        [Description("Event raised when the value of the GlowColor property is changed."), Category("Property Changed")]
        public event EventHandler GlowColorChanged;

        /// <summary>
        /// Raises the <see cref="E:Glass.GlassButton.GlowColorChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected virtual void OnGlowColorChanged(EventArgs e)
        {
            if (GlowColorChanged != null)
            {
                BorderColorChanged(this, e);
            }
        }

        #endregion

        #region " Overrided Methods "

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            CreateFrames();
            base.OnSizeChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            isKeyDown = isMouseDown = false;
            base.OnClick(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnEnter(EventArgs e)
        {
            isFocused = isFocusedByKey = true;
            base.OnEnter(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            isFocused = isFocusedByKey = isKeyDown = isMouseDown = false;
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnKeyUp(System.Windows.Forms.KeyEventArgs)" /> event.
        /// </summary>
        /// <param name="kevent">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs kevent)
        {
            if (kevent.KeyCode == Keys.Space)
            {
                isKeyDown = true;
                Invalidate();
            }
            base.OnKeyDown(kevent);
        }

        /// <summary>
        /// Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnKeyUp(System.Windows.Forms.KeyEventArgs)" /> event.
        /// </summary>
        /// <param name="kevent">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
        protected override void OnKeyUp(KeyEventArgs kevent)
        {
            if (isKeyDown && kevent.KeyCode == Keys.Space)
            {
                isKeyDown = false;
                Invalidate();
            }
            base.OnKeyUp(kevent);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!isMouseDown && e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                isFocusedByKey = false;
                Invalidate();
            }
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (isMouseDown)
            {
                isMouseDown = false;
                Invalidate();
            }
            base.OnMouseUp(e);
        }

        /// <summary>
        /// Raises the <see cref="M:System.Windows.Forms.Control.OnMouseMove(System.Windows.Forms.MouseEventArgs)" /> event.
        /// </summary>
        /// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
            if (mevent.Button != MouseButtons.None)
            {
                if (!ClientRectangle.Contains(mevent.X, mevent.Y))
                {
                    if (isHovered)
                    {
                        isHovered = false;
                        Invalidate();
                    }
                }
                else if (!isHovered)
                {
                    isHovered = true;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            FadeIn();
            Invalidate();
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            isHovered = false;
            FadeOut();
            Invalidate();
            base.OnMouseLeave(e);
        }

        #endregion

        #region " Painting "

        /// <summary>
        /// Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnPaint(System.Windows.Forms.PaintEventArgs)" /> event.
        /// </summary>
        /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            DrawButtonBackgroundFromBuffer(pevent.Graphics);
            DrawForegroundFromButton(pevent);
            //DrawButtonForeground(pevent.Graphics);
        }

        private void DrawButtonBackgroundFromBuffer(Graphics graphics)
        {
            int frame;
            if (!Enabled)
            {
                frame = FRAME_DISABLED;
            }
            else if (isPressed)
            {
                frame = FRAME_PRESSED;
            }
            else if (!isAnimating && currentFrame == 0)
            {
                frame = FRAME_NORMAL;
            }
            else
            {
                if (!HasAnimationFrames)
                {
                    CreateFrames(true);
                }
                frame = FRAME_ANIMATED + currentFrame;
            }
            if (frames == null)
            {
                CreateFrames();
            }
            graphics.DrawImage(frames[frame], Point.Empty);
        }

        public Image CreateBackgroundFrame(bool pressed, bool hovered,
            bool animating, bool enabled, float glowOpacity)
        {
            Rectangle rect = ClientRectangle;
            if (rect.Width <= 0)
            {
                rect.Width = 1;
            }
            if (rect.Height <= 0)
            {
                rect.Height = 1;
            }
            Image img = new Bitmap(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(img))
            {
                g.Clear(Color.Transparent);
                DrawButtonBackground(g, rect, pressed, hovered, animating, enabled,
                    backColor, glowColor, shineColor, borderColor,
                    glowOpacity, radius, rectRoundCS);
            }
            return img;
        }

        private static void DrawButtonBackground(Graphics g, Rectangle rectangle,
            bool pressed, bool hovered, bool animating, bool enabled,
            Color backColor, Color glowColor, Color shineColor,
            Color borderColor, float glowOpacity, int rad, RoundCornerStyle rcStyle)
        {
            SmoothingMode sm = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            #region " outter border "
            Rectangle rect = rectangle;
            rect.Width--;
            rect.Height--;            
            using (GraphicsPath bw = CreateRoundRectangle(rect, rad, rcStyle))
            {
                using (Pen p = new Pen(borderColor))
                {
                    g.DrawPath(p, bw);
                }
            }
            #endregion

            rect.X++;
            rect.Y++;
            rect.Width -= 2;
            rect.Height -= 2;
            Rectangle rect2 = rect;
            rect2.Height >>= 1;

            #region " content "            
            using (GraphicsPath bb = CreateRoundRectangle(rect, rad - 2, rcStyle))
            {
                /*
                int opacity = pressed ? 0x7f : 0xFF;
                using (Brush br = new SolidBrush(Color.FromArgb(opacity, backColor)))
                {
                    g.FillPath(br, bb);
                }
                */

                int opacity = 0xbb;

                // pky
                if (!enabled)
                {
                    opacity = 0xdd;
                    
                }
                else
                {
                    opacity = pressed ? 0x7f : 0xFF;
                    using (Brush br = new SolidBrush(Color.FromArgb(opacity, backColor)))
                    {
                        g.FillPath(br, bb);
                    }
                }

                using (Brush br = new SolidBrush(Color.FromArgb(opacity, backColor)))
                {
                    g.FillPath(br, bb);
                }

            }
            #endregion

            #region " glow "
            if ((hovered || animating) && !pressed)
            {                
                using (GraphicsPath clip = CreateRoundRectangle(rect, rad - 2, rcStyle))
                {
                    g.SetClip(clip, CombineMode.Intersect);
                    using (GraphicsPath brad = CreateBottomRadialPath(rect))
                    {
                        using (PathGradientBrush pgr = new PathGradientBrush(brad))
                        {
                            unchecked
                            {
                                int opacity = (int)(0xB2 * glowOpacity + .5f);
                                RectangleF bounds = brad.GetBounds();
                                pgr.CenterPoint = new PointF((bounds.Left + bounds.Right) / 2f, (bounds.Top + bounds.Bottom) / 2f);
                                pgr.CenterColor = Color.FromArgb(opacity, glowColor);
                                pgr.SurroundColors = new Color[] { Color.FromArgb(0, glowColor) };
                            }
                            g.FillPath(pgr, brad);
                        }
                    }
                    g.ResetClip();
                }
            }
            #endregion

            #region " shine "
            if (rect2.Width > 0 && rect2.Height > 0)
            {
                rect2.Height++;                
                using (GraphicsPath bh = CreateTopRoundRectangle(rect2, rad - 2, rcStyle))
                {
                    rect2.Height++;
                    int opacity = 0x99;

                           
                    // Disable 버튼인 경우 들어간 느낌 주기 위해 - pky
                    if (pressed | !enabled)
                    //if (pressed)
                    {
                       // opacity = (int)(.4f * opacity + .5f);
                       // opacity = (int)(.9f * opacity + .9f);
                        opacity = 0x33;
                    }
                                       
                    
                    using (LinearGradientBrush br = new LinearGradientBrush(rect2, Color.FromArgb(opacity, shineColor), Color.FromArgb(opacity / 3, shineColor), LinearGradientMode.Vertical))
                    {
                        g.FillPath(br, bh);
                    }
                   
                }
                rect2.Height -= 2;
            }
            #endregion

            #region " inner border "            
            using (GraphicsPath bb = CreateRoundRectangle(rect, rad - 1, rcStyle))
            {
                using (Pen p = new Pen(borderColor))
                {
                    g.DrawPath(p, bb);
                }
            }
            #endregion

            g.SmoothingMode = sm;
        }        

        private Button imageButton;
        private void DrawForegroundFromButton(PaintEventArgs pevent)
        {
            if (imageButton == null)
            {
                imageButton = new Button();
                imageButton.Parent = new TransparentControl();
                imageButton.BackColor = Color.Transparent;
                imageButton.FlatAppearance.BorderSize = 0;
                imageButton.FlatStyle = FlatStyle.Flat;
            }
            imageButton.AutoEllipsis = AutoEllipsis;
            if (Enabled)
            {
                imageButton.ForeColor = ForeColor;
            }
            else
            {
                // 선택된 버튼이 Disalbe 될 경우, forecolor 을 white 로 (선택된 단말이 눈에 잘 띄기 위해) - pky
                if (backColor.Name != "Black")
                {
                    imageButton.ForeColor = Color.White;
                }
                else
                {
                    imageButton.ForeColor = Color.FromArgb((2 * ForeColor.R + backColor.R) >> 2,
                        (2 * ForeColor.G + backColor.G) >> 2,
                        (2 * ForeColor.B + backColor.B) >> 2);
                }
            }
            imageButton.Font = Font;
            imageButton.RightToLeft = RightToLeft;
            imageButton.Image = Image;
            imageButton.ImageAlign = ImageAlign;
            imageButton.ImageIndex = ImageIndex;
            imageButton.ImageKey = ImageKey;
            imageButton.ImageList = ImageList;
            imageButton.Padding = Padding;
            imageButton.Size = Size;
            imageButton.Text = Text;
            imageButton.TextAlign = TextAlign;
            imageButton.TextImageRelation = TextImageRelation;
            imageButton.UseCompatibleTextRendering = UseCompatibleTextRendering;
            imageButton.UseMnemonic = UseMnemonic;
            InvokePaint(imageButton, pevent);
        }

        class TransparentControl : Control
        {
            protected override void OnPaintBackground(PaintEventArgs pevent) { }
            protected override void OnPaint(PaintEventArgs e) { }
        }

        private static GraphicsPath CreateRoundRectangle(Rectangle rectangle, int radius, RoundCornerStyle rcStyle)
        {
            GraphicsPath path = new GraphicsPath();

            int l = rectangle.Left;
            int t = rectangle.Top;
            int w = rectangle.Width;
            int h = rectangle.Height;

            if (radius <= 0)
            {
                path.AddLine(l, t, l + w, t); // top                
                path.AddLine(l + w, t, l + w, t + h); // right                
                path.AddLine(l + w, t + h, l, t + h); // bottom                
                path.AddLine(l, t + h, l, t); // left
            }
            else
            {
                int d = radius << 1;

                int lefttop_eff = 0;
                int leftbtm_eff = 0;
                int righttop_eff = 0;
                int rightbtm_eff = 0;
                int topleft_eff = 0;
                int topright_eff = 0;
                int btmleft_eff = 0;
                int btmright_eff = 0;

                if ((rcStyle & RoundCornerStyle.LeftTop) == RoundCornerStyle.LeftTop)
                {
                    lefttop_eff = radius;
                    topleft_eff = radius;
                }

                if ((rcStyle & RoundCornerStyle.RightTop) == RoundCornerStyle.RightTop)
                {
                    righttop_eff = radius;
                    topright_eff = radius;
                }

                if ((rcStyle & RoundCornerStyle.RightBottom) == RoundCornerStyle.RightBottom)
                {
                    btmright_eff = radius;
                    rightbtm_eff = radius;
                }

                if ((rcStyle & RoundCornerStyle.LeftBottom) == RoundCornerStyle.LeftBottom)
                {
                    leftbtm_eff = radius;
                    btmleft_eff = radius;
                }

                if ((rcStyle & RoundCornerStyle.LeftTop) == RoundCornerStyle.LeftTop) path.AddArc(l, t, d, d, 180, 90); // topleft                
                path.AddLine(l + lefttop_eff, t, l + w - righttop_eff, t); // top
                if ((rcStyle & RoundCornerStyle.RightTop) == RoundCornerStyle.RightTop) path.AddArc(l + w - d, t, d, d, 270, 90); // topright
                path.AddLine(l + w, t + topright_eff, l + w, t + h - btmright_eff); // right
                if ((rcStyle & RoundCornerStyle.RightBottom) == RoundCornerStyle.RightBottom) path.AddArc(l + w - d, t + h - d, d, d, 0, 90); // bottomright
                path.AddLine(l + w - rightbtm_eff, t + h, l + leftbtm_eff, t + h); // bottom
                if ((rcStyle & RoundCornerStyle.LeftBottom) == RoundCornerStyle.LeftBottom) path.AddArc(l, t + h - d, d, d, 90, 90); // bottomleft
                path.AddLine(l, t + h - btmleft_eff, l, t + topleft_eff); // left
            }

            path.CloseFigure();
            return path;
        }

        private static GraphicsPath CreateTopRoundRectangle(Rectangle rectangle, int radius, RoundCornerStyle rcStyle)
        {
            GraphicsPath path = new GraphicsPath();
            int l = rectangle.Left;
            int t = rectangle.Top;
            int w = rectangle.Width;
            int h = rectangle.Height;

            if (radius <= 0)
            {
                path.AddLine(l, t, l + w, t); // top                
                path.AddLine(l + w, t, l + w, t + h); // right                
                path.AddLine(l + w, t + h, l, t + h); // bottom                
                path.AddLine(l, t + h, l, t); // left
            }
            else
            {
                int d = radius << 1;

                int lefttop_eff = 0;
                int righttop_eff = 0;
                int topleft_eff = 0;
                int topright_eff = 0;

                if ((rcStyle & RoundCornerStyle.LeftTop) == RoundCornerStyle.LeftTop)
                {
                    lefttop_eff = radius;
                    topleft_eff = radius;
                }

                if ((rcStyle & RoundCornerStyle.RightTop) == RoundCornerStyle.RightTop)
                {
                    righttop_eff = radius;
                    topright_eff = radius;
                }

                if ((rcStyle & RoundCornerStyle.LeftTop) == RoundCornerStyle.LeftTop) path.AddArc(l, t, d, d, 180, 90); // topleft
                path.AddLine(l + lefttop_eff, t, l + w - righttop_eff, t); // top
                if ((rcStyle & RoundCornerStyle.RightTop) == RoundCornerStyle.RightTop) path.AddArc(l + w - d, t, d, d, 270, 90); // topright
                path.AddLine(l + w, t + topright_eff, l + w, t + h); // right
                path.AddLine(l + w, t + h, l, t + h); // bottom
                path.AddLine(l, t + h, l, t + topleft_eff); // left
            }

            path.CloseFigure();
            return path;
        }

        //private static GraphicsPath CreateRoundRectangle(Rectangle rectangle, int radius)
        //{
        //    GraphicsPath path = new GraphicsPath();
            
        //    int l = rectangle.Left;
        //    int t = rectangle.Top;
        //    int w = rectangle.Width;
        //    int h = rectangle.Height;

        //    if (radius <= 0)
        //    {
        //        path.AddLine(l, t, l + w, t); // top                
        //        path.AddLine(l + w, t, l + w, t + h); // right                
        //        path.AddLine(l + w, t + h, l, t + h); // bottom                
        //        path.AddLine(l, t + h, l, t); // left
        //    }
        //    else
        //    {
        //        int d = radius << 1;
        //        path.AddArc(l, t, d, d, 180, 90); // topleft
        //        path.AddLine(l + radius, t, l + w - radius, t); // top
        //        path.AddArc(l + w - d, t, d, d, 270, 90); // topright
        //        path.AddLine(l + w, t + radius, l + w, t + h - radius); // right
        //        path.AddArc(l + w - d, t + h - d, d, d, 0, 90); // bottomright
        //        path.AddLine(l + w - radius, t + h, l + radius, t + h); // bottom
        //        path.AddArc(l, t + h - d, d, d, 90, 90); // bottomleft
        //        path.AddLine(l, t + h - radius, l, t + radius); // left
        //    }

        //    path.CloseFigure();
        //    return path;
        //}

        //private static GraphicsPath CreateTopRoundRectangle(Rectangle rectangle, int radius)
        //{
        //    GraphicsPath path = new GraphicsPath();
        //    int l = rectangle.Left;
        //    int t = rectangle.Top;
        //    int w = rectangle.Width;
        //    int h = rectangle.Height;

        //    if (radius <= 0)
        //    {
        //        path.AddLine(l, t, l + w, t); // top                
        //        path.AddLine(l + w, t, l + w, t + h); // right                
        //        path.AddLine(l + w, t + h, l, t + h); // bottom                
        //        path.AddLine(l, t + h, l, t); // left
        //    }
        //    else
        //    {
        //        int d = radius << 1;
        //        path.AddArc(l, t, d, d, 180, 90); // topleft
        //        path.AddLine(l + radius, t, l + w - radius, t); // top
        //        path.AddArc(l + w - d, t, d, d, 270, 90); // topright
        //        path.AddLine(l + w, t + radius, l + w, t + h); // right
        //        path.AddLine(l + w, t + h, l, t + h); // bottom
        //        path.AddLine(l, t + h, l, t + radius); // left
        //    }

        //    path.CloseFigure();
        //    return path;
        //}

        private static GraphicsPath CreateBottomRadialPath(Rectangle rectangle)
        {
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = rectangle;
            rect.X -= rect.Width * .35f;
            rect.Y -= rect.Height * .15f;
            rect.Width *= 1.7f;
            rect.Height *= 2.3f;
            path.AddEllipse(rect);
            path.CloseFigure();
            return path;
        }

        #endregion

        #region " Unused Properties & Events "

        /// <summary>This property is not relevant for this class.</summary>
        /// <returns>This property is not relevant for this class.</returns>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public new FlatButtonAppearance FlatAppearance
        {
            get { return base.FlatAppearance; }
        }

        /// <summary>This property is not relevant for this class.</summary>
        /// <returns>This property is not relevant for this class.</returns>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public new FlatStyle FlatStyle
        {
            get { return base.FlatStyle; }
            set { base.FlatStyle = value; }
        }

        /// <summary>This property is not relevant for this class.</summary>
        /// <returns>This property is not relevant for this class.</returns>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public new bool UseVisualStyleBackColor
        {
            get { return base.UseVisualStyleBackColor; }
            set { base.UseVisualStyleBackColor = value; }
        }

        #endregion

        #region " Animation Support "

        private List<Image> frames;

        private const int FRAME_DISABLED = 0;
        private const int FRAME_PRESSED = 1;
        private const int FRAME_NORMAL = 2;
        private const int FRAME_ANIMATED = 3;

        private bool HasAnimationFrames
        {
            get
            {
                return frames != null && frames.Count > FRAME_ANIMATED;
            }
        }

        private void CreateFrames()
        {
            CreateFrames(false);
        }

        private void CreateFrames(bool withAnimationFrames)
        {
            DestroyFrames();
            if (!IsHandleCreated)
            {
                return;
            }
            if (frames == null)
            {
                frames = new List<Image>();
            }
            frames.Add(CreateBackgroundFrame(false, false, false, false, 0));
            frames.Add(CreateBackgroundFrame(true, true, false, true, 0));
            frames.Add(CreateBackgroundFrame(false, false, false, true, 0));
            if (!withAnimationFrames)
            {
                return;
            }
            for (int i = 0; i < framesCount; i++)
            {
                frames.Add(CreateBackgroundFrame(false, true, true, true, (float)i / (framesCount - 1F)));
            }
        }

        private void DestroyFrames()
        {
            if (frames != null)
            {
                while (frames.Count > 0)
                {
                    frames[frames.Count - 1].Dispose();
                    frames.RemoveAt(frames.Count - 1);
                }
            }
        }

        private const int animationLength = 300;
        private const int framesCount = 10;
        private int currentFrame;
        private int direction;

        private bool isAnimating
        {
            get
            {
                return direction != 0;
            }
        }

        private void FadeIn()
        {
            direction = 1;
            timer.Enabled = true;
        }

        private void FadeOut()
        {
            direction = -1;
            timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!timer.Enabled)
            {
                return;
            }
            Refresh();
            currentFrame += direction;
            if (currentFrame == -1)
            {
                currentFrame = 0;
                timer.Enabled = false;
                direction = 0;
                return;
            }
            if (currentFrame == framesCount)
            {
                currentFrame = framesCount - 1;
                timer.Enabled = false;
                direction = 0;
            }
        }

        #endregion
    }
}
