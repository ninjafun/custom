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
    [Description("3 targets May23 2010")]
    public class str3targerts : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int smalength = 200;
		private int emalength = 100;
		private int hmalength = 34;
		
		private int target1 = 12;
		private int target2 = 10;
		private int target3 = 24;
		
		private int stop = 24;
		
		private bool be2 = true;
		private bool be3 = true;

        private double myTickSize = 0;
		
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            CalculateOnBarClose = true;
			EntryHandling		= EntryHandling.UniqueEntries;
			ExitOnClose = false;
			EntriesPerDirection = 10000;
        }
		
		private void GoLong()
		{
			SetStopLoss("target1",CalculationMode.Price, Close[0] - (Stop*myTickSize), false);
			SetStopLoss("target2",CalculationMode.Price, Close[0] - (Stop*myTickSize), false);
			SetStopLoss("target3",CalculationMode.Price, Close[0] - (Stop*myTickSize), false);
			
			SetProfitTarget("target1",CalculationMode.Price, Close[0] + (Target1*myTickSize));
			SetProfitTarget("target2",CalculationMode.Price, Close[0] + ((Target1+Target2)*myTickSize));
			SetProfitTarget("target3",CalculationMode.Price, Close[0] + ((Target1+Target2+Target3)*myTickSize));
			
			EnterLong("target1");
			EnterLong("target2");
			EnterLong("target3");
			
		}
		private void GoShort()
		{
			SetStopLoss("target1",CalculationMode.Price, Close[0] + (Stop*myTickSize), false);
			SetStopLoss("target2",CalculationMode.Price, Close[0] + (Stop*myTickSize), false);
			SetStopLoss("target3",CalculationMode.Price, Close[0] + (Stop*myTickSize), false);
			
			SetProfitTarget("target1",CalculationMode.Price, Close[0] - (Target1*myTickSize));
			SetProfitTarget("target2",CalculationMode.Price, Close[0] - ((Target1+Target2)*myTickSize));
			SetProfitTarget("target3",CalculationMode.Price, Close[0] - ((Target1+Target2+Target3)*myTickSize));
			
			EnterShort("target1");
			EnterShort("target2");
			EnterShort("target3");
			
		}
		
		private void ManageOrders()
		{			
			if (Position.MarketPosition == MarketPosition.Long)
			{
                if (BE3 && High[0] > Position.AvgPrice + ((Target1 + Target2) * myTickSize))
                    SetStopLoss("target3", CalculationMode.Price, Position.AvgPrice, false);
				else if (BE2 && High[0] > Position.AvgPrice + (Target1 * myTickSize))
                {
                    SetStopLoss("target2", CalculationMode.Price, Position.AvgPrice - ((Stop*myTickSize)/2), false);
                    SetStopLoss("target3", CalculationMode.Price, Position.AvgPrice - ((Stop*myTickSize)/2), false);
                }				
			}
			if (Position.MarketPosition == MarketPosition.Short)
			{
                if (BE3 && High[0] < Position.AvgPrice - ((Target1+Target2)*myTickSize))
                    SetStopLoss("target3", CalculationMode.Price, Position.AvgPrice, false);								
                else if (BE2 && High[0] < Position.AvgPrice - (Target1 * myTickSize))
                {
                    SetStopLoss("target2", CalculationMode.Price, Position.AvgPrice + ((Stop*myTickSize)/2), false);
                    SetStopLoss("target3", CalculationMode.Price, Position.AvgPrice + ((Stop*myTickSize)/2), false);
                }
			}
		}
		
        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
			EntryHandling	= EntryHandling.UniqueEntries;
            myTickSize = TickSize*10;
			
			SMA	smav		= SMA(SMAlength);
			EMA	emav		= EMA(EMAlength);
			HMA	hmav		= HMA(HMAlength);
			
			ManageOrders();
			
			if (Position.MarketPosition != MarketPosition.Flat) return;
			
			if (Rising(smav) && Rising(emav) && Rising(hmav))
				GoLong();			
			else if (Falling(smav) && Falling(emav) && Falling(hmav))
				GoShort();
        }

        #region Properties
        [Description("")]
        [GridCategory("Parameters")]
        public int SMAlength
        {
            get { return smalength; }
            set { smalength = Math.Max(1, value); }
        }
        [Description("")]
        [GridCategory("Parameters")]
        public int EMAlength
        {
            get { return emalength; }
            set { emalength = Math.Max(1, value); }
        }
        [Description("")]
        [GridCategory("Parameters")]
        public int HMAlength
        {
            get { return hmalength; }
            set { hmalength = Math.Max(1, value); }
        }		
        [Description("")]
        [GridCategory("Parameters")]
        public int Target1
        {
            get { return target1; }
            set { target1 = Math.Max(1, value); }
        }		
        [Description("")]
        [GridCategory("Parameters")]
        public int Target2
        {
            get { return target2; }
            set { target2 = Math.Max(1, value); }
        }		
        [Description("")]
        [GridCategory("Parameters")]
        public int Target3
        {
            get { return target3; }
            set { target3 = Math.Max(1, value); }
        }		
        [Description("")]
        [GridCategory("Parameters")]
        public int Stop
        {
            get { return stop; }
            set { stop = Math.Max(1, value); }
        }			
        [Description("")]
        [GridCategory("Parameters")]
        public bool BE2
        {
            get { return be2; }
            set { be2 = value; }
        }				
        [Description("")]
        [GridCategory("Parameters")]
        public bool BE3
        {
            get { return be2; }
            set { be3 = value; }
        }			
		#endregion
    }
}
