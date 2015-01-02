/// Indicator: Bill Williams' Fractals
/// Author: cvax
/// Version: 0.1.0
/// Modified by: mrLogik (Added color Serialize on inputs.
/// 
/// References:	Fractal for eSignal 7.x by Chris D. Kryza (FRACTAL.EFS)
/// 			Bill Williams Profitunity System v1.0 by Daniel (http://www.wealth-lab.com/cgi-bin/WealthLab.DLL/editsystem?id=27909)
/// 
/// Usage: http://www.metaquotes.net/techanalysis/indicators/fractal/
/// 
/// Modified DrawText() by adding autoscale boolean parameter to work with NT 7.0.0.5 - Gains 12/09/09

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
    [Description("Fractal Technical Indicator it is a series of at least five successive bars, with the highest HIGH in the middle, and two lower HIGHs on both sides. The reversing set is a series of at least five successive bars, with the lowest LOW in the middle, and two higher LOWs on both sides, which correlates to the sell fractal. The fractals are have High and Low values and are indicated with the up and down arrows.")]
    [Gui.Design.DisplayName("Profitunity: Fractals (by Bill Williams)")]
    public class bwFractals : Indicator
    {
        #region Variables
		private int barcounter = 0;
		private float markersize = 10;
		private Color upcolor = Color.Green;
		private Color downcolor = Color.Red;
		private	System.Drawing.Font		textFont;
        #endregion

        protected override void Initialize()
        {
            CalculateOnBarClose	= false;
            Overlay				= true;
            PriceTypeSupported	= false;
        }

        protected override void OnBarUpdate()
        {
			if(CurrentBar == 0)
				textFont = new Font("Arial",markersize);
			if(CurrentBar < 5)
				return;
			if(FirstTickOfBar)
				barcounter++;
			isHighPivot(2,upcolor);
			isLowPivot(2,downcolor);
        }
		
		private void isHighPivot(int period, Color color)
		{
			#region HighPivot
			int y = 0;
			int Lvls = 0;
			
			//Four Matching Highs
			if(High[period]==High[period+1] && High[period]==High[period+2] && High[period]==High[period+3])
			{
				y = 1;
				while(y<=period)
				{
					if(y!=period ? High[period+3]>High[period+3+y] : High[period+3]>High[period+3+y])
						Lvls++;
					if(y!=period ? High[period]>High[period-y] : High[period]>High[period-y])
						Lvls++;
					y++;
				}
			}
			//Three Matching Highs
			else if(High[period]==High[period+1] && High[period]==High[period+2])
			{
				y=1;
				while(y<=period)
				{
					if(y!=period ? High[period+2]>High[period+2+y] : High[period+2]>High[period+2+y])
						Lvls++;
					if(y!=period ? High[period]>High[period-y] : High[period]>High[period-y])
						Lvls++;
					y++;
				}
			}
			//Two Matching Highs
			else if(High[period]==High[period+1])
			{
				y=1;
				while(y<=period)
				{
					if(y!=period ? High[period+1]>High[period+1+y] : High[period+1]>High[period+1+y])
						Lvls++;
					if(y!=period ? High[period]>High[period-y] : High[period]>High[period-y])
						Lvls++;
					y++;
				}
			}
			//Regular Pivot
			else
			{
				y=1;
				while(y<=period)
				{
					if(y!=period ? High[period]>High[period+y] : High[period]>High[period+y])
						Lvls++;
					if(y!=period ? High[period]>High[period-y] : High[period]>High[period-y])
						Lvls++;
					y++;
				}
			}
			
			//Auxiliary Checks
			if(Lvls<period*2)
			{
				Lvls=0;
				//Four Highs - First and Last Matching - Middle 2 are lower
				if(High[period]>=High[period+1] && High[period]>=High[period+2] && High[period]==High[period+3])
				{
					y=1;
					while(y<=period)
					{
						if(y!=period ? High[period+3]>High[period+3+y] : High[period+3]>High[period+3+y])
							Lvls++;
						if(y!=period ? High[period]>High[period-y] : High[period]>High[period-y])
							Lvls++;
						y++;
					}
				}
			}
			if(Lvls<period*2)
			{
				Lvls=0;
				//Three Highs - Middle is lower than two outside
				if(High[period]>=High[period+1] && High[period]==High[period+2])
				{
					y=1;
					while(y<=period)
					{
						if(y!=period ? High[period+2]>High[period+2+y] : High[period+2]>High[period+2+y])
						Lvls++;
					if(y!=period ? High[period]>High[period-y] : High[period]>High[period-y])
						Lvls++;
					y++;
					}
				}
			}
			if(Lvls>=period*2)
			{ 
				// "☼"
				DrawText("High"+barcounter.ToString(), true, High[period].ToString(".0000"), period, High[period], 10, color, textFont, StringAlignment.Center, Color.Transparent, Color.Transparent, 0);
			}
			#endregion
		}

		private void isLowPivot(int period, Color color)
		{
			#region LowPivot
			int y = 0;
			int Lvls = 0;
			
			//Four Matching Lows
			if(Low[period]==Low[period+1] && Low[period]==Low[period+2] && Low[period]==Low[period+3])
			{
				y = 1;
				while(y<=period)
				{
					if(y!=period ? Low[period+3]<Low[period+3+y] : Low[period+3]<Low[period+3+y])
						Lvls++;
					if(y!=period ? Low[period]<Low[period-y] : Low[period]<Low[period-y])
						Lvls++;
					y++;
				}
			}
			//Three Matching Lows
			else if(Low[period]==Low[period+1] && Low[period]==Low[period+2])
			{
				y=1;
				while(y<=period)
				{
					if(y!=period ? Low[period+2]<Low[period+2+y] : Low[period+2]<Low[period+2+y])
						Lvls++;
					if(y!=period ? Low[period]<Low[period-y] : Low[period]<Low[period-y])
						Lvls++;
					y++;
				}
			}
			//Two Matching Lows
			else if(Low[period]==Low[period+1])
			{
				y=1;
				while(y<=period)
				{
					if(y!=period ? Low[period+1]<Low[period+1+y] : Low[period+1]<Low[period+1+y])
						Lvls++;
					if(y!=period ? Low[period]<Low[period-y] : Low[period]<Low[period-y])
						Lvls++;
					y++;
				}
			}
			//Regular Pivot
			else
			{
				y=1;
				while(y<=period)
				{
					if(y!=period ? Low[period]<Low[period+y] : Low[period]<Low[period+y])
						Lvls++;
					if(y!=period ? Low[period]<Low[period-y] : Low[period]<Low[period-y])
						Lvls++;
					y++;
				}
			}
			
			//Auxiliary Checks
			if(Lvls<period*2)
			{
				Lvls=0;
				//Four Lows - First and Last Matching - Middle 2 are lower
				if(Low[period]<=Low[period+1] && Low[period]<=Low[period+2] && Low[period]==Low[period+3])
				{
					y=1;
					while(y<=period)
					{
						if(y!=period ? Low[period+3]<Low[period+3+y] : Low[period+3]<Low[period+3+y])
							Lvls++;
						if(y!=period ? Low[period]<Low[period-y] : Low[period]<Low[period-y])
							Lvls++;
						y++;
					}
				}
			}
			if(Lvls<period*2)
			{
				Lvls=0;
				//Three Lows - Middle is lower than two outside
				if(Low[period]<=Low[period+1] && Low[period]==Low[period+2])
				{
					y=1;
					while(y<=period)
					{
						if(y!=period ? Low[period+2]<Low[period+2+y] : Low[period+2]<Low[period+2+y])
						Lvls++;
					if(y!=period ? Low[period]<Low[period-y] : Low[period]<Low[period-y])
						Lvls++;
					y++;
					}
				}
			}
			if(Lvls>=period*2)
			{
				DrawText("Low"+barcounter.ToString(), true,Low[period].ToString(".0000"),period,Low[period],-10,color, textFont, StringAlignment.Center, Color.Transparent, Color.Transparent, 0);
			}
			#endregion
		}
		
        #region Properties
		[Description("Marker Size")]
		[Category("Visual")]
		[Gui.Design.DisplayName("Marker Size")]
		public float MarkerSize
		{
			get { return markersize; }
			set { markersize = Math.Max(1,value); }
		}
		
		[XmlIgnore()]
		[Description("Up Fractal Color")]
		[Category("Visual")]
		[Gui.Design.DisplayName("Color: Up")]
		public Color UpColor
		{
			get { return upcolor; }
			set { upcolor = value; }
		}
		[Browsable(false)]
		public string UpPaintColorSerialize
		{
			get { return NinjaTrader.Gui.Design.SerializableColor.ToString(upcolor); }
			set { upcolor = NinjaTrader.Gui.Design.SerializableColor.FromString(value); }
		}		
		
		[XmlIgnore()]
		[Description("Down Fractal Color")]
		[Category("Visual")]
		[Gui.Design.DisplayName("Color: Down")]
		public Color DownColor
		{
			get { return downcolor; }
			set { downcolor = value; }
		}
		[Browsable(false)]
		public string DnPaintColorSerialize
		{
			get { return NinjaTrader.Gui.Design.SerializableColor.ToString(downcolor); }
			set { downcolor = NinjaTrader.Gui.Design.SerializableColor.FromString(value); }
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
        private bwFractals[] cachebwFractals = null;

        private static bwFractals checkbwFractals = new bwFractals();

        /// <summary>
        /// Fractal Technical Indicator it is a series of at least five successive bars, with the highest HIGH in the middle, and two lower HIGHs on both sides. The reversing set is a series of at least five successive bars, with the lowest LOW in the middle, and two higher LOWs on both sides, which correlates to the sell fractal. The fractals are have High and Low values and are indicated with the up and down arrows.
        /// </summary>
        /// <returns></returns>
        public bwFractals bwFractals()
        {
            return bwFractals(Input);
        }

        /// <summary>
        /// Fractal Technical Indicator it is a series of at least five successive bars, with the highest HIGH in the middle, and two lower HIGHs on both sides. The reversing set is a series of at least five successive bars, with the lowest LOW in the middle, and two higher LOWs on both sides, which correlates to the sell fractal. The fractals are have High and Low values and are indicated with the up and down arrows.
        /// </summary>
        /// <returns></returns>
        public bwFractals bwFractals(Data.IDataSeries input)
        {
            if (cachebwFractals != null)
                for (int idx = 0; idx < cachebwFractals.Length; idx++)
                    if (cachebwFractals[idx].EqualsInput(input))
                        return cachebwFractals[idx];

            lock (checkbwFractals)
            {
                if (cachebwFractals != null)
                    for (int idx = 0; idx < cachebwFractals.Length; idx++)
                        if (cachebwFractals[idx].EqualsInput(input))
                            return cachebwFractals[idx];

                bwFractals indicator = new bwFractals();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                Indicators.Add(indicator);
                indicator.SetUp();

                bwFractals[] tmp = new bwFractals[cachebwFractals == null ? 1 : cachebwFractals.Length + 1];
                if (cachebwFractals != null)
                    cachebwFractals.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cachebwFractals = tmp;
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
        /// Fractal Technical Indicator it is a series of at least five successive bars, with the highest HIGH in the middle, and two lower HIGHs on both sides. The reversing set is a series of at least five successive bars, with the lowest LOW in the middle, and two higher LOWs on both sides, which correlates to the sell fractal. The fractals are have High and Low values and are indicated with the up and down arrows.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.bwFractals bwFractals()
        {
            return _indicator.bwFractals(Input);
        }

        /// <summary>
        /// Fractal Technical Indicator it is a series of at least five successive bars, with the highest HIGH in the middle, and two lower HIGHs on both sides. The reversing set is a series of at least five successive bars, with the lowest LOW in the middle, and two higher LOWs on both sides, which correlates to the sell fractal. The fractals are have High and Low values and are indicated with the up and down arrows.
        /// </summary>
        /// <returns></returns>
        public Indicator.bwFractals bwFractals(Data.IDataSeries input)
        {
            return _indicator.bwFractals(input);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// Fractal Technical Indicator it is a series of at least five successive bars, with the highest HIGH in the middle, and two lower HIGHs on both sides. The reversing set is a series of at least five successive bars, with the lowest LOW in the middle, and two higher LOWs on both sides, which correlates to the sell fractal. The fractals are have High and Low values and are indicated with the up and down arrows.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.bwFractals bwFractals()
        {
            return _indicator.bwFractals(Input);
        }

        /// <summary>
        /// Fractal Technical Indicator it is a series of at least five successive bars, with the highest HIGH in the middle, and two lower HIGHs on both sides. The reversing set is a series of at least five successive bars, with the lowest LOW in the middle, and two higher LOWs on both sides, which correlates to the sell fractal. The fractals are have High and Low values and are indicated with the up and down arrows.
        /// </summary>
        /// <returns></returns>
        public Indicator.bwFractals bwFractals(Data.IDataSeries input)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.bwFractals(input);
        }
    }
}
#endregion
