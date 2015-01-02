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
    /// just test
    /// </summary>
    [Description("just test")]
    public class RValStrategy3 : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int overbought = 8; // Default setting for Overbought
        private int oversold = -8; // Default setting for Oversold
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
			DrawDot("Over" + CurrentBar, false, 0, High[0] + 4 * TickSize, Color.Red);
            if (RValueCharts(false, true, 2, Color.Red, Color.Blue, Color.Red, Color.Thistle, 5, true, Color.BurlyWood, Color.Black, 1, 60, false, false, Color.Green, global::RValueCharts.Utility.ValueChartStyle.CandleStick).VClose[0] >= Overbought)
            {
                DrawDot("Over" + CurrentBar, false, 0, High[0] + 4 * TickSize, Color.Red);
            }
        }

        #region Properties
        [Description("when overbought")]
        [GridCategory("Parameters")]
        public int Overbought
        {
            get { return overbought; }
            set { overbought = Math.Max(8, value); }
        }

        [Description("when oversold")]
        [GridCategory("Parameters")]
        public int Oversold
        {
            get { return oversold; }
            set { oversold = Math.Max(-8, value); }
        }
        #endregion
    }
}
