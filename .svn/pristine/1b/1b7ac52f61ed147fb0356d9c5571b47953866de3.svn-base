//### 
//### Dynamic SR Lines
//###
//### User		Date 		Description
//### ------	-------- 	-------------
//### Gaston	Apr 2011 	Created
//###
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
using System.Collections.Generic;		
#endregion

namespace NinjaTrader.Indicator
{
    [Description("Draws nearest level of S/R lines above and below current market based on historical price swing High/Low pivots")]
    public class DynamicSRLines : Indicator
    {
		#region Variables
		
		int pivotStrength   = 5;		
		int maxLookBackBars	= 200;
		int pivotTickDiff   = 10;
		int zoneTickSize    = 2;		
		int maxLevels       = 3;
		bool showPivots     = false;
		
		public struct PRICE_SWING {
			public int    Type;			
			public int    Bar;			
			public double Price;
		} 		
		PRICE_SWING listEntry;
		List<PRICE_SWING>pivot = new List<PRICE_SWING>();	
		
		int	lastBar	 = 0;
		
		System.Drawing.Font textFont  = new Font("Times",  8,System.Drawing.FontStyle.Regular);
		System.Drawing.Font boldFont  = new Font("Arial", 10,System.Drawing.FontStyle.Bold);
		
		Color colorAbove=Color.Red;	
		Color colorBelow=Color.Blue;		
		
		int i=0, j=0, x=0;
		int pxAbove=0, countAbove=0, maxCountAbove=0;
		int pxBelow=0, countBelow=0, maxCountBelow=0;
		double p=0, p1=0, level=0;
		string str="";
		
        #endregion

        protected override void Initialize() {
			Overlay = true;
        }
		
        protected override void OnBarUpdate() {
			
			if ( CurrentBar <= maxLookBackBars ) return;
			
			if ( lastBar != CurrentBar ) {					
					
				x = HighestBar(High, (pivotStrength*2)+1);	
				if ( x == pivotStrength ) {					
					listEntry.Type  = +1;					
					listEntry.Price = High[x];				
					listEntry.Bar   = CurrentBar-x;			
					pivot.Add(listEntry);					
					if ( pivot.Count > maxLookBackBars ) {
						pivot.RemoveAt(0);					
					}
				}
					
				x = LowestBar(Low, (pivotStrength*2)+1);	
				if ( x == pivotStrength ) {					
					listEntry.Type  = -1;					
					listEntry.Price = Low[x];				
					listEntry.Bar   = CurrentBar-x;			
					pivot.Add(listEntry);					
					if ( pivot.Count > maxLookBackBars ) {
						pivot.RemoveAt(0);					
					}
				}
				
					
				if ( CurrentBar >= Bars.Count-2 && CurrentBar > maxLookBackBars ) {	
							
					p = Close[0]; p1 = 0; str = ""; 
					for( level=1; level <= maxLevels; level++) {
						p = get_sr_level (p, +1);						
						DrawRectangle("resZone"+level, false, 0, p-((zoneTickSize*TickSize)/2), maxLookBackBars, p+((zoneTickSize*TickSize)/2), colorAbove, colorAbove, 3);
						DrawLine     ("resLine"+level, false, 0, p-((zoneTickSize*TickSize)/2), maxLookBackBars, p-((zoneTickSize*TickSize)/2), colorAbove, DashStyle.Solid, 1);	
						str = ( p == p1 ) ? str+" L"+level : "L"+level;	
						DrawText     ("resTag"+level,  false, "\t    "+str, 0, p, 0, colorAbove, boldFont, StringAlignment.Near, Color.Empty, Color.Empty, 0);
						p1 = p; p += (pivotTickDiff*TickSize);			
					}
							
					p = Close[0]; p1 = 0; str = "";
					for( level=1; level <= maxLevels; level++) {
						p = get_sr_level (p, -1);						
						DrawRectangle("supZone"+level, false, 0, p-((zoneTickSize*TickSize)/2), maxLookBackBars, p+((zoneTickSize*TickSize)/2), colorBelow, colorBelow, 3);
						DrawLine     ("supLine"+level, false, 0, p-((zoneTickSize*TickSize)/2), maxLookBackBars, p-((zoneTickSize*TickSize)/2), colorBelow, DashStyle.Solid, 1);	
						str = ( p == p1 ) ? str+" L"+level : "L"+level;	
						DrawText     ("supTag"+level,  false, "\t    "+str, 0, p, 0, colorBelow, boldFont, StringAlignment.Near, Color.Empty, Color.Empty, 0);
						p1 = p; p -= (pivotTickDiff*TickSize);			
					}
				}
			}
						
			lastBar = CurrentBar;			
        }

