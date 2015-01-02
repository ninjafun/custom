using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
using SlingShot.Utility;

namespace NinjaTrader.Indicator
{
    [Description("SlingShot Indicator - Paints Region between two moving averages of your choice. Created by TradingStudies.com")]
    public class SlingShot : Indicator
    {
        private MovingAverageType _slowAverageType = MovingAverageType.HMA;
        private PriceType _slowType = PriceType.Close;
        private MovingAverageType _fastAverageType = MovingAverageType.HMA;
        private PriceType _fastType = PriceType.Close;
        private int _slowAverage = 63;
        private int _fastAverage = 38;
        private Color _bandAreaColorUp = Color.Green;
        private Color _bandAreaColorDown = Color.Red;
        private int _opacity = 80;
        private IDataSeries _slowInput;
        private IDataSeries _fastInput;

        protected override void Initialize()
        {
            Add(new Plot(Color.Transparent, PlotStyle.Line, "FastMA"));
            Add(new Plot(Color.Transparent, PlotStyle.Line, "SlowMA"));
            ZOrder = -1;
            CalculateOnBarClose = true;
            Overlay = true;
            PriceTypeSupported = false;
            PaintPriceMarkers = false;
            Plots[0].Pen.Width = 2;
            Plots[1].Pen.Width = 2;
        }

        protected override void OnStartUp()
        {
            switch (_slowType)
            {
                case PriceType.Close:
                    _slowInput = Close;
                    break;
                case PriceType.High:
                    _slowInput = High;
                    break;
                case PriceType.Low:
                    _slowInput = Low;
                    break;
                case PriceType.Median:
                    _slowInput = Median;
                    break;
                case PriceType.Open:
                    _slowInput = Open;
                    break;
                case PriceType.Typical:
                    _slowInput = Typical;
                    break;
                default:
                    break;
            }
            switch (_fastType)
            {
                case PriceType.Close:
                    _fastInput = Close;
                    break;
                case PriceType.High:
                    _fastInput = High;
                    break;
                case PriceType.Low:
                    _fastInput = Low;
                    break;
                case PriceType.Median:
                    _fastInput = Median;
                    break;
                case PriceType.Open:
                    _fastInput = Open;
                    break;
                case PriceType.Typical:
                    _fastInput = Typical;
                    break;
                default:
                    break;
            }
        }

        protected override void OnBarUpdate()
        {
            double f;
            double s;
            if (CurrentBar <= Math.Max(FastAverage, SlowAverage)) return;
            switch (FastAverageType)
            {
                case MovingAverageType.EMA:
                    f = EMA(_fastInput, FastAverage)[0];
                    break;
                case MovingAverageType.SMA:
                    f = SMA(_fastInput, FastAverage)[0];
                    break;
                case MovingAverageType.TMA:
                    f = TMA(_fastInput, FastAverage)[0];
                    break;
                case MovingAverageType.WMA:
                    f = WMA(_fastInput, FastAverage)[0];
                    break;
                case MovingAverageType.VWMA:
                    f = VWMA(_fastInput, FastAverage)[0];
                    break;
                default:
                    f = HMA(_fastInput, FastAverage)[0];
                    break;
            }

            FastMA.Set(f);

            switch (SlowAverageType)
            {
                case MovingAverageType.EMA:
                    s = EMA(_slowInput, SlowAverage)[0];
                    break;
                case MovingAverageType.SMA:
                    s = SMA(_slowInput, SlowAverage)[0];
                    break;
                case MovingAverageType.TMA:
                    s = TMA(_slowInput, SlowAverage)[0];
                    break;
                case MovingAverageType.WMA:
                    s = WMA(_slowInput, SlowAverage)[0];
                    break;
                case MovingAverageType.VWMA:
                    s = VWMA(_slowInput, SlowAverage)[0];
                    break;
                default:
                    s = HMA(_slowInput, SlowAverage)[0];
                    break;
            }
            SlowMA.Set(s);

        }

        #region Properties

        [Browsable(false)]
        [XmlIgnore]
        public DataSeries FastMA
        {
            get { return Values[0]; }
        }

        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries SlowMA
        {
            get { return Values[1]; }
        }

        [Description("Fast MA Length")]
        [GridCategory("Parameters")]
        [Gui.Design.DisplayName("1. Fast MA Length")]
        public int FastAverage
        {
            get { return _fastAverage; }
            set { _fastAverage = Math.Max(1, value); }
        }

        [Description("Type of Fast Average")]
        [GridCategory("Parameters")]
        [Gui.Design.DisplayName("2. Fast MA Type")]
        public MovingAverageType FastAverageType
        {
            get { return _fastAverageType; }
            set { _fastAverageType = value; }
        }

