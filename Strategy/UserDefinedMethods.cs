using System.Collections.Generic;

#region Using declarations

using System;
using System.ComponentModel;
using System.Drawing;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Indicator;
using NinjaTrader.Strategy;
#endregion

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    /// <summary>
    /// This file holds all user defined strategy methods.
    /// </summary>
    partial class Strategy
    {
        public bool AODifferentColor(int barsAgo)
        {
            int negCol = 0;
            int posCol = 0;
            for (int i = 1; i <= barsAgo; i++)
            {
                if (bwAO().AOValue[i] < 0)
                {
                    negCol++;
                }
                else if (bwAO().AOValue[i] > 0)
                {
                    posCol++;
                }
            }
            if ((negCol != 0) && (posCol != 0))
            {
                return true;
            }
            return false;
        }

        public bool RagheeDifferentColor(int barsAgo)
        {
            int negCol = 0;
            int posCol = 0;
            for (int i = 1; i <= barsAgo; i++)
            {
                if ((Open[i] <= Close[i]
                && Close[i] > EMA(High, 34)[i]) ||
                    (Open[i] >= Close[i]
                && Close[i] > EMA(High, 34)[i]))
                {
                    posCol++;
                }

                else if ((Open[i] <= Close[i]
                && Close[i] < EMA(Low, 34)[i]) ||
                    (Open[i] >= Close[i]
                && Close[i] < EMA(Low, 34)[i]))
                {
                    negCol++;
                }

            }
            if ((negCol != 0) && (posCol != 0))
            {
                return true;
            }
            return false;
        }
        public double GetHighest(int numOfBars)
        {
            double curHigh;
            double mostHigh = High[0];
            for (int i = 1; i <= numOfBars; i++)
            {
                curHigh = High[i];
                if (curHigh > mostHigh)
                {
                    mostHigh = curHigh;
                }
            }
            return mostHigh;
        }
        public double GetLowest(int numOfBars)
        {
            double curLow;
            double mostLow = Low[0];
            for (int i = 1; i <= numOfBars; i++)
            {
                curLow = Low[i];
                if (curLow < mostLow)
                {
                    mostLow = curLow;
                }
            }
            return mostLow;
        }
        /// <summary>
        /// Series Trend
        /// </summary>
        /// <param name="series"> input series</param>
        /// <param name="numOfBars"> number of bars </param>
        /// <returns>1 - rising, -1 falling, 0 flat or unknown</returns>
        public int SeriesTrend(IDataSeries series, int numOfBars)
        {
            bool rising = false;
            bool falling = false;
//            bool flat = false;

            int cnt = series.Count;
            for (int i = 1; i < cnt; i++)
            {
                if (series[i-1] > series[i])
                {
                    rising = true;
                }
                else if (series[i - 1] < series[i])
                {
                    falling = true;
                }
//                else if (series[i - 1] == series[i])
//                {
//                    flat = true;
//                }
                if (rising && falling)
                {
                    return 0;
                }
            }
            if (rising && falling)
                return 0;
            
            if (rising)
                return 1;

            if (falling)
                return -1;

            return 0;

        }

        #region TTMWave
        /// <summary>
        /// Return whether the wave crossed
        /// </summary>
        /// <param name="direction">true for Long and false for Short</param>
        /// <param name="waveLongOrShort">true for UP and false for DOWN</param>
        /// <param name="waveA_B_or_C">could be A, B or C</param>
        /// <param name="numOfBars">Number of Bars ago </param>
        /// <returns>1 for long, -1 for short, 0 didn't cross</returns>
        public int IsWaveCrossed(bool direction, bool waveLongOrShort, string waveA_B_or_C, int numOfBars)
        {
            List<double> listValues = new List<double>();
            bool aboveZero = false;
            bool belowZero = false;
            switch (waveA_B_or_C)
            {
                case "A":
                    for (int i = 0; i <= numOfBars; i++)
                        listValues.Add(waveLongOrShort?GetWaveALong(i):GetWaveAShort(i));
                    break;
                case "B":
                    for (int i = 0; i <= numOfBars; i++)
                        listValues.Add(waveLongOrShort ? GetWaveBLong(i) : GetWaveBShort(i));
                    break;
                case "C":
                    for (int i = 0; i <= numOfBars; i++)
                        listValues.Add(waveLongOrShort ? GetWaveCLong(i) : GetWaveCShort(i));
                    break;
            }
            
            foreach (double val in listValues)
            {
                if (val > 0)
                    aboveZero = true;
                else if (val < 0)
                    belowZero = true;
                if (aboveZero && belowZero)
                {
                    if (val > 0)
                        return -1;
                    else
                        return 1;
                }
            }

            return 0;
        }
        public double GetWaveAShort(int barsAgo)
        {
            double rVal = 0.0;
            rVal = TTMWaveAOC(false).Wave1[barsAgo];
            return rVal;
        }

        public double GetWaveALong(int barsAgo)
        {
            double rVal = 0.0;
            rVal = TTMWaveAOC(false).Wave2[barsAgo];
            return rVal;
        }

        public double GetWaveBShort(int barsAgo)
        {
            double rVal = 0.0;
            rVal = TTMWaveBOC(false).Wave1[barsAgo];
            return rVal;
        }

        public double GetWaveBLong(int barsAgo)
        {
            double rVal = 0.0;
            rVal = TTMWaveBOC(false).Wave2[barsAgo];
            return rVal;
        }

        public double GetWaveCShort(int barsAgo)
        {
            double rVal = 0.0;
            rVal = TTMWaveCOC(false).Wave1[barsAgo];
            return rVal;
        }

        public double GetWaveCLong(int barsAgo)
        {
            double rVal = 0.0;
            rVal = TTMWaveCOC(false).Wave2[barsAgo];
            return rVal;
        }
        #endregion
    }
}
