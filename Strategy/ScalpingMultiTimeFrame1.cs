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
    /// 75 Tick Eur/Usd
    /// </summary>
    [Description("75 Tick Eur/Usd")]
    public class ScalpingMultiTimeFrame1 : Strategy
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
        private static int _positionQuantity;
        private double _unrealizedPnl;
        private DataSeries _MyHeikenAshiSeries;
        private static PositionAction _positionAction = PositionAction.DoNothing;
        private static int percForLongExit;
        private static int percForShortExit;

        // User defined variables (add any user defined variables below)
        #endregion

        protected void ResetValues()
        {
            percForLongExit = 0;
            percForShortExit = 0;
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
            _positionQuantity = 0;
            _unrealizedPnl = 0;
            IgnoreOverFill = true;
        }


        private IOrder SendLimitOrderLong(int qty)
        {
            return SubmitOrder(0, OrderAction.Buy, OrderType.Limit, qty, 1.23, 0, "111", "222");
        }
        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            if (BarsInProgress != 0)
                return;

            IOrder x = SendLimitOrderLong(2);
            ChangeOrder(x, 2, 0, 0);
        }

        protected override void OnExecution(IExecution execution)
        {
            base.OnExecution(execution);
        }

        protected override void OnOrderUpdate(IOrder order)
        {
            base.OnOrderUpdate(order);
        }
        protected override void OnPositionUpdate(IPosition position)
        {
            base.OnPositionUpdate(position);
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