        [Description("Input of Fast Average")]
        [GridCategory("Parameters")]
        [Gui.Design.DisplayName("3. Fast MA Input")]
        public PriceType FastAverageInput
        {
            get { return _fastType; }
            set { _fastType = value; }
        }

        [Description("Slow MA Length")]
        [GridCategory("Parameters")]
        [Gui.Design.DisplayName("4. Slow MA Length")]
        public int SlowAverage
        {
            get { return _slowAverage; }
            set { _slowAverage = Math.Max(1, value); }
        }

        [Description("Type of Slow Average")]
        [GridCategory("Parameters")]
        [Gui.Design.DisplayName("5. Slow MA Type")]
        public MovingAverageType SlowAverageType
        {
            get { return _slowAverageType; }
            set { _slowAverageType = value; }
        }

        [Description("Input of Slow Average")]
        [GridCategory("Parameters")]
        [Gui.Design.DisplayName("6. Slow MA Input")]
        public PriceType SlowAverageInput
        {
            get { return _slowType; }
            set { _slowType = value; }
        }

        [Description("Colored Region Opacity")]
        [GridCategory("Visual")]
        [Gui.Design.DisplayName("Colored Region Opacity")]
        public int Opacity
        {
            get { return _opacity; }
            set { _opacity = Math.Min(255, Math.Max(value, 0)); }
        }

        [Description("Color of Positive Region")]
        [GridCategory("Visual")]
        [Gui.Design.DisplayName("Color of Positive Region")]
        public Color BandAreaColorUp
        {
            get { return _bandAreaColorUp; }
            set { _bandAreaColorUp = value; }
        }

        [Browsable(false)]
        public string BandAreaColorUpSerialize
        {
            get { return Gui.Design.SerializableColor.ToString(_bandAreaColorUp); }
            set { _bandAreaColorUp = Gui.Design.SerializableColor.FromString(value); }
        }

        [Description("Color of Negative Region")]
        [GridCategory("Visual")]
        [Gui.Design.DisplayName("Color of Negative Region")]
        public Color BandAreaColorDown
        {
            get { return _bandAreaColorDown; }
            set { _bandAreaColorDown = value; }
        }

        [Browsable(false)]
        public string BandAreaColorDownSerialize
        {
            get { return Gui.Design.SerializableColor.ToString(_bandAreaColorDown); }
            set { _bandAreaColorDown = Gui.Design.SerializableColor.FromString(value); }
        }

        #endregion

        public override void Plot(Graphics graphics, Rectangle bounds, double min, double max)
        {
            base.Plot(graphics, bounds, min, max); // We call base Plot() method to paint defined Plots

            if (_bandAreaColorUp == Color.Transparent && _bandAreaColorDown == Color.Transparent || _opacity == 0)
                return; // if bandArea colors are transparent or opacity is 0 returning to avoid unnecessary calculations

            SolidBrush upBrush = new SolidBrush(Color.FromArgb(_opacity, _bandAreaColorUp));
            SolidBrush downBrush = new SolidBrush(Color.FromArgb(_opacity, _bandAreaColorDown));

            List<Point> points = new List<Point>();

            int lastX = -1;
            double lastValueFast = -1;
            double lastValueSlow = -1;
            SolidBrush brush = null;

            for (int idx = FirstBarIndexPainted; idx <= LastBarIndexPainted; idx++)
            {
                if (idx - Displacement < 0 || idx - Displacement >= BarsArray[0].Count || (!ChartControl.ShowBarsRequired && idx - Displacement < BarsRequired))
                    continue;

                bool fastHasValue = FastMA.IsValidPlot(idx) && FastMA.Get(idx) > 0;
                bool slowHasValue = SlowMA.IsValidPlot(idx) && SlowMA.Get(idx) > 0;
                if (!slowHasValue || !fastHasValue) //if we don't have valid values for BOTH moving averages then skip the loop step
                    continue;

                double valueFast = FastMA.Get(idx);
                double valueSlow = SlowMA.Get(idx);

                int x = ChartControl.GetXByBarIdx(BarsArray[0], idx);

                if (lastX >= 0)
                {
                    int yf0 = ChartControl.GetYByValue(this, lastValueFast);
                    int yf1 = ChartControl.GetYByValue(this, valueFast);
                    int ys0 = ChartControl.GetYByValue(this, lastValueSlow);
                    int ys1 = ChartControl.GetYByValue(this, valueSlow);

                    if (ys0 != yf0)
                        brush = ys0 < yf0 ? downBrush : upBrush;

                    if (Math.Sign(yf1 - ys1)!= 0 && Math.Sign(yf0 - ys0) != Math.Sign(yf1 - ys1) || points.Count == 0) // We have a cross or only start painting
                    {
                        if(points.Count > 0 && brush != null) //
                        {
                            SmoothingMode oldSmoothingMode = graphics.SmoothingMode;

                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.FillPolygon(brush, points.ToArray());
                            graphics.SmoothingMode = oldSmoothingMode;
                        }
                        points.Clear();
                        points.Add(new Point(lastX, yf0));
                        points.Add(new Point(x, yf1));
                        points.Add(new Point(x, ys1));
                        points.Add(new Point(lastX, ys0));
                        points.Add(new Point(lastX, yf0));
                    }
                    else
                    {
                        int pos = points.Count / 2;
                        points.Insert(pos, new Point(x, ys1));
                        points.Insert(pos, new Point(x, yf1));
                    }

                }
                // save as previous point
                lastX = x;
                lastValueFast = valueFast;
                lastValueSlow = valueSlow;
            }
            if (points.Count > 0 && brush != null)
            {
                SmoothingMode oldSmoothingMode = graphics.SmoothingMode;

                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.FillPolygon(brush, points.ToArray());
                graphics.SmoothingMode = oldSmoothingMode;
                points.Clear();
            }
        }
    }
}
namespace SlingShot.Utility
{

