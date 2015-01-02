using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.Design;
using RValueCharts.Utility;

namespace RValueCharts.Utility
{
    public enum ValueChartStyle
    {
        CandleStick,
        OHLC,
        HiLoBars,
        LineOnClose
    }
}

namespace NinjaTrader.Indicator
{
    /// <summary>
    /// RValueCharts. 
    /// 12/02/08 Option to display market valuation text and alert on OB/OS conditions added by Elliott Wave.
    /// </summary>
    [Description("Value Charts by David Stendhal; Converted to NinjaTrader by TradingStudies.com")]
    public class RValueCharts : Indicator
    {
        // Wizard generated variables
        private int _length = 5; // Default setting for Length
        private Color _upColor = Color.Green;
        private Color _downColor = Color.Red;
        private Color _outlineColor = Color.Black;
        private Color _moderateOver = Color.BurlyWood;
        private Color _extremeOver = Color.Thistle;
        private Color _colorDotOversold = Color.Blue;
        private Color _colorDotOverbought = Color.Red;
        private float _outlineWidth = 1;
        private bool _changeBackground;
        private bool _mainChartSettings = true;
        private ValueChartStyle _vCt = ValueChartStyle.OHLC;
        private DataSeries _vOpen;
        private DataSeries _vHigh;
        private DataSeries _vLow;
        private bool _dotdraw = true;
        private int _mltpl = 2;
        private bool _textWarnings;
        private bool _soundWarnings;
        private int _alertoff = 60;

        protected override void Initialize()
        {
            Name = " RValueCharts";
            Add(new Plot(Color.Black, PlotStyle.Hash, "VClose"));
            _vOpen = new DataSeries(this, MaximumBarsLookBack.Infinite);
            _vHigh = new DataSeries(this, MaximumBarsLookBack.Infinite);
            _vLow = new DataSeries(this, MaximumBarsLookBack.Infinite);
            CalculateOnBarClose = false;
            Overlay = false;
            PriceTypeSupported = false;
            PlotsConfigurable = false;
            DrawOnPricePanel = true;
        }

        protected override void OnBarUpdate()
        {
            if (CurrentBar < Math.Max(Length, 5)) return;

            _vOpen.Set(VChart(Length, Open));
            _vHigh.Set(VChart(Length, High));
            _vLow.Set(VChart(Length, Low));
            VClose.Set(VChart(Length, Close));

            //Text Section

            if (_textWarnings)
            {
                if (VClose[0] < -8)
                    DrawTextFixed("Oversold2", " Significantly Oversold!", TextPosition.BottomLeft, Color.Black,
                                  new Font("Arial", 10), Color.Red, Color.Red, 7);
                else
                    RemoveDrawObject("Oversold2");

                if (VClose[0] > 8)
                    DrawTextFixed("Overbought2", " Significantly Overbought!", TextPosition.BottomLeft, Color.Black,
                                  new Font("Arial", 10), Color.Red, Color.Red, 7);
                else
                    RemoveDrawObject("Overbought2");

                if (VClose[0] < -4 && VClose[0] > -8)
                    DrawTextFixed("Oversold1", " Moderately Oversold", TextPosition.BottomLeft, Color.Black,
                                  new Font("Arial", 10), Color.Gold, Color.Gold, 7);
                else
                    RemoveDrawObject("Oversold1");

                if (VClose[0] > 4 && VClose[0] < 8)
                    DrawTextFixed("Overbought1", " Moderately Overbought", TextPosition.BottomLeft, Color.Black,
                                  new Font("Arial", 10), Color.Gold, Color.Gold, 7);
                else
                    RemoveDrawObject("Overbought1");

                if (VClose[0] > -4 && VClose[0] < 4)
                    DrawTextFixed("FairValue", " Fair Value", TextPosition.BottomLeft, Color.Black,
                                  new Font("Arial", 10), Color.Green, Color.Green, 7);
                else
                    RemoveDrawObject("FairValue");
            }

            if (_soundWarnings)
            {
                if (VClose[0] < -8.00)
                    Alert("AlertOS", Priority.High, "Significantly Oversold", "vc.wav", _alertoff, Color.Black, Color.Red);

                if (VClose[0] > 8.00)
                    Alert("AlertOB", Priority.High, "Significantly Overbought", "vc.wav", _alertoff, Color.Black,
                          Color.Red);
            }
            if (!_dotdraw) return;

            if (_vLow[0] < -8.00)
                DrawDiamond(CurrentBar + "dot", false, 0, Low[0] - _mltpl * TickSize, _colorDotOversold);

            if (_vHigh[0] > 8.00)
                DrawDiamond(CurrentBar + "dot", false, 0, High[0] + _mltpl * TickSize, _colorDotOverbought);
        }

