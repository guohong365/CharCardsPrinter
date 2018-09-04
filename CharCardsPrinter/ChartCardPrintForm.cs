using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PinYin4N;
using PinYin4N.format;
    namespace CharCardsPrinter
{
    public partial class ChartCardPrintForm : Form
    {
        private PrintDocument _printDocument;
        private PrintDialog _printDialog;
        private PrintPreviewDialog _previewDialog;
        private PageSetupDialog _pageSetupDialog;
        private List<CharCard> _cards = new List<CharCard>();

        private const float _DEFAULT_COL_1_X = 14.7f;
        private const float _DEFAULT_COL_2_X = 112.4f;
        private const float _DEFAULT_ROW_SPACE = 57.5f;
        private const float _DEFAULT_ROW_Y = 9.5f;
        private const float _DEFAULT_CARD_WIDTH = 83;
        private const float _DEFAULT_CARD_HEIGHT = 48;
        private const int _DEFAULT_COUNT_PER_PAGE = 10;
       
        private int _current;
        private int _printCount;

        public ChartCardPrintForm()
        {
            InitializeComponent();
            _printDocument = new PrintDocument();
            _printDocument.PrintPage += doPrintPage;
            _printDocument.BeginPrint += doBeginPrint;
            _printDialog = new PrintDialog();
            _printDialog.Document = _printDocument;
            _previewDialog = new PrintPreviewDialog();
            _previewDialog.Document = _printDocument;
            _pageSetupDialog = new PageSetupDialog();
            _pageSetupDialog.Document = _printDocument;            
        }

        private void doPageSetup(object sender, EventArgs e)
        {
            _pageSetupDialog.ShowDialog();
        }

        private void doPreview(object sender, EventArgs e)
        {
            createCards();
            _previewDialog.ShowDialog();
        }

