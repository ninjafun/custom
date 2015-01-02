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
    public class Raghee1 : Strategy
    {
        #region Variables
        // Wizard generated variables
        private string startTime = @"02:00"; // Default setting for StartTime
        private string endTime = @"11:00"; // Default setting for EndTime
        private bool tradeUpTrend = true; // Default setting for TradeUpTrend
        private bool tradeDownTrend = true; // Default setting for TradeDownTrend
        private bool tradeOnBarClose = true; // Default setting for TradeOnBarClose
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {

            CalculateOnBarClose = true;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            // Condition set 1
            if (EMA(Close, 20)[0] > EMA(Close, 50)[0])
            {
            }
        }

        #region Properties
        [Description("start time for position entry")]
        [GridCategory("Parameters")]
        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        [Description("end time for position entry")]
        [GridCategory("Parameters")]
        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        [Description("Whether to enter long")]
        [GridCategory("Parameters")]
        public bool TradeUpTrend
        {
            get { return tradeUpTrend; }
            set { tradeUpTrend = value; }
        }

        [Description("Whether to enter short")]
        [GridCategory("Parameters")]
        public bool TradeDownTrend
        {
            get { return tradeDownTrend; }
            set { tradeDownTrend = value; }
        }

        [Description("Trade on bar close only or not")]
        [GridCategory("Parameters")]
        public bool TradeOnBarClose
        {
            get { return tradeOnBarClose; }
            set { tradeOnBarClose = value; }
        }
        #endregion
    }
}
