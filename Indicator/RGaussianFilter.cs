using System;
using System.Drawing;
using System.ComponentModel;
using System.Xml.Serialization;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;

namespace NinjaTrader.Indicator
{
    [Description("Gaussian Filter Ported to NT")]
    public class RGaussianFilter : Indicator
    {
        private int _period = 90;
        private double _w;
        private double _y1;
        private double _aa;
        private double _y;
        private double _y2;
        private readonly double _sqrtOf2 = Math.Sqrt(2.0);
        private double _y3;
        private double _y4;
        private double _a2;
        private double _a3;
        private double _a4;
        private const double Pi = 22 / 7;
        private double _a12;
        private int _poles = 2;
        private double _b;
        private double _a13;
        private double _a14;
        private double _a1;
        private double _price;

        protected override void Initialize()
        {
			Name = " RGaussianFilter";
            Add(new Plot(Color.Red, PlotStyle.Line, "Gauss"));
            CalculateOnBarClose = false;
            Overlay = true;
            PriceTypeSupported = true;
        }

        protected override void OnBarUpdate()
        {
            _price = Input[0];
            if (CurrentBar == 0)
            {
                _w = 2 * Pi / Period;
                _b = (1 - Math.Cos(_w)) / (Math.Pow(_sqrtOf2, 2.0 / _poles) - 1);
                _aa = -_b + Math.Sqrt(_b * _b + 2 * _b);
                _a1 = 1.0 - _aa;
                _a12 = _a1 * _a1;
                _a13 = _a1 * _a1 * _a1;
                _a14 = _a12 * _a12;
                _a2 = _aa * _aa;
                _a3 = _aa * _aa * _aa;
                _a4 = _a2 * _a2;
                _y1 = _price;
                _y2 = _y1;
                _y3 = _y2;
                _y4 = _y3;
            }

            if (CurrentBar < Period)
            {
                return;
            }

            switch (_poles)
            {
                case 1:
                    _y = _aa * _price + _a1 * _y1; break;
                case 2:
                    _y = _a2 * _price + 2 * _a1 * _y1 - _a12 * _y2; break;
                case 3:
                    _y = _a3 * _price + 3 * _a1 * _y1 - 3 * _a12 * _y2 + _a13 * _y3; break;
                case 4:
                    _y = _a4 * _price + 4 * _a1 * _y1 - 6 * _a12 * _y2 + 4 * _a13 * _y3 - _a14 * _y4; break;
            }
            if (FirstTickOfBar)
            {
                _y4 = _y3;
                _y3 = _y2;
                _y2 = _y1;
                _y1 = _y;
            }
            Gauss.Set(_y);
        }


        #region Properties
        [Browsable(false)]	
        [XmlIgnore]		
        public DataSeries Gauss
        {
            get { return Values[0]; }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int Poles
        {
            get { return _poles; }
            set { _poles = Math.Min(Math.Max(1, value), 4); }
        }


        [Description("")]
        [GridCategory("Parameters")]
        public int Period
        {
            get { return _period; }
            set { _period = Math.Max(1, value); }
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
        private RGaussianFilter[] cacheRGaussianFilter = null;

        private static RGaussianFilter checkRGaussianFilter = new RGaussianFilter();

        /// <summary>
        /// Gaussian Filter Ported to NT
        /// </summary>
        /// <returns></returns>
        public RGaussianFilter RGaussianFilter(int period, int poles)
        {
            return RGaussianFilter(Input, period, poles);
        }

        /// <summary>
        /// Gaussian Filter Ported to NT
        /// </summary>
        /// <returns></returns>
        public RGaussianFilter RGaussianFilter(Data.IDataSeries input, int period, int poles)
        {
            if (cacheRGaussianFilter != null)
                for (int idx = 0; idx < cacheRGaussianFilter.Length; idx++)
                    if (cacheRGaussianFilter[idx].Period == period && cacheRGaussianFilter[idx].Poles == poles && cacheRGaussianFilter[idx].EqualsInput(input))
                        return cacheRGaussianFilter[idx];

            lock (checkRGaussianFilter)
            {
                checkRGaussianFilter.Period = period;
                period = checkRGaussianFilter.Period;
                checkRGaussianFilter.Poles = poles;
                poles = checkRGaussianFilter.Poles;

                if (cacheRGaussianFilter != null)
                    for (int idx = 0; idx < cacheRGaussianFilter.Length; idx++)
                        if (cacheRGaussianFilter[idx].Period == period && cacheRGaussianFilter[idx].Poles == poles && cacheRGaussianFilter[idx].EqualsInput(input))
                            return cacheRGaussianFilter[idx];

                RGaussianFilter indicator = new RGaussianFilter();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.Period = period;
                indicator.Poles = poles;
                Indicators.Add(indicator);
                indicator.SetUp();

                RGaussianFilter[] tmp = new RGaussianFilter[cacheRGaussianFilter == null ? 1 : cacheRGaussianFilter.Length + 1];
                if (cacheRGaussianFilter != null)
                    cacheRGaussianFilter.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheRGaussianFilter = tmp;
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
        /// Gaussian Filter Ported to NT
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.RGaussianFilter RGaussianFilter(int period, int poles)
        {
            return _indicator.RGaussianFilter(Input, period, poles);
        }

        /// <summary>
        /// Gaussian Filter Ported to NT
        /// </summary>
        /// <returns></returns>
        public Indicator.RGaussianFilter RGaussianFilter(Data.IDataSeries input, int period, int poles)
        {
            return _indicator.RGaussianFilter(input, period, poles);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// Gaussian Filter Ported to NT
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.RGaussianFilter RGaussianFilter(int period, int poles)
        {
            return _indicator.RGaussianFilter(Input, period, poles);
        }

        /// <summary>
        /// Gaussian Filter Ported to NT
        /// </summary>
        /// <returns></returns>
        public Indicator.RGaussianFilter RGaussianFilter(Data.IDataSeries input, int period, int poles)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.RGaussianFilter(input, period, poles);
        }
    }
}
#endregion