        public double VChart(int l, IDataSeries price)
        {
            double lra, lrb, lrc, lrd, lre;
            int varP = (int)Math.Round((double)l / 5, 0);

            if (l > 7)
            {
                lra = MAX(High, varP)[0] - MIN(Low, varP)[0];
                lrb = MAX(High, varP)[varP + 1] - MIN(Low, varP)[varP];
                lrc = MAX(High, varP)[varP * 2] - MIN(Low, varP)[varP * 2];
                lrd = MAX(High, varP)[varP * 3] - MIN(Low, varP)[varP * 3];
                lre = MAX(High, varP)[varP * 4] - MIN(Low, varP)[varP * 4];
            }
            else
            {
                lra = (Math.Abs(Close[0] - Close[1]) > (High[0] - Low[0]))
                          ? Math.Abs(Close[0] - Close[1])
                          : (High[0] - Low[0]);
                lrb = (Math.Abs(Close[1] - Close[2]) > (High[1] - Low[1]))
                          ? Math.Abs(Close[1] - Close[2])
                          : (High[1] - Low[1]);
                lrc = (Math.Abs(Close[2] - Close[3]) > (High[2] - Low[2]))
                          ? Math.Abs(Close[2] - Close[3])
                          : (High[2] - Low[2]);
                lrd = (Math.Abs(Close[3] - Close[4]) > (High[3] - Low[3]))
                          ? Math.Abs(Close[3] - Close[4])
                          : (High[3] - Low[3]);
                lre = (Math.Abs(Close[4] - Close[5]) > (High[4] - Low[4]))
                          ? Math.Abs(Close[4] - Close[5])
                          : (High[4] - Low[4]);
            }

            double lRange = ((lra + lrb + lrc + lrd + lre) * .2) * .2;

            if (lRange > 0)
                return ((price[0] - SMA(Median, l)[0]) / lRange);
            return (price[0] - SMA(Median, l)[0]);
        }

