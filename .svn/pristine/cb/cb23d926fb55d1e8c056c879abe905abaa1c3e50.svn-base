#region Using declarations
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Indicator;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Strategy;
#endregion

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    /// <summary>
    /// just test
    /// </summary>
    [Description("just test")]
    public class RValStrategy : Strategy
    {
        #region Variables
        // Wizard generated variables
        private int overbought = 8; // Default setting for Overbought
        private int oversold = -8; // Default setting for Oversold
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            Add(RValueCharts(false, true, 2, Color.Red, Color.Blue, Color.Red, Color.Thistle, 5, true, Color.BurlyWood, Color.Black, 1, 60, false, false, Color.Green, global::RValueCharts.Utility.ValueChartStyle.CandleStick));

            CalculateOnBarClose = true;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            // Condition set 1
            if (RValueCharts(false, true, 2, Color.Red, Color.Blue, Color.Red, Color.Thistle, 5, true, Color.BurlyWood, Color.Black, 1, 60, false, false, Color.Green, global::RValueCharts.Utility.ValueChartStyle.CandleStick).VClose[0] >= Overbought)
            {
                DrawDot("Over" + CurrentBar, false, 0, High[0] + 4 * TickSize, Color.Red);
            }
        }

        #region Properties
        [Description("when overbought")]
        [GridCategory("Parameters")]
        public int Overbought
        {
            get { return overbought; }
            set { overbought = Math.Max(8, value); }
        }

        [Description("when oversold")]
        [GridCategory("Parameters")]
        public int Oversold
        {
            get { return oversold; }
            set { oversold = Math.Max(-8, value); }
        }
        #endregion
    }
}

