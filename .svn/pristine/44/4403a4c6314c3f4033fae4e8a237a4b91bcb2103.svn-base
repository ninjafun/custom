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
	/// Daily Session Fibonacci Retracements
	/// </summary>
	[Description("anaFibonacciClusterV14L.")]
	public class anaFibonacciClusterV14L : Indicator
	{
		#region Variables
		private	SolidBrush[]		brushes					= { new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black), 
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black), 
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black), 
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black), 
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black), 
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),	
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black), 
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),	
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black), 
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black), 
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),	
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black), 
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),	
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black), 
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),  
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black),
															new SolidBrush(Color.Black), new SolidBrush(Color.Black), new SolidBrush(Color.Black)}; 

		private DateTime				currentDate				= Cbi.Globals.MinDate;
		private DateTime  				sessionBegin			= Cbi.Globals.MinDate;
		private DateTime  				sessionEnd				= Cbi.Globals.MinDate;
		private TimeSpan				offset					= new TimeSpan(0,0,0);
		private TimeSpan				sessionLength			= new TimeSpan(0,0,0);
		private	double					currentHigh				= double.MinValue;
		private	double					currentLow				= double.MaxValue;
		private double					currentClose			= 0;
		private double					currentLowAfterHigh		= double. MaxValue;
		private double					currentHighAfterLow		= double.MinValue;
		private SolidBrush				textBrush				= new SolidBrush(Color.Red);
		private Font					textFont				= new Font("Arial", 12);
		private string					errorData				= "Please increase chart lookback period to match lookback period of Fibonacci indicator.";
		private bool					dailyChart				= false;
		private bool					firstPeriod				= true;
		private bool					plotPivots				= false;
		private bool					periodOpen				= false;
		private int						sessionNumber			= 0;
		private	StringFormat			stringFormatFar			= new StringFormat();
		private	StringFormat			stringFormatNear		= new StringFormat();
		private	StringFormat			stringFormatCenter		= new StringFormat();
		private int						width					= 15;
		private int						labelPosition			= 10;
		private int						lookBack				= 100;
		private int						filter					= 85;
		private int						shortperiod				= 0;
		private int						longperiod				= 0;
		private double[]				priorHigh				= new double[5];
		private double[]				priorLow				= new double[5];
		private double[]				priorClose				= new double[5];
		private double					priorLowAfterHigh		= double.MinValue;
		private double					priorHighAfterLow		= double.MaxValue;
		private DateTime				currentHighTime			= Cbi.Globals.MinDate;
		private DateTime				currentLowTime			= Cbi.Globals.MinDate;
		private DateTime				currentLowAfterHighTime	= Cbi.Globals.MinDate;
		private DateTime				currentHighAfterLowTime	= Cbi.Globals.MinDate;
		private DateTime[]				priorHighTime			= new DateTime[5];
		private DateTime[]				priorLowTime			= new DateTime[5];
		private DateTime				priorLowAfterHighTime	= Cbi.Globals.MinDate;
		private DateTime				priorHighAfterLowTime	= Cbi.Globals.MinDate;
		private double					newPrimaryHigh			= double.MaxValue;
		private double					newSecondaryHigh		= double.MaxValue;
		private double					newPrimaryLow			= double.MinValue;
		private double					newSecondaryLow			= double.MinValue;
		private bool					existsNewPrimaryHigh	= false;
		private bool					existsNewSecondaryHigh	= false;
		private bool					existsNewPrimaryLow		= false;
		private bool					existsNewSecondaryLow	= false;
		private bool					existsOldSecondaryHigh	= false;
		private bool					existsOldSecondaryLow	= false;
		private int						swingIndex				= -1;
		private int						rIndex					= 0;
		private double[]				swingHigh				= new double[10];
		private double[]				swingLow				= new double[10];
		private DateTime[]				swingHighTime			= new DateTime[10];
		private DateTime[]				swingLowTime			= new DateTime[10];
		private double[]				coveredHigh				= new double[10];
		private double[]				coveredLow				= new double[10];
		private DateTime[]				coveredHighTime			= new DateTime[10];
		private DateTime[]				coveredLowTime			= new DateTime[10];
		private int						highFibIndex			= -1;
		private int						lowFibIndex				= -1;
		private int[]					highPairIndex			= new int[10];
		private int[]					lowPairIndex			= new int[10];
		private double[]				highFib					= new double[10];
		private double[]				coupledLowFib			= new double[10];
		private DateTime[]				highFibTime				= new DateTime[10];
		private double[]				lowFib					= new double[10];
		private double[]				coupledHighFib			= new double[10];
		private DateTime[]				lowFibTime				= new DateTime[10];
		private double					coupledLowFibTemp		= double.MaxValue;
		private double					coupledHighFibTemp		= double.MinValue;
		private int						highExtIndex			= -1;
		private int						lowExtIndex				= -1;
		private int[]					highExtPairIndex		= new int[10];
		private int[]					lowExtPairIndex			= new int[10];
		private double[]				highExtFib				= new double[10];
		private double[]				coupledLowExtFib		= new double[10];
		private DateTime[]				highExtFibTime			= new DateTime[10];
		private double[]				lowExtFib				= new double[10];
		private double[]				coupledHighExtFib		= new double[10];
		private DateTime[]				lowExtFibTime			= new DateTime[10];
		private bool					noextensions			= false;
		private double					fib000					= 0;
		private double					fib1000					= 0;
		private int[]					fibCounter				= new int[192];
		private DateTimeSeries[]		highDate;
		private DateTimeSeries[]		lowDate;
		private bool 					show_S_Plus				= true;
		private bool					show_S_Minus			= true;
		private bool 					show_E_Plus				= true;
		private bool					show_E_Minus			= true;		
		private bool 					show_D_Plus				= true;
		private bool					show_D_Minus			= true;		
		private bool 					show_C_Plus				= true;
		private bool					show_C_Minus			= true;		
		private bool 					show_B_Plus				= true;
		private bool					show_B_Minus			= true;
		private bool 					show_A_Plus				= true;
		private bool					show_A_Minus			= true;
		private bool					show_Xtensions			= true;
		private bool					show_Highs_Lows			= true;
		private bool					show_Y_Plus				= true;
		private bool					show_Y_Minus			= true;
		private bool					show_Z_Plus				= true;
		private bool					show_Z_Minus			= true;		
		private DateTimeSeries[]		hiddenHighDate;
		private DateTimeSeries[]		hiddenLowDate;
		private DateTimeSeries[]		highExtDate;
		private DateTimeSeries[]		lowExtDate;
		private double					runningHigh				= double.MinValue;
		private double					runningLow				= double.MaxValue;
		private DateTime				runningHighTime			= Cbi.Globals.MinDate;
		private DateTime				runningLowTime			= Cbi.Globals.MinDate;
		private int						runningHighIndex		=-1;
		private int 					runningLowIndex			=-1;
		private double					lastHighFib				= double.MaxValue;
		private double					lastLowFib				= double.MinValue;
		private double					lastCoupledLowFib		= double.MinValue ;
		private double					lastCoupledHighFib		= double.MaxValue;
		private DateTime				lastHighFibTime			= Cbi.Globals.MinDate;
		private DateTime				lastLowFibTime			= Cbi.Globals.MinDate;
		private DateTimeSeries			lastHighDate;
		private DateTimeSeries			lastLowDate;
		private bool					recentDown				= false;
		private bool					recentUp				= false;
		private bool					zExtensionDown			= false;
		private bool					zExtensionUp			= false;
		private double					recentHigh				= double.MaxValue;
		private double					recentLow				= double.MinValue;
		private DateTime				recentHighTime			= Cbi.Globals.MinDate;
		private DateTime				recentLowTime			= Cbi.Globals.MinDate;
		private double					recentCoupledLow		= double.MinValue;
		private double					recentCoupledHigh		= double. MaxValue;
		private DateTimeSeries			recentHighDate;
		private DateTimeSeries			recentLowDate;
		private double					rememberHigh			= 0;
		private double					rememberLow				= 0;
		private DateTime				rememberTime			= Cbi.Globals.MinDate;
		private string[] 				plotlabel				= new string[192];	
			
		#endregion

		/// <summary>
		/// This method is used to configure the indicator and is called once before any bar data is loaded.
		/// </summary>
		protected override void Initialize()
		{
			Add(new Plot(new Pen(Color.BlueViolet,1), PlotStyle.Line,"X+ 127,2 "));
			Add(new Plot(new Pen(Color.BlueViolet,1), PlotStyle.Line,"X+ 161,8 "));	
			Add(new Plot(new Pen(Color.BlueViolet,1), PlotStyle.Line,"X+ 200,0 "));	
			Add(new Plot(new Pen(Color.DarkOrange,1), PlotStyle.Line,"X+ 127,2 "));
			Add(new Plot(new Pen(Color.DarkOrange,1), PlotStyle.Line,"X+ 161,8 "));	
			Add(new Plot(new Pen(Color.DarkOrange,1), PlotStyle.Line,"X+ 200,0 "));	
			Add(new Plot(new Pen(Color.OrangeRed,1), PlotStyle.Line,"X+ 127,2 "));
			Add(new Plot(new Pen(Color.OrangeRed,1), PlotStyle.Line,"X+ 161,8 "));	
			Add(new Plot(new Pen(Color.OrangeRed,1), PlotStyle.Line,"X+ 200,0 "));	
			Add(new Plot(new Pen(Color.Green,1), PlotStyle.Line,"X+ 127,2 "));
			Add(new Plot(new Pen(Color.Green,1), PlotStyle.Line,"X+ 161,8 "));	
			Add(new Plot(new Pen(Color.Green,1), PlotStyle.Line,"X+ 200,0 "));	
			Add(new Plot(new Pen(Color.Blue,1), PlotStyle.Line,"X+ 127,2 "));
			Add(new Plot(new Pen(Color.Blue,1), PlotStyle.Line,"X+ 161,8 "));	
			Add(new Plot(new Pen(Color.Blue,1), PlotStyle.Line,"X+ 200,0 "));	
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"X+ 127,2 "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"X+ 161,8 "));	
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"X+ 200,0 "));	

			Add(new Plot(new Pen(Color.BlueViolet,1), PlotStyle.Line,"X- 127,2 "));
			Add(new Plot(new Pen(Color.BlueViolet,1), PlotStyle.Line,"X- 161,8 "));	
			Add(new Plot(new Pen(Color.BlueViolet,1), PlotStyle.Line,"X- 200,0 "));	
			Add(new Plot(new Pen(Color.DarkOrange,1), PlotStyle.Line,"X- 127,2 "));
			Add(new Plot(new Pen(Color.DarkOrange,1), PlotStyle.Line,"X- 161,8 "));	
			Add(new Plot(new Pen(Color.DarkOrange,1), PlotStyle.Line,"X- 200,0 "));	
			Add(new Plot(new Pen(Color.OrangeRed,1), PlotStyle.Line,"X- 127,2 "));
			Add(new Plot(new Pen(Color.OrangeRed,1), PlotStyle.Line,"X- 161,8 "));	
			Add(new Plot(new Pen(Color.OrangeRed,1), PlotStyle.Line,"X- 200,0 "));	
			Add(new Plot(new Pen(Color.Green,1), PlotStyle.Line,"X- 127,2 "));
			Add(new Plot(new Pen(Color.Green,1), PlotStyle.Line,"X- 161,8 "));	
			Add(new Plot(new Pen(Color.Green,1), PlotStyle.Line,"X- 200,0 "));	
			Add(new Plot(new Pen(Color.Blue,1), PlotStyle.Line,"X- 127,2 "));
			Add(new Plot(new Pen(Color.Blue,1), PlotStyle.Line,"X- 161,8 "));	
			Add(new Plot(new Pen(Color.Blue,1), PlotStyle.Line,"X- 200,0 "));	
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"X- 127,2 "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"X- 161,8 "));	
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"X- 200,0 "));	
			
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"Z+   0,0 "));
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"Z+  23,6 "));
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"Z+  38,2 "));
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"Z+  50,0 "));
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"Z+  61,8 "));
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"Z+  76,4 "));
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"HIGH   "));
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"Z+ 127,2 "));
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"Z+ 161,8 "));
			
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"Z-   0,0 "));
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"Z-  23,6 "));
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"Z-  38,2 "));
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"Z-  50,0 "));
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"Z-  61,8 "));
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"Z-  76,4 "));
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"LOW      "));
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"Z- 127,2 "));
			Add(new Plot(new Pen(Color.Maroon,1), PlotStyle.Line,"Z- 161,8 "));
			
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"Y+   0,0 "));
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"Y+  23,6 "));
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"Y+  38,2 "));
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"Y+  50,0 "));
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"Y+  61,8 "));
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"Y+  76,4 "));
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"HIGH      "));
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"Y+ 127,2 "));
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"Y+ 161,8 "));
			
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"Y-   0,0 "));
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"Y-  23,6 "));
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"Y-  38,2 "));
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"Y-  50,0 "));
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"Y-  61,8 "));
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"Y-  76,4 "));
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"LOW      "));
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"Y- 127,2 "));
			Add(new Plot(new Pen(Color.DarkViolet,1), PlotStyle.Line,"Y- 161,8 "));
			
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"S+   0,0 "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"S+  23,6 "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"S+  38,2 "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"S+  50,0 "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"S+  61,8 "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"S+  76,4 "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"HIGH      "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"S+ 127,2 "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"S+ 161,8 "));
			
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"S-   0,0 "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"S-  23,6 "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"S-  38,2 "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"S-  50,0 "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"S-  61,8 "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"S-  76,4 "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"LOW      "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"S- 127,2 "));
			Add(new Plot(new Pen(Color.Navy,1), PlotStyle.Line,"S- 161,8 "));
			
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"E+   0,0 "));
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"E+  23,6 "));
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"E+  38,2 "));
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"E+  50,0 "));
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"E+  61,8 "));
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"E+  76,4 "));
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"HIGH      "));
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"E+ 127,2 "));
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"E+ 161,8 "));
			
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"E-   0,0 "));
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"E-  23,6 "));
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"E-  38,2 "));
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"E-  50,0 "));
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"E-  61,8 "));
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"E-  76,4 "));
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"LOW      "));
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"E- 127,2 "));
			Add(new Plot(new Pen(Color.DarkOrange,2), PlotStyle.Line,"E- 161,8 "));
		
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"D+   0,0 "));
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"D+  23,6 "));
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"D+  38,2 "));
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"D+  50,0 "));
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"D+  61,8 "));
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"D+  76,4 "));
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"HIGH      "));
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"D+ 127,2 "));
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"D+ 161,8 "));
			
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"D-   0,0 "));
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"D-  23,6 "));
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"D-  38,2 "));
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"D-  50,0 "));
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"D-  61,8 "));
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"D-  76,4 "));
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"LOW      "));
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"D- 127,2 "));
			Add(new Plot(new Pen(Color.OrangeRed,2), PlotStyle.Line,"D- 161,8 "));
	
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"C+   0,0 "));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"C+  23,6 "));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"C+  38,2 "));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"C+  50,0 "));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"C+  61,8 "));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"C+  76,4 "));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"HIGH      "));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"C+ 127,2 "));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"C+ 161,8 "));
	
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"C-   0,0 "));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"C-  23,6 "));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"C-  38,2 "));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"C-  50,0 "));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"C-  61,8 "));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"C-  76,4 "));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"LOW      "));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"C- 127,2 "));
			Add(new Plot(new Pen(Color.Green,2), PlotStyle.Line,"C- 161,8 "));
						
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"B+   0,0 "));
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"B+  23,6 "));
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"B+  38,2 "));
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"B+  50,0 "));
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"B+  61,8 "));
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"B+  76,4 "));
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"HIGH      "));
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"B+ 127,2 "));
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"B+ 161,8 "));
			
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"B-   0,0 "));
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"B-  23,6 "));
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"B-  38,2 "));
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"B-  50,0 "));
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"B-  61,8 "));
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"B-  76,4 "));
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"LOW      "));
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"B- 127,2 "));
			Add(new Plot(new Pen(Color.Blue,2), PlotStyle.Line,"B- 161,8 "));
			
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"A+   0,0 "));
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"A+  23,6 "));
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"A+  38,2 "));
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"A+  50,0 "));
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"A+  61,8 "));
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"A+  76,4 "));
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"HIGH      "));
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"A+ 127,2 "));
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"A+ 161,8 "));
			
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"A-   0,0 "));
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"A-  23,6 "));
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"A-  38,2 "));
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"A-  50,0 "));
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"A-  61,8 "));
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"A-  76,4 "));
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"LOW      "));
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"A- 127,2 "));
			Add(new Plot(new Pen(Color.Navy,2), PlotStyle.Line,"A- 161,8 "));	
			
			Add(new Plot(new Pen(Color.Black,2), PlotStyle.Line,"HIGH      "));
			Add(new Plot(new Pen(Color.Black,2), PlotStyle.Line,"HIGH      "));
			Add(new Plot(new Pen(Color.Black,2), PlotStyle.Line,"HIGH      "));
			Add(new Plot(new Pen(Color.Black,2), PlotStyle.Line,"HIGH      "));
			Add(new Plot(new Pen(Color.Black,2), PlotStyle.Line,"HIGH      "));
			Add(new Plot(new Pen(Color.Black,2), PlotStyle.Line,"HIGH      "));
			
			Add(new Plot(new Pen(Color.Black,2), PlotStyle.Line,"LOW      "));
			Add(new Plot(new Pen(Color.Black,2), PlotStyle.Line,"LOW      "));
			Add(new Plot(new Pen(Color.Black,2), PlotStyle.Line,"LOW      "));
			Add(new Plot(new Pen(Color.Black,2), PlotStyle.Line,"LOW      "));
			Add(new Plot(new Pen(Color.Black,2), PlotStyle.Line,"LOW      "));
			Add(new Plot(new Pen(Color.Black,2), PlotStyle.Line,"LOW      "));

			Plots[0].Pen.DashStyle = DashStyle.Dash;
			Plots[1].Pen.DashStyle = DashStyle.Dash;
			Plots[2].Pen.DashStyle = DashStyle.Dash;
			Plots[3].Pen.DashStyle = DashStyle.Dash;
			Plots[4].Pen.DashStyle = DashStyle.Dash;
			Plots[5].Pen.DashStyle = DashStyle.Dash;
			Plots[6].Pen.DashStyle = DashStyle.Dash;
			Plots[7].Pen.DashStyle = DashStyle.Dash;
			Plots[8].Pen.DashStyle = DashStyle.Dash;
			Plots[9].Pen.DashStyle = DashStyle.Dash;
			Plots[10].Pen.DashStyle = DashStyle.Dash;
			Plots[11].Pen.DashStyle = DashStyle.Dash;
			Plots[12].Pen.DashStyle = DashStyle.Dash;
			Plots[13].Pen.DashStyle = DashStyle.Dash;
			Plots[14].Pen.DashStyle = DashStyle.Dash;
			Plots[15].Pen.DashStyle = DashStyle.Dash;
			Plots[16].Pen.DashStyle = DashStyle.Dash;
			Plots[17].Pen.DashStyle = DashStyle.Dash;
			Plots[18].Pen.DashStyle = DashStyle.Dash;
			Plots[19].Pen.DashStyle = DashStyle.Dash;
			Plots[20].Pen.DashStyle = DashStyle.Dash;
			Plots[21].Pen.DashStyle = DashStyle.Dash;
			Plots[22].Pen.DashStyle = DashStyle.Dash;
			Plots[23].Pen.DashStyle = DashStyle.Dash;
			Plots[24].Pen.DashStyle = DashStyle.Dash;
			Plots[25].Pen.DashStyle = DashStyle.Dash;
			Plots[26].Pen.DashStyle = DashStyle.Dash;
			Plots[27].Pen.DashStyle = DashStyle.Dash;
			Plots[28].Pen.DashStyle = DashStyle.Dash;
			Plots[29].Pen.DashStyle = DashStyle.Dash;
			Plots[30].Pen.DashStyle = DashStyle.Dash;
			Plots[31].Pen.DashStyle = DashStyle.Dash;
			Plots[32].Pen.DashStyle = DashStyle.Dash;
			Plots[33].Pen.DashStyle = DashStyle.Dash;
			Plots[34].Pen.DashStyle = DashStyle.Dash;
			Plots[35].Pen.DashStyle = DashStyle.Dash;
			Plots[36].Pen.DashStyle = DashStyle.Dash;
			Plots[37].Pen.DashStyle = DashStyle.Dash;
			Plots[38].Pen.DashStyle = DashStyle.Dash;
			Plots[39].Pen.DashStyle = DashStyle.Dash;
			Plots[40].Pen.DashStyle = DashStyle.Dash;
			Plots[41].Pen.DashStyle = DashStyle.Dash;
			Plots[42].Pen.DashStyle = DashStyle.Dash;
			Plots[43].Pen.DashStyle = DashStyle.Dash;
			Plots[44].Pen.DashStyle = DashStyle.Dash;
			Plots[45].Pen.DashStyle = DashStyle.Dash;
			Plots[46].Pen.DashStyle = DashStyle.Dash;
			Plots[47].Pen.DashStyle = DashStyle.Dash;
			Plots[48].Pen.DashStyle = DashStyle.Dash;
			Plots[49].Pen.DashStyle = DashStyle.Dash;
			Plots[50].Pen.DashStyle = DashStyle.Dash;
			Plots[51].Pen.DashStyle = DashStyle.Dash;
			Plots[52].Pen.DashStyle = DashStyle.Dash;
			Plots[53].Pen.DashStyle = DashStyle.Dash;
			Plots[54].Pen.DashStyle = DashStyle.Dash;
			Plots[55].Pen.DashStyle = DashStyle.Dash;
			Plots[56].Pen.DashStyle = DashStyle.Dash;
			Plots[57].Pen.DashStyle = DashStyle.Dash;
			Plots[58].Pen.DashStyle = DashStyle.Dash;
			Plots[59].Pen.DashStyle = DashStyle.Dash;
			Plots[60].Pen.DashStyle = DashStyle.Dash;
			Plots[61].Pen.DashStyle = DashStyle.Dash;
			Plots[62].Pen.DashStyle = DashStyle.Dash;
			Plots[63].Pen.DashStyle = DashStyle.Dash;
			Plots[64].Pen.DashStyle = DashStyle.Dash;
			Plots[65].Pen.DashStyle = DashStyle.Dash;
			Plots[66].Pen.DashStyle = DashStyle.Dash;
			Plots[67].Pen.DashStyle = DashStyle.Dash;
			Plots[68].Pen.DashStyle = DashStyle.Dash;
			Plots[69].Pen.DashStyle = DashStyle.Dash;
			Plots[70].Pen.DashStyle = DashStyle.Dash;
			Plots[71].Pen.DashStyle = DashStyle.Dash;
			Plots[72].Pen.DashStyle = DashStyle.Dash;
			Plots[73].Pen.DashStyle = DashStyle.Dash;
			Plots[74].Pen.DashStyle = DashStyle.Dash;
			Plots[75].Pen.DashStyle = DashStyle.Dash;
			Plots[76].Pen.DashStyle = DashStyle.Dash;
			Plots[77].Pen.DashStyle = DashStyle.Dash;
			Plots[78].Pen.DashStyle = DashStyle.Dash;
			Plots[79].Pen.DashStyle = DashStyle.Dash;
			Plots[80].Pen.DashStyle = DashStyle.Dash;
			Plots[81].Pen.DashStyle = DashStyle.Dash;
			Plots[82].Pen.DashStyle = DashStyle.Dash;
			Plots[83].Pen.DashStyle = DashStyle.Dash;
			Plots[84].Pen.DashStyle = DashStyle.Dash;
			Plots[85].Pen.DashStyle = DashStyle.Dash;
			Plots[86].Pen.DashStyle = DashStyle.Dash;
			Plots[87].Pen.DashStyle = DashStyle.Dash;
			Plots[88].Pen.DashStyle = DashStyle.Dash;
			Plots[89].Pen.DashStyle = DashStyle.Dash;
			Plots[90].Pen.DashStyle = DashStyle.Dash;
			Plots[91].Pen.DashStyle = DashStyle.Dash;
			Plots[92].Pen.DashStyle = DashStyle.Dash;
			Plots[93].Pen.DashStyle = DashStyle.Dash;
			Plots[94].Pen.DashStyle = DashStyle.Dash;
			Plots[95].Pen.DashStyle = DashStyle.Dash;
			Plots[96].Pen.DashStyle = DashStyle.Dash;
			Plots[97].Pen.DashStyle = DashStyle.Dash;
			Plots[98].Pen.DashStyle = DashStyle.Dash;
			Plots[99].Pen.DashStyle = DashStyle.Dash;
			Plots[100].Pen.DashStyle = DashStyle.Dash;
			Plots[101].Pen.DashStyle = DashStyle.Dash;
			Plots[102].Pen.DashStyle = DashStyle.Dash;
			Plots[103].Pen.DashStyle = DashStyle.Dash;
			Plots[104].Pen.DashStyle = DashStyle.Dash;
			Plots[105].Pen.DashStyle = DashStyle.Dash;
			Plots[106].Pen.DashStyle = DashStyle.Dash;
			Plots[107].Pen.DashStyle = DashStyle.Dash;
			Plots[108].Pen.DashStyle = DashStyle.Dash;
			Plots[109].Pen.DashStyle = DashStyle.Dash;
			Plots[110].Pen.DashStyle = DashStyle.Dash;
			Plots[111].Pen.DashStyle = DashStyle.Dash;
			Plots[112].Pen.DashStyle = DashStyle.Dash;
			Plots[113].Pen.DashStyle = DashStyle.Dash;
			Plots[114].Pen.DashStyle = DashStyle.Dash;
			Plots[115].Pen.DashStyle = DashStyle.Dash;
			Plots[116].Pen.DashStyle = DashStyle.Dash;
			Plots[117].Pen.DashStyle = DashStyle.Dash;
			Plots[118].Pen.DashStyle = DashStyle.Dash;
			Plots[119].Pen.DashStyle = DashStyle.Dash;
			Plots[120].Pen.DashStyle = DashStyle.Dash;
			Plots[121].Pen.DashStyle = DashStyle.Dash;
			Plots[122].Pen.DashStyle = DashStyle.Dash;
			Plots[123].Pen.DashStyle = DashStyle.Dash;
			Plots[124].Pen.DashStyle = DashStyle.Dash;
			Plots[125].Pen.DashStyle = DashStyle.Dash;
			Plots[126].Pen.DashStyle = DashStyle.Dash;
			Plots[127].Pen.DashStyle = DashStyle.Dash;
			Plots[128].Pen.DashStyle = DashStyle.Dash;
			Plots[129].Pen.DashStyle = DashStyle.Dash;
			Plots[130].Pen.DashStyle = DashStyle.Dash;
			Plots[131].Pen.DashStyle = DashStyle.Dash;
			Plots[132].Pen.DashStyle = DashStyle.Dash;
			Plots[133].Pen.DashStyle = DashStyle.Dash;
			Plots[134].Pen.DashStyle = DashStyle.Dash;
			Plots[135].Pen.DashStyle = DashStyle.Dash;
			Plots[136].Pen.DashStyle = DashStyle.Dash;
			Plots[137].Pen.DashStyle = DashStyle.Dash;
			Plots[138].Pen.DashStyle = DashStyle.Dash;
			Plots[139].Pen.DashStyle = DashStyle.Dash;
			Plots[140].Pen.DashStyle = DashStyle.Dash;
			Plots[141].Pen.DashStyle = DashStyle.Dash;
			Plots[142].Pen.DashStyle = DashStyle.Dash;
			Plots[143].Pen.DashStyle = DashStyle.Dash;
			Plots[144].Pen.DashStyle = DashStyle.Dash;
			Plots[145].Pen.DashStyle = DashStyle.Dash;
			Plots[146].Pen.DashStyle = DashStyle.Dash;
			Plots[147].Pen.DashStyle = DashStyle.Dash;
			Plots[148].Pen.DashStyle = DashStyle.Dash;
			Plots[149].Pen.DashStyle = DashStyle.Dash;
			Plots[150].Pen.DashStyle = DashStyle.Dash;
			Plots[151].Pen.DashStyle = DashStyle.Dash;
			Plots[152].Pen.DashStyle = DashStyle.Dash;
			Plots[153].Pen.DashStyle = DashStyle.Dash;
			Plots[154].Pen.DashStyle = DashStyle.Dash;
			Plots[155].Pen.DashStyle = DashStyle.Dash;
			Plots[156].Pen.DashStyle = DashStyle.Dash;
			Plots[157].Pen.DashStyle = DashStyle.Dash;
			Plots[158].Pen.DashStyle = DashStyle.Dash;
			Plots[159].Pen.DashStyle = DashStyle.Dash;
			Plots[160].Pen.DashStyle = DashStyle.Dash;
			Plots[161].Pen.DashStyle = DashStyle.Dash;		
			Plots[162].Pen.DashStyle = DashStyle.Dash;
			Plots[163].Pen.DashStyle = DashStyle.Dash;
			Plots[164].Pen.DashStyle = DashStyle.Dash;
			Plots[165].Pen.DashStyle = DashStyle.Dash;
			Plots[166].Pen.DashStyle = DashStyle.Dash;
			Plots[167].Pen.DashStyle = DashStyle.Dash;
			Plots[168].Pen.DashStyle = DashStyle.Dash;
			Plots[169].Pen.DashStyle = DashStyle.Dash;
			Plots[170].Pen.DashStyle = DashStyle.Dash;
			Plots[171].Pen.DashStyle = DashStyle.Dash;
			Plots[172].Pen.DashStyle = DashStyle.Dash;
			Plots[173].Pen.DashStyle = DashStyle.Dash;
			Plots[174].Pen.DashStyle = DashStyle.Dash;
			Plots[175].Pen.DashStyle = DashStyle.Dash;
			Plots[176].Pen.DashStyle = DashStyle.Dash;
			Plots[177].Pen.DashStyle = DashStyle.Dash;
			Plots[178].Pen.DashStyle = DashStyle.Dash;
			Plots[179].Pen.DashStyle = DashStyle.Dash;
			Plots[180].Pen.DashStyle = DashStyle.Dash;
			Plots[181].Pen.DashStyle = DashStyle.Dash;
			Plots[182].Pen.DashStyle = DashStyle.Dash;
			Plots[183].Pen.DashStyle = DashStyle.Dash;
			Plots[184].Pen.DashStyle = DashStyle.Dash;
			Plots[185].Pen.DashStyle = DashStyle.Dash;
			Plots[186].Pen.DashStyle = DashStyle.Dash;
			Plots[187].Pen.DashStyle = DashStyle.Dash;
			Plots[188].Pen.DashStyle = DashStyle.Dash;
			Plots[189].Pen.DashStyle = DashStyle.Dash;
			Plots[190].Pen.DashStyle = DashStyle.Dash;
			Plots[191].Pen.DashStyle = DashStyle.Dash;
			
			highDate = new DateTimeSeries[6];
			lowDate = new DateTimeSeries[6];
			hiddenHighDate = new DateTimeSeries[6];
			hiddenLowDate = new DateTimeSeries[6];
			highExtDate = new DateTimeSeries[6];
			lowExtDate = new DateTimeSeries[6];

			for (int i=0; i<6; i++)
			{
				highDate[i] = new DateTimeSeries(this, MaximumBarsLookBack.Infinite);
				lowDate[i] = new DateTimeSeries(this, MaximumBarsLookBack.Infinite);
				hiddenHighDate[i]= new DateTimeSeries(this, MaximumBarsLookBack.Infinite);
				hiddenLowDate[i]= new DateTimeSeries(this, MaximumBarsLookBack.Infinite);
				highExtDate[i] = new DateTimeSeries(this, MaximumBarsLookBack.Infinite);
				lowExtDate[i] = new DateTimeSeries(this, MaximumBarsLookBack.Infinite);
			}
			
			lastHighDate = new DateTimeSeries(this, MaximumBarsLookBack.Infinite);
			lastLowDate = new DateTimeSeries(this, MaximumBarsLookBack.Infinite);
			recentHighDate = new DateTimeSeries(this, MaximumBarsLookBack.Infinite);
			recentLowDate = new DateTimeSeries(this, MaximumBarsLookBack.Infinite);
			
			AutoScale						= false;
			Overlay							= true;
			stringFormatNear.Alignment 		= StringAlignment.Near;
			stringFormatCenter.Alignment 	= StringAlignment.Center;
			stringFormatFar.Alignment		= StringAlignment.Far;
			
			// Defining all instruments for which this indicator can be used. Indicator will not show on any instruments not listed here!
			
			if(Instrument == null)
				return;
		
			//Initialisation	
			
			for (int i=0; i<5; i++)
			{
				priorHigh[i]=double.MaxValue;
				priorLow[i]= double.MinValue;
				priorClose[i] = 0;
				priorHighTime[i] = Cbi.Globals.MinDate;
				priorLowTime[i] = Cbi.Globals.MinDate;
			}
			for (int i=0; i<10; i++)
			{
				swingHigh[i] = double.MaxValue;
				swingLow[i] = double.MinValue;	
				swingHighTime[i] = Cbi.Globals.MinDate;
				swingLowTime[i] = Cbi.Globals.MinDate;	
				coveredHigh[i] = double.MaxValue;
				coveredLow[i] = double.MinValue;	
				coveredHighTime[i] = Cbi.Globals.MinDate;
				coveredLowTime[i] = Cbi.Globals.MinDate;					
				highPairIndex[i]= -1;
				lowPairIndex[i]= -1;		
				highFib[i]= double.MaxValue;	
				coupledLowFib[i]= double.MinValue;
				highFibTime[i]=Cbi.Globals.MinDate;	
				lowFib[i] = double.MinValue;
				coupledHighFib[i] = double.MaxValue;
				lowFibTime[i]=Cbi.Globals.MinDate;	
				highExtPairIndex[i]= -1;
				lowExtPairIndex[i]= -1;		
				highExtFib[i]= double.MaxValue;	
				coupledLowExtFib[i]= double.MinValue;
				highExtFibTime[i]=Cbi.Globals.MinDate;	
				lowExtFib[i] = double.MinValue;
				coupledHighExtFib[i] = double.MaxValue;
				lowExtFibTime[i]=Cbi.Globals.MinDate;	
			}
			for (int i=0; i<192; i++)
			{	
				fibCounter[i] = 0;
				plotlabel[i]= "empty";
			}
		}

		/// <summary>
		/// Called on each bar update event (incoming tick)
		/// </summary>
		protected override void OnBarUpdate()
		{
			if (Bars == null)
				return;
			if (!Data.BarsType.GetInstance(Bars.Period.Id).IsIntraday)		
			{	
				if(Bars.Period.Id != PeriodType.Day)
					return;
				else if (Bars.Period.Value > 1)
					return;
				else 
					dailyChart = true;
			}
			DateTime firstBarTime = Bars.Get(0).Time;
			if (firstBarTime.DayOfWeek == DayOfWeek.Monday)
				firstBarTime = firstBarTime.AddDays(-2);
			if (firstBarTime.Date > DateTime.Now.AddDays(2-lookBack).Date )
			{
				textBrush.Color = ChartControl.AxisColor;
				DrawTextFixed("errortag", errorData, TextPosition.Center, textBrush.Color, textFont, Color.Transparent,Color.Transparent,0);
				return;
			}
			int extendedLookBack = 5*lookBack/4 + 5;
			if (Time[0]< DateTime.Now.AddDays(-extendedLookBack))
				return;
			
			if (!dailyChart)
			{
				Bars.Session.GetNextBeginEnd(Time[0], out sessionBegin, out sessionEnd);

				if (offset == new TimeSpan(0,0,0) || offset > sessionEnd.Subtract(sessionBegin))
					sessionLength = sessionEnd.Subtract(sessionBegin);
				else
					sessionLength = offset;
			}
			
			double	high	=	High[0];
			double	low		=	Low[0];
			double	close	=	Close[0];
	
			if (Bars.FirstBarOfSession || dailyChart)
			{
				periodOpen = false;
				if (!firstPeriod)
				{
					sessionNumber = sessionNumber+1;
					for (int i=4;i>0;i--)
					{		
						priorHigh[i]=priorHigh[i-1];
						priorLow[i]=priorLow[i-1];
						priorClose[i]= priorClose[i-1];
						priorHighTime[i]=priorHighTime[i-1];
						priorLowTime[i]=priorLowTime[i-1];
					}
					priorHigh[0]		= currentHigh;
					priorLow[0]			= currentLow;
					priorClose[0]		= currentClose;
					priorHighTime[0]	= currentHighTime;
					priorLowTime[0] 	= currentLowTime;
					priorLowAfterHigh 	= currentLowAfterHigh;
					priorHighAfterLow 	= currentHighAfterLow;
					priorLowAfterHighTime = currentLowAfterHighTime;
					priorHighAfterLowTime = currentHighAfterLowTime;
					
					//Identifiying Swing Highs for Fibonacci Retracements
					existsNewPrimaryHigh=false;
					existsNewSecondaryHigh=false;
					coupledLowFibTemp =	Math.Min(coupledLowFibTemp, priorLow[0]);
					swingIndex=-1;
					if (priorHigh[2] > priorHigh[3] && priorHigh[2] > priorHigh[4] && // CHANGED HERE
						priorHigh[2] > priorHigh[1] && priorHigh[2] > priorHigh[0])	
					{	
						newPrimaryHigh = priorHigh[2];
						existsNewPrimaryHigh = true;
						if (priorHigh[0] >= priorHigh[1])
						{	
							newSecondaryHigh = priorHigh[0];
							existsNewSecondaryHigh = true;
							coupledLowFibTemp = priorLowAfterHigh;
							swingIndex=0;
						}
					}
					else if (priorHigh[1] > priorHigh[2] && priorHigh[1] > priorHigh[0])  // CHANGED HERE
					{	
						newSecondaryHigh = priorHigh[1];
						existsNewSecondaryHigh = true;
						swingIndex=1;
					}
					else if (priorHigh[0] > priorHigh[1]) // CHANGED HERE
					{
						newSecondaryHigh = priorHigh[0];
						existsNewSecondaryHigh = true;
						coupledLowFibTemp = priorLowAfterHigh;
						swingIndex=0;
					}
					
					// Filtering out insignificant Primary Highs occuring shortly after a higher Primary High
					for (int i=1; i<9; i++) 
					if (swingHighTime[i+1] != Cbi.Globals.MinDate)
					{
							shortperiod = (swingHighTime[0]-swingHighTime[i]).Days;
							longperiod = (swingHighTime[0]-swingHighTime[i+1]).Days;
							if (Convert.ToDouble(shortperiod)/Convert.ToDouble(longperiod) > Convert.ToDouble(Filter)/Convert.ToDouble(100))
						{
							for (int j=i; j<9; j++)
							{	
								swingHigh[j]=swingHigh[j+1];
								swingHighTime[j]=swingHighTime[j+1];
							}
							swingHigh[9]=double.MaxValue;
							swingHighTime[9]=Cbi.Globals.MinDate;	
						}	
					}
					
					//Deleting old Secondary High
					if ((existsNewPrimaryHigh || existsNewSecondaryHigh) && existsOldSecondaryHigh)	
					{
						for (int i=0; i<9; i++)
						{
							swingHigh[i]=swingHigh[i+1];
							swingHighTime[i]=swingHighTime[i+1];
						}
						swingHigh[9]=double.MaxValue;
						swingHighTime[9]=Cbi.Globals.MinDate;
						existsOldSecondaryHigh=false;
					}
					
					//Adding new Primary High to Array
					if (existsNewPrimaryHigh)
					{
						if (newPrimaryHigh < swingHigh[0])
						{
							for (int i=9; i>0; i--)
							{
								swingHigh[i]=swingHigh[i-1];
								swingHighTime[i]=swingHighTime[i-1];
							}
						}
						else
						{
							rIndex = 1;
							for (int i=1; i<10; i++)
							{
								if (newPrimaryHigh >= swingHigh[i])
									rIndex = i+1;
							}
							// rIndex defines the number of Swing Highs to be deleted and transferred to Covered High Array
						    for (int i=9; i>=rIndex; i--)
							{
								coveredHigh[i]=coveredHigh[i-rIndex];
								coveredHighTime[i]=coveredHighTime[i-rIndex];
							}
							for (int i=0; i<rIndex; i++)
							{
								coveredHigh[i]=swingHigh[i];
								coveredHighTime[i]=swingHighTime[i];
							}
							// Covered Highs need to be sorted chronologically
							for (int i = 9; i>0; i--)
								for (int j=i; j>0; j--)
									if (coveredHighTime[j]>coveredHighTime[j-1])
									{
										rememberHigh = coveredHigh[j];
										coveredHigh[j] = coveredHigh[j-1];
										coveredHigh[j-1] = rememberHigh;
										rememberTime = coveredHighTime[j];
										coveredHighTime[j]= coveredHighTime[j-1];
										coveredHighTime[j-1]= rememberTime;
									}
							for (int i=0; i<10-rIndex; i++)
								if (rIndex>1)	
								{
									swingHigh[i+1]=swingHigh[i+rIndex];	
									swingHighTime[i+1]=swingHighTime[i+rIndex];
								}
							for (int i=9; i>10-rIndex; i--)
							{
								swingHigh[i]=double.MaxValue;	
								swingHighTime[i]=Cbi.Globals.MinDate;	
							}
						}
						swingHigh[0]=newPrimaryHigh;
						swingHighTime[0]= priorHighTime[2];
					}
					
					//Adding new Secondary High to Array
					if (existsNewSecondaryHigh)
					{	
						if (newSecondaryHigh < swingHigh[0])
						{
							for (int i=9; i>0; i--)
							{
								swingHigh[i]=swingHigh[i-1];
								swingHighTime[i]=swingHighTime[i-1];
							}
						}
						else
						{	
							rIndex = 1;
							for (int i=1; i<10; i++)
							{
								if (newSecondaryHigh >= swingHigh[i])
									rIndex = i+1;
							}
							// rIndex defines the number of Swing Highs to be deleted and transferred to Covered High Array
						    for (int i=9; i>=rIndex; i--)
							{
								coveredHigh[i]=coveredHigh[i-rIndex];
								coveredHighTime[i]=coveredHighTime[i-rIndex];
							}
							for (int i=0; i<rIndex; i++)
							{
								coveredHigh[i]=swingHigh[i];
								coveredHighTime[i]=swingHighTime[i];
							}
							// Covered Highs need to be sorted chronologically
							for (int i = 9; i>0; i--)
								for (int j=i; j>0; j--)
									if (coveredHighTime[j]>coveredHighTime[j-1])
									{
										rememberHigh = coveredHigh[j];
										coveredHigh[j] = coveredHigh[j-1];
										coveredHigh[j-1] = rememberHigh;
										rememberTime = coveredHighTime[j];
										coveredHighTime[j]= coveredHighTime[j-1];
										coveredHighTime[j-1]= rememberTime;
									}
							for (int i=0; i<10-rIndex; i++)
								if(rIndex>1)
								{
									swingHigh[i+1]=swingHigh[i+rIndex];	
									swingHighTime[i+1]=swingHighTime[i+rIndex];
								}
							for (int i=9; i>10-rIndex; i--)
							{
								swingHigh[i]=double.MaxValue;	
								swingHighTime[i]=Cbi.Globals.MinDate;	
							}
						}	
						swingHigh[0]=newSecondaryHigh;
						swingHighTime[0]= priorHighTime[swingIndex];
						existsOldSecondaryHigh = true;
					}	
			
					//Identifiying Swing Lows for Fibonacci Retracements
					existsNewPrimaryLow=false;
					existsNewSecondaryLow=false;
					coupledHighFibTemp = Math.Max(coupledHighFibTemp, priorHigh[0]);
					swingIndex=-1;
					if (priorLow[2] < priorLow[3] && priorLow[2]<priorLow[4] &&  //CHANGED HERE
						priorLow[2] < priorLow[1] && priorLow[2]<priorLow[0])	
					{	
						newPrimaryLow = priorLow[2];
						existsNewPrimaryLow = true;
						if (priorLow[0] <= priorLow[1])
						{	
							newSecondaryLow = priorLow[0];
							existsNewSecondaryLow = true;
							coupledHighFibTemp = priorHighAfterLow;
							swingIndex=0;
						}
					}
					else if (priorLow[1] < priorLow[2] && priorLow[1] < priorLow[0])  // CHANGED HERE
					{	
						newSecondaryLow = priorLow[1];
						existsNewSecondaryLow = true;
						swingIndex=1;
					}
					else if (priorLow[0] < priorLow[1]) // CHANGED HERE
					{
						newSecondaryLow = priorLow[0];
						existsNewSecondaryLow = true;
						coupledHighFibTemp = priorHighAfterLow;
						swingIndex=0;
					}
					
					// Filtering out insignificant Primary Lows occuring shortly after a lower Primary Low
					for (int i=1; i<9; i++) 
					if (swingLowTime[i+1] != Cbi.Globals.MinDate)
					{
							shortperiod = (swingLowTime[0]-swingLowTime[i]).Days;
							longperiod = (swingLowTime[0]-swingLowTime[i+1]).Days;
							if (Convert.ToDouble(shortperiod)/Convert.ToDouble(longperiod) > Convert.ToDouble(Filter)/Convert.ToDouble(100))
						{
							for (int j=i; j<9; j++)
							{	
								swingLow[j]=swingLow[j+1];
								swingLowTime[j]=swingLowTime[j+1];
							}
							swingLow[9]=double.MinValue;
							swingLowTime[9]=Cbi.Globals.MinDate;	
						}	
					}
					
					//Deleting old Secondary Low
					if ((existsNewPrimaryLow == true || existsNewSecondaryLow == true) && existsOldSecondaryLow == true )	
					{
						for (int i=0; i<9; i++)
						{
							swingLow[i]=swingLow[i+1];
							swingLowTime[i]=swingLowTime[i+1];
						}
						swingLow[9]=double.MinValue;
						swingLowTime[9]=Cbi.Globals.MinDate;
						existsOldSecondaryLow = false;
					}
					
					//Adding new Primary Low to Array
					if (existsNewPrimaryLow)
					{
						if (newPrimaryLow > swingLow[0])
						{
							for (int i=9; i>0; i--)
							{
								swingLow[i]=swingLow[i-1];
								swingLowTime[i]=swingLowTime[i-1];
							}
						}
						else
						{
							rIndex = 1;
							for (int i=1; i<10; i++)
							{
								if (newPrimaryLow <= swingLow[i])
									rIndex = i+1;
							}
							// rIndex defines the number of Swing Lows to be deleted amd transferred to Covered Low Array
						    for (int i=9; i>=rIndex; i--)
							{
								coveredLow[i]=coveredLow[i-rIndex];
								coveredLowTime[i]=coveredLowTime[i-rIndex];
							}
							for (int i=0; i<rIndex; i++)
							{
								coveredLow[i]=swingLow[i];
								coveredLowTime[i]=swingLowTime[i];
							}
							// Covered Lows need to be sorted chronologically
							for (int i = 9; i>0; i--)
								for (int j=i; j>0; j--)
									if (coveredLowTime[j]>coveredLowTime[j-1])
									{
										rememberLow = coveredLow[j];
										coveredLow[j] = coveredLow[j-1];
										coveredLow[j-1] = rememberLow;
										rememberTime = coveredLowTime[j];
										coveredLowTime[j]= coveredLowTime[j-1];
										coveredLowTime[j-1]= rememberTime;
									}
							for (int i=0; i<10-rIndex; i++)
								if (rIndex>1)
								{
									swingLow[i+1]=swingLow[i+rIndex];	
									swingLowTime[i+1]=swingLowTime[i+rIndex];
								}
							for (int i=9; i>10-rIndex; i--)
							{
								swingLow[i]=double.MinValue;	
								swingLowTime[i]=Cbi.Globals.MinDate;	
							}
						}
						swingLow[0]=newPrimaryLow;
						swingLowTime[0]= priorLowTime[2];
					}
					
					//Adding new Secondary Low to Array
					if (existsNewSecondaryLow)
					{	
						if (newSecondaryLow > swingLow[0])
						{
							for (int i=9; i>0; i--)
							{
								swingLow[i]=swingLow[i-1];
								swingLowTime[i]=swingLowTime[i-1];
							}
						}
						else
						{	
							rIndex = 1;
							for (int i=1; i<10; i++)
							{
								if (newSecondaryLow <= swingLow[i])
									rIndex = i+1;
							}
							// rIndex defines the number of Swing Lows to be deleted amd transferred to Covered Low Array
						    for (int i=9; i>=rIndex; i--)
							{
								coveredLow[i]=coveredLow[i-rIndex];
								coveredLowTime[i]=coveredLowTime[i-rIndex];
							}
							for (int i=0; i<rIndex; i++)
							{
								coveredLow[i]=swingLow[i];
								coveredLowTime[i]=swingLowTime[i];
							}
							// Covered Lows need to be sorted chronologically
							for (int i = 9; i>0; i--)
								for (int j=i; j>0; j--)
									if (coveredLowTime[j]>coveredLowTime[j-1])
									{
										rememberLow = coveredLow[j];
										coveredLow[j] = coveredLow[j-1];
										coveredLow[j-1] = rememberLow;
										rememberTime = coveredLowTime[j];
										coveredLowTime[j]= coveredLowTime[j-1];
										coveredLowTime[j-1]= rememberTime;
									}
							for (int i=0; i<10-rIndex; i++)
							{
								if (rIndex>1)
								{
									swingLow[i+1]=swingLow[i+rIndex];	
									swingLowTime[i+1]=swingLowTime[i+rIndex];
								}
							}
							for (int i=9; i>10-rIndex; i--)
							{
								swingLow[i]=double.MinValue;	
								swingLowTime[i]=Cbi.Globals.MinDate;	
							}
						}
						swingLow[0]=newSecondaryLow;
						swingLowTime[0]= priorLowTime[swingIndex];;
						existsOldSecondaryLow = true;
					}	
		
					//Creating Fibonacci Retracements from Swing Highs and Lows Arrays		
					if (existsNewPrimaryHigh || existsNewSecondaryHigh || existsNewPrimaryLow || existsNewSecondaryLow)
					{
						//Selecting Approriate Lows for Highs
						highFibIndex=-1;
						for (int i=0; i<10; i++)
						{	
							if (swingHighTime[i]== Cbi.Globals.MinDate)
							break; // High number i and following numbers cannot be used for Fib retracements
							highPairIndex[i]=-1;
							for (int j=0; j<10; j++)
							{	
							if (swingHighTime[i]<swingLowTime[j])
							highPairIndex[i]=j;
							}
							if (highPairIndex[i] > -1)
							{
								highFibIndex = highFibIndex+1;
								highFib[highFibIndex]=swingHigh[i];
								coupledLowFib[highFibIndex]= swingLow[highPairIndex[i]];
								highFibTime[highFibIndex] = swingHighTime[i];
							}
						}
	
						//Selecting Fibonacci Extensions for Covered Highs
						highExtIndex=-1;
						for (int i=0; i<10; i++)
						{	
							for (int j=0; j<10; j++)
							if (coveredHighTime[i] <= swingLowTime[j])
								highExtPairIndex[i] = j;
							if (coveredHighTime[i] == Cbi.Globals.MinDate)
								highExtPairIndex[i] = -1;
						}
						for (int i=1; i<10; i++)
							for (int j = 1; j<=i; j++)
								if (coveredHigh[i] < coveredHigh[i-j] && highExtPairIndex[i] == highExtPairIndex[i-j])
									highExtPairIndex[i] = -1;
						
						for (int i=0; i<10; i++)
						{	
							if (highExtPairIndex[i] > -1)
							{
								highExtIndex = highExtIndex+1;
								highExtFib[highExtIndex]=coveredHigh[i];
								coupledLowExtFib[highExtIndex]= swingLow[highExtPairIndex[i]];
								highExtFibTime[highExtIndex]= coveredHighTime[i];
							}
						}
						
						//Selecting Appropriate Highs for Lows
						lowFibIndex=-1;
						for (int i=0; i<10; i++)
						{	
							if (swingLowTime[i]== Cbi.Globals.MinDate)
							break; // Low number i and following numbers cannot be used for Fib retracements
							lowPairIndex[i]=-1;
							for (int j=0; j<10; j++)
							{	
							if (swingLowTime[i]<=swingHighTime[j])
							lowPairIndex[i]=j;
							}
							if (lowPairIndex[i] > -1)
							{
								lowFibIndex = lowFibIndex+1;
								lowFib[lowFibIndex]=swingLow[i];
								coupledHighFib[lowFibIndex]= swingHigh[lowPairIndex[i]];
								lowFibTime[lowFibIndex]= swingLowTime[i];
							}
						}
						
						//Selecting Fibonacci Extensions for Covered Lows
						lowExtIndex=-1;
						for (int i=0; i<10; i++)
						{	
							for (int j=0; j<10; j++)
							if (coveredLowTime[i] <= swingHighTime[j])
								lowExtPairIndex[i] = j;
							if (coveredLowTime[i] == Cbi.Globals.MinDate)
								lowExtPairIndex[i] = -1;
						}
						for (int i=1; i<10; i++)
							for (int j = 1; j<=i; j++)
								if (coveredLow[i] > coveredLow[i-j] && lowExtPairIndex[i] == lowExtPairIndex[i-j])
									lowExtPairIndex[i] = -1;
						
						for (int i=0; i<10; i++)
						{	
							if (lowExtPairIndex[i] > -1)
							{
								lowExtIndex = lowExtIndex+1;
								lowExtFib[lowExtIndex]=coveredLow[i];
								coupledHighExtFib[lowExtIndex]= swingHigh[lowExtPairIndex[i]];
								lowExtFibTime[lowExtIndex]= coveredLowTime[i];
							}
						}
					}
				}
				firstPeriod = false;
				if (Time[0] > sessionEnd.Subtract(sessionLength)|| dailyChart)				
				{	
					currentHighTime = Time[0];
					currentLowTime = Time[0];
					currentLowAfterHighTime = Time[0];
					currentHighAfterLowTime = Time[0];
					runningHighTime = Time[0];
					runningLowTime = Time[0];
					currentHigh		= high;
					currentLow		= low;
					currentClose	= close;
					currentLowAfterHigh = close;
					currentHighAfterLow = close;
					runningHigh		= High[0];
					runningLow		= Low[0];
					periodOpen		= true;
				}
			}
			else if (!periodOpen && Time[0] > sessionEnd.Subtract(sessionLength))
			{	
				currentHighTime 		= Time[0]; 
				currentLowTime 			= Time[0];
				currentLowAfterHighTime = Time[0];
				currentHighAfterLowTime = Time[0];
				runningHighTime 		= Time[0];
				runningLowTime 			= Time[0];
				currentHigh			= high;
				currentLow			= low;
				currentClose		= close;
				currentLowAfterHigh = close;
				currentHighAfterLow = close;
				runningHigh			= High[0];
				runningLow			= Low[0];
				periodOpen			=  true;
			}
			else if (periodOpen && Time[0] > sessionEnd.Subtract(sessionLength))	
			{
				if (high > currentHigh)
				{	
					currentHighTime = Time[0];
					currentLowAfterHigh = close;
					currentLowAfterHighTime = Time[0];
				}
				if (low < currentLow)
				{
					currentLowTime = Time[0];
					currentHighAfterLow = close;
					currentHighAfterLowTime = Time[0];
				}
				if (high <= currentHigh)
				{
					if (low < currentLowAfterHigh)
						currentLowAfterHighTime = Time[0];
					currentLowAfterHigh = Math.Min(currentLowAfterHigh, low);
				}
				if (low >= currentLow)
				{
					if (high > currentHighAfterLow)
						currentHighAfterLowTime = Time[0];
					currentHighAfterLow = Math.Max(currentHighAfterLow, high);
				}
				currentHigh		= Math.Max(currentHigh, high);
				currentLow		= Math.Min(currentLow, low);
				currentClose	= close;
				if(High[0] > runningHigh)
					runningHighTime = Time[0];
				if(Low[0] < runningLow)
					runningLowTime = Time[0];
				runningHigh		= Math.Max(runningHigh, High[0]);
				runningLow		= Math.Min(runningLow, Low[0]);
			}
			
			if (!dailyChart)
			{
				//Creating Fibonacci Retracement from last Swing High, Swing Low in both directions 
				runningHighIndex = -1;
				runningLowIndex = -1;
				if (runningLow < swingLow[0] && runningHigh <= swingHigh[0] && priorHigh[0]!=swingHigh[0]) // Trend Down
				{
					if (swingHighTime[0] > swingLowTime[0]) 
					{
						lastHighFib = swingHigh[0];
						lastHighFibTime = swingHighTime[0];
						lastCoupledLowFib = runningLow;
						runningHighIndex = 0;
					}
				}
				else if (runningHigh > swingHigh[0] && runningLow >= swingLow[0] && priorLow[0]!=swingLow[0]) // Trend Up
				{
					if (swingLowTime[0] > swingHighTime[0])
					{
						lastLowFib = swingLow[0];
						lastLowFibTime = swingLowTime[0];
						lastCoupledHighFib = runningHigh;
						runningLowIndex = 0;
					}
				}
				else if (runningLow >= swingLow[0] && runningHigh <= swingHigh[0]) // Inside Range
				{
					if (swingLowTime[0] >= swingHighTime[0]) 
					{	
						if (swingLow[0] != priorLow[0]) // Creating Fib Retracement from Swing Low to the Right 
						{
							lastLowFib = swingLow[0];
							lastLowFibTime = swingLowTime[0];
							lastCoupledHighFib = Math.Max(coupledHighFibTemp, runningHigh);
							runningLowIndex = 1;	
						}	
						if (priorHighTime[1]>swingHighTime[0] && swingLowTime[0]>priorHighTime[1] && priorHigh[1]>priorHigh[0] // Creating Fib Retracement backwards 
							&& (priorHigh[1]-priorClose[2] > 0.20*(priorHigh[1]-priorLow[1]) || priorHigh[1]-priorClose[2] > 0.20*(priorHigh[2]-priorLow[2])
							|| priorHighTime[1]>priorLowTime[1]))
						{
								lastHighFib = priorHigh[1];
								lastHighFibTime = priorHighTime[1];
								lastCoupledLowFib = Math.Min(swingLow[0],runningLow);
								runningHighIndex = 1;
						}
					}
					else if (swingHighTime[0] >= swingLowTime[0])
					{	
						if (swingHigh[0] != priorHigh[0]) // Creating Fib Retracement from Swing High to the Right 
						{
							lastHighFib = swingHigh[0];
							lastHighFibTime = swingHighTime[0];
							lastCoupledLowFib = Math.Min(coupledLowFibTemp, runningLow);
							runningHighIndex = 1;
						}	
						if (priorLowTime[1]>swingLowTime[0] && swingHighTime[0]>priorLowTime[1] && priorLow[1]<priorLow[0] //Creating Fib Retracement backwards
							&& (priorClose[2]-priorLow[1] > 0.20*(priorHigh[1]-priorLow[1])|| priorClose[2]-priorLow[1] > 0.20*(priorHigh[2]-priorLow[2])
							|| priorLowTime[1]> priorHighTime[1]))
						{
								lastLowFib = priorLow[1];
								lastLowFibTime = priorLowTime[1];
								lastCoupledHighFib = Math.Max(swingHigh[0],runningHigh);
								runningLowIndex = 1;
						}
					}
				}			
				// Creating Fib retracement from yesterday's (or from the day before yesterday) and today's Highs and Lows
				recentDown = false;
				recentUp = false;
				zExtensionDown = false;
				zExtensionUp = false;
				if (runningHigh > priorHigh[0] && runningLow >= priorLow[0] )// Trend Up
				{
					if (priorHigh[0]!=swingHigh[0] || priorLow[0]!=swingLow[0] || priorLowTime[0]>priorHighTime[0])
					{
						if (priorLow[0]==swingLow[0] || priorClose[1]-priorLow[0] > 0.2*(priorHigh[0]-priorLow[0]) 
							|| priorClose[1]-priorLow[0] > 0.2*(priorHigh[1]-priorLow[1]) || priorLowTime[0]>priorHighTime[0] ) // this is to exclude insignificant Lows
						{
								recentLow = priorLow[0];
								recentLowTime = priorLowTime[0];	
								recentCoupledHigh = runningHigh;
								recentDown = true;
						}
						else if ((priorLow[1] != swingLow[0])&& (priorClose[2]-priorLow[1] > 0.2*(priorHigh[1]-priorLow[1]) 
							|| priorClose[2]-priorLow[1] > 0.2*(priorHigh[2]-priorLow[2]) || priorLowTime[1]>priorHighTime[1])) // if yesterday's Low ain't no good, take the day before
						{
								recentLow = priorLow[1];
								recentLowTime = priorLowTime[1];	
								recentCoupledHigh = runningHigh;
								recentDown = true;
						}	
					}
					//the following section only is needed to display upward extensions after prior high has been taken out
					if (priorHighTime[0]>=priorLowTime[0]) 
					{
						recentHigh = priorHigh[0];
						recentHighTime = priorHighTime[0]; 
						recentCoupledLow = Math.Min(runningLow, priorLowAfterHigh);
						recentUp = true;
						zExtensionUp = true;
					}
					if (priorLowTime[0]>priorHighTime[0])
					{
						if ((priorHigh[0]==swingHigh[0] || priorHigh[0]-priorClose[1] > 0.2*(priorHigh[0]-priorLow[0])
							|| priorHigh[0]-priorClose[1] > 0.2*(priorHigh[1]-priorLow[1]) || priorHighTime[0]>priorLowTime[0])
							&& (priorHigh[0]!=swingHigh[0] || priorLow[0]!=swingLow[0]))
						{
							recentHigh = priorHigh[0];
							recentHighTime = priorHighTime[0];
							recentCoupledLow = priorLow[0];
							recentUp = true;
							zExtensionUp = true;
						}	
					}	
					// end of section for display of upward extensions 
				}
				else if (runningHigh <= priorHigh[0] && runningLow < priorLow[0] )// Trend Down
				{
					if (priorHigh[0]!=swingHigh[0] || priorLow[0]!=swingLow[0] || priorHighTime[0]>priorLowTime[0])
					{	
						
						if (priorHigh[0]==swingHigh[0] || priorHigh[0]-priorClose[1] > 0.2*(priorHigh[0]-priorLow[0])
							|| priorHigh[0]-priorClose[1] > 0.2*(priorHigh[1]-priorLow[1]) || priorHighTime[0] > priorLowTime[0]) // this is to exclude insignificant Highs
						{
								recentHigh = priorHigh[0];
								recentHighTime = priorHighTime[0];
								recentCoupledLow = runningLow;
								recentUp = true;
						}
						else if ((priorHigh[1] != swingHigh[0])&& (priorHigh[1]-priorClose[2] > 0.2*(priorHigh[1]-priorLow[1]) 
							|| priorHigh[1]-priorClose[2] > 0.2*(priorHigh[2]-priorLow[2])|| priorHighTime[1]>priorLowTime[1])) // if yesterday's High ain't no good take the day before
						{
								recentHigh = priorHigh[1];
								recentHighTime = priorHighTime[1];	
								recentCoupledLow = runningLow;
								recentUp = true;
						}		
					}
					//the following section only is needed to display downward extensions after prior low has been taken out
					if (priorHighTime[0]>=priorLowTime[0])
					{
						if ((priorLow[0]==swingLow[0] || priorClose[1]-priorLow[0] > 0.2*(priorHigh[0]-priorLow[0]) 
							|| priorClose[1]-priorLow[0] > 0.2*(priorHigh[1]-priorLow[1]) || priorLowTime[0]>priorHighTime[0])
							&& (priorHigh[0]!=swingHigh[0] || priorLow[0]!=swingLow[0]))
						{
							recentLow = priorLow[0];
							recentLowTime = priorLowTime[0];
							recentCoupledHigh = priorHigh[0];
							recentDown = true;
							zExtensionDown = true;
						}
					}
					if (priorLowTime[0]>priorHighTime[0])
					{
						recentLow = priorLow[0];
						recentLowTime = priorLowTime[0];
						recentCoupledHigh = Math.Max(runningHigh,priorHighAfterLow);
						recentDown = true;
						zExtensionDown = true;
					}
					// end of section for display of downward extensions 
				}
				else if (runningHigh <= priorHigh[0] && runningLow >= priorLow[0]) //Inside Bar
				{
					if (priorHighTime[0]>=priorLowTime[0]) // this will be displayed with Daily Bars as both times are identical
					{
						if ((priorLow[0]==swingLow[0] || priorClose[1]-priorLow[0] > 0.2*(priorHigh[0]-priorLow[0]) 
							|| priorClose[1]-priorLow[0] > 0.2*(priorHigh[1]-priorLow[1]) || priorLowTime[0]>priorHighTime[0])
							&& (priorHigh[0]!=swingHigh[0] || priorLow[0]!=swingLow[0]))
						{
							recentLow = priorLow[0];
							recentLowTime = priorLowTime[0];
							recentCoupledHigh = priorHigh[0];
							recentDown = true;
						}
						recentHigh = priorHigh[0];
						recentHighTime = priorHighTime[0]; 
						recentCoupledLow = Math.Min(runningLow, priorLowAfterHigh);
						recentUp = true;
					}
					if (priorLowTime[0]>priorHighTime[0])	// this will never occur with Daily Bars
					{
						if ((priorHigh[0]==swingHigh[0] || priorHigh[0]-priorClose[1] > 0.2*(priorHigh[0]-priorLow[0])
							|| priorHigh[0]-priorClose[1] > 0.2*(priorHigh[1]-priorLow[1]) || priorHighTime[0]>priorLowTime[0])
							&& (priorHigh[0]!=swingHigh[0] || priorLow[0]!=swingLow[0]))
						{
							recentHigh = priorHigh[0];
							recentHighTime = priorHighTime[0];
							recentCoupledLow = priorLow[0];
							recentUp = true;
						}	
						recentLow = priorLow[0];
						recentLowTime = priorLowTime[0];
						recentCoupledHigh = Math.Max(runningHigh,priorHighAfterLow);
						recentDown = true;
					}	
				}	
				else if (runningHigh > priorHigh[0] && runningLow < priorLow[0]) // Outside Bar
				{
					if (runningLowTime >= runningHighTime)
					{
						recentHigh = runningHigh;
						recentHighTime = runningHighTime;
						recentCoupledLow = runningLow;
						recentUp = true;
					}
				
					if (runningHighTime > runningLowTime)	
					{
						recentLow = runningLow;
						recentLowTime = runningLowTime;
						recentCoupledHigh = runningHigh;
						recentDown = true;
					}		
				}
			}
			
			if (highExtIndex>-1 && Show_Xtensions)
			{
				fib1000 = highExtFib[0]/TickSize;
				fib000 = coupledLowExtFib[0]/TickSize;
				Extension0.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				Extension1.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				Extension2.Set(TickSize*Math.Round(fib000+2.0*(fib1000-fib000)));
				highExtDate[0].Set(highExtFibTime[0]);
				for (int i=0;i<=9;i++)
				if (swingHighTime[i]>highExtFibTime[0] && swingHigh[i]>Extension1[0])
				{
					Extension0.Set(0);
					break;
				}
				for (int i=0;i<=9;i++)
				if (swingHighTime[i]>highExtFibTime[0] && swingHigh[i]>Extension2[0])
				{
					Extension1.Set(0);
					break;
				}
			}
			else
			{
				Extension0.Set(0);
				Extension1.Set(0);
				Extension2.Set(0);
				highExtDate[0].Set(Cbi.Globals.MinDate);
			}
			
			if (highExtIndex>0 && Show_Xtensions)
			{
				fib1000 = highExtFib[1]/TickSize;
				fib000 = coupledLowExtFib[1]/TickSize;
				Extension3.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				Extension4.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				Extension5.Set(TickSize*Math.Round(fib000+2.0*(fib1000-fib000)));
				highExtDate[1].Set(highExtFibTime[1]);
				for (int i=0;i<=9;i++)
				if (swingHighTime[i]>highExtFibTime[1] && swingHigh[i]>Extension4[0])
				{
					Extension3.Set(0);
					break;
				}
				for (int i=0;i<=9;i++)
				if (swingHighTime[i]>highExtFibTime[1] && swingHigh[i]>Extension5[0])
				{
					Extension4.Set(0);
					break;
				}
			}
			else
			{
				Extension3.Set(0);
				Extension4.Set(0);
				Extension5.Set(0);
				highExtDate[1].Set(Cbi.Globals.MinDate);
			}
			
			if (highExtIndex>1 && Show_Xtensions)
			{
				fib1000 = highExtFib[2]/TickSize;
				fib000 = coupledLowExtFib[2]/TickSize;
				Extension6.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				Extension7.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				Extension8.Set(TickSize*Math.Round(fib000+2.0*(fib1000-fib000)));
				highExtDate[2].Set(highExtFibTime[2]);
				for (int i=0;i<=9;i++)
				if (swingHighTime[i]>highExtFibTime[2] && swingHigh[i]>Extension7[0])
				{
					Extension6.Set(0);
					break;
				}
				for (int i=0;i<=9;i++)
				if (swingHighTime[i]>highExtFibTime[2] && swingHigh[i]>Extension8[0])
				{
					Extension7.Set(0);
					break;
				}
			}
			else
			{
				Extension6.Set(0);
				Extension7.Set(0);
				Extension8.Set(0);
				highExtDate[2].Set(Cbi.Globals.MinDate);
			}
			
			if (highExtIndex>2 && Show_Xtensions)
			{
				fib1000 = highExtFib[3]/TickSize;
				fib000 = coupledLowExtFib[3]/TickSize;
				Extension9.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				Extension10.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				Extension11.Set(TickSize*Math.Round(fib000+2.0*(fib1000-fib000)));
				highExtDate[3].Set(highExtFibTime[3]);
				for (int i=0;i<=9;i++)
				if (swingHighTime[i]>highExtFibTime[3] && swingHigh[i]>Extension10[0])
				{
					Extension9.Set(0);
					break;
				}
				for (int i=0;i<=9;i++)
				if (swingHighTime[i]>highExtFibTime[3] && swingHigh[i]>Extension11[0])
				{
					Extension10.Set(0);
					break;
				}
			}
			else
			{
				Extension9.Set(0);
				Extension10.Set(0);
				Extension11.Set(0);
				highExtDate[3].Set(Cbi.Globals.MinDate);
			}
			
			if (highExtIndex>3 && Show_Xtensions)
			{
				fib1000 = highExtFib[4]/TickSize;
				fib000 = coupledLowExtFib[4]/TickSize;
				Extension12.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				Extension13.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				Extension14.Set(TickSize*Math.Round(fib000+2.0*(fib1000-fib000)));
				highExtDate[4].Set(highExtFibTime[4]);
				for (int i=0;i<=9;i++)
				if (swingHighTime[i]>highExtFibTime[4] && swingHigh[i]>Extension13[0])
				{
					Extension12.Set(0);
					break;
				}
				for (int i=0;i<=9;i++)
				if (swingHighTime[i]>highExtFibTime[4] && swingHigh[i]>Extension14[0])
				{
					Extension13.Set(0);
					break;
				}
			}
			else
			{
				Extension12.Set(0);
				Extension13.Set(0);
				Extension14.Set(0);
				highExtDate[4].Set(Cbi.Globals.MinDate);
			}
			
			if (highExtIndex>4 && Show_Xtensions)
			{
				fib1000 = highExtFib[5]/TickSize;
				fib000 = coupledLowExtFib[5]/TickSize;
				Extension15.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				Extension16.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				Extension17.Set(TickSize*Math.Round(fib000+2.0*(fib1000-fib000)));
				highExtDate[5].Set(highExtFibTime[5]);
				for (int i=0;i<=9;i++)
				if (swingHighTime[i]>highExtFibTime[5] && swingHigh[i]>Extension16[0])
				{
					Extension15.Set(0);
					break;
				}
				for (int i=0;i<=9;i++)
				if (swingHighTime[i]>highExtFibTime[5] && swingHigh[i]>Extension17[0])
				{
					Extension16.Set(0);
					break;
				}
			}
			else
			{
				Extension15.Set(0);
				Extension16.Set(0);
				Extension17.Set(0);
				highExtDate[5].Set(Cbi.Globals.MinDate);
			}

			if (lowExtIndex>-1)
			{
				fib1000 = lowExtFib[0]/TickSize;
				fib000 = coupledHighExtFib[0]/TickSize;
				Extension18.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				Extension19.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				Extension20.Set(TickSize*Math.Round(fib000+2.0*(fib1000-fib000)));
				lowExtDate[0].Set(lowExtFibTime[0]);
				for (int i=0;i<=9;i++)
				if (swingLowTime[i]>lowExtFibTime[0] && swingLow[i]<Extension19[0])
				{
					Extension18.Set(0);
					break;
				}
				for (int i=0;i<=9;i++)
				if (swingLowTime[i]>lowExtFibTime[0] && swingLow[i]<Extension20[0])
				{
					Extension19.Set(0);
					break;
				}
			}
			else
			{
				Extension18.Set(0);
				Extension19.Set(0);
				Extension20.Set(0);
				lowExtDate[0].Set(Cbi.Globals.MinDate);
			}
			
			if (lowExtIndex>0 && Show_Xtensions)
			{
				fib1000 = lowExtFib[1]/TickSize;
				fib000 = coupledHighExtFib[1]/TickSize;
				Extension21.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				Extension22.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				Extension23.Set(TickSize*Math.Round(fib000+2.0*(fib1000-fib000)));
				lowExtDate[1].Set(lowExtFibTime[1]);
				for (int i=0;i<=9;i++)
				if (swingLowTime[i]>lowExtFibTime[1] && swingLow[i]<Extension22[0])
				{
					Extension21.Set(0);
					break;
				}
				for (int i=0;i<=9;i++)
				if (swingLowTime[i]>lowExtFibTime[1] && swingLow[i]<Extension23[0])
				{
					Extension22.Set(0);
					break;
				}
			}
			else
			{
				Extension21.Set(0);
				Extension22.Set(0);
				Extension23.Set(0);
				lowExtDate[1].Set(Cbi.Globals.MinDate);
			}
			
			if (lowExtIndex>1 && Show_Xtensions)
			{
				fib1000 = lowExtFib[2]/TickSize;
				fib000 = coupledHighExtFib[2]/TickSize;
				Extension24.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				Extension25.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				Extension26.Set(TickSize*Math.Round(fib000+2.0*(fib1000-fib000)));
				lowExtDate[2].Set(lowExtFibTime[2]);
				for (int i=0;i<=9;i++)
				if (swingLowTime[i]>lowExtFibTime[2] && swingLow[i]<Extension25[0])
				{
					Extension24.Set(0);
					break;
				}
				for (int i=0;i<=9;i++)
				if (swingLowTime[i]>lowExtFibTime[2] && swingLow[i]<Extension26[0])
				{
					Extension25.Set(0);
					break;
				}
			}
			else
			{
				Extension24.Set(0);
				Extension25.Set(0);
				Extension26.Set(0);
				lowExtDate[2].Set(Cbi.Globals.MinDate);
			}
			
			if (lowExtIndex>2 && Show_Xtensions)
			{
				fib1000 = lowExtFib[3]/TickSize;
				fib000 = coupledHighExtFib[3]/TickSize;
				Extension27.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				Extension28.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				Extension29.Set(TickSize*Math.Round(fib000+2.0*(fib1000-fib000)));
				lowExtDate[3].Set(lowExtFibTime[3]);
			
				for (int i=0;i<=9;i++)
				if (swingLowTime[i]>lowExtFibTime[3] && swingLow[i]<Extension28[0])
				{
					Extension27.Set(0);
					break;
				}
				for (int i=0;i<=9;i++)
				if (swingLowTime[i]>lowExtFibTime[3] && swingLow[i]<Extension29[0])
				{
					Extension28.Set(0);
					break;
				}
			}
			else
			{
				Extension27.Set(0);
				Extension28.Set(0);
				Extension29.Set(0);
				lowExtDate[3].Set(Cbi.Globals.MinDate);
			}
			
			if (lowExtIndex>3 && Show_Xtensions)
			{
				fib1000 = lowExtFib[4]/TickSize;
				fib000 = coupledHighExtFib[4]/TickSize;
				Extension30.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				Extension31.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				Extension32.Set(TickSize*Math.Round(fib000+2.0*(fib1000-fib000)));
				lowExtDate[4].Set(lowExtFibTime[4]);
				for (int i=0;i<=9;i++)
				if (swingLowTime[i]>lowExtFibTime[4] && swingLow[i]<Extension31[0])
				{
					Extension30.Set(0);
					break;
				}
				for (int i=0;i<=9;i++)
				if (swingLowTime[i]>lowExtFibTime[4] && swingLow[i]<Extension32[0])
				{
					Extension31.Set(0);
					break;
				}
			}
			else
			{
				Extension30.Set(0);
				Extension31.Set(0);
				Extension32.Set(0);
				lowExtDate[4].Set(Cbi.Globals.MinDate);
			}
			
			if (lowExtIndex>4 && Show_Xtensions)
			{
				fib1000 = lowExtFib[5]/TickSize;
				fib000 = coupledHighExtFib[5]/TickSize;
				Extension33.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				Extension34.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				Extension35.Set(TickSize*Math.Round(fib000+2.0*(fib1000-fib000)));
				lowExtDate[5].Set(lowExtFibTime[5]);
				for (int i=0;i<=9;i++)
				if (swingLowTime[i]>lowExtFibTime[5] && swingLow[i]<Extension34[0])
				{
					Extension33.Set(0);
					break;
				}
				for (int i=0;i<=9;i++)
				if (swingLowTime[i]>lowExtFibTime[5] && swingLow[i]<Extension35[0])
				{
					Extension34.Set(0);
					break;
				}
			}
			else
			{
				Extension33.Set(0);
				Extension34.Set(0);
				Extension35.Set(0);
				lowExtDate[5].Set(Cbi.Globals.MinDate);
			}
	
			if (sessionNumber > 0 && recentUp == true && Show_Z_Plus && Data.BarsType.GetInstance(Bars.Period.Id).IsIntraday)
			{
				fib1000 = recentHigh/TickSize;
				fib000 = recentCoupledLow/TickSize;
				if (runningHigh<=recentHigh+0.618*(recentHigh-recentCoupledLow))
					ZplusFib000.Set(TickSize*Math.Round(fib000));
				else
					ZplusFib000.Set(0);
				if (!zExtensionUp)
				{	
					ZplusFib236.Set(TickSize*Math.Round(fib000+0.236*(fib1000-fib000)));
					ZplusFib382.Set(TickSize*Math.Round(fib000+0.382*(fib1000-fib000)));
					ZplusFib500.Set(TickSize*Math.Round(fib000+0.500*(fib1000-fib000)));
					ZplusFib618.Set(TickSize*Math.Round(fib000+0.618*(fib1000-fib000)));
					ZplusFib764.Set(TickSize*Math.Round(fib000+0.764*(fib1000-fib000)));
				}
				else
				{	
					ZplusFib236.Set(0);
					ZplusFib382.Set(0);
					ZplusFib500.Set(0);
					ZplusFib618.Set(0);
					ZplusFib764.Set(0);
				}
				ZplusFib1000.Set(TickSize*Math.Round(fib1000));
				if (runningHigh<=recentHigh+0.618*(recentHigh-recentCoupledLow))  // CHANGED HERE
					ZplusFib1272.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				else
					ZplusFib1272.Set(0);
				if (runningHigh<=recentHigh+(recentHigh-recentCoupledLow)) // CHANGED HERE
					ZplusFib1618.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				else
					ZplusFib1618.Set(0);
				recentHighDate.Set(recentHighTime);
			}
			else
			{
				ZplusFib000.Set(0);
				ZplusFib236.Set(0);
				ZplusFib382.Set(0);
				ZplusFib500.Set(0);
				ZplusFib618.Set(0);
				ZplusFib764.Set(0);
				ZplusFib1000.Set(0);
				ZplusFib1272.Set(0);
				ZplusFib1618.Set(0);
				recentHighDate.Set(Cbi.Globals.MinDate);
			}	
			
			if (sessionNumber > 0 && recentDown == true && Show_Z_Minus && Data.BarsType.GetInstance(Bars.Period.Id).IsIntraday)
			{
				fib1000 = recentLow/TickSize;
				fib000 = recentCoupledHigh/TickSize;
				if (runningLow >= recentLow-0.618*(recentCoupledHigh-recentLow))
					ZminusFib000.Set(TickSize*Math.Round(fib000));
				else
					ZminusFib000.Set(0);
				if (!zExtensionDown)
				{	
					ZminusFib236.Set(TickSize*Math.Round(fib000+0.236*(fib1000-fib000)));
					ZminusFib382.Set(TickSize*Math.Round(fib000+0.382*(fib1000-fib000)));
					ZminusFib500.Set(TickSize*Math.Round(fib000+0.500*(fib1000-fib000)));
					ZminusFib618.Set(TickSize*Math.Round(fib000+0.618*(fib1000-fib000)));
					ZminusFib764.Set(TickSize*Math.Round(fib000+0.764*(fib1000-fib000)));
				}
				else
				{	
					ZminusFib236.Set(0);
					ZminusFib382.Set(0);
					ZminusFib500.Set(0);
					ZminusFib618.Set(0);
					ZminusFib764.Set(0);
				}
				ZminusFib1000.Set(TickSize*Math.Round(fib1000));
				if (runningLow >= recentLow-0.618*(recentCoupledHigh-recentLow)) // CHANGED HERE
					ZminusFib1272.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				else
					ZminusFib1272.Set(0);
				if (runningLow >= recentLow-(recentCoupledHigh-recentLow))  // CHANGED HERE
					ZminusFib1618.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				else
					ZminusFib1618.Set(0);
				recentLowDate.Set(recentLowTime);
			}
			else
			{
				ZminusFib000.Set(0);
				ZminusFib236.Set(0);
				ZminusFib382.Set(0);
				ZminusFib500.Set(0);
				ZminusFib618.Set(0);
				ZminusFib764.Set(0);
				ZminusFib1000.Set(0);
				ZminusFib1272.Set(0);
				ZminusFib1618.Set(0);
				recentLowDate.Set(Cbi.Globals.MinDate);
			}

			if (sessionNumber > 2 && runningHighIndex > -1 && Show_Y_Plus && Data.BarsType.GetInstance(Bars.Period.Id).IsIntraday)
			{
				fib1000 = lastHighFib/TickSize;
				fib000 = lastCoupledLowFib/TickSize;
				YplusFib000.Set(TickSize*Math.Round(fib000));
				if (runningHigh < lastHighFib)
				{
					YplusFib236.Set(TickSize*Math.Round(fib000+0.236*(fib1000-fib000)));
					YplusFib382.Set(TickSize*Math.Round(fib000+0.382*(fib1000-fib000)));
					YplusFib500.Set(TickSize*Math.Round(fib000+0.500*(fib1000-fib000)));
					YplusFib618.Set(TickSize*Math.Round(fib000+0.618*(fib1000-fib000)));
					YplusFib764.Set(TickSize*Math.Round(fib000+0.764*(fib1000-fib000)));
				}
				else
				{	
					YplusFib236.Set(0);
					YplusFib382.Set(0);
					YplusFib500.Set(0);
					YplusFib618.Set(0);
					YplusFib764.Set(0);
				}
				YplusFib1000.Set(TickSize*Math.Round(fib1000));
				if (runningHigh < lastHighFib || runningLow > lastCoupledLowFib || runningLowTime<runningHighTime)
				{
					YplusFib1272.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
					YplusFib1618.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				}
				else
				{	
					YplusFib1272.Set(0);
					YplusFib1618.Set(0);
				}						
				lastHighDate.Set(lastHighFibTime);
			}
			else
			{
				YplusFib000.Set(0);
				YplusFib236.Set(0);
				YplusFib382.Set(0);
				YplusFib500.Set(0);
				YplusFib618.Set(0);
				YplusFib764.Set(0);
				YplusFib1000.Set(0);
				YplusFib1272.Set(0);
				YplusFib1618.Set(0);
				lastHighDate.Set(Cbi.Globals.MinDate);
			}	
			
			if (sessionNumber > 2 && runningLowIndex > -1 && Show_Y_Minus && Data.BarsType.GetInstance(Bars.Period.Id).IsIntraday)
			{
				fib1000 = lastLowFib/TickSize;
				fib000 = lastCoupledHighFib/TickSize;
				YminusFib000.Set(TickSize*Math.Round(fib000));
				if (runningLow > lastLowFib)
				{
					YminusFib236.Set(TickSize*Math.Round(fib000+0.236*(fib1000-fib000)));
					YminusFib382.Set(TickSize*Math.Round(fib000+0.382*(fib1000-fib000)));
					YminusFib500.Set(TickSize*Math.Round(fib000+0.500*(fib1000-fib000)));
					YminusFib618.Set(TickSize*Math.Round(fib000+0.618*(fib1000-fib000)));
					YminusFib764.Set(TickSize*Math.Round(fib000+0.764*(fib1000-fib000)));
				}
				else
				{	
					YminusFib236.Set(0);
					YminusFib382.Set(0);
					YminusFib500.Set(0);
					YminusFib618.Set(0);
					YminusFib764.Set(0);
				}
	
				YminusFib1000.Set(TickSize*Math.Round(fib1000));
				if (runningLow > lastLowFib || runningHigh < lastCoupledHighFib || runningHighTime<runningLowTime)
				{
				YminusFib1272.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				YminusFib1618.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				}
				else
				{	
					YminusFib1272.Set(0);
					YminusFib1618.Set(0);
				}	
				lastLowDate.Set(lastLowFibTime);
			}
			else
			{
				YminusFib000.Set(0);
				YminusFib236.Set(0);
				YminusFib382.Set(0);
				YminusFib500.Set(0);
				YminusFib618.Set(0);
				YminusFib764.Set(0);
				YminusFib1000.Set(0);
				YminusFib1272.Set(0);
				YminusFib1618.Set(0);
				lastLowDate.Set(Cbi.Globals.MinDate);
			}

			if(highFibIndex > -1 && Show_S_Plus && Data.BarsType.GetInstance(Bars.Period.Id).IsIntraday)
			{
				fib1000 = highFib[0]/TickSize;
				fib000 = Math.Min(coupledLowFib[0],runningLow)/TickSize;
				SplusFib000.Set(TickSize*Math.Round(fib000));
				if (runningHigh < highFib[0])
				{	
					SplusFib236.Set(TickSize*Math.Round(fib000+0.236*(fib1000-fib000)));
					SplusFib382.Set(TickSize*Math.Round(fib000+0.382*(fib1000-fib000)));
					SplusFib500.Set(TickSize*Math.Round(fib000+0.500*(fib1000-fib000)));
					SplusFib618.Set(TickSize*Math.Round(fib000+0.618*(fib1000-fib000)));
					SplusFib764.Set(TickSize*Math.Round(fib000+0.764*(fib1000-fib000)));
				}
				else
				{	
					SplusFib236.Set(0);
					SplusFib382.Set(0);
					SplusFib500.Set(0);
					SplusFib618.Set(0);
					SplusFib764.Set(0);
				}
				SplusFib1000.Set(TickSize*Math.Round(fib1000));
				if (runningHigh < highFib[0] || runningLow > coupledLowFib[0] || runningLowTime<runningHighTime)
				{
					SplusFib1272.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
					SplusFib1618.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				}
				else
				{	
					SplusFib1272.Set(0);
					SplusFib1618.Set(0);
				}						
				highDate[0].Set(highFibTime[0]);
			}
			else
			{
				SplusFib000.Set(0);
				SplusFib236.Set(0);
				SplusFib382.Set(0);
				SplusFib500.Set(0);
				SplusFib618.Set(0);
				SplusFib764.Set(0);
				SplusFib1000.Set(0);
				SplusFib1272.Set(0);
				SplusFib1618.Set(0);
				highDate[0].Set(Cbi.Globals.MinDate);
			}	

			if(lowFibIndex > -1 && Show_S_Minus && Data.BarsType.GetInstance(Bars.Period.Id).IsIntraday)
			{
				fib1000 = lowFib[0]/TickSize;
				fib000 = Math.Max(coupledHighFib[0],runningHigh)/TickSize;
				
				SminusFib000.Set(TickSize*Math.Round(fib000));
				if (runningLow > lowFib[0])
				{	
					SminusFib236.Set(TickSize*Math.Round(fib000+0.236*(fib1000-fib000)));
					SminusFib382.Set(TickSize*Math.Round(fib000+0.382*(fib1000-fib000)));
					SminusFib500.Set(TickSize*Math.Round(fib000+0.500*(fib1000-fib000)));
					SminusFib618.Set(TickSize*Math.Round(fib000+0.618*(fib1000-fib000)));
					SminusFib764.Set(TickSize*Math.Round(fib000+0.764*(fib1000-fib000)));
				}
				else
				{	
					SminusFib236.Set(0);
					SminusFib382.Set(0);
					SminusFib500.Set(0);
					SminusFib618.Set(0);
					SminusFib764.Set(0);
				}
				SminusFib1000.Set(TickSize*Math.Round(fib1000));
				if (runningLow > lowFib[0] || runningHigh < coupledHighFib[0] || runningHighTime<runningLowTime)
				{
				SminusFib1272.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				SminusFib1618.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				}
				else
				{	
					SminusFib1272.Set(0);
					SminusFib1618.Set(0);
				}						
				lowDate[0].Set(lowFibTime[0]);
			}
			else
			{
				SminusFib000.Set(0);
				SminusFib236.Set(0);
				SminusFib382.Set(0);
				SminusFib500.Set(0);
				SminusFib618.Set(0);
				SminusFib764.Set(0);
				SminusFib1000.Set(0);
				SminusFib1272.Set(0);
				SminusFib1618.Set(0);
				lowDate[0].Set(Cbi.Globals.MinDate);
			}
			
			if(highFibIndex > 0 && Show_E_Plus)
			{
				fib1000 = highFib[1]/TickSize;
				fib000 = Math.Min(coupledLowFib[1],runningLow)/TickSize;
				EplusFib000.Set(TickSize*Math.Round(fib000));
				if (runningHigh < highFib[1])
				{	
					EplusFib236.Set(TickSize*Math.Round(fib000+0.236*(fib1000-fib000)));
					EplusFib382.Set(TickSize*Math.Round(fib000+0.382*(fib1000-fib000)));
					EplusFib500.Set(TickSize*Math.Round(fib000+0.500*(fib1000-fib000)));
					EplusFib618.Set(TickSize*Math.Round(fib000+0.618*(fib1000-fib000)));
					EplusFib764.Set(TickSize*Math.Round(fib000+0.764*(fib1000-fib000)));
				}
				else
				{	
					EplusFib236.Set(0);
					EplusFib382.Set(0);
					EplusFib500.Set(0);
					EplusFib618.Set(0);
					EplusFib764.Set(0);
				}
				EplusFib1000.Set(TickSize*Math.Round(fib1000));
				if (runningHigh < highFib[1] || runningLow > coupledLowFib[1] || runningLowTime<runningHighTime)
				{
					EplusFib1272.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
					EplusFib1618.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				}
				else
				{	
					EplusFib1272.Set(0);
					EplusFib1618.Set(0);
				}						
				highDate[1].Set(highFibTime[1]);
			}				
			else
			{
				EplusFib000.Set(0);
				EplusFib236.Set(0);
				EplusFib382.Set(0);
				EplusFib500.Set(0);
				EplusFib618.Set(0);
				EplusFib764.Set(0);
				EplusFib1000.Set(0);
				EplusFib1272.Set(0);
				EplusFib1618.Set(0);
				highDate[1].Set(Cbi.Globals.MinDate);
			}	

			if(lowFibIndex > 0 && Show_E_Minus)
			{
				fib1000 = lowFib[1]/TickSize;
				fib000 = Math.Max(coupledHighFib[1],runningHigh)/TickSize;
				EminusFib000.Set(TickSize*Math.Round(fib000));
				if (runningLow > lowFib[1])
				{	
					EminusFib236.Set(TickSize*Math.Round(fib000+0.236*(fib1000-fib000)));
					EminusFib382.Set(TickSize*Math.Round(fib000+0.382*(fib1000-fib000)));
					EminusFib500.Set(TickSize*Math.Round(fib000+0.500*(fib1000-fib000)));
					EminusFib618.Set(TickSize*Math.Round(fib000+0.618*(fib1000-fib000)));
					EminusFib764.Set(TickSize*Math.Round(fib000+0.764*(fib1000-fib000)));
				}
				else
				{	
					EminusFib236.Set(0);
					EminusFib382.Set(0);
					EminusFib500.Set(0);
					EminusFib618.Set(0);
					EminusFib764.Set(0);
				}
				EminusFib1000.Set(TickSize*Math.Round(fib1000));
				if (runningLow > lowFib[1] || runningHigh < coupledHighFib[1] || runningHighTime<runningLowTime)
				{
				EminusFib1272.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				EminusFib1618.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				}
				else
				{	
					EminusFib1272.Set(0);
					EminusFib1618.Set(0);
				}						
				lowDate[1].Set(lowFibTime[1]);
			}
			else
			{
				EminusFib000.Set(0);
				EminusFib236.Set(0);
				EminusFib382.Set(0);
				EminusFib500.Set(0);
				EminusFib618.Set(0);
				EminusFib764.Set(0);
				EminusFib1000.Set(0);
				EminusFib1272.Set(0);
				EminusFib1618.Set(0);
				lowDate[1].Set(Cbi.Globals.MinDate);
			}				
			
			if(highFibIndex > 1 && Show_D_Plus)
			{
				fib1000 = highFib[2]/TickSize;
				fib000 = Math.Min(coupledLowFib[2],runningLow)/TickSize;
				DplusFib000.Set(TickSize*Math.Round(fib000));
				if (runningHigh < highFib[2])
				{	
					DplusFib236.Set(TickSize*Math.Round(fib000+0.236*(fib1000-fib000)));
					DplusFib382.Set(TickSize*Math.Round(fib000+0.382*(fib1000-fib000)));
					DplusFib500.Set(TickSize*Math.Round(fib000+0.500*(fib1000-fib000)));
					DplusFib618.Set(TickSize*Math.Round(fib000+0.618*(fib1000-fib000)));
					DplusFib764.Set(TickSize*Math.Round(fib000+0.764*(fib1000-fib000)));
				}
				else
				{	
					DplusFib236.Set(0);
					DplusFib382.Set(0);
					DplusFib500.Set(0);
					DplusFib618.Set(0);
					DplusFib764.Set(0);
				}
				DplusFib1000.Set(TickSize*Math.Round(fib1000));
				if (runningHigh < highFib[2] || runningLow > coupledLowFib[2] || runningLowTime<runningHighTime)
				{
					DplusFib1272.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
					DplusFib1618.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				}
				else
				{	
					DplusFib1272.Set(0);
					DplusFib1618.Set(0);
				}						
				highDate[2].Set(highFibTime[2]);
			}
			else
			{
				DplusFib000.Set(0);
				DplusFib236.Set(0);
				DplusFib382.Set(0);
				DplusFib500.Set(0);
				DplusFib618.Set(0);
				DplusFib764.Set(0);
				DplusFib1000.Set(0);
				DplusFib1272.Set(0);
				DplusFib1618.Set(0);
				highDate[2].Set(Cbi.Globals.MinDate);
			}	
			
			if(lowFibIndex > 1 && Show_D_Minus)
			{
				fib1000 = lowFib[2]/TickSize;
				fib000 = Math.Max(coupledHighFib[2],runningHigh)/TickSize;
				DminusFib000.Set(TickSize*Math.Round(fib000));
				if (runningLow > lowFib[2])
				{	
					DminusFib236.Set(TickSize*Math.Round(fib000+0.236*(fib1000-fib000)));
					DminusFib382.Set(TickSize*Math.Round(fib000+0.382*(fib1000-fib000)));
					DminusFib500.Set(TickSize*Math.Round(fib000+0.500*(fib1000-fib000)));
					DminusFib618.Set(TickSize*Math.Round(fib000+0.618*(fib1000-fib000)));
					DminusFib764.Set(TickSize*Math.Round(fib000+0.764*(fib1000-fib000)));
				}
				else
				{	
					DminusFib236.Set(0);
					DminusFib382.Set(0);
					DminusFib500.Set(0);
					DminusFib618.Set(0);
					DminusFib764.Set(0);
				}
				DminusFib1000.Set(TickSize*Math.Round(fib1000));
				if (runningLow > lowFib[2] || runningHigh < coupledHighFib[2] || runningHighTime<runningLowTime)
				{
				DminusFib1272.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				DminusFib1618.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				}
				else
				{	
					DminusFib1272.Set(0);
					DminusFib1618.Set(0);
				}						
				lowDate[2].Set(lowFibTime[2]);
			}
			else
			{
				DminusFib000.Set(0);
				DminusFib236.Set(0);
				DminusFib382.Set(0);
				DminusFib500.Set(0);
				DminusFib618.Set(0);
				DminusFib764.Set(0);
				DminusFib1000.Set(0);
				DminusFib1272.Set(0);
				DminusFib1618.Set(0);
				lowDate[2].Set(Cbi.Globals.MinDate);
			}	
			
			if(highFibIndex > 2 && Show_C_Plus)
			{
				fib1000 = highFib[3]/TickSize;
				fib000 = Math.Min(coupledLowFib[3],runningLow)/TickSize;
				CplusFib000.Set(TickSize*Math.Round(fib000));
				if (runningHigh < highFib[3])
				{	
					CplusFib236.Set(TickSize*Math.Round(fib000+0.236*(fib1000-fib000)));
					CplusFib382.Set(TickSize*Math.Round(fib000+0.382*(fib1000-fib000)));
					CplusFib500.Set(TickSize*Math.Round(fib000+0.500*(fib1000-fib000)));
					CplusFib618.Set(TickSize*Math.Round(fib000+0.618*(fib1000-fib000)));
					CplusFib764.Set(TickSize*Math.Round(fib000+0.764*(fib1000-fib000)));
				}
				else
				{	
					CplusFib236.Set(0);
					CplusFib382.Set(0);
					CplusFib500.Set(0);
					CplusFib618.Set(0);
					CplusFib764.Set(0);
				}
				CplusFib1000.Set(TickSize*Math.Round(fib1000));
				if (runningHigh < highFib[3] || runningLow > coupledLowFib[3] || runningLowTime<runningHighTime)
				{
					CplusFib1272.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
					CplusFib1618.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				}
				else
				{	
					CplusFib1272.Set(0);
					CplusFib1618.Set(0);
				}						
				highDate[3].Set(highFibTime[3]);
			}
			else
			{
				CplusFib000.Set(0);
				CplusFib236.Set(0);
				CplusFib382.Set(0);
				CplusFib500.Set(0);
				CplusFib618.Set(0);
				CplusFib764.Set(0);
				CplusFib1000.Set(0);
				CplusFib1272.Set(0);
				CplusFib1618.Set(0);
				highDate[3].Set(Cbi.Globals.MinDate);
			}	

			if(lowFibIndex > 2 && Show_C_Minus)
			{
				fib1000 = lowFib[3]/TickSize;
				fib000 = Math.Max(coupledHighFib[3],runningHigh)/TickSize;
				CminusFib000.Set(TickSize*Math.Round(fib000));
				if (runningLow > lowFib[3])
				{	
					CminusFib236.Set(TickSize*Math.Round(fib000+0.236*(fib1000-fib000)));
					CminusFib382.Set(TickSize*Math.Round(fib000+0.382*(fib1000-fib000)));
					CminusFib500.Set(TickSize*Math.Round(fib000+0.500*(fib1000-fib000)));
					CminusFib618.Set(TickSize*Math.Round(fib000+0.618*(fib1000-fib000)));
					CminusFib764.Set(TickSize*Math.Round(fib000+0.764*(fib1000-fib000)));
				}
				else
				{	
					CminusFib236.Set(0);
					CminusFib382.Set(0);
					CminusFib500.Set(0);
					CminusFib618.Set(0);
					CminusFib764.Set(0);
				}
				CminusFib1000.Set(TickSize*Math.Round(fib1000));
				if (runningLow > lowFib[3] || runningHigh < coupledHighFib[3] || runningHighTime<runningLowTime)
				{
				CminusFib1272.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				CminusFib1618.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				}
				else
				{	
					CminusFib1272.Set(0);
					CminusFib1618.Set(0);
				}						
				lowDate[3].Set(lowFibTime[3]);
			}
			else
			{
				CminusFib000.Set(0);
				CminusFib236.Set(0);
				CminusFib382.Set(0);
				CminusFib500.Set(0);
				CminusFib618.Set(0);
				CminusFib764.Set(0);
				CminusFib1000.Set(0);
				CminusFib1272.Set(0);
				CminusFib1618.Set(0);
				lowDate[3].Set(Cbi.Globals.MinDate);
			}	
		
			if(highFibIndex > 3 && Show_B_Plus)
			{
				fib1000 = highFib[4]/TickSize;
				fib000 = Math.Min(coupledLowFib[4],runningLow)/TickSize;
				BplusFib000.Set(TickSize*Math.Round(fib000));
				if (runningHigh < highFib[4])
				{	
					BplusFib236.Set(TickSize*Math.Round(fib000+0.236*(fib1000-fib000)));
					BplusFib382.Set(TickSize*Math.Round(fib000+0.382*(fib1000-fib000)));
					BplusFib500.Set(TickSize*Math.Round(fib000+0.500*(fib1000-fib000)));
					BplusFib618.Set(TickSize*Math.Round(fib000+0.618*(fib1000-fib000)));
					BplusFib764.Set(TickSize*Math.Round(fib000+0.764*(fib1000-fib000)));
				}
				else
				{	
					BplusFib236.Set(0);
					BplusFib382.Set(0);
					BplusFib500.Set(0);
					BplusFib618.Set(0);
					BplusFib764.Set(0);
				}
				BplusFib1000.Set(TickSize*Math.Round(fib1000));
				if (runningHigh < highFib[4] || runningLow > coupledLowFib[4] || runningLowTime<runningHighTime)
				{
					BplusFib1272.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
					BplusFib1618.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				}
				else
				{	
					BplusFib1272.Set(0);
					BplusFib1618.Set(0);
				}						
				highDate[4].Set(highFibTime[4]);
			}			
			else
			{
				BplusFib000.Set(0);
				BplusFib236.Set(0);
				BplusFib382.Set(0);
				BplusFib500.Set(0);
				BplusFib618.Set(0);
				BplusFib764.Set(0);
				BplusFib1000.Set(0);
				BplusFib1272.Set(0);
				BplusFib1618.Set(0);
				highDate[4].Set(Cbi.Globals.MinDate);
			}	

			if(lowFibIndex > 3 && Show_B_Minus)
			{
				fib1000 = lowFib[4]/TickSize;
				fib000 = Math.Max(coupledHighFib[4],runningHigh)/TickSize;
				BminusFib000.Set(TickSize*Math.Round(fib000));
				if (runningLow > lowFib[4])
				{	
					BminusFib236.Set(TickSize*Math.Round(fib000+0.236*(fib1000-fib000)));
					BminusFib382.Set(TickSize*Math.Round(fib000+0.382*(fib1000-fib000)));
					BminusFib500.Set(TickSize*Math.Round(fib000+0.500*(fib1000-fib000)));
					BminusFib618.Set(TickSize*Math.Round(fib000+0.618*(fib1000-fib000)));
					BminusFib764.Set(TickSize*Math.Round(fib000+0.764*(fib1000-fib000)));
				}
				else
				{	
					BminusFib236.Set(0);
					BminusFib382.Set(0);
					BminusFib500.Set(0);
					BminusFib618.Set(0);
					BminusFib764.Set(0);
				}
				BminusFib1000.Set(TickSize*Math.Round(fib1000));
				if (runningLow > lowFib[4] || runningHigh < coupledHighFib[4] || runningHighTime<runningLowTime)
				{
				BminusFib1272.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				BminusFib1618.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				}
				else
				{	
					BminusFib1272.Set(0);
					BminusFib1618.Set(0);
				}						
				lowDate[4].Set(lowFibTime[4]);
			}			
			else
			{
				BminusFib000.Set(0);
				BminusFib236.Set(0);
				BminusFib382.Set(0);
				BminusFib500.Set(0);
				BminusFib618.Set(0);
				BminusFib764.Set(0);
				BminusFib1000.Set(0);
				BminusFib1272.Set(0);
				BminusFib1618.Set(0);
				lowDate[4].Set(Cbi.Globals.MinDate);
			}	
		
			if(highFibIndex > 4 && Show_A_Plus)
			{
				fib1000 = highFib[5]/TickSize;
				fib000 = Math.Min(coupledLowFib[5],runningLow)/TickSize;
				AplusFib000.Set(TickSize*Math.Round(fib000));
				if (runningHigh < highFib[5])
				{	
					AplusFib236.Set(TickSize*Math.Round(fib000+0.236*(fib1000-fib000)));
					AplusFib382.Set(TickSize*Math.Round(fib000+0.382*(fib1000-fib000)));
					AplusFib500.Set(TickSize*Math.Round(fib000+0.500*(fib1000-fib000)));
					AplusFib618.Set(TickSize*Math.Round(fib000+0.618*(fib1000-fib000)));
					AplusFib764.Set(TickSize*Math.Round(fib000+0.764*(fib1000-fib000)));
				}
				else
				{	
					AplusFib236.Set(0);
					AplusFib382.Set(0);
					AplusFib500.Set(0);
					AplusFib618.Set(0);
					AplusFib764.Set(0);
				}
				AplusFib1000.Set(TickSize*Math.Round(fib1000));
				if (runningHigh < highFib[5] || runningLow > coupledLowFib[5] || runningLowTime<runningHighTime)
				{
					AplusFib1272.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
					AplusFib1618.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				}
				else
				{	
					AplusFib1272.Set(0);
					AplusFib1618.Set(0);
				}						
				highDate[5].Set(highFibTime[5]);
			}						
			else
			{
				AplusFib000.Set(0);
				AplusFib236.Set(0);
				AplusFib382.Set(0);
				AplusFib500.Set(0);
				AplusFib618.Set(0);
				AplusFib764.Set(0);
				AplusFib1000.Set(0);
				AplusFib1272.Set(0);
				AplusFib1618.Set(0);
				highDate[5].Set(Cbi.Globals.MinDate);
			}	
		
			if(lowFibIndex > 4 && Show_A_Minus)
			{
				fib1000 = lowFib[5]/TickSize;
				fib000 = Math.Max(coupledHighFib[5],runningHigh)/TickSize;
				AminusFib000.Set(TickSize*Math.Round(fib000));
				if (runningLow > lowFib[5])
				{	
					AminusFib236.Set(TickSize*Math.Round(fib000+0.236*(fib1000-fib000)));
					AminusFib382.Set(TickSize*Math.Round(fib000+0.382*(fib1000-fib000)));
					AminusFib500.Set(TickSize*Math.Round(fib000+0.500*(fib1000-fib000)));
					AminusFib618.Set(TickSize*Math.Round(fib000+0.618*(fib1000-fib000)));
					AminusFib764.Set(TickSize*Math.Round(fib000+0.764*(fib1000-fib000)));
				}
				else
				{	
					AminusFib236.Set(0);
					AminusFib382.Set(0);
					AminusFib500.Set(0);
					AminusFib618.Set(0);
					AminusFib764.Set(0);
				}
				AminusFib1000.Set(TickSize*Math.Round(fib1000));
				if (runningLow > lowFib[5] || runningHigh < coupledHighFib[5] || runningHighTime<runningLowTime)
				{
				AminusFib1272.Set(TickSize*Math.Round(fib000+1.272*(fib1000-fib000)));
				AminusFib1618.Set(TickSize*Math.Round(fib000+1.618*(fib1000-fib000)));
				}
				else
				{	
					AminusFib1272.Set(0);
					AminusFib1618.Set(0);
				}						
				lowDate[5].Set(lowFibTime[5]);
			}
			else
			{
				AminusFib000.Set(0);
				AminusFib236.Set(0);
				AminusFib382.Set(0);
				AminusFib500.Set(0);
				AminusFib618.Set(0);
				AminusFib764.Set(0);
				AminusFib1000.Set(0);
				AminusFib1272.Set(0);
				AminusFib1618.Set(0);
				lowDate[5].Set(Cbi.Globals.MinDate);
			}	
		
			if (coveredHighTime[0] > Cbi.Globals.MinDate && Show_Highs_Lows == true)
			{
				HiddenHigh0.Set(TickSize*Math.Round(coveredHigh[0]/TickSize));
				hiddenHighDate[0].Set(coveredHighTime[0]);
			}
			else
			{
				HiddenHigh0.Set(0);
				hiddenHighDate[0].Set(Cbi.Globals.MinDate);
			}

			if (coveredHighTime[1] > Cbi.Globals.MinDate && Show_Highs_Lows == true)
			{
				HiddenHigh1.Set(TickSize*Math.Round(coveredHigh[1]/TickSize));
				hiddenHighDate[1].Set(coveredHighTime[1]);
			}
			else
			{
				HiddenHigh1.Set(0);
				hiddenHighDate[1].Set(Cbi.Globals.MinDate);
			}

			if (coveredHighTime[2] > Cbi.Globals.MinDate && Show_Highs_Lows == true)
			{
				HiddenHigh2.Set(TickSize*Math.Round(coveredHigh[2]/TickSize));
				hiddenHighDate[2].Set(coveredHighTime[2]);
			}
			else
			{
				HiddenHigh2.Set(0);
				hiddenHighDate[2].Set(Cbi.Globals.MinDate);
			}
							
			if (coveredHighTime[3] > Cbi.Globals.MinDate && Show_Highs_Lows == true)
			{
				HiddenHigh3.Set(TickSize*Math.Round(coveredHigh[3]/TickSize));
				hiddenHighDate[3].Set(coveredHighTime[3]);
			}
			else
			{
				HiddenHigh3.Set(0);
				hiddenHighDate[3].Set(Cbi.Globals.MinDate);
			}
							
			if (coveredHighTime[4] > Cbi.Globals.MinDate && Show_Highs_Lows == true)
			{
				HiddenHigh4.Set(TickSize*Math.Round(coveredHigh[4]/TickSize));
				hiddenHighDate[4].Set(coveredHighTime[4]);
			}
			else
			{
				HiddenHigh4.Set(0);
				hiddenHighDate[4].Set(Cbi.Globals.MinDate);
			}
							
			if (coveredHighTime[5] > Cbi.Globals.MinDate && Show_Highs_Lows == true)
			{
				HiddenHigh5.Set(TickSize*Math.Round(coveredHigh[5]/TickSize));
				hiddenHighDate[5].Set(coveredHighTime[5]);
			}
			else
			{
				HiddenHigh5.Set(0);
				hiddenHighDate[5].Set(Cbi.Globals.MinDate);
			}
							
			if (coveredLowTime[0] > Cbi.Globals.MinDate && Show_Highs_Lows == true)
			{
				HiddenLow0.Set(TickSize*Math.Round(coveredLow[0]/TickSize));
				hiddenLowDate[0].Set(coveredLowTime[0]);
			}
			else
			{
				HiddenLow0.Set(0);
				hiddenLowDate[0].Set(Cbi.Globals.MinDate);
			}
			
			if (coveredLowTime[1] > Cbi.Globals.MinDate && Show_Highs_Lows == true)
			{
				HiddenLow1.Set(TickSize*Math.Round(coveredLow[1]/TickSize));
				hiddenLowDate[1].Set(coveredLowTime[1]);
			}
			else
			{
				HiddenLow1.Set(0);
				hiddenLowDate[1].Set(Cbi.Globals.MinDate);
			}
							
			if (coveredLowTime[2] > Cbi.Globals.MinDate && Show_Highs_Lows == true)
			{
				HiddenLow2.Set(TickSize*Math.Round(coveredLow[2]/TickSize));
				hiddenLowDate[2].Set(coveredLowTime[2]);
			}
			else
			{
				HiddenLow2.Set(0);
				hiddenLowDate[2].Set(Cbi.Globals.MinDate);
			}
							
			if (coveredLowTime[3] > Cbi.Globals.MinDate && Show_Highs_Lows == true)
			{
				HiddenLow3.Set(TickSize*Math.Round(coveredLow[3]/TickSize));
				hiddenLowDate[3].Set(coveredLowTime[3]);
			}
			else
			{
				HiddenLow3.Set(0);
				hiddenLowDate[3].Set(Cbi.Globals.MinDate);
			}
							
			if (coveredLowTime[4] > Cbi.Globals.MinDate && Show_Highs_Lows == true)
			{
				HiddenLow4.Set(TickSize*Math.Round(coveredLow[4]/TickSize));
				hiddenLowDate[4].Set(coveredLowTime[4]);
			}
			else
			{
				HiddenLow4.Set(0);
				hiddenLowDate[4].Set(Cbi.Globals.MinDate);
			}
							
			if (coveredLowTime[5] > Cbi.Globals.MinDate && Show_Highs_Lows == true)
			{
				HiddenLow5.Set(TickSize*Math.Round(coveredLow[5]/TickSize));
				hiddenLowDate[5].Set(coveredLowTime[5]);
			}
			else
			{
				HiddenLow5.Set(0);
				hiddenLowDate[5].Set(Cbi.Globals.MinDate);
			}
		}

		#region Properties
		
		/// <summary>
		/// </summary>
		[Description("Distance of label from line.")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Label Position")]
		public int LabelPosition
		{
			get { return labelPosition; }
			set { labelPosition = Math.Max(1, value); }
		}
	
		///<summary
		///</summary>
		[XmlIgnore]
		[Browsable(false)]
		public TimeSpan Offset
		{
			get { return offset;}
			set { offset = value;}
		}	
	
		///<summary
		///</summary>
		[Description("For RTH pivots enter RTH session length. RTH session end is taken from session template. Only with CalcFromIntradayData.")]
		[Category("\rUser defined values")]
		[Gui.Design.DisplayNameAttribute("Session Length RTH")]
		public string S_Offset	
		{
			get 
			{ 
				return string.Format("{0:D2}:{1:D2}", offset.Hours, offset.Minutes);
			}
			set 
			{ 
			string[]values =((string)value).Split(':');
			offset = new TimeSpan(Convert.ToInt16(values[0]),Convert.ToInt16(values[1]),0);
			}
		}
		
		/// <summary>
		/// </summary>
		[Description("Option to show S+ Fibs")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Lines S+")]
		public bool Show_S_Plus
		{
			get { return show_S_Plus; }
			set { show_S_Plus = value; }
		}
		
		/// <summary>
		/// </summary>
		[Description("Option to show S- Fibs")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Lines S-")]
		public bool Show_S_Minus
		{
			get { return show_S_Minus; }
			set { show_S_Minus = value; }
		}
	
		/// <summary>
		/// </summary>
		[Description("Option to show E+ Fibs")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Lines E+")]
		public bool Show_E_Plus
		{
			get { return show_E_Plus; }
			set { show_E_Plus = value; }
		}
		
		/// <summary>
		/// </summary>
		[Description("Option to show E- Fibs")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Lines E-")]
		public bool Show_E_Minus
		{
			get { return show_E_Minus; }
			set { show_E_Minus = value; }
		}
		
	
		/// <summary>
		/// </summary>
		[Description("Option to show D+ Fibs")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Lines D+")]
		public bool Show_D_Plus
		{
			get { return show_D_Plus; }
			set { show_D_Plus = value; }
		}
		
		/// <summary>
		/// </summary>
		[Description("Option to show D- Fibs")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Lines D-")]
		public bool Show_D_Minus
		{
			get { return show_D_Minus; }
			set { show_D_Minus = value; }
		}
	
		/// <summary>
		/// </summary>
		[Description("Option to show C+ Fibs")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Lines C+")]
		public bool Show_C_Plus
		{
			get { return show_C_Plus; }
			set { show_C_Plus = value; }
		}
		
		/// <summary>
		/// </summary>
		[Description("Option to show C- Fibs")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Lines C-")]
		public bool Show_C_Minus
		{
			get { return show_C_Minus; }
			set { show_C_Minus = value; }
		}
						
	
		/// <summary>
		/// </summary>
		[Description("Option to show B+ Fibs")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Lines B+")]
		public bool Show_B_Plus
		{
			get { return show_B_Plus; }
			set { show_B_Plus = value; }
		}
		
		/// <summary>
		/// </summary>
		[Description("Option to show B- Fibs")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Lines B-")]
		public bool Show_B_Minus
		{
			get { return show_B_Minus; }
			set { show_B_Minus = value; }
		}
				
	
		/// <summary>
		/// </summary>
		[Description("Option to show A+ Fibs")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Lines A+")]
		public bool Show_A_Plus
		{
			get { return show_A_Plus; }
			set { show_A_Plus = value; }
		}
		
		/// <summary>
		/// </summary>
		[Description("Option to show A- Fibs")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Lines A-")]
		public bool Show_A_Minus
		{
			get { return show_A_Minus; }
			set { show_A_Minus = value; }
		}
				
		/// <summary>
		/// </summary>
		[Description("Option to show Fib Extensions of Covered Highs and Lows")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Extensions")]
		public bool Show_Xtensions 
		{
			get { return show_Xtensions; }
			set { show_Xtensions = value; }
		}
			
		/// <summary>
		/// </summary>
		[Description("Option to show Swing Highs and Lows")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Swing Highs and Lows")]
		public bool Show_Highs_Lows 
		{
			get { return show_Highs_Lows; }
			set { show_Highs_Lows = value; }
		}
		
		/// <summary>
		/// </summary>
		[Description("Option to show last Fib restracements")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Lines Y+")]
		public bool Show_Y_Plus 
		{
			get { return show_Y_Plus; }
			set { show_Y_Plus = value; }
		}
		
		/// <summary>
		/// </summary>
		[Description("Option to show last Fib restracements")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Lines Y-")]
		public bool Show_Y_Minus 
		{
			get { return show_Y_Minus; }
			set { show_Y_Minus = value; }
		}
		
		/// <summary>
		/// </summary>
		[Description("Option to show last Fib restracements")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Lines Z+")]
		public bool Show_Z_Plus 
		{
			get { return show_Z_Plus; }
			set { show_Z_Plus = value; }
		}
		
		/// <summary>
		/// </summary>
		[Description("Option to show last Fib restracements")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Show Fib Lines Z-")]
		public bool Show_Z_Minus 
		{
			get { return show_Z_Minus; }
			set { show_Z_Minus = value; }
		}
					
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension0
		{
			get { return Values[0]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension1
		{
			get { return Values[1]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension2
		{
			get { return Values[2]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension3
		{
			get { return Values[3]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension4
		{
			get { return Values[4]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension5
		{
			get { return Values[5]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension6
		{
			get { return Values[6]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension7
		{
			get { return Values[7]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension8
		{
			get { return Values[8]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension9
		{
			get { return Values[9]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension10
		{
			get { return Values[10]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension11
		{
			get { return Values[11]; }
		}		
				
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension12
		{
			get { return Values[12]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension13
		{
			get { return Values[13]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension14
		{
			get { return Values[14]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension15
		{
			get { return Values[15]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension16
		{
			get { return Values[16]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension17
		{
			get { return Values[17]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension18
		{
			get { return Values[18]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension19
		{
			get { return Values[19]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension20
		{
			get { return Values[20]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension21
		{
			get { return Values[21]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension22
		{
			get { return Values[22]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension23
		{
			get { return Values[23]; }
		}		

		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension24
		{
			get { return Values[24]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension25
		{
			get { return Values[25]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension26
		{
			get { return Values[26]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension27
		{
			get { return Values[27]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension28
		{
			get { return Values[28]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension29
		{
			get { return Values[29]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension30
		{
			get { return Values[30]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension31
		{
			get { return Values[31]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension32
		{
			get { return Values[32]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension33
		{
			get { return Values[33]; }
		}		

		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension34
		{
			get { return Values[34]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries Extension35
		{
			get { return Values[35]; }
		}		

		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZplusFib000
		{
			get { return Values[36]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZplusFib236
		{
			get { return Values[37]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZplusFib382
		{
			get { return Values[38]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZplusFib500
		{
			get { return Values[39]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZplusFib618
		{
			get { return Values[40]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZplusFib764
		{
			get { return Values[41]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZplusFib1000
		{
			get { return Values[42]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZplusFib1272
		{
			get { return Values[43]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZplusFib1618
		{
			get { return Values[44]; }
		}		
	
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZminusFib000
		{
			get { return Values[45]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZminusFib236
		{
			get { return Values[46]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZminusFib382
		{
			get { return Values[47]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZminusFib500
		{
			get { return Values[48]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZminusFib618
		{
			get { return Values[49]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZminusFib764
		{
			get { return Values[50]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZminusFib1000
		{
			get { return Values[51]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZminusFib1272
		{
			get { return Values[52]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries ZminusFib1618
		{
			get { return Values[53]; }
		}		
			
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YplusFib000
		{
			get { return Values[54]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YplusFib236
		{
			get { return Values[55]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YplusFib382
		{
			get { return Values[56]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YplusFib500
		{
			get { return Values[57]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YplusFib618
		{
			get { return Values[58]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YplusFib764
		{
			get { return Values[59]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YplusFib1000
		{
			get { return Values[60]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YplusFib1272
		{
			get { return Values[61]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YplusFib1618
		{
			get { return Values[62]; }
		}		
	
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YminusFib000
		{
			get { return Values[63]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YminusFib236
		{
			get { return Values[64]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YminusFib382
		{
			get { return Values[65]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YminusFib500
		{
			get { return Values[66]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YminusFib618
		{
			get { return Values[67]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YminusFib764
		{
			get { return Values[68]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YminusFib1000
		{
			get { return Values[69]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YminusFib1272
		{
			get { return Values[70]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries YminusFib1618
		{
			get { return Values[71]; }
		}		
	
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SplusFib000
		{
			get { return Values[72]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SplusFib236
		{
			get { return Values[73]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SplusFib382
		{
			get { return Values[74]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SplusFib500
		{
			get { return Values[75]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SplusFib618
		{
			get { return Values[76]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SplusFib764
		{
			get { return Values[77]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SplusFib1000
		{
			get { return Values[78]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SplusFib1272
		{
			get { return Values[79]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SplusFib1618
		{
			get { return Values[80]; }
		}		
				
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SminusFib000
		{
			get { return Values[81]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SminusFib236
		{
			get { return Values[82]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SminusFib382
		{
			get { return Values[83]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SminusFib500
		{
			get { return Values[84]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SminusFib618
		{
			get { return Values[85]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SminusFib764
		{
			get { return Values[86]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SminusFib1000
		{
			get { return Values[87]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SminusFib1272
		{
			get { return Values[88]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries SminusFib1618
		{
			get { return Values[89]; }
		}			
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EplusFib000
		{
			get { return Values[90]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EplusFib236
		{
			get { return Values[91]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EplusFib382
		{
			get { return Values[92]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EplusFib500
		{
			get { return Values[93]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EplusFib618
		{
			get { return Values[94]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EplusFib764
		{
			get { return Values[95]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EplusFib1000
		{
			get { return Values[96]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EplusFib1272
		{
			get { return Values[97]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EplusFib1618
		{
			get { return Values[98]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EminusFib000
		{
			get { return Values[99]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EminusFib236
		{
			get { return Values[100]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EminusFib382
		{
			get { return Values[101]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EminusFib500
		{
			get { return Values[102]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EminusFib618
		{
			get { return Values[103]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EminusFib764
		{
			get { return Values[104]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EminusFib1000
		{
			get { return Values[105]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EminusFib1272
		{
			get { return Values[106]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries EminusFib1618
		{
			get { return Values[107]; }
		}		
				
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DplusFib000
		{
			get { return Values[108]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DplusFib236
		{
			get { return Values[109]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DplusFib382
		{
			get { return Values[110]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DplusFib500
		{
			get { return Values[111]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DplusFib618
		{
			get { return Values[112]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DplusFib764
		{
			get { return Values[113]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DplusFib1000
		{
			get { return Values[114]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DplusFib1272
		{
			get { return Values[115]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DplusFib1618
		{
			get { return Values[116]; }
		}		
	
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DminusFib000
		{
			get { return Values[117]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DminusFib236
		{
			get { return Values[118]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DminusFib382
		{
			get { return Values[119]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DminusFib500
		{
			get { return Values[120]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DminusFib618
		{
			get { return Values[121]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DminusFib764
		{
			get { return Values[122]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DminusFib1000
		{
			get { return Values[123]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DminusFib1272
		{
			get { return Values[124]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries DminusFib1618
		{
			get { return Values[125]; }
		}		
						
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CplusFib000
		{
			get { return Values[126]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CplusFib236
		{
			get { return Values[127]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CplusFib382
		{
			get { return Values[128]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CplusFib500
		{
			get { return Values[129]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CplusFib618
		{
			get { return Values[130]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CplusFib764
		{
			get { return Values[131]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CplusFib1000
		{
			get { return Values[132]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CplusFib1272
		{
			get { return Values[133]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CplusFib1618
		{
			get { return Values[134]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CminusFib000
		{
			get { return Values[135]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CminusFib236
		{
			get { return Values[136]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CminusFib382
		{
			get { return Values[137]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CminusFib500
		{
			get { return Values[138]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CminusFib618
		{
			get { return Values[139]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CminusFib764
		{
			get { return Values[140]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CminusFib1000
		{
			get { return Values[141]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CminusFib1272
		{
			get { return Values[142]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries CminusFib1618
		{
			get { return Values[143]; }
		}		
				
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BplusFib000
		{
			get { return Values[144]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BplusFib236
		{
			get { return Values[145]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BplusFib382
		{
			get { return Values[146]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BplusFib500
		{
			get { return Values[147]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BplusFib618
		{
			get { return Values[148]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BplusFib764
		{
			get { return Values[149]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BplusFib1000
		{
			get { return Values[150]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BplusFib1272
		{
			get { return Values[151]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BplusFib1618
		{
			get { return Values[152]; }
		}		

		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BminusFib000
		{
			get { return Values[153]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BminusFib236
		{
			get { return Values[154]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BminusFib382
		{
			get { return Values[155]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BminusFib500
		{
			get { return Values[156]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BminusFib618
		{
			get { return Values[157]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BminusFib764
		{
			get { return Values[158]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BminusFib1000
		{
			get { return Values[159]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BminusFib1272
		{
			get { return Values[160]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries BminusFib1618
		{
			get { return Values[161]; }
		}		
				
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AplusFib000
		{
			get { return Values[162]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AplusFib236
		{
			get { return Values[163]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AplusFib382
		{
			get { return Values[164]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AplusFib500
		{
			get { return Values[165]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AplusFib618
		{
			get { return Values[166]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AplusFib764
		{
			get { return Values[167]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AplusFib1000
		{
			get { return Values[168]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AplusFib1272
		{
			get { return Values[169]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AplusFib1618
		{
			get { return Values[170]; }
		}		

		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AminusFib000
		{
			get { return Values[171]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AminusFib236
		{
			get { return Values[172]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AminusFib382
		{
			get { return Values[173]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AminusFib500
		{
			get { return Values[174]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AminusFib618
		{
			get { return Values[175]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AminusFib764
		{
			get { return Values[176]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AminusFib1000
		{
			get { return Values[177]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AminusFib1272
		{
			get { return Values[178]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries AminusFib1618
		{
			get { return Values[179]; }
		}		
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries HiddenHigh0
		{
			get { return Values[180]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries HiddenHigh1
		{
			get { return Values[181]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries HiddenHigh2
		{
			get { return Values[182]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries HiddenHigh3
		{
			get { return Values[183]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries HiddenHigh4
		{
			get { return Values[184]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries HiddenHigh5
		{
			get { return Values[185]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries HiddenLow0
		{
			get { return Values[186]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries HiddenLow1
		{
			get { return Values[187]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries HiddenLow2
		{
			get { return Values[188]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries HiddenLow3
		{
			get { return Values[189]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries HiddenLow4
		{
			get { return Values[190]; }
		}
		
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public DataSeries HiddenLow5
		{
			get { return Values[191]; }
		}		

		/// <summary>
		/// </summary>
		[Description("Width of the pivot lines as # of bars.")]
		[Gui.Design.DisplayNameAttribute("Line Width")]
		[Category("Parameters")]
		public int Width
		{
			get { return width; }
			set { width = Math.Max(1, value); }
		}
	
		/// <summary>
		/// </summary>
		[Description("Lookback Period of Indicator")]
        [Category("Parameters")]
 		[Gui.Design.DisplayNameAttribute("Lookback Period in Days")]
      	public int LookBack
        {
            get { return lookBack; }
            set { lookBack = value; }
        }
					
		/// <summary>
		/// </summary>
		[Description("Filter, no filter = 100, standard value = 85")]
        [Category("Parameters")]
 		[Gui.Design.DisplayNameAttribute("Filter for Swing Lows and Highs")]
       	public int Filter
        {
            get { return filter; }
            set { filter = Math.Max(0, Math.Min(100, value)); }
        }

		#endregion

		
		#region Miscellaneous


		/// <summary>
        /// Overload this method to handle the termination of an indicator. Use this method to dispose of any resources vs overloading the Dispose() method.
		/// </summary>
		protected override void OnTermination()
		{
			textBrush.Dispose();
			foreach (SolidBrush solidBrush in brushes)
				solidBrush.Dispose();
			stringFormatNear.Dispose();	
			stringFormatCenter.Dispose();	
			stringFormatFar.Dispose();
		}
		
		
		/// <summary>
		/// </summary>
		/// <param name="graphics"></param>
		/// <param name="bounds"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		public override void Plot(Graphics graphics, Rectangle bounds, double min, double max)
		{
			if (Bars == null || ChartControl == null)
				return;

			//Counting for multiple Fib and S/R levels	
			for (int seriesCount=0; seriesCount<Values.Length; seriesCount++)
					fibCounter[seriesCount] = 1;
			for (int seriesCount=0; seriesCount<Values.Length ; seriesCount++)
				{
					for(int j=seriesCount+1; j<Values.Length; j++)
					{
						if (Values[seriesCount].Get(Math.Min(Bars.Count - 1, this.LastBarIndexPainted)) == Values[j].Get(Math.Min(Bars.Count - 1, this.LastBarIndexPainted)))
						{
							fibCounter[seriesCount]=fibCounter[seriesCount]+1;
							fibCounter[j]=fibCounter[j]+1;
						}	
					}
				}
			
			// Preparing Plot Labels		
			string highDateLabel = "no date";
			string lowDateLabel = "no date";
				
			for (int i=0; i<6; i++)
			{	
				plotlabel[3*i]=(highExtDate[i].Get(Math.Min(Bars.Count - 1, this.LastBarIndexPainted))).ToString("dd MMM ");
				plotlabel[3*i+1]=(highExtDate[i].Get(Math.Min(Bars.Count - 1, this.LastBarIndexPainted))).ToString("dd MMM ");
				plotlabel[3*i+2]=(highExtDate[i].Get(Math.Min(Bars.Count - 1, this.LastBarIndexPainted))).ToString("dd MMM ");
				plotlabel[3*i+18]=(lowExtDate[i].Get(Math.Min(Bars.Count - 1, this.LastBarIndexPainted))).ToString("dd MMM ");
				plotlabel[3*i+19]=(lowExtDate[i].Get(Math.Min(Bars.Count - 1, this.LastBarIndexPainted))).ToString("dd MMM ");
				plotlabel[3*i+20]=(lowExtDate[i].Get(Math.Min(Bars.Count - 1, this.LastBarIndexPainted))).ToString("dd MMM ");
			}
			for (int i=0;i<9;i++)
			{
				plotlabel[i+36]=(recentHighDate.Get(Math.Min(Bars.Count - 1, this.LastBarIndexPainted))).ToString("dd MMM ");
				plotlabel[i+45]=(recentLowDate.Get(Math.Min(Bars.Count - 1, this.LastBarIndexPainted))).ToString("dd MMM ");
			}			
			for (int i=0;i<9;i++)
			{	
				plotlabel[i+54]=(lastHighDate.Get(Math.Min(Bars.Count - 1, this.LastBarIndexPainted))).ToString("dd MMM ");
				plotlabel[i+63]=(lastLowDate.Get(Math.Min(Bars.Count - 1, this.LastBarIndexPainted))).ToString("dd MMM ");
			}
			for (int i=0; i<6; i++)
			{
				highDateLabel = (highDate[i].Get(Math.Min(Bars.Count - 1, this.LastBarIndexPainted))).ToString("dd MMM ");
				lowDateLabel = (lowDate[i].Get(Math.Min(Bars.Count - 1, this.LastBarIndexPainted))).ToString("dd MMM ");
				for (int j=0; j<9; j++) 
				{
					plotlabel[72+18*i+j] = highDateLabel;
					plotlabel[81+18*i+j] = lowDateLabel;
				}
			}
			for (int i=0; i<6; i++)
			{	
				plotlabel[i+180]=(hiddenHighDate[i].Get(Math.Min(Bars.Count - 1, this.LastBarIndexPainted))).ToString("dd MMM ");
				plotlabel[i+186]=(hiddenLowDate[i].Get(Math.Min(Bars.Count - 1, this.LastBarIndexPainted))).ToString("dd MMM ");
			}
			
			// Drawing Lines and Labels
			int	barWidth = ChartControl.ChartStyle.GetBarPaintWidth(ChartControl.BarWidth);
			for (int seriesCount = 0; seriesCount < Values.Length; seriesCount++)
			{
				SolidBrush		brush				= brushes[seriesCount];
				DateTime 		lastPeriodEnd 		= Cbi.Globals.MinDate;
				int				lastX				= -1;
				int				lastY				= -1;
				int 			firstX				= -1;
				string			label				= " ";
				SmoothingMode	oldSmoothingMode	= graphics.SmoothingMode;
				Gui.Chart.Plot	plot				= Plots[seriesCount];
				DataSeries	series					= (DataSeries) Values[seriesCount];

				if (brush.Color != plot.Pen.Color)	
					brush = new SolidBrush(plot.Pen.Color);
				
				using (GraphicsPath	path = new GraphicsPath())
				{
					if (brush.Color != plot.Pen.Color)	
						brush = new SolidBrush(plot.Pen.Color);
	
					for (int idx = this.LastBarIndexPainted; idx >= Math.Max(this.FirstBarIndexPainted, this.LastBarIndexPainted - Width); idx--)
					{
						if (idx - Displacement < 0 || idx - Displacement >= Bars.Count || (!ChartControl.ShowBarsRequired && idx - Displacement < BarsRequired))
							continue;
						else if (!series.IsValidPlot(idx))
							continue;
					
						double	val = series.Get(idx);
						int		x	= ChartControl.GetXByBarIdx(BarsArray[0], idx);
						int		y	= ChartControl.GetYByValue(this, val);

						if (lastX >= 0)
						{
							if (y != lastY) // Problem here is, that last bar of old day has date of new day
								y = lastY;
							path.AddLine(lastX - plot.Pen.Width / 2, lastY, x - plot.Pen.Width / 2, y);
						}
						lastX	= x;
						lastY	= y;
						if (idx == this.LastBarIndexPainted || idx == this.LastBarIndexPainted-1)
							firstX	= x;
					}
					graphics.SmoothingMode = SmoothingMode.AntiAlias;
					graphics.DrawPath(plot.Pen, path);
					graphics.SmoothingMode = oldSmoothingMode;
					if (fibCounter[seriesCount]==1)
						label = plot.Name + "  " + plotlabel[seriesCount];
					else
						label = "Multiple S/R (" + Convert.ToString(fibCounter[seriesCount]) + ")" ;
					graphics.DrawString(label, ChartControl.Font, brush, firstX + LabelPosition + 20, lastY - ChartControl.Font.GetHeight() / 2, stringFormatNear);
				}
			}
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
        private anaFibonacciClusterV14L[] cacheanaFibonacciClusterV14L = null;

        private static anaFibonacciClusterV14L checkanaFibonacciClusterV14L = new anaFibonacciClusterV14L();

        /// <summary>
        /// anaFibonacciClusterV14L.
        /// </summary>
        /// <returns></returns>
        public anaFibonacciClusterV14L anaFibonacciClusterV14L(int filter, int labelPosition, int lookBack, bool show_A_Minus, bool show_A_Plus, bool show_B_Minus, bool show_B_Plus, bool show_C_Minus, bool show_C_Plus, bool show_D_Minus, bool show_D_Plus, bool show_E_Minus, bool show_E_Plus, bool show_Highs_Lows, bool show_S_Minus, bool show_S_Plus, bool show_Xtensions, bool show_Y_Minus, bool show_Y_Plus, bool show_Z_Minus, bool show_Z_Plus, int width)
        {
            return anaFibonacciClusterV14L(Input, filter, labelPosition, lookBack, show_A_Minus, show_A_Plus, show_B_Minus, show_B_Plus, show_C_Minus, show_C_Plus, show_D_Minus, show_D_Plus, show_E_Minus, show_E_Plus, show_Highs_Lows, show_S_Minus, show_S_Plus, show_Xtensions, show_Y_Minus, show_Y_Plus, show_Z_Minus, show_Z_Plus, width);
        }

        /// <summary>
        /// anaFibonacciClusterV14L.
        /// </summary>
        /// <returns></returns>
        public anaFibonacciClusterV14L anaFibonacciClusterV14L(Data.IDataSeries input, int filter, int labelPosition, int lookBack, bool show_A_Minus, bool show_A_Plus, bool show_B_Minus, bool show_B_Plus, bool show_C_Minus, bool show_C_Plus, bool show_D_Minus, bool show_D_Plus, bool show_E_Minus, bool show_E_Plus, bool show_Highs_Lows, bool show_S_Minus, bool show_S_Plus, bool show_Xtensions, bool show_Y_Minus, bool show_Y_Plus, bool show_Z_Minus, bool show_Z_Plus, int width)
        {
            if (cacheanaFibonacciClusterV14L != null)
                for (int idx = 0; idx < cacheanaFibonacciClusterV14L.Length; idx++)
                    if (cacheanaFibonacciClusterV14L[idx].Filter == filter && cacheanaFibonacciClusterV14L[idx].LabelPosition == labelPosition && cacheanaFibonacciClusterV14L[idx].LookBack == lookBack && cacheanaFibonacciClusterV14L[idx].Show_A_Minus == show_A_Minus && cacheanaFibonacciClusterV14L[idx].Show_A_Plus == show_A_Plus && cacheanaFibonacciClusterV14L[idx].Show_B_Minus == show_B_Minus && cacheanaFibonacciClusterV14L[idx].Show_B_Plus == show_B_Plus && cacheanaFibonacciClusterV14L[idx].Show_C_Minus == show_C_Minus && cacheanaFibonacciClusterV14L[idx].Show_C_Plus == show_C_Plus && cacheanaFibonacciClusterV14L[idx].Show_D_Minus == show_D_Minus && cacheanaFibonacciClusterV14L[idx].Show_D_Plus == show_D_Plus && cacheanaFibonacciClusterV14L[idx].Show_E_Minus == show_E_Minus && cacheanaFibonacciClusterV14L[idx].Show_E_Plus == show_E_Plus && cacheanaFibonacciClusterV14L[idx].Show_Highs_Lows == show_Highs_Lows && cacheanaFibonacciClusterV14L[idx].Show_S_Minus == show_S_Minus && cacheanaFibonacciClusterV14L[idx].Show_S_Plus == show_S_Plus && cacheanaFibonacciClusterV14L[idx].Show_Xtensions == show_Xtensions && cacheanaFibonacciClusterV14L[idx].Show_Y_Minus == show_Y_Minus && cacheanaFibonacciClusterV14L[idx].Show_Y_Plus == show_Y_Plus && cacheanaFibonacciClusterV14L[idx].Show_Z_Minus == show_Z_Minus && cacheanaFibonacciClusterV14L[idx].Show_Z_Plus == show_Z_Plus && cacheanaFibonacciClusterV14L[idx].Width == width && cacheanaFibonacciClusterV14L[idx].EqualsInput(input))
                        return cacheanaFibonacciClusterV14L[idx];

            lock (checkanaFibonacciClusterV14L)
            {
                checkanaFibonacciClusterV14L.Filter = filter;
                filter = checkanaFibonacciClusterV14L.Filter;
                checkanaFibonacciClusterV14L.LabelPosition = labelPosition;
                labelPosition = checkanaFibonacciClusterV14L.LabelPosition;
                checkanaFibonacciClusterV14L.LookBack = lookBack;
                lookBack = checkanaFibonacciClusterV14L.LookBack;
                checkanaFibonacciClusterV14L.Show_A_Minus = show_A_Minus;
                show_A_Minus = checkanaFibonacciClusterV14L.Show_A_Minus;
                checkanaFibonacciClusterV14L.Show_A_Plus = show_A_Plus;
                show_A_Plus = checkanaFibonacciClusterV14L.Show_A_Plus;
                checkanaFibonacciClusterV14L.Show_B_Minus = show_B_Minus;
                show_B_Minus = checkanaFibonacciClusterV14L.Show_B_Minus;
                checkanaFibonacciClusterV14L.Show_B_Plus = show_B_Plus;
                show_B_Plus = checkanaFibonacciClusterV14L.Show_B_Plus;
                checkanaFibonacciClusterV14L.Show_C_Minus = show_C_Minus;
                show_C_Minus = checkanaFibonacciClusterV14L.Show_C_Minus;
                checkanaFibonacciClusterV14L.Show_C_Plus = show_C_Plus;
                show_C_Plus = checkanaFibonacciClusterV14L.Show_C_Plus;
                checkanaFibonacciClusterV14L.Show_D_Minus = show_D_Minus;
                show_D_Minus = checkanaFibonacciClusterV14L.Show_D_Minus;
                checkanaFibonacciClusterV14L.Show_D_Plus = show_D_Plus;
                show_D_Plus = checkanaFibonacciClusterV14L.Show_D_Plus;
                checkanaFibonacciClusterV14L.Show_E_Minus = show_E_Minus;
                show_E_Minus = checkanaFibonacciClusterV14L.Show_E_Minus;
                checkanaFibonacciClusterV14L.Show_E_Plus = show_E_Plus;
                show_E_Plus = checkanaFibonacciClusterV14L.Show_E_Plus;
                checkanaFibonacciClusterV14L.Show_Highs_Lows = show_Highs_Lows;
                show_Highs_Lows = checkanaFibonacciClusterV14L.Show_Highs_Lows;
                checkanaFibonacciClusterV14L.Show_S_Minus = show_S_Minus;
                show_S_Minus = checkanaFibonacciClusterV14L.Show_S_Minus;
                checkanaFibonacciClusterV14L.Show_S_Plus = show_S_Plus;
                show_S_Plus = checkanaFibonacciClusterV14L.Show_S_Plus;
                checkanaFibonacciClusterV14L.Show_Xtensions = show_Xtensions;
                show_Xtensions = checkanaFibonacciClusterV14L.Show_Xtensions;
                checkanaFibonacciClusterV14L.Show_Y_Minus = show_Y_Minus;
                show_Y_Minus = checkanaFibonacciClusterV14L.Show_Y_Minus;
                checkanaFibonacciClusterV14L.Show_Y_Plus = show_Y_Plus;
                show_Y_Plus = checkanaFibonacciClusterV14L.Show_Y_Plus;
                checkanaFibonacciClusterV14L.Show_Z_Minus = show_Z_Minus;
                show_Z_Minus = checkanaFibonacciClusterV14L.Show_Z_Minus;
                checkanaFibonacciClusterV14L.Show_Z_Plus = show_Z_Plus;
                show_Z_Plus = checkanaFibonacciClusterV14L.Show_Z_Plus;
                checkanaFibonacciClusterV14L.Width = width;
                width = checkanaFibonacciClusterV14L.Width;

                if (cacheanaFibonacciClusterV14L != null)
                    for (int idx = 0; idx < cacheanaFibonacciClusterV14L.Length; idx++)
                        if (cacheanaFibonacciClusterV14L[idx].Filter == filter && cacheanaFibonacciClusterV14L[idx].LabelPosition == labelPosition && cacheanaFibonacciClusterV14L[idx].LookBack == lookBack && cacheanaFibonacciClusterV14L[idx].Show_A_Minus == show_A_Minus && cacheanaFibonacciClusterV14L[idx].Show_A_Plus == show_A_Plus && cacheanaFibonacciClusterV14L[idx].Show_B_Minus == show_B_Minus && cacheanaFibonacciClusterV14L[idx].Show_B_Plus == show_B_Plus && cacheanaFibonacciClusterV14L[idx].Show_C_Minus == show_C_Minus && cacheanaFibonacciClusterV14L[idx].Show_C_Plus == show_C_Plus && cacheanaFibonacciClusterV14L[idx].Show_D_Minus == show_D_Minus && cacheanaFibonacciClusterV14L[idx].Show_D_Plus == show_D_Plus && cacheanaFibonacciClusterV14L[idx].Show_E_Minus == show_E_Minus && cacheanaFibonacciClusterV14L[idx].Show_E_Plus == show_E_Plus && cacheanaFibonacciClusterV14L[idx].Show_Highs_Lows == show_Highs_Lows && cacheanaFibonacciClusterV14L[idx].Show_S_Minus == show_S_Minus && cacheanaFibonacciClusterV14L[idx].Show_S_Plus == show_S_Plus && cacheanaFibonacciClusterV14L[idx].Show_Xtensions == show_Xtensions && cacheanaFibonacciClusterV14L[idx].Show_Y_Minus == show_Y_Minus && cacheanaFibonacciClusterV14L[idx].Show_Y_Plus == show_Y_Plus && cacheanaFibonacciClusterV14L[idx].Show_Z_Minus == show_Z_Minus && cacheanaFibonacciClusterV14L[idx].Show_Z_Plus == show_Z_Plus && cacheanaFibonacciClusterV14L[idx].Width == width && cacheanaFibonacciClusterV14L[idx].EqualsInput(input))
                            return cacheanaFibonacciClusterV14L[idx];

                anaFibonacciClusterV14L indicator = new anaFibonacciClusterV14L();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.Filter = filter;
                indicator.LabelPosition = labelPosition;
                indicator.LookBack = lookBack;
                indicator.Show_A_Minus = show_A_Minus;
                indicator.Show_A_Plus = show_A_Plus;
                indicator.Show_B_Minus = show_B_Minus;
                indicator.Show_B_Plus = show_B_Plus;
                indicator.Show_C_Minus = show_C_Minus;
                indicator.Show_C_Plus = show_C_Plus;
                indicator.Show_D_Minus = show_D_Minus;
                indicator.Show_D_Plus = show_D_Plus;
                indicator.Show_E_Minus = show_E_Minus;
                indicator.Show_E_Plus = show_E_Plus;
                indicator.Show_Highs_Lows = show_Highs_Lows;
                indicator.Show_S_Minus = show_S_Minus;
                indicator.Show_S_Plus = show_S_Plus;
                indicator.Show_Xtensions = show_Xtensions;
                indicator.Show_Y_Minus = show_Y_Minus;
                indicator.Show_Y_Plus = show_Y_Plus;
                indicator.Show_Z_Minus = show_Z_Minus;
                indicator.Show_Z_Plus = show_Z_Plus;
                indicator.Width = width;
                Indicators.Add(indicator);
                indicator.SetUp();

                anaFibonacciClusterV14L[] tmp = new anaFibonacciClusterV14L[cacheanaFibonacciClusterV14L == null ? 1 : cacheanaFibonacciClusterV14L.Length + 1];
                if (cacheanaFibonacciClusterV14L != null)
                    cacheanaFibonacciClusterV14L.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheanaFibonacciClusterV14L = tmp;
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
        /// anaFibonacciClusterV14L.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.anaFibonacciClusterV14L anaFibonacciClusterV14L(int filter, int labelPosition, int lookBack, bool show_A_Minus, bool show_A_Plus, bool show_B_Minus, bool show_B_Plus, bool show_C_Minus, bool show_C_Plus, bool show_D_Minus, bool show_D_Plus, bool show_E_Minus, bool show_E_Plus, bool show_Highs_Lows, bool show_S_Minus, bool show_S_Plus, bool show_Xtensions, bool show_Y_Minus, bool show_Y_Plus, bool show_Z_Minus, bool show_Z_Plus, int width)
        {
            return _indicator.anaFibonacciClusterV14L(Input, filter, labelPosition, lookBack, show_A_Minus, show_A_Plus, show_B_Minus, show_B_Plus, show_C_Minus, show_C_Plus, show_D_Minus, show_D_Plus, show_E_Minus, show_E_Plus, show_Highs_Lows, show_S_Minus, show_S_Plus, show_Xtensions, show_Y_Minus, show_Y_Plus, show_Z_Minus, show_Z_Plus, width);
        }

        /// <summary>
        /// anaFibonacciClusterV14L.
        /// </summary>
        /// <returns></returns>
        public Indicator.anaFibonacciClusterV14L anaFibonacciClusterV14L(Data.IDataSeries input, int filter, int labelPosition, int lookBack, bool show_A_Minus, bool show_A_Plus, bool show_B_Minus, bool show_B_Plus, bool show_C_Minus, bool show_C_Plus, bool show_D_Minus, bool show_D_Plus, bool show_E_Minus, bool show_E_Plus, bool show_Highs_Lows, bool show_S_Minus, bool show_S_Plus, bool show_Xtensions, bool show_Y_Minus, bool show_Y_Plus, bool show_Z_Minus, bool show_Z_Plus, int width)
        {
            return _indicator.anaFibonacciClusterV14L(input, filter, labelPosition, lookBack, show_A_Minus, show_A_Plus, show_B_Minus, show_B_Plus, show_C_Minus, show_C_Plus, show_D_Minus, show_D_Plus, show_E_Minus, show_E_Plus, show_Highs_Lows, show_S_Minus, show_S_Plus, show_Xtensions, show_Y_Minus, show_Y_Plus, show_Z_Minus, show_Z_Plus, width);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// anaFibonacciClusterV14L.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.anaFibonacciClusterV14L anaFibonacciClusterV14L(int filter, int labelPosition, int lookBack, bool show_A_Minus, bool show_A_Plus, bool show_B_Minus, bool show_B_Plus, bool show_C_Minus, bool show_C_Plus, bool show_D_Minus, bool show_D_Plus, bool show_E_Minus, bool show_E_Plus, bool show_Highs_Lows, bool show_S_Minus, bool show_S_Plus, bool show_Xtensions, bool show_Y_Minus, bool show_Y_Plus, bool show_Z_Minus, bool show_Z_Plus, int width)
        {
            return _indicator.anaFibonacciClusterV14L(Input, filter, labelPosition, lookBack, show_A_Minus, show_A_Plus, show_B_Minus, show_B_Plus, show_C_Minus, show_C_Plus, show_D_Minus, show_D_Plus, show_E_Minus, show_E_Plus, show_Highs_Lows, show_S_Minus, show_S_Plus, show_Xtensions, show_Y_Minus, show_Y_Plus, show_Z_Minus, show_Z_Plus, width);
        }

        /// <summary>
        /// anaFibonacciClusterV14L.
        /// </summary>
        /// <returns></returns>
        public Indicator.anaFibonacciClusterV14L anaFibonacciClusterV14L(Data.IDataSeries input, int filter, int labelPosition, int lookBack, bool show_A_Minus, bool show_A_Plus, bool show_B_Minus, bool show_B_Plus, bool show_C_Minus, bool show_C_Plus, bool show_D_Minus, bool show_D_Plus, bool show_E_Minus, bool show_E_Plus, bool show_Highs_Lows, bool show_S_Minus, bool show_S_Plus, bool show_Xtensions, bool show_Y_Minus, bool show_Y_Plus, bool show_Z_Minus, bool show_Z_Plus, int width)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.anaFibonacciClusterV14L(input, filter, labelPosition, lookBack, show_A_Minus, show_A_Plus, show_B_Minus, show_B_Plus, show_C_Minus, show_C_Plus, show_D_Minus, show_D_Plus, show_E_Minus, show_E_Plus, show_Highs_Lows, show_S_Minus, show_S_Plus, show_Xtensions, show_Y_Minus, show_Y_Plus, show_Z_Minus, show_Z_Plus, width);
        }
    }
}
#endregion
