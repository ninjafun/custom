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
    /// Indicates the Fractal Level
    /// </summary>
    [Description("Draws the fractal pivot lines based on 7 fractal bar patterns")]
    public class FractalLevel : Indicator
    {
        #region Variables
        // Wizard generated variables
            private int myInput0 = 1; // Default setting for MyInput0
			private double upFractal;
			private double downFractal;
            private DataSeries upFractalSeries;
            private DataSeries downFractalSeries;
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the indicator and is called once before any bar data is loaded.
        /// </summary>
        protected override void Initialize()
        {
            Add(new Plot(Color.FromKnownColor(KnownColor.Green), PlotStyle.Line, "Plot0"));
            Add(new Plot(Color.FromKnownColor(KnownColor.Red), PlotStyle.Line, "Plot1"));
            upFractalSeries = new DataSeries(this);
            downFractalSeries = new DataSeries(this);
            Overlay				= true;
			CalculateOnBarClose = true;
			upFractal			= 0.0;
			downFractal			= 0.0;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            // Use this method for calculating your indicator values. Assign a value to each
            // plot below by replacing 'Close[0]' with your own formula.
			if (CurrentBar < 10)
				return;
		
			if(High[2]<High[3] && High[1]<High[3])
			{
				// Fractal type 1
				if( High[5]<High[3] && High[4]<High[3] )
					upFractal=High[3];

				// Fractal type 2
				else if( High[6]< High[3] && High[5]< High[3] && High[4]==High[3] )
					upFractal=High[3];

				// Fractal type 3, 4
				else if( High[7]< High[3] && High[6]< High[3] && High[5]==High[3] && High[4]<=High[3] )
					upFractal=High[3];

				// Fractal type 5
				else if( High[8]< High[3] && High[7]< High[3] && High[6]==High[3] && High[5]< High[3] && High[4]==High[3] )
					upFractal=High[3];

				// Fractal type 6
				else if( High[8]< High[3] && High[7]< High[3] && High[6]==High[3] && High[5]==High[3] && High[4]< High[3] )
					upFractal=High[3];

				// Fractal type 7
				else if( High[9]< High[3] && High[8]< High[3] && High[7]==High[3] && High[6]< High[3] && High[5]==High[3] && High[4]< High[3] )
					upFractal=High[3];
			}
			
			if(Low[2]>Low[3] && Low[1]>Low[3])
			{
				// Fractal type 1
				if( Low[5]>Low[3] && Low[4]>Low[3] )
					downFractal=Low[3];

				// Fractal type 2
				else if( Low[6]> Low[3] && Low[5]> Low[3] && Low[4]==Low[3] )
					downFractal=Low[3];

				// Fractal type 3, 4
				else if( Low[7]> Low[3] && Low[6]> Low[3] && Low[5]==Low[3] && Low[4]>=Low[3] )
					downFractal=Low[3];

				// Fractal type 5
				else if( Low[8]> Low[3] && Low[7]> Low[3] && Low[6]==Low[3] && Low[5]> Low[3] && Low[4]==Low[3] )
					downFractal=Low[3];

				// Fractal type 6
				else if( Low[8]> Low[3] && Low[7]> Low[3] && Low[6]==Low[3] && Low[5]==Low[3] && Low[4]> Low[3] )
					downFractal=Low[3];

				// Fractal type 7
				else if( Low[9]> Low[3] && Low[8]> Low[3] && Low[7]==Low[3] && Low[6]> Low[3] && Low[5]==Low[3] && Low[4]> Low[3] )
					downFractal=Low[3];
			}
			if(upFractal != 0)
				Plot0.Set(upFractal);
			if(downFractal !=0)
				Plot1.Set(downFractal);
            if (Math.Abs(Math.Abs(upFractalSeries[0]) - Math.Abs(upFractalSeries[1])) <= 0.0001)
            {
                upFractalSeries[0] = upFractalSeries[1];
            }
            else
                upFractalSeries[0] = upFractal;
            if (Math.Abs(Math.Abs(downFractalSeries[0]) - Math.Abs(downFractalSeries[1])) <= 0.0001)
            {
                downFractalSeries[0] = downFractalSeries[1];
            }
            else
                downFractalSeries[0] = downFractal;
        }

        #region Properties
        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries Plot0
        {
            get { return Values[0]; }
        }

        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries Plot1
        {
            get { return Values[1]; }
        }

        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries UpFractals
        {
            get { return upFractalSeries; }
        }

        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries DownFractals
        {
            get { return downFractalSeries; }
        }


        [Description("")]
        [GridCategory("Parameters")]
        public int MyInput0
        {
            get { return myInput0; }
            set { myInput0 = Math.Max(1, value); }
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
        private FractalLevel[] cacheFractalLevel = null;

        private static FractalLevel checkFractalLevel = new FractalLevel();

        /// <summary>
        /// Draws the fractal pivot lines based on 7 fractal bar patterns
        /// </summary>
        /// <returns></returns>
        public FractalLevel FractalLevel(int myInput0)
        {
            return FractalLevel(Input, myInput0);
        }

        /// <summary>
        /// Draws the fractal pivot lines based on 7 fractal bar patterns
        /// </summary>
        /// <returns></returns>
        public FractalLevel FractalLevel(Data.IDataSeries input, int myInput0)
        {
            if (cacheFractalLevel != null)
                for (int idx = 0; idx < cacheFractalLevel.Length; idx++)
                    if (cacheFractalLevel[idx].MyInput0 == myInput0 && cacheFractalLevel[idx].EqualsInput(input))
                        return cacheFractalLevel[idx];

            lock (checkFractalLevel)
            {
                checkFractalLevel.MyInput0 = myInput0;
                myInput0 = checkFractalLevel.MyInput0;

                if (cacheFractalLevel != null)
                    for (int idx = 0; idx < cacheFractalLevel.Length; idx++)
                        if (cacheFractalLevel[idx].MyInput0 == myInput0 && cacheFractalLevel[idx].EqualsInput(input))
                            return cacheFractalLevel[idx];

                FractalLevel indicator = new FractalLevel();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.MyInput0 = myInput0;
                Indicators.Add(indicator);
                indicator.SetUp();

                FractalLevel[] tmp = new FractalLevel[cacheFractalLevel == null ? 1 : cacheFractalLevel.Length + 1];
                if (cacheFractalLevel != null)
                    cacheFractalLevel.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheFractalLevel = tmp;
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
        /// Draws the fractal pivot lines based on 7 fractal bar patterns
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.FractalLevel FractalLevel(int myInput0)
        {
            return _indicator.FractalLevel(Input, myInput0);
        }

        /// <summary>
        /// Draws the fractal pivot lines based on 7 fractal bar patterns
        /// </summary>
        /// <returns></returns>
        public Indicator.FractalLevel FractalLevel(Data.IDataSeries input, int myInput0)
        {
            return _indicator.FractalLevel(input, myInput0);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// Draws the fractal pivot lines based on 7 fractal bar patterns
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.FractalLevel FractalLevel(int myInput0)
        {
            return _indicator.FractalLevel(Input, myInput0);
        }

        /// <summary>
        /// Draws the fractal pivot lines based on 7 fractal bar patterns
        /// </summary>
        /// <returns></returns>
        public Indicator.FractalLevel FractalLevel(Data.IDataSeries input, int myInput0)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.FractalLevel(input, myInput0);
        }
    }
}
#endregion