        public override void Plot(Graphics graphics, Rectangle bounds, double min, double max)
        {
            int barPaintWidth = ChartControl.ChartStyle.GetBarPaintWidth(ChartControl.BarWidth);
            SolidBrush brushUp = new SolidBrush(_upColor);
            SolidBrush brushDown = new SolidBrush(_downColor);
            int penwidth = Math.Max(1, ChartControl.BarWidth - 2);
            int offset = Math.Max(1, (barPaintWidth / 2 - 2));
            Pen myPen = new Pen(_outlineColor);

            if (_mainChartSettings)
            {
                switch (ChartControl.ChartStyle.ChartStyleType.ToString())
                {
                    case "OHLC":
                        _vCt = ValueChartStyle.OHLC;
                        break;
                    case "HiLoBars":
                        _vCt = ValueChartStyle.HiLoBars;
                        break;
                    case "CandleStick":
                        _vCt = ValueChartStyle.CandleStick;
                        break;
                    case "LineOnClose":
                        _vCt = ValueChartStyle.LineOnClose;
                        break;
                    default:
                        _vCt = ValueChartStyle.OHLC;
                        break;
                }
                _upColor = ChartControl.ChartStyle.UpColor;
                _downColor = ChartControl.ChartStyle.DownColor;
                _outlineColor = ChartControl.ChartStyle.Pen.Color;
                _outlineWidth = ChartControl.ChartStyle.Pen.Width;
            }

            int m8 = ChartControl.GetYByValue(this, -8);
            int m4 = ChartControl.GetYByValue(this, -4);
            int p8 = ChartControl.GetYByValue(this, 8);
            int p4 = ChartControl.GetYByValue(this, 4);

            if (_changeBackground)
            {
                graphics.FillRectangle(new SolidBrush(_extremeOver), 0, bounds.Y, ChartControl.Right,
                                       (int)((max - 8.0) / ChartControl.MaxMinusMin(max, min) * bounds.Height));
                graphics.FillRectangle(new SolidBrush(_moderateOver), 0, bounds.Y +
                                                                        (int)
                                                                        ((max - 8.0) / ChartControl.MaxMinusMin(max, min) *
                                                                         bounds.Height), ChartControl.Right,
                                       (int)((4.0) / ChartControl.MaxMinusMin(max, min) * bounds.Height + 1));

                graphics.FillRectangle(new SolidBrush(_moderateOver), 0, bounds.Y +
                                                                        (int)
                                                                        ((max + 4.0) / ChartControl.MaxMinusMin(max, min) *
                                                                         bounds.Height), ChartControl.Right,
                                       (int)((4.0) / ChartControl.MaxMinusMin(max, min) * bounds.Height + 1));
                graphics.FillRectangle(new SolidBrush(_extremeOver), 0, bounds.Y +
                                                                       (int)
                                                                       ((max + 8.0) / ChartControl.MaxMinusMin(max, min) *
                                                                        bounds.Height), ChartControl.Right,
                                       bounds.Height -
                                       (int)((max + 8) / ChartControl.MaxMinusMin(max, min) * bounds.Height));
            }

            graphics.DrawLine(new Pen(Color.Red), 0, m8, ChartControl.Right, m8);
            graphics.DrawLine(new Pen(Color.Green), 0, m4, ChartControl.Right, m4);
            graphics.DrawLine(new Pen(Color.Green), 0, p4, ChartControl.Right, p4);
            graphics.DrawLine(new Pen(Color.Red), 0, p8, ChartControl.Right, p8);

            for (int idx = LastBarIndexPainted; idx >= FirstBarIndexPainted; idx--)
            {
                if (idx - Displacement < 0 || idx - Displacement >= Bars.Count || (!ChartControl.ShowBarsRequired && idx - Displacement < BarsRequired))
                    continue;

                double valH = _vHigh.Get(idx);
                double valL = _vLow.Get(idx);
                double valC = VClose.Get(idx);
                double valO = _vOpen.Get(idx);
                int x = ChartControl.GetXByBarIdx(BarsArray[0], idx);

                int y1 = ChartControl.GetYByValue(this, valO);
                int y2 = ChartControl.GetYByValue(this, valH);
                int y3 = ChartControl.GetYByValue(this, valL);
                int y4 = ChartControl.GetYByValue(this, valC);

                switch (_vCt)
                {
                    case ValueChartStyle.OHLC:
                        myPen.Color = (y4 > y1) ? _downColor : _upColor;
                        myPen.Width = penwidth;
                        graphics.DrawLine(myPen, x, y2 - penwidth / 2, x, y3 + penwidth / 2);
                        graphics.DrawLine(myPen, x - offset, y1, x, y1);
                        graphics.DrawLine(myPen, x, y4, x + offset, y4);
                        break;
                    case ValueChartStyle.CandleStick:
                        if (y4 > y1)
                        {
                            graphics.FillRectangle(brushDown, x - barPaintWidth / 2 + 1, y1, barPaintWidth - 1, y4 - y1);
                            graphics.DrawRectangle(new Pen(_outlineColor, _outlineWidth),
                                                   (x - barPaintWidth / 2) + (_outlineWidth / 2), y1,
                                                   barPaintWidth - _outlineWidth, y4 - y1);
                        }
                        if (y4 < y1)
                        {
                            graphics.FillRectangle(brushUp, x - barPaintWidth / 2 + 1, y4, barPaintWidth - 1, y1 - y4);
                            graphics.DrawRectangle(new Pen(_outlineColor, _outlineWidth),
                                                   (x - barPaintWidth / 2) + (_outlineWidth / 2), y4,
                                                   barPaintWidth - _outlineWidth, y1 - y4);
                        }
                        graphics.DrawLine(new Pen(_outlineColor, _outlineWidth), x, y2, x, Math.Min(y1, y4));
                        graphics.DrawLine(new Pen(_outlineColor, _outlineWidth), x, y3, x, Math.Max(y1, y4));
                        if (y1 == y4)
                            graphics.DrawLine(new Pen(_outlineColor, _outlineWidth), x - barPaintWidth / 2 + 1, y1,
                                              x - barPaintWidth / 2 + barPaintWidth - 2, y4);
                        break;
                    case ValueChartStyle.HiLoBars:
                        myPen.Color = (y4 > y1) ? _downColor : _upColor;
                        myPen.Width = penwidth;
                        graphics.DrawLine(myPen, x, y2, x, y3);
                        if (y2 == y3) graphics.DrawLine(myPen, x - penwidth / 2, y2, x - penwidth / 2 + penwidth, y3);
                        break;
                    case ValueChartStyle.LineOnClose:
                        myPen.Color = _upColor;
                        myPen.Width = ChartControl.BarWidth;
                        if ((idx - 1) >= 0)
                        {
                            int prevx = ChartControl.GetXByBarIdx(BarsArray[0], idx - 1);
                            int prevy = ChartControl.GetYByValue(this, VClose.Get(idx - 1));
                            SmoothingMode oldSmoothingMode = graphics.SmoothingMode;
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.DrawLine(myPen, prevx, prevy, x, y4);
                            graphics.SmoothingMode = oldSmoothingMode;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public override void GetMinMaxValues(ChartControl chartControl, ref double min, ref double max)
        {
            base.GetMinMaxValues(chartControl, ref min, ref max);
            max = Math.Max(max, 9.00) + 1.00;
            min = Math.Min(min, -9.00) - 1.00;
        }

        public override string ToString()
        {
            return string.Format(" RValueCharts({0})", Length);
        }

        #region Properties

        [Description("Period Length")]
        [GridCategory("Parameters")]
        [Gui.Design.DisplayName("Period Length")]
        public int Length
        {
            get { return _length; }
            set { _length = Math.Min(Math.Max(2, value), 1000); }
        }

        [Browsable(false)]
        [XmlIgnore]
        public DataSeries VClose
        {
            get { return Values[0]; }
        }
        [Browsable(false)]
        [XmlIgnore]
        public DataSeries VHigh
        {
            get { return _vHigh; }
        }

        [Browsable(false)]
        [XmlIgnore]
        public DataSeries VLow
        {
            get { return _vLow; }
        }

        [Description("Main Chart Settings")]
        [GridCategory("Appearance")]
        [Gui.Design.DisplayName("01. Main Chart Settings")]
        public bool MainChartSettings
        {
            get { return _mainChartSettings; }
            set { _mainChartSettings = value; }
        }

        [Description("Value Chart Style")]
        [GridCategory("Appearance")]
        [Gui.Design.DisplayName("02. Value Chart Style")]
        public ValueChartStyle Vct
        {
            get { return _vCt; }
            set { _vCt = value; }
        }

        [Description("Bar Up Color")]
        [GridCategory("Appearance")]
        [Gui.Design.DisplayName("03. Bar Up Color")]
        public Color UpColor
        {
            get { return _upColor; }
            set { _upColor = value; }
        }

        [Description("Bar Down Color")]
        [GridCategory("Appearance")]
        [Gui.Design.DisplayName("04. Bar Down Color")]
        public Color DownColor
        {
            get { return _downColor; }
            set { _downColor = value; }
        }

        [Description("Candle Outline  Color")]
        [GridCategory("Appearance")]
        [Gui.Design.DisplayName("05. Candle Outline Color")]
        public Color OutlineColor
        {
            get { return _outlineColor; }
            set { _outlineColor = value; }
        }

        [Description("Candle Outline Width")]
        [GridCategory("Appearance")]
        [Gui.Design.DisplayName("06. Candle Outline Width")]
        public float OutlineWidth
        {
            get { return _outlineWidth; }
            set { _outlineWidth = Math.Max(1, value); }
        }

        [Description("Background Color Change")]
        [GridCategory("Appearance")]
        [Gui.Design.DisplayName("07. Background Color Change")]
        public bool ChangeBackground
        {
            get { return _changeBackground; }
            set { _changeBackground = value; }
        }

        [Description("Moderate Oversold/Overbought Color")]
        [GridCategory("Appearance")]
        [Gui.Design.DisplayName("08. Moderate Over Color")]
        public Color ModerateOver
        {
            get { return _moderateOver; }
            set { _moderateOver = value; }
        }

        [Description("Extreme Oversold/Overbought Color")]
        [GridCategory("Appearance")]
        [Gui.Design.DisplayName("09. Extreme Over Color")]
        public Color ExtremeOver
        {
            get { return _extremeOver; }
            set { _extremeOver = value; }
        }

        [Description("Text marker showing market valuation.")]
        [Gui.Design.DisplayName("10. Show market valuation Message?")]
        [GridCategory("Appearance")]
        public bool TextWarnings
        {
            get { return _textWarnings; }
            set { _textWarnings = value; }
        }

        [Description("Sound Alert for OB/OS condition.")]
        [Gui.Design.DisplayName("11. Sound alert on overbought/oversold condition?")]
        [GridCategory("Appearance")]
        public bool SoundWarnings
        {
            get { return _soundWarnings; }
            set { _soundWarnings = value; }
        }

        [Description("Sound Alert reset interval.")]
        [Gui.Design.DisplayName("12. Sound alert intervals (sec)")]
        [GridCategory("Appearance")]
        public int SoundAlertInterval
        {
            get { return _alertoff; }
            set { _alertoff = value; }
        }

        [Description("Draws Diamond on Price Panel")]
        [Gui.Design.DisplayName("13. Draw Diamond?")]
        [GridCategory("Appearance")]
        public bool DotDraw
        {
            get { return _dotdraw; }
            set { _dotdraw = value; }
        }

        [Description("Dot offset by ticks")]
        [Gui.Design.DisplayName("14.Ticks for Diamond Offset")]
        [GridCategory("Appearance")]
        public int DotOffset
        {
            get { return _mltpl; }
            set { _mltpl = Math.Max(value, 1); }
        }

        [Description("Diamond OverBought Color")]
        [GridCategory("Appearance")]
        [Gui.Design.DisplayName("15. Diamond OverBought Color")]
        public Color DotOverBought
        {
            get { return _colorDotOverbought; }
            set { _colorDotOverbought = value; }
        }

        [Description("Diamond OverSold Color")]
        [GridCategory("Appearance")]
        [Gui.Design.DisplayName("16. Diamond OverSold Color")]
        public Color DotOverSold
        {
            get { return _colorDotOversold; }
            set { _colorDotOversold = value; }
        }

        [Browsable(false)]
        public string ColorDotOversoldSerialize
        {
            get { return SerializableColor.ToString(_colorDotOversold); }
            set { _colorDotOversold = SerializableColor.FromString(value); }
        }

        [Browsable(false)]
        public string ColorDotOverboughtSerialize
        {
            get { return SerializableColor.ToString(_colorDotOverbought); }
            set { _colorDotOverbought = SerializableColor.FromString(value); }
        }

        [Browsable(false)]
        public string UpColorSerialize
        {
            get { return SerializableColor.ToString(_upColor); }
            set { _upColor = SerializableColor.FromString(value); }
        }

        [Browsable(false)]
        public string DownColorSerialize
        {
            get { return SerializableColor.ToString(_downColor); }
            set { _downColor = SerializableColor.FromString(value); }
        }

        [Browsable(false)]
        public string ModerateOverSerialize
        {
            get { return SerializableColor.ToString(_moderateOver); }
            set { _moderateOver = SerializableColor.FromString(value); }
        }

        [Browsable(false)]
        public string ExtremeOverSerialize
        {
            get { return SerializableColor.ToString(_extremeOver); }
            set { _extremeOver = SerializableColor.FromString(value); }
        }

        [Browsable(false)]
        public string OutlineColorSerialize
        {
            get { return SerializableColor.ToString(_outlineColor); }
            set { _outlineColor = SerializableColor.FromString(value); }
        }

        #endregion
    }
}

#region NinjaScript generated code. Neither change nor remove.
// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    public partial class Indicator : IndicatorBase
    {
        private RValueCharts[] cacheRValueCharts = null;

        private static RValueCharts checkRValueCharts = new RValueCharts();

        /// <summary>
        /// Value Charts by David Stendhal; Converted to NinjaTrader by TradingStudies.com
        /// </summary>
        /// <returns></returns>
        public RValueCharts RValueCharts(bool changeBackground, bool dotDraw, int dotOffset, Color dotOverBought, Color dotOverSold, Color downColor, Color extremeOver, int length, bool mainChartSettings, Color moderateOver, Color outlineColor, float outlineWidth, int soundAlertInterval, bool soundWarnings, bool textWarnings, Color upColor, ValueChartStyle vct)
        {
            return RValueCharts(Input, changeBackground, dotDraw, dotOffset, dotOverBought, dotOverSold, downColor, extremeOver, length, mainChartSettings, moderateOver, outlineColor, outlineWidth, soundAlertInterval, soundWarnings, textWarnings, upColor, vct);
        }

        /// <summary>
        /// Value Charts by David Stendhal; Converted to NinjaTrader by TradingStudies.com
        /// </summary>
        /// <returns></returns>
        public RValueCharts RValueCharts(Data.IDataSeries input, bool changeBackground, bool dotDraw, int dotOffset, Color dotOverBought, Color dotOverSold, Color downColor, Color extremeOver, int length, bool mainChartSettings, Color moderateOver, Color outlineColor, float outlineWidth, int soundAlertInterval, bool soundWarnings, bool textWarnings, Color upColor, ValueChartStyle vct)
        {
            if (cacheRValueCharts != null)
                for (int idx = 0; idx < cacheRValueCharts.Length; idx++)
                    if (cacheRValueCharts[idx].ChangeBackground == changeBackground && cacheRValueCharts[idx].DotDraw == dotDraw && cacheRValueCharts[idx].DotOffset == dotOffset && cacheRValueCharts[idx].DotOverBought == dotOverBought && cacheRValueCharts[idx].DotOverSold == dotOverSold && cacheRValueCharts[idx].DownColor == downColor && cacheRValueCharts[idx].ExtremeOver == extremeOver && cacheRValueCharts[idx].Length == length && cacheRValueCharts[idx].MainChartSettings == mainChartSettings && cacheRValueCharts[idx].ModerateOver == moderateOver && cacheRValueCharts[idx].OutlineColor == outlineColor && cacheRValueCharts[idx].OutlineWidth == outlineWidth && cacheRValueCharts[idx].SoundAlertInterval == soundAlertInterval && cacheRValueCharts[idx].SoundWarnings == soundWarnings && cacheRValueCharts[idx].TextWarnings == textWarnings && cacheRValueCharts[idx].UpColor == upColor && cacheRValueCharts[idx].Vct == vct && cacheRValueCharts[idx].EqualsInput(input))
                        return cacheRValueCharts[idx];

            lock (checkRValueCharts)
            {
                checkRValueCharts.ChangeBackground = changeBackground;
                changeBackground = checkRValueCharts.ChangeBackground;
                checkRValueCharts.DotDraw = dotDraw;
                dotDraw = checkRValueCharts.DotDraw;
                checkRValueCharts.DotOffset = dotOffset;
                dotOffset = checkRValueCharts.DotOffset;
                checkRValueCharts.DotOverBought = dotOverBought;
                dotOverBought = checkRValueCharts.DotOverBought;
                checkRValueCharts.DotOverSold = dotOverSold;
                dotOverSold = checkRValueCharts.DotOverSold;
                checkRValueCharts.DownColor = downColor;
                downColor = checkRValueCharts.DownColor;
                checkRValueCharts.ExtremeOver = extremeOver;
                extremeOver = checkRValueCharts.ExtremeOver;
                checkRValueCharts.Length = length;
                length = checkRValueCharts.Length;
                checkRValueCharts.MainChartSettings = mainChartSettings;
                mainChartSettings = checkRValueCharts.MainChartSettings;
                checkRValueCharts.ModerateOver = moderateOver;
                moderateOver = checkRValueCharts.ModerateOver;
                checkRValueCharts.OutlineColor = outlineColor;
                outlineColor = checkRValueCharts.OutlineColor;
                checkRValueCharts.OutlineWidth = outlineWidth;
                outlineWidth = checkRValueCharts.OutlineWidth;
                checkRValueCharts.SoundAlertInterval = soundAlertInterval;
                soundAlertInterval = checkRValueCharts.SoundAlertInterval;
                checkRValueCharts.SoundWarnings = soundWarnings;
                soundWarnings = checkRValueCharts.SoundWarnings;
                checkRValueCharts.TextWarnings = textWarnings;
                textWarnings = checkRValueCharts.TextWarnings;
                checkRValueCharts.UpColor = upColor;
                upColor = checkRValueCharts.UpColor;
                checkRValueCharts.Vct = vct;
                vct = checkRValueCharts.Vct;

                if (cacheRValueCharts != null)
                    for (int idx = 0; idx < cacheRValueCharts.Length; idx++)
                        if (cacheRValueCharts[idx].ChangeBackground == changeBackground && cacheRValueCharts[idx].DotDraw == dotDraw && cacheRValueCharts[idx].DotOffset == dotOffset && cacheRValueCharts[idx].DotOverBought == dotOverBought && cacheRValueCharts[idx].DotOverSold == dotOverSold && cacheRValueCharts[idx].DownColor == downColor && cacheRValueCharts[idx].ExtremeOver == extremeOver && cacheRValueCharts[idx].Length == length && cacheRValueCharts[idx].MainChartSettings == mainChartSettings && cacheRValueCharts[idx].ModerateOver == moderateOver && cacheRValueCharts[idx].OutlineColor == outlineColor && cacheRValueCharts[idx].OutlineWidth == outlineWidth && cacheRValueCharts[idx].SoundAlertInterval == soundAlertInterval && cacheRValueCharts[idx].SoundWarnings == soundWarnings && cacheRValueCharts[idx].TextWarnings == textWarnings && cacheRValueCharts[idx].UpColor == upColor && cacheRValueCharts[idx].Vct == vct && cacheRValueCharts[idx].EqualsInput(input))
                            return cacheRValueCharts[idx];

                RValueCharts indicator = new RValueCharts();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.ChangeBackground = changeBackground;
                indicator.DotDraw = dotDraw;
                indicator.DotOffset = dotOffset;
                indicator.DotOverBought = dotOverBought;
                indicator.DotOverSold = dotOverSold;
                indicator.DownColor = downColor;
                indicator.ExtremeOver = extremeOver;
                indicator.Length = length;
                indicator.MainChartSettings = mainChartSettings;
                indicator.ModerateOver = moderateOver;
                indicator.OutlineColor = outlineColor;
                indicator.OutlineWidth = outlineWidth;
                indicator.SoundAlertInterval = soundAlertInterval;
                indicator.SoundWarnings = soundWarnings;
                indicator.TextWarnings = textWarnings;
                indicator.UpColor = upColor;
                indicator.Vct = vct;
                Indicators.Add(indicator);
                indicator.SetUp();

                RValueCharts[] tmp = new RValueCharts[cacheRValueCharts == null ? 1 : cacheRValueCharts.Length + 1];
                if (cacheRValueCharts != null)
                    cacheRValueCharts.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheRValueCharts = tmp;
                return indicator;
            }
        }
    }
}

// This namespace holds all market analyzer column definitions and is required. Do not change it.
namespace NinjaTrader.MarketAnalyzer
{
    public partial class Column : ColumnBase
    {
        /// <summary>
        /// Value Charts by David Stendhal; Converted to NinjaTrader by TradingStudies.com
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.RValueCharts RValueCharts(bool changeBackground, bool dotDraw, int dotOffset, Color dotOverBought, Color dotOverSold, Color downColor, Color extremeOver, int length, bool mainChartSettings, Color moderateOver, Color outlineColor, float outlineWidth, int soundAlertInterval, bool soundWarnings, bool textWarnings, Color upColor, ValueChartStyle vct)
        {
            return _indicator.RValueCharts(Input, changeBackground, dotDraw, dotOffset, dotOverBought, dotOverSold, downColor, extremeOver, length, mainChartSettings, moderateOver, outlineColor, outlineWidth, soundAlertInterval, soundWarnings, textWarnings, upColor, vct);
        }

        /// <summary>
        /// Value Charts by David Stendhal; Converted to NinjaTrader by TradingStudies.com
        /// </summary>
        /// <returns></returns>
        public Indicator.RValueCharts RValueCharts(Data.IDataSeries input, bool changeBackground, bool dotDraw, int dotOffset, Color dotOverBought, Color dotOverSold, Color downColor, Color extremeOver, int length, bool mainChartSettings, Color moderateOver, Color outlineColor, float outlineWidth, int soundAlertInterval, bool soundWarnings, bool textWarnings, Color upColor, ValueChartStyle vct)
        {
            return _indicator.RValueCharts(input, changeBackground, dotDraw, dotOffset, dotOverBought, dotOverSold, downColor, extremeOver, length, mainChartSettings, moderateOver, outlineColor, outlineWidth, soundAlertInterval, soundWarnings, textWarnings, upColor, vct);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// Value Charts by David Stendhal; Converted to NinjaTrader by TradingStudies.com
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.RValueCharts RValueCharts(bool changeBackground, bool dotDraw, int dotOffset, Color dotOverBought, Color dotOverSold, Color downColor, Color extremeOver, int length, bool mainChartSettings, Color moderateOver, Color outlineColor, float outlineWidth, int soundAlertInterval, bool soundWarnings, bool textWarnings, Color upColor, ValueChartStyle vct)
        {
            return _indicator.RValueCharts(Input, changeBackground, dotDraw, dotOffset, dotOverBought, dotOverSold, downColor, extremeOver, length, mainChartSettings, moderateOver, outlineColor, outlineWidth, soundAlertInterval, soundWarnings, textWarnings, upColor, vct);
        }

        /// <summary>
        /// Value Charts by David Stendhal; Converted to NinjaTrader by TradingStudies.com
        /// </summary>
        /// <returns></returns>
        public Indicator.RValueCharts RValueCharts(Data.IDataSeries input, bool changeBackground, bool dotDraw, int dotOffset, Color dotOverBought, Color dotOverSold, Color downColor, Color extremeOver, int length, bool mainChartSettings, Color moderateOver, Color outlineColor, float outlineWidth, int soundAlertInterval, bool soundWarnings, bool textWarnings, Color upColor, ValueChartStyle vct)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.RValueCharts(input, changeBackground, dotDraw, dotOffset, dotOverBought, dotOverSold, downColor, extremeOver, length, mainChartSettings, moderateOver, outlineColor, outlineWidth, soundAlertInterval, soundWarnings, textWarnings, upColor, vct);
        }
    }
}
#endregion