    public enum MovingAverageType
    {
        SMA,
        SMMA,
        TMA,
        WMA,
        VWMA,
        TEMA,
        HMA,
        EMA,
        VMA
    }

}

#region NinjaScript generated code. Neither change nor remove.
// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    public partial class Indicator : IndicatorBase
    {
        private SlingShot[] cacheSlingShot = null;

        private static SlingShot checkSlingShot = new SlingShot();

        /// <summary>
        /// SlingShot Indicator - Paints Region between two moving averages of your choice. Created by TradingStudies.com
        /// </summary>
        /// <returns></returns>
        public SlingShot SlingShot(Color bandAreaColorDown, Color bandAreaColorUp, int fastAverage, PriceType fastAverageInput, MovingAverageType fastAverageType, int opacity, int slowAverage, PriceType slowAverageInput, MovingAverageType slowAverageType)
        {
            return SlingShot(Input, bandAreaColorDown, bandAreaColorUp, fastAverage, fastAverageInput, fastAverageType, opacity, slowAverage, slowAverageInput, slowAverageType);
        }

        /// <summary>
        /// SlingShot Indicator - Paints Region between two moving averages of your choice. Created by TradingStudies.com
        /// </summary>
        /// <returns></returns>
        public SlingShot SlingShot(Data.IDataSeries input, Color bandAreaColorDown, Color bandAreaColorUp, int fastAverage, PriceType fastAverageInput, MovingAverageType fastAverageType, int opacity, int slowAverage, PriceType slowAverageInput, MovingAverageType slowAverageType)
        {
            if (cacheSlingShot != null)
                for (int idx = 0; idx < cacheSlingShot.Length; idx++)
                    if (cacheSlingShot[idx].BandAreaColorDown == bandAreaColorDown && cacheSlingShot[idx].BandAreaColorUp == bandAreaColorUp && cacheSlingShot[idx].FastAverage == fastAverage && cacheSlingShot[idx].FastAverageInput == fastAverageInput && cacheSlingShot[idx].FastAverageType == fastAverageType && cacheSlingShot[idx].Opacity == opacity && cacheSlingShot[idx].SlowAverage == slowAverage && cacheSlingShot[idx].SlowAverageInput == slowAverageInput && cacheSlingShot[idx].SlowAverageType == slowAverageType && cacheSlingShot[idx].EqualsInput(input))
                        return cacheSlingShot[idx];

            lock (checkSlingShot)
            {
                checkSlingShot.BandAreaColorDown = bandAreaColorDown;
                bandAreaColorDown = checkSlingShot.BandAreaColorDown;
                checkSlingShot.BandAreaColorUp = bandAreaColorUp;
                bandAreaColorUp = checkSlingShot.BandAreaColorUp;
                checkSlingShot.FastAverage = fastAverage;
                fastAverage = checkSlingShot.FastAverage;
                checkSlingShot.FastAverageInput = fastAverageInput;
                fastAverageInput = checkSlingShot.FastAverageInput;
                checkSlingShot.FastAverageType = fastAverageType;
                fastAverageType = checkSlingShot.FastAverageType;
                checkSlingShot.Opacity = opacity;
                opacity = checkSlingShot.Opacity;
                checkSlingShot.SlowAverage = slowAverage;
                slowAverage = checkSlingShot.SlowAverage;
                checkSlingShot.SlowAverageInput = slowAverageInput;
                slowAverageInput = checkSlingShot.SlowAverageInput;
                checkSlingShot.SlowAverageType = slowAverageType;
                slowAverageType = checkSlingShot.SlowAverageType;

                if (cacheSlingShot != null)
                    for (int idx = 0; idx < cacheSlingShot.Length; idx++)
                        if (cacheSlingShot[idx].BandAreaColorDown == bandAreaColorDown && cacheSlingShot[idx].BandAreaColorUp == bandAreaColorUp && cacheSlingShot[idx].FastAverage == fastAverage && cacheSlingShot[idx].FastAverageInput == fastAverageInput && cacheSlingShot[idx].FastAverageType == fastAverageType && cacheSlingShot[idx].Opacity == opacity && cacheSlingShot[idx].SlowAverage == slowAverage && cacheSlingShot[idx].SlowAverageInput == slowAverageInput && cacheSlingShot[idx].SlowAverageType == slowAverageType && cacheSlingShot[idx].EqualsInput(input))
                            return cacheSlingShot[idx];

                SlingShot indicator = new SlingShot();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.BandAreaColorDown = bandAreaColorDown;
                indicator.BandAreaColorUp = bandAreaColorUp;
                indicator.FastAverage = fastAverage;
                indicator.FastAverageInput = fastAverageInput;
                indicator.FastAverageType = fastAverageType;
                indicator.Opacity = opacity;
                indicator.SlowAverage = slowAverage;
                indicator.SlowAverageInput = slowAverageInput;
                indicator.SlowAverageType = slowAverageType;
                Indicators.Add(indicator);
                indicator.SetUp();

                SlingShot[] tmp = new SlingShot[cacheSlingShot == null ? 1 : cacheSlingShot.Length + 1];
                if (cacheSlingShot != null)
                    cacheSlingShot.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheSlingShot = tmp;
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
        /// SlingShot Indicator - Paints Region between two moving averages of your choice. Created by TradingStudies.com
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.SlingShot SlingShot(Color bandAreaColorDown, Color bandAreaColorUp, int fastAverage, PriceType fastAverageInput, MovingAverageType fastAverageType, int opacity, int slowAverage, PriceType slowAverageInput, MovingAverageType slowAverageType)
        {
            return _indicator.SlingShot(Input, bandAreaColorDown, bandAreaColorUp, fastAverage, fastAverageInput, fastAverageType, opacity, slowAverage, slowAverageInput, slowAverageType);
        }

        /// <summary>
        /// SlingShot Indicator - Paints Region between two moving averages of your choice. Created by TradingStudies.com
        /// </summary>
        /// <returns></returns>
        public Indicator.SlingShot SlingShot(Data.IDataSeries input, Color bandAreaColorDown, Color bandAreaColorUp, int fastAverage, PriceType fastAverageInput, MovingAverageType fastAverageType, int opacity, int slowAverage, PriceType slowAverageInput, MovingAverageType slowAverageType)
        {
            return _indicator.SlingShot(input, bandAreaColorDown, bandAreaColorUp, fastAverage, fastAverageInput, fastAverageType, opacity, slowAverage, slowAverageInput, slowAverageType);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// SlingShot Indicator - Paints Region between two moving averages of your choice. Created by TradingStudies.com
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.SlingShot SlingShot(Color bandAreaColorDown, Color bandAreaColorUp, int fastAverage, PriceType fastAverageInput, MovingAverageType fastAverageType, int opacity, int slowAverage, PriceType slowAverageInput, MovingAverageType slowAverageType)
        {
            return _indicator.SlingShot(Input, bandAreaColorDown, bandAreaColorUp, fastAverage, fastAverageInput, fastAverageType, opacity, slowAverage, slowAverageInput, slowAverageType);
        }

        /// <summary>
        /// SlingShot Indicator - Paints Region between two moving averages of your choice. Created by TradingStudies.com
        /// </summary>
        /// <returns></returns>
        public Indicator.SlingShot SlingShot(Data.IDataSeries input, Color bandAreaColorDown, Color bandAreaColorUp, int fastAverage, PriceType fastAverageInput, MovingAverageType fastAverageType, int opacity, int slowAverage, PriceType slowAverageInput, MovingAverageType slowAverageType)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.SlingShot(input, bandAreaColorDown, bandAreaColorUp, fastAverage, fastAverageInput, fastAverageType, opacity, slowAverage, slowAverageInput, slowAverageType);
        }
    }
}
#endregion
