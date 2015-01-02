//	© Joydeep Mitra.
//	visit www.volumedigger.com for more indicators and strategies.

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
    /// Enter the description of your new custom indicator here
    /// </summary>
    [Description("Enter the description of your new custom indicator here")]
    public class dsFibConfluence : Indicator
    {
        #region Variables
        //FIB A
		//resets the high pivot tag if a low pivot is found and vise-verse
		int Ahigh = -1;
		int Alow = -1;
		//tags for the arrows
//		string htag;	//tag string for pivot high
//		string ltag;	//tag string for pivot low
		//pivot high and low	
		double Aphigh = 0;	//pivot high
		double Aplow = 0;	//pivot low
		//FIB B
		int Bhigh = -1;
		int Blow = -1;
		double Bphigh = 0;
		double Bplow = 0;
		
		//FIB C
		int Chigh = -1;
		int Clow = -1;
		double Cphigh = 0;
		double Cplow = 0;
		
		//show support of resistance fibs
		int showfibs = 0;	//0 = up fibs/resistance; 1 = down fibs/supports
		
		int largeextension = 100;
		int mediumextension = 75;
		int smallextension = 50;
		
		int largewidth = 3;
		int mediumwidth = 2;
		int smallwidth = 1;
		
		double largedeviation = .75;
		double mediumdeviation = .5;
		double smalldeviation = .25;
			
			
        #endregion

        /// <summary>
        /// This method is used to configure the indicator and is called once before any bar data is loaded.
        /// </summary>
        protected override void Initialize()
        {
            //Add(new Plot(Color.FromKnownColor(KnownColor.Orange), PlotStyle.Line, "Plot0"));
            CalculateOnBarClose	= false;
            Overlay				= true;
			base.AllowRemovalOfDrawObjects = true;
		}

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
			//draw the large picture
			int highzz = ZigZag(Close,DeviationType.Percent,largedeviation,false).HighBar(0,1,150);
			int lowzz = ZigZag(Close,DeviationType.Percent,largedeviation,false).LowBar(0,1,150);

			if (highzz == 1)
			{
				CalculateHighPivot("A",ref Alow,ref Ahigh,ref Aphigh,ref Aplow,largewidth,largeextension);
			}
					
			if (lowzz == 1)
			{
				CalculateLowPivot("A",ref Alow,ref Ahigh,ref Aphigh,ref Aplow,largewidth,largeextension);
			}
			//the medium picture
			highzz = ZigZag(Close,DeviationType.Percent,mediumdeviation,false).HighBar(0,1,100);
			lowzz = ZigZag(Close,DeviationType.Percent,mediumdeviation,false).LowBar(0,1,100);
			
			if (highzz == 1)
			{
				CalculateHighPivot("B",ref Blow,ref Bhigh,ref Bphigh,ref Bplow,mediumwidth,mediumextension);
			}
			if (lowzz == 1)
			{
				CalculateLowPivot("B",ref Blow,ref Bhigh,ref Bphigh,ref Bplow,mediumwidth,mediumextension);
			}
			//the small pic
			highzz = ZigZag(Close,DeviationType.Percent,smalldeviation,false).HighBar(0,1,60);
			lowzz = ZigZag(Close,DeviationType.Percent,smalldeviation,false).LowBar(0,1,60);
			
			if (highzz == 1)
			{
				CalculateHighPivot("C",ref Clow,ref Chigh,ref Cphigh,ref Cplow,smallwidth,smallextension);
			}
			if (lowzz == 1)
			{
				CalculateLowPivot("C",ref Clow,ref Chigh,ref Cphigh,ref Cplow,smallwidth,smallextension);
			}
			
		}

		
		private void CalculateLowPivot(string tag,ref int pivotlowflag,ref int pivothighflag,
			ref double pivothigh,ref double pivotlow,int linewidth,int endbarago)
		{
			pivothighflag = - 1;
				if (pivotlowflag == - 1)	//if new/first pivot low
				{
					//calculate the previous range and draw the fib
					if (showfibs == 0 && (pivotlow != 0 || pivothigh !=0))
					{
						DrawUpFibs(tag,pivothigh,pivotlow,linewidth,endbarago);
					}
					//start calculating the new range
//					ltag = "raydown" + CurrentBar;
//					DrawArrowUp(ltag,1,Low[1],Color.Green );
					pivotlowflag = 0;
					pivotlow = Low[1];
				}
				else if (pivotlowflag == 0)
				{
//					DrawArrowUp(ltag,1,Low[1],Color.Green );
					pivotlow = Low[1];
				}
		}
		
		
		private void CalculateHighPivot(string tag,ref int pivotlowflag,ref int pivothighflag,
			ref double pivothigh,ref double pivotlow,int linewidth,int endbarago)
		{
			//reset the pivot low tag
			pivotlowflag = -1;
			
			if (pivothighflag == -1)
			{
				//calculate the previous range and draw the fib
				if (showfibs == 1 && (pivotlow != 0 || pivothigh !=0))
				{
					DrawDownFibs(tag,pivothigh,pivotlow,linewidth,endbarago);
				}
//				htag = "rayup" + CurrentBar;
//				DrawArrowDown(htag,1,High[1],Color.Red);
				pivothigh = High[1];
				pivothighflag = 0;
			}
			else if (pivothighflag == 0)
			{
//				DrawArrowDown(htag,1,High[1],Color.Red);
				pivothigh = High[1];
			}
		}
	
		
		
		private void DrawUpFibs(string tag,double high,double low,int linewidth,int endbarago)	//tag R = resistance or fib up
		{
			double f161 = low + (high - low) * 1.618;
			DrawLine(tag + "R161-" + CurrentBar,false,1,f161,-endbarago,f161,Color.Green,DashStyle.Dot,linewidth);
					
			double f261 = low + (high - low) * 2.618;
			DrawLine(tag + "R261-" + CurrentBar,false,1,f261,-endbarago,f261,Color.DarkOliveGreen,DashStyle.DashDot,linewidth);
										
			double f423 = low + (high - low) * 4.236;
			DrawLine(tag + "R426-"+CurrentBar,false,1,f423,-endbarago,f423,Color.Black,DashStyle.Dash,linewidth);
		}
		
		private void DrawDownFibs(string tag,double high,double low,int linewidth,int endbarago)	//tag S = Support or fib down
		{
			double f161 = high - (high - low) * 1.618;
			DrawLine(tag + "S161-" + CurrentBar,false,1,f161,-endbarago,f161,Color.Green,DashStyle.Dot,linewidth);
					
			double f261 = high - (high - low) * 2.618;
			DrawLine(tag + "S261-" + CurrentBar,false,1,f261,-endbarago,f261,Color.DarkOliveGreen,DashStyle.DashDot,linewidth);
									
			double f423 = high - (high - low) * 4.236;
			DrawLine(tag + "S426-"+CurrentBar,false,1,f423,-endbarago,f423,Color.Black,DashStyle.Dash,linewidth);
		}
		
		
        #region Properties
