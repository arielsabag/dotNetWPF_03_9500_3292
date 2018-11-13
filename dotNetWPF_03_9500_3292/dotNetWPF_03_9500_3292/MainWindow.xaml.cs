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
//using System.Collections;
namespace dotNetWPF_03_9500_3292
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		PrinterUserControl currentPrinter;
		Queue<PrinterUserControl> queue;
		public MainWindow()
		{
			InitializeComponent();
			queue = new Queue<PrinterUserControl>();
			foreach (Control item in printersGrid.Children)
			{
				if (item is PrinterUserControl)
				{
					PrinterUserControl printer = item as PrinterUserControl;
					queue.Enqueue(printer);
				}
			}
			

			currentPrinter = queue.Dequeue();
			this.currentPrinter.PageMissing += PageMiss;
			this.currentPrinter.InkEmpty += InkEmpt;

			this.printButton.MouseEnter += Button_MouseEnter;
			this.printButton.MouseLeave += Button_MouseLeave;
		}


		public void PageMiss(object sender, EventArgs e)
		{
			PrinterEventArgs p = e as PrinterEventArgs;

			MessageBox.Show("at: " + p.Date_Time.ToString() + "\nMessage from pointer: Missing " + this.currentPrinter.CurrentAmountOfPageToPrint + " pages"  , p.PrinterName + p.Error_Warning_Message.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
			this.currentPrinter.AddPages();
			
		}
		public void InkEmpt(object sender, EventArgs e)
		{
			PrinterEventArgs p = e as PrinterEventArgs;
			if (p.CriticalWarning)
			{
				MessageBox.Show("at: " + p.Date_Time.ToString() + "\nMessage from pointer: your ink is only " + this.currentPrinter.InkCount + " %", p.PrinterName + p.Error_Warning_Message.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
			}
			if (!p.CriticalWarning)
			{
				MessageBox.Show("at: " + p.Date_Time.ToString() + "\nMessage from pointer: your ink is only " + this.currentPrinter.InkCount + " %",p.PrinterName + p.Error_Warning_Message.ToString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);

			}

		}
		private void printButton_Click(object sender, RoutedEventArgs e)
		{

			this.currentPrinter.Print();
			MessageBox.Show("clicked");
		}
		private void Button_MouseEnter(object sender, MouseEventArgs e)
		{
			Button b = sender as Button;
			if (b != null)
			{
				b.FontSize += 10;
			}
		}
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
