/// Indicator: Bill Williams' Awesome Oscillator
/// Author: cvax
/// Version: 0.1.0
/// 
/// Usage: http://www.metaquotes.net/techanalysis/indicators/awesome_oscillator

#region Using declarations
using System;
using System.Collections;
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
    [Description("Awesome Oscillator Technical Indicator (AO) is a 34-period simple moving average, plotted through the middle points of the bars (H+L)/2, which is subtracted from the 5-period simple moving average, built across the central points of the bars (H+L)/2. It shows us quite clearly what’s happening to the market driving force at the present moment.")]
    [Gui.Design.DisplayName("Profitunity: AO (Awesome Oscillator by Bill Williams)")]
    public class bwAO : Indicator
    {
        #region Variables
		private double ao = 0;
		private double ao1 = 0;
        #endregion

        protected override void Initialize()
        {
			Add(new Plot(new Pen(Color.Red, 9), PlotStyle.Bar, "AO (Negative)"));
			Add(new Plot(new Pen(Color.Green, 9), PlotStyle.Bar, "AO (Positive)"));
			Add(new Plot(Color.Transparent, PlotStyle.Line, "AO"));
			
			DrawOnPricePanel	= false;
            CalculateOnBarClose	= false;
            Overlay				= false;
            PriceTypeSupported	= false;
        }

        protected override void OnBarUpdate()
        {
			if(CurrentBar == 0)
				DrawHorizontalLine("Zero Line", true, 0, Color.Black, DashStyle.Solid, 1);
			
			ao = SMA(Median,5)[0]-SMA(Median,34)[0];
			
			if(ao>ao1)
				AOPos.Set(ao);
			else
				AONeg.Set(ao);
			
			if(FirstTickOfBar)
				ao1=ao;
			
			AOValue.Set(ao);
        }

        #region Properties
		[Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries AONeg
        {
            get { return Values[0]; }
        }

		[Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries AOPos
        {
            get { return Values[1]; }
        }
		
		[Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries AOValue
        {
            get { return Values[2]; }
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
        private bwAO[] cachebwAO = null;

        private static bwAO checkbwAO = new bwAO();

        /// <summary>
        /// Awesome Oscillator Technical Indicator (AO) is a 34-period simple moving average, plotted through the middle points of the bars (H+L)/2, which is subtracted from the 5-period simple moving average, built across the central points of the bars (H+L)/2. It shows us quite clearly what’s happening to the market driving force at the present moment.
        /// </summary>
        /// <returns></returns>
        public bwAO bwAO()
        {
            return bwAO(Input);
        }

        /// <summary>
        /// Awesome Oscillator Technical Indicator (AO) is a 34-period simple moving average, plotted through the middle points of the bars (H+L)/2, which is subtracted from the 5-period simple moving average, built across the central points of the bars (H+L)/2. It shows us quite clearly what’s happening to the market driving force at the present moment.
        /// </summary>
        /// <returns></returns>
        public bwAO bwAO(Data.IDataSeries input)
        {
            if (cachebwAO != null)
                for (int idx = 0; idx < cachebwAO.Length; idx++)
                    if (cachebwAO[idx].EqualsInput(input))
                        return cachebwAO[idx];

            lock (checkbwAO)
            {
                if (cachebwAO != null)
                    for (int idx = 0; idx < cachebwAO.Length; idx++)
                        if (cachebwAO[idx].EqualsInput(input))
                            return cachebwAO[idx];

                bwAO indicator = new bwAO();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                Indicators.Add(indicator);
                indicator.SetUp();

                bwAO[] tmp = new bwAO[cachebwAO == null ? 1 : cachebwAO.Length + 1];
                if (cachebwAO != null)
                    cachebwAO.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cachebwAO = tmp;
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
        /// Awesome Oscillator Technical Indicator (AO) is a 34-period simple moving average, plotted through the middle points of the bars (H+L)/2, which is subtracted from the 5-period simple moving average, built across the central points of the bars (H+L)/2. It shows us quite clearly what’s happening to the market driving force at the present moment.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.bwAO bwAO()
        {
            return _indicator.bwAO(Input);
        }

        /// <summary>
        /// Awesome Oscillator Technical Indicator (AO) is a 34-period simple moving average, plotted through the middle points of the bars (H+L)/2, which is subtracted from the 5-period simple moving average, built across the central points of the bars (H+L)/2. It shows us quite clearly what’s happening to the market driving force at the present moment.
        /// </summary>
        /// <returns></returns>
        public Indicator.bwAO bwAO(Data.IDataSeries input)
        {
            return _indicator.bwAO(input);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// Awesome Oscillator Technical Indicator (AO) is a 34-period simple moving average, plotted through the middle points of the bars (H+L)/2, which is subtracted from the 5-period simple moving average, built across the central points of the bars (H+L)/2. It shows us quite clearly what’s happening to the market driving force at the present moment.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.bwAO bwAO()
        {
            return _indicator.bwAO(Input);
        }

        /// <summary>
        /// Awesome Oscillator Technical Indicator (AO) is a 34-period simple moving average, plotted through the middle points of the bars (H+L)/2, which is subtracted from the 5-period simple moving average, built across the central points of the bars (H+L)/2. It shows us quite clearly what’s happening to the market driving force at the present moment.
        /// </summary>
        /// <returns></returns>
        public Indicator.bwAO bwAO(Data.IDataSeries input)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.bwAO(input);
        }
    }
}
#endregion
