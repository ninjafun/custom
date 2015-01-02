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
using RSqueeze.Utility;
#endregion

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    /// <summary>
    /// My new strategy March 13 2012
    /// </summary>
    [Description("My new strategy March 13 2012")]
    public class MacdSqueezeKeltnerMA : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int myParam1 = 1; // Default setting for MyParam1
        private int myParam2 = 1; // Default setting for MyParam2
        private int myParam3 = 1; // Default setting for MyParam3
        private bool firstTargetReached;
        private double EntryPrice;
        private double firstTarget;
        
        

        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {			
            Add(MACD(12, 26, 9));            
            Add(EMA(9));
            Add(EMA(14));
            Add(EMA(21));
            Add(KeltnerChannel(1.5, 10));
            Add(RSqueeze(SqueezeStyle.BBSqueeze));
            Add(bwAO());
            Add(PeriodType.Minute, 60);
            
            CalculateOnBarClose = true;
            EntryPrice = 0;
            firstTargetReached = false;
            firstTarget = 0;
            
            
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            if (CurrentBar <= 30)
                return;
            
            //Check whether position is flat, and whether there any orders pending.
            //if (Account.Orders.Count != 0)
            if (Position.MarketPosition != MarketPosition.Flat)
            {
                if (Position.MarketPosition == MarketPosition.Long)

                {
                    if (!firstTargetReached)
                    {
                        if ((High[0] > firstTarget) && (Convert.ToDouble(RSqueeze(SqueezeStyle.BBSqueeze).Mom[0]) > 0.0001))
                        {
                            firstTargetReached = true;
                            //SetStopLoss("SquezeLong1", CalculationMode.Price, EntryPrice, false);
                        }
                    }
                    else
                    {
                        ExitLong();
                        //if (Convert.ToDouble(RSqueeze(SqueezeStyle.BBSqueeze).Mom[0]) < -0.0003)
                        //    ExitLong();
                    }
                }

                ////manage existing position or how orders are getting filled.
                //foreach (Order accOrder in Account.Orders)
                //{

                //    Print("Order: " + accOrder.ToString());
                //}
                return;
            }
            firstTargetReached = false;
            EntryPrice = 0;
            firstTarget = 0;
            //Look to enter into a brand new position. Place stop/loss and target orders as well.
            //Check for Long condition:
            if (bwAO()[0] > 0)
            {
                if ((CrossAbove(MACD(12, 26, 9).Default, 0, 1)))
                //            if ((CrossAbove(MACD(12, 26, 9).FastEma, MACD(12, 26, 9).SlowEma, 5) && !(CrossBelow(MACD(12, 26, 9).FastEma, MACD(12, 26, 9).SlowEma, 5))) ||
                //CrossAbove(MACD(12, 26, 9).FastEma, 0, 1))
                {
                    double stop;
                    if (Close[0] >= KeltnerChannel(1.5, 10).Upper[0] - (KeltnerChannel(1.5, 10).Upper[0] - KeltnerChannel(1.5, 10).Lower[0]) / 2)
                        stop = KeltnerChannel(1.5, 10).Lower[0];
                    else
                        stop = Close[0] - (KeltnerChannel(1.5, 10).Upper[0] - KeltnerChannel(1.5, 10).Lower[0]) / 2;
                    EntryPrice = Close[0];
                    firstTarget = Close[0] + (KeltnerChannel(1.5, 10).Upper[0] - KeltnerChannel(1.5, 10).Lower[0]);
                    SetStopLoss("SquezeLong1", CalculationMode.Price, stop, false);
                    SetProfitTarget("SquezeLong1", CalculationMode.Price, Close[0] + 30 * ((KeltnerChannel(1.5, 10).Upper[0] - KeltnerChannel(1.5, 10).Lower[0])));
                    EnterLong(10000, "SquezeLong1");
                }
            }
            // Condition set 1
            //if (CrossAbove(MACD(12, 26, 9).Avg, MACD(12, 26, 9).Avg, 1)
            //    && Close[1] < EMA(14)[1])
            //{
            //    EnterLong(10000, "long");
            //}
        }
        #region OnTerminition()
        //protected override void OnTermination()
        ////This method is called once when a stratgey is disabled
        //{
        //    Print("");
        //    Print("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
        //    Print(DateTime.Now + " SYSTEM MONITOR TERMINATED.");
        //    Print("Total Open Positions: " + Account.Positions.Count);
        //    System.Collections.IEnumerator ListPositions = Account.Positions.GetEnumerator();
        //    for (int i = 0; i < Account.Positions.Count; i++)
        //    {
        //        ListPositions.MoveNext();
        //        Print("Open Position: " + ListPositions.Current);
        //    }
        //    // TRY THIS >>> Print(Account.Orders. etc etc
        //    Print("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
        //    Print("");

        //    if (alertsWindow && Account.Positions.Count != 0) Alert("openpositions", NinjaTrader.Cbi.Priority.High, "Open Positions/SystemMonitor terminated", "triple_klaxon.wav", 10, Color.Red, Color.White);

        //    myTimer.Dispose(); // Cleans up the resources
        //    pingSender.Dispose();

        //}//Close OnTermination()

        #endregion
        #region Properties
        [Description("")]
        [GridCategory("Parameters")]
        public int MyParam1
        {
            get { return myParam1; }
            set { myParam1 = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int MyParam2
        {
            get { return myParam2; }
            set { myParam2 = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int MyParam3
        {
            get { return myParam3; }
            set { myParam3 = Math.Max(1, value); }
        }
        #endregion
    }
}
