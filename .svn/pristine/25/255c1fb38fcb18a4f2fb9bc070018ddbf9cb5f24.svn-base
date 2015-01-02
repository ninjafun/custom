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
    /// SuperTrend, Raghee, Bill Williams, MACD, MA
    /// </summary>
    [Description("SuperTrend, Raghee, Bill Williams, MACD, MA")]
    public class AutomatedSuperTrend : Strategy
    {
        #region Variables
        // Wizard generated variables
        private bool backtest = true;  // Set true for backtesting

        private int STLength = 14;
        private double STMult = 2.618;
        private bool STArrows = true;
        private IOrder entryOrder = null;
        private IOrder stopOrder = null;
        private IOrder targetOrder = null;
        private double stopPrice = 0.0;
        private double newStopPrice = 0.0;
        private int curPos = 0;
        private int prevToTime = 0;
        
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            Add(PeriodType.Tick, 25);
            Add(SuperTrend(STLength, STMult, STArrows));
            Add(mahTrendGRaBerV1(34,34,34,2));
            Add(SMA(50));
            CalculateOnBarClose = true;
            EntryHandling = EntryHandling.UniqueEntries;
            ExitOnClose = false;
            EntriesPerDirection = 10000;
            BarsRequired = 51;
            //Unmanaged = true;
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

            if (BarsInProgress == 1)
            {
                //if (curPos == 0)
                if (Position.MarketPosition == MarketPosition.Flat)
                {
                    //entryOrder = null;
                    //stopOrder = null;

                    if (SuperTrend(Closes[0], STLength, STMult, STArrows).Trend[0] &&
                        EMA(Lows[0], 34)[0] >= SMA(Closes[0], 50)[0] &&
                        Closes[1][0] <= EMA(Highs[0], 34)[0] + 50 * TickSize &&
                        Closes[1][0] >= SMA(Closes[0], 50)[0])
                    {
                        if ((ToTime(Times[1][0]) - prevToTime) > 1000)
                        {
                            prevToTime = ToTime(Times[1][0]);
                            //entryOrder = SubmitOrder(0, OrderAction.Buy, OrderType.Market, 10000, 0, 0, "", "Long");
                            stopPrice = getStopPrice();
                            EnterLong(10000, "STUp");
                            //curPos = curPos + 10000;

                            SetStopLoss("STUp", CalculationMode.Price, stopPrice, false);
                            SetProfitTarget("STUp", CalculationMode.Price, Closes[0][0] + (EMA(Highs[0], 34)[0] - EMA(Lows[0], 34)[0]) * 500000 * TickSize);
                        }
                    }
                }
            }
            else if (BarsInProgress == 0)
            {
                if (Position.MarketPosition == MarketPosition.Long)
                //if (curPos > 0)
                {
                    newStopPrice = getStopPrice();
                    if (newStopPrice > stopPrice)
                    {
                        stopPrice = newStopPrice;
                        SetStopLoss("STUp", CalculationMode.Price, stopPrice, false);
                        //ChangeOrder(stopOrder, 0, 0, stopPrice);
                    }

                    //if (stopOrder == null)
                    //{
                    //    stopPrice = getStopPrice();
                    //    stopOrder = SubmitOrder(0, OrderAction.SellShort, OrderType.Stop, 10000, 0, stopPrice, "", "Stop");
                    //}
                    //else
                    //{
                    //    newStopPrice = getStopPrice();
                    //    if (newStopPrice != stopPrice)
                    //    {
                    //        stopPrice = newStopPrice;
                    //        ChangeOrder(stopOrder, 0, 0, stopPrice);
                    //    }
                    //}
                }
            }
            //Long 

  

            
        }
        //protected override void OnExecution(IExecution execution)
        //{
        //    if (entryOrder != null && entryOrder == execution.Order)
        //    {
        //        stopPrice = getStopPrice();
        //        stopOrder = SubmitOrder(0, OrderAction.SellShort, OrderType.Stop, 10000, 0, stopPrice, "OCO", "Stop");
        //        targetOrder = SubmitOrder(0, OrderAction.Sell, OrderType.Limit, 10000, 0, Position.AvgPrice + 10 * TickSize * 10, "OCO", "Target");
        //    }
        //}

        //protected override void OnPositionUpdate(IPosition position)
        //{
        //    if (position.MarketPosition == MarketPosition.Flat)
        //    {
        //        entryOrder = null;
        //        stopOrder = null;
        //    }
        //    //else
        //    //{
        //    //    stopPrice = getStopPrice();
        //    //    stopOrder = SubmitOrder(0, OrderAction.SellShort, OrderType.Stop, 10000, 0, stopPrice, "", "Stop");
        //    //}
        //}
        public double getStopPrice()
        {
            return SuperTrend(Closes[0], STLength, STMult, STArrows).UpTrend[0] - 50 * TickSize;
        }

        #region Properties
        [Description("")]
        [GridCategory("Parameters")]
        public bool BackTest
        {
            get { return backtest; }
            set { backtest = value; }
        }
        #endregion
    }
}