#region Wizard settings, neither change nor remove
/*@
<?xml version="1.0" encoding="utf-16"?>
<NinjaTrader>
  <Name>RValStrategy</Name>
  <CalculateOnBarClose>True</CalculateOnBarClose>
  <Description>just test</Description>
  <Parameters>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>8</Default2>
      <Default3>
      </Default3>
      <Description>when overbought</Description>
      <Minimum>8</Minimum>
      <Name>Overbought</Name>
      <Type>int</Type>
    </Parameter>
    <Parameter>
      <Default1>
      </Default1>
      <Default2>-8</Default2>
      <Default3>
      </Default3>
      <Description>when oversold</Description>
      <Minimum>-8</Minimum>
      <Name>Oversold</Name>
      <Type>int</Type>
    </Parameter>
  </Parameters>
  <State>
    <CurrentState>
      <StrategyWizardState xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
        <Name>Flat</Name>
        <Sets>
          <StrategyWizardStateSet>
            <Actions>
              <StrategyWizardAction>
                <DisplayName>Dot</DisplayName>
                <Help />
                <MemberName>DrawDot</MemberName>
                <Parameters>
                  <string>tag</string>
                  <string>autoScale</string>
                  <string>barsAgo</string>
                  <string>y</string>
                  <string>color</string>
                </Parameters>
                <Values>
                  <string>"Over"</string>
                  <string>False</string>
                  <string>0</string>
                  <string>High[0] + 4 * TickSize</string>
                  <string>Color.Red</string>
                </Values>
                <WizardItems>
                  <StrategyWizardItem>
                    <DisplayName />
                    <IsIndicator>false</IsIndicator>
                    <IsInt>false</IsInt>
                    <IsMethod>false</IsMethod>
                    <IsSet>true</IsSet>
                    <MemberName />
                    <Parameters />
                    <Values />
                    <WizardItems />
                  </StrategyWizardItem>
                  <StrategyWizardItem>
                    <DisplayName />
                    <IsIndicator>false</IsIndicator>
                    <IsInt>false</IsInt>
                    <IsMethod>false</IsMethod>
                    <IsSet>true</IsSet>
                    <MemberName />
                    <Parameters />
                    <Values />
                    <WizardItems />
                  </StrategyWizardItem>
                  <StrategyWizardItem>
                    <DisplayName>barsAgo</DisplayName>
                    <IsIndicator>false</IsIndicator>
                    <IsInt>true</IsInt>
                    <IsMethod>false</IsMethod>
                    <IsSet>false</IsSet>
                    <MemberName>barsAgo</MemberName>
                    <Parameters />
                    <Values />
                    <WizardItems />
                  </StrategyWizardItem>
                  <StrategyWizardItem>
                    <DisplayName>High</DisplayName>
                    <IsIndicator>false</IsIndicator>
                    <IsInt>false</IsInt>
                    <IsMethod>false</IsMethod>
                    <IsSet>true</IsSet>
                    <MemberName>High</MemberName>
                    <Parameters>
                      <string>	barsAgo</string>
                      <string>	offsetType</string>
                      <string>	offset</string>
                    </Parameters>
                    <Values>
                      <string>0</string>
                      <string>NinjaTrader.Strategy.CalculationMode.Ticks</string>
                      <string>4</string>
                    </Values>
                    <WizardItems>
                      <StrategyWizardItem>
                        <DisplayName>	barsAgo</DisplayName>
                        <IsIndicator>false</IsIndicator>
                        <IsInt>true</IsInt>
                        <IsMethod>false</IsMethod>
                        <IsSet>false</IsSet>
                        <MemberName>0</MemberName>
                        <Parameters />
                        <Values />
                        <WizardItems />
                      </StrategyWizardItem>
                      <StrategyWizardItem>
                        <DisplayName />
                        <IsIndicator>false</IsIndicator>
                        <IsInt>false</IsInt>
                        <IsMethod>false</IsMethod>
                        <IsSet>true</IsSet>
                        <MemberName />
                        <Parameters />
                        <Values />
                        <WizardItems />
                      </StrategyWizardItem>
                      <StrategyWizardItem>
                        <DisplayName>Numeric value</DisplayName>
                        <IsIndicator>false</IsIndicator>
                        <IsInt>true</IsInt>
                        <IsMethod>false</IsMethod>
                        <IsSet>true</IsSet>
                        <MemberName>4</MemberName>
                        <Parameters />
                        <Values />
                        <WizardItems />
                      </StrategyWizardItem>
                    </WizardItems>
                  </StrategyWizardItem>
                  <StrategyWizardItem>
                    <DisplayName />
                    <IsIndicator>false</IsIndicator>
                    <IsInt>false</IsInt>
                    <IsMethod>false</IsMethod>
                    <IsSet>true</IsSet>
                    <MemberName />
                    <Parameters />
                    <Values />
                    <WizardItems />
                  </StrategyWizardItem>
                </WizardItems>
              </StrategyWizardAction>
            </Actions>
            <Conditions>
              <StrategyWizardCondition>
                <AndOr>And</AndOr>
                <Left>
                  <DisplayName>RValueCharts</DisplayName>
                  <IsIndicator>true</IsIndicator>
                  <IsInt>false</IsInt>
                  <IsMethod>true</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>RValueCharts</MemberName>
                  <Parameters>
                    <string>	inputSeries</string>
                    <string>ChangeBackground</string>
                    <string>DotDraw</string>
                    <string>DotOffset</string>
                    <string>DotOverBought</string>
                    <string>DotOverSold</string>
                    <string>DownColor</string>
                    <string>ExtremeOver</string>
                    <string>Length</string>
                    <string>MainChartSettings</string>
                    <string>ModerateOver</string>
                    <string>OutlineColor</string>
                    <string>OutlineWidth</string>
                    <string>SoundAlertInterval</string>
                    <string>SoundWarnings</string>
                    <string>TextWarnings</string>
                    <string>UpColor</string>
                    <string>Vct</string>
                    <string>	plot</string>
                    <string>	barsAgo</string>
                    <string>	offsetType</string>
                    <string>	offset</string>
                    <string>	plotOnChart</string>
                  </Parameters>
                  <Values>
                    <string>DefaultInput</string>
                    <string>False</string>
                    <string>True</string>
                    <string>2</string>
                    <string>Color.Red</string>
                    <string>Color.Blue</string>
                    <string>Color.Red</string>
                    <string>Color.Thistle</string>
                    <string>5</string>
                    <string>True</string>
                    <string>Color.BurlyWood</string>
                    <string>Color.Black</string>
                    <string>1</string>
                    <string>60</string>
                    <string>False</string>
                    <string>False</string>
                    <string>Color.Green</string>
                    <string>RValueCharts.Utility.ValueChartStyle.CandleStick</string>
                    <string>"VClose"</string>
                    <string>0</string>
                    <string>NinjaTrader.Strategy.CalculationMode.Ticks</string>
                    <string>0</string>
                    <string>True</string>
                  </Values>
                  <WizardItems>
                    <StrategyWizardItem>
                      <DisplayName>DefaultInput</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>DefaultInput</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>False</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>False</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>True</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>True</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>2</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>2</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>Color [Red]</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>Color [Red]</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>Color [Blue]</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>Color [Blue]</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>Color [Red]</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>Color [Red]</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>Color [Thistle]</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>Color [Thistle]</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>5</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>5</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>True</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>True</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>Color [BurlyWood]</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>Color [BurlyWood]</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>Color [Black]</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>Color [Black]</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>1</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>1</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>60</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>60</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>False</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>False</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>False</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>False</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>Color [Green]</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>Color [Green]</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>RValueCharts.Utility.ValueChartStyle.CandleStick</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName>RValueCharts.Utility.ValueChartStyle.CandleStick</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>	barsAgo</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>false</IsSet>
                      <MemberName>0</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName>	offset</DisplayName>
                      <IsIndicator>false</IsIndicator>
                      <IsInt>true</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>false</IsSet>
                      <MemberName>0</MemberName>
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                    <StrategyWizardItem>
                      <DisplayName />
                      <IsIndicator>false</IsIndicator>
                      <IsInt>false</IsInt>
                      <IsMethod>false</IsMethod>
                      <IsSet>true</IsSet>
                      <MemberName />
                      <Parameters />
                      <Values />
                      <WizardItems />
                    </StrategyWizardItem>
                  </WizardItems>
                </Left>
                <LookBackPeriod>1</LookBackPeriod>
                <Operator>&gt;=</Operator>
                <Right>
                  <DisplayName>Overbought</DisplayName>
                  <IsIndicator>false</IsIndicator>
                  <IsInt>true</IsInt>
                  <IsMethod>false</IsMethod>
                  <IsSet>true</IsSet>
                  <MemberName>Overbought</MemberName>
                  <Parameters />
                  <Values />
                  <WizardItems />
                </Right>
              </StrategyWizardCondition>
            </Conditions>
          </StrategyWizardStateSet>
        </Sets>
        <StopTargets />
      </StrategyWizardState>
    </CurrentState>
  </State>
</NinjaTrader>
@*/
#endregion
