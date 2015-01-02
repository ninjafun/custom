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
    /// SuperTrend Indicator
    /// </summary>
    [Description("SuperTrend Indicator developed by Roonius")]
    public class SuperTrend : Indicator
    {
        #region Variables
        // Wizard generated variables
            private int lenght = 14; // Default setting for Lenght
            private double multiplier = 2.618; // Default setting for Multiplier
			private bool showArrows = true;
        // User defined variables (add any user defined variables below)
			public BoolSeries Trend;
		//	private DataSeries TrendDown;
        #endregion

        /// <summary>
        /// This method is used to configure the indicator and is called once before any bar data is loaded.
        /// </summary>
        protected override void Initialize()
        {
            Add(new Plot(Color.Green, PlotStyle.Line, "UpTrend"));
            Add(new Plot(Color.Red, PlotStyle.Line, "DownTrend"));
            CalculateOnBarClose	= true;
            Overlay				= true;
            PriceTypeSupported	= false;
			Trend = new BoolSeries(this);
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            // Use this method for calculating your indicator values. Assign a value to each
            // plot below by replacing 'Close[0]' with your own formula.
			if (CurrentBar < Lenght)
				{ 
					Trend.Set(true);
					UpTrend.Set (Close[0]); 
					DownTrend.Set(Close[0]);
					return; 
				};
			if (Close[0]>DownTrend[1])
				Trend.Set(true);
			else
				if (Close[0]<UpTrend[1])
					Trend.Set(false);
				else
					Trend.Set(Trend[1]);
			if(Trend[0] && !Trend[1])
			{
				UpTrend.Set(Median[0] - ATR(Lenght)[0]*Multiplier);
				UpTrend.Set(1, DownTrend[1]);
				if (ShowArrows)
					DrawArrowUp(CurrentBar.ToString(), true, 0, UpTrend[0] - TickSize, Color.Blue);
			} else
				if (!Trend[0]  && Trend[1])
				{
					DownTrend.Set(Median[0] + ATR(Lenght)[0]*Multiplier);
					DownTrend.Set(1, UpTrend[1]);
					if (ShowArrows)
						DrawArrowDown(CurrentBar.ToString(), true, 0, DownTrend[0] + TickSize, Color.Red);
				} else
					if (Trend[0])
						UpTrend.Set((Median[0] - ATR(Lenght)[0]*Multiplier) > UpTrend[1] ? (Median[0] - ATR(Lenght)[0]*Multiplier) : UpTrend[1]);
					else
						DownTrend.Set((Median[0] + ATR(Lenght)[0]*Multiplier) < DownTrend[1] ? (Median[0] + ATR(Lenght)[0]*Multiplier) : DownTrend[1]);
        }

        #region Properties
        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries UpTrend
        {
            get { return Values[0]; }
        }

        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries DownTrend
        {
            get { return Values[1]; }
        }

        [Description("ATR Period")]
        [Category("Parameters")]
        public int Lenght
        {
            get { return lenght; }
            set { lenght = Math.Max(1, value); }
        }
        [Description("Show Arrows when Trendline is violated")]
        [Category("Parameters")]
        public bool ShowArrows
        {
            get { return showArrows; }
            set { showArrows = value; }
        }

        [Description("ATR Multiplier")]
        [Category("Parameters")]
        public double Multiplier
        {
            get { return multiplier; }
            set { multiplier = Math.Max(0.0001, value); }
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
        private SuperTrend[] cacheSuperTrend = null;

        private static SuperTrend checkSuperTrend = new SuperTrend();

        /// <summary>
        /// SuperTrend Indicator developed by Roonius
        /// </summary>
        /// <returns></returns>
        public SuperTrend SuperTrend(int lenght, double multiplier, bool showArrows)
        {
            return SuperTrend(Input, lenght, multiplier, showArrows);
        }

        /// <summary>
        /// SuperTrend Indicator developed by Roonius
        /// </summary>
        /// <returns></returns>
        public SuperTrend SuperTrend(Data.IDataSeries input, int lenght, double multiplier, bool showArrows)
        {
            if (cacheSuperTrend != null)
                for (int idx = 0; idx < cacheSuperTrend.Length; idx++)
                    if (cacheSuperTrend[idx].Lenght == lenght && Math.Abs(cacheSuperTrend[idx].Multiplier - multiplier) <= double.Epsilon && cacheSuperTrend[idx].ShowArrows == showArrows && cacheSuperTrend[idx].EqualsInput(input))
                        return cacheSuperTrend[idx];

            lock (checkSuperTrend)
            {
                checkSuperTrend.Lenght = lenght;
                lenght = checkSuperTrend.Lenght;
                checkSuperTrend.Multiplier = multiplier;
                multiplier = checkSuperTrend.Multiplier;
                checkSuperTrend.ShowArrows = showArrows;
                showArrows = checkSuperTrend.ShowArrows;

                if (cacheSuperTrend != null)
                    for (int idx = 0; idx < cacheSuperTrend.Length; idx++)
                        if (cacheSuperTrend[idx].Lenght == lenght && Math.Abs(cacheSuperTrend[idx].Multiplier - multiplier) <= double.Epsilon && cacheSuperTrend[idx].ShowArrows == showArrows && cacheSuperTrend[idx].EqualsInput(input))
                            return cacheSuperTrend[idx];

                SuperTrend indicator = new SuperTrend();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.Lenght = lenght;
                indicator.Multiplier = multiplier;
                indicator.ShowArrows = showArrows;
                Indicators.Add(indicator);
                indicator.SetUp();

                SuperTrend[] tmp = new SuperTrend[cacheSuperTrend == null ? 1 : cacheSuperTrend.Length + 1];
                if (cacheSuperTrend != null)
                    cacheSuperTrend.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheSuperTrend = tmp;
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
        /// SuperTrend Indicator developed by Roonius
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.SuperTrend SuperTrend(int lenght, double multiplier, bool showArrows)
        {
            return _indicator.SuperTrend(Input, lenght, multiplier, showArrows);
        }

        /// <summary>
        /// SuperTrend Indicator developed by Roonius
        /// </summary>
        /// <returns></returns>
        public Indicator.SuperTrend SuperTrend(Data.IDataSeries input, int lenght, double multiplier, bool showArrows)
        {
            return _indicator.SuperTrend(input, lenght, multiplier, showArrows);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// SuperTrend Indicator developed by Roonius
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.SuperTrend SuperTrend(int lenght, double multiplier, bool showArrows)
        {
            return _indicator.SuperTrend(Input, lenght, multiplier, showArrows);
        }

        /// <summary>
        /// SuperTrend Indicator developed by Roonius
        /// </summary>
        /// <returns></returns>
        public Indicator.SuperTrend SuperTrend(Data.IDataSeries input, int lenght, double multiplier, bool showArrows)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.SuperTrend(input, lenght, multiplier, showArrows);
        }
    }
}
#endregion
