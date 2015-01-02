//	© Joydeep Mitra.
//	visit www.volumedigger.com for more indicators and strategies.
//  if you like the strategy then please do support the project by donating a small sum.
//  http://www.volumedigger.com/Donate.aspx

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

using System.Windows.Forms;
using System.Collections.Generic;

#endregion

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
	
	
    /// <summary>
    /// Enter the description of your strategy here
    /// </summary>
    [Description("Puts custom Buy Sell button in the ToolStrip from where Unmanaged orders can be placed")]
    public class ChartTrader : Strategy
    {
		
		class mitOrder : IEquatable <mitOrder>
		{
			public mitOrder()
			{}
			
			
			public OrderAction Action{get;set;}
			public OrderType Type{get;set;}
			public int Quantity{get;set;}
			public int Remaining{get;set;}
			public double LimitPrice{get;set;}
			public double StopPrice{get;set;}
			public OrderState State{get;set;}
			public double FillPrice{get;set;}
			
			public bool Equals(mitOrder mitorder)
			{
				return mitorder.Token == Token;
			}
			
			public DateTime TimeStamp{get;set;}
			public string Token{get;set;}
		}
		
		
        #region Variables
        //Form
		Form frm = null;
		//buttons
		Button btnSubmitOrder = null;
		Button btnModifyOrder = null;
		Button btnCancelOrder = null;
		//numeric up downs
		NumericUpDown nudQuantity = null;
		NumericUpDown nudLmtPrice = null;
		NumericUpDown nudStopPrice = null;
		//combo box
		ComboBox cbOrderAction = null;
		ComboBox cbOrderType = null;
		//ComboBox cbCompare = null;
		//datagridview
		DataGridView dgv = null;
		//labels
		Label label1 = null;
		Label label2 = null;
		Label label3 = null;
		Label label4 = null;
		Label label5 = null;
		//Label label6 = null;
		Label lblMsg = null;
//				
//		IOrder buyorder = null;
//		IOrder sellorder = null;
				
		
			
		//binding list
		BindingList<mitOrder> blist = null;
		
		List<IOrder> olist = null;
		
		//
		//int row = 0;
		
		#endregion

        /// <summary>
        /// This method is used to configure the strategy and is called once before any strategy method is called.
        /// </summary>
        protected override void Initialize()
        {
            CalculateOnBarClose = false;
			Unmanaged = true;
        }
		
		protected override void OnStartUp()
		{
			CreateForm();
			blist = new BindingList<mitOrder>();
			olist = new List<IOrder>();
		}
		
		
        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
			
			
        }
		
		protected override void OnMarketData(MarketDataEventArgs e)
		{
			if (e.MarketDataType == MarketDataType.Ask)
			{
				base.DrawLine("ask",false,0,e.Price,-3,e.Price,Color.Red,DashStyle.Solid,1);
			}
			else if (e.MarketDataType == MarketDataType.Bid)
			{
				base.DrawLine("bid",false,0,e.Price,-3,e.Price,Color.Green,DashStyle.Solid,1);
			}
		}
			
		
		protected override void OnExecution(IExecution execution)
		{
			mitOrder mo = new mitOrder();
			mo.Token = execution.Order.Token;
			int i = blist.IndexOf(mo);
			if (i >= 0)
			{
				blist[i].State = execution.Order.OrderState;
				blist[i].FillPrice = execution.Order.AvgFillPrice;
				blist[i].Remaining = blist[i].Remaining - execution.Quantity;
				dgv.Refresh();
			}

			if (execution.Order.OrderState == OrderState.Filled || execution.Order.OrderState == OrderState.Cancelled
				|| execution.Order.OrderState == OrderState.Rejected)
			{
				if (olist.Contains(execution.Order))
				{
					olist.Remove(execution.Order);
					if (i >=0 ) blist.RemoveAt(i);
				}
			}
			
			//lblMsg.Text = olist.Count.ToString() + " orders remaining";
			if (Position.MarketPosition == MarketPosition.Long)
			{
				lblMsg.Text = Position.MarketPosition.ToString()  + ": Quantity: " +  Position.Quantity.ToString() + " Price: " + Position.AvgPrice.ToString();
				lblMsg.ForeColor = Color.Blue;
			}
			else if (Position.MarketPosition == MarketPosition.Short)
			{
				lblMsg.Text = Position.MarketPosition.ToString()  + ": Quantity: " +  Position.Quantity.ToString() + " Price: " + Position.AvgPrice.ToString();
				lblMsg.ForeColor = Color.Brown;
			}
			else if (Position.MarketPosition == MarketPosition.Flat)
			{
				lblMsg.Text = "";
				//lblMsg.Text = olist.Count.ToString() + " orders pending";
				lblMsg.ForeColor = Color.Black;
			}
		}
		
		
		protected override void OnOrderUpdate(IOrder order)
		{
			
	
			if (order.OrderState == OrderState.Cancelled)
			{
				if (olist.Contains(order))
				{
					olist.Remove(order);
				}
				mitOrder mo= new mitOrder();
				mo.Token = order.Token;

				int i = blist.IndexOf(mo);
				if (i >=0)
				{
					blist.RemoveAt(i);
				}
			}
			
			//lblMsg.Text = olist.Count.ToString();
		}
			
			
			
		#region "create form"
		private void CreateForm()
		{
			if (ChartControl != null)
			{
				//ToolStrip toolstrip = (ToolStrip)ChartControl.Controls["tsrTool"];
				
				//datagridview
				this.dgv = new DataGridView();
				this.dgv.AllowUserToAddRows = false;
            	this.dgv.AllowUserToDeleteRows = false;
            	this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            	this.dgv.Location = new System.Drawing.Point(0, 52);
            	this.dgv.Name = "dgv";
            	this.dgv.ReadOnly = true;
            	this.dgv.RowHeadersVisible = false;
            	this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            	this.dgv.Size = new System.Drawing.Size(847, 239);
            	this.dgv.TabIndex = 9;
            	//this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            	// btnSubmitOrder
				this.btnSubmitOrder = new Button();
            	this.btnSubmitOrder.Location = new System.Drawing.Point(532, 11);
            	this.btnSubmitOrder.Name = "btnSubmitOrder";
            	this.btnSubmitOrder.Size = new System.Drawing.Size(75, 23);
            	this.btnSubmitOrder.TabIndex = 6;
            	this.btnSubmitOrder.Text = "Submit";
            	this.btnSubmitOrder.UseVisualStyleBackColor = true;
				this.btnSubmitOrder.Click += new System.EventHandler(this.btnSubmitOrder_Click);
            	// btnModifyOrder
				this.btnModifyOrder = new Button();
            	this.btnModifyOrder.Location = new System.Drawing.Point(626, 11);
            	this.btnModifyOrder.Name = "btnModifyOrder";
            	this.btnModifyOrder.Size = new System.Drawing.Size(75, 23);
            	this.btnModifyOrder.TabIndex = 7;
            	this.btnModifyOrder.Text = "Modify";
            	this.btnModifyOrder.UseVisualStyleBackColor = true;
            	this.btnModifyOrder.Click += new System.EventHandler(this.btnModifyOrder_Click);
				// btnCancelOrder
				this.btnCancelOrder = new Button();
            	this.btnCancelOrder.Location = new System.Drawing.Point(720, 11);
            	this.btnCancelOrder.Name = "btnCancelOrder";
            	this.btnCancelOrder.Size = new System.Drawing.Size(75, 23);
            	this.btnCancelOrder.TabIndex = 8;
            	this.btnCancelOrder.Text = "Cancel";
            	this.btnCancelOrder.UseVisualStyleBackColor = true;
            	this.btnCancelOrder.Click += new System.EventHandler(this.btnCancelOrder_Click);
            	// cbOrderType
				this.cbOrderType = new ComboBox();
            	this.cbOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            	this.cbOrderType.FormattingEnabled = true;
            	this.cbOrderType.Items.AddRange(new object[] {
            	"Market",
            	"Limit",
            	"Stop",
            	"StopLimit"});
            	this.cbOrderType.Location = new System.Drawing.Point(178, 13);
            	this.cbOrderType.Name = "cbOrderType";
            	this.cbOrderType.Size = new System.Drawing.Size(77, 21);
            	this.cbOrderType.TabIndex = 2;
            	this.cbOrderType.SelectedIndexChanged += new System.EventHandler(this.cbOrderType_SelectedIndexChanged);
            	// cbOrderAction
				this.cbOrderAction = new ComboBox();
            	this.cbOrderAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            	this.cbOrderAction.FormattingEnabled = true;
            	this.cbOrderAction.Items.AddRange(new object[] {
            	"Buy",
            	"Sell"});
            	this.cbOrderAction.Location = new System.Drawing.Point(12, 13);
            	this.cbOrderAction.Name = "cbOrderAction";
            	this.cbOrderAction.Size = new System.Drawing.Size(77, 21);
            	this.cbOrderAction.TabIndex = 0;
//				// cbCompare
//            	// 
//            	this.cbCompare = new ComboBox();
//				this.cbCompare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            	this.cbCompare.FormattingEnabled = true;
//            	this.cbCompare.Items.AddRange(new object[] {
//            	"GreaterEqual",
//            	"LesserEqual"});
//            	this.cbCompare.Location = new System.Drawing.Point(344, 13);
//            	this.cbCompare.Name = "cbCompare";
//            	this.cbCompare.Size = new System.Drawing.Size(77, 21);
//            	this.cbCompare.TabIndex = 4;
				// nudQuantity
				this.nudQuantity = new NumericUpDown();
            	this.nudQuantity.Location = new System.Drawing.Point(95, 14);
            	this.nudQuantity.Maximum = new decimal(new int[] {
            	9999999,
            	0,
            	0,
            	0});
            	this.nudQuantity.Minimum = new decimal(new int[] {
            	1,
            	0,
            	0,
            	0});
            	this.nudQuantity.Name = "nudQuantity";
            	this.nudQuantity.Size = new System.Drawing.Size(77, 20);
            	this.nudQuantity.TabIndex = 1;
            	this.nudQuantity.Value = new decimal(new int[] {
            	1,
            	0,
            	0,
            	0});
            
				// **********************************************
				//calculate the decimal places
				//decimal places
				double d = TickSize - (int)TickSize;
				string dstr = d.ToString();
				int decimalplaces;
				if (dstr.IndexOf(".") > 0) decimalplaces = dstr.Length - 2;
				else decimalplaces = 0;
				//*********************************************************
				
				// nudStopPrice
				this.nudStopPrice = new NumericUpDown();
            	this.nudStopPrice.Location = new System.Drawing.Point(344, 14);
            	this.nudStopPrice.Maximum = new decimal(new int[] {
            	9999999,
            	0,
            	0,
            	0});
            	this.nudStopPrice.Name = "nudStopPrice";
            	this.nudStopPrice.Size = new System.Drawing.Size(77, 20);
            	this.nudStopPrice.TabIndex = 5;
				this.nudStopPrice.DecimalPlaces = decimalplaces;
				this.nudStopPrice.Increment = (decimal)TickSize;
				// nudLmtPrice
				this.nudLmtPrice = new NumericUpDown();
            	this.nudLmtPrice.Location = new System.Drawing.Point(261, 14);
	            this.nudLmtPrice.Maximum = new decimal(new int[] {
    	        9999999,
        	    0,
            	0,
	            0});
    	        this.nudLmtPrice.Name = "nudLmtPrice";
        	    this.nudLmtPrice.Size = new System.Drawing.Size(77, 20);
            	this.nudLmtPrice.TabIndex = 3;
				this.nudLmtPrice.DecimalPlaces = decimalplaces;
				this.nudLmtPrice.Increment = (decimal)TickSize;
				
				//
				// label1
				this.label1 = new Label();
            	this.label1.AutoSize = true;
            	this.label1.Location = new System.Drawing.Point(341, 0);
            	this.label1.Name = "label1";
            	this.label1.Size = new System.Drawing.Size(78, 13);
            	this.label1.TabIndex = 16;
            	this.label1.Text = "Stop Price";
				// label2
				this.label2 = new Label();
           		this.label2.AutoSize = true;
	            this.label2.Location = new System.Drawing.Point(9, 0);
    	        this.label2.Name = "label2";
        	    this.label2.Size = new System.Drawing.Size(66, 13);
            	this.label2.TabIndex = 11;
            	this.label2.Text = "Order Action";
            	// label3
				this.label3 = new Label();
            	this.label3.AutoSize = true;
	            this.label3.Location = new System.Drawing.Point(92, 0);
    	        this.label3.Name = "label3";
        	    this.label3.Size = new System.Drawing.Size(46, 13);
            	this.label3.TabIndex = 12;
            	this.label3.Text = "Quantity";
            	// label4
				this.label4 = new Label();
            	this.label4.AutoSize = true;
	            this.label4.Location = new System.Drawing.Point(175, 0);
    	        this.label4.Name = "label4";
        	    this.label4.Size = new System.Drawing.Size(60, 13);
            	this.label4.TabIndex = 13;
            	this.label4.Text = "Order Type";
            	// label5
				this.label5 = new Label();
            	this.label5.AutoSize = true;
	            this.label5.Location = new System.Drawing.Point(258, 0);
    	        this.label5.Name = "label5";
        	    this.label5.Size = new System.Drawing.Size(55, 13);
            	this.label5.TabIndex = 14;
            	this.label5.Text = "Limit Price";
//				//label6
//				this.label6 = new Label();
//				this.label6.AutoSize = true;
//            	this.label6.Location = new System.Drawing.Point(341, 0);
//            	this.label6.Name = "label6";
//            	this.label6.Size = new System.Drawing.Size(49, 13);
//            	this.label6.TabIndex = 15;
//            	this.label6.Text = "Compare";
				// lblMsg
				this.lblMsg = new Label();
            	this.lblMsg.AutoSize = true;
            	this.lblMsg.Location = new System.Drawing.Point(9, 36);
            	this.lblMsg.Name = "lblMsg";
            	this.lblMsg.Size = new System.Drawing.Size(17, 13);
            	this.lblMsg.TabIndex = 10;
            	this.lblMsg.Text = "www.volumedigger.com";	//please do not remove this line
				
				//form
				frm = new Form();
				frm.Name = "ChartTrader";
				frm.Text = "Chart Trader";
				//frm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
				frm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
				frm.MaximizeBox = false;
				frm.Size = new System.Drawing.Size(853,318);
				//frm.TopMost = true;
				frm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frm_FormClosing);
				frm.Resize += new System.EventHandler(frm_Resize);
				frm.Load += new System.EventHandler(frm_Load);
				
				frm.Controls.Add(btnSubmitOrder);
				frm.Controls.Add(btnModifyOrder);
				frm.Controls.Add(btnCancelOrder);
				frm.Controls.Add(nudStopPrice);
				frm.Controls.Add(nudLmtPrice);
				frm.Controls.Add(nudQuantity);
				frm.Controls.Add(cbOrderAction);
				frm.Controls.Add(cbOrderType);
				//frm.Controls.Add(cbCompare);
				frm.Controls.Add(dgv);
				frm.Controls.Add(label1);
				frm.Controls.Add(label2);
				frm.Controls.Add(label3);
				frm.Controls.Add(label4);
				frm.Controls.Add(label5);
				//frm.Controls.Add(label6);
				frm.Controls.Add(lblMsg);
								
				frm.Show();
			}
		}
		#endregion
		

		private void btnSubmitOrder_Click(object sender, EventArgs e)
        {
			TriggerCustomEvent(new CustomEvent(btnSubmitOrderClick),0,e);
        }
		private void btnSubmitOrderClick(object state)
		{
			OrderAction oAction = (OrderAction)Enum.Parse(typeof(OrderAction),cbOrderAction.SelectedItem.ToString());
			OrderType oType = (OrderType)Enum.Parse(typeof(OrderType),cbOrderType.SelectedItem.ToString());
			double limitprice = 0;
			double stopprice = 0;
			if (oType == OrderType.Limit)
			{
				limitprice = (double)nudLmtPrice.Value;
			}
			else if (oType == OrderType.Stop)
			{
				stopprice = (double)nudStopPrice.Value;
				if (oAction == OrderAction.Buy)
				{
					if (stopprice <= Close[0])
					{
						MessageBox.Show("Stop Price cannot be less than Last traded price");
						return;
					}
				}
				else if (oAction == OrderAction.Sell)
				{
					if (stopprice >= Close[0])
					{
						MessageBox.Show("Stop Price cannot be greater than Last traded price");
						return;
					}
				}
					
			}
			else if (oType == OrderType.StopLimit)
			{
				limitprice = (double)nudLmtPrice.Value;
				stopprice = (double)nudStopPrice.Value;
				if (oAction == OrderAction.Buy)
				{
					if (stopprice <= Close[0])
					{
						MessageBox.Show("Stop Price cannot be less than Last traded price");
						return;
					}
					if (limitprice < stopprice)
					{
						MessageBox.Show("Limit Price cannot be less than Stop Price");
						return;
					}
				}
				else if (oAction == OrderAction.Sell)
				{
					if (stopprice >= Close[0])
					{
						MessageBox.Show("Stop Price cannot be greater than Last traded price");
						return;
					}
					if (limitprice > stopprice)
					{
						MessageBox.Show("Limit Price cannot be greater than Stop Price");
						return;
					}
				}
			}
			
					
			IOrder order = SubmitOrder(0,oAction,oType,(int)nudQuantity.Value,limitprice,stopprice,"",oType.ToString() + oAction.ToString());
			olist.Add(order);
			
			mitOrder morder = new mitOrder();
			morder.Action = oAction;
			morder.StopPrice = stopprice;
			morder.FillPrice = order.AvgFillPrice;
			morder.LimitPrice = limitprice;
			morder.Quantity = (int)nudQuantity.Value;
			morder.Remaining = morder.Quantity;
			morder.State = OrderState.Working;
			morder.TimeStamp = order.Time;
			morder.Token = order.Token;
			morder.Type = oType;
								
			blist.Add(morder);
			dgv.DataSource = blist;
			
		}
		
		private void btnModifyOrder_Click(object sender, EventArgs e)
		{
			TriggerCustomEvent(new CustomEvent(btnModifyOrderClick),0,e);
		}
		private void btnModifyOrderClick(object state)
		{
			
			if (olist.Count == 0)
			{
				MessageBox.Show("No pending orders");
				return;
			}
			
			int row = dgv.CurrentCell.RowIndex;
			
			if (!dgv.Rows[row].Selected) 
			{
				MessageBox.Show("Please select a row");
				return;
			}
			
			
			
			if (blist[row].State == OrderState.Filled || blist[row].State == OrderState.Rejected 
				|| blist[row].State == OrderState.Cancelled)
			{
				lblMsg.Text = blist[row].State.ToString() + " orders cannot be modified";
				return;
			}
			
			
			//
			OrderAction oAction = blist[row].Action;
			OrderType oType = blist[row].Type;
			double limitprice = 0;
			double stopprice = 0;
			if (oType == OrderType.Limit)
			{
				limitprice = (double)nudLmtPrice.Value;
			}
			else if (oType == OrderType.Stop)
			{
				stopprice = (double)nudStopPrice.Value;
				if (oAction == OrderAction.Buy)
				{
					if (stopprice <= Close[0])
					{
						MessageBox.Show("Stop Price cannot be less than Last traded price");
						return;
					}
				}
				else if (oAction == OrderAction.Sell)
				{
					if (stopprice >= Close[0])
					{
						MessageBox.Show("Stop Price cannot be greater than Last traded price");
						return;
					}
				}
					
			}
			else if (oType == OrderType.StopLimit)
			{
				limitprice = (double)nudLmtPrice.Value;
				stopprice = (double)nudStopPrice.Value;
				if (oAction == OrderAction.Buy)
				{
					if (stopprice <= Close[0])
					{
						MessageBox.Show("Stop Price cannot be less than Last traded price");
						return;
					}
					if (limitprice < stopprice)
					{
						MessageBox.Show("Limit Price cannot be less than Stop Price");
						return;
					}
				}
				else if (oAction == OrderAction.Sell)
				{
					if (stopprice >= Close[0])
					{
						MessageBox.Show("Stop Price cannot be greater than Last traded price");
						return;
					}
					if (limitprice > stopprice)
					{
						MessageBox.Show("Limit Price cannot be greater than Stop Price");
						return;
					}
				}
			}
			
			//
			blist[row].Quantity = (int)nudQuantity.Value;
			blist[row].LimitPrice = limitprice;
			blist[row].StopPrice = stopprice;
			blist[row].Remaining = blist[row].Quantity;
			
			for (int i = 0;i <= olist.Count - 1;i++)
			{
				if (olist[i].Token == blist[row].Token)
				{
					ChangeOrder(olist[i],(int)nudQuantity.Value,limitprice,stopprice);
					break;
				}
			}
			
			dgv.Refresh();
			
		}
		
		private void btnCancelOrder_Click(object sender, EventArgs e)
		{
			TriggerCustomEvent(new CustomEvent(btnCancelOrderClick),0,e);
		}
		private void btnCancelOrderClick(object state)
		{
			
			if (olist.Count == 0)
			{
				MessageBox.Show("No pending orders");
				return;
			}
			
			int row = dgv.CurrentCell.RowIndex;
			if (!dgv.Rows[row].Selected) 
			{
				MessageBox.Show("Please select a row");
				return;
			}
			
					
			if (blist[row].State == OrderState.Filled || blist[row].State == OrderState.Rejected 
				|| blist[row].State == OrderState.Cancelled)
			{
				lblMsg.Text = blist[row].State.ToString() + " orders cannot be cancelled";
				return;
			}
			int i;
			for (i = 0;i <= olist.Count - 1;i++)
			{
				if (olist[i].Token == blist[row].Token)
				{
					CancelOrder(olist[i]);
					break;
				}
			}
			
			//olist.Remove(olist[i]);
			
			blist[row].State = OrderState.Cancelled;
			dgv.Refresh();
			//lblMsg.Text = blist[row].Action.ToString() + " order for " + blist[row].Remaining.ToString()
			//+ " contracts cancelled";
						
		}
		
		private void cbOrderType_SelectedIndexChanged(object sender, EventArgs e)
	 	{
			TriggerCustomEvent(new CustomEvent(cbOrderTypeSelectedIndexChanged),0,e);
		}
		private void cbOrderTypeSelectedIndexChanged(object state)
		{
			if (cbOrderType.SelectedIndex == 0)	
			{
				nudLmtPrice.Enabled = false;
				nudStopPrice.Enabled = false;
			}
			else if (cbOrderType.SelectedIndex == 1) 
			{
				nudLmtPrice.Enabled = true;
				nudStopPrice.Enabled = false;
			}
			else if (cbOrderType.SelectedIndex == 2)
			{
				nudLmtPrice.Enabled = false;
				nudStopPrice.Enabled = true;
			}
			else if (cbOrderType.SelectedIndex == 3)
			{
				nudLmtPrice.Enabled = true;
				nudStopPrice.Enabled = true;
			}
		}
		
		//frm load dosent works with triggercustomevent
		private void frm_Load(object sender,EventArgs e)
		{
			//TriggerCustomEvent(new CustomEvent(frmLoad),0,e);
			cbOrderType.SelectedIndex = 0;
			nudLmtPrice.Enabled = false;
			nudStopPrice.Enabled = false;
			cbOrderAction.SelectedIndex = 0;
			nudStopPrice.Value = (decimal)Close[0];
			nudLmtPrice.Value = (decimal)Close[0];
			
			dgv.DataSource = blist;
		}
