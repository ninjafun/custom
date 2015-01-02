using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
using RSqueeze.Utility;

namespace RSqueeze.Utility
{
    public enum SqueezeStyle
    {
        BBSqueeze,
        PBFSqueeze,
        CounterTrend
    }
}

namespace NinjaTrader.Indicator
{  
	[Description("Squeeze collection")]
    public class RSqueeze : Indicator
    {
        private int _length = 20;
        private double _nBb = 2;
        private double _nK = 1.5;
        private int _mLength = 20;
        private SqueezeStyle _sStyle = SqueezeStyle.BBSqueeze;
        private DataSeries _myValue2;
        private double _fv1;
	    private double _sv1;
	    private double _fv2;
	    private double _sv2;
	    private double _fv3;
	    private double _sv3;
	    private double _fv4;
	    private double _sv4;
	    private double _d1;
	    private double _d2;
	    private double _d3;
	    private double _d4;
	    private int _upct, _downct;
        private Color _alertcolor = Color.Red;
        private Color _normalcolor = Color.Blue;
        private Color _pmup = Color.Lime;
        private Color _pmdown = Color.DarkGreen;
        private Color _nmdown = Color.Red;
        private Color _nmup = Color.Maroon;
        private Color _pbfbuy = Color.DarkGreen;
        private Color _pbfsell = Color.DarkRed;
        private int _zerolinewidth = 2;
        private int _histogramwidth = 5;
        private int _cciperiod = 13;
        private int _gaussperiod = 21;
        private int _gausspoles = 3;
        private int _gausspolescounter = 4;

        protected override void Initialize()
        {
			Name = " RSqueeze";
            Add(new Plot(_alertcolor, PlotStyle.Dot, "Squeeze"));
            Add(new Plot(_pmup, PlotStyle.Bar, "Momentum"));
            _myValue2 = new DataSeries(this);

            CalculateOnBarClose = true;
            Overlay = false;
            PriceTypeSupported = false;
            PlotsConfigurable = false;

            Plots[0].Pen.Width = _zerolinewidth;
            Plots[1].Pen.Width = _histogramwidth;
        }

