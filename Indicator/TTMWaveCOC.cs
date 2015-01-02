using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.Design;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace NinjaTrader.Indicator
{


    [NinjaTrader.Gui.Design.DisplayName("TTM Wave C OC Gapless"), Description("TTM Wave C Open Code with gapless option")]
    public class TTMWaveCOC : NinjaTrader.Indicator.Indicator
    {
        private string IndicatorName = "TTM Wave C OC Gapless";
        private Color ppWaveContractionColor = Color.Crimson;
        private Color ppWaveExpansionColor = Color.DarkOrange;
		private bool useGapless = false;//Added Sim22
		private bool showSponsor = true;//Added Sim22

        protected override void Initialize()
        {
            base.Add(new Plot(new Pen(this.pWaveExpansionColor, 6f), PlotStyle.Bar, "Wave1"));
            base.Add(new Plot(new Pen(this.pWaveContractionColor, 6f), PlotStyle.Bar, "Wave2"));
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
                MACDGapless macd = base.MACDGapless(base.Close, 8, 0xe9, 1,useGapless); //gapless added Sim22
                MACDGapless macd2 = base.MACDGapless(base.Close, 8, 0x179, 1,useGapless); //gapless added Sim22
                double num = macd.Default[0];
                double num2 = base.EMA(macd.Default, 0xe9)[0];
                double num3 = num - num2;
                this.Wave1.Set(num3);
                double num4 = macd2.Default[0];
                double num5 = base.EMA(macd2.Default, 0x179)[0];
                double num6 = num4 - num5;
                this.Wave2.Set(num6);
            }
        }

        public override string ToString()
        {
            return this.IndicatorName;
        }

        [Category("Display"), Description("OutsideColor"), NinjaTrader.Gui.Design.DisplayName("c2_OutsideColor")]
        public Color pWaveContractionColor
        {
            get
            {
                return this.ppWaveContractionColor;
            }
            set
            {
                this.ppWaveContractionColor = value;
            }
        }

        [Browsable(false)]
        public string pWaveContractionColorSerialize
        {
            get
            {
                return SerializableColor.ToString(this.ppWaveContractionColor);
            }
            set
            {
                this.ppWaveContractionColor = SerializableColor.FromString(value);
            }
        }

        [Category("Display"), NinjaTrader.Gui.Design.DisplayName("c1_InsideColor"), Description("InsideColor")]
        public Color pWaveExpansionColor
        {
            get
            {
                return this.ppWaveExpansionColor;
            }
            set
            {
                this.ppWaveExpansionColor = value;
            }
        }

        [Browsable(false)]
        public string pWaveExpansionColorSerialize
        {
            get
            {
                return SerializableColor.ToString(this.ppWaveExpansionColor);
            }
            set
            {
                this.ppWaveExpansionColor = SerializableColor.FromString(value);
            }
        }

        [XmlIgnore, Browsable(false)]
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
        private TTMWaveCOC[] cacheTTMWaveCOC = null;

        private static TTMWaveCOC checkTTMWaveCOC = new TTMWaveCOC();

        /// <summary>
        /// VM Wave A Open Code with gapless option
        /// </summary>
        /// <returns></returns>
        public TTMWaveCOC TTMWaveCOC(bool useGapless)
        {
            return TTMWaveCOC(Input, useGapless);
        }

        /// <summary>
        /// VM Wave A Open Code with gapless option
        /// </summary>
        /// <returns></returns>
        public TTMWaveCOC TTMWaveCOC(Data.IDataSeries input, bool useGapless)
        {
            if (cacheTTMWaveCOC != null)
                for (int idx = 0; idx < cacheTTMWaveCOC.Length; idx++)
                    if (cacheTTMWaveCOC[idx].UseGapless == useGapless && cacheTTMWaveCOC[idx].EqualsInput(input))
                        return cacheTTMWaveCOC[idx];

            lock (checkTTMWaveCOC)
            {
                checkTTMWaveCOC.UseGapless = useGapless;
                useGapless = checkTTMWaveCOC.UseGapless;

                if (cacheTTMWaveCOC != null)
                    for (int idx = 0; idx < cacheTTMWaveCOC.Length; idx++)
                        if (cacheTTMWaveCOC[idx].UseGapless == useGapless && cacheTTMWaveCOC[idx].EqualsInput(input))
                            return cacheTTMWaveCOC[idx];

                TTMWaveCOC indicator = new TTMWaveCOC();
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

                TTMWaveCOC[] tmp = new TTMWaveCOC[cacheTTMWaveCOC == null ? 1 : cacheTTMWaveCOC.Length + 1];
                if (cacheTTMWaveCOC != null)
                    cacheTTMWaveCOC.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheTTMWaveCOC = tmp;
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
        public Indicator.TTMWaveCOC TTMWaveCOC(bool useGapless)
        {
            return _indicator.TTMWaveCOC(Input, useGapless);
        }

        /// <summary>
        /// VM Wave A Open Code with gapless option
        /// </summary>
        /// <returns></returns>
        public Indicator.TTMWaveCOC TTMWaveCOC(Data.IDataSeries input, bool useGapless)
        {
            return _indicator.TTMWaveCOC(input, useGapless);
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
        public Indicator.TTMWaveCOC TTMWaveCOC(bool useGapless)
        {
            return _indicator.TTMWaveCOC(Input, useGapless);
        }

        /// <summary>
        /// VM Wave A Open Code with gapless option
        /// </summary>
        /// <returns></returns>
        public Indicator.TTMWaveCOC TTMWaveCOC(Data.IDataSeries input, bool useGapless)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.TTMWaveCOC(input, useGapless);
        }
    }
}
#endregion