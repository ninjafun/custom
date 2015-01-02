// 
// Copyright (C) 2006, NinjaTrader LLC <www.ninjatrader.com>.
// NinjaTrader reserves the right to modify or overwrite this NinjaScript component with each release.
//

#region Using declarations
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Xml.Serialization;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
#endregion

// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
	/// <summary>
	/// The MACD (Moving Average Convergence/Divergence) is a trend following momentum indicator that shows the relationship between two moving averages of prices.
	/// </summary>
	[Description("The MACD (Moving Average Convergence/Divergence) is a trend following momentum indicator that shows the relationship between two moving averages of prices.")]
	public class MACDGapless : Indicator
	{
		#region Variables
		private int					fast	= 12;
		private int					slow	= 26;
		private int					smooth	= 9;
		private	DataSeries		fastEma;
		private	DataSeries		slowEma;
        private bool            useGapless  =    true;
		#endregion

		/// <summary>
		/// This method is used to configure the indicator and is called once before any bar data is loaded.
		/// </summary>
		protected override void Initialize()
		{
			Add(new Plot(Color.Green, "Macd"));
			Add(new Plot(Color.DarkViolet, "Avg"));
			Add(new Plot(new Pen(Color.Navy, 2), PlotStyle.Bar, "Diff"));

			Add(new Line(Color.DarkGray, 0, "Zero line"));

			fastEma	= new DataSeries(this);
			slowEma	= new DataSeries(this);
		}

		/// <summary>
		/// Calculates the indicator value(s) at the current index.
		/// </summary>
		protected override void OnBarUpdate()
		{
				if (CurrentBar == 0)
				{
				fastEma.Set(Input[0]);
				slowEma.Set(Input[0]);
				Value.Set(0);
				Avg.Set(0);
				Diff.Set(0);
				}
				else if (Bars.FirstBarOfSession)
				{
                    if (useGapless)
                    {
                        fastEma.Set((2.0 / (1 + Fast)) * Input[0] + (1 - (2.0 / (1 + Fast))) * (fastEma[1] + Open[0] - Close[1]));
                        slowEma.Set((2.0 / (1 + Slow)) * Input[0] + (1 - (2.0 / (1 + Slow))) * (slowEma[1] + Open[0] - Close[1]));
                    }
                    else
                    {
                        fastEma.Set((2.0 / (1 + Fast)) * Input[0] + (1 - (2.0 / (1 + Fast))) * fastEma[1]);
                        slowEma.Set((2.0 / (1 + Slow)) * Input[0] + (1 - (2.0 / (1 + Slow))) * slowEma[1]);
                    
                    }
						
						
						double macd		= fastEma[0] - slowEma[0];
						double macdAvg	= (2.0 / (1 + Smooth)) * macd + (1 - (2.0 / (1 + Smooth))) * Avg[1];
				
						Value.Set(macd);
						Avg.Set(macdAvg);
						Diff.Set(macd - macdAvg);
				}
				else
				{
                    if (useGapless)
                    {
                        fastEma.Set((2.0 / (1 + Fast)) * Input[0] + (1 - (2.0 / (1 + Fast))) * (fastEma[1] + Open[0] - Close[1]));
                        slowEma.Set((2.0 / (1 + Slow)) * Input[0] + (1 - (2.0 / (1 + Slow))) * (slowEma[1] + Open[0] - Close[1]));
                    }
                    else
                    {
                        fastEma.Set((2.0 / (1 + Fast)) * Input[0] + (1 - (2.0 / (1 + Fast))) * fastEma[1]);
                        slowEma.Set((2.0 / (1 + Slow)) * Input[0] + (1 - (2.0 / (1 + Slow))) * slowEma[1]);

                    }
                    
                        
						double macd		= fastEma[0] - slowEma[0];
						double macdAvg	= (2.0 / (1 + Smooth)) * macd + (1 - (2.0 / (1 + Smooth))) * Avg[1];
				
						Value.Set(macd);
						Avg.Set(macdAvg);
						Diff.Set(macd - macdAvg);
				}					
			
		}

		#region Properties
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public DataSeries Avg
		{
			get { return Values[1]; }
		}

		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public DataSeries Default
		{
			get { return Values[0]; }
		}
		
        /// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public DataSeries Diff
		{
			get { return Values[2]; }
		}

		/// <summary>
		/// </summary>
		[Description("Number of bars for fast EMA")]
		[GridCategory("Parameters")]
		public int Fast
		{
			get { return fast; }
			set { fast = Math.Max(1, value); }
		}

		/// <summary>
		/// </summary>
		[Description("Number of bars for slow EMA")]
		[GridCategory("Parameters")]
		public int Slow
		{
			get { return slow; }
			set { slow = Math.Max(1, value); }
		}

		/// <summary>
		/// </summary>
		[Description("Number of bars for smoothing")]
		[GridCategory("Parameters")]
		public int Smooth
		{
			get { return smooth; }
			set { smooth = Math.Max(1, value); }
		}

        [Description("Use gapless method. Ignores gaps between close[1] and open[0]. Best used for new session intraday open.")]
        [GridCategory("Parameters")]
        public bool UseGapless
        {
            get { return useGapless; }
            set { useGapless = value; }
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
        private MACDGapless[] cacheMACDGapless = null;

        private static MACDGapless checkMACDGapless = new MACDGapless();

        /// <summary>
        /// The MACD (Moving Average Convergence/Divergence) is a trend following momentum indicator that shows the relationship between two moving averages of prices.
        /// </summary>
        /// <returns></returns>
        public MACDGapless MACDGapless(int fast, int slow, int smooth, bool useGapless)
        {
            return MACDGapless(Input, fast, slow, smooth, useGapless);
        }

        /// <summary>
        /// The MACD (Moving Average Convergence/Divergence) is a trend following momentum indicator that shows the relationship between two moving averages of prices.
        /// </summary>
        /// <returns></returns>
        public MACDGapless MACDGapless(Data.IDataSeries input, int fast, int slow, int smooth, bool useGapless)
        {
            if (cacheMACDGapless != null)
                for (int idx = 0; idx < cacheMACDGapless.Length; idx++)
                    if (cacheMACDGapless[idx].Fast == fast && cacheMACDGapless[idx].Slow == slow && cacheMACDGapless[idx].Smooth == smooth && cacheMACDGapless[idx].UseGapless == useGapless && cacheMACDGapless[idx].EqualsInput(input))
                        return cacheMACDGapless[idx];

            lock (checkMACDGapless)
            {
                checkMACDGapless.Fast = fast;
                fast = checkMACDGapless.Fast;
                checkMACDGapless.Slow = slow;
                slow = checkMACDGapless.Slow;
                checkMACDGapless.Smooth = smooth;
                smooth = checkMACDGapless.Smooth;
                checkMACDGapless.UseGapless = useGapless;
                useGapless = checkMACDGapless.UseGapless;

                if (cacheMACDGapless != null)
                    for (int idx = 0; idx < cacheMACDGapless.Length; idx++)
                        if (cacheMACDGapless[idx].Fast == fast && cacheMACDGapless[idx].Slow == slow && cacheMACDGapless[idx].Smooth == smooth && cacheMACDGapless[idx].UseGapless == useGapless && cacheMACDGapless[idx].EqualsInput(input))
                            return cacheMACDGapless[idx];

                MACDGapless indicator = new MACDGapless();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.Fast = fast;
                indicator.Slow = slow;
                indicator.Smooth = smooth;
                indicator.UseGapless = useGapless;
                Indicators.Add(indicator);
                indicator.SetUp();

                MACDGapless[] tmp = new MACDGapless[cacheMACDGapless == null ? 1 : cacheMACDGapless.Length + 1];
                if (cacheMACDGapless != null)
                    cacheMACDGapless.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheMACDGapless = tmp;
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
        /// The MACD (Moving Average Convergence/Divergence) is a trend following momentum indicator that shows the relationship between two moving averages of prices.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.MACDGapless MACDGapless(int fast, int slow, int smooth, bool useGapless)
        {
            return _indicator.MACDGapless(Input, fast, slow, smooth, useGapless);
        }

        /// <summary>
        /// The MACD (Moving Average Convergence/Divergence) is a trend following momentum indicator that shows the relationship between two moving averages of prices.
        /// </summary>
        /// <returns></returns>
        public Indicator.MACDGapless MACDGapless(Data.IDataSeries input, int fast, int slow, int smooth, bool useGapless)
        {
            return _indicator.MACDGapless(input, fast, slow, smooth, useGapless);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// The MACD (Moving Average Convergence/Divergence) is a trend following momentum indicator that shows the relationship between two moving averages of prices.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.MACDGapless MACDGapless(int fast, int slow, int smooth, bool useGapless)
        {
            return _indicator.MACDGapless(Input, fast, slow, smooth, useGapless);
        }

        /// <summary>
        /// The MACD (Moving Average Convergence/Divergence) is a trend following momentum indicator that shows the relationship between two moving averages of prices.
        /// </summary>
        /// <returns></returns>
        public Indicator.MACDGapless MACDGapless(Data.IDataSeries input, int fast, int slow, int smooth, bool useGapless)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.MACDGapless(input, fast, slow, smooth, useGapless);
        }
    }
}
#endregion
