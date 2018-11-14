using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dotNetWPF_03_9500_3292
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		PrinterUserControl currentPrinter;
		Queue<PrinterUserControl> queue; // queue of printers
		public MainWindow()
		{
			InitializeComponent();
			queue = new Queue<PrinterUserControl>();
			foreach (Control item in printersGrid.Children)
			{
				if (item is PrinterUserControl)
				{
					PrinterUserControl printer = item as PrinterUserControl;
					printer.PageMissing += PageMiss; // add event pageMissing
					printer.InkEmpty += InkEmpt; // add event inkEmpty
					queue.Enqueue(printer); // push to queue
				}
			}

			currentPrinter = queue.Dequeue(); // take one printer
			this.printButton.MouseEnter += Button_MouseEnter; // add event
			this.printButton.MouseLeave += Button_MouseLeave; // add event
		}
		/// <summary>
		/// event pageMiss
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void PageMiss(object sender, EventArgs e)
		{
			PrinterEventArgs p = e as PrinterEventArgs;
			// show warnning
			MessageBox.Show("at: " + p.Date_Time.ToString() + "\nMessage from pointer: Missing " + this.currentPrinter.CurrentAmountOfPageToPrint + " pages"  , p.PrinterName + p.Error_Warning_Message.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
			this.currentPrinter.AddPages(); // add pages
			currentPrinter.pageLabel.Foreground = System.Windows.Media.Brushes.Black; // paint in black again
			queue.Enqueue(currentPrinter); // push to queue
			currentPrinter = queue.Dequeue();// take the next printer from queue
		}
		/// <summary>
		/// event inkEmpt
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void InkEmpt(object sender, EventArgs e)
		{
			PrinterEventArgs p = e as PrinterEventArgs;
			// if ink over or under 1%
			if (p.CriticalWarning)
			{
				// show warnning
				MessageBox.Show("at: " + p.Date_Time.ToString() + "\nMessage from pointer: your ink is only " + this.currentPrinter.InkCount + " %", p.PrinterName + p.Error_Warning_Message.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
				this.currentPrinter.AddInk(); // add ink
				currentPrinter.inkLabel.Foreground = System.Windows.Media.Brushes.Black; // paint in black again
				queue.Enqueue(currentPrinter);// push to queue
				currentPrinter = queue.Dequeue();// take the next printer from queue
			}
			// if amount is low
			if (!p.CriticalWarning)
			{
				// show warnning
				MessageBox.Show("at: " + p.Date_Time.ToString() + "\nMessage from pointer: your ink is only " + this.currentPrinter.InkCount + " %",p.PrinterName + p.Error_Warning_Message.ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}
		/// <summary>
		/// event button click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void printButton_Click(object sender, RoutedEventArgs e)
		{
			this.currentPrinter.Print();
			MessageBox.Show("clicked");
		}

		/// <summary>
		/// mouse over event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_MouseEnter(object sender, MouseEventArgs e)
		{
			Button b = sender as Button;
			if (b != null)
			{
				b.FontSize += 10;
			}
		}
		/// <summary>
		/// mouse leave vevent
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_MouseLeave(object sender, MouseEventArgs e)
		{
			Button b = sender as Button;
			if (b != null)
			{
				b.FontSize -= 10;
			}
		}
	}
}
