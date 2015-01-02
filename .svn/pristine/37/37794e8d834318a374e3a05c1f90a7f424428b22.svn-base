#region Using declarations
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
#endregion

// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    /// <summary>
    /// Enter the description of your new custom indicator here
    /// </summary>
    [Description("This indicator will alert crossovers for manually drawn lines. Alerts are reset with each new bar or when ManualReset input set to true. ")]
    public class LineAlert : Indicator
    {
        #region Variables
		private double 	lineLength;
		private double 	lineSlope;
		private double 	leftY;
		private double 	rightY;
		private double 	leftBarsAgo;
		private double 	rightBarsAgo;
		private double 	lineValueAtLastBar;
		
		//User inputs and default values here. 
		private bool	manualReset 		= false;
		private bool 	changeLineColor		= true;
		private Color 	lineColor 			= Color.Red;
		private string 	alertMessage		= "Price Crossed line here";
		private string 	soundFile			= "Alert2.wav";
		
        #endregion

        /// <summary>
        /// This method is used to configure the indicator and is called once before any bar data is loaded.
        /// </summary>
        protected override void Initialize()
        {			
			Overlay = true;
			CalculateOnBarClose = false;
        }
		
		public override string ToString()
		{
			return Name;
		}


        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {	
			if (Historical) return;
			
			RemoveDrawObject("noAlert");
			
			//resets alert each new bar. 
			if (FirstTickOfBar)
				ResetAlert("AlertLineCrossing");
					
			//Manual Alert reset. 
			if (ManualReset)
			{
				ResetAlert("AlertLineCrossing");
				ManualReset = false;
			}
			
			foreach (IDrawObject draw in DrawObjects)
			{
				
				if (draw.UserDrawn &&
					(draw.DrawType == DrawType.Line || draw.DrawType == DrawType.Ray || draw.DrawType == DrawType.ExtendedLine || draw.DrawType == DrawType.HorizontalLine))
				{	//Sets some line properties programatically
					ILine globalLine 		= (ILine) draw;
					globalLine.Locked 		= false;
					globalLine.Pen.Width	= 2;
					
					
					if (ChangeLineColor)
						globalLine.Pen.Color	= LineColor;

					//Sets right and left points depending on how line is drawn.
					if (globalLine.StartBarsAgo > globalLine.EndBarsAgo) 
					{	
						leftY 			= globalLine.StartY;
						rightY 	  		= globalLine.EndY;
						leftBarsAgo		= globalLine.StartBarsAgo;
						rightBarsAgo	= globalLine.EndBarsAgo;
					}
					
					else if (globalLine.StartBarsAgo < globalLine.EndBarsAgo)
					{	
						leftY 			= globalLine.EndY;
						rightY			= globalLine.StartY;
						leftBarsAgo		= globalLine.EndBarsAgo;
						rightBarsAgo	= globalLine.StartBarsAgo;
					}
					
					//Sets lineLength based on lines position relative to most recent updated bar
					if (rightBarsAgo <= 0 && leftBarsAgo >= 0) //most Likely Alert scenario here
						lineLength = leftBarsAgo + Math.Abs(rightBarsAgo);
					
					else if (leftBarsAgo < 0 && rightBarsAgo < 0) //Alert possible --eventually, but no crossing is available yet
						lineLength = Math.Abs(rightBarsAgo) - Math.Abs(leftBarsAgo);
					
					else if (leftBarsAgo > 0 && rightBarsAgo > 0) //no alert case.
						lineLength = Math.Abs(rightBarsAgo - leftBarsAgo);
								
					lineSlope = ((rightY - leftY) / lineLength); //Sets slope
					
					if(draw.DrawType == DrawType.HorizontalLine)
						lineValueAtLastBar = globalLine.EndY;
									
					else if (leftBarsAgo == 0)
						lineValueAtLastBar = leftY;
					
					else if (leftBarsAgo > 0)
						lineValueAtLastBar = leftY + leftBarsAgo * lineSlope;
					
					//No Alert handling here. 					
					if ((leftBarsAgo < 0 && rightBarsAgo < 0) || (leftBarsAgo > 0 && rightBarsAgo > 0) && draw.DrawType != DrawType.HorizontalLine )
						DrawTextFixed("noAlert", "Check start & end points for your line(s). 1 or more is not eligible for an alert. ", TextPosition.TopRight);
					
					else if (leftBarsAgo == rightBarsAgo && draw.DrawType != DrawType.HorizontalLine)
						DrawTextFixed("noAlert", "Your line(s) is not eligible for an alert. No alerts for vertical lines", TextPosition.TopRight);
	
					else
					{			
						//Alert Checking is done here. Can add your own actions to the block if desired. 
						if(CrossBelow(Close, lineValueAtLastBar, 1) || CrossAbove(Close, lineValueAtLastBar, 1))
						{
							Alert("AlertLineCrossing", Cbi.Priority.High, alertMessage, soundFile, 0, Color.White, Color.Black);  
						}
					}
				} 
			}
			
		}

        #region Properties
		[Description("Set this value to true to manually reset the alert. ")]
        [GridCategory("Parameters")]
        public bool ManualReset
        {
            get { return manualReset; }
            set { manualReset = value; }
        }
		[Description("Allows the script to change color of manually placed lines.")]
        [GridCategory("Parameters")]
        public bool ChangeLineColor
        {
            get { return changeLineColor; }
            set { changeLineColor = value; }
        }
		[XmlIgnore()]
		[Description("Sets the color for all manually drawn lines and rays. ")]
		[GridCategory("Parameters")]
		public Color LineColor
		{
    	 	get { return lineColor; }
     		set { lineColor = value; }
		}
		
		[Browsable(false)]
		public string lineColorSerialize
		{
			get { return NinjaTrader.Gui.Design.SerializableColor.ToString(lineColor); }
			set { lineColor = NinjaTrader.Gui.Design.SerializableColor.FromString(value); }
		}
		
		[Description("The message that appears in File > new > Alerts window")]
        [GridCategory("Parameters")]
        public string AlertMessage
        {
            get { return alertMessage; }
            set { alertMessage = value; }
        }
		
		[Description("The name of the sound file. NT will look for this file in \\Program Files\\NinjaTrader\\sounds")]
        [GridCategory("Parameters")]
        public string SoundFile
        {
            get { return soundFile; }
            set { soundFile = value; }
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
        private LineAlert[] cacheLineAlert = null;

        private static LineAlert checkLineAlert = new LineAlert();

        /// <summary>
        /// This indicator will alert crossovers for manually drawn lines. Alerts are reset with each new bar or when ManualReset input set to true. 
        /// </summary>
        /// <returns></returns>
        public LineAlert LineAlert(string alertMessage, bool changeLineColor, Color lineColor, bool manualReset, string soundFile)
        {
            return LineAlert(Input, alertMessage, changeLineColor, lineColor, manualReset, soundFile);
        }

        /// <summary>
        /// This indicator will alert crossovers for manually drawn lines. Alerts are reset with each new bar or when ManualReset input set to true. 
        /// </summary>
        /// <returns></returns>
        public LineAlert LineAlert(Data.IDataSeries input, string alertMessage, bool changeLineColor, Color lineColor, bool manualReset, string soundFile)
        {
            if (cacheLineAlert != null)
                for (int idx = 0; idx < cacheLineAlert.Length; idx++)
                    if (cacheLineAlert[idx].AlertMessage == alertMessage && cacheLineAlert[idx].ChangeLineColor == changeLineColor && cacheLineAlert[idx].LineColor == lineColor && cacheLineAlert[idx].ManualReset == manualReset && cacheLineAlert[idx].SoundFile == soundFile && cacheLineAlert[idx].EqualsInput(input))
                        return cacheLineAlert[idx];

            lock (checkLineAlert)
            {
                checkLineAlert.AlertMessage = alertMessage;
                alertMessage = checkLineAlert.AlertMessage;
                checkLineAlert.ChangeLineColor = changeLineColor;
                changeLineColor = checkLineAlert.ChangeLineColor;
                checkLineAlert.LineColor = lineColor;
                lineColor = checkLineAlert.LineColor;
                checkLineAlert.ManualReset = manualReset;
                manualReset = checkLineAlert.ManualReset;
                checkLineAlert.SoundFile = soundFile;
                soundFile = checkLineAlert.SoundFile;

                if (cacheLineAlert != null)
                    for (int idx = 0; idx < cacheLineAlert.Length; idx++)
                        if (cacheLineAlert[idx].AlertMessage == alertMessage && cacheLineAlert[idx].ChangeLineColor == changeLineColor && cacheLineAlert[idx].LineColor == lineColor && cacheLineAlert[idx].ManualReset == manualReset && cacheLineAlert[idx].SoundFile == soundFile && cacheLineAlert[idx].EqualsInput(input))
                            return cacheLineAlert[idx];

                LineAlert indicator = new LineAlert();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.AlertMessage = alertMessage;
                indicator.ChangeLineColor = changeLineColor;
                indicator.LineColor = lineColor;
                indicator.ManualReset = manualReset;
                indicator.SoundFile = soundFile;
                Indicators.Add(indicator);
                indicator.SetUp();

                LineAlert[] tmp = new LineAlert[cacheLineAlert == null ? 1 : cacheLineAlert.Length + 1];
                if (cacheLineAlert != null)
                    cacheLineAlert.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheLineAlert = tmp;
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
        /// This indicator will alert crossovers for manually drawn lines. Alerts are reset with each new bar or when ManualReset input set to true. 
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.LineAlert LineAlert(string alertMessage, bool changeLineColor, Color lineColor, bool manualReset, string soundFile)
        {
            return _indicator.LineAlert(Input, alertMessage, changeLineColor, lineColor, manualReset, soundFile);
        }

        /// <summary>
        /// This indicator will alert crossovers for manually drawn lines. Alerts are reset with each new bar or when ManualReset input set to true. 
        /// </summary>
        /// <returns></returns>
        public Indicator.LineAlert LineAlert(Data.IDataSeries input, string alertMessage, bool changeLineColor, Color lineColor, bool manualReset, string soundFile)
        {
            return _indicator.LineAlert(input, alertMessage, changeLineColor, lineColor, manualReset, soundFile);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// This indicator will alert crossovers for manually drawn lines. Alerts are reset with each new bar or when ManualReset input set to true. 
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.LineAlert LineAlert(string alertMessage, bool changeLineColor, Color lineColor, bool manualReset, string soundFile)
        {
            return _indicator.LineAlert(Input, alertMessage, changeLineColor, lineColor, manualReset, soundFile);
        }

        /// <summary>
        /// This indicator will alert crossovers for manually drawn lines. Alerts are reset with each new bar or when ManualReset input set to true. 
        /// </summary>
        /// <returns></returns>
        public Indicator.LineAlert LineAlert(Data.IDataSeries input, string alertMessage, bool changeLineColor, Color lineColor, bool manualReset, string soundFile)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.LineAlert(input, alertMessage, changeLineColor, lineColor, manualReset, soundFile);
        }
    }
}
#endregion
