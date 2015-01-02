 #region Using declarations
using System;
using System.Collections ;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using System.Windows.Forms ;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
#endregion

// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    /// <summary>
    /// Label Ray Lines
    /// </summary>
    [Description("Label Ray Lines Alerts")]
    public class LabelRayLineAlerts : Indicator
    {
        #region Variables
        // Wizard generated variables
        private LabelSideType labelSide = LabelSideType.Right;
		private LabelAreaType labelArea = LabelAreaType.Above;
		private float fontSize = 12.0f ;
		
        // User defined variables (add any user defined variables below)
        private Boolean _init = false;
        private StringFormat _format1 = new StringFormat();
        private string _priceFormat;
		private Font largerFont ;
		private bool					triggered					= false;
		private bool					triggerOnGreaterThan		= false;
		private bool					triggerSet					= false;
		private double					price						= 0;
		private bool	enableAlert = false ;
		private string	alertFileName = "Alert4.wav" ;
		private ArrayList lineInfoArray = new ArrayList() ;
		
        #endregion
		
        /// <summary>
        /// This method is used to configure the indicator and is called once before any bar data is loaded.
        /// </summary>
        protected override void Initialize()
        {
			ChartOnly 			= true ;
			AutoScale 			= false ;
			DisplayInDataBox 	= false ;
            Overlay				= true;
            CalculateOnBarClose	= false;
        }
		

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
			int j ;
			String beginMessage = Instrument.FullName + " - Ray Line Price Triggered on " ;
			
			// Determine the Period Value
			switch ( BarsPeriod.Id )
			{
				case PeriodType.Minute:
					beginMessage += BarsPeriod.Value + "M Timeframe @ " ;
					break ;
					
				case PeriodType.Day:
					beginMessage += BarsPeriod.Value + "D Timeframe @ " ;
					break ;
					
				case PeriodType.Week:
					beginMessage += BarsPeriod.Value + "W Timeframe @ " ;
					break ;
					
				case PeriodType.Month:
					beginMessage += BarsPeriod.Value + "MO Timeframe @ " ;
					break ;
					
				case PeriodType.Year:
					beginMessage += BarsPeriod.Value + "Y Timeframe @ " ;
					break ;
					
				case PeriodType.Tick:
					beginMessage += BarsPeriod.Value + "Tick Bar @ " ;
					break ;
					
				case PeriodType.Range:
					beginMessage += BarsPeriod.Value + "Range Bar @ " ;
					break ;
					
				case PeriodType.Volume:
					beginMessage += BarsPeriod.Value + "Volume Bar @ " ;
					break ;
					
			}
			
            // Use this method for calculating your indicator values. Assign a value to each
            // plot below by replacing 'Close[0]' with your own formula.
            if (!_init)
            {
                _init = true;
                int Digits = 0;
				if (TickSize.ToString().StartsWith("1E-"))
				{
					Digits=Convert.ToInt32(TickSize.ToString().Substring(3));
				}
				else if (TickSize.ToString().IndexOf(".")>0)
				{
					Digits=TickSize.ToString().Substring(TickSize.ToString().IndexOf("."),TickSize.ToString().Length-1).Length-1;
				}
                _priceFormat = string.Format("F{0}", Digits);
				
				// Build a larger font size
				largerFont = new Font( ChartControl.Font.Name, fontSize, FontStyle.Bold ) ;
            }    
			
			// Check to see if Alerts are enabled
			if (enableAlert)
			{
				// Step through each one of the lines
				for ( j=0; j < lineInfoArray.Count; j++ )
				{
					LineInfo lineInfo = (LineInfo)lineInfoArray[j] ;
				
					if ( Historical || lineInfo.lineTriggered )
						continue ;
				
					if (( lineInfo.lineGreaterThan && Input[0] >= lineInfo.linePrice - (TickSize * 0.5)) ||
						(!lineInfo.lineGreaterThan && Input[0] <= lineInfo.linePrice + (TickSize * 0.5)))
					{
						
						lineInfo.lineTriggered = true ;
						
						Alert(DateTime.Now.Millisecond.ToString(), NinjaTrader.Cbi.Priority.Medium, beginMessage + lineInfo.linePrice.ToString(_priceFormat), Cbi.Core.InstallDir + @"\sounds\" + alertFileName, 0, Color.Yellow, Color.Black ) ;
												
						lineInfoArray[j] = lineInfo ;
						
					}
				}
			}
        }
		
		// Override the Chart Label to just show if the Alerts are enabled
		public override string ToString()
		{
			return Name + "(" + (enableAlert ? "Alerts ON" : "Alerts OFF") + ")" ;
		}
		
        public override void Plot(Graphics graphics, Rectangle bounds, double min, double max)
        {
			int i;
			ArrayList validTagIds = new ArrayList() ;
			
			// Define the SizeF Class
			SizeF stringSize = new SizeF() ;
			
			foreach (ChartObject co in ChartControl.ChartObjects)
            {
                if (co is ChartRay)
                {
                    
                    ChartRay l1 = (co as ChartRay);

						int x2 = bounds.Width;
						_format1.Alignment = StringAlignment.Far;
						
						if (labelSide != LabelSideType.Right)
						{	
                        	x2 = 0;	
							_format1.Alignment = StringAlignment.Near;
						}		
						
						if (enableAlert)
						{
							// Add the Tag Id to the valid TagId Array List so we can clean up
							// the alert ArrayList
							validTagIds.Add( l1.Tag ) ;
							
							bool existLI = false ;
							LineInfo lineInfo = new LineInfo() ;
						
							// Put the info in the array
							for( i=0; i<lineInfoArray.Count; i++ )
							{
								// Get the lineInfo from the Array List
								lineInfo = (LineInfo)lineInfoArray[i] ;
							
								// Check to see if the tag id already exists in the arraylist
								if ( lineInfo.lineTag == l1.Tag )
								{
									existLI = true ;
									break ;
								}
							}
						
							if ( !existLI )
							{
								LineInfo newLineInfo = new LineInfo() ;
								newLineInfo.lineTag = l1.Tag ;
								newLineInfo.linePrice = l1.EndY ;
								newLineInfo.lineGreaterThan = ( Input[0] >= newLineInfo.linePrice - (TickSize * 0.5 ) ? false : true ) ;
								newLineInfo.lineTriggered = false ;
							
								lineInfoArray.Add( newLineInfo ) ;
							}
							else
							{
								if ( lineInfo.linePrice != l1.EndY )
								{
									lineInfo.linePrice = l1.EndY ;
									lineInfo.lineGreaterThan = ( Input[0] >= lineInfo.linePrice - (TickSize * 0.5) ? false : true ) ;
									lineInfo.lineTriggered = false ;
								
									lineInfoArray[i] = lineInfo ;
								}
							}
						}
						
						double y1 = l1.EndY;
                        
                        int y2 = (bounds.Y + bounds.Height) - ((int)(((y1 - min) / ChartControl.MaxMinusMin(max, min)) * bounds.Height));
						
						string s = l1.EndY.ToString(_priceFormat);
						stringSize = graphics.MeasureString( s, largerFont ) ;
                        graphics.DrawString(s, largerFont, l1.Pen.Brush, x2, (y2 - ( labelArea == LabelAreaType.Above ? stringSize.Height : 0 )), _format1);
			
                }
            }
			
			// Clean up the lineInfoArray to remove lines that have been deleted
			if ( enableAlert ) 
			{
				ArrayList removeTagIds = new ArrayList() ;
				
				foreach( LineInfo li in lineInfoArray )
				{
					// Check to see if Line Id is still in the valid list of Lines
					if (( validTagIds.IndexOf( li.lineTag )) == -1 )
						// Add it to the ArrayList to remove 
						removeTagIds.Add(li) ;
						
				}
				
				foreach( LineInfo li in removeTagIds )
				{
					// Remove the LineInfo from the LineInfoArray
					lineInfoArray.Remove(li) ;
				}
			}
        }

        #region Properties

        [Description("Display the Label on the Right or Left side of the line")]
        [GridCategory("Parameters")]
		[Gui.Design.DisplayNameAttribute("Label Side")]
		public LabelSideType LabelSide
        {
            get { return labelSide; }
            set { labelSide = value; }
        }
		[Description("Display the label Above or Below the line")]
		[GridCategory("Parameters")]
		[Gui.Design.DisplayNameAttribute("Label Above/Below")]
		public LabelAreaType LabelArea
		{
			get { return labelArea; }
			set { labelArea = value ; }
		}
		[Description("Set the size of the display font")]
		[GridCategory("Parameters")]
		[Gui.Design.DisplayNameAttribute("Font Size")]
		public float FontSize
		{
			get { return fontSize; }
			set { fontSize = value; }
		}
		[Browsable(true)]
		[Gui.Design.DisplayNameAttribute("Enable Alerts")]
		[GridCategory("Parameters")]
		public bool EnableAlert
		{
			get { return enableAlert; }
			set { enableAlert = value; }
		}
		
		[Gui.Design.DisplayNameAttribute("Alert File Name")]
		[GridCategory("Parameters")]
		[Description("Enter the Filename of the Alert wav file to use. It must be stored in the standard sounds directory for NT7")]
		public string AlertFileName
		{
			get { return alertFileName; }
			set { alertFileName = value; }
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
        private LabelRayLineAlerts[] cacheLabelRayLineAlerts = null;

        private static LabelRayLineAlerts checkLabelRayLineAlerts = new LabelRayLineAlerts();

        /// <summary>
        /// Label Ray Lines Alerts
        /// </summary>
        /// <returns></returns>
        public LabelRayLineAlerts LabelRayLineAlerts(string alertFileName, bool enableAlert, float fontSize, LabelAreaType labelArea, LabelSideType labelSide)
        {
            return LabelRayLineAlerts(Input, alertFileName, enableAlert, fontSize, labelArea, labelSide);
        }

        /// <summary>
        /// Label Ray Lines Alerts
        /// </summary>
        /// <returns></returns>
        public LabelRayLineAlerts LabelRayLineAlerts(Data.IDataSeries input, string alertFileName, bool enableAlert, float fontSize, LabelAreaType labelArea, LabelSideType labelSide)
        {
            if (cacheLabelRayLineAlerts != null)
                for (int idx = 0; idx < cacheLabelRayLineAlerts.Length; idx++)
                    if (cacheLabelRayLineAlerts[idx].AlertFileName == alertFileName && cacheLabelRayLineAlerts[idx].EnableAlert == enableAlert && cacheLabelRayLineAlerts[idx].FontSize == fontSize && cacheLabelRayLineAlerts[idx].LabelArea == labelArea && cacheLabelRayLineAlerts[idx].LabelSide == labelSide && cacheLabelRayLineAlerts[idx].EqualsInput(input))
                        return cacheLabelRayLineAlerts[idx];

            lock (checkLabelRayLineAlerts)
            {
                checkLabelRayLineAlerts.AlertFileName = alertFileName;
                alertFileName = checkLabelRayLineAlerts.AlertFileName;
                checkLabelRayLineAlerts.EnableAlert = enableAlert;
                enableAlert = checkLabelRayLineAlerts.EnableAlert;
                checkLabelRayLineAlerts.FontSize = fontSize;
                fontSize = checkLabelRayLineAlerts.FontSize;
                checkLabelRayLineAlerts.LabelArea = labelArea;
                labelArea = checkLabelRayLineAlerts.LabelArea;
                checkLabelRayLineAlerts.LabelSide = labelSide;
                labelSide = checkLabelRayLineAlerts.LabelSide;

                if (cacheLabelRayLineAlerts != null)
                    for (int idx = 0; idx < cacheLabelRayLineAlerts.Length; idx++)
                        if (cacheLabelRayLineAlerts[idx].AlertFileName == alertFileName && cacheLabelRayLineAlerts[idx].EnableAlert == enableAlert && cacheLabelRayLineAlerts[idx].FontSize == fontSize && cacheLabelRayLineAlerts[idx].LabelArea == labelArea && cacheLabelRayLineAlerts[idx].LabelSide == labelSide && cacheLabelRayLineAlerts[idx].EqualsInput(input))
                            return cacheLabelRayLineAlerts[idx];

                LabelRayLineAlerts indicator = new LabelRayLineAlerts();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.AlertFileName = alertFileName;
                indicator.EnableAlert = enableAlert;
                indicator.FontSize = fontSize;
                indicator.LabelArea = labelArea;
                indicator.LabelSide = labelSide;
                Indicators.Add(indicator);
                indicator.SetUp();

                LabelRayLineAlerts[] tmp = new LabelRayLineAlerts[cacheLabelRayLineAlerts == null ? 1 : cacheLabelRayLineAlerts.Length + 1];
                if (cacheLabelRayLineAlerts != null)
                    cacheLabelRayLineAlerts.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheLabelRayLineAlerts = tmp;
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
        /// Label Ray Lines Alerts
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.LabelRayLineAlerts LabelRayLineAlerts(string alertFileName, bool enableAlert, float fontSize, LabelAreaType labelArea, LabelSideType labelSide)
        {
            return _indicator.LabelRayLineAlerts(Input, alertFileName, enableAlert, fontSize, labelArea, labelSide);
        }

        /// <summary>
        /// Label Ray Lines Alerts
        /// </summary>
        /// <returns></returns>
        public Indicator.LabelRayLineAlerts LabelRayLineAlerts(Data.IDataSeries input, string alertFileName, bool enableAlert, float fontSize, LabelAreaType labelArea, LabelSideType labelSide)
        {
            return _indicator.LabelRayLineAlerts(input, alertFileName, enableAlert, fontSize, labelArea, labelSide);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// Label Ray Lines Alerts
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.LabelRayLineAlerts LabelRayLineAlerts(string alertFileName, bool enableAlert, float fontSize, LabelAreaType labelArea, LabelSideType labelSide)
        {
            return _indicator.LabelRayLineAlerts(Input, alertFileName, enableAlert, fontSize, labelArea, labelSide);
        }

        /// <summary>
        /// Label Ray Lines Alerts
        /// </summary>
        /// <returns></returns>
        public Indicator.LabelRayLineAlerts LabelRayLineAlerts(Data.IDataSeries input, string alertFileName, bool enableAlert, float fontSize, LabelAreaType labelArea, LabelSideType labelSide)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.LabelRayLineAlerts(input, alertFileName, enableAlert, fontSize, labelArea, labelSide);
        }
    }
}
#endregion