//        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
//        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
//        public DataSeries Plot0
//        {
//            get { return Values[0]; }
//        }
//
//        [Description("")]
//        [GridCategory("Parameters")]
//        public int MyInput0
//        {
//            get { return myInput0; }
//            set { myInput0 = Math.Max(1, value); }
//        }
		[Description("Show Up or Down Fibonacci; 0 = Resistance; 1 = Support")]
		[Category("Parameters")]
		public int ShowFibs
		{
			get{return showfibs;}
			set{showfibs = Math.Max(0,value);
				showfibs = Math.Min(1,showfibs);}
		}
		
		[Description("The number of bars the fib line will extend")]
		[Category("Large Swing")]
		public int LargeLineExtension
		{
			get{return largeextension;}
			set{largeextension = Math.Max(50,value);
				largeextension = Math.Min(300,largeextension);}
		}
		[Description("The number of bars the fib line will extend")]
		[Category("Medium Swing")]
		public int MediumLineExtension
		{
			get{return mediumextension;}
			set{mediumextension = Math.Max(40,value);
				mediumextension = Math.Min(200,mediumextension);}
		}
		[Description("The number of bars the fib line will extend")]
		[Category("Small Swing")]
		public int SmallLineExtension
		{
			get{return smallextension;}
			set{smallextension = Math.Max(10,value);
				smallextension = Math.Min(100,smallextension);}
		}
		
		[Description("Line width")]
		[Category("Large Swing")]
		public int LargeLineWidth
		{
			get{return largewidth;}
			set{largewidth = Math.Max(2,value);
				largewidth = Math.Min(6,largewidth);}
		}
		[Description("Line width")]
		[Category("Medium Swing")]
		public int MediumLineWidth
		{
			get{return mediumwidth;}
			set{mediumwidth = Math.Max(1,value);
				mediumwidth = Math.Min(4,mediumwidth);}
		}
		[Description("Line width")]
		[Category("Small Swing")]
		public int SmallLineWidth
		{
			get{return smallwidth;}
			set{smallwidth = Math.Max(1,value);
				smallwidth = Math.Min(3,smallwidth);}
		}
		
		[Description("Deviation value")]
		[Category("Large Swing")]
		public double LargeDeviation
		{
			get{return largedeviation;}
			set{largedeviation = Math.Max(.5,value);
				largedeviation = Math.Min(1.5,largedeviation);}
		}
		[Description("Deviation value")]
		[Category("Medium Swing")]
		public double MediumDeviation
		{
			get{return mediumdeviation;}
			set{mediumdeviation = Math.Max(.25,value);
				mediumdeviation = Math.Min(1.0,mediumdeviation);}
		}
		[Description("Deviation value")]
		[Category("Small Swing")]
		public double SmallDeviation
		{
			get{return smalldeviation;}
			set{smalldeviation = Math.Max(.1,value);
				smalldeviation = Math.Min(.6,smalldeviation);}
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
        private dsFibConfluence[] cachedsFibConfluence = null;

        private static dsFibConfluence checkdsFibConfluence = new dsFibConfluence();

        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        public dsFibConfluence dsFibConfluence(int showFibs)
        {
            return dsFibConfluence(Input, showFibs);
        }

        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        public dsFibConfluence dsFibConfluence(Data.IDataSeries input, int showFibs)
        {
            if (cachedsFibConfluence != null)
                for (int idx = 0; idx < cachedsFibConfluence.Length; idx++)
                    if (cachedsFibConfluence[idx].ShowFibs == showFibs && cachedsFibConfluence[idx].EqualsInput(input))
                        return cachedsFibConfluence[idx];

            lock (checkdsFibConfluence)
            {
                checkdsFibConfluence.ShowFibs = showFibs;
                showFibs = checkdsFibConfluence.ShowFibs;

                if (cachedsFibConfluence != null)
                    for (int idx = 0; idx < cachedsFibConfluence.Length; idx++)
                        if (cachedsFibConfluence[idx].ShowFibs == showFibs && cachedsFibConfluence[idx].EqualsInput(input))
                            return cachedsFibConfluence[idx];

                dsFibConfluence indicator = new dsFibConfluence();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.ShowFibs = showFibs;
                Indicators.Add(indicator);
                indicator.SetUp();

                dsFibConfluence[] tmp = new dsFibConfluence[cachedsFibConfluence == null ? 1 : cachedsFibConfluence.Length + 1];
                if (cachedsFibConfluence != null)
                    cachedsFibConfluence.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cachedsFibConfluence = tmp;
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
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.dsFibConfluence dsFibConfluence(int showFibs)
        {
            return _indicator.dsFibConfluence(Input, showFibs);
        }

        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        public Indicator.dsFibConfluence dsFibConfluence(Data.IDataSeries input, int showFibs)
        {
            return _indicator.dsFibConfluence(input, showFibs);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.dsFibConfluence dsFibConfluence(int showFibs)
        {
            return _indicator.dsFibConfluence(Input, showFibs);
        }

        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        public Indicator.dsFibConfluence dsFibConfluence(Data.IDataSeries input, int showFibs)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.dsFibConfluence(input, showFibs);
        }
    }
}
#endregion
