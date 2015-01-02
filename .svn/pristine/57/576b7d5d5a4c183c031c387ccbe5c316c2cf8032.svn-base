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
	
	public enum lot_type
	{
		en_fixed=1,  // Fixed
		en_percent=2 // Percent of deposit
	}		
	
    /// <summary>
    /// championship 2010 M
    /// </summary>
    [Description("championship 2010 M")]
    public class avoitenko : Strategy
    {
			
        #region Variables
	
	
//+------------------------------------------------------------------+
//|   Input parameters                                               |
//+------------------------------------------------------------------+
        private int 	period;//			= 	  250; // Default setting for PERIOD
		private int     stop_loss      	=      50; // The level of stop loss 
		private int     take_profit    	=     100; // The level of Take Profit
		private int     inside_level   	=      30; // Level back into the channel
		private int     trailing_stop  	=      20; // Trailing Stop Level
		private int     trailing_step  	=      10; // Trailing Step Level
		private int     order_step     	=      10; // Step movement of a pending order
		private ushort  slippage       	=       2; // Slippage
		private double  lot            	=       5; // Lot value		
		private int     magic_number   	=     867; // Unique adviser number
		public lot_type   EN_LOT_TYPE;//       =   en_fixed; // Lot type
//+------------------------------------------------------------------+
//| Global variables                                                 |
//+------------------------------------------------------------------+
		private bool buy_open    = false; // buy flag
		private bool sell_open   = false; // sell flag
		private double CalcHigh  = 0;     // resistance level
		private double CalcLow   = 0;     // support level
		private double order_open_price;  // open order price
		private double spread;            // spread
		private ulong stop_level;         // minimum level of the price to install a Stop Loss / Take Profit
		private ulong order_type;         // order type
		private ulong order_ticket;       // order ticket 
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
			if (EN_LOT_TYPE.ToString() =="")
			{
				EN_LOT_TYPE=lot_type.en_fixed;
			}
            CalculateOnBarClose = true;
			
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
			CalcHigh=HighestBar(High, period);
			CalcLow=LowestBar(Low, period);
			if(CalcHigh<0.01 || CalcLow<0.01) return;
			//spread=Ask[0]-Bid[0];
			
        }

        #region Properties
        [Description("")]
        [GridCategory("Parameters")]
        public int PERIOD
        {
            get { return period; }
            set { period = Math.Max(1, value); }
        }
        [Description("")]
        [GridCategory("Parameters")]
        public lot_type LOT_TYPE
        {
            get { return EN_LOT_TYPE; }
            set { EN_LOT_TYPE = value; }
        }		
        #endregion
    }
}
