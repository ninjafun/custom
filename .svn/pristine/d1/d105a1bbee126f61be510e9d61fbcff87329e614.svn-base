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
    /// Find the the current, prevailing linear trend.
    /// </summary>
    [Description("Finds the the current, prevailing linear trend.")]
    public class TrendSpotter : Indicator
    {
        #region Variables
        
            private int sTPeriod = 40;
            private int lTPeriod = 100;
		
			private bool sTUTbool = true;
			private bool lTUTbool = true;
			private bool sTDTbool = false;	
			private bool lTDTbool = false;
		
			private Color sTUTColor 	= Color.Red;
			private Color lTUTColor 	= Color.Blue;
			private Color sTDTColor 	= Color.Orange;
			private Color lTDTColor 	= Color.Lime;
		
		
				
        #endregion

        /// <summary>
        /// This method is used to configure the indicator and is called once before any bar data is loaded.
        /// </summary>
        protected override void Initialize()
        {
			CalculateOnBarClose	= false;
            Overlay				= true;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
			// These four sets find the starting point for the trend line, according to the defined lookback period.
			int STUTStartX		= (LowestBar(Low, STPeriod));
			double STUTStartY	= (Low[STUTStartX]);
			
			int LTUTStartX		= (LowestBar(Low, LTPeriod));
			double LTUTStartY	= (Low[LTUTStartX]);
		
			
			int STDTStartX		= (HighestBar(High, STPeriod));
			double STDTStartY	= (High[STDTStartX]);
			
			int LTDTStartX		= (HighestBar(High, LTPeriod));
			double LTDTStartY	= (High[LTDTStartX]);
			
			
			if (STUTbool == true) // Will only run if "Short Term Uptrend" is set to true in UI.
			{
			
				if (STUTStartX < 1)
					return;
				
				double[] STUTSlopeArray = new double[STUTStartX]; // This creates an array of values that find the slope between the starting point and each bar's low, up to current bar.
				
				for (int STUT = 0; STUT < STUTStartX; STUT++)
				{
					STUTSlopeArray[STUT] = ((Low[STUT] - STUTStartY) / (STUTStartX - STUT)); // Rise over run.
				}
				
				double STUTMinSlope = STUTSlopeArray[0]; // Initial minimum slope.
				int STUTMinSlopeBar = 0; // Initial bar for initial minimum slope.
			
				for (int i = 0; i < STUTStartX; i++) // This loops finds and set the min slope of the above array, and assigns the correct bar.
				{
					if (STUTSlopeArray[i] < STUTMinSlope)
					{
						STUTMinSlope = STUTSlopeArray[i];
						STUTMinSlopeBar = i;
					}
				}
	
				double STUTEndY = ((STUTMinSlope*STUTStartX) + STUTStartY); // This follows the linear line quation y = m*x + b to find the end point beneath the current bar.
				
				DrawLine("ShortTermUptrend", true, STUTStartX, STUTStartY, 0, STUTEndY, STUTColor, DashStyle.Solid, 2);
			}
			
			if (LTUTbool == true)
			{
				if (LTUTStartX < 1)
					return;
				
				double[] LTUTSlopeArray = new double[LTUTStartX];
				
				for (int LTUT = 0; LTUT < LTUTStartX; LTUT++)
				{
					LTUTSlopeArray[LTUT] = ((Low[LTUT] - LTUTStartY) / (LTUTStartX - LTUT));	
				}
				
				double LTUTMinSlope = LTUTSlopeArray[0];
				int LTUTMinSlopeBar = 0;
			
				for (int i = 0; i < LTUTStartX; i++)
				{
					if (LTUTSlopeArray[i] < LTUTMinSlope)
					{
						LTUTMinSlope = LTUTSlopeArray[i];
						LTUTMinSlopeBar = i;
					}
				}
	
				double LTUTEndY = ((LTUTMinSlope*LTUTStartX) + LTUTStartY);
				
				DrawLine("LongTermUptrend", true, LTUTStartX, LTUTStartY, 0, LTUTEndY, LTUTColor, DashStyle.Solid, 2);
			}
			
			
			if (STDTbool == true)
			{
			
				if (STDTStartX < 1)
					return;
				
				double[] STDTSlopeArray = new double[STDTStartX];
				
				for (int STDT = 0; STDT < STDTStartX; STDT++)
				{
					STDTSlopeArray[STDT] = ((High[STDT] - STDTStartY) / (STDTStartX - STDT));	
				}
				
				double STDTMaxSlope = STDTSlopeArray[0];
				int STDTMaxSlopeBar = 0;
			
				for (int i = 0; i < STDTStartX; i++)
				{
					if (STDTSlopeArray[i] > STDTMaxSlope)
					{
						STDTMaxSlope = STDTSlopeArray[i];
						STDTMaxSlopeBar = i;
					}
				}
	
				double STDTEndY = ((STDTMaxSlope*STDTStartX) + STDTStartY);
				
				DrawLine("ShortTermDowntrend", true, STDTStartX, STDTStartY, 0, STDTEndY, STDTColor, DashStyle.Solid, 2);
			}
			
			if (LTDTbool == true)
			{
				if (LTDTStartX < 1)
					return;
				
				double[] LTDTSlopeArray = new double[LTDTStartX];
				
				for (int LTDT = 0; LTDT < LTDTStartX; LTDT++)
				{
					LTDTSlopeArray[LTDT] = ((High[LTDT] - LTDTStartY) / (LTDTStartX - LTDT));	
				}
				
				double LTDTMaxSlope = LTDTSlopeArray[0];
				int LTDTMaxSlopeBar = 0;
			
				for (int i = 0; i < LTDTStartX; i++)
				{
					if (LTDTSlopeArray[i] > LTDTMaxSlope)
					{
						LTDTMaxSlope = LTDTSlopeArray[i];
						LTDTMaxSlopeBar = i;
					}
				}
	
				double LTDTEndY = ((LTDTMaxSlope*LTDTStartX) + LTDTStartY);
				
				DrawLine("LongTermDowntrend", true, LTDTStartX, LTDTStartY, 0, LTDTEndY, LTDTColor, DashStyle.Solid, 2);
			}
        }

        #region Properties

		[Description("Number of bars used n calculation for Long Term Period")]
        [GridCategory("Periods")]
        [Gui.Design.DisplayNameAttribute("Long Term Period")]
		public int STPeriod
        {
            get { return sTPeriod; }
            set { sTPeriod = Math.Max(1, value); }
        }

        [Description("Number of bars used n calculation for Short Term Period")]
        [GridCategory("Periods")]
        [Gui.Design.DisplayNameAttribute("Short Term Period")]
		public int LTPeriod
        {
            get { return lTPeriod; }
            set { lTPeriod = Math.Max(1, value); }
        }
		
		
        [Description("Plot Short Term Uptrend?")]
        [GridCategory("Plot Options")]
        [Gui.Design.DisplayNameAttribute("Short Term Uptrend")]
		public bool STUTbool
        {
            get { return sTUTbool; }
            set { sTUTbool = value; }
        }

        [Description("Plot Long Term Uptrend?")]
        [GridCategory("Plot Options")]
        [Gui.Design.DisplayNameAttribute("Long Term Uptrend")]
		public bool LTUTbool
        {
            get { return lTUTbool; }
            set { lTUTbool = value; }
        }
		
        [Description("Plot Short Term Downtrend?")]
        [GridCategory("Plot Options")]
        [Gui.Design.DisplayNameAttribute("Short Term Downtrend")]
		public bool STDTbool
        {
            get { return sTDTbool; }
            set { sTDTbool = value; }
        }
		
        [Description("Plot Long Term Downtrend?")]
        [GridCategory("Plot Options")]
        [Gui.Design.DisplayNameAttribute("Long Term Downtrend")]
		public bool LTDTbool
        {
            get { return lTDTbool; }
            set { lTDTbool = value; }
        }
		
		
		[XmlIgnore()]
        [Description("Color for Short Term Uptrend")]
        [Category("Plot Colors")]
		[Gui.Design.DisplayNameAttribute("Short Term Uptrend Color")]
        public Color STUTColor
        {
            get { return sTUTColor; }
            set { sTUTColor = value; }
        }
		
		[Browsable(false)]
		public string STUTColorSerialize
		{
			get { return NinjaTrader.Gui.Design.SerializableColor.ToString(sTUTColor); }
			set { sTUTColor = NinjaTrader.Gui.Design.SerializableColor.FromString(value); }
		}
		
		[XmlIgnore()]
        [Description("Color for Long Term Uptrend")]
        [Category("Plot Colors")]
		[Gui.Design.DisplayNameAttribute("Long Term Uptrend Color")]
        public Color LTUTColor
        {
            get { return lTUTColor; }
            set { lTUTColor = value; }
        }
		
		[Browsable(false)]
		public string LTUTColorSerialize
		{
			get { return NinjaTrader.Gui.Design.SerializableColor.ToString(lTUTColor); }
			set { lTUTColor = NinjaTrader.Gui.Design.SerializableColor.FromString(value); }
		}
		
		[XmlIgnore()]
        [Description("Color for Short Term Downtrend")]
        [Category("Plot Colors")]
		[Gui.Design.DisplayNameAttribute("Short Term Downtrend Color")]
        public Color STDTColor
        {
            get { return sTDTColor; }
            set { sTDTColor = value; }
        }
		
		[Browsable(false)]
		public string STDTColorSerialize
		{
			get { return NinjaTrader.Gui.Design.SerializableColor.ToString(sTDTColor); }
			set { sTDTColor = NinjaTrader.Gui.Design.SerializableColor.FromString(value); }
		}
		
		[XmlIgnore()]
        [Description("Color for Long Term Downtrend")]
        [Category("Plot Colors")]
		[Gui.Design.DisplayNameAttribute("Long Term Downtrend Color")]
        public Color LTDTColor
        {
            get { return lTDTColor; }
            set { lTDTColor = value; }
        }
		
		[Browsable(false)]
		public string LTDTColorSerialize
		{
			get { return NinjaTrader.Gui.Design.SerializableColor.ToString(lTDTColor); }
			set { lTDTColor = NinjaTrader.Gui.Design.SerializableColor.FromString(value); }
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
        private TrendSpotter[] cacheTrendSpotter = null;

        private static TrendSpotter checkTrendSpotter = new TrendSpotter();

        /// <summary>
        /// Finds the the current, prevailing linear trend.
        /// </summary>
        /// <returns></returns>
        public TrendSpotter TrendSpotter(bool lTDTbool, int lTPeriod, bool lTUTbool, bool sTDTbool, int sTPeriod, bool sTUTbool)
        {
            return TrendSpotter(Input, lTDTbool, lTPeriod, lTUTbool, sTDTbool, sTPeriod, sTUTbool);
        }

        /// <summary>
        /// Finds the the current, prevailing linear trend.
        /// </summary>
        /// <returns></returns>
        public TrendSpotter TrendSpotter(Data.IDataSeries input, bool lTDTbool, int lTPeriod, bool lTUTbool, bool sTDTbool, int sTPeriod, bool sTUTbool)
        {
            if (cacheTrendSpotter != null)
                for (int idx = 0; idx < cacheTrendSpotter.Length; idx++)
                    if (cacheTrendSpotter[idx].LTDTbool == lTDTbool && cacheTrendSpotter[idx].LTPeriod == lTPeriod && cacheTrendSpotter[idx].LTUTbool == lTUTbool && cacheTrendSpotter[idx].STDTbool == sTDTbool && cacheTrendSpotter[idx].STPeriod == sTPeriod && cacheTrendSpotter[idx].STUTbool == sTUTbool && cacheTrendSpotter[idx].EqualsInput(input))
                        return cacheTrendSpotter[idx];

            lock (checkTrendSpotter)
            {
                checkTrendSpotter.LTDTbool = lTDTbool;
                lTDTbool = checkTrendSpotter.LTDTbool;
                checkTrendSpotter.LTPeriod = lTPeriod;
                lTPeriod = checkTrendSpotter.LTPeriod;
                checkTrendSpotter.LTUTbool = lTUTbool;
                lTUTbool = checkTrendSpotter.LTUTbool;
                checkTrendSpotter.STDTbool = sTDTbool;
                sTDTbool = checkTrendSpotter.STDTbool;
                checkTrendSpotter.STPeriod = sTPeriod;
                sTPeriod = checkTrendSpotter.STPeriod;
                checkTrendSpotter.STUTbool = sTUTbool;
                sTUTbool = checkTrendSpotter.STUTbool;

                if (cacheTrendSpotter != null)
                    for (int idx = 0; idx < cacheTrendSpotter.Length; idx++)
                        if (cacheTrendSpotter[idx].LTDTbool == lTDTbool && cacheTrendSpotter[idx].LTPeriod == lTPeriod && cacheTrendSpotter[idx].LTUTbool == lTUTbool && cacheTrendSpotter[idx].STDTbool == sTDTbool && cacheTrendSpotter[idx].STPeriod == sTPeriod && cacheTrendSpotter[idx].STUTbool == sTUTbool && cacheTrendSpotter[idx].EqualsInput(input))
                            return cacheTrendSpotter[idx];

                TrendSpotter indicator = new TrendSpotter();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.LTDTbool = lTDTbool;
                indicator.LTPeriod = lTPeriod;
                indicator.LTUTbool = lTUTbool;
                indicator.STDTbool = sTDTbool;
                indicator.STPeriod = sTPeriod;
                indicator.STUTbool = sTUTbool;
                Indicators.Add(indicator);
                indicator.SetUp();

                TrendSpotter[] tmp = new TrendSpotter[cacheTrendSpotter == null ? 1 : cacheTrendSpotter.Length + 1];
                if (cacheTrendSpotter != null)
                    cacheTrendSpotter.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheTrendSpotter = tmp;
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
        /// Finds the the current, prevailing linear trend.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.TrendSpotter TrendSpotter(bool lTDTbool, int lTPeriod, bool lTUTbool, bool sTDTbool, int sTPeriod, bool sTUTbool)
        {
            return _indicator.TrendSpotter(Input, lTDTbool, lTPeriod, lTUTbool, sTDTbool, sTPeriod, sTUTbool);
        }

        /// <summary>
        /// Finds the the current, prevailing linear trend.
        /// </summary>
        /// <returns></returns>
        public Indicator.TrendSpotter TrendSpotter(Data.IDataSeries input, bool lTDTbool, int lTPeriod, bool lTUTbool, bool sTDTbool, int sTPeriod, bool sTUTbool)
        {
            return _indicator.TrendSpotter(input, lTDTbool, lTPeriod, lTUTbool, sTDTbool, sTPeriod, sTUTbool);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// Finds the the current, prevailing linear trend.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.TrendSpotter TrendSpotter(bool lTDTbool, int lTPeriod, bool lTUTbool, bool sTDTbool, int sTPeriod, bool sTUTbool)
        {
            return _indicator.TrendSpotter(Input, lTDTbool, lTPeriod, lTUTbool, sTDTbool, sTPeriod, sTUTbool);
        }

        /// <summary>
        /// Finds the the current, prevailing linear trend.
        /// </summary>
        /// <returns></returns>
        public Indicator.TrendSpotter TrendSpotter(Data.IDataSeries input, bool lTDTbool, int lTPeriod, bool lTUTbool, bool sTDTbool, int sTPeriod, bool sTUTbool)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.TrendSpotter(input, lTDTbool, lTPeriod, lTUTbool, sTDTbool, sTPeriod, sTUTbool);
        }
    }
}
#endregion