        protected override void OnBarUpdate()
        {
			Squeeze.Set(0);
            switch (_sStyle)
            {
                case SqueezeStyle.BBSqueeze:
                    {
                        double avtrrg = ATR(_length)[0];
                        double sd = StdDev(Close, _length)[0];
                        double bbsInd = (_nK * avtrrg) != 0 ? _nBb * sd / (_nK * avtrrg) : 1;
						PlotColors[0][0] = bbsInd <= 1 ? _alertcolor : _normalcolor;
                        _myValue2.Set(Input[0] - (((DonchianChannel(Input, _mLength).Mean.Get(CurrentBar)) + (EMA(Input, _mLength)[0])) / 2));
                        Mom.Set(LinReg(_myValue2, _mLength)[0]);
						if(CurrentBar > 0) 
							PlotColors[1][0] = Mom[0] > 0 ? (Mom[0] > Mom[1] ? _pmup : _pmdown) : (Mom[0] > Mom[1] ? _nmup : _nmdown);
                    }
                    break;
                case SqueezeStyle.PBFSqueeze:
                    if (CurrentBar == 0)
                        _myValue2.Set(Close[0]);
                    else
                    {
                        _myValue2.Set(RGaussianFilter(Typical, _gaussperiod, _gausspoles)[0]);
						PlotColors[0][0] = _myValue2[0] <= _myValue2[1] ? _pbfsell : _pbfbuy;
                        _fv1 = RGaussianFilter(Typical, 8, 4)[0];
                        _sv1 = RGaussianFilter(Typical, 21, 4)[0];
                        _d1 = _fv1 - _sv1;
                        _fv2 = RGaussianFilter(Typical, 13, 4)[0];
                        _sv2 = RGaussianFilter(Typical, 34, 4)[0];
                        _d2 = _fv2 - _sv2;
                        _fv3 = RGaussianFilter(Typical, 21, 4)[0];
                        _sv3 = RGaussianFilter(Typical, 55, 4)[0];
                        _d3 = _fv3 - _sv3;
                        _fv4 = RGaussianFilter(Typical, 34, 4)[0];
                        _sv4 = RGaussianFilter(Typical, 89, 4)[0];
                        _d4 = _fv4 - _sv4;
                        Mom.Set((_d1 + _d2 + _d3 + _d4) / 4);
						PlotColors[1][0] = Mom[0] > 0 ? (Mom[0] > Mom[1] ? _pmup : _pmdown) : (Mom[0] > Mom[1] ? _nmup : _nmdown);
                    }
                    break;
                default:
                    if (CurrentBar == 0)
                        _myValue2.Set(Close[0]);
                    else
                    {
                        _myValue2.Set(RGaussianFilter(Typical, _gaussperiod, _gausspoles)[0]);
						PlotColors[0][0] = _myValue2[0] <= _myValue2[1] ? _pbfsell : _pbfbuy;
                        _fv1 = RGaussianFilter(Typical, 8, _gausspolescounter)[0];
                        _sv1 = RGaussianFilter(Typical, 21, _gausspolescounter)[0];
                        _d1 = _fv1 - _sv1;
                        _fv2 = RGaussianFilter(Typical, 13, _gausspolescounter)[0];
                        _sv2 = RGaussianFilter(Typical, 34, _gausspolescounter)[0];
                        _d2 = _fv2 - _sv2;
                        _fv3 = RGaussianFilter(Typical, 21, _gausspolescounter)[0];
                        _sv3 = RGaussianFilter(Typical, 55, _gausspolescounter)[0];
                        _d3 = _fv3 - _sv3;
                        _fv4 = RGaussianFilter(Typical, 34, _gausspolescounter)[0];
                        _sv4 = RGaussianFilter(Typical, 89, _gausspolescounter)[0];
                        _d4 = _fv4 - _sv4;
                        Mom.Set((_d1 + _d2 + _d3 + _d4) / 4);

						PlotColors[1][0] = Mom[0] > 0 ? (Mom[0] > Mom[1] ? _pmup : _pmdown) : (Mom[0] > Mom[1] ? _nmup : _nmdown);

                        if (CCI(_cciperiod)[1] > 50 && CCI(_cciperiod)[0] <= 50 && Mom[0] > 0 && Mom[1] > 0 && Mom[2] > 0)
                        {
							PlotColors[1][0] = _nmup;
                            _downct = 1;
                        }

                        if (_downct == 1 && Mom[0] > 0 && CCI(_cciperiod)[0] < 100)
							PlotColors[1][0] = _nmup;

                        if (_downct == 1 && CCI(_cciperiod)[1] < 100 && CCI(_cciperiod)[0] > 100)
                        {
							PlotColors[1][0] = _pmup;
                            _downct = 0;
                        }

                        if (_downct == 1 && Mom[0] < 0)
                            _downct = 0;

                        if (CCI(_cciperiod)[1] < -50 && CCI(_cciperiod)[0] > -50 && Mom[0] < 0 && Mom[1] < 0 && Mom[2] < 0)
                        {
							PlotColors[1][0] = _pmdown;
                            _upct = 1;
                        }
						
                        if (_upct == 1 && Mom[0] > 0)
                            _upct = 0;

                        if (_upct == 1 && Mom[0] < 0 && CCI(_cciperiod)[0] > -100)
							PlotColors[1][0] = _pmdown;

                        if (_upct == 1 && CCI(_cciperiod)[1] > -100 && CCI(_cciperiod)[0] < -100)
                        {
							PlotColors[1][0] = _nmdown;
                            _upct = 0;
                        }
                    }
                    break;
            }
        }
        #region Properties
        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries Squeeze
        {
            get { return Values[0]; }
        }

        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries Mom
        {
            get { return Values[1]; }
        }

        [Description("BB Squeeze ATR and StdDev Lenght")]
        [Category("BB Squeeze")]
        [Gui.Design.DisplayName("ATR, StdDev Lenght")]
        public int Length
        {
            get { return _length; }
            set { _length = Math.Max(1, value); }
        }

        [Description("Zero Line Width")]
        [Category("Visual")]
        [Gui.Design.DisplayName("Zero Line Width")]
        public int ZeroLineWidth
        {
            get { return _zerolinewidth; }
            set { _zerolinewidth = Math.Max(1, value); }
        }

        [Description("Histogram (Momentum) Width")]
        [Category("Visual")]
        [Gui.Design.DisplayName("Histogram (Momentum) Width")]
        public int HistogramWidth
        {
            get { return _histogramwidth; }
            set { _histogramwidth = Math.Max(1, value); }
        }

