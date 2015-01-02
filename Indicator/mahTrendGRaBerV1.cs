

// Written by Aligator (mahaghi@cox.net)
// Version 1.0, October 7, 2011
// Credit is given to Raghee Horner (IBFX) for intoducing this setup.
// Refer to Raghee's webinars to better understand how this setup is used.
// Revisded by Aligator on October 6, 2011 to add shading for the EMA band

#region Using declarations
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Indicator;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
#endregion
// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    /// <summary>
    /// This is an adaptation of the GRAB (GRB) setup used by Raghee Horner of InterbankFX for trading currency pairs.
    /// </summary>
    [Description("This is an adaptation of the GRAB (GRB) setup used by Raghee Horner of InterbankFX for trading currency pairs.")]
    public class mahTrendGRaBerV1 : Indicator
    {
        #region Variables
        // Wizard generated variables
            private int emah = 34; // Default setting for Emah
            private int emac = 34; // Default setting for Emac
            private int emal = 34; // Default setting for Emal
        // User defined variables (add any user defined variables below)
		// Added Opacity for the band on October 20, 2011
		    private int				opacityBands = 2;
		
        #endregion

        /// <summary>
        /// This method is used to configure the indicator and is called once before any bar data is loaded.
        /// </summary>
        protected override void Initialize()
        {
            Add(new Plot(Color.FromKnownColor(KnownColor.ForestGreen), PlotStyle.Line, "EmaHigh"));
            Add(new Plot(Color.FromKnownColor(KnownColor.DeepSkyBlue), PlotStyle.Line, "EmaClose"));
            Add(new Plot(Color.FromKnownColor(KnownColor.Red), PlotStyle.Line, "EmaLow"));
            Plots[0].Pen.Width = 1;
			Plots[1].Pen.Width = 1;
			Plots[2].Pen.Width = 1;
			Plots[0].Pen.DashStyle = DashStyle.Dot;
			Plots[1].Pen.DashStyle = DashStyle.Dash;
			Plots[2].Pen.DashStyle = DashStyle.Dot;
			
			
			Overlay				= true;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
          	
			// Condition set 1
            if (Open[0] <= Close[0]
                && Close[0] > EMA(High, Emah)[0])
            {
                BarColor = Color.Chartreuse;
                CandleOutlineColor = Color.Chartreuse;
            }

            // Condition set 2
            if (Open[0] >= Close[0]
                && Close[0] > EMA(High, Emah)[0])
            {
                BarColor = Color.Green;
                CandleOutlineColor = Color.Green;
            }

            // Condition set 3
            if (Open[0] <= Close[0]
                && Close[0] < EMA(High, Emah)[0]
                && Close[0] > EMA(Low, Emal)[0])
            {
                BarColor = Color.LightBlue;
                CandleOutlineColor = Color.LightBlue;
            }

            // Condition set 4
            if (Open[0] >= Close[0]
                && Close[0] < EMA(High, Emah)[0]
                && Close[0] > EMA(Low, Emal)[0])
            {
                BarColor = Color.RoyalBlue;
                CandleOutlineColor = Color.RoyalBlue;
            }

            // Condition set 5
            if (Open[0] <= Close[0]
                && Close[0] < EMA(Low, Emal)[0])
            {
                BarColor = Color.DarkOrange;
                CandleOutlineColor = Color.DarkOrange;
            }

            // Condition set 6
            if (Open[0] >= Close[0]
                && Close[0] < EMA(Low, Emal)[0])
            {
                BarColor = Color.Red;
                CandleOutlineColor = Color.Red;
            }
			
			// Use this method for calculating your indicator values. Assign a value to each
            // Plot EMA for High, Low, and Close.
            {  EmaHigh.Set(EMA(High, Emah)[0]);
               EmaClose.Set(EMA(Close, Emac)[0]);
               EmaLow.Set(EMA(Low, Emal)[0]);
			}
			
			// Added Draw Region on October 20, 2011
			if (!Bars.FirstBarOfSession)
			{
				DrawRegion("Fill1" + CurrentBar, 1, 0, EmaHigh, EmaLow, Color.Transparent, Color.Gainsboro, opacityBands);
			}
        }

        #region Properties
        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries EmaHigh
        {
            get { return Values[0]; }
        }

        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries EmaClose
        {
            get { return Values[1]; }
        }

        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries EmaLow
        {
            get { return Values[2]; }
        }

        [Description("Default value for EMA of High")]
        [GridCategory("Parameters")]
        public int Emah
        {
            get { return emah; }
            set { emah = Math.Max(1, value); }
        }

        [Description("Default value for EMA of Close")]
        [GridCategory("Parameters")]
        public int Emac
        {
            get { return emac; }
            set { emac = Math.Max(1, value); }
        }

        [Description("Default value for EMA of Low")]
        [GridCategory("Parameters")]
        public int Emal
        {
            get { return emal; }
            set { emal = Math.Max(1, value); }
        }
		[Description("Transperancy of Bands 1 - 10")]
		[Category("Parameters")]
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
		public int OpacityBands
		{
			get { return opacityBands; }
			set { opacityBands = Math.Min(10, value); }
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
        private mahTrendGRaBerV1[] cachemahTrendGRaBerV1 = null;

        private static mahTrendGRaBerV1 checkmahTrendGRaBerV1 = new mahTrendGRaBerV1();

        /// <summary>
        /// This is an adaptation of the GRAB (GRB) setup used by Raghee Horner of InterbankFX for trading currency pairs.
        /// </summary>
        /// <returns></returns>
        public mahTrendGRaBerV1 mahTrendGRaBerV1(int emac, int emah, int emal, int opacityBands)
        {
            return mahTrendGRaBerV1(Input, emac, emah, emal, opacityBands);
        }

        /// <summary>
        /// This is an adaptation of the GRAB (GRB) setup used by Raghee Horner of InterbankFX for trading currency pairs.
        /// </summary>
        /// <returns></returns>
        public mahTrendGRaBerV1 mahTrendGRaBerV1(Data.IDataSeries input, int emac, int emah, int emal, int opacityBands)
        {
            if (cachemahTrendGRaBerV1 != null)
                for (int idx = 0; idx < cachemahTrendGRaBerV1.Length; idx++)
                    if (cachemahTrendGRaBerV1[idx].Emac == emac && cachemahTrendGRaBerV1[idx].Emah == emah && cachemahTrendGRaBerV1[idx].Emal == emal && cachemahTrendGRaBerV1[idx].OpacityBands == opacityBands && cachemahTrendGRaBerV1[idx].EqualsInput(input))
                        return cachemahTrendGRaBerV1[idx];

            lock (checkmahTrendGRaBerV1)
            {
                checkmahTrendGRaBerV1.Emac = emac;
                emac = checkmahTrendGRaBerV1.Emac;
                checkmahTrendGRaBerV1.Emah = emah;
                emah = checkmahTrendGRaBerV1.Emah;
                checkmahTrendGRaBerV1.Emal = emal;
                emal = checkmahTrendGRaBerV1.Emal;
                checkmahTrendGRaBerV1.OpacityBands = opacityBands;
                opacityBands = checkmahTrendGRaBerV1.OpacityBands;

                if (cachemahTrendGRaBerV1 != null)
                    for (int idx = 0; idx < cachemahTrendGRaBerV1.Length; idx++)
                        if (cachemahTrendGRaBerV1[idx].Emac == emac && cachemahTrendGRaBerV1[idx].Emah == emah && cachemahTrendGRaBerV1[idx].Emal == emal && cachemahTrendGRaBerV1[idx].OpacityBands == opacityBands && cachemahTrendGRaBerV1[idx].EqualsInput(input))
                            return cachemahTrendGRaBerV1[idx];

                mahTrendGRaBerV1 indicator = new mahTrendGRaBerV1();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.Emac = emac;
                indicator.Emah = emah;
                indicator.Emal = emal;
                indicator.OpacityBands = opacityBands;
                Indicators.Add(indicator);
                indicator.SetUp();

                mahTrendGRaBerV1[] tmp = new mahTrendGRaBerV1[cachemahTrendGRaBerV1 == null ? 1 : cachemahTrendGRaBerV1.Length + 1];
                if (cachemahTrendGRaBerV1 != null)
                    cachemahTrendGRaBerV1.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cachemahTrendGRaBerV1 = tmp;
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
        /// This is an adaptation of the GRAB (GRB) setup used by Raghee Horner of InterbankFX for trading currency pairs.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.mahTrendGRaBerV1 mahTrendGRaBerV1(int emac, int emah, int emal, int opacityBands)
        {
            return _indicator.mahTrendGRaBerV1(Input, emac, emah, emal, opacityBands);
        }

        /// <summary>
        /// This is an adaptation of the GRAB (GRB) setup used by Raghee Horner of InterbankFX for trading currency pairs.
        /// </summary>
        /// <returns></returns>
        public Indicator.mahTrendGRaBerV1 mahTrendGRaBerV1(Data.IDataSeries input, int emac, int emah, int emal, int opacityBands)
        {
            return _indicator.mahTrendGRaBerV1(input, emac, emah, emal, opacityBands);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// This is an adaptation of the GRAB (GRB) setup used by Raghee Horner of InterbankFX for trading currency pairs.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.mahTrendGRaBerV1 mahTrendGRaBerV1(int emac, int emah, int emal, int opacityBands)
        {
            return _indicator.mahTrendGRaBerV1(Input, emac, emah, emal, opacityBands);
        }

        /// <summary>
        /// This is an adaptation of the GRAB (GRB) setup used by Raghee Horner of InterbankFX for trading currency pairs.
        /// </summary>
        /// <returns></returns>
        public Indicator.mahTrendGRaBerV1 mahTrendGRaBerV1(Data.IDataSeries input, int emac, int emah, int emal, int opacityBands)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.mahTrendGRaBerV1(input, emac, emah, emal, opacityBands);
        }
    }
}
#endregion
