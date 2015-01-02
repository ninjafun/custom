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
    public class UnamangedSample1 : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int counter = 1;
        private int numberOfExits = 1; // Default setting for NumberOfExits
        private double targetA = 1; // Default setting for TargetA
        private double targetB = 1; // Default setting for TargetB
        private double targetC = 1; // Default setting for TargetC
        private bool goLong = false; // Default setting for GoLong
        private bool goShort = true; // Default setting for GoShort       
        private int quantityA = 0;
        private int quantityB = 0;
        private int quantityC = 0;
        private int totalQuantity;
        private static string curAcctName;
        private static string curInstrument;
        private static bool firstTime = true;
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {                    
            Add(SMA(50));
            Add(RValueCharts(false, false, 2, Color.Red, Color.Blue, Color.Red, Color.Thistle, 5, true, Color.BurlyWood, Color.Black, 1, 60, false, false, Color.Green, global::RValueCharts.Utility.ValueChartStyle.CandleStick));
            Add(MACD(12, 26, 9));
            Unmanaged = true;           
            curAcctName = Account.Name;
            curInstrument = Instrument.FullName;
            CalculateOnBarClose = true;
            RealtimeErrorHandling = NinjaTrader.Strategy.RealtimeErrorHandling.TakeNoAction;
            ExitOnClose = false;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            if (firstTime)
            {
                totalQuantity = GetAccountQuantity(curAcctName);
                switch (numberOfExits)
                {
                    case 1:
                        quantityA = totalQuantity;
                        break;
                    case 2:
                        quantityA = Convert.ToInt32(totalQuantity * 0.6);
                        quantityB = totalQuantity - quantityA;
                        break;
                    case 3:
                        quantityA = Convert.ToInt32(totalQuantity * 0.5);
                        quantityB = Convert.ToInt32(totalQuantity * 0.3);
                        quantityC = totalQuantity - quantityA - quantityB;
                        break;
                    default:
                        quantityA = totalQuantity;
                        break;
                }
                Log("Quanity A is:" + quantityA + "Quanity B is:" + quantityB + "Quanity C is:" + quantityC, LogLevel.Error);
                firstTime = false;
            }
            //if (FirstTickOfBar)
            //{
            //    Log("first tick of the bar now" + counter++, LogLevel.Warning);
            //}
            // Condition set 1
            if (SMA(50)[0] == Close[0])
            {
                EnterLong(DefaultQuantity, "longa");
            }
        }
        private int GetAccountQuantity(string accountName)
        {
            foreach (Account acct in Cbi.Globals.Accounts)
            {
                Log("acct.Name=" + acct.Name + ", curAcctName=" + curAcctName, LogLevel.Error);
                if (acct.Name != curAcctName) continue;
                if (acct.Positions != null)
                {
                    PositionCollection positions = acct.Positions;
                    foreach (Position pos in positions)
                    {
                        Print(pos.Account.Name + " " + pos.Instrument + " " + pos.MarketPosition + " " + pos.Quantity + " " + pos.AvgPrice);
                        Log(pos.Account.Name + " " + pos.Instrument + " " + pos.MarketPosition + " " + pos.Quantity + " " + pos.AvgPrice, LogLevel.Error);
                        return pos.Quantity;
                    }
                }
                else
                    return 0;
            }
            return -1;
        }
        #region Properties
        [Description("")]
        [GridCategory("Parameters")]
        public int NumberOfExits
        {
            get { return numberOfExits; }
            set { numberOfExits = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public double TargetA
        {
            get { return targetA; }
            set { targetA = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public double TargetB
        {
            get { return targetB; }
            set { targetB = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public double TargetC
        {
            get { return targetC; }
            set { targetC = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public bool GoLong
        {
            get { return goLong; }
            set { goLong = value; }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public bool GoShort
        {
            get { return goShort; }
            set { goShort = value; }
        }
        #endregion
    }
}
