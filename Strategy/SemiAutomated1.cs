#region Using declarations

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Indicator;
using NinjaTrader.Strategy;
using System;
using System.ComponentModel;
using NinjaTrader.Data;
using SlingShot.Utility;

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

        #region RiskOrderManagement

        private static string _strategyName = "slingshot";
        private static bool _execInProgress = false;
        private static int _instCounter = 0;
        private static int _lowTimeRange = 210000;
        private static int _upperTimeRange = 110000;
        private static int _target1 = 1; // Default setting for Target1
        private static int _exitPercent1 = 100; // Default setting for ExitPercent1
        private static int _maxTotalQty = 30000; // Default setting for MaxTotalQty
        private static bool _longEntry = true; // Default setting for LongEntry
        private static bool _shortEntry = true; // Default setting for ShortEntry
        private static double _maxTradeLoss = 500.000; // Default setting for MaxTradeLoss
        private static double _maxDailyLoss = 100; // Default setting for MaxDailyLoss
        private static double _maxTradeWin = 1000; // Default setting for MaxTradeWin
        private static bool _firstTime = true;
        private double _unrealizedPnl;
//        private double _totalNetPnl;
        private static int _percForLongExit;
        private static int _percForShortExit;
        private static MarketPosition _marketPosition = MarketPosition.Flat;
        private static int _totalPositionQuantity;
        private static ConnectionStatus _orderConnectionStatus;
        private static ConnectionStatus _priceConnectionStatus;
        private static int _managedPositionQuantity;
        private static int _unmanagedPositionQuantity;
        private static PositionAction _positionAction = PositionAction.DoNothing;
        private List<IOrder> _managedOrderList = new List<IOrder>();
        private List<Order> _unmanagedOrderList = new List<Order>();
        private static bool _backtest = true;
        private static bool _series1 = false;
        private static bool _series2 = false;
        private static int _stopLossTicksFromMA = 7;
        #endregion

        #region Renko

        private static int _renkoHeight = 50;
        private DataSeries _MyHeikenAshiSeries;
        private static double _rVar51Sma = 0;
        private double _rVarWaveBLong;
        private double _rVarWaveBShort;
        private double _rVarWaveAShort;
        private double _rVarWaveALong;
        private EMA _rVar20Ema;
        private static double _rVarUpperFractal0 = 0;
        private static double _rVarLowerFractal0 = 0;

        #endregion

        #region Tick

        private double _tVar51Sma;
        private double _tVar34EmaHigh;
        private double _tVar34EmaLow;
        private double _tVarWaveBLong;
        private double _tVarWaveBShort;
        private double _tVarWaveALong;
        private double _tVarWaveAShort;
        private double _tVarSlingShotSlow;
        private double _tVarSlingShotFast;
        private double _lowerEntryRange;
        private double _upperEntryRange;
        private PositionAction _desiredEntryDirection = PositionAction.DoNothing;
        private double _curAsk=0;
        private double _curBid=0;
        private double _tVarUpperFractal;
        private double _tVarLowerFractal;

        #endregion

        #endregion

        #region StartupInitialize

        protected void ResetValues()
        {
            _percForLongExit = 0;
            _percForShortExit = 0;
            _totalPositionQuantity = 0;
            //_unrealizedPnl = 0;
            _managedPositionQuantity = 0;
            _unmanagedPositionQuantity = 0;
            _managedOrderList.Clear();
            _unmanagedOrderList.Clear();
        }

        protected override void Initialize()
        {
            Helper.BackTest = BackTest;
            //ClearOutputWindow();
            //PrintWithTimeStamp("test0");
            //Print("test1");
            CalculateOnBarClose = true;
            BarsRequired = 70;
            ExitOnClose = false;
            Unmanaged = true;
            RealtimeErrorHandling = RealtimeErrorHandling.TakeNoAction;
            SyncAccountPosition = false;
            _totalPositionQuantity = 0;
            _unrealizedPnl = 0;
            _execInProgress = false;
            IgnoreOverFill = true;
            AddRenko(Instrument.FullName, RenkoHeight, MarketDataType.Last);
            Add(PeriodType.Tick, 15);
            Add(FractalLevel(1));
            Add(SlingShot(Color.Red, Color.Green, 38, PriceType.Close, MovingAverageType.HMA, 80, 63, PriceType.Close,
                MovingAverageType.HMA));
        }

        protected override void OnStartUp()
        {
            if (_instCounter++ == 0)
            {
                Helper.st = this;
            }
            Helper.LogSetup(Instrument.FullName, Account.Name);
        }

        //protected override void OnPositionUpdate(IPosition position)
        //{
        //    Helper.logger.Debug("myOnPositionUpdate");
        //    Helper.logger.Debug("myPosition Qty is: " + NtGetPositionQty(Account, Instrument));
        //    Helper.logger.Debug("myPosition AvgPrice is: " + position.AvgPrice);
        //    Helper.logger.Debug("myPosition MarketPosition is: " + NtGetPositionDirection(Account, Instrument));
        //    position = null;
        //}

        protected override void OnConnectionStatus(ConnectionStatus orderStatus, ConnectionStatus priceStatus)
        {
            if (BackTest)
                return;
            Helper.Log("Connection status of orderStatus is:" + orderStatus, NLog.LogLevel.Debug);
            Helper.Log("Connection status of priceStatus is:" + priceStatus, NLog.LogLevel.Debug);
            _orderConnectionStatus = orderStatus;
            _priceConnectionStatus = priceStatus;
            if (_orderConnectionStatus != ConnectionStatus.Connected ||
                _priceConnectionStatus != ConnectionStatus.Connected)
            {
                return;
            }
            NtCancelAllLimitOrders(Account, Instrument);
            ResetValues();
            if (ETradeCtrMaxDailyLoss())
            {
                Disable();
                return;
            }
            NtGetUnrealizedQuantity(Account, Instrument, ref _totalPositionQuantity, ref _marketPosition);
            Helper.logger.Trace("Quantity is: {0}. Position is: {1}", _totalPositionQuantity, _marketPosition);
            _firstTime = true;
            //NtPopulateManualOrders(Account, Instrument, ref _unmanagedOrderList);
        }

        protected override void OnTermination()
        {
            if (--_instCounter == 0)
            {
                Helper.st = null;
            }
        }

        #endregion

        #region ETradeControls

        private bool ETradeCtrMaxDailyLoss()
        {
            if (BackTest)
                return false;
            double totalNetPnl = NtGetTotalNetNotional(Account, Instrument);
            if ((totalNetPnl < 0) && Math.Abs(totalNetPnl) > _maxDailyLoss)
            {
                _execInProgress = true;
                NtClosePosition(Account, Instrument, ref _totalPositionQuantity);
                Helper.logger.Error("Disabling Strategy because Max Loss reached. Loss is: " + totalNetPnl);
                return true;
            }
            return false;
        }

        #endregion

        private int PercentForLongExit()
        {
            bool exitWaveA = (NtGetWaveALong(0) < 0) && (NtGetWaveAShort(0) < 0);
            bool exitWaveB = (NtGetWaveBLong(0) < 0) && (NtGetWaveBShort(0) < 0);
            if (exitWaveA && exitWaveB)
                return 100;
            return 0;
        }

        private int PercentForShortExit()
        {
            bool exitWaveA = (NtGetWaveALong(0) > 0) && (NtGetWaveAShort(0) > 0);
            bool exitWaveB = (NtGetWaveBLong(0) > 0) && (NtGetWaveBShort(0) > 0);
            if (exitWaveA && exitWaveB)
                return 100;
            return 0;
        }

        private int PercentForLongEntry()
        {
            return 0;
        }

        private int PercentForShortEntry()
        {
            return 0;
        }

        private int QtyForLongEntry()
        {
            return 0;
        }

        private int QtyForShortEntry()
        {
            return 0;
        }

        private bool ShouldEnterLong()
        {
            return false;
        }

        private bool ShouldEnterShort()
        {
            return false;
        }

        protected override void OnExecution(IExecution execution)
        {
            _execInProgress = false;
            NtGetUnrealizedQuantity(Account, Instrument, ref _totalPositionQuantity, ref _marketPosition);
            //if (!BackTest)
            //{
            //    NtGetUnrealizedQuantity(Account, Instrument, ref _totalPositionQuantity, ref _marketPosition);
            //}
            
        }
        protected override void OnOrderUpdate(IOrder iOrder)
        {
            if (iOrder.OrderState == OrderState.Rejected || iOrder.OrderState == OrderState.Cancelled)
            {
                _execInProgress = false;
                NtGetUnrealizedQuantity(Account, Instrument, ref _totalPositionQuantity, ref _marketPosition);
            }
        }

        protected override void OnBarUpdate()
        {
            if (!BackTest && Historical)
                return;

            //At leat certain amount of bars in both timeframes
            if (CurrentBars[0] < BarsRequired || CurrentBars[1] < BarsRequired)
                return;

            // If flat and outside of time range - return
            if (_marketPosition == MarketPosition.Flat && (ToTime(Time[0]) <= LowTimeRange && ToTime(Time[0]) >= UpperTimeRange))
                return;
            _curAsk = GetCurrentAsk();
            _curBid = GetCurrentBid();

            if (_firstTime)
            {
                Calculate600TickValues();
                CalculateRenkoValues();
                //UpdateDesiredTrend();
                _firstTime = false;
                if (BarsInProgress != 2)
                    return;
            }

            switch (BarsInProgress)
            {
                case 0:
                    Calculate600TickValues();
                    return;
                    break;
                case 1:
                    //if (ETradeCtrMaxDailyLoss())
                    //{
                    //    Disable();
                    //    return;
                    //}
                    //CalculateRenkoValues();
                    //return;
                    break;
                case 2:
                    if (!_series1)
                        return;
                    break;
            }

            if (_execInProgress)
                return;

            if (_marketPosition == MarketPosition.Flat)
            {
                if (EnterNewPosition())
                {
                    return;
                }
                //If Flat - look for beginnig entry
                //If did enter - exit
            
            }
            else
            {

                //Check whether it is time to close position. If it is - close position and exit
                if (StopLossReached())
                {
                    _execInProgress = true;
                    NtClosePosition(Account, Instrument, ref _totalPositionQuantity);
                    Helper.Log("Closing because Stop Loss Reached", NLog.LogLevel.Debug);
                    return;
                }

                if (TargetReached())
                {
                    _execInProgress = true;
                    NtClosePosition(Account, Instrument, ref _totalPositionQuantity);
                    Helper.Log("Closing because Target Reached", NLog.LogLevel.Debug);
                    return;
                }
                //If In Position - look to scale out
                //If did scale out - return

                //If In Position - look to scale in
                //If scaled in - return
            }
            
            








 
            ////If Long
            //if (_totalPositionQuantity > 0)
            //{
            //    if (Close[0] <= _tVar51Sma
            //        //&& _tVar34EAMLow >= _tVar51Sma
            //        //&& (_tVar34EAMLow - _tVar51Sma) < 0.0010
            //        //&& Math.Abs(Close[0] - _tVar34EAMLow) < 0.0007
            //        //&& Close[0] > RVar51SMA
            //        && (Close[0] - _rVarLowerFractal0) <= -0.0001
            //        //&& RVarUpperFractal0 > RVarLowerFractal0
            //        )
            //    {
            //        if (BackTest)
            //            Position.Close();
            //        else
            //            NtClosePosition(Account, Instrument);
            //        _totalPositionQuantity = 0;
            //        return;
            //    }
            //    if ((NtGetUnrealizedPips(Account, Instrument)) >= 30)
            //    {
            //        if (BackTest)
            //            Position.Close();
            //        else
            //            NtClosePosition(Account, Instrument);
            //        _totalPositionQuantity = 0;
            //        return;
            //    }
            //    //percForLongExit = PercentForLongExit();
            //    //if (percForLongExit == 100)
            //    //{
            //    //    if (BackTest)
            //    //        Position.Close();
            //    //    else
            //    //        NtClosePosition(Account, Instrument);
            //    //    _totalPositionQuantity = 0;
            //    //    _positionAction = PositionAction.DoNothing;
            //    //    percForLongExit = 0;
            //    //    return;
            //    //}
            //    //else if (percForLongExit > 0 && percForLongExit < 100)
            //    //{
            //    //    _positionAction = PositionAction.ScaleOutFromLong;
            //    //    return;
            //    //}
            //    //else if (percForLongExit == 0)
            //    //{
            //    //    _positionAction = PositionAction.DoNothing;
            //    //    return;
            //    //}
            //    //double percForLongEntry = PercentForLongEntry();
            //    ////Check for scaling into long position and then return
            //}
            //    //Else If Short
            //    //else if (_totalPositionQuantity < 0)
            //    //{
            //    //    percForShortExit = PercentForShortExit();
            //    //    if (percForShortExit == 100)
            //    //    {
            //    //        if (BackTest)
            //    //            Position.Close();
            //    //        else
            //    //            NtClosePosition(Account, Instrument);
            //    //        _totalPositionQuantity = 0;
            //    //        _positionAction = PositionAction.DoNothing;
            //    //        percForShortExit = 0;
            //    //        return;
            //    //    }
            //    //    else if (percForShortExit > 0 && percForShortExit < 100)
            //    //    {
            //    //        _positionAction = PositionAction.ScaleOutFromShort;
            //    //        return;
            //    //    }
            //    //    else if (percForShortExit == 0)
            //    //    {
            //    //        _positionAction = PositionAction.DoNothing;
            //    //        return;
            //    //    }
            //    //    //Check for scaling into short position and then return
            //    //}
            //    //Else If Flat
            //else
            //{
            //    if (Close[0] >= _tVar51Sma
            //        && _tVar34EmaLow >= _tVar51Sma
            //        && (_tVar34EmaLow - _tVar51Sma) < 0.0010
            //        && Math.Abs(Close[0] - _tVar34EmaLow) < 0.0007
            //        && Close[0] > _rVar51Sma
            //        && (Close[0] - _rVarLowerFractal0) >= 0.0001
            //        && _rVarUpperFractal0 > _rVarLowerFractal0
            //        )
            //    {
            //        _managedOrderList.Add(SubmitOrder(0, OrderAction.Buy, OrderType.Market, MaxTotalQty, 0, 0,
            //            "MyLongOCO", "MyLongSignal"));
            //        _totalPositionQuantity = MaxTotalQty;
            //    }
            //}
            //////Check param whether to trade long or short  or both
            ////if (LongEntry)
            ////{
            ////    if (ShouldEnterLong())
            ////    {
            ////        //EnterLong
            ////        _totalPositionQuantity = NtGetPositionQty(Account, Instrument);
            ////        if (_totalPositionQuantity != 0)
            ////        {
            ////            _marketPosition = MarketPosition.Long;
            ////        }
            ////    }
            ////    return;
            ////}
            ////if (ShortEntry)
            ////{
            ////    if (ShouldEnterShort())
            ////    {
            ////        //EnterShort
            ////        _totalPositionQuantity = NtGetPositionQty(Account, Instrument);
            ////        if (_totalPositionQuantity != 0)
            ////        {
            ////            _marketPosition = MarketPosition.Short;
            ////        }
            ////    }
            ////    return;
            ////}


            ////if long or both - check long conditions
            ////if long conditions match - enter position and exit function
            ////if short position of both - check short conditions
            ////if short conditions match - enter position and exit function


            ////Check whether to close position
            ////Check to add to position
            ////Check to exit position
        }

        private bool EnterNewPosition()
        {
            if (_totalPositionQuantity < MaxTotalQty)
            {
                if (_desiredEntryDirection == PositionAction.ScaleInToLong)
                {
                    if (NtBetween(_curAsk, _lowerEntryRange, _upperEntryRange))
                    {
                        _execInProgress = true;
                        _managedOrderList.Add(SubmitOrder(0, OrderAction.Buy, OrderType.Market, MaxTotalQty - _totalPositionQuantity, 0, 0,
                            "", ""));
                        Helper.Log("Entered new position ScaleInToLong with Qty " + (MaxTotalQty - _totalPositionQuantity), NLog.LogLevel.Debug);
                        Helper.Log("Cur Ask is " + _curAsk, NLog.LogLevel.Debug);
                        //_totalPositionQuantity += MaxTotalQty;
                        return true;
                    }
                }
                else if (_desiredEntryDirection == PositionAction.ScaleInToShort)
                {
                    if (NtBetween(_curBid, _lowerEntryRange, _upperEntryRange))
                    {
                        _execInProgress = true;
                        _managedOrderList.Add(SubmitOrder(0, OrderAction.Sell, OrderType.Market, MaxTotalQty - _totalPositionQuantity, 0, 0,
                            "", ""));
                        Helper.Log("Entered new position ScaleInToShort with Qty " + (MaxTotalQty - _totalPositionQuantity), NLog.LogLevel.Debug);
                        Helper.Log("Cur Bid is " + _curBid, NLog.LogLevel.Debug);
                        //_totalPositionQuantity += MaxTotalQty;

                        return true;
                    }
                }
            }
            return false;  
        }

        private bool TargetReached()
        {
            if (_marketPosition != MarketPosition.Flat)
            {
                if (NtGetUnrealizedPips(Account, Instrument) > 50)
                {
                    Helper.Log("Target Reached:" + NtGetUnrealizedPips(Account, Instrument) + "pips", NLog.LogLevel.Debug);
                    Helper.Log("Cur Ask is " + _curAsk, NLog.LogLevel.Debug);
                    return true;
                }
            }
            return false;
        }

        private bool StopLossReached()
        {
            
            if (_marketPosition == MarketPosition.Long)
            {
                if (_strategyName == "slingshot")
                {
                    if (Closes[0][0] < _tVarLowerFractal)
                    {
                        Helper.Log("Cur Ask is " + _curAsk, NLog.LogLevel.Debug);
                        Helper.Log("StopLossReached with Qty " + (_totalPositionQuantity), NLog.LogLevel.Debug);
                        return true;
                    }
                }
                else if (_strategyName == "reg")
                {
                    if (_curAsk < _tVar51Sma)
                    {
                        if ((_tVar51Sma - _curAsk) > _stopLossTicksFromMA * 10 * TickSize)
                        {
                            Helper.Log("Cur Ask is " + _curAsk, NLog.LogLevel.Debug);
                            Helper.Log("StopLossReached with Qty " + (_totalPositionQuantity), NLog.LogLevel.Debug);
                            return true;
                        }
                    }
                }
            }
            else if (_marketPosition == MarketPosition.Short)
            {
                if (_strategyName == "slingshot")
                {
                    if (Closes[0][0] > _tVarUpperFractal)
                    {
                        Helper.Log("Cur Bid is " + _curBid, NLog.LogLevel.Debug);
                        Helper.Log("StopLossReached with Qty " + (_totalPositionQuantity), NLog.LogLevel.Debug);
                        return true;
                    }
                }
                else if (_strategyName == "reg")
                {
                    if (_curBid > _tVar51Sma)
                    {
                        if ((_curBid - _tVar51Sma) > _stopLossTicksFromMA * 10 * TickSize)
                        {
                            Helper.Log("Cur Bid is " + _curBid, NLog.LogLevel.Debug);
                            Helper.Log("StopLossReached with Qty " + (_totalPositionQuantity), NLog.LogLevel.Debug);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void CalculateRenkoValues()
        {
            _rVar51Sma = SMA(51)[0];
            _rVar20Ema = EMA(20);
            _rVarUpperFractal0 = FractalLevel(1).UpFractals[0];
            _rVarLowerFractal0 = FractalLevel(1).DownFractals[0];
            _rVarWaveBLong = NtGetWaveBLong(0);
            _rVarWaveBShort = NtGetWaveBShort(0);
            _rVarWaveALong = NtGetWaveALong(0);
            _rVarWaveAShort = NtGetWaveAShort(0);
            var r = HeikenAshi().HAClose[0];

            //Print("Plot 0 is: " + FractalLevel(1).Plot0[0]);
            //Print("Plot 1 is: " + FractalLevel(1).Plot1[0]);
            if (!_series2)
                _series2 = true;
        }

        private void Calculate600TickValues()
        {
            _tVar51Sma = SMA(BarsArray[0], 51)[0];
            _tVar34EmaHigh = EMA(Highs[0], 34)[0];
            _tVar34EmaLow = EMA(Lows[0], 34)[0];
            _tVarWaveBLong = NtGetWaveBLong(BarsArray[0], 0);
            _tVarWaveBShort = NtGetWaveBShort(BarsArray[0], 0);
            _tVarWaveALong = NtGetWaveALong(BarsArray[0], 0);
            _tVarWaveAShort = NtGetWaveAShort(BarsArray[0], 0);
            _tVarSlingShotSlow = NtGetSlingShotSlow(BarsArray[0], 0);
            _tVarSlingShotFast = NtGetSlingShotFast(BarsArray[0], 0);
            _tVarLowerFractal = NtGetFractalsLow(BarsArray[0], 0);
            _tVarUpperFractal = NtGetFractalsHigh(BarsArray[0], 0);

            if (!_series1)
            {
                if ((_tVar51Sma != 0)
                    && (_tVar34EmaHigh != 0)
                    && (_tVar34EmaLow != 0)
                    && (_tVarWaveBLong != 0)
                    && (_tVarWaveBShort != 0)
                    && (_tVarWaveALong != 0)
                    && (_tVarWaveAShort != 0)
                    && (_tVarSlingShotSlow != 0)
                    && (_tVarSlingShotFast != 0)
                    && (_tVarLowerFractal != 0)
                    && (_tVarUpperFractal != 0))
                {
                    _series1 = true;
                }
                else
                {
                    return;
                }
            }
               
            UpdateDesiredTrend();
            Helper.Log(String.Format("_tVar51Sma = {0}," +
                                     "_tVar34EmaHigh = {1}," +
                                     "_tVar34EmaLow = {2}," +
                                     "_tVarWaveBLong = {3}," +
                                     "_tVarWaveBShort = {4}," +
                                     "_tVarWaveALong = {5}," +
                                     "_tVarWaveAShort = {6}," +
                                     "_tVarSlingShotSlow = {7}," +
                                     "_tVarSlingShotFast = {8}," +
                                     "_desiredEntryDirection = {9}" +
                                     "_tVarLowerFractal = {10}," +
                                     "_tVarUpperFractal = {11}", _tVar51Sma, _tVar34EmaHigh,
                                     _tVar34EmaLow, _tVarWaveBLong, _tVarWaveBShort,
                                     _tVarWaveALong, _tVarWaveAShort,
                                     _tVarSlingShotSlow, _tVarSlingShotFast, _desiredEntryDirection,
                                     _tVarLowerFractal, _tVarUpperFractal), NLog.LogLevel.Trace);
        }

        private void UpdateDesiredTrend()
        {
            if (_series1)//((_series1) && (_series2))
            {
                switch (_strategyName)
                {
                    case "slingshot":
                        if ((_tVarSlingShotFast > _tVarSlingShotSlow)
                            && (Closes[0][0] < _tVarSlingShotFast)
                            && (_tVarLowerFractal < _tVarUpperFractal)
                            && (Closes[0][0] > _tVarLowerFractal))
                        {
                            _upperEntryRange = (_tVarLowerFractal + _tVarUpperFractal)/2;
                            _lowerEntryRange = _tVarLowerFractal;
                            _desiredEntryDirection = PositionAction.ScaleInToLong;
                        }
                        else if ((_tVarSlingShotFast < _tVarSlingShotSlow)
                            && (Closes[0][0] > _tVarSlingShotFast)
                            && (_tVarLowerFractal < _tVarUpperFractal)
                            && (Closes[0][0] < _tVarUpperFractal))
                        {
                            _upperEntryRange = _tVarUpperFractal;
                            _lowerEntryRange = (_tVarLowerFractal + _tVarUpperFractal) / 2;
                            _desiredEntryDirection = PositionAction.ScaleInToShort;
                        }
                        else
                        {
                            _desiredEntryDirection = PositionAction.DoNothing;
                        }
                        break;
                    default:
                        if (_tVar34EmaHigh > _tVar51Sma
                            //&& _curBid > _tVar51Sma
                            //&& _curBid < _tVar34EmaHigh
                            && _tVarWaveBLong > 0
                            && _tVarWaveBShort > 0
                            )
                        {
                            _upperEntryRange = _tVar34EmaHigh;
                            _lowerEntryRange = _tVar51Sma;
                            _desiredEntryDirection = PositionAction.ScaleInToLong;
                        }
                        else if (_tVar34EmaLow < _tVar51Sma
                            //&& _curAsk < _tVar51Sma
                            //&& _curAsk > _tVar34EmaLow
                            && _tVarWaveBLong < 0
                            && _tVarWaveBShort < 0
                            )
                        {
                            _upperEntryRange = _tVar51Sma;
                            _lowerEntryRange = _tVar34EmaLow;
                            _desiredEntryDirection = PositionAction.ScaleInToShort;
                        }
                        else
                        {
                            _desiredEntryDirection = PositionAction.DoNothing;
                        }
                        break;
                }
                
            }
        }

        #region Properties

        [Description("")]
        [GridCategory("Parameters")]
        public bool BackTest
        {
            get { return _backtest; }
            set { _backtest = value; }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public string StrategyName
        {
            get { return _strategyName; }
            set { _strategyName = value; }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int LowTimeRange
        {
            get { return _lowTimeRange; }
            set { _lowTimeRange = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int UpperTimeRange
        {
            get { return _upperTimeRange; }
            set { _upperTimeRange = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int RenkoHeight
        {
            get { return _renkoHeight; }
            set { _renkoHeight = Math.Max(1, value); }
        }

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