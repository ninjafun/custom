
#region Using declarations
using System;
using System.Diagnostics;
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
    /// 
    /// </summary>
    [Description("")]
    [Gui.Design.DisplayName("DStochZeroLag")]
    public class DStochZeroLag : Indicator
    {
        #region Variables
        private int dSTLen=10;
        private int priceactionFilter=5;

		private bool bStochRising=false;
		private DataSeries Zerolag;
        #endregion

        /// <summary>
        /// This method is used to configure the indicator and is called once before any bar data is loaded.
        /// </summary>
        protected override void Initialize()
        {
            Add(new Plot(Color.Cyan, PlotStyle.Line, "DStoch_OverBot"));
			Add(new Plot(Color.Magenta, PlotStyle.Line, "DStoch_OverSold"));
            Add(new Line(Color.Blue, 90, "Upper"));
            Add(new Line(Color.Blue, 10, "Lower"));
			Lines[0].Pen.DashStyle = DashStyle.Dash;
			Lines[1].Pen.DashStyle = DashStyle.Dash;
            CalculateOnBarClose	= false;
            Overlay		= false;
            PriceTypeSupported	= false;
			
			Zerolag = new DataSeries(this);
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
			if (CurrentBar>2)
			{
				double aa = Math.Exp(((-1*Math.Sqrt(2))*Math.PI) / priceactionFilter);
				double bb = 2*aa*Math.Cos((Math.Sqrt(2)*180) / priceactionFilter);
				double CB = bb;
				double CC = -aa*aa;
				double CA = 1 - CB - CC;
			
				Zerolag.Set((CA*DoubleStochastics(Input,dSTLen)[0]) + (CB*DoubleStochastics(Input,dSTLen)[1]) + (CC*DoubleStochastics(Input,dSTLen)[2]));
				
				if (CurrentBar>3)
				{
					if (Falling(Zerolag))
					{
						if (bStochRising)
						{
							DStoch_OverBot.Set(Zerolag[0]);
						}
						DStoch_OverSold.Set(Zerolag[0]);
						bStochRising = false;
					}
					else
					{
						if (!bStochRising)
						{
							DStoch_OverSold.Set(Zerolag[0]);
						}
						DStoch_OverBot.Set(Zerolag[0]);
						bStochRising = true;
					}				
				}
			}
        }

        #region Properties
        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries DStoch_OverBot
        {
            get { return Values[0]; }
        }

		[Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries DStoch_OverSold
        {
            get { return Values[1]; }
        }

		[Description("")]
        [Category("Parameters")]
        public int DSTlen
        {
            get { return dSTLen; }
            set { dSTLen = Math.Max(1, value); }
        }

		[Description("")]
        [Category("Parameters")]
        public int PriceactionFilter
        {
            get { return priceactionFilter; }
            set { priceactionFilter = Math.Max(1, value); }
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
        private DStochZeroLag[] cacheDStochZeroLag = null;

        private static DStochZeroLag checkDStochZeroLag = new DStochZeroLag();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DStochZeroLag DStochZeroLag(int dSTlen, int priceactionFilter)
        {
            return DStochZeroLag(Input, dSTlen, priceactionFilter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DStochZeroLag DStochZeroLag(Data.IDataSeries input, int dSTlen, int priceactionFilter)
        {
            if (cacheDStochZeroLag != null)
                for (int idx = 0; idx < cacheDStochZeroLag.Length; idx++)
                    if (cacheDStochZeroLag[idx].DSTlen == dSTlen && cacheDStochZeroLag[idx].PriceactionFilter == priceactionFilter && cacheDStochZeroLag[idx].EqualsInput(input))
                        return cacheDStochZeroLag[idx];

            lock (checkDStochZeroLag)
            {
                checkDStochZeroLag.DSTlen = dSTlen;
                dSTlen = checkDStochZeroLag.DSTlen;
                checkDStochZeroLag.PriceactionFilter = priceactionFilter;
                priceactionFilter = checkDStochZeroLag.PriceactionFilter;

                if (cacheDStochZeroLag != null)
                    for (int idx = 0; idx < cacheDStochZeroLag.Length; idx++)
                        if (cacheDStochZeroLag[idx].DSTlen == dSTlen && cacheDStochZeroLag[idx].PriceactionFilter == priceactionFilter && cacheDStochZeroLag[idx].EqualsInput(input))
                            return cacheDStochZeroLag[idx];

                DStochZeroLag indicator = new DStochZeroLag();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.DSTlen = dSTlen;
                indicator.PriceactionFilter = priceactionFilter;
                Indicators.Add(indicator);
                indicator.SetUp();

                DStochZeroLag[] tmp = new DStochZeroLag[cacheDStochZeroLag == null ? 1 : cacheDStochZeroLag.Length + 1];
                if (cacheDStochZeroLag != null)
                    cacheDStochZeroLag.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheDStochZeroLag = tmp;
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
        /// 
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.DStochZeroLag DStochZeroLag(int dSTlen, int priceactionFilter)
        {
            return _indicator.DStochZeroLag(Input, dSTlen, priceactionFilter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Indicator.DStochZeroLag DStochZeroLag(Data.IDataSeries input, int dSTlen, int priceactionFilter)
        {
            return _indicator.DStochZeroLag(input, dSTlen, priceactionFilter);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.DStochZeroLag DStochZeroLag(int dSTlen, int priceactionFilter)
        {
            return _indicator.DStochZeroLag(Input, dSTlen, priceactionFilter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Indicator.DStochZeroLag DStochZeroLag(Data.IDataSeries input, int dSTlen, int priceactionFilter)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.DStochZeroLag(input, dSTlen, priceactionFilter);
        }
    }
}
#endregion