        private void doPrint(object sender, EventArgs e)
        {
            createCards();
            _printDialog.AllowSomePages = true;
            _printDialog.PrinterSettings.FromPage = 1;
            _printDialog.PrinterSettings.ToPage = getPageCount();
            _pageSetupDialog.AllowPaper = true;
            if(_printDialog.ShowDialog()== DialogResult.OK)
            {
                //_current = _DEFAULT_COUNT_PER_PAGE * (_pageSetupDialog.PrinterSettings.FromPage -1);
                //_printCount = _DEFAULT_COUNT_PER_PAGE * _pageSetupDialog.PrinterSettings.ToPage;
                _current = 0;
                _printDocument.Print();
            }
        }
        private int getPageCount()
        {
            return _cards.Count / _DEFAULT_COUNT_PER_PAGE + _cards.Count % _DEFAULT_COUNT_PER_PAGE;
        }

        
        private void doPrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Graphics g = e.Graphics;            
            float y = _DEFAULT_ROW_Y;
            int count = 0;
            Matrix matrix = new Matrix();
            for(int i = _current; i< _cards.Count && count <_DEFAULT_COUNT_PER_PAGE ; i++)
            {
                count++;
                float x = (count + 1) % 2 == 0 ? _DEFAULT_COL_1_X : _DEFAULT_COL_2_X;
                //GraphicsState state= g.Save();
                matrix.Reset();
                matrix.Translate(x, y);
                g.Transform = matrix;
                _cards[i].Draw(g);
                if ((count + 1) % 2 != 0) y += _DEFAULT_ROW_SPACE;
            }
            _current += count;
            e.HasMorePages = _current < _cards.Count - 1;
        }
        const string _CHAR_STR= 
            "大小多少长短方圆前后左右上下里外远近高矮出入开关起落来去轻重快慢有无空满凉热冷暖真假好坏生熟湿干粗细软硬横竖直弯正斜薄厚宽窄深浅春夏秋冬阴晴早晚" +
            "东南西北酸甜苦辣日月水火山石土田天地星云风雨雷电衣帽鞋袜巾帕枕垫杯瓶壶桶钟表灯扇勺筷刀叉锅碗盘盆镜梳床凳钉铲针线琴棋书画笔纸尺盒伞烛鼓铃球筒箱包" +
            "瓜果梨桃饭菜米面葱姜蒜椒糕饼蛋糖柿茄菠豆莓李橙蕉红黄蓝绿紫粉白黑花草树木梅兰竹菊冰雪沙虹江河海瀑桌椅门窗机车船帆枝叶芽苗杨柳松桦哥姐弟妹爷奶爸妈" +
            "你我他她男女老幼眼耳口鼻手足牙脸人身头发师生工医哭笑喜怒吃喝醒睡走跑坐卧摸爬站立折剪编涂滑骑抱玩写指举拍看想拿洗鸟兽虫鱼猫狗兔雁猪马牛羊鸡鸭鹅燕";
        private void formLoad(object sender, EventArgs e)
        {
            textBox1.Text = _CHAR_STR;
            btnBorderColor.Click += doSelectBorderColor;
            btnFont.Click += doSelectFont;
            btnFontColor.Click += doSelectFontColor;
            btnGridColor.Click += doSelectGridColor;
            btnPreview.Click += doPreview;
            btnPageSetup.Click += doPageSetup;
            btnPrint.Click += doPrint;
            txtWidth.TextChanged += widthChanged;
            txtHeight.TextChanged += heightChanged;
            txtBorderThick.TextChanged += borderWidthChanged;
            cbDrawGrid.CheckedChanged += showGridCheckedChanged;
            cbRoundRect.CheckedChanged += roundRectCheckedChanged;
            createSampleCard();
        }
        private void widthChanged(object sender, EventArgs e)
        {
            _sampleCard.Width = Convert.ToSingle(txtWidth.Text);
            panelCardPreview.Invalidate();
        }
        private void heightChanged(object sender, EventArgs e)
        {
            _sampleCard.Height = Convert.ToSingle(txtHeight.Text);
            panelCardPreview.Invalidate();
        }
        private void borderWidthChanged(object sender, EventArgs e)
        {
            _sampleCard.BorderWidth = Convert.ToSingle(txtBorderThick.Text);
            panelCardPreview.Invalidate();
        }
        private void roundRChanged(object sender, EventArgs e)
        {
            _sampleCard.RoundBoxRadius = Convert.ToSingle(txtR.Text);
            panelCardPreview.Invalidate();
        }
        private void roundRectCheckedChanged(object sender, EventArgs e)
        {
            _sampleCard.BorderStyle = cbRoundRect.Checked ? BorderStyle.ROUND_RECTANGLE : BorderStyle.RECTANGLE;
            panelCardPreview.Invalidate();
        }
        private void showGridCheckedChanged(object sender, EventArgs e)
        {
            _sampleCard.ShowGrid = cbDrawGrid.Checked;
            panelCardPreview.Invalidate();
        }
        private Color chooseColor(object sender, Color initColor)
        {
            Color result = initColor;
            ColorDialog dialog = new ColorDialog();
            dialog.Color = initColor;
            if(dialog.ShowDialog()== DialogResult.OK)
            {
                result= dialog.Color;
                panelCardPreview.Invalidate();
            }
            ((Control)sender).BackColor = result;
            return result;
        }
        private void doSelectBorderColor(object sender, EventArgs e)
        {
            _borderColor = chooseColor(sender,_borderColor);
        }
        private void doSelectFontColor(object sender, EventArgs e)
        {
            _fontColor = chooseColor(sender,_fontColor);
        }
        private void doSelectGridColor(object sender, EventArgs e)
        {
            _gridColor = chooseColor(sender,_gridColor);
        }
        private void doSelectFont(object sender, EventArgs e)
        {
            FontDialog dialog = new FontDialog();
            dialog.FontMustExist = true;
            dialog.Color = _fontColor;
            dialog.Font = new Font(_fontFamily, 16);
            dialog.ShowColor = true;
            dialog.ShowApply = false;
            if(dialog.ShowDialog()== DialogResult.OK)
            {
                _fontColor = dialog.Color;
                _fontFamily = dialog.Font.FontFamily;
                btnFontColor.BackColor = _fontColor;
                panelCardPreview.Invalidate();
            }
        }
        Color _borderColor=Color.Black;
        Color _gridColor=Color.Black;
        FontFamily _fontFamily=new FontFamily("楷体");
        FontStyle _fontStyle = FontStyle.Regular;
        Color _fontColor=Color.Black;
        CharCard _sampleCard = null;
        void createSampleCard()
        {
            float cardWidth = Convert.ToSingle(txtWidth.Text);
            float cardHeight = Convert.ToSingle(txtHeight.Text);
            float borderWidth = Convert.ToSingle(txtBorderThick.Text);
            bool useRoundRectangle = cbRoundRect.Checked;
            bool drawGrid = cbDrawGrid.Checked;
            _sampleCard = new CharCard("永", new SizeF(cardWidth, cardHeight))
            {
                FontFamily = _fontFamily,
                FontColor = _fontColor,
                Style = (int)_fontStyle,
                BorderWidth = borderWidth,
                BorderStyle = useRoundRectangle ? BorderStyle.ROUND_RECTANGLE : BorderStyle.RECTANGLE,
                BorderColor = _borderColor,
                ShowGrid = drawGrid,
                GridColor = _gridColor
            };
        }

