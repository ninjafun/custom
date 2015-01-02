// 
// Added comments, simplied code.	KBJ 30-Oct-2007
//
// For a FULL description of this indicator and how to use it,
// please see:  http://www.kumotrader.com/ichimoku_wiki/
//
// OR
// please see:  http://www.forexabode.com/technical-analysis/ichimoku-cloud
//
// Revised to display the same colors for SenkouSpanA&B as found in above description.	31-Oct-2007
//
// This code was subsequently hacked up by goldfly on 25-Mar-2008

/* ***********************
Revisions and Modifications: Zeos6

04/08/11:	Added code to allow selection of up and down band area colors.
10/12/12:	Fixed up some of the properties.
			Added a new reference for the Ichimoku cloud.
			Shifted the cloud forward by an amount equal to the Kijun value.
			Added the Chikou Span, Tenkan Sen, and Kijun Sen lines with the option to display or not display these three lines.
*/ //*********************

#region Using declarations
	using System;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.ComponentModel;
	using System.Xml.Serialization;
	using NinjaTrader.Cbi;
	using NinjaTrader.Data;
	using NinjaTrader.Gui.Chart;
#endregion

// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    /// <summary>
    /// Enter the description of your new custom indicator here
    /// </summary>
    [Description("IchimokuCloud Kinko Hyo - \"Equilibrium Chart at a Glance\" - For a FULL description of this indicator and how to use it, please see:  http://www.forexabode.com/technical-analysis/ichimoku-cloud")]
    [Gui.Design.DisplayName("IchimokuCloud Kinko Hyo")]
    public class IchimokuCloud : Indicator
    {
        #region Variables
			private bool	displayCloudOnly			= false;		// Default setting for DisplayCloudOnly.
            private int		periodFast					= 9;			// Default setting for PeriodFast (Tenkan).
            private int		periodMedium				= 26;			// Default setting for PeriodMedium (Kijun).
            private int		periodSlow					= 52;			// Default setting for PeriodSlow (Chikou Span).
			private int		cloudColorOpacity			= 40;			// Value 0 indicates complete transparency. Value 100 indicates complete opacity.
			private Color	cloudAreaColorUp			= Color.Green;	// Default positive cloud area color.
			private Color	cloudAreaColorDown			= Color.Red;	// Default negative cloud area color.
			private DataSeries SenkouSpanA, SenkouSpanB;
        #endregion

        /// <summary>
        /// This method is used to configure the indicator and is called once before any bar data is loaded.
        /// </summary>
        protected override void Initialize()
        {
            Add( new Plot( new Pen(Color.Red,        1), PlotStyle.Line,	"TenkanSen"));	// Conversion line.
            Add( new Plot( new Pen(Color.Purple,     1), PlotStyle.Line,	"KijunSen"));	// Base line.
			Add( new Plot( new Pen(Color.MediumBlue, 1), PlotStyle.Line,	"ChikouSpan"));	// Lagging span.

			SenkouSpanA = new DataSeries(this, MaximumBarsLookBack.Infinite);				// Senkou Span A.
			SenkouSpanB = new DataSeries(this, MaximumBarsLookBack.Infinite);				// Senkou Span B.

            CalculateOnBarClose	= true;		// Updated once per bar (as opposed to on every tick.)
            Overlay				= true;		// Display on top of the price in panel #1.
            PriceTypeSupported	= false;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
			if ((CurrentBar < PeriodMedium) || (CurrentBar < PeriodFast))
				return;										// Wait until we have enough bars.

			//
			// The following is a "cloud display" that shows what support/resistance values might be based on prior hi/low averages.
			// The cloud is defined by span-A & span-B.  Span-A is the average of the TenkanSen & Kijunsen, but shifted PeriodMedium
			// bars forward in time, and Span-B is the long-term (slow) average of the lowest-low & highest-high, shifted forward as well.
			//
			if ((CurrentBar < PeriodFast+PeriodMedium) || (CurrentBar < PeriodMedium+PeriodMedium) || (CurrentBar < PeriodSlow+PeriodMedium)) return;

			double fastSum		= MAX(High,PeriodFast)[0] + MIN(Low,PeriodFast)[0];
			double mediumSum	= MAX(High,PeriodMedium)[0] + MIN(Low,PeriodMedium)[0];

        	TenkanSen.Set(fastSum / 2.0);
        	KijunSen.Set(mediumSum / 2.0);
			ChikouSpan.Set(periodMedium, Close[0]);

			SenkouSpanA.Set( ( fastSum + mediumSum ) / 4.0 );
			SenkouSpanB.Set( ( MAX(High,PeriodSlow)[0] + MIN(Low,PeriodSlow)[0] ) / 2.0 );

			if (displayCloudOnly)
			{
				PlotColors[0][0] = Color.Transparent;
				PlotColors[1][0] = Color.Transparent;
				PlotColors[2][0] = Color.Transparent;
			}

        }

        #region Properties
        	[Browsable(false)]
        	[XmlIgnore]
        	public DataSeries TenkanSen
        	{
            	get { return Values[0]; }
        	}

        	[Browsable(false)]
        	[XmlIgnore]
        	public DataSeries KijunSen
        	{
            	get { return Values[1]; }
        	}

        	[Browsable(false)]
        	[XmlIgnore]
        	public DataSeries ChikouSpan
        	{
            	get { return Values[2]; }
        	}

        	[Browsable(false)]
        	[XmlIgnore]
        	public DataSeries _SenkouSpanA
        	{
            	get { return SenkouSpanA; }
        	}

        	[Browsable(false)]
        	[XmlIgnore]
        	public DataSeries _SenkouSpanB
        	{
            	get { return SenkouSpanB; }
        	}

        	[Description("If set TRUE, displays only the Senkou Span lines (i.e. the cloud). If set FALSE, displays the Tenkan Sen conversion line, the Kijun Sen base line, and the Chikou Span (lagging span) line in addition to the cloud (i.e. the Senkou Span A and B lines).")]
        	[GridCategory("Parameters")]
        	[Gui.Design.DisplayName("Display Cloud Only?")]
    		public bool DisplayCloudOnly
        	{
            	get { return displayCloudOnly; }
            	set { displayCloudOnly = value; }
        	}

        	[Description("Tenkan Sen (conversion line) period.  The default value 9 represents 1.5 Japanese working weeks [at 6 working days per week]. Often set to 7 in countries with 5 day work weeks.")]
        	[GridCategory("Parameters")]
        	[Gui.Design.DisplayName("Period: Fast (Tenkan Sen)")]
        	public int PeriodFast
        	{
            	get { return periodFast; }
            	set { periodFast = Math.Max(1, value); }
        	}

        	[Description("Kijun Sen (baseline) period.  The default value 26 represents one Japanese working month [at 6 working days per week]. Often set to 22 in countries with 5 day work weeks.")]
        	[GridCategory("Parameters")]
        	[Gui.Design.DisplayName("Period: Medium (Kijun Sen)")]
        	public int PeriodMedium
        	{
            	get { return periodMedium; }
            	set { periodMedium = Math.Max(1, value); }
        	}

        	[Description("Chikou Span period. The default value 52 represents two Japanese working months [at 6 working days per week]. Often set to 44 in countries with 5 day work weeks.")]
        	[GridCategory("Parameters")]
        	[Gui.Design.DisplayName("Period: Slow (Chikou Span)")]
        	public int PeriodSlow
        	{
            	get { return periodSlow; }
            	set { periodSlow = Math.Max(1, value); }
        	}

        	[Description("Cloud color opacity. Value 0 indicates complete transparency. Value 100 indicates complete opaqueness.")]
        	[GridCategory("Colors")]
        	[Gui.Design.DisplayName("Cloud Color Opacity")]
        	public int CloudColorOpacity
        	{
            	get { return cloudColorOpacity; }
            	set { cloudColorOpacity = Math.Min(100, Math.Max(0, value)); }
        	}

			[XmlIgnore()]
        	[Description("Cloud area color up; i.e. the color of the positive cloud.")]
        	[GridCategory("Colors")]
			[Gui.Design.DisplayNameAttribute("Color of Positive Cloud")]
        	public Color CloudAreaColorUp
        	{
            	get { return cloudAreaColorUp; }
            	set { cloudAreaColorUp = value; }
        	}

			[Browsable(false)]
			public string CloudAreaColorUpSerialize
			{
				get { return NinjaTrader.Gui.Design.SerializableColor.ToString(cloudAreaColorUp); }
				set { cloudAreaColorUp = NinjaTrader.Gui.Design.SerializableColor.FromString(value); }
			}

			[XmlIgnore()]
        	[Description("Cloud area color down; i.e. the color of the negative cloud.")]
        	[GridCategory("Colors")]
			[Gui.Design.DisplayNameAttribute("Color of Negative Cloud")]
        	public Color CloudAreaColorDown
        	{
            	get { return cloudAreaColorDown; }
            	set { cloudAreaColorDown = value; }
        	}

			[Browsable(false)]
			public string CloudAreaColorDownSerialize
			{
				get { return NinjaTrader.Gui.Design.SerializableColor.ToString(cloudAreaColorDown); }
				set { cloudAreaColorDown = NinjaTrader.Gui.Design.SerializableColor.FromString(value); }
			}
        #endregion

		#region Miscellaneous
			public override void Plot(Graphics graphics, Rectangle bounds, double min, double max)
			{
				if (Bars == null || ChartControl == null) return;

            	if (Color.Transparent == cloudAreaColorUp && Color.Transparent == cloudAreaColorDown || 0 == cloudColorOpacity)
				{
           			base.Plot(graphics, bounds, min, max);		// Call base Plot() method to paint defined Plots.
                	return;										// If cloudAreaColor colors are transparent or cloudColorOpacity is 0
																// there is nothing to paint so return to avoid unnecessary calculations.
            	}

				SolidBrush brush		= null;					// Set current brush color here.
				SolidBrush brushUP		= new SolidBrush(Color.FromArgb(cloudColorOpacity, cloudAreaColorUp));
				SolidBrush brushDOWN	= new SolidBrush(Color.FromArgb(cloudColorOpacity, cloudAreaColorDown));

				int	barWidth = ChartControl.ChartStyle.GetBarPaintWidth(ChartControl.BarWidth);
				SmoothingMode oldSmoothingMode = graphics.SmoothingMode;
				GraphicsPath path = new GraphicsPath();			// Create a new path.

				brush = brushUP;								// Start with the upwards color.
				int barcount = 0;								// Start with leftmost bar.
				bool firstbar = true;							// Plotting the first bar.

				while (barcount < ChartControl.BarsPainted)		// Continue until all bars have been painted.
				{
					int count = 0;								// Counter for innner loop.
					for (int seriesCount = 0; seriesCount < 2; seriesCount++)
					{
						int	lastX = -1;
						int	lastY = -1;
						DataSeries	series = (0 == seriesCount) ? SenkouSpanA : SenkouSpanB;

						for (count = barcount; count < ChartControl.BarsPainted; count++)
						{
							int idx = ChartControl.LastBarPainted - ChartControl.BarsPainted + 1 + count - periodMedium;
							if (idx < 0 || idx >= Input.Count || (!ChartControl.ShowBarsRequired && idx < BarsRequired))
								continue;

							bool senkouSpanAHasValue = SenkouSpanA.IsValidPlot(idx) && SenkouSpanA.Get(idx) > 0;
							bool senkouSpanBHasValue = SenkouSpanB.IsValidPlot(idx) && SenkouSpanB.Get(idx) > 0;
                			if (!senkouSpanAHasValue || !senkouSpanAHasValue)	// If we don't have valid value for the series then skip the loop step
																				// as there is nothing to plot. So ignore the entry.
                    			continue;

							double val	= series.Get(idx);						// Get next y-value to be plotted.

							int	x = (int) (ChartControl.CanvasRight - ChartControl.BarMarginRight - barWidth / 2
							  	+ (count - ChartControl.BarsPainted + 1) * ChartControl.BarSpace) + 1;

							int	y = (int) ((bounds.Y + bounds.Height) - ((val - min ) / Gui.Chart.ChartControl.MaxMinusMin(max, min)) * bounds.Height);

							double val0 = SenkouSpanA.Get(idx);
							double val1 = SenkouSpanB.Get(idx);

							if (((val0 > val1) && (brush != brushUP))	// Now going in wrong direction?
						 		|| ((val0 < val1) && (brush != brushDOWN)))
							{											// Yes.  Done with this loop.
								if (lastX >= 0)							// Was there a last point?
								{										// Yes.  Connect it to the position half-way to this one.
									path.AddLine( lastX, lastY, (x +lastX) / 2, (lastY + y)/2);
																		// Plot vertex of cross-over of the lines (1/2 way point).
								}
								break;									// Done, exit inner loop to change color.
							}

							if (firstbar == false)						// Is this the first plotted bar of the chart?
							{											// No.  Plot all bars after the first one.
								if (count == barcount)					// First bar after direction change (and color swap)?
								{										// Yes.  Add line segment for cross-over, 1/2 bar back.
									double valm1 = series.Get(idx-1);	// Get prior y-value to be plotted.
									lastX = x - ChartControl.BarSpace/2;// Back up 1/2 a bar for x-value.
									lastY = (y + (int) ((bounds.Y + bounds.Height) - ((valm1 - min ) / Gui.Chart.ChartControl.MaxMinusMin(max, min)) * bounds.Height))/2;
								}

								path.AddLine(lastX, lastY, x, y);		// Connect last point to this one.
							}
							firstbar = false;							// No longer the first bar.
							lastX = x;									// Save current position for next time, so we can connect the dots.
							lastY = y;
						}

						path.Reverse();									// Go back the other direction.
					}

					graphics.SmoothingMode = SmoothingMode.AntiAlias;
					graphics.FillPath(brush, path);
					path.Reset();										// Eliminate points already colored.

					barcount = count;									// Get ready to process next segment.
					brush = (brush == brushUP) ? brushDOWN : brushUP;	// Switch colors for next segment.
				}

				graphics.SmoothingMode = oldSmoothingMode;				// Restore smoothing mode before exiting.
				brushUP.Dispose();										// Dispose of brushes.
				brushDOWN.Dispose();
				path.Dispose();											// Dispose of path.
				base.Plot(graphics, bounds, min, max);					// Call the base Plot() method to paint defined Plots.
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
        private IchimokuCloud[] cacheIchimokuCloud = null;

        private static IchimokuCloud checkIchimokuCloud = new IchimokuCloud();

        /// <summary>
        /// IchimokuCloud Kinko Hyo - please see http://www.forexabode.com/technical-analysis/ichimoku-cloud
        /// </summary>
        /// <returns></returns>
        public IchimokuCloud IchimokuCloud(int periodFast, int periodMedium, int periodSlow)
        {
            return IchimokuCloud(Input, periodFast, periodMedium, periodSlow);
        }

        /// <summary>
        /// IchimokuCloud Kinko Hyo - please see http://www.forexabode.com/technical-analysis/ichimoku-cloud
        /// </summary>
        /// <returns></returns>
        public IchimokuCloud IchimokuCloud(Data.IDataSeries input, int periodFast, int periodMedium, int periodSlow)
        {
            checkIchimokuCloud.PeriodFast = periodFast;
            periodFast = checkIchimokuCloud.PeriodFast;
            checkIchimokuCloud.PeriodMedium = periodMedium;
            periodMedium = checkIchimokuCloud.PeriodMedium;
            checkIchimokuCloud.PeriodSlow = periodSlow;
            periodSlow = checkIchimokuCloud.PeriodSlow;

            if (cacheIchimokuCloud != null)
                for (int idx = 0; idx < cacheIchimokuCloud.Length; idx++)
                    if (cacheIchimokuCloud[idx].PeriodFast == periodFast && cacheIchimokuCloud[idx].PeriodMedium == periodMedium && cacheIchimokuCloud[idx].PeriodSlow == periodSlow && cacheIchimokuCloud[idx].EqualsInput(input))
                        return cacheIchimokuCloud[idx];

            IchimokuCloud indicator = new IchimokuCloud();
            indicator.SetUp();
            indicator.CalculateOnBarClose = CalculateOnBarClose;
            indicator.Input = input;
            indicator.PeriodFast = periodFast;
            indicator.PeriodMedium = periodMedium;
            indicator.PeriodSlow = periodSlow;

            IchimokuCloud[] tmp = new IchimokuCloud[cacheIchimokuCloud == null ? 1 : cacheIchimokuCloud.Length + 1];
            if (cacheIchimokuCloud != null)
                cacheIchimokuCloud.CopyTo(tmp, 0);
            tmp[tmp.Length - 1] = indicator;
            cacheIchimokuCloud = tmp;
            Indicators.Add(indicator);

            return indicator;
        }

    }
}

