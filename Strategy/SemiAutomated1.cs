#region Using declarations
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
//using log4net;
//using log4net.Repository.Hierarchy;
//using log4net.Core;
//using log4net.Appender;
//using log4net.Layout;
using NinjaTrader.Cbi;
using NinjaTrader.Indicator;
using NinjaTrader.Strategy;
//using NLog;
//using NLog.Config;
//using NLog.Targets;
using NLog;
using LogLevel = NinjaTrader.Cbi.LogLevel;




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
        #region RiskOrderManagement
        private static int _lowTimeRange = 210000;
        private static int _upperTimeRange = 110000;
        private static int _target1 = 1; // Default setting for Target1
        private static int _exitPercent1 = 100; // Default setting for ExitPercent1
        private static int _maxTotalQty = 100000; // Default setting for MaxTotalQty
        private static bool _longEntry = true; // Default setting for LongEntry
        private static bool _shortEntry = true; // Default setting for ShortEntry
        private static double _maxTradeLoss = 500.000; // Default setting for MaxTradeLoss
        private static double _maxDailyLoss = 1; // Default setting for MaxDailyLoss
        private static double _maxTradeWin = 1000; // Default setting for MaxTradeWin
        private double _unrealizedPnl;
        private double _totalNetPnl;
        private static int _percForLongExit;
        private static int _percForShortExit;
        private static MarketPosition _marketPosition;
        private static int _totalPositionQuantity;
        private static ConnectionStatus _orderConnectionStatus;
        private static ConnectionStatus _priceConnectionStatus;
        private static int _managedPositionQuantity;
        private static int _unmanagedPositionQuantity;
        private static PositionAction _positionAction = PositionAction.DoNothing;
        private List<IOrder> _managedOrderList = new List<IOrder>();
        private List<Order> _unmanagedOrderList = new List<Order>();
        private static bool _backtest = false;
        private static bool _series1 = false;
        private static bool _series2 = false;
        #endregion
        #region Renko
        private static int _renkoHeight = 50;
        private DataSeries _MyHeikenAshiSeries;
        private static double _rVar51Sma = 0;
        #endregion       
        #region Fractals
        private static double _rVarUpperFractal0 = 0;
        private static double _rVarLowerFractal0 = 0;
        #endregion
        #region Raghee
        private double RVarWaveBLong;
        private double RVarWaveBShort;
        private double RVarWaveAShort;
        private double RVarWaveALong;
        private EMA RVar20EMA;
        private double TVar51SMA;
        #endregion
        #region TTMWave
        private double TVar34EAMHigh;
        private double TVar34EAMLow;
        private double TVarWaveBLong;
        private double TVarWaveBShort;
        private double TVarWaveALong;
        private double TVarWaveAShort;
        #endregion
        #region NLog

        //private static readonly ILog TestLog = LogManager.GetLogger(typeof(SemiAutomated1));
        //private PatternLayout _layout = new PatternLayout();
        //private const string LOG_PATTERN = "%d [%t] %-5p %m%n";
        //public string DefaultPattern
        //{
        //    get { return LOG_PATTERN; }
        //}

        //public PatternLayout DefaultLayout
        //{
        //    get { return _layout; }
        //}
        #endregion
        //private DataSeries myDataSeries;
        #endregion

        #region StartupInitialize
        protected void ResetValues()
        {
            _percForLongExit = 0;
            _percForShortExit = 0;
            _totalPositionQuantity = 0;
            _unrealizedPnl = 0;
            _managedPositionQuantity = 0;
            _unmanagedPositionQuantity = 0;
            _unmanagedOrderList.Clear();
        }

        protected override void Initialize()
        {
            ClearOutputWindow();
            PrintWithTimeStamp("test0");
            Print("test1");
            CalculateOnBarClose = true;
            BarsRequired = 70;
            ExitOnClose = false;
            Unmanaged = true;
            RealtimeErrorHandling = NinjaTrader.Strategy.RealtimeErrorHandling.TakeNoAction;
            SyncAccountPosition = false;
            _totalPositionQuantity = 0;
            _unrealizedPnl = 0;
            IgnoreOverFill = true;
            AddRenko(Instrument.FullName, RenkoHeight, MarketDataType.Last);
            Add(PeriodType.Tick, 15);
            Helper.BackTest = BackTest;
        }

        protected override void OnStartUp()
        {
            Helper.LogSetup(Instrument.FullName);
        }
        //protected override void OnStart()
        //{

        //    //if (myDataSeries == null)
        //    //{
        //    //    myDataSeries = new DataSeries(HeikenAshi(BarsArray[1]));
        //    //}
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
            if (BackTest)
                return;
		
            _orderConnectionStatus = orderStatus;
            _priceConnectionStatus = priceStatus;
            if (_orderConnectionStatus != ConnectionStatus.Connected ||
                _priceConnectionStatus != ConnectionStatus.Connected)
            {
                return;
            }
            ResetValues();
            _managedOrderList.Clear();
            _unmanagedOrderList.Clear();
            if (ETradeCtrMaxDailyLoss())
                return;
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
        #endregion


        #region ETradeControls
        private bool ETradeCtrMaxDailyLoss()
        {
            _totalNetPnl = NtGetTotalNetNotional(Account, Instrument);


            if ((_totalNetPnl < 0) && Math.Abs(_totalNetPnl) > _maxDailyLoss)
            {
                NtClosePosition(Account, Instrument);
                Helper.logger.Error("Disabling Strategy because Max Loss reached. Loss is: " + _totalNetPnl);
                Disable();
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
            if (!BackTest && ETradeCtrMaxDailyLoss())
                return;
        }

        protected override void OnBarUpdate()
        {
            if (!BackTest && Historical)
                return;

            //At leat certain amount of bars in both timeframes
            if (CurrentBars[0] < BarsRequired || CurrentBars[1] < BarsRequired)
                return;
           // BasicConfigurator.Configure();
            //DOMConfigurator.Configure();
            //var fileAppender = LogManager.GetLoggerRepository()
            //                 .GetAppenders()
            //                 .OfType<FileAppender>()
            //                 .FirstOrDefault(fa => fa.Name == "LogFileAppender");
            //if (fileAppender != null)
            //{
            //    fileAppender.File = Path.Combine(Environment.CurrentDirectory, "foo.txt");
            //    fileAppender.ActivateOptions();
            //}
            //TestLog.Info("Here is a debug log.");

            // If flat and outside of time range - return
            if (_totalPositionQuantity == 0 && (ToTime(Time[0]) <= LowTimeRange && ToTime(Time[0]) >= UpperTimeRange))
                return;


            if (BarsInProgress == 0)
            {
                TVar51SMA = SMA(51)[0];
                TVar34EAMHigh = EMA(High, 34)[0];
                TVar34EAMLow = EMA(Low, 34)[0];
                TVarWaveBLong = NtGetWaveBLong(0);
                TVarWaveBShort = NtGetWaveBShort(0);
                TVarWaveALong = NtGetWaveALong(0);
                TVarWaveAShort = NtGetWaveAShort(0);
                _series1 = true;
            }
            else if (BarsInProgress == 1)
            {
                _rVar51Sma = SMA(51)[0];
                RVar20EMA = EMA(20);
                _rVarUpperFractal0 = FractalLevel(1).UpFractals[0];
                _rVarLowerFractal0 = FractalLevel(1).DownFractals[0];
                RVarWaveBLong = NtGetWaveBLong(0);
                RVarWaveBShort = NtGetWaveBShort(0);
                RVarWaveALong = NtGetWaveALong(0);
                RVarWaveAShort = NtGetWaveAShort(0);
                var r = HeikenAshi().HAClose[0];
                PrintWithTimeStamp("Renko");
                _series2 = true;
                //Print("Plot 0 is: " + FractalLevel(1).Plot0[0]);
                //Print("Plot 1 is: " + FractalLevel(1).Plot1[0]);
            }
            else if (BarsInProgress == 2)
            {
                if (!_series1 || !_series2)
                    return;
                PrintWithTimeStamp("Bar0");
                //If Long
                if (_totalPositionQuantity > 0)
                {
                    if (Close[0] <= TVar51SMA
                        //&& TVar34EAMLow >= TVar51SMA
                        //&& (TVar34EAMLow - TVar51SMA) < 0.0010
                        //&& Math.Abs(Close[0] - TVar34EAMLow) < 0.0007
                        //&& Close[0] > RVar51SMA
                        && (Close[0] - _rVarLowerFractal0) <= -0.0001
                        //&& RVarUpperFractal0 > RVarLowerFractal0
                        )
                    {
                        if (BackTest)
                            Position.Close();
                        else
                            NtClosePosition(Account, Instrument);
                        _totalPositionQuantity = 0;
                        return;
                    }
                    if ((NtGetUnrealizedPips(Account, Instrument)) >= 30)
                    {
                        if (BackTest)
                            Position.Close();
                        else
                            NtClosePosition(Account, Instrument);
                        _totalPositionQuantity = 0;
                        return;
                    }
                    //percForLongExit = PercentForLongExit();
                    //if (percForLongExit == 100)
                    //{
                    //    if (BackTest)
                    //        Position.Close();
                    //    else
                    //        NtClosePosition(Account, Instrument);
                    //    _totalPositionQuantity = 0;
                    //    _positionAction = PositionAction.DoNothing;
                    //    percForLongExit = 0;
                    //    return;
                    //}
                    //else if (percForLongExit > 0 && percForLongExit < 100)
                    //{
                    //    _positionAction = PositionAction.ScaleOutFromLong;
                    //    return;
                    //}
                    //else if (percForLongExit == 0)
                    //{
                    //    _positionAction = PositionAction.DoNothing;
                    //    return;
                    //}
                    //double percForLongEntry = PercentForLongEntry();
                    ////Check for scaling into long position and then return
                }
                    //Else If Short
                    //else if (_totalPositionQuantity < 0)
                    //{
                    //    percForShortExit = PercentForShortExit();
                    //    if (percForShortExit == 100)
                    //    {
                    //        if (BackTest)
                    //            Position.Close();
                    //        else
                    //            NtClosePosition(Account, Instrument);
                    //        _totalPositionQuantity = 0;
                    //        _positionAction = PositionAction.DoNothing;
                    //        percForShortExit = 0;
                    //        return;
                    //    }
                    //    else if (percForShortExit > 0 && percForShortExit < 100)
                    //    {
                    //        _positionAction = PositionAction.ScaleOutFromShort;
                    //        return;
                    //    }
                    //    else if (percForShortExit == 0)
                    //    {
                    //        _positionAction = PositionAction.DoNothing;
                    //        return;
                    //    }
                    //    //Check for scaling into short position and then return
                    //}
                    //Else If Flat
                else
                {
                    if (Close[0] >= TVar51SMA
                        && TVar34EAMLow >= TVar51SMA
                        && (TVar34EAMLow - TVar51SMA) < 0.0010
                        && Math.Abs(Close[0] - TVar34EAMLow) < 0.0007
                        && Close[0] > _rVar51Sma
                        && (Close[0] - _rVarLowerFractal0) >= 0.0001
                        && _rVarUpperFractal0 > _rVarLowerFractal0
                        )
                    {
                        _managedOrderList.Add(SubmitOrder(0, OrderAction.Buy, OrderType.Market, MaxTotalQty, 0, 0,
                            "MyLongOCO", "MyLongSignal"));
                        _totalPositionQuantity = MaxTotalQty;
                    }
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
        public bool BackTest
        {
            get { return _backtest; }
            set { _backtest = value; }
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