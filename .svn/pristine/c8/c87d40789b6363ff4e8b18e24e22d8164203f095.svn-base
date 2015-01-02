#region Using declarations
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Indicator;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Strategy;
#endregion

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    /// <summary>
    /// Enter the description of your strategy here
    /// </summary>
    [Description("Enter the description of your strategy here")]
    public class MyCustomStrategy : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int myInput0 = 1; // Default setting for MyInput0
        // User defined variables (add any user defined variables below)
        #endregion
        private DateTime startTime;
        private DateTime endTime;
        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            //int basePerioudValue = BarsPeriod.BasePeriodValue;
            //// Add a 5 minute Bars object to the strategy
            //Add(PeriodType.Minute, basePerioudValue * 5);
            Add(RValueCharts(false, false, 2, Color.Red, Color.Blue, Color.Red, Color.Thistle, 5, true, Color.BurlyWood, Color.Black, 1, 60, false, false, Color.Green, global::RValueCharts.Utility.ValueChartStyle.CandleStick));
            //// Add a 15 minute Bars object to the strategy
            //Add(PeriodType.Minute, basePerioudValue * 25);
            
            CalculateOnBarClose = true;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            /*Check if any orders are pending
             * Check what type of strategy/timeframe is utilized
             * use LINQ to get data from XML/Classes
            */
            double x = (double)RValueCharts(false, false, 2, Color.Red, Color.Blue, Color.Red, Color.Thistle, 5, true, Color.BurlyWood, Color.Black, 1, 60, false, false, Color.Green, global::RValueCharts.Utility.ValueChartStyle.CandleStick).VHigh[0];
            //double y = (double)RValueCharts(false, false, 2, Color.Red, Color.Blue, Color.Red, Color.Thistle, 5, true, Color.BurlyWood, Color.Black, 1, 60, false, false, Color.Green, global::RValueCharts.Utility.ValueChartStyle.CandleStick).Low[0];
            double z = (double)RValueCharts(false, false, 2, Color.Red, Color.Blue, Color.Red, Color.Thistle, 5, true, Color.BurlyWood, Color.Black, 1, 60, false, false, Color.Green, global::RValueCharts.Utility.ValueChartStyle.CandleStick).VClose[0];
			if (Position.MarketPosition == MarketPosition.Flat)
			{
                //Check for possible entry:
                Print((double)RValueCharts(false, false, 2, Color.Red, Color.Blue, Color.Red, Color.Thistle, 5, true, Color.BurlyWood, Color.Black, 1, 60, false, false, Color.Green, global::RValueCharts.Utility.ValueChartStyle.CandleStick)[0]);
                //if No orders pending...
                //else if some orders pending...
                    //Manage existing position 
			}
            //Put some time filters
            if ((Time[0].TimeOfDay > startTime.TimeOfDay) && (Time[0].TimeOfDay < endTime.TimeOfDay))
                return;


			
        }

        #region Properties
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