// This namespace holds all market analyzer column definitions and is required. Do not change it.
namespace NinjaTrader.MarketAnalyzer
{
    public partial class Column : ColumnBase
    {
        /// <summary>
        /// IchiCloud Kinko Hyo - please see http: http://www.forexabode.com/technical-analysis/ichimoku-cloud
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.IchimokuCloud IchimokuCloud(int periodFast, int periodMedium, int periodSlow)
        {
            return _indicator.IchimokuCloud(Input, periodFast, periodMedium, periodSlow);
        }

        /// <summary>
        /// IchiCloud Kinko Hyo - please see http://www.forexabode.com/technical-analysis/ichimoku-cloud
        /// </summary>
        /// <returns></returns>
        public Indicator.IchimokuCloud IchimokuCloud(Data.IDataSeries input, int periodFast, int periodMedium, int periodSlow)
        {
            return _indicator.IchimokuCloud(input, periodFast, periodMedium, periodSlow);
        }

    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// IchiCloud Kinko Hyo - please see http://www.forexabode.com/technical-analysis/ichimoku-cloud
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.IchimokuCloud IchimokuCloud(int periodFast, int periodMedium, int periodSlow)
        {
            return _indicator.IchimokuCloud(Input, periodFast, periodMedium, periodSlow);
        }

        /// <summary>
        /// IchiCloud Kinko Hyo - please see http://www.forexabode.com/technical-analysis/ichimoku-cloud
        /// </summary>
        /// <returns></returns>
        public Indicator.IchimokuCloud IchimokuCloud(Data.IDataSeries input, int periodFast, int periodMedium, int periodSlow)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.IchimokuCloud(input, periodFast, periodMedium, periodSlow);
        }

    }
}
#endregion
