/// Indicator: Bill Williams' Alligator
/// Author: cvax
/// Version: 0.1.0
/// 
/// Usage: http://www.metaquotes.net/techanalysis/indicators/alligator/

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

namespace NinjaTrader.Indicator
{
    [Description("In principle, bwAlligator Technical Indicator is a combination of Balance Lines (Moving Averages) that use fractal geometry and nonlinear dynamics.")]
    [Gui.Design.DisplayName("Profitunity: Alligator (by Bill Williams)")]
    public class bwAlligator : Indicator
    {
        #region Variables
        #endregion

        protected override void Initialize()
        {
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line, "Alligator's Jaw"));
			Add(new Plot(new Pen(Color.Red,2), PlotStyle.Line, "Alligator's Teeth"));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line, "Alligator's Lips"));
            CalculateOnBarClose	= false;
            Overlay				= true;
            PriceTypeSupported	= false;
       } 

		protected override void OnBarUpdate()
        {
			if(CurrentBar < 8)
				return;
			Jaw.Set(SMMA(Median, 13)[8]);
			Teeth.Set(SMMA(Median, 8)[5]);
			Lips.Set(SMMA(Median, 5)[3]);
        }

        #region Properties
		[Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries Jaw
        {
            get { return Values[0]; }
        }

		[Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries Teeth
        {
            get { return Values[1]; }
        }
		
		[Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries Lips
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
        private bwAlligator[] cachebwAlligator = null;

        private static bwAlligator checkbwAlligator = new bwAlligator();

        /// <summary>
        /// In principle, bwAlligator Technical Indicator is a combination of Balance Lines (Moving Averages) that use fractal geometry and nonlinear dynamics.
        /// </summary>
        /// <returns></returns>
        public bwAlligator bwAlligator()
        {
            return bwAlligator(Input);
        }

        /// <summary>
        /// In principle, bwAlligator Technical Indicator is a combination of Balance Lines (Moving Averages) that use fractal geometry and nonlinear dynamics.
        /// </summary>
        /// <returns></returns>
        public bwAlligator bwAlligator(Data.IDataSeries input)
        {
            if (cachebwAlligator != null)
                for (int idx = 0; idx < cachebwAlligator.Length; idx++)
                    if (cachebwAlligator[idx].EqualsInput(input))
                        return cachebwAlligator[idx];

            lock (checkbwAlligator)
            {
                if (cachebwAlligator != null)
                    for (int idx = 0; idx < cachebwAlligator.Length; idx++)
                        if (cachebwAlligator[idx].EqualsInput(input))
                            return cachebwAlligator[idx];

                bwAlligator indicator = new bwAlligator();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                Indicators.Add(indicator);
                indicator.SetUp();

                bwAlligator[] tmp = new bwAlligator[cachebwAlligator == null ? 1 : cachebwAlligator.Length + 1];
                if (cachebwAlligator != null)
                    cachebwAlligator.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cachebwAlligator = tmp;
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
        /// In principle, bwAlligator Technical Indicator is a combination of Balance Lines (Moving Averages) that use fractal geometry and nonlinear dynamics.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.bwAlligator bwAlligator()
        {
            return _indicator.bwAlligator(Input);
        }

        /// <summary>
        /// In principle, bwAlligator Technical Indicator is a combination of Balance Lines (Moving Averages) that use fractal geometry and nonlinear dynamics.
        /// </summary>
        /// <returns></returns>
        public Indicator.bwAlligator bwAlligator(Data.IDataSeries input)
        {
            return _indicator.bwAlligator(input);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// In principle, bwAlligator Technical Indicator is a combination of Balance Lines (Moving Averages) that use fractal geometry and nonlinear dynamics.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.bwAlligator bwAlligator()
        {
            return _indicator.bwAlligator(Input);
        }

        /// <summary>
        /// In principle, bwAlligator Technical Indicator is a combination of Balance Lines (Moving Averages) that use fractal geometry and nonlinear dynamics.
        /// </summary>
        /// <returns></returns>
        public Indicator.bwAlligator bwAlligator(Data.IDataSeries input)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.bwAlligator(input);
        }
    }
}
#endregion