        void updateSampleCard()
        {
            if (_sampleCard == null) return;
            float cardWidth = Convert.ToSingle(txtWidth.Text);
            float cardHeight = Convert.ToSingle(txtHeight.Text);
            float borderWidth = Convert.ToSingle(txtBorderThick.Text);
            float radius = Convert.ToSingle(txtR.Text);
            bool useRoundRectangle = cbRoundRect.Checked;
            bool drawGrid = cbDrawGrid.Checked;
            _sampleCard.FontFamily = _fontFamily;
            _sampleCard.FontColor = _fontColor;
            _sampleCard.Style = (int)_fontStyle;
            _sampleCard.BorderWidth = borderWidth;
            _sampleCard.BorderStyle = useRoundRectangle ? BorderStyle.ROUND_RECTANGLE : BorderStyle.RECTANGLE;
            _sampleCard.BorderColor = _borderColor;
            _sampleCard.ShowGrid = drawGrid;
            _sampleCard.GridColor = _gridColor;
            _sampleCard.RoundBoxRadius = radius;
        }
        void createCards()
        {
            float cardWidth=Convert.ToSingle(txtWidth.Text);
            float cardHeight=Convert.ToSingle(txtHeight.Text);
            float borderWidth=Convert.ToSingle(txtBorderThick.Text);
            float radius = Convert.ToSingle(txtR.Text);
            bool useRoundRectangle=cbRoundRect.Checked;
            bool drawGrid=cbDrawGrid.Checked;
            
            _cards.Clear();
            char[] chars = textBox1.Text.Trim().Replace("\r\n","").ToCharArray();
            for(int i= 0; i < chars.Length; i++)
            {
                CharCard card = new CharCard(chars[i], new SizeF(cardWidth, cardHeight))
                {
                    FontFamily = _fontFamily,
                    FontColor = _fontColor,
                    Style = (int)_fontStyle,
                    BorderWidth = borderWidth,
                    BorderStyle = useRoundRectangle ? BorderStyle.ROUND_RECTANGLE : BorderStyle.RECTANGLE,
                    BorderColor = _borderColor,
                    ShowGrid = drawGrid,
                    GridColor = _gridColor,
                    RoundBoxRadius = radius,
                    Scale = _sampleCard == null ? float.NaN : _sampleCard.Scale
                };
                _cards.Add(card);
            }
            
        }        

        private void doBeginPrint(object sender, PrintEventArgs e)
        {

        }


        private void doPaintCardPreview(object sender, PaintEventArgs e)
        {
            if (_sampleCard == null) return;
            updateSampleCard();
            Rectangle rectangle = e.ClipRectangle;
            Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float ratio=DrawHelper.GetRatio(  rectangle.Width * 0.9f, rectangle.Height * 0.9f, _sampleCard.Width,_sampleCard.Height);
            float x = (rectangle.Width - _sampleCard.Width * ratio)/2;
            float y = (rectangle.Height - _sampleCard.Height * ratio)/2;
            
            
            Brush brush = new SolidBrush(Color.White);
            g.FillRectangle(brush, rectangle);

            Matrix matrix = new Matrix();
            matrix.Translate(x, y);
            matrix.Scale(ratio, ratio);            
            g.Transform = matrix;
            _sampleCard.Draw(g);
            e.Graphics.DrawImageUnscaled(bitmap, 0, 0);

        }

        private void doPaintPagePreview(object sender, PaintEventArgs e)
        {

        }

    }
}
