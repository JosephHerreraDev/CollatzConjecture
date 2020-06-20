using System;
using System.Data;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CollatzConjeture
{	
	public partial class MainWindow : Window
	{
		//Load variables
		DataTable mainTable = new DataTable("Numbers");
		int Click = 0;

		public MainWindow()
		{
			InitializeComponent();			
		}

		#region Check Value
		//Revision if the entry is valid
		private void TxbNumber_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (System.Text.RegularExpressions.Regex.IsMatch(txbNumber.Text, "[^0-9]"))
			{
				MessageBox.Show("Please enter only positive, not decimal numbers.");
				txbNumber.Text = txbNumber.Text.Remove(txbNumber.Text.Length - 1);
			}
		}
		#endregion

		#region Click Button Start & Clear
		private void BtnStart_Click(object sender, RoutedEventArgs e)
		{
			string Number = txbNumber.Text;
			CollatzConjeture(Number);
			Click++;
			Cleaner(Click, Number);
		}

		private void BtnClear_Click(object sender, RoutedEventArgs e)
		{
			txbNumber.Text = "";
		}

		#endregion

		#region Main Method
		public void CollatzConjeture(string number)
		{
			//Adds column to datatable
			if(mainTable.Columns.Count < 1)			
				mainTable.Columns.Add("Number");

			//Initialize variables and add row to dataset
			string positiveString = number;
			var posBigInt = BigInteger.Parse(positiveString);
			mainTable.Rows.Add(number);

			//Loop to find numbers and add the to the list
			while (posBigInt != 1)
			{
				if (posBigInt % 2 == 0)
				{
					posBigInt /= 2;

					string SposBigInt = Convert.ToString(posBigInt);
					mainTable.Rows.Add(SposBigInt);
				}
				else
				{
					posBigInt = (posBigInt * 3) + 1;
					string SposBigInt1 = Convert.ToString(posBigInt);
					mainTable.Rows.Add(SposBigInt1);
				}
			}

			//Add datagrid items source and auto scroll
			dataGrid.ItemsSource = mainTable.AsDataView();

			if (dataGrid.Items.Count > 0)
			{
				var border = VisualTreeHelper.GetChild(dataGrid, 0) as Decorator;
				if (border != null)
				{
					var scroll = border.Child as ScrollViewer;
					if (scroll != null) scroll.ScrollToEnd();
				}
			}
		}
		#endregion

		#region Cleaner
		public void Cleaner(int clicks, string initialNumber)
		{
			if (clicks > 1)
			{
				mainTable.Rows.Clear();
				CollatzConjeture(initialNumber);
			}
		}
		#endregion		
	}
}
