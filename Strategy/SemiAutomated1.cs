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
    /// User will enter orders. Strategy will pick it up and will send its own orders.
    /// </summary>
    [Description("User will enter orders. Strategy will pick it up and will send its own orders.")]
    public class SemiAutomated1 : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int target1 = 1; // Default setting for Target1
        private int exitPercent1 = 100; // Default setting for ExitPercent1
        private int maxTotalQty = 100000; // Default setting for MaxTotalQty
        private bool longEntry = true; // Default setting for LongEntry
        private bool shortEntry = true; // Default setting for ShortEntry
        private double maxTradeLoss = 500.000; // Default setting for MaxTradeLoss
        private double maxDailyLoss = 1000.000; // Default setting for MaxDailyLoss
        private double maxTradeWin = 1000; // Default setting for MaxTradeWin
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
        }

        #region Properties
        [Description("")]
        [GridCategory("Parameters")]
        public int Target1
        {
            get { return target1; }
            set { target1 = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int ExitPercent1
        {
            get { return exitPercent1; }
            set { exitPercent1 = Math.Max(60, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int MaxTotalQty
        {
            get { return maxTotalQty; }
            set { maxTotalQty = Math.Max(1000, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public bool LongEntry
        {
            get { return longEntry; }
            set { longEntry = value; }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public bool ShortEntry
        {
            get { return shortEntry; }
            set { shortEntry = value; }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public double MaxTradeLoss
        {
            get { return maxTradeLoss; }
            set { maxTradeLoss = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public double MaxDailyLoss
        {
            get { return maxDailyLoss; }
            set { maxDailyLoss = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public double MaxTradeWin
        {
            get { return maxTradeWin; }
            set { maxTradeWin = Math.Max(1, value); }
        }
        #endregion
    }
}
