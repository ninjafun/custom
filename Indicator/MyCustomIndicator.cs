// 
// 	v 1.1 by Osikani, koganam@nc.rr.com November 13, 2008
//
//=====================================================================================================
//	This indicator is based on the shipped version of the HeikenAshi indicator in Ninja Trader 6.5.1000.7
//	
//	1.	It has been recoded to reduce resource use by optionally repainting only bars that are visible 
//		in the viewing canvas.
//	2.	The indicator will use your defined bar colors for the Heiken Ashi bodies.
//	3.	The HeikenAshi bar shadows are in the color of the action of the actual underlying equity. 
//		This allows you to see the underlying movement of the stock independently of how the HeikenAshi
//		bars behave. This may be able to give you advance warning of a reversal or breakout.
//	4.	This version has been smoothed to better filter out noise that is present even in the HeikenAshi chart.
//		It has been smoothed by using the Hull Moving Average, as I have found that this average, at low periods,
//		has very little lag to what it is tracking.
//
//  Enjoy this NinjaScript v6.5.1000.7 indicator!  May it help bring you great profits!
//
//  If you have profited from this code, and only if you wish, I will be glad to accept a donation of
//	any amount. Contact information is given below. You are NOT in any manner obliged to donate anything
//	to me for using this indicator.
//
//  Feel free to email enhancement suggestions to:
//
//			Osikani, koganam@nc.rr.com
//
//	Modifications:
//
//	v1.1
//  =======
//	1. Allows one to select the manner in which HeikenAshi bars will be painted.
//	2. CalculateOnBarClose set as "false" should now paint bars properly.
//
//=====================================================================================================

#region Using declarations
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Xml.Serialization;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
#endregion

// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    /// <summary>
	/// HeikenAshi technique discussed in the article 'Using Heiken-Ashi Technique' in February 2004 issue of TASC magazine.
    /// </summary>
    [Description("HeikenAshi technique discussed in the article 'Using Heiken-Ashi Technique' in February 2004 issue of TASC magazine. "
	+ "This version has been smoothed to better filter out noise that is present even in the HeikenAshi chart.")]
	public class HeikenAshi_Smoothed : Indicator
    {
        #region Variables
		private Color	barColorUp 		= Color.Empty;
		private Color	barColorDown 	= Color.Empty;
		private Color	saveDownColor	= Color.Empty;
//		private Pen		savePen			= null;
		private Color	saveUpColor		= Color.Empty;
		private Color	shadowColor 	= Color.Empty;
		private int		shadowWidth		= 2;
		private int		smoothingPeriod = 4;
		private bool	initialized		= false;

		public enum PaintingStyle
			{
			PaintVisibleOnly,
			PaintToLast,
			PaintAll
			}		
		
		PaintingStyle 		PaintStyle = PaintingStyle.PaintVisibleOnly;	
		
		#endregion

        /// <summary>
        /// This method is used to configure the indicator and is called once before any bar data is loaded.
        /// </summary>
        protected override void Initialize()
        {
			Add(new Plot(Color.Transparent, PlotStyle.Line, "HAOpen"));
			Add(new Plot(Color.Transparent, PlotStyle.Line, "HAHigh"));
			Add(new Plot(Color.Transparent, PlotStyle.Line, "HALow"));
			Add(new Plot(Color.Transparent, PlotStyle.Line, "HAClose"));

			ChartOnly			= true;
			PlotsConfigurable 	= false;
			PaintPriceMarkers 	= false;
			Overlay				= true;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {	
		if (!initialized)
			{if (ChartControl != null && saveDownColor == Color.Empty && ChartControl.ChartStyle.DownColor != Color.Transparent)
				{
					saveDownColor	= ChartControl.ChartStyle.DownColor;
					saveUpColor		= ChartControl.ChartStyle.UpColor;
//					savePen			= ChartControl.ChartStyle.Pen;
					
					// Use the defined chart colors
					barColorDown	= ChartControl.ChartStyle.DownColor;
					barColorUp		= ChartControl.ChartStyle.UpColor;
	
					// make normal bars invisible
					ChartControl.ChartStyle.DownColor	= Color.Transparent; 
					ChartControl.ChartStyle.UpColor		= Color.Transparent; 
					ChartControl.ChartStyle.Pen.Color	= Color.Transparent;
					//ChartControl.ChartStyle.Pen			= new Pen(Color.Transparent); 
				}
			initialized = true;
			}
			
			if (CurrentBar == 0)
			{
				HAOpen.Set(0);
				HAHigh.Set(0);
				HALow.Set(0);
				HAClose.Set(0);
				return;
			}
			
			// Draw HeikenAshi bars as specified by user

			int lastBar		= Math.Min(ChartControl.LastBarPainted, Bars.Count-1);
			int firstBar	= (lastBar - ChartControl.BarsPainted) + 1;
			
			switch (PaintStyle) 
			{
				case PaintingStyle.PaintVisibleOnly:
					if (CurrentBar <= firstBar + 2) return;
					if (CurrentBar >= lastBar + 1) return;
					break;
					
				case PaintingStyle.PaintToLast:
					if (CurrentBar <= firstBar + 2) return;
					break;
					
				case PaintingStyle.PaintAll:
//					
//					break;
//					
				default:
					
					break;
			}
			
			HAClose.Set((HMA(Open, smoothingPeriod)[0] + HMA(High, smoothingPeriod)[0] + HMA(Low, smoothingPeriod)[0] + HMA(Close, smoothingPeriod)[0]) / 4); // Calculate the close
			HAOpen.Set((HMA(HAOpen, smoothingPeriod)[1] + HMA(HAClose, smoothingPeriod)[1]) / 2); // Calculate the open
			HAHigh.Set(Math.Max(HMA(High, smoothingPeriod)[0], HMA(HAOpen, smoothingPeriod)[0])); // Calculate the high
			HALow.Set(Math.Min(HMA(Low, smoothingPeriod)[0], HMA(HAOpen, smoothingPeriod)[0])); // Calculate the low

			Color barColor = (HAClose[0] > HAOpen[0] ? BarColorUp : BarColorDown);
			Color ShadowColor = (Close[0] > Open[0] ? BarColorUp : BarColorDown);
			
			int BodyWidth = Math.Max(ChartControl.BarWidth+2,ChartControl.BarSpace-3);
			
			DrawLine(CurrentBar.ToString(), false, 0, HAHigh[0], 0, HALow[0], ShadowColor, DashStyle.Solid, ShadowWidth);
			DrawLine(CurrentBar.ToString() + "OC", false, 0, HAOpen[0], 0, HAClose[0], barColor, DashStyle.Solid, BodyWidth);
		}

        #region Properties
		/// <summary>
		/// </summary>
		[XmlIgnore()]
		[Description("Color of down bars.")]
		[Category("Visual")]
		[Gui.Design.DisplayNameAttribute("Down color")]
		public Color BarColorDown
		{
			get { return barColorDown; }
			set { barColorDown = value; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		public string BarColorDownSerialize
		{
			get { return NinjaTrader.Gui.Design.SerializableColor.ToString(barColorDown); }
			set { barColorDown = NinjaTrader.Gui.Design.SerializableColor.FromString(value); }
		}
		
		/// <summary>
		/// </summary>
		[XmlIgnore()]
		[Description("Color of up bars.")]
		[Category("Visual")]
		[Gui.Design.DisplayNameAttribute("Up color")]
		public Color BarColorUp
		{
			get { return barColorUp; }
			set { barColorUp = value; }
		}

		/// <summary>
		/// </summary>
		[Browsable(false)]
		public string BarColorUpSerialize
		{
			get { return NinjaTrader.Gui.Design.SerializableColor.ToString(barColorUp); }
			set { barColorUp = NinjaTrader.Gui.Design.SerializableColor.FromString(value); }
		}
		
		/// <summary>
		/// </summary>
		[XmlIgnore()]
		[Description("Color of shadow line.")]
		[Category("Visual")]
		[Gui.Design.DisplayNameAttribute("Shadow color")]
		public Color ShadowColor
		{
			get { return shadowColor; }
			set { shadowColor = value; }
		}

		/// <summary>
		/// </summary>
		[Browsable(false)]
		public string ShadowColorSerialize
		{
			get { return NinjaTrader.Gui.Design.SerializableColor.ToString(shadowColor); }
			set { shadowColor = NinjaTrader.Gui.Design.SerializableColor.FromString(value); }
		}
		
		/// <summary>
		/// </summary>
		[Description("Width of shadow line.")]
		[Category("Visual")]
		[Gui.Design.DisplayNameAttribute("Shadow width")]
		public int ShadowWidth
		{
			get { return shadowWidth; }
			set { shadowWidth = Math.Max(value, 1); }
		}
		
        [Description("Period for smoothing the indicator.")]
        [Category("Parameters")]
		[Gui.Design.DisplayName("Smoothing period")]
        public int SmoothingPeriod
        {
            get { return smoothingPeriod; }
            set { smoothingPeriod = Math.Max(0, value); }
        }

		[Description("Datermines how HeikenAshi bars are drawn.\r\n\r\n" + 
			"PaintVisibleOnly: repaints only the bars that are visible in the viewing canvas.\r\n\r\n" +
			"PaintToLast: repaints all bars from the first visible bar on the canvas to the last bar on the chart.\r\n\r\n" + 
			"PaintAll: repaints all bars every time. This is no different from the shipped indicator, " +
			"and so places the same load on the processor. The only difference is in the color style and smoothing.")]
        [Category("Parameters")]
		[Gui.Design.DisplayName("Painting Style")]
		public PaintingStyle paintStyle
		{
			get { return PaintStyle; }
			set { PaintStyle = value; }
		}	
		

		/// <summary>
		/// Gets the HeikenAshi_Smoothed Open value.
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public DataSeries HAOpen
		{
			get { return Values[0]; }
		}
				
		/// <summary>
		/// Gets the HeikenAshi_Smoothed High value.
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public DataSeries HAHigh
		{
			get { return Values[1]; }
		}

		/// <summary>
		/// Gets the HeikenAshi_Smoothed Low value.
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public DataSeries HALow
		{
			get { return Values[2]; }
		}

		/// <summary>
		/// Gets the HeikenAshi_Smoothed Close value.
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public DataSeries HAClose
		{
			get { return Values[3]; }
		}
        #endregion

        #region Miscellaneous
        public override void Dispose()
        {
            if (ChartControl != null && saveDownColor != Color.Empty && ChartControl.ChartStyle.DownColor == Color.Transparent)
            {
				ChartControl.ChartStyle.DownColor	= saveDownColor;
				ChartControl.ChartStyle.UpColor		= saveUpColor;
				//ChartControl.ChartStyle.Pen			= savePen;
            }
            base.Dispose();
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
        private HeikenAshi_Smoothed[] cacheHeikenAshi_Smoothed = null;

        private static HeikenAshi_Smoothed checkHeikenAshi_Smoothed = new HeikenAshi_Smoothed();

        /// <summary>
        /// HeikenAshi_Smoothed technique discussed in the article 'Using Heiken-Ashi Technique' in February 2004 issue of TASC magazine.
        /// </summary>
        /// <returns></returns>
        public HeikenAshi_Smoothed HeikenAshi_Smoothed()
        {
            return HeikenAshi_Smoothed(Input);
        }

        /// <summary>
        /// HeikenAshi_Smoothed technique discussed in the article 'Using Heiken-Ashi Technique' in February 2004 issue of TASC magazine.
        /// </summary>
        /// <returns></returns>
        public HeikenAshi_Smoothed HeikenAshi_Smoothed(Data.IDataSeries input)
        {

            if (cacheHeikenAshi_Smoothed != null)
                for (int idx = 0; idx < cacheHeikenAshi_Smoothed.Length; idx++)
                    if (cacheHeikenAshi_Smoothed[idx].EqualsInput(input))
                        return cacheHeikenAshi_Smoothed[idx];

            HeikenAshi_Smoothed indicator = new HeikenAshi_Smoothed();
            indicator.BarsRequired = BarsRequired;
            indicator.CalculateOnBarClose = CalculateOnBarClose;
            indicator.Input = input;
            indicator.SetUp();

            HeikenAshi_Smoothed[] tmp = new HeikenAshi_Smoothed[cacheHeikenAshi_Smoothed == null ? 1 : cacheHeikenAshi_Smoothed.Length + 1];
            if (cacheHeikenAshi_Smoothed != null)
                cacheHeikenAshi_Smoothed.CopyTo(tmp, 0);
            tmp[tmp.Length - 1] = indicator;
            cacheHeikenAshi_Smoothed = tmp;
            Indicators.Add(indicator);

            return indicator;
        }

    }
}

// This namespace holds all market analyzer column definitions and is required. Do not change it.
namespace NinjaTrader.MarketAnalyzer
{
    public partial class Column : ColumnBase
    {
        /// <summary>
        /// HeikenAshi_Smoothed technique discussed in the article 'Using Heiken-Ashi Technique' in February 2004 issue of TASC magazine.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.HeikenAshi_Smoothed HeikenAshi_Smoothed()
        {
            return _indicator.HeikenAshi_Smoothed(Input);
        }

        /// <summary>
        /// HeikenAshi_Smoothed technique discussed in the article 'Using Heiken-Ashi Technique' in February 2004 issue of TASC magazine.
        /// </summary>
        /// <returns></returns>
        public Indicator.HeikenAshi_Smoothed HeikenAshi_Smoothed(Data.IDataSeries input)
        {
            return _indicator.HeikenAshi_Smoothed(input);
        }

    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// HeikenAshi_Smoothed technique discussed in the article 'Using Heiken-Ashi Technique' in February 2004 issue of TASC magazine.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.HeikenAshi_Smoothed HeikenAshi_Smoothed()
        {
            return _indicator.HeikenAshi_Smoothed(Input);
        }

        /// <summary>
        /// HeikenAshi_Smoothed technique discussed in the article 'Using Heiken-Ashi Technique' in February 2004 issue of TASC magazine.
        /// </summary>
        /// <returns></returns>
        public Indicator.HeikenAshi_Smoothed HeikenAshi_Smoothed(Data.IDataSeries input)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.HeikenAshi_Smoothed(input);
        }

    }
}
#endregion
