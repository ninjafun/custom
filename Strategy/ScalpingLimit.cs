using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

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
//using OfficeOpenXml;

#endregion

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    /// <summary>
    /// single timeframe with limit orders
    /// </summary>
    [Description("Trading on news events")]
    public class ScalpingLimit : Strategy
    {
        #region Variables
        private bool _backtest = true;  // Set true for backtesting
        private static double _prevStop, _curStop, _curTarget;
        private double _lFractal, _hFractal;
        MarketPosition _marketPosition = MarketPosition.Flat;
        private int _positionQuantity = 0;
        private double _unrealizedPNL = 0;
      
        int target1 = 30;
        int stop1 = 10;
        private double myTickSize = 0;
        int NumOfContracts = 100000;
        double lowest0;
        double lowest1;
        double highest0;
        double highest1;
        int stopNumOfBars = 20;
        List<IOrder> _managedOrderList = new List<IOrder>();
        List<Order> _unmanagedOrderList = new List<Order>();
        private ConnectionStatus _orderConnectionStatus = ConnectionStatus.Disconnected;
        private ConnectionStatus _priceConnectionStatus = ConnectionStatus.Disconnected;
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {


            CalculateOnBarClose = true;
            BarsRequired = 70;
            ExitOnClose = true;
            EntriesPerDirection = 10000;
            Print("test");
            //Unmanaged = true;
            
            RealtimeErrorHandling = NinjaTrader.Strategy.RealtimeErrorHandling.TakeNoAction;

            //Add(bwAO());
            //Add(mahTrendGRaBerV1(34, 34, 34, 2));
            //Add(SMA(51));
            //Add(FractalLevel(1));
            //TraceOrders = true;
            //ExcelPackage pck;
            //pck = new ExcelPackage();

        }

        protected override void OnStartUp()
        {
            NtGetPositionTest(Account, Instrument);
            Log("test2", LogLevel.Error);
            //FileInfo newFile = new FileInfo(@"F:\Users\Vadim\Documents\Unified Functional Testing\ForexFactory1\Output\ForexFactoryInCentralTime.xlsx");
            //using (ExcelPackage pck = new ExcelPackage(newFile))
            //{
            //    ExcelWorksheet ws = pck.Workbook.Worksheets["Events"];

            //    //                ws.Cells["A1"].LoadFromDataTable(dataTable, true);
                
            //}
        }

        protected override void OnConnectionStatus(ConnectionStatus orderStatus, ConnectionStatus priceStatus)
        {
            _orderConnectionStatus = orderStatus;
            _priceConnectionStatus = priceStatus;
            if (_orderConnectionStatus != ConnectionStatus.Connected || _priceConnectionStatus != ConnectionStatus.Connected)
            {
                return;
            }
            _unmanagedOrderList.Clear();
            NtPopulateManualOrders(Account, Instrument, ref _unmanagedOrderList);
            _marketPosition = NtGetPositionDirection(Account, Instrument);
            if (_marketPosition != MarketPosition.Flat)
            {
                _positionQuantity = NtGetUnrealizedQuantity(Account, Instrument);
                _unrealizedPNL = NtGetUnrealizedNotional(Account, Instrument);
            }
            else
            {
                _positionQuantity = 0;
            }
        }

        protected override void OnPositionUpdate(IPosition position)
        {
           // MessageBox.Show("Hello4");
            Print("Position is " + position.MarketPosition);
        }


        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            if (Historical)
                return;
            NtGetPositionTest(Account, Instrument);
            return;
          //  MessageBox.Show("Hello3");
            Cbi.Position myPosition = Account.Positions.FindByInstrument(Instrument);
            int iOrderCount = Account.Orders.Count; Print("Total Open Orders: " + iOrderCount); 
            System.Collections.IEnumerator ListOrders = Account.Orders.GetEnumerator();
            for (int i = 0; i < iOrderCount; i++)
            {
                ListOrders.MoveNext(); 
                Print(" Open Orders: " + ListOrders.Current);
                Order myOrder = ListOrders.Current as NinjaTrader.Cbi.Order;
                if (myOrder.OrderState == OrderState.Working)
                    myOrder.Cancel();
            }


            Log("test3", LogLevel.Error);
            if (_backtest == false)
            {
                if (Historical)
                    return;
            }
            Print(Position.MarketPosition.ToString() + " " + Position.Quantity.ToString());
            double pl = Position.GetProfitLoss(Close[0], PerformanceUnit.Currency);

 


            if (Position.MarketPosition == MarketPosition.Flat)
            {
                if (ToTime(Time[0]) >= 20000 && ToTime(Time[0]) <= 80000)
                {

                }
                else
                    return;
            }

            EntryHandling = EntryHandling.UniqueEntries;
            myTickSize = TickSize * 10;

            #region Flat
            if (Position.MarketPosition == MarketPosition.Flat)
            {
                //look to enter long or short position

                if (bwAO().AOValue[0] > 0)
                {
                    lowest0 = Open[0] > Close[0] ? Close[0] : Open[0];
                    lowest1 = Open[1] > Close[1] ? Close[1] : Open[1];
                    if ((lowest0 > EMA(High, 34)[0])
                        && (lowest0 > SMA(51)[0])
                        && (lowest1 > EMA(High, 34)[1])
                        && (lowest1 > SMA(51)[1]))
                    {
                        if (NtRagheeDifferentColor(8))
                        {
                            _lFractal = NtGetLowestFractal(stopNumOfBars, 4);
                            _prevStop = _curStop = _lFractal;
                            //SetStopLoss("target1", CalculationMode.Price, curStop, false);

                            _curTarget = Close[0] + (target1 * myTickSize);
                            //prevStop = curStop = Close[0] - (stop1 * myTickSize);
                            SetProfitTarget("target1", CalculationMode.Price, _curTarget);
                            SetStopLoss("target1", CalculationMode.Price, _curStop, false);
                            EnterLong(NumOfContracts, "target1");
                            PrintWithTimeStamp("Long; stop = " + _curStop.ToString() + "; target = " + _curTarget.ToString());
                        }
                    }

                }
                else if (bwAO().AOValue[0] < 0)
                {
                    highest0 = Open[0] > Close[0] ? Open[0] : Close[0];
                    highest1 = Open[1] > Close[1] ? Open[1] : Close[1];
                    if ((highest0 < EMA(Low, 34)[0])
                        && (highest0 < SMA(51)[0])
                        && (highest1 < EMA(Low, 34)[1])
                        && (highest1 < SMA(51)[1]))
                    {
                        if (NtRagheeDifferentColor(8))
                        {
                            _hFractal = NtGetHighestFractal(stopNumOfBars, 4);
                            _prevStop = _curStop = _hFractal;
                            //SetStopLoss("target1", CalculationMode.Price, curStop, false);

                            _curTarget = Close[0] - (target1 * myTickSize);
                            //prevStop = curStop = Close[0] + (stop1 * myTickSize);
                            SetProfitTarget("target1", CalculationMode.Price, _curTarget);
                            SetStopLoss("target1", CalculationMode.Price, _curStop, false);
                            EnterShort(NumOfContracts, "target1");
                            PrintWithTimeStamp("Short; stop = " + _curStop.ToString() + "; target = " + _curTarget.ToString());
                        }
                    }
                }

            }
            #endregion
            else if (Position.MarketPosition == MarketPosition.Long)
            {
                _lFractal = NtGetLowestFractal(stopNumOfBars, 4);
                if (_lFractal > _curStop)
                {
                    _curStop = _lFractal;
                    SetStopLoss("target1", CalculationMode.Price, _curStop, false);
                }
                //if (Close[0] < curStop)
                //{
                //    ExitLong("target1");
                //}
            }
            else if (Position.MarketPosition == MarketPosition.Short)
            {
                _hFractal = NtGetHighestFractal(stopNumOfBars, 4);
                if (_hFractal < _curStop)
                {
                    _curStop = _hFractal;
                    SetStopLoss("target1", CalculationMode.Price, _curStop, false);
                }
                //if (Close[0] > curStop)
                //{
                //    ExitShort("target1");
                //}
            }
        }



        private double NtGetLowestFractal(int numOfBars, int numOfFractal)
        {
            return NtGetLowest(numOfBars);

            int fractalChanges = 0;
            double curFractalPrice = 0;
            double lowestFractalPrice = FractalLevel(1).DownFractals[3];
            for (int i = 4; i < numOfBars; i++)
            {
                curFractalPrice = FractalLevel(1).DownFractals[i];
                if (FractalLevel(1).DownFractals[i] != lowestFractalPrice)
                {
                    fractalChanges++;
                    if (FractalLevel(1).DownFractals[i] < lowestFractalPrice)
                    {
                        lowestFractalPrice = FractalLevel(1).DownFractals[i];
                        if (fractalChanges == numOfFractal)
                        {
                            return lowestFractalPrice;
                        }
                    }
                }
            }
            return lowestFractalPrice;
        }
        private double NtGetHighestFractal(int numOfBars, int numOfFractal)
        {
            return NtGetHighest(numOfBars);
            int fractalChanges = 0;
            double curFractalPrice = 0;
            double HighestFractalPrice = FractalLevel(1).UpFractals[3];
            for (int i = 4; i < numOfBars; i++)
            {
                curFractalPrice = FractalLevel(1).UpFractals[i];
                if (FractalLevel(1).UpFractals[i] != HighestFractalPrice)
                {
                    fractalChanges++;
                    if (FractalLevel(1).UpFractals[i] > HighestFractalPrice)
                    {
                        HighestFractalPrice = FractalLevel(1).UpFractals[i];
                        if (fractalChanges == numOfFractal)
                        {
                            return HighestFractalPrice;
                        }
                    }
                }
            }
            return HighestFractalPrice;
        }
        //private double GetHighFractal()
        //{
        //    double upFractal = -1;

        //    if (High[2] < High[3] && High[1] < High[3])
        //    {
        //        // Fractal type 1
        //        if (High[5] < High[3] && High[4] < High[3])
        //            upFractal = High[3];

        //        // Fractal type 2
        //        else if (High[6] < High[3] && High[5] < High[3] && High[4] == High[3])
        //            upFractal = High[3];

        //        // Fractal type 3, 4
        //        else if (High[7] < High[3] && High[6] < High[3] && High[5] == High[3] && High[4] <= High[3])
        //            upFractal = High[3];

        //        // Fractal type 5
        //        else if (High[8] < High[3] && High[7] < High[3] && High[6] == High[3] && High[5] < High[3] && High[4] == High[3])
        //            upFractal = High[3];

        //        // Fractal type 6
        //        else if (High[8] < High[3] && High[7] < High[3] && High[6] == High[3] && High[5] == High[3] && High[4] < High[3])
        //            upFractal = High[3];

        //        // Fractal type 7
        //        else if (High[9] < High[3] && High[8] < High[3] && High[7] == High[3] && High[6] < High[3] && High[5] == High[3] && High[4] < High[3])
        //            upFractal = High[3];
        //    }
        //    return upFractal;
        //}
        //private double GetLowFractal()
        //{
        //    double downFractal = -1;
        //    if (Low[2] > Low[3] && Low[1] > Low[3])
        //    {
        //        // Fractal type 1
        //        if (Low[5] > Low[3] && Low[4] > Low[3])
        //            downFractal = Low[3];

        //        // Fractal type 2
        //        else if (Low[6] > Low[3] && Low[5] > Low[3] && Low[4] == Low[3])
        //            downFractal = Low[3];

        //        // Fractal type 3, 4
        //        else if (Low[7] > Low[3] && Low[6] > Low[3] && Low[5] == Low[3] && Low[4] >= Low[3])
        //            downFractal = Low[3];

        //        // Fractal type 5
        //        else if (Low[8] > Low[3] && Low[7] > Low[3] && Low[6] == Low[3] && Low[5] > Low[3] && Low[4] == Low[3])
        //            downFractal = Low[3];

        //        // Fractal type 6
        //        else if (Low[8] > Low[3] && Low[7] > Low[3] && Low[6] == Low[3] && Low[5] == Low[3] && Low[4] > Low[3])
        //            downFractal = Low[3];

        //        // Fractal type 7
        //        else if (Low[9] > Low[3] && Low[8] > Low[3] && Low[7] == Low[3] && Low[6] > Low[3] && Low[5] == Low[3] && Low[4] > Low[3])
        //            downFractal = Low[3];
        //    }
        //    return downFractal;
        //}


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
        public int Target1
        {
            get { return target1; }
            set { target1 = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int Stop1
        {
            get { return stop1; }
            set { stop1 = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int StopNumOfBars
        {
            get { return stopNumOfBars; }
            set { stopNumOfBars = Math.Max(1, value); }
        }

        #endregion
    }
}