		double get_sr_level (double refPrice, int pos) {
			int i=0, j=0;
			double levelPrice=0;
				
			if ( CurrentBar >= Bars.Count-2 && CurrentBar > maxLookBackBars ) {
				maxCountAbove=0; maxCountBelow=0; pxAbove=-1; pxBelow=-1;
				for ( i=pivot.Count-1; i >= 0; i-- ) {							
					countAbove=0; countBelow=0;
					if ( pivot[i].Bar < CurrentBar-maxLookBackBars ) break; 	
					for ( j=0; j < pivot.Count-1; j++ ) {						
						if ( pivot[j].Bar > CurrentBar-maxLookBackBars ) { 		
								
							if ( pos > 0 && pivot[i].Price >= refPrice ) {
								if ( Math.Abs(pivot[i].Price-pivot[j].Price)/TickSize <= pivotTickDiff ) countAbove++;	
								if ( countAbove > maxCountAbove ) {
									maxCountAbove = countAbove;
									levelPrice = pivot[i].Price;
									pxAbove = i;
								}
							}
								
							else  
							if ( pos < 0 && pivot[i].Price <= refPrice ) {
								if ( Math.Abs(pivot[i].Price-pivot[j].Price)/TickSize <= pivotTickDiff ) countBelow++;	
								if ( countBelow > maxCountBelow ) {
									maxCountBelow = countBelow;
									levelPrice = pivot[i].Price;
									pxBelow = i;
								}
							}
						}
					}
				}

				if ( pos > 0 ) levelPrice = ( pxAbove >= 0 ) ? pivot[pxAbove].Price : High[HighestBar(High,maxLookBackBars)];
				if ( pos < 0 ) levelPrice = ( pxBelow >= 0 ) ? pivot[pxBelow].Price : Low[LowestBar(Low,maxLookBackBars)];
			}
			
			return Instrument.MasterInstrument.Round2TickSize( levelPrice );
		}
		
        #region Properties
        [Description("Number of S/R Levels To Draw Aboe and Below The Market")]
        [GridCategory("Parameters")]
		[Gui.Design.DisplayName("1. Max Levels")]
        public int MaxLevels {
            get { return maxLevels; }
            set { maxLevels = value; }
        }
		
        [Description("Max Number of Bars to Look Back for calculations")]
        [GridCategory("Parameters")]
		[Gui.Design.DisplayName("2. Max Look Back Bars")]
        public int MaxLookBackBars {
            get { return maxLookBackBars; }
            set { maxLookBackBars = value; }
        }
		
        [Description("How many bars should be on each side of Swing Hi/Low")]
        [GridCategory("Parameters")]
		[Gui.Design.DisplayName("3. Swing Pivot Strength")]
        public int PivotStrength {
            get { return pivotStrength; }
            set { pivotStrength = value; }
        }

        [Description("Proximity of pivots in Ticks to determine where S/R level will be")]
        [GridCategory("Parameters")]
		[Gui.Design.DisplayName("4. Pivot Tick Proximity")]
        public int PivotTickDiff {
            get { return pivotTickDiff; }
            set { pivotTickDiff = value; }
        }
		
