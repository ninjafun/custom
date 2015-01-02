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
    /// Carter and Nemeth
    /// </summary>
    [Description("Carter and Nemeth")]
    public class Nemeth1 : Strategy
    {
        #region Variables
        private bool backtest = true;  // Set true for backtesting
        private double myTickSize = 0;
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            CalculateOnBarClose = true;
//            AddRenko(Instrument.FullName, 50, MarketDataType.Last);
            Add(PeriodType.Tick, 25);
            Add(HeikenAshi());
            CalculateOnBarClose = true;
            BarsRequired = 70;
            ExitOnClose = false;
            Add(SMA(51));
            Add(TTMWaveAOC(false));
            Add(TTMWaveBOC(false));
            Add(TTMWaveCOC(false));
//            myDataSeries = new DataSeries(this, MaximumBarsLookBack.Infinite);

        }

        protected override void OnStartUp()
        {
//            if (myDataSeries == null)
//            {
//                myDataSeries = new DataSeries(HeikenAshi(BarsArray[1]));
//            }
            EntryHandling = EntryHandling.UniqueEntries;
            myTickSize = TickSize * 10;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            if (backtest == false)
            {
                if (Historical)
                    return;
            }
        }

        #region Properties

        #endregion
    }
}
