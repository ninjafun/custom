using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.Design;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace NinjaTrader.Indicator
{


    [Description("TTM Wave A Open Code with gapless option"), NinjaTrader.Gui.Design.DisplayName("TTM Wave A OC Gapless")]
    public class TTMWaveAOC : Indicator
    {
        private string IndicatorName = "TTM Wave A OC Gapless";
        private Color ppWave1Color = Color.Orange;
        private Color ppWave2Color = Color.LimeGreen;
		private bool useGapless = false;//Added Sim22
		private bool showSponsor = false;//Added Sim22

        protected override void Initialize()
        {
            base.Add(new Plot(new Pen(this.pWave1Color, 6f), PlotStyle.Bar, "Wave1"));
            base.Add(new Plot(new Pen(this.pWave2Color, 6f), PlotStyle.Bar, "Wave2"));
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
                MACDGapless macd = base.MACDGapless(base.Close, 8, 0x15, 1, useGapless); //gapless added Sim22
                MACDGapless macd2 = base.MACDGapless(base.Close, 8, 0x22, 1,useGapless); //gapless added Sim22
                MACDGapless macd3 = base.MACDGapless(base.Close, 8, 0x37, 1,useGapless); //gapless added Sim22
                double num = macd.Default[0];
                double num2 = base.EMA(macd.Default, 0x15)[0];
                double num3 = num - num2;
                this.Wave1.Set(num3);
                double num4 = macd2.Default[0];
                double num5 = base.EMA(macd2.Default, 0x22)[0];
                double num6 = num4 - num5;
                this.Wave1.Set(num6);
                double num7 = macd3.Default[0];
                double num8 = base.EMA(macd3.Default, 0x37)[0];
                double num9 = num7 - num8;
                this.Wave2.Set(num9);
            }
        }

        public override string ToString()
        {
            return this.IndicatorName;
        }

        [Description("Wave1Color"), NinjaTrader.Gui.Design.DisplayName("c1_Wave1Color"), Category("Display")]
        public Color pWave1Color
        {
            get
            {
                return this.ppWave1Color;
            }
            set
            {
                this.ppWave1Color = value;
            }
        }

        [Browsable(false)]
        public string pWave1ColorSerialize
        {
            get
            {
                return SerializableColor.ToString(this.ppWave1Color);
            }
            set
            {
                this.ppWave1Color = SerializableColor.FromString(value);
            }
        }

        [Description("Wave2Color"), Category("Display"), NinjaTrader.Gui.Design.DisplayName("c2_Wave2Color")]
        public Color pWave2Color
        {
            get
            {
                return this.ppWave2Color;
            }
            set
            {
                this.ppWave2Color = value;
            }
        }

        [Browsable(false)]
        public string pWave2ColorSerialize
        {
            get
            {
                return SerializableColor.ToString(this.ppWave2Color);
            }
            set
            {
                this.ppWave2Color = SerializableColor.FromString(value);
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

        [Browsable(false), XmlIgnore]
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
        private TTMWaveAOC[] cacheTTMWaveAOC = null;

        private static TTMWaveAOC checkTTMWaveAOC = new TTMWaveAOC();

        /// <summary>
        /// VM Wave A Open Code with gapless option
        /// </summary>
        /// <returns></returns>
        public TTMWaveAOC TTMWaveAOC(bool useGapless)
        {
            return TTMWaveAOC(Input, useGapless);
        }

        /// <summary>
        /// VM Wave A Open Code with gapless option
        /// </summary>
        /// <returns></returns>
        public TTMWaveAOC TTMWaveAOC(Data.IDataSeries input, bool useGapless)
        {
            if (cacheTTMWaveAOC != null)
                for (int idx = 0; idx < cacheTTMWaveAOC.Length; idx++)
                    if (cacheTTMWaveAOC[idx].UseGapless == useGapless && cacheTTMWaveAOC[idx].EqualsInput(input))
                        return cacheTTMWaveAOC[idx];

            lock (checkTTMWaveAOC)
            {
                checkTTMWaveAOC.UseGapless = useGapless;
                useGapless = checkTTMWaveAOC.UseGapless;

                if (cacheTTMWaveAOC != null)
                    for (int idx = 0; idx < cacheTTMWaveAOC.Length; idx++)
                        if (cacheTTMWaveAOC[idx].UseGapless == useGapless && cacheTTMWaveAOC[idx].EqualsInput(input))
                            return cacheTTMWaveAOC[idx];

                TTMWaveAOC indicator = new TTMWaveAOC();
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

                TTMWaveAOC[] tmp = new TTMWaveAOC[cacheTTMWaveAOC == null ? 1 : cacheTTMWaveAOC.Length + 1];
                if (cacheTTMWaveAOC != null)
                    cacheTTMWaveAOC.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheTTMWaveAOC = tmp;
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
        public Indicator.TTMWaveAOC TTMWaveAOC(bool useGapless)
        {
            return _indicator.TTMWaveAOC(Input, useGapless);
        }

        /// <summary>
        /// VM Wave A Open Code with gapless option
        /// </summary>
        /// <returns></returns>
        public Indicator.TTMWaveAOC TTMWaveAOC(Data.IDataSeries input, bool useGapless)
        {
            return _indicator.TTMWaveAOC(input, useGapless);
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
        public Indicator.TTMWaveAOC TTMWaveAOC(bool useGapless)
        {
            return _indicator.TTMWaveAOC(Input, useGapless);
        }

        /// <summary>
        /// VM Wave A Open Code with gapless option
        /// </summary>
        /// <returns></returns>
        public Indicator.TTMWaveAOC TTMWaveAOC(Data.IDataSeries input, bool useGapless)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.TTMWaveAOC(input, useGapless);
        }
    }
}
#endregion
