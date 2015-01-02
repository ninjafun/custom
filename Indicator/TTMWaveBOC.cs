using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.Design;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace NinjaTrader.Indicator
{


    [NinjaTrader.Gui.Design.DisplayName("TTM Wave B OC Gapless"), Description("TTM Wave B Open Code with gapless option")]
    public class TTMWaveBOC : NinjaTrader.Indicator.Indicator
    {
        private string IndicatorName = "TTM Wave B OC Gapless";
        private Color ppInsideColor = Color.Blue;
        private Color ppOutsideColor = Color.Magenta;
		private bool useGapless = false;//Added Sim22
		private bool showSponsor = true;//Added Sim22

        protected override void Initialize()
        {
            base.Add(new Plot(new Pen(this.pInsideColor, 6f), PlotStyle.Bar, "Wave1"));
            base.Add(new Plot(new Pen(this.pOutsideColor, 6f), PlotStyle.Bar, "Wave2"));
            base.Add(new Line(Color.Gray, 0.0, "ZeroLine"));
            base.Overlay = false;
            base.PriceTypeSupported = false;
            base.PaintPriceMarkers = false;
			base.DrawOnPricePanel = false;//Added Sim22
        }

        protected override void OnBarUpdate()
        {
            if (base.CurrentBar >= 1)
            {
				if (showSponsor) DrawTextFixed("tag1", "www.ewef.net", TextPosition.BottomLeft);//Added Sim22
                MACDGapless macd = base.MACDGapless(base.Close, 8, 0x59, 1,useGapless); //gapless added Sim22
                MACDGapless macd2 = base.MACDGapless(base.Close, 8, 0x90, 1,useGapless); //gapless added Sim22
                MACDGapless macd3 = base.MACDGapless(base.Close, 8, 0xe9, 1,useGapless); //gapless added Sim22
                double num = macd.Default[0];
                double num2 = base.EMA(macd.Default, 0x59)[0];
                double num3 = num - num2;
                this.Wave1.Set(num3);
                double num4 = macd2.Default[0];
                double num5 = base.EMA(macd2.Default, 0x90)[0];
                double num6 = num4 - num5;
                this.Wave2.Set(num6);
                double num1 = macd3.Default[0];
                double num7 = base.EMA(macd3.Default, 0xe9)[0];
            }
        }

        public override string ToString()
        {
            return this.IndicatorName;
        }

        [Category("Display"), NinjaTrader.Gui.Design.DisplayName("c1_InsideColor"), Description("InsideColor")]
        public Color pInsideColor
        {
            get
            {
                return this.ppInsideColor;
            }
            set
            {
                this.ppInsideColor = value;
            }
        }

        [Browsable(false)]
        public string pInsideColorSerialize
        {
            get
            {
                return SerializableColor.ToString(this.ppInsideColor);
            }
            set
            {
                this.ppInsideColor = SerializableColor.FromString(value);
            }
        }

        [Description("OutsideColor"), NinjaTrader.Gui.Design.DisplayName("c2_OutsideColor"), Category("Display")]
        public Color pOutsideColor
        {
            get
            {
                return this.ppOutsideColor;
            }
            set
            {
                this.ppOutsideColor = value;
            }
        }

        [Browsable(false)]
        public string pOutsideColorSerialize
        {
            get
            {
                return SerializableColor.ToString(this.ppOutsideColor);
            }
            set
            {
                this.ppOutsideColor = SerializableColor.FromString(value);
            }
        }

        [Browsable(false), XmlIgnore]
        public DataSeries Wave1
        {
            get
            {
                return base.Values[0];
            }
        }

        [XmlIgnore, Browsable(false)]
        public DataSeries Wave2
        {
            get
            {
                return base.Values[1];
            }
        }
		
		//Added Sim22
		[Description("Use gapless method. Ignores gaps between close[1] and open[0]. Best used for new session intraday open.")]
        [GridCategory("Parameters")]
        public bool UseGapless
        {
            get { return useGapless; }
            set { useGapless = value; }
        }
		
		[NinjaTrader.Gui.Design.DisplayName("Sponsor"), GridCategory("Parameters"), Description("www.ewef.net")]
        public bool ShowSponsor
        {
            get
            {
                return this.showSponsor;
            }
            set
            {
                this.showSponsor = value;
            }
		}
    }
}

#region NinjaScript generated code. Neither change nor remove.
// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    public partial class Indicator : IndicatorBase
    {
        private TTMWaveBOC[] cacheTTMWaveBOC = null;

        private static TTMWaveBOC checkTTMWaveBOC = new TTMWaveBOC();

        /// <summary>
        /// VM Wave A Open Code with gapless option
        /// </summary>
        /// <returns></returns>
        public TTMWaveBOC TTMWaveBOC(bool useGapless)
        {
            return TTMWaveBOC(Input, useGapless);
        }

        /// <summary>
        /// VM Wave A Open Code with gapless option
        /// </summary>
        /// <returns></returns>
        public TTMWaveBOC TTMWaveBOC(Data.IDataSeries input, bool useGapless)
        {
            if (cacheTTMWaveBOC != null)
                for (int idx = 0; idx < cacheTTMWaveBOC.Length; idx++)
                    if (cacheTTMWaveBOC[idx].UseGapless == useGapless && cacheTTMWaveBOC[idx].EqualsInput(input))
                        return cacheTTMWaveBOC[idx];

            lock (checkTTMWaveBOC)
            {
                checkTTMWaveBOC.UseGapless = useGapless;
                useGapless = checkTTMWaveBOC.UseGapless;

                if (cacheTTMWaveBOC != null)
                    for (int idx = 0; idx < cacheTTMWaveBOC.Length; idx++)
                        if (cacheTTMWaveBOC[idx].UseGapless == useGapless && cacheTTMWaveBOC[idx].EqualsInput(input))
                            return cacheTTMWaveBOC[idx];

                TTMWaveBOC indicator = new TTMWaveBOC();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.UseGapless = useGapless;
                Indicators.Add(indicator);
                indicator.SetUp();

                TTMWaveBOC[] tmp = new TTMWaveBOC[cacheTTMWaveBOC == null ? 1 : cacheTTMWaveBOC.Length + 1];
                if (cacheTTMWaveBOC != null)
                    cacheTTMWaveBOC.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheTTMWaveBOC = tmp;
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
        /// VM Wave A Open Code with gapless option
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.TTMWaveBOC TTMWaveBOC(bool useGapless)
        {
            return _indicator.TTMWaveBOC(Input, useGapless);
        }

        /// <summary>
        /// VM Wave A Open Code with gapless option
        /// </summary>
        /// <returns></returns>
        public Indicator.TTMWaveBOC TTMWaveBOC(Data.IDataSeries input, bool useGapless)
        {
            return _indicator.TTMWaveBOC(input, useGapless);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// VM Wave A Open Code with gapless option
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.TTMWaveBOC TTMWaveBOC(bool useGapless)
        {
            return _indicator.TTMWaveBOC(Input, useGapless);
        }

        /// <summary>
        /// VM Wave A Open Code with gapless option
        /// </summary>
        /// <returns></returns>
        public Indicator.TTMWaveBOC TTMWaveBOC(Data.IDataSeries input, bool useGapless)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.TTMWaveBOC(input, useGapless);
        }
    }
}
#endregion
