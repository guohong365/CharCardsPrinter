using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace CharCardsPrinter
{
    public enum BorderStyle
    {
        RECTANGLE,
        ROUND_RECTANGLE
    }
    public class CharCard
    {
        private const string _DEFAULT_FONT_FAMILY_NAME = "楷体";
        private const float _BLANK_PERCENTAGE = 0.8f;
        private SizeF _size;
        public SizeF Size { get { return _size; } set { _size = value; } }
        public string Charecters { get; }
        public FontFamily FontFamily { get; set; }
        public Color FontColor { get; set; }
        public int Style { get; set; }
        public float BorderWidth { get; set; }
        public Color BorderColor { get; set; }
        public BorderStyle BorderStyle { get; set; }
        public bool ShowGrid{get;set;}
        public Color GridColor { get; set; }
        public float Width { get { return _size.Width; } set { _size.Width = value; } }
        public float Height { get { return Size.Height; } set { _size.Height = value; } }
        public float Scale { get; set; }
        public float RoundBoxRadius { get; set; }

        public CharCard(char ch, SizeF size)
            :this(new string(ch, 1), size)
        {
        }

        public CharCard(char ch, SizeF size, FontFamily fontFamily, int style)
            :this(new string(ch, 1), size, fontFamily, style)
        {
        }
        public CharCard(string chs, SizeF size)
            :this(chs, size, new FontFamily(_DEFAULT_FONT_FAMILY_NAME), (int)FontStyle.Regular)
        {

        }
        public CharCard(string chs, SizeF size, FontFamily fontFamily, int style)
        {
            Charecters = chs;
            Size = size;
            FontFamily = fontFamily;
            Style = style;
            Scale = float.NaN;
        }

        public void Draw(Graphics graphics)
        {
            if (BorderStyle == BorderStyle.RECTANGLE)
            {
                DrawRectangleBorder(graphics);
            } else
            {
                DrawRoundRectBorder(graphics);
            }
            if (ShowGrid)
            {
                DrawGrid(graphics);
            }
            DrawChar(graphics);
        }

        protected PointF Center {
            get
            {
                return new PointF(Size.Width / 2, Size.Height/2);
            }
        }
        public static void CalcCharBounds()
        {

        }
        protected void DrawRectangleBorder(Graphics graphics)
        {
            Pen pen = new Pen(BorderColor, BorderWidth);
            graphics.DrawRectangle(pen, BorderWidth/2, BorderWidth/2, Size.Width - BorderWidth/2, Size.Height- BorderWidth/2);
        }
        protected void DrawRoundRectBorder(Graphics graphics)
        {
            Pen pen = new Pen(BorderColor, BorderWidth);
            float d = BorderWidth / 2;
            float r = RoundBoxRadius - d;
            float c = RoundBoxRadius * 2 - BorderWidth;

            graphics.DrawLine(pen, r, d, Size.Width - r, d);
            graphics.DrawLine(pen, r, Height - d, Width - r, Height - d);

            graphics.DrawLine(pen, d, r, d, Height - r);
            graphics.DrawLine(pen, Width - d, r, Width - d, Height - r);

            graphics.DrawArc(pen, d, d, c, c, 180, 90);
            graphics.DrawArc(pen, Width - c - d, d,  c, c, 270, 90);
            graphics.DrawArc(pen, d, Height -c -d, c, c, 90, 90);
            graphics.DrawArc(pen, Width - c -d, Height -c -d, c, c, 0, 90);


        }


        protected void DrawGrid(Graphics graphics)
        {
            Pen pen = new Pen(GridColor, 0.1f);
            pen.DashStyle = DashStyle.Dash;
            float size =Math.Min(Size.Width, Size.Height) - BorderWidth - 2;
            float left = BorderWidth / 2 + (Size.Width - size) / 2;
            float top = BorderWidth / 2 + (Size.Height - size) / 2;
            graphics.DrawRectangle(pen, left,top, size, size);
            graphics.DrawLine(pen, left, top , left + size, top + size);
            graphics.DrawLine(pen, left, top + size, left + size, top);
            graphics.DrawLine(pen, left, top + size/2 , left + size, top + size/2);
            graphics.DrawLine(pen, left + size/2, top, left + size/2, top + size);
        }

        protected void DrawChar(Graphics graphics)
        {
            GraphicsPath path = new GraphicsPath();
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Near;
            SolidBrush brush=new SolidBrush(FontColor);
            format.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.DisplayFormatControl | StringFormatFlags.NoWrap;
            path.AddString(Charecters, FontFamily, Style, Size.Height * _BLANK_PERCENTAGE, new PointF(0, 0), format);
            RectangleF bounds = path.GetBounds();
            SizeF layout = new SizeF(Size.Width, Size.Height);
            if (float.IsNaN(Scale))
            {
                Scale = DrawHelper.GetRatio(
                    layout.Width * _BLANK_PERCENTAGE, layout.Height * _BLANK_PERCENTAGE,
                    bounds.Width, bounds.Height);
            }
            PointF pt=new PointF();
            Matrix m=new Matrix();
            m.Scale(Scale, Scale);
            path.Transform(m);
            bounds= path.GetBounds();
            m.Reset();
            m.Translate(-bounds.X, -bounds.Y);
            pt.X = (layout.Width - bounds.Width) / 2;
            pt.Y = (layout.Height - bounds.Height) / 2;
            m.Translate(pt.X, pt.Y);
            path.Transform(m);
            graphics.FillPath(brush, path);
        }
        
    }
}
