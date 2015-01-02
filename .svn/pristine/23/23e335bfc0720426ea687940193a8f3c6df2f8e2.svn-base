// TradingStudies.com
// info@tradingStudies.com
using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;

namespace NinjaTrader.Indicator
{
    [Description("Rolling Intraday Pivots (created by TradingStudies.com)")]
    public class TSIntradayPivots : Indicator
    {
        private bool _existsHistHourlyData;
        private Bars _intradayBars;
        private bool _isInit;
        private bool _isLoaded;
        private int _minutes = 60;

        protected override void Initialize()
        {
            Name = " TS Intraday Pivots";
            Add(new Plot(Color.Orchid, PlotStyle.Hash, "Hpp"));
            Add(new Plot(Color.Green, PlotStyle.Hash, "Hs1"));
            Add(new Plot(Color.Green, PlotStyle.Hash, "Hs2"));
            Add(new Plot(Color.Red, PlotStyle.Hash, "Hr1"));
            Add(new Plot(Color.Red, PlotStyle.Hash, "Hr2"));
            Overlay = true;
            AutoScale = false;
        }

        protected override void OnBarUpdate()
        {
            if (Bars == null)
                return;
            if (!BarsType.GetInstance(Bars.Period.Id).IsIntraday)
                return;
            if (Bars.Period.Id == PeriodType.Minute && Bars.Period.Value > _minutes/2)
                return;

            if (!_isLoaded && !_isInit)
            {
                _isInit = true;
                _intradayBars = Bars.GetBars(Bars.Instrument, new Period(PeriodType.Minute, _minutes, MarketDataType.Last), Bars.From, Bars.To, (Session) Bars.Session.Clone(), Bars.SplitAdjust, Bars.DividendAdjust);
                _existsHistHourlyData = (_intradayBars.Count <= 1) ? false : true;
                _isInit = false;
                _isLoaded = true;
            }

            if (!_existsHistHourlyData)
                return;

            DateTime intradayBarTime = Time[0].AddMinutes(-_minutes);
            IBar intradayBar = _intradayBars.Get(_intradayBars.GetBar(intradayBarTime));
            double high = intradayBar.High;
            double low = intradayBar.Low;
            double close = intradayBar.Close;
            Hpp.Set((high + low + close)/3);
            Hs1.Set(2*Hpp[0] - high);
            Hr1.Set(2*Hpp[0] - low);
            Hs2.Set(Hpp[0] - (high - low));
            Hr2.Set(Hpp[0] + (high - low));
        }

        #region Properties

        [GridCategory("Parameters")]
        public int Minutes
        {
            get { return _minutes; }
            set { _minutes = Math.Max(value, 15); }
        }

        [Browsable(false)] // this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore] // this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
            public DataSeries Hpp
        {
            get { return Values[0]; }
        }

        [Browsable(false)] // this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore] // this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
            public DataSeries Hs1
        {
            get { return Values[1]; }
        }

        [Browsable(false)] // this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore] // this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
            public DataSeries Hs2
        {
            get { return Values[2]; }
        }

        [Browsable(false)] // this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore] // this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
            public DataSeries Hr1
        {
            get { return Values[3]; }
        }

        [Browsable(false)] // this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore] // this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
            public DataSeries Hr2
        {
            get { return Values[4]; }
        }

        #endregion
    }
}

#region NinjaScript generated code. Neither change nor remove.
// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    public partial class Indicator : IndicatorBase
    {
        private TSIntradayPivots[] cacheTSIntradayPivots = null;

        private static TSIntradayPivots checkTSIntradayPivots = new TSIntradayPivots();

        /// <summary>
        /// Rolling Intraday Pivots (created by TradingStudies.com)
        /// </summary>
        /// <returns></returns>
        public TSIntradayPivots TSIntradayPivots(int minutes)
        {
            return TSIntradayPivots(Input, minutes);
        }

        /// <summary>
        /// Rolling Intraday Pivots (created by TradingStudies.com)
        /// </summary>
        /// <returns></returns>
        public TSIntradayPivots TSIntradayPivots(Data.IDataSeries input, int minutes)
        {
            if (cacheTSIntradayPivots != null)
                for (int idx = 0; idx < cacheTSIntradayPivots.Length; idx++)
                    if (cacheTSIntradayPivots[idx].Minutes == minutes && cacheTSIntradayPivots[idx].EqualsInput(input))
                        return cacheTSIntradayPivots[idx];

            lock (checkTSIntradayPivots)
            {
                checkTSIntradayPivots.Minutes = minutes;
                minutes = checkTSIntradayPivots.Minutes;

                if (cacheTSIntradayPivots != null)
                    for (int idx = 0; idx < cacheTSIntradayPivots.Length; idx++)
                        if (cacheTSIntradayPivots[idx].Minutes == minutes && cacheTSIntradayPivots[idx].EqualsInput(input))
                            return cacheTSIntradayPivots[idx];

                TSIntradayPivots indicator = new TSIntradayPivots();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.Minutes = minutes;
                Indicators.Add(indicator);
                indicator.SetUp();

                TSIntradayPivots[] tmp = new TSIntradayPivots[cacheTSIntradayPivots == null ? 1 : cacheTSIntradayPivots.Length + 1];
                if (cacheTSIntradayPivots != null)
                    cacheTSIntradayPivots.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheTSIntradayPivots = tmp;
                return indicator;
            }
        }
    }
}

// This namespace holds all market analyzer column definitions and is required. Do not change it.
namespace NinjaTrader.MarketAnalyzer
{
    public partial class Column : ColumnBase
    {
        /// <summary>
        /// Rolling Intraday Pivots (created by TradingStudies.com)
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.TSIntradayPivots TSIntradayPivots(int minutes)
        {
            return _indicator.TSIntradayPivots(Input, minutes);
        }

        /// <summary>
        /// Rolling Intraday Pivots (created by TradingStudies.com)
        /// </summary>
        /// <returns></returns>
        public Indicator.TSIntradayPivots TSIntradayPivots(Data.IDataSeries input, int minutes)
        {
            return _indicator.TSIntradayPivots(input, minutes);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// Rolling Intraday Pivots (created by TradingStudies.com)
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.TSIntradayPivots TSIntradayPivots(int minutes)
        {
            return _indicator.TSIntradayPivots(Input, minutes);
        }

        /// <summary>
        /// Rolling Intraday Pivots (created by TradingStudies.com)
        /// </summary>
        /// <returns></returns>
        public Indicator.TSIntradayPivots TSIntradayPivots(Data.IDataSeries input, int minutes)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.TSIntradayPivots(input, minutes);
        }
    }
}
#endregion
