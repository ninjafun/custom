using System.Collections.Generic;
using NinjaTrader.Cbi;

#region Using declarations

using System;
using System.ComponentModel;
using NinjaTrader.Data;

#endregion

public enum PositionAction
{
    ScaleInToLong,
    ScaleInToShort,
    ScaleOutFromLong,
    ScaleOutFromShort,
    CloseLong,
    CloseShort,
    DoNothing
}
// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Custom.Strategy
{
    /// <summary>
    /// User will enter orders. Strategy will pick it up and will send its own orders.
    /// </summary>
    [Description("User will enter orders. Strategy will pick it up and will send its own orders.")]
    public class SemiAutomated1 : NinjaTrader.Strategy.Strategy
    {
        #region Variables
        // Wizard generated variables
        private static int _target1 = 1; // Default setting for Target1
        private static int _exitPercent1 = 100; // Default setting for ExitPercent1
        private static int _maxTotalQty = 100000; // Default setting for MaxTotalQty
        private static bool _longEntry = true; // Default setting for LongEntry
        private static bool _shortEntry = true; // Default setting for ShortEntry
        private static double _maxTradeLoss = 500.000; // Default setting for MaxTradeLoss
        private static double _maxDailyLoss = 1000.000; // Default setting for MaxDailyLoss
        private static double _maxTradeWin = 1000; // Default setting for MaxTradeWin
        private static ConnectionStatus _orderConnectionStatus;
        private static ConnectionStatus _priceConnectionStatus;
        private static MarketPosition _marketPosition;
        private static int _totalPositionQuantity;
        private double _unrealizedPnl;
        private static int _managedPositionQuantity;
        private static int _unmanagedPositionQuantity;
        private DataSeries _MyHeikenAshiSeries;
        private static PositionAction _positionAction = PositionAction.DoNothing;
        private  static int percForLongExit;
        private static int percForShortExit;
        List<IOrder> _managedOrderList = new List<IOrder>();
        List<Order> _unmanagedOrderList = new List<Order>();
        //private DataSeries myDataSeries;

        // User defined variables (add any user defined variables below)
        #endregion

        protected void ResetValues()
        {
            percForLongExit = 0;
            percForShortExit = 0;
            _totalPositionQuantity = 0;
            _unrealizedPnl = 0;
            _managedPositionQuantity = 0;
            _unmanagedPositionQuantity = 0;
            _unmanagedOrderList.Clear();
        }
        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            ClearOutputWindow();
            CalculateOnBarClose = true;
            BarsRequired = 70;
            ExitOnClose = false;
            Unmanaged = true;
            RealtimeErrorHandling = NinjaTrader.Strategy.RealtimeErrorHandling.TakeNoAction;
            SyncAccountPosition = false;
            _totalPositionQuantity = 0;
            _unrealizedPnl = 0;
            AddRenko(Instrument.FullName, 50, MarketDataType.Last);
            //myDataSeries = new DataSeries(this, MaximumBarsLookBack.TwoHundredFiftySix);
            //Add(PeriodType.Tick, 10);
            
            

            //Log("Error", LogLevel.Error);
            //Log("Information", LogLevel.Information);
            //Log("Warning", LogLevel.Warning);
        }
        //protected override void OnStart()
        //{
        //    if (myDataSeries == null)
        //    {
        //        myDataSeries = new DataSeries(HeikenAshi(BarsArray[1]));
        //    }
        //}
        //protected override void OnOrderStatus(OrderStatusEventArgs e)
        //{
        //    base.OnOrderStatus(e);
        //    Log("myOnOrderStatus", LogLevel.Warning);
        //}

        //protected override void OnOrderUpdate(IOrder order)
        //{
        //    base.OnOrderUpdate(order);
        //    Log("myOnOrderUpdate", LogLevel.Warning);
        //}

        //protected override void OnExecution(IExecution execution)
        //{
        //    base.OnExecution(execution);
        //    Log("myOnExecution", LogLevel.Warning);
        //}

        //protected override void OnExecutionUpdate(ExecutionUpdateEventArgs e)
        //{
        //    base.OnExecutionUpdate(e);
        //    Log("myOnExecutionUpdate", LogLevel.Warning);
        //}

        //protected override void OnPositionUpdate(IPosition position)
        //{
        //    //base.OnPositionUpdate(position);

        //    Log("myOnPositionUpdate", LogLevel.Warning);
        //    Log("myPosition Qty is: " + NtGetPositionQty(Account, Instrument), LogLevel.Warning);
        //    Log("myPosition AvgPrice is: " + position.AvgPrice, LogLevel.Warning);
        //    Log("myPosition MarketPosition is: " + NtGetPositionDirection(Account,Instrument), LogLevel.Warning);
        //    position = null;
        //}

        //protected override void OnOrderTrace(DateTime timestamp, string message)
        //{
        //    base.OnOrderTrace(timestamp, message);
        //    Log("myOnOrderTrace", LogLevel.Warning);
        //}

        protected override void OnConnectionStatus(ConnectionStatus orderStatus, ConnectionStatus priceStatus)
        {
            _orderConnectionStatus = orderStatus;
            _priceConnectionStatus = priceStatus;
            if (_orderConnectionStatus != ConnectionStatus.Connected || _priceConnectionStatus != ConnectionStatus.Connected)
            {
                return;
            }
            ResetValues();
            _managedOrderList.Clear();
            _unmanagedOrderList.Clear();
            NtCancelAllLimitOrders(Account, Instrument);
            _marketPosition = NtGetPositionDirection(Account, Instrument);
            if (_marketPosition != MarketPosition.Flat)
            {
                _totalPositionQuantity = NtGetUnrealizedQuantity(Account, Instrument);
                _unrealizedPnl = NtGetUnrealizedNotional(Account, Instrument);
            }
            //NtPopulateManualOrders(Account, Instrument, ref _unmanagedOrderList);
        }

        protected override void OnTermination()
        {
            _unmanagedOrderList.Clear();
        }

        int PercentForLongExit()
        {
            bool exitWaveA = (NtGetWaveALong(0) < 0) && (NtGetWaveAShort(0) < 0);
            bool exitWaveB = (NtGetWaveBLong(0) < 0) && (NtGetWaveBShort(0) < 0);
            if (exitWaveA && exitWaveB)
                return 100;
            return 0;
        }

        int PercentForShortExit()
        {
            bool exitWaveA = (NtGetWaveALong(0) > 0) && (NtGetWaveAShort(0) > 0);
            bool exitWaveB = (NtGetWaveBLong(0) > 0) && (NtGetWaveBShort(0) > 0);
            if (exitWaveA && exitWaveB)
                return 100;
            return 0;
        }

        int PercentForLongEntry()
        {
            return 0;
        }

        int PercentForShortEntry()
        {
            return 0;
        }

        int QtyForLongEntry()
        {
            return 0;
        }

        int QtyForShortEntry()
        {
            return 0;
        }
            
        bool ShouldEnterLong()
        {
            return false;
        }

        bool ShouldEnterShort()
        {
            return false;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            if (Historical)
                return;

            if (CurrentBars[0] <= BarsRequired || CurrentBars[1] <= BarsRequired)
                return;

            if (_totalPositionQuantity == 0 && (ToTime(Time[0]) <= 210000 && ToTime(Time[0]) >= 110000))
            {
                return;
            }


            if (BarsInProgress == 0)
            {
                if (_totalPositionQuantity > 0)
                {
                    percForLongExit = PercentForLongExit();
                    if (percForLongExit == 100)
                    {
                        NtClosePosition(Account, Instrument);
                        _totalPositionQuantity = 0;
                        _positionAction = PositionAction.DoNothing;
                        percForLongExit = 0;
                        return;
                    }
                    else if (percForLongExit > 0 && percForLongExit < 100)
                    {
                        _positionAction = PositionAction.ScaleOutFromLong;
                        return;
                    }
                    else if (percForLongExit == 0)
                    {
                        _positionAction = PositionAction.DoNothing;
                        return;
                    }
                    double percForLongEntry = PercentForLongEntry();
                    //Check for scaling into long position and then return
                }
                else if (_totalPositionQuantity < 0)
                {
                    percForShortExit = PercentForShortExit();
                    if (percForShortExit == 100)
                    {
                        NtClosePosition(Account, Instrument);
                        _totalPositionQuantity = 0;
                        _positionAction = PositionAction.DoNothing;
                        percForShortExit = 0;
                        return;
                    }
                    else if (percForShortExit > 0 && percForShortExit < 100)
                    {
                        _positionAction = PositionAction.ScaleOutFromShort;
                        return;
                    }
                    else if (percForShortExit == 0)
                    {
                        _positionAction = PositionAction.DoNothing;
                        return;
                    }
                    //Check for scaling into short position and then return
                }

                ////Check param whether to trade long or short  or both
                //if (LongEntry)
                //{
                //    if (ShouldEnterLong())
                //    {
                //        //EnterLong
                //        _totalPositionQuantity = NtGetPositionQty(Account, Instrument);
                //        if (_totalPositionQuantity != 0)
                //        {
                //            _marketPosition = MarketPosition.Long;
                //        }
                //    }
                //    return;
                //}
                //if (ShortEntry)
                //{
                //    if (ShouldEnterShort())
                //    {
                //        //EnterShort
                //        _totalPositionQuantity = NtGetPositionQty(Account, Instrument);
                //        if (_totalPositionQuantity != 0)
                //        {
                //            _marketPosition = MarketPosition.Short;
                //        }
                //    }
                //    return;
                //}
            }
            else if (BarsInProgress == 1)
            {
                
            }
            
            

            //if long or both - check long conditions
                //if long conditions match - enter position and exit function
            //if short position of both - check short conditions
                //if short conditions match - enter position and exit function


            //Check whether to close position
            //Check to add to position
            //Check to exit position

        }

        #region Properties
        [Description("")]
        [GridCategory("Parameters")]
        public int Target1
        {
            get { return _target1; }
            set { _target1 = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int ExitPercent1
        {
            get { return _exitPercent1; }
            set { _exitPercent1 = Math.Max(60, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int MaxTotalQty
        {
            get { return _maxTotalQty; }
            set { _maxTotalQty = Math.Max(1000, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public bool LongEntry
        {
            get { return _longEntry; }
            set { _longEntry = value; }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public bool ShortEntry
        {
            get { return _shortEntry; }
            set { _shortEntry = value; }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public double MaxTradeLoss
        {
            get { return _maxTradeLoss; }
            set { _maxTradeLoss = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public double MaxDailyLoss
        {
            get { return _maxDailyLoss; }
            set { _maxDailyLoss = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public double MaxTradeWin
        {
            get { return _maxTradeWin; }
            set { _maxTradeWin = Math.Max(1, value); }
        }
        #endregion
    }
}
