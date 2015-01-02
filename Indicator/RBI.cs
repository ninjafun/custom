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

/*
******************************************
Written by Dr Ben (bszapiro "@" yahoo.com)
            Enjoy and Improve!
******************************************
*/

// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    /// <summary>
    /// Plots the Renko Bars Close[0] of any Brick Size and Reversal on a 1 tick chart. Sorry, It will not improve your batting average!
    /// </summary>")

	[Description("Generates a Generalized Renko Bars 'LineOnClose' Plot on a 1 TICK chart. You should only apply RBI to 1 TICK Charts! But you may get 'interesting'  results if you like to experiment with other charts! Sorry, but it will not improve your batting average!")]
	
    public class RBI : Indicator
    {
        #region Variables
        // Wizard generated variables
            private int brickSize = 4;
			private int reversal  = 2;
			private bool wantColors  = true;
		
            private double Brick;		
			private bool FlagUp=true;
			private bool FlagDn=true;
		
			private double temp1Up;
			private double temp1Dn;
			private double temp2Up;
			private double temp2Dn;
		
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the indicator and is called once before any bar data is loaded.
        /// </summary>
        protected override void Initialize()
        {
            Add(new Plot(new Pen(Color.Blue,4),  PlotStyle.Line, "RenkoPlot"));
            Add(new Plot(new Pen(Color.Black,4), PlotStyle.Line, "RenkoTrend"));
			
            Overlay	  = true;
			AutoScale = false;
			
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
			
			Brick = (double)BrickSize*TickSize;
			
			if(Bars.FirstBarOfSession) RenkoPlot.Set(Close[0]);
			
			if(CurrentBar<1) return;
			
			RenkoPlot.Set(RenkoPlot[1]);
			
			//Preventing Gap Problems:
			temp1Up = Values[0][1] + 1*Brick;
			temp1Dn = Values[0][1] - 1*Brick;					
					
			if(Close[0]>temp1Up) 		{RenkoPlot.Set(temp1Up);FlagUp=true;FlagDn=false;}
			if(Close[0]<temp1Dn) 		{RenkoPlot.Set(temp1Dn);FlagDn=true;FlagUp=false;}
					
			//Calculation of the Renko Close Plot: 
			
				 if (FlagUp && Close[0]==Values[0][0] + Brick)
					{RenkoPlot.Set(Close[0]);FlagUp=true; FlagDn=false;}
					
			else if (FlagDn && Close[0]==Values[0][0] - Brick )
					{RenkoPlot.Set(Close[0]);FlagDn=true; FlagUp=false;}
					
			else if (FlagDn && Close[0]==Values[0][0] + Reversal*Brick )
				{RenkoPlot.Set(Close[0]);FlagUp=true; FlagDn=false;}
				
			else if (FlagUp && Close[0]==Values[0][0] - Reversal*Brick )
				{RenkoPlot.Set(Close[0]);FlagDn=true; FlagUp=false;}	

			if(FlagUp) {if (WantColors)BackColor=Color.Lime; RenkoTrend.Set(+1);}
			if(FlagDn) {if (WantColors)BackColor=Color.Pink; RenkoTrend.Set(-1);}
			
        }

        #region Properties
        [Browsable(false)]	
        [XmlIgnore()]		
        public DataSeries RenkoPlot
        {
            get { return Values[0]; }
        }
		
        [Browsable(false)]	
        [XmlIgnore()]		
        public DataSeries RenkoTrend
        {
            get { return Values[1]; }
        }

        [Description("Renko Brick Size selection determines what is Noise (ignored) vs. what is Signal (tradable moves) ")]
        [Category("Parameters")]
        public int BrickSize
        {
            get { return brickSize; }
            set { brickSize = Math.Max(1, value); }
        }
		
        [Description("Minimum Countertrend Bars Required to Upgrade a Minor Retracement to a Major Reversal Status. Standard Renko uses 2 Bars Reversals. Many other choices are possible (including 1)...")]
        [Category("Parameters")]
        public int Reversal
        {
            get { return reversal; }
            set { reversal = Math.Max(1, value); }
        }
		
        [Description("Color Chart Background According to Up/Down RenkoTrend")]
        [Category("Parameters")]
        public bool WantColors
        {
            get { return wantColors; }
            set { wantColors =  value; }
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
        private RBI[] cacheRBI = null;

        private static RBI checkRBI = new RBI();

        /// <summary>
        /// Generates a Generalized Renko Bars 'LineOnClose' Plot on a 1 TICK chart. You should only apply RBI to 1 TICK Charts! But you may get 'interesting'  results if you like to experiment with other charts! Sorry, but it will not improve your batting average!
        /// </summary>
        /// <returns></returns>
        public RBI RBI(int brickSize, int reversal, bool wantColors)
        {
            return RBI(Input, brickSize, reversal, wantColors);
        }

        /// <summary>
        /// Generates a Generalized Renko Bars 'LineOnClose' Plot on a 1 TICK chart. You should only apply RBI to 1 TICK Charts! But you may get 'interesting'  results if you like to experiment with other charts! Sorry, but it will not improve your batting average!
        /// </summary>
        /// <returns></returns>
        public RBI RBI(Data.IDataSeries input, int brickSize, int reversal, bool wantColors)
        {
            if (cacheRBI != null)
                for (int idx = 0; idx < cacheRBI.Length; idx++)
                    if (cacheRBI[idx].BrickSize == brickSize && cacheRBI[idx].Reversal == reversal && cacheRBI[idx].WantColors == wantColors && cacheRBI[idx].EqualsInput(input))
                        return cacheRBI[idx];

            lock (checkRBI)
            {
                checkRBI.BrickSize = brickSize;
                brickSize = checkRBI.BrickSize;
                checkRBI.Reversal = reversal;
                reversal = checkRBI.Reversal;
                checkRBI.WantColors = wantColors;
                wantColors = checkRBI.WantColors;

                if (cacheRBI != null)
                    for (int idx = 0; idx < cacheRBI.Length; idx++)
                        if (cacheRBI[idx].BrickSize == brickSize && cacheRBI[idx].Reversal == reversal && cacheRBI[idx].WantColors == wantColors && cacheRBI[idx].EqualsInput(input))
                            return cacheRBI[idx];

                RBI indicator = new RBI();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.BrickSize = brickSize;
                indicator.Reversal = reversal;
                indicator.WantColors = wantColors;
                Indicators.Add(indicator);
                indicator.SetUp();

                RBI[] tmp = new RBI[cacheRBI == null ? 1 : cacheRBI.Length + 1];
                if (cacheRBI != null)
                    cacheRBI.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheRBI = tmp;
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
        /// Generates a Generalized Renko Bars 'LineOnClose' Plot on a 1 TICK chart. You should only apply RBI to 1 TICK Charts! But you may get 'interesting'  results if you like to experiment with other charts! Sorry, but it will not improve your batting average!
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.RBI RBI(int brickSize, int reversal, bool wantColors)
        {
            return _indicator.RBI(Input, brickSize, reversal, wantColors);
        }

        /// <summary>
        /// Generates a Generalized Renko Bars 'LineOnClose' Plot on a 1 TICK chart. You should only apply RBI to 1 TICK Charts! But you may get 'interesting'  results if you like to experiment with other charts! Sorry, but it will not improve your batting average!
        /// </summary>
        /// <returns></returns>
        public Indicator.RBI RBI(Data.IDataSeries input, int brickSize, int reversal, bool wantColors)
        {
            return _indicator.RBI(input, brickSize, reversal, wantColors);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// Generates a Generalized Renko Bars 'LineOnClose' Plot on a 1 TICK chart. You should only apply RBI to 1 TICK Charts! But you may get 'interesting'  results if you like to experiment with other charts! Sorry, but it will not improve your batting average!
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.RBI RBI(int brickSize, int reversal, bool wantColors)
        {
            return _indicator.RBI(Input, brickSize, reversal, wantColors);
        }

        /// <summary>
        /// Generates a Generalized Renko Bars 'LineOnClose' Plot on a 1 TICK chart. You should only apply RBI to 1 TICK Charts! But you may get 'interesting'  results if you like to experiment with other charts! Sorry, but it will not improve your batting average!
        /// </summary>
        /// <returns></returns>
        public Indicator.RBI RBI(Data.IDataSeries input, int brickSize, int reversal, bool wantColors)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.RBI(input, brickSize, reversal, wantColors);
        }
    }
}
#endregion