        [Description("Bolinger Bands Std. Devs from Average")]
        [Category("BB Squeeze")]
        [Gui.Design.DisplayName("Bolinger Bands Std. Devs from Average")]
        public double Nbb
        {
            get { return _nBb; }
            set { _nBb = Math.Max(0, value); }
        }

        [Description("Keltner Channel ATRs from Average")]
        [Category("BB Squeeze")]
        [Gui.Design.DisplayName("Keltner Channel ATRs from Average")]
        public double Nk
        {
            get { return _nK; }
            set { _nK = Math.Max(0, value); }
        }

        [Description("Momentum Lenght")]
        [Category("BB Squeeze")]
        [Gui.Design.DisplayName("Momentum Period Length")]
        public int MLength
        {
            get { return _mLength; }
            set { _mLength = Math.Max(1, value); }
        }

        [Description("CCI Period for CounterTrend Mode")]
        [Category("CounterTrend")]
        [Gui.Design.DisplayName("CCI Period")]
        public int CciPeriod
        {
            get { return _cciperiod; }
            set { _cciperiod = Math.Max(1, value); }
        }

        [Description("Gauss Filter Poles for Countertrend Mode")]
        [Category("CounterTrend")]
        [Gui.Design.DisplayName("Gauss Filter Poles")]
        public int GaussPolesCounter
        {
            get { return _gausspolescounter; }
            set { _gausspolescounter = Math.Max(1, value); }
        }

        [Description("Gaussian Filter for PBF Period")]
        [Category("PBF Squeeze")]
        [Gui.Design.DisplayName("Gaussian Filter Period")]
        public int GaussPeriod
        {
            get { return _gaussperiod; }
            set { _gaussperiod = Math.Max(1, value); }
        }

        [Description("Gaussian Filter for PBF Poles")]
        [Category("PBF Squeeze")]
        [Gui.Design.DisplayName("Gaussian Filter Poles")]
        public int GaussPoles
        {
            get { return _gausspoles; }
            set { _gausspoles = Math.Max(1, value); }
        }

        [Description("Squeeze Style")]
        [Category("Parameters")]
        [Gui.Design.DisplayName("Squeeze Style")]
        public SqueezeStyle SStyle
        {
            get { return _sStyle; }
            set { _sStyle = value; }
        }

        [Description("BBSqueeze  ZeroLine AlertColor")]
        [Category("BB Squeeze")]
        [Gui.Design.DisplayName("BBSqueeze AlertColor")]
        public Color Alertcolor
        {
            get { return _alertcolor; }
            set { _alertcolor = value; }
        }

        [Browsable(false)]
        public string AlertcolorSerialize
        {
            get { return Gui.Design.SerializableColor.ToString(_alertcolor); }
            set { _alertcolor = Gui.Design.SerializableColor.FromString(value); }
        }

        [Description("BBSqueeze  ZeroLine NormalColor")]
        [Category("BB Squeeze")]
        [Gui.Design.DisplayName("BBSqueeze NormalColor")]
        public Color Normalcolor
        {
            get { return _normalcolor; }
            set { _normalcolor = value; }
        }

        [Browsable(false)]
        public string NormalcolorSerialize
        {
            get { return Gui.Design.SerializableColor.ToString(_normalcolor); }
            set { _normalcolor = Gui.Design.SerializableColor.FromString(value); }
        }

        [Description("Positive Momentum Rising")]
        [Category("Visual")]
        [Gui.Design.DisplayName("Positive Momentum Rising")]
        public Color Pmup
        {
            get { return _pmup; }
            set { _pmup = value; }
        }

        [Browsable(false)]
        public string PmupSerialize
        {
            get { return Gui.Design.SerializableColor.ToString(_pmup); }
            set { _pmup = Gui.Design.SerializableColor.FromString(value); }
        }

        [Description("Positive Momentum Falling")]
        [Category("Visual")]
        [Gui.Design.DisplayName("Positive Momentum Falling")]
        public Color Pmdown
        {
            get { return _pmdown; }
            set { _pmdown = value; }
        }

        [Browsable(false)]
        public string PmdownSerialize
        {
            get { return Gui.Design.SerializableColor.ToString(_pmdown); }
            set { _pmdown = Gui.Design.SerializableColor.FromString(value); }
        }

        [Description("Negative Momentum Falling")]
        [Category("Visual")]
        [Gui.Design.DisplayName("Negative Momentum Falling")]
        public Color Nmdown
        {
            get { return _nmdown; }
            set { _nmdown = value; }
        }

