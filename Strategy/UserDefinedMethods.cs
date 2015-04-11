#region Using declarations
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using NLog;
using NLog.Config;
using NLog.Targets;
using SlingShot.Utility;
using LogLevel = NLog.LogLevel;// NinjaTrader.Cbi.LogLevel;
using System;
using System.ComponentModel;
using System.Drawing;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Indicator;
using NinjaTrader.Strategy;
#endregion

namespace NinjaTrader.Strategy
{
    public class OrderWrapper : Order, INotifyPropertyChanged
    {
        private OrderState _orderState;

        OrderWrapper()
        {
            _orderState = base.OrderState;
        }

        public OrderState OrderState
        {
            get
            {
                if (_orderState != base.OrderState)
                {
                    _orderState = base.OrderState;
                    PropertyChanged(this, new PropertyChangedEventArgs("OrderState"));
                }
                return _orderState;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }

    public static class Helper
    {
        private static LoggingConfiguration config = new LoggingConfiguration();
        private static FileTarget fileTarget = new FileTarget();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        public static bool BackTest = true;
        public static int _totalPositionQuantity = 0;
        public static Strategy st;
        private static string IsBackTest()
        {
            if (BackTest)
                return "BackTest";
            return "Real";
        }
        public static void LogSetup(string instrumentName, string accountName)
        {
            if (fileTarget.FileName == null)
            {
                if (IsBackTest() == "BackTest")
                {
                    fileTarget.FileName = "C:\\temp\\BackTest" + instrumentName + DateTime.Now + ".log"; 
                }
                else
                {
                    fileTarget.FileName = "C:\\temp\\" + instrumentName + "_" + accountName + ".log";    
                }
                
                fileTarget.Layout = "${longdate} ${callsite} ${level} ${event-context:item=StrategyId}  ${message}";

                config.AddTarget("file", fileTarget);
                // Step 4. Define rules
                LoggingRule rule2 = new LoggingRule("*", NLog.LogLevel.Trace, fileTarget);
                config.LoggingRules.Add(rule2);

                // Step 5. Activate the configuration
                LogManager.Configuration = config;
            }
        }

        public static void Log(string message, NLog.LogLevel logLevel)
        {
            logger.Log(logLevel, st.Time[0].ToString() + " " + message);
        }
    }

    /// <summary>
    /// This file holds all user defined strategy methods.
    /// </summary>
    partial class Strategy
    {

        //private enum PositionDirection
        //{
        //    Flat,
        //    Long,
        //    Short
        //};

        #region BillWilliams
        public bool NtAODifferentColor(int barsAgo)
        {
            int negCol = 0;
            int posCol = 0;
            for (int i = 1; i <= barsAgo; i++)
            {
                if (bwAO().AOValue[i] < 0)
                {
                    negCol++;
                }
                else if (bwAO().AOValue[i] > 0)
                {
                    posCol++;
                }
            }
            if ((negCol != 0) && (posCol != 0))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region RagheeHorner
        public bool NtRagheeDifferentColor(int barsAgo)
        {
            int negCol = 0;
            int posCol = 0;
            for (int i = 1; i <= barsAgo; i++)
            {
                if ((Open[i] <= Close[i]
                     && Close[i] > EMA(High, 34)[i]) ||
                    (Open[i] >= Close[i]
                     && Close[i] > EMA(High, 34)[i]))
                {
                    posCol++;
                }

                else if ((Open[i] <= Close[i]
                          && Close[i] < EMA(Low, 34)[i]) ||
                         (Open[i] >= Close[i]
                          && Close[i] < EMA(Low, 34)[i]))
                {
                    negCol++;
                }
            }
            if ((negCol != 0) && (posCol != 0))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region GeneralHelpers
        public double NtGetHighest(int numOfBars)
        {
            return BarsArray[0][BarsArray[0].HighestBar(numOfBars)];
            //double curHigh;
            //double mostHigh = High[0];
            //for (int i = 1; i <= numOfBars; i++)
            //{
            //    curHigh = High[i];
            //    if (curHigh > mostHigh)
            //    {
            //        mostHigh = curHigh;
            //    }
            //}
            //return mostHigh;
        }
        public double NtGetHighest(int dsIndex, int numOfBars)
        {
            return BarsArray[dsIndex][BarsArray[dsIndex].HighestBar(numOfBars)];
        }

        public double NtGetLowest(int numOfBars)
        {
            double curLow;
            double mostLow = Low[0];
            for (int i = 1; i <= numOfBars; i++)
            {
                curLow = Low[i];
                if (curLow < mostLow)
                {
                    mostLow = curLow;
                }
            }
            return mostLow;
        }
        
        /// <summary>
        /// Series Trend
        /// </summary>
        /// <param name="series"> input series</param>
        /// <param name="numOfBars"> number of bars </param>
        /// <returns>1 - rising, -1 falling, 0 flat or unknown</returns>
        public int NtSeriesTrend(IDataSeries series, int numOfBars)
        {
            bool rising = false;
            bool falling = false;
//            bool flat = false;

            int cnt = series.Count;
            for (int i = 1; i < cnt; i++)
            {
                if (series[i - 1] > series[i])
                {
                    rising = true;
                }
                else if (series[i - 1] < series[i])
                {
                    falling = true;
                }
//                else if (series[i - 1] == series[i])
//                {
//                    flat = true;
//                }
                if (rising && falling)
                {
                    return 0;
                }
            }
            if (rising && falling)
                return 0;

            if (rising)
                return 1;

            if (falling)
                return -1;

            return 0;
        }
        #endregion

        #region TTMWave

        /// <summary>
        /// Return whether the wave crossed
        /// </summary>
        /// <param name="direction">true for Long and false for Short</param>
        /// <param name="waveLongOrShort">true for UP and false for DOWN</param>
        /// <param name="waveA_B_or_C">could be A, B or C</param>
        /// <param name="numOfBars">Number of Bars ago </param>
        /// <returns>1 for long, -1 for short, 0 didn't cross</returns>
        public int NtIsWaveCrossed(bool direction, bool waveLongOrShort, string waveA_B_or_C, int numOfBars)
        {
            List<double> listValues = new List<double>();
            bool aboveZero = false;
            bool belowZero = false;
            switch (waveA_B_or_C)
            {
                case "A":
                    for (int i = 0; i <= numOfBars; i++)
                        listValues.Add(waveLongOrShort ? NtGetWaveALong(i) : NtGetWaveAShort(i));
                    break;
                case "B":
                    for (int i = 0; i <= numOfBars; i++)
                        listValues.Add(waveLongOrShort ? NtGetWaveBLong(i) : NtGetWaveBShort(i));
                    break;
                case "C":
                    for (int i = 0; i <= numOfBars; i++)
                        listValues.Add(waveLongOrShort ? NtGetWaveCLong(i) : NtGetWaveCShort(i));
                    break;
            }

            foreach (double val in listValues)
            {
                if (val > 0)
                    aboveZero = true;
                else if (val < 0)
                    belowZero = true;
                if (aboveZero && belowZero)
                {
                    if (val > 0)
                        return -1;
                    else
                        return 1;
                }
            }

            return 0;
        }

        public double NtGetWaveAShort(int barsAgo)
        {
            double rVal = 0.0;
            rVal = TTMWaveAOC(false).Wave1[barsAgo];
            return rVal;
        }
        public double NtGetWaveAShort(IDataSeries ds, int barsAgo)
        {
            double rVal = 0.0;
            rVal = TTMWaveAOC(ds, false).Wave1[barsAgo];
            return rVal;
        }
        public double NtGetWaveALong(int barsAgo)
        {
            double rVal = 0.0;
            rVal = TTMWaveAOC(false).Wave2[barsAgo];
            return rVal;
        }
        public double NtGetWaveALong(IDataSeries ds, int barsAgo)
        {
            double rVal = 0.0;
            rVal = TTMWaveAOC(ds, false).Wave2[barsAgo];
            return rVal;
        }
        public double NtGetWaveBShort(int barsAgo)
        {
            double rVal = 0.0;
            rVal = TTMWaveBOC(false).Wave1[barsAgo];
            return rVal;
        }

        public double NtGetWaveBShort(IDataSeries ds, int barsAgo)
        {
            double rVal = 0.0;
            rVal = TTMWaveBOC(ds, false).Wave1[barsAgo];
            return rVal;
        }

        public double NtGetWaveBLong(int barsAgo)
        {
            double rVal = 0.0;
            rVal = TTMWaveBOC(false).Wave2[barsAgo];
            return rVal;
        }
        public double NtGetWaveBLong(IDataSeries ds, int barsAgo)
        {
            double rVal = 0.0;
            rVal = TTMWaveBOC(ds, false).Wave2[barsAgo];
            return rVal;
        }
        public double NtGetWaveCShort(int barsAgo)
        {
            double rVal = 0.0;
            rVal = TTMWaveCOC(false).Wave1[barsAgo];
            return rVal;
        }

        public double NtGetWaveCLong(int barsAgo)
        {
            double rVal = 0.0;
            rVal = TTMWaveCOC(false).Wave2[barsAgo];
            return rVal;
        }

        public double NtGetSlingShotSlow(int barsAgo)
        {
            return SlingShot(Color.Red, Color.Green, 38, PriceType.Close, MovingAverageType.HMA, 80, 63, PriceType.Close,
                MovingAverageType.HMA).SlowMA[barsAgo];
        }
        public double NtGetSlingShotSlow(IDataSeries ds, int barsAgo)
        {
            return SlingShot(ds, Color.Red, Color.Green, 38, PriceType.Close, MovingAverageType.HMA, 80, 63, PriceType.Close,
                MovingAverageType.HMA).SlowMA[barsAgo];
        }
        public double NtGetSlingShotFast(int barsAgo)
        {
            return SlingShot(Color.Red, Color.Green, 38, PriceType.Close, MovingAverageType.HMA, 80, 63, PriceType.Close,
                MovingAverageType.HMA).FastMA[barsAgo];
        }
        public double NtGetSlingShotFast(IDataSeries ds, int barsAgo)
        {
            return SlingShot(ds, Color.Red, Color.Green, 38, PriceType.Close, MovingAverageType.HMA, 80, 63, PriceType.Close,
                MovingAverageType.HMA).FastMA[barsAgo];
        }
        #endregion

        #region OMS

        public double NtGetPositionTest(Account account, Instrument instrument)
        {
            var x1 = NtGetUnrealizedNotional(account, instrument);
            var x2 = NtGetAvgPrice(account, instrument);
            var x3 = NtGetPositionDirection(account, instrument);
            var x4 = NtGetUnrealizedPips(account, instrument);
            return 0;
        }

        public double NtGetUnrealizedNotional(Account account, Instrument instrument)
        {
            Position myPosition = account.Positions.FindByInstrument(instrument);
            if (myPosition == null)
            {
                return 0;
            }
            return myPosition.GetProfitLoss(Close[0], PerformanceUnit.Currency);
        }

        public double NtGetTotalNetNotional(Account account, Instrument instrument)
        {
            if (Helper.BackTest)
            {
                return NtGetTotalNetNotionalBackTest();
            }
            else
            {
                return NtGetTotalNetNotionalReal(account, instrument);
            }
        }
        private double NtGetTotalNetNotionalReal(Account account, Instrument instrument)
        {
            Position myPosition = account.Positions.FindByInstrument(instrument);
            if (myPosition == null)
            {
                return 0;
            }
            double realizedPnl = GetAccountValue(AccountItem.RealizedProfitLoss);
            double retVal = realizedPnl + myPosition.GetProfitLoss(Close[0], PerformanceUnit.Currency);
            return retVal;
        }
        private double NtGetTotalNetNotionalBackTest()
        {
            double realizedPnl = NtGetRealizedPnlBackTest();
            double unrealizedPnl = Position.GetProfitLoss(Close[0], PerformanceUnit.Currency);
            return realizedPnl + unrealizedPnl;
        }

        public void NtGetUnrealizedQuantity(Account account, Instrument instrument, ref int quantity, ref MarketPosition position)
        {
            Position myPosition = account.Positions.FindByInstrument(instrument);
            if (myPosition == null)
            {
                quantity = 0;
                position = MarketPosition.Flat;
            }
            else
            {
                quantity = myPosition.Quantity;
                position = myPosition.MarketPosition;
            }
        }

        private double NtGetRealizedPnlBackTest()
        {
            if (Performance.AllTrades.Count > 1)
                return Performance.AllTrades.TradesPerformance.Currency.CumProfit;
            else
                return 0;
        }

        public double NtGetUnrealizedPips(Account account, Instrument instrument)
        {
            if (!Helper.BackTest)
            {


                Position myPosition = account.Positions.FindByInstrument(instrument);
                if (myPosition == null)
                {
                    return 0;
                }
                else if (myPosition.MarketPosition == MarketPosition.Long)
                {
                    return myPosition.GetProfitLoss(GetCurrentBid(), PerformanceUnit.Points) * 10000;
                }
                else
                {
                    return myPosition.GetProfitLoss(GetCurrentAsk(), PerformanceUnit.Points) * 10000;
                }
            }
            if (Position.MarketPosition == MarketPosition.Long)
            {
                return (GetCurrentBid() - Position.AvgPrice)*10000;
            }
            else if (Position.MarketPosition == MarketPosition.Short)
            {
                return (Position.AvgPrice - GetCurrentAsk()) * 10000;
            }
            else
            {
                return 0;
            }
            
        }

        public void NtClosePosition(Account account, Instrument instrument, ref int _totalPositionQuantity)
        {
            if (!Helper.BackTest)
            {
                MarketPosition pos = NtGetPositionDirection(account, instrument);
                if (pos == MarketPosition.Flat)
                {
                    _totalPositionQuantity = 0;
                    return;                    
                }
                else if (pos == MarketPosition.Long)
                {
                    if (_totalPositionQuantity == 0)
                    {
                        Helper.logger.Error("Cannot close position because Qty is 0");
                        return;
                    }
                    SubmitOrder(0, OrderAction.Sell, OrderType.Market, _totalPositionQuantity, 0, 0,
                    "", "");
                }
                else if (pos == MarketPosition.Short)
                {
                    if (_totalPositionQuantity == 0)
                    {
                        Helper.logger.Error("Cannot close position because Qty is 0");
                        return;
                    }
                    SubmitOrder(0, OrderAction.Buy, OrderType.Market, _totalPositionQuantity, 0, 0,
                        "", "");
                }
                //Position myPosition = account.Positions.FindByInstrument(instrument);
                //if (myPosition == null)
                //{
                //    _totalPositionQuantity = 0;
                //    return;
                //}
                //myPosition.Close();
                //_totalPositionQuantity = 0;
                return;
            }
            if (Position.MarketPosition == MarketPosition.Long)
            {
                SubmitOrder(0, OrderAction.SellShort, OrderType.Market, _totalPositionQuantity, 0, 0,
                    "MyLongOCO", "MyLongSignal");
            }
            else if (Position.MarketPosition == MarketPosition.Short)
            {
                SubmitOrder(0, OrderAction.BuyToCover, OrderType.Market, _totalPositionQuantity, 0, 0,
                                "MyShortOCO", "MyShortSignal");
            }
            _totalPositionQuantity = 0;
        }

        public double NtGetAvgPrice(Account account, Instrument instrument)
        {
            Position myPosition = account.Positions.FindByInstrument(instrument);
            if (myPosition == null)
            {
                return 0;
            }
            return myPosition.AvgPrice;
        }

        public MarketPosition NtGetPositionDirection(Account account, Instrument instrument)
        {
            if (!Helper.BackTest)
            {

                Position myPosition = account.Positions.FindByInstrument(instrument);
                if (myPosition == null)
                {
                    return MarketPosition.Flat;
                }
                //if (myPosition.Quantity > 0)
                //    return MarketPosition.Long;
                //else if (myPosition.Quantity < 0)
                //    return MarketPosition.Short;
                //else
                //{
                //    return MarketPosition.Flat;
                //}
                
                return myPosition.MarketPosition;
            }
            return Position.MarketPosition;

            //int iOrderCount = Account.Orders.Count;
            //Print("Total Open Orders: " + iOrderCount);
            //System.Collections.IEnumerator ListOrders = Account.Orders.GetEnumerator();
            //for (int i = 0; i < iOrderCount; i++)
            //{
            //    ListOrders.MoveNext();
            //    Print(" Open Orders: " + ListOrders.Current);
            //    Order myOrder = ListOrders.Current as NinjaTrader.Cbi.Order;
            //    //if (myOrder.OrderState == OrderState.Working)
            //    //    myOrder.Cancel();
            //}

            //return PositionDirection.Flat;
        }

        public void NtPopulateManualOrders(Account account, Instrument instrument, ref List<Order> orders)
        {
            int iOrderCount = Account.Orders.Count;
            System.Collections.IEnumerator ListOrders = Account.Orders.GetEnumerator();
            for (int i = 0; i < iOrderCount; i++)
            {
                ListOrders.MoveNext();
                Order myOrder = ListOrders.Current as NinjaTrader.Cbi.Order;
                if ((myOrder != null) 
                    && ((myOrder.OrderState == OrderState.Working) 
                    || (myOrder.OrderState == OrderState.PartFilled)))
                {
                    orders.Add(myOrder);
                }
            }
        }

        public void NtCancelAllLimitOrders(Account account, Instrument instrument)
        {
            int iOrderCount = Account.Orders.Count;
            System.Collections.IEnumerator ListOrders = Account.Orders.GetEnumerator();
            for (int i = 0; i < iOrderCount; i++)
            {
                ListOrders.MoveNext();
                Order myOrder = ListOrders.Current as NinjaTrader.Cbi.Order;
                if ((myOrder != null)
                    && ((myOrder.OrderState == OrderState.Working)
                    || (myOrder.OrderState == OrderState.PartFilled)
                    || (myOrder.OrderState == OrderState.Initialized)
                    || (myOrder.OrderState == OrderState.PendingChange)
                    || (myOrder.OrderState == OrderState.PendingSubmit)
                    || (myOrder.OrderState == OrderState.Unknown)))
                {
                    try
                    {
                        myOrder.Cancel();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        //private double NtGetUnrealizedNotional(Account account, Instrument instrument)
        //{
        //    MarketPosition marketPosition = NtGetPositionDirection(account, instrument);
        //    if (marketPosition == MarketPosition.Flat)
        //        return 0;
        //    double averagePrice = NtGetAvgPrice(account, instrument);
        //    double workingQty = NtGetUnrealizedNotional()
        //}

        public int NtGetAccountQuantity(string accountName)
        {
            //IOrder ord = EnterLong(10000, "testorder");
            //while (ord.OrderState == OrderState.Working)
            //{
            //    Thread.Sleep(2000);
            //    Helper.Log("Working", LogLevel.Warning);
            //}
            foreach (Account acct in Cbi.Globals.Accounts)
            {
                
                Helper.Log("acct.Name=" + acct.Name + ", curAcctName=" + accountName, LogLevel.Error);
                if (acct.Name != accountName) continue;
                var x = GetAccountValue(AccountItem.CashValue);
                var x2 = GetAccountValue(AccountItem.TotalCashBalance);
                var x3 = GetAccountValue(AccountItem.BuyingPower);
                Helper.Log(String.Format("CashValue is {0}", x), NLog.LogLevel.Warn);
                var xa = acct.GetAccountValue(AccountItem.CashValue, Currency.UsDollar);
                var xa2 = acct.GetAccountValue(AccountItem.TotalCashBalance, Currency.UsDollar);
                var xa3 = acct.GetAccountValue(AccountItem.BuyingPower, Currency.UsDollar);
                if (acct.Positions != null)
                {
                    PositionCollection positions = acct.Positions;
                    foreach (Position pos in positions)
                    {
                        Print(pos.Account.Name + " " + pos.Instrument + " " + pos.MarketPosition + " " + pos.Quantity +
                              " " + pos.AvgPrice);
                        Helper.Log(
                            pos.Account.Name + " " + pos.Instrument + " " + pos.MarketPosition + " " + pos.Quantity +
                            " " + pos.AvgPrice, LogLevel.Error);
                        return pos.Quantity;
                    }
                }
                else
                    return 0;
            }
            return -1;
        }

        #endregion
    }
}