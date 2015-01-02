using System.Collections.Generic;

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
    /// TTMWaves
    /// </summary>
    [Description("TTMWaves")]
    public class TTM1 : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int minOrderSize;

        // User defined variables (add any user defined variables below)
        private bool backtest = true;  // Set true for backtesting
        private double myTickSize = 0;
        private Dictionary<string, int> direction; 
        private static List<DateTime> eventsList = new List<DateTime>(); 
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            direction = new Dictionary<string, int>();
            direction.Add("long",1);
            direction.Add("short", -1);
            direction.Add("none", 0);
            CalculateOnBarClose = true;
            BarsRequired = 70;
            ExitOnClose = false;
            Add(mahTrendGRaBerV1(34, 34, 34, 2));
            Add(SMA(51));
            Add(SMA(200));
            Add(TTMWaveAOC(false));
            Add(TTMWaveBOC(false));
            Add(TTMWaveCOC(false));
            EntriesPerDirection = 300000;
            EntryHandling = EntryHandling.AllEntries;
            eventsList.Clear();
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
            EntryHandling = EntryHandling.UniqueEntries;
            myTickSize = TickSize * 10;

            #region CheckTrend
            string strongTrend = "flat";
            int trend = 0;
            int numOfTrendConditions = 0;

            //Check Direction to look entry to

            //Check 200 SMA risign/falling/flat
            trend = trend + SeriesTrend(SMA(200), 10);
            numOfTrendConditions++;

            //Check Raghe Horner indicator

            //Check TTM C Wave both waves
            //if (GetWaveBLong(0)>)

            if (Math.Abs(numOfTrendConditions) == Math.Abs(trend))
            {
                if (trend > 0)
                    strongTrend = "long";
                else if (trend < 0)
                    strongTrend = "short";
            }
            #endregion



        }

        #region Properties
        [Description("")]
        [GridCategory("Parameters")]
        public bool BackTest
        {
            get { return backtest; }
            set { backtest = value; }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int MinOrderSize
        {
            get { return minOrderSize; }
            set { MinOrderSize = Math.Max(10000, value); }
        }

        #endregion
    }
}