        [Browsable(false)]
        public string NmdownSerialize
        {
            get { return Gui.Design.SerializableColor.ToString(_nmdown); }
            set { _nmdown = Gui.Design.SerializableColor.FromString(value); }
        }
        [Description("Negative Momentum Rising")]
        [Category("Visual")]
        [Gui.Design.DisplayName("Negative Momentum Rising")]
        public Color Nmup
        {
            get { return _nmup; }
            set { _nmup = value; }
        }

        [Browsable(false)]
        public string NmupSerialize
        {
            get { return Gui.Design.SerializableColor.ToString(_nmup); }
            set { _nmup = Gui.Design.SerializableColor.FromString(value); }
        }


        [Description("PBF Buy Signal Color")]
        [Category("PBF Squeeze")]
        [Gui.Design.DisplayName("PBF Buy Color (Zero Line)")]
        public Color PbfBuy
        {
            get { return _pbfbuy; }
            set { _pbfbuy = value; }
        }

        [Browsable(false)]
        public string PbfbuySerialize
        {
            get { return Gui.Design.SerializableColor.ToString(_pbfbuy); }
            set { _pbfbuy = Gui.Design.SerializableColor.FromString(value); }
        }

        [Description("PBF Sell Signal Color")]
        [Category("PBF Squeeze")]
        [Gui.Design.DisplayName("PBF Sell Color (Zero Line)")]
        public Color PbfSell
        {
            get { return _pbfsell; }
            set { _pbfsell = value; }
        }

        [Browsable(false)]
        public string PbfsellSerialize
        {
            get { return Gui.Design.SerializableColor.ToString(_pbfsell); }
            set { _pbfsell = Gui.Design.SerializableColor.FromString(value); }
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
        private RSqueeze[] cacheRSqueeze = null;

        private static RSqueeze checkRSqueeze = new RSqueeze();

        /// <summary>
        /// Squeeze collection
        /// </summary>
        /// <returns></returns>
        public RSqueeze RSqueeze(SqueezeStyle sStyle)
        {
            return RSqueeze(Input, sStyle);
        }

        /// <summary>
        /// Squeeze collection
        /// </summary>
        /// <returns></returns>
        public RSqueeze RSqueeze(Data.IDataSeries input, SqueezeStyle sStyle)
        {
            if (cacheRSqueeze != null)
                for (int idx = 0; idx < cacheRSqueeze.Length; idx++)
                    if (cacheRSqueeze[idx].SStyle == sStyle && cacheRSqueeze[idx].EqualsInput(input))
                        return cacheRSqueeze[idx];

            lock (checkRSqueeze)
            {
                checkRSqueeze.SStyle = sStyle;
                sStyle = checkRSqueeze.SStyle;

                if (cacheRSqueeze != null)
                    for (int idx = 0; idx < cacheRSqueeze.Length; idx++)
                        if (cacheRSqueeze[idx].SStyle == sStyle && cacheRSqueeze[idx].EqualsInput(input))
                            return cacheRSqueeze[idx];

                RSqueeze indicator = new RSqueeze();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.SStyle = sStyle;
                Indicators.Add(indicator);
                indicator.SetUp();

                RSqueeze[] tmp = new RSqueeze[cacheRSqueeze == null ? 1 : cacheRSqueeze.Length + 1];
                if (cacheRSqueeze != null)
                    cacheRSqueeze.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheRSqueeze = tmp;
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
        /// Squeeze collection
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.RSqueeze RSqueeze(SqueezeStyle sStyle)
        {
            return _indicator.RSqueeze(Input, sStyle);
        }

        /// <summary>
        /// Squeeze collection
        /// </summary>
        /// <returns></returns>
        public Indicator.RSqueeze RSqueeze(Data.IDataSeries input, SqueezeStyle sStyle)
        {
            return _indicator.RSqueeze(input, sStyle);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// Squeeze collection
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.RSqueeze RSqueeze(SqueezeStyle sStyle)
        {
            return _indicator.RSqueeze(Input, sStyle);
        }

        /// <summary>
        /// Squeeze collection
        /// </summary>
        /// <returns></returns>
        public Indicator.RSqueeze RSqueeze(Data.IDataSeries input, SqueezeStyle sStyle)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.RSqueeze(input, sStyle);
        }
    }
}
#endregion
