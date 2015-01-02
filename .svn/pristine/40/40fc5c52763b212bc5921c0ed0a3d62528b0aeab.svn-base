#region Using declarations
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
#endregion

// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    /// <summary>
    /// EMAs
    /// </summary>
    [Description("EMAs")]
    public class GrabRange : Indicator
    {
        #region Variables
        // Wizard generated variables
        private int period = 34; // Default setting for Period
        private Color barColorDown = Color.LightSteelBlue;//.Red;
        private Color barColorUp = Color.SteelBlue;//.Lime;
        private SolidBrush brushDown = null;
        private SolidBrush brushUp = null;
        private SolidBrush brushGreen = null;
        private SolidBrush brushChartreuse = null;
        private SolidBrush brushRed = null;
        private SolidBrush brushMaroon = null;
        private SolidBrush brushLightSteelBlue = null;
        private SolidBrush brushSteelBlue = null;
        private Color shadowColor = Color.Black;
        private Pen shadowPen = null;
        private int shadowWidth = 1;
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the indicator and is called once before any bar data is loaded.
        /// </summary>
        protected override void Initialize()
        {

            Add(new Plot(Color.Gray, PlotStyle.Line, "HAOpen"));
            Add(new Plot(Color.Gray, PlotStyle.Line, "HAHigh"));
            Add(new Plot(Color.Gray, PlotStyle.Line, "HALow"));
            Add(new Plot(Color.Gray, PlotStyle.Line, "HAClose"));
            Add(new Plot(Color.FromKnownColor(KnownColor.Crimson), PlotStyle.Line, "PlotEMA_H"));
            Add(new Plot(Color.FromKnownColor(KnownColor.DodgerBlue), PlotStyle.Line, "PlotEMT_C"));
            Add(new Plot(Color.FromKnownColor(KnownColor.Lime), PlotStyle.Line, "PlotEMA_L"));
            //brushChartreuse = new SolidBrush(Color.Chartreuse);
            //brushGreen = new SolidBrush(Color.Green);
            //brushRed = new SolidBrush(Color.Red);
            //brushMaroon = new SolidBrush(Color.Maroon);
            //brushLightSteelBlue = new SolidBrush(Color.LightSteelBlue);
            //brushSteelBlue = new SolidBrush(Color.SteelBlue);
            PaintPriceMarkers = false;
            CalculateOnBarClose = false;
            PlotsConfigurable = false;
            Overlay = true;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            if (CurrentBar < Period) return;
            if (Displacement + (CalculateOnBarClose ? 1 : 0) > 0 && CurrentBar > 0 && BarColorSeries[1] != Color.Transparent)
                InitColorSeries();
            BarColorSeries.Set(Math.Max(0, CurrentBar + Math.Max(0, Displacement) + (CalculateOnBarClose ? 1 : 0)), Color.Transparent);
            CandleOutlineColorSeries.Set(Math.Max(0, CurrentBar + Math.Max(0, Displacement) + (CalculateOnBarClose ? 1 : 0)), Color.Transparent);
            // Use this method for calculating your indicator values. Assign a value to each
            // plot below by replacing 'Close[0]' with your own formula.

            PlotEMA_H.Set(CurrentBar == 0 ? High[0] : High[0] * (2.0 / (1 + Period)) + (1 - (2.0 / (1 + Period))) * PlotEMA_H[1]);
            PlotEMT_C.Set(CurrentBar == 0 ? Close[0] : Close[0] * (2.0 / (1 + Period)) + (1 - (2.0 / (1 + Period))) * PlotEMT_C[1]);
            PlotEMA_L.Set(CurrentBar == 0 ? Low[0] : Low[0] * (2.0 / (1 + Period)) + (1 - (2.0 / (1 + Period))) * PlotEMA_L[1]);

            if (CurrentBar == 0)
            {
                HAOpen.Set(Open[0]);
                HAHigh.Set(High[0]);
                HALow.Set(Low[0]);
                HAClose.Set(Close[0]);
                return;
            }
            if (Close[0] <= PlotEMA_H[0] && Close[0] >= PlotEMA_L[0])
            {
                HAOpen.Set(Open[0]);
                HAHigh.Set(High[0]);
                HALow.Set(Low[0]);
                HAClose.Set(Close[0]);
            }
            //else if (Close[0] < PlotEMA_L[0])
            //{
            //    brushUp = brushRed;
            //    brushDown = brushMaroon;
            //    HAOpen.Set(Open[0]);
            //    HAHigh.Set(High[0]);
            //    HALow.Set(Low[0]);
            //    HAClose.Set(Close[0]);
            //}
            //else
            //{
            //    brushUp = brushLightSteelBlue;
            //    brushDown = brushSteelBlue;
            //    HAOpen.Set(Open[0]);
            //    HAHigh.Set(High[0]);
            //    HALow.Set(Low[0]);
            //    HAClose.Set(Close[0]);
            //}
        }

        #region Properties
        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries PlotEMA_H
        {
            get { return Values[4]; }
        }

        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries PlotEMT_C
        {
            get { return Values[5]; }
        }

        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries PlotEMA_L
        {
            get { return Values[6]; }
        }

        [Description("Number of periods")]
        [GridCategory("Parameters")]
        public int Period
        {
            get { return period; }
            set { period = Math.Max(1, value); }
        }


        [Browsable(false)]
        [XmlIgnore]
        public DataSeries HAOpen
        {
            get { return Values[0]; }
        }

        [Browsable(false)]
        [XmlIgnore]
        public DataSeries HAHigh
        {
            get { return Values[1]; }
        }

        [Browsable(false)]
        [XmlIgnore]
        public DataSeries HALow
        {
            get { return Values[2]; }
        }

        [Browsable(false)]
        [XmlIgnore]
        public DataSeries HAClose
        {
            get { return Values[3]; }
        }

        [XmlIgnore]
        [Description("Color of down bars.")]
        [Category("Visual")]
        [Gui.Design.DisplayNameAttribute("Down color")]
        public Color BarColorDown
        {
            get { return barColorDown; }
            set { barColorDown = value; }
        }

        /// <summary>
        /// </summary>
        [Browsable(false)]
        public string BarColorDownSerialize
        {
            get { return Gui.Design.SerializableColor.ToString(barColorDown); }
            set { barColorDown = Gui.Design.SerializableColor.FromString(value); }
        }

        /// <summary>
        /// </summary>
        [XmlIgnore]
        [Description("Color of up bars.")]
        [Category("Visual")]
        [Gui.Design.DisplayNameAttribute("Up color")]
        public Color BarColorUp
        {
            get { return barColorUp; }
            set { barColorUp = value; }
        }

        /// <summary>
        /// </summary>
        [Browsable(false)]
        public string BarColorUpSerialize
        {
            get { return Gui.Design.SerializableColor.ToString(barColorUp); }
            set { barColorUp = Gui.Design.SerializableColor.FromString(value); }
        }

        /// <summary>
        /// </summary>
        [XmlIgnore]
        [Description("Color of shadow line.")]
        [Category("Visual")]
        [Gui.Design.DisplayNameAttribute("Shadow color")]
        public Color ShadowColor
        {
            get { return shadowColor; }
            set { shadowColor = value; }
        }

        /// <summary>
        /// </summary>
        [Browsable(false)]
        public string ShadowColorSerialize
        {
            get { return Gui.Design.SerializableColor.ToString(shadowColor); }
            set { shadowColor = Gui.Design.SerializableColor.FromString(value); }
        }

        /// <summary>
        /// </summary>
        [Description("Width of shadow line.")]
        [Category("Visual")]
        [Gui.Design.DisplayNameAttribute("Shadow width")]
        public int ShadowWidth
        {
            get { return shadowWidth; }
            set { shadowWidth = Math.Max(value, 1); }
        }
        #endregion

        #region Miscellaneous

        private void InitColorSeries()
        {
            for (int i = 0; i <= CurrentBar + Displacement + (CalculateOnBarClose ? 1 : 0); i++)
            {
                BarColorSeries.Set(i, Color.Transparent);
                CandleOutlineColorSeries.Set(i, Color.Transparent);
            }
        }

        protected override void OnStartUp()
        {
            if (ChartControl == null || Bars == null)
                return;

            brushUp = new SolidBrush(barColorUp);
            brushDown = new SolidBrush(barColorDown);
            shadowPen = new Pen(shadowColor, shadowWidth);
        }

        protected override void OnTermination()
        {
            if (brushUp != null) brushUp.Dispose();
            if (brushDown != null) brushDown.Dispose();
            if (shadowPen != null) shadowPen.Dispose();
        }

        public override void GetMinMaxValues(ChartControl chartControl, ref double min, ref double max)
        {
            if (Bars == null || ChartControl == null)
                return;

            for (int idx = FirstBarIndexPainted; idx <= LastBarIndexPainted; idx++)
            {
                double tmpHigh = HAHigh.Get(idx);
                double tmpLow = HALow.Get(idx);

                if (tmpHigh != 0 && tmpHigh > max)
                    max = tmpHigh;
                if (tmpLow != 0 && tmpLow < min)
                    min = tmpLow;
            }
        }

        public override void Plot(Graphics graphics, Rectangle bounds, double min, double max)
        {
            if (Bars == null || ChartControl == null)
                return;

            int barPaintWidth = Math.Max(3, 1 + 2 * ((int)Bars.BarsData.ChartStyle.BarWidth - 1) + 2 * shadowWidth);

            for (int idx = FirstBarIndexPainted; idx <= LastBarIndexPainted; idx++)
            {
                if (idx - Displacement < 0 || idx - Displacement >= BarsArray[0].Count || (!ChartControl.ShowBarsRequired && idx - Displacement < BarsRequired))
                    continue;
                double valH = HAHigh.Get(idx);
                double valL = HALow.Get(idx);
                double valC = HAClose.Get(idx);
                double valO = HAOpen.Get(idx);
                int x = ChartControl.GetXByBarIdx(BarsArray[0], idx);
                int y1 = ChartControl.GetYByValue(this, valO);
                int y2 = ChartControl.GetYByValue(this, valH);
                int y3 = ChartControl.GetYByValue(this, valL);
                int y4 = ChartControl.GetYByValue(this, valC);

                graphics.DrawLine(shadowPen, x, y2, x, y3);

                if (y4 == y1)
                    graphics.DrawLine(shadowPen, x - barPaintWidth / 2, y1, x + barPaintWidth / 2, y1);
                else
                {
                    if (y4 > y1)
                        graphics.FillRectangle(brushDown, x - barPaintWidth / 2, y1, barPaintWidth, y4 - y1);
                    else
                        graphics.FillRectangle(brushUp, x - barPaintWidth / 2, y4, barPaintWidth, y1 - y4);
                    graphics.DrawRectangle(shadowPen, (x - barPaintWidth / 2) + (shadowPen.Width / 2), Math.Min(y4, y1), barPaintWidth - shadowPen.Width, Math.Abs(y4 - y1));
                }
            }
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
        private GrabRange[] cacheGrabRange = null;

        private static GrabRange checkGrabRange = new GrabRange();

        /// <summary>
        /// EMAs
        /// </summary>
        /// <returns></returns>
        public GrabRange GrabRange(int period)
        {
            return GrabRange(Input, period);
        }

        /// <summary>
        /// EMAs
        /// </summary>
        /// <returns></returns>
        public GrabRange GrabRange(Data.IDataSeries input, int period)
        {
            if (cacheGrabRange != null)
                for (int idx = 0; idx < cacheGrabRange.Length; idx++)
                    if (cacheGrabRange[idx].Period == period && cacheGrabRange[idx].EqualsInput(input))
                        return cacheGrabRange[idx];

            lock (checkGrabRange)
            {
                checkGrabRange.Period = period;
                period = checkGrabRange.Period;

                if (cacheGrabRange != null)
                    for (int idx = 0; idx < cacheGrabRange.Length; idx++)
                        if (cacheGrabRange[idx].Period == period && cacheGrabRange[idx].EqualsInput(input))
                            return cacheGrabRange[idx];

                GrabRange indicator = new GrabRange();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.Period = period;
                Indicators.Add(indicator);
                indicator.SetUp();

                GrabRange[] tmp = new GrabRange[cacheGrabRange == null ? 1 : cacheGrabRange.Length + 1];
                if (cacheGrabRange != null)
                    cacheGrabRange.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheGrabRange = tmp;
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
        /// EMAs
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.GrabRange GrabRange(int period)
        {
            return _indicator.GrabRange(Input, period);
        }

        /// <summary>
        /// EMAs
        /// </summary>
        /// <returns></returns>
        public Indicator.GrabRange GrabRange(Data.IDataSeries input, int period)
        {
            return _indicator.GrabRange(input, period);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// EMAs
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.GrabRange GrabRange(int period)
        {
            return _indicator.GrabRange(Input, period);
        }

        /// <summary>
        /// EMAs
        /// </summary>
        /// <returns></returns>
        public Indicator.GrabRange GrabRange(Data.IDataSeries input, int period)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.GrabRange(input, period);
        }
    }
}
#endregion