        [Description("Height in Ticks to draw S/R Zone around S/R level")]
		[Category("Parameters")]
        [Gui.Design.DisplayNameAttribute("5. Zone Draw Size (Ticks)")]
        public int ZoneTickSize
        {
            get { return zoneTickSize; }
            set { zoneTickSize = (value > 1 ) ? value : 2 ; }
        }
		
        [Description("S/R Support Line/Zone Color")]
		[Category("Parameters")]
        [Gui.Design.DisplayNameAttribute("6. S/R Sone Color")]
        public Color ColorBelow
        {
            get { return colorBelow; }
            set { colorBelow = value; }
        }
        [Browsable(false)]
        public string ColorBelowSerialize
        {
            get { return Gui.Design.SerializableColor.ToString(colorBelow); }
            set { colorBelow = Gui.Design.SerializableColor.FromString(value); }
        }		
				
        [Description("S/R Resistance Line/Zone Color")]
		[Category("Parameters")]
        [Gui.Design.DisplayNameAttribute("7. Resistance Zone Color")]
        public Color ColorAbove
        {
            get { return colorAbove; }
            set { colorAbove = value; }
        }
        [Browsable(false)]
        public string ColorAboveSerialize
        {
            get { return Gui.Design.SerializableColor.ToString(colorAbove); }
            set { colorAbove = Gui.Design.SerializableColor.FromString(value); }
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
        private DynamicSRLines[] cacheDynamicSRLines = null;

        private static DynamicSRLines checkDynamicSRLines = new DynamicSRLines();

        /// <summary>
        /// Draws nearest level of S/R lines above and below current market based on historical price swing High/Low pivots
        /// </summary>
        /// <returns></returns>
        public DynamicSRLines DynamicSRLines(Color colorAbove, Color colorBelow, int maxLevels, int maxLookBackBars, int pivotStrength, int pivotTickDiff, int zoneTickSize)
        {
            return DynamicSRLines(Input, colorAbove, colorBelow, maxLevels, maxLookBackBars, pivotStrength, pivotTickDiff, zoneTickSize);
        }

        /// <summary>
        /// Draws nearest level of S/R lines above and below current market based on historical price swing High/Low pivots
        /// </summary>
        /// <returns></returns>
        public DynamicSRLines DynamicSRLines(Data.IDataSeries input, Color colorAbove, Color colorBelow, int maxLevels, int maxLookBackBars, int pivotStrength, int pivotTickDiff, int zoneTickSize)
        {
            if (cacheDynamicSRLines != null)
                for (int idx = 0; idx < cacheDynamicSRLines.Length; idx++)
                    if (cacheDynamicSRLines[idx].ColorAbove == colorAbove && cacheDynamicSRLines[idx].ColorBelow == colorBelow && cacheDynamicSRLines[idx].MaxLevels == maxLevels && cacheDynamicSRLines[idx].MaxLookBackBars == maxLookBackBars && cacheDynamicSRLines[idx].PivotStrength == pivotStrength && cacheDynamicSRLines[idx].PivotTickDiff == pivotTickDiff && cacheDynamicSRLines[idx].ZoneTickSize == zoneTickSize && cacheDynamicSRLines[idx].EqualsInput(input))
                        return cacheDynamicSRLines[idx];

            lock (checkDynamicSRLines)
            {
                checkDynamicSRLines.ColorAbove = colorAbove;
                colorAbove = checkDynamicSRLines.ColorAbove;
                checkDynamicSRLines.ColorBelow = colorBelow;
                colorBelow = checkDynamicSRLines.ColorBelow;
                checkDynamicSRLines.MaxLevels = maxLevels;
                maxLevels = checkDynamicSRLines.MaxLevels;
                checkDynamicSRLines.MaxLookBackBars = maxLookBackBars;
                maxLookBackBars = checkDynamicSRLines.MaxLookBackBars;
                checkDynamicSRLines.PivotStrength = pivotStrength;
                pivotStrength = checkDynamicSRLines.PivotStrength;
                checkDynamicSRLines.PivotTickDiff = pivotTickDiff;
                pivotTickDiff = checkDynamicSRLines.PivotTickDiff;
                checkDynamicSRLines.ZoneTickSize = zoneTickSize;
                zoneTickSize = checkDynamicSRLines.ZoneTickSize;

                if (cacheDynamicSRLines != null)
                    for (int idx = 0; idx < cacheDynamicSRLines.Length; idx++)
                        if (cacheDynamicSRLines[idx].ColorAbove == colorAbove && cacheDynamicSRLines[idx].ColorBelow == colorBelow && cacheDynamicSRLines[idx].MaxLevels == maxLevels && cacheDynamicSRLines[idx].MaxLookBackBars == maxLookBackBars && cacheDynamicSRLines[idx].PivotStrength == pivotStrength && cacheDynamicSRLines[idx].PivotTickDiff == pivotTickDiff && cacheDynamicSRLines[idx].ZoneTickSize == zoneTickSize && cacheDynamicSRLines[idx].EqualsInput(input))
                            return cacheDynamicSRLines[idx];

                DynamicSRLines indicator = new DynamicSRLines();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.ColorAbove = colorAbove;
                indicator.ColorBelow = colorBelow;
                indicator.MaxLevels = maxLevels;
                indicator.MaxLookBackBars = maxLookBackBars;
                indicator.PivotStrength = pivotStrength;
                indicator.PivotTickDiff = pivotTickDiff;
                indicator.ZoneTickSize = zoneTickSize;
                Indicators.Add(indicator);
                indicator.SetUp();

                DynamicSRLines[] tmp = new DynamicSRLines[cacheDynamicSRLines == null ? 1 : cacheDynamicSRLines.Length + 1];
                if (cacheDynamicSRLines != null)
                    cacheDynamicSRLines.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheDynamicSRLines = tmp;
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
        /// Draws nearest level of S/R lines above and below current market based on historical price swing High/Low pivots
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.DynamicSRLines DynamicSRLines(Color colorAbove, Color colorBelow, int maxLevels, int maxLookBackBars, int pivotStrength, int pivotTickDiff, int zoneTickSize)
        {
            return _indicator.DynamicSRLines(Input, colorAbove, colorBelow, maxLevels, maxLookBackBars, pivotStrength, pivotTickDiff, zoneTickSize);
        }

        /// <summary>
        /// Draws nearest level of S/R lines above and below current market based on historical price swing High/Low pivots
        /// </summary>
        /// <returns></returns>
        public Indicator.DynamicSRLines DynamicSRLines(Data.IDataSeries input, Color colorAbove, Color colorBelow, int maxLevels, int maxLookBackBars, int pivotStrength, int pivotTickDiff, int zoneTickSize)
        {
            return _indicator.DynamicSRLines(input, colorAbove, colorBelow, maxLevels, maxLookBackBars, pivotStrength, pivotTickDiff, zoneTickSize);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// Draws nearest level of S/R lines above and below current market based on historical price swing High/Low pivots
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.DynamicSRLines DynamicSRLines(Color colorAbove, Color colorBelow, int maxLevels, int maxLookBackBars, int pivotStrength, int pivotTickDiff, int zoneTickSize)
        {
            return _indicator.DynamicSRLines(Input, colorAbove, colorBelow, maxLevels, maxLookBackBars, pivotStrength, pivotTickDiff, zoneTickSize);
        }

        /// <summary>
        /// Draws nearest level of S/R lines above and below current market based on historical price swing High/Low pivots
        /// </summary>
        /// <returns></returns>
        public Indicator.DynamicSRLines DynamicSRLines(Data.IDataSeries input, Color colorAbove, Color colorBelow, int maxLevels, int maxLookBackBars, int pivotStrength, int pivotTickDiff, int zoneTickSize)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.DynamicSRLines(input, colorAbove, colorBelow, maxLevels, maxLookBackBars, pivotStrength, pivotTickDiff, zoneTickSize);
        }
    }
}
#endregion