//		private void frmLoad(object state)
//		{
//			this.cbOrderType.SelectedIndex = 0;
//			this.nudLmtPrice.Enabled = false;
//		}
		
		//resize will give an error when custom events exceeds 100. so we call the same directly.
		private void frm_Resize(object sender,EventArgs e)
		{
			//TriggerCustomEvent(new CustomEvent(frmResize),0,e);
			dgv.Width = frm.Width - 6;
			dgv.Height = frm.Height - 76;
		}
//		private void frmResize(object state)
//		{
//			dgv.Width = frm.Width;
//			dgv.Height = frm.Height - 76;
//		}
		
		private void frm_FormClosing(object sender,FormClosingEventArgs e)
		{
			TriggerCustomEvent(new CustomEvent(frmFormClosing),0,e);
		}
		private void frmFormClosing(object state)
		{
			FormClosingEventArgs e = (FormClosingEventArgs)state;
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
			}
		}
		
		protected override void OnTermination()
		{
			if (ChartControl != null)
			{
				//ToolStrip toolstrip = (ToolStrip)ChartControl.Controls["tsrTool"];
				
				if (frm != null)
				{
					//remove the data grid view
					//this.dgv.CellClick -= new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            		frm.Controls.Remove(dgv);
					this.dgv.Dispose();
					//remove the buttons
					this.btnSubmitOrder.Click -= new System.EventHandler(btnSubmitOrder_Click);
					frm.Controls.Remove(btnSubmitOrder);
					this.btnSubmitOrder.Dispose();
					
					this.btnModifyOrder.Click -= new System.EventHandler(btnModifyOrder_Click);
					frm.Controls.Remove(btnModifyOrder);
					this.btnModifyOrder.Dispose();
					
					this.btnCancelOrder.Click -= new System.EventHandler(btnCancelOrder_Click);
					frm.Controls.Remove(btnCancelOrder);
					this.btnCancelOrder.Dispose();
					
					//remove the numeric up down boxes
					frm.Controls.Remove(nudStopPrice);
					this.nudStopPrice.Dispose();
					
					frm.Controls.Remove(this.nudLmtPrice);
					this.nudLmtPrice.Dispose();
					
					frm.Controls.Remove(nudQuantity);
					this.nudQuantity.Dispose();
					
					//remove the combo box
					this.cbOrderType.SelectedIndexChanged -= new System.EventHandler(cbOrderType_SelectedIndexChanged);
            		frm.Controls.Remove(cbOrderType);
					this.cbOrderType.Dispose();
					
					frm.Controls.Remove(cbOrderAction);
					this.cbOrderAction.Dispose();
					
//					frm.Controls.Remove(cbCompare);
//					this.cbCompare.Dispose();
					
					frm.Controls.Remove(label1);
					label1.Dispose();
					
					frm.Controls.Remove(label2);
					label2.Dispose();
					
					frm.Controls.Remove(label3);
					label3.Dispose();
					
					frm.Controls.Remove(label4);
					label4.Dispose();
					
					frm.Controls.Remove(label5);
					label5.Dispose();
					
//					frm.Controls.Remove(label6);
//					label6.Dispose();
					
					frm.Controls.Remove(lblMsg);
					lblMsg.Dispose();
					
					frm.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(frm_FormClosing);
					frm.Load -= new System.EventHandler(frm_Load);
					frm.Resize -= new System.EventHandler(frm_Resize);
					frm.Close();
					frm.Dispose();
				}
				
				if (blist != null)
				{
					blist.Clear();
				}
				if (olist != null)
				{
					olist.Clear();
				}
			}
		}
		
		

        #region Properties
      
        #endregion
    }
}
