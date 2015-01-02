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
    /// Enter the description of your strategy here
    /// </summary>
    [Description("Enter the description of your strategy here")]
    public class strKeltner : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int curUp = 1; // Default setting for CurUp
        private int curLo = 1; // Default setting for CurLo
        private int curMid = 1; // Default setting for CurMid
        private int curStop = 1; // Default setting for CurStop
        private int curTarget = 1; // Default setting for CurTarget
        private static int RSI_50 = 0;
        private const int RSI_50_NotCrossLong = -15;
        private static int RSI_Curr_Cross = 0;
        private static int RSI_Prev_Cross = 0;
        private static bool longDirection = false;
        private static bool shortDirection = false;
		

        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>sssss'as
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            //SetStopLoss("", CalculationMode.Percent, CurStop, false);
            Add(RSI(14, 3));
            Add(KeltnerChannel(1.5, 10));
            Add(EMA(20));
            Add(RSqueeze(SqueezeStyle.BBSqueeze));
            CalculateOnBarClose = true;
           
        }

        public bool CheckRSI50Long()
        {
            if ((RSI(14, 3).Default[0] > 50) && (RSI(14, 3).Default[1] <= 50) && (RSI_50 < RSI_50_NotCrossLong))
            {
                return true;
            }
            return false;            
        }
        public void UpdateRSICnt()
        {
            if ((RSI(14, 3)[0] >= 50) && (RSI(14, 3)[1] > 50))
            {
                RSI_50++;
            }
            else if ((RSI(14, 3)[0] <= 50) && (RSI(14, 3)[1] < 50))
            {
                RSI_50--;
            }
            else
                RSI_50 = 0;
        }
        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
			//Manage existing orders			
			//Verify what time is it now
			//Condition/Oscilators for entry 
            // Condition set 1
            
            if (CurrentBar < 20)
                return;

            if (Position.MarketPosition == MarketPosition.Flat)
            {
                if (CheckRSI50Long())
                {
                    SetStopLoss("long1", CalculationMode.Price, Close[0] - ((KeltnerChannel(1.5, 10).Upper[0] - KeltnerChannel(1.5, 10).Lower[0])), false);
                    //SetProfitTarget("long1", CalculationMode.Price, Close[0] + ((KeltnerChannel(1.5, 10).Upper[0] - KeltnerChannel(1.5, 10).Lower[0])));
                    SetProfitTarget("long1", CalculationMode.Ticks, 900);//Close[0] + ((KeltnerChannel(1.5, 10).Upper[0] - KeltnerChannel(1.5, 10).Lower[0])));
                    EnterLong(10000, "long1");
                    longDirection = true;
                    shortDirection = false;
                }
            }
            else if ((longDirection) && ! (shortDirection))
            {
                if (Close[0] <= KeltnerChannel(1.5, 10).Lower[0])
                {
                    ExitLong("long1");
                    longDirection = false;
                    shortDirection = false;
                }
            }


            //if (Close[0] >= KeltnerChannel(1.5, 10).Upper[0])
            //{
            //    EnterShort(DefaultQuantity, "target1");
            //}

            //// Condition set 2
            //if (Close[0] <= KeltnerChannel(1.5, 10).Upper[0])
            //{
            //    EnterLong(DefaultQuantity, "long1");
            //}
            UpdateRSICnt();
        }
	
        #region Properties
        [Description("")]
        [GridCategory("Parameters")]
        public int CurUp
        {
            get { return curUp; }
            set { curUp = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int CurLo
        {
            get { return curLo; }
            set { curLo = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int CurMid
        {
            get { return curMid; }
            set { curMid = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int CurStop
        {
            get { return curStop; }
            set { curStop = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int CurTarget
        {
            get { return curTarget; }
            set { curTarget = Math.Max(1, value); }
        }
        #endregion
    }
}
