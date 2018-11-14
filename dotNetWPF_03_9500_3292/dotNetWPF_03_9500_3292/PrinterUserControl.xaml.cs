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
	/// Interaction logic for PrinterUserControl.xaml
	/// </summary>
	public partial class PrinterUserControl : UserControl
	{

		public PrinterUserControl()
		{
			InitializeComponent();
			PrinterName = "Printer " + code++;  // sets the names  printer1 , printer 2 ...
			AddInk(); // ad ink to current printer
			AddPages(); // add pages to current printer
			DataContext = this;  
		}

		PrinterEventArgs p;

		public delegate void EventHandler<PrinterEventArgs>(object sender, PrinterEventArgs e);
		public event EventHandler PageMissing;  // page missing  event
		public event EventHandler InkEmpty;  // ink empty event

		static Random r = new Random(); // to randomly add or substract ink ad pages from current printer
		static int code = 1; // for each printer name
		/// <summary>
		/// constants values
		/// </summary>
		const double MAX_INK = 100;  // the max amount of ink
		const double MIN_ADD_INK = MAX_INK / 10; // the min amount to add to ink
		const double MAX_PRINT_INK = 70;  // max amount of ink to add toprinter
		const int MAX_PAGES = 400;  // the max amount of pages in printer
		const int MIN_ADD_PAGES = MAX_PAGES / 10;  // the min amount of pages to add to printer
		const int MAX_PRINT_PAGES = 300;  // max amount of pages to add to printer
										  // end of constants values


		public double MaxPages
		{
			get { return MAX_PAGES; }
		}


		/// <summary>
		/// reveling the printer name
		/// </summary>
		public string PrinterName { get; set; }

		/// <summary>
		/// reveling the ink amount
		/// </summary>
		private double inkCount;
		public double InkCount
		{
			get { return inkCount; }
			set { inkCount = value; }
		}

		/// <summary>
		/// reveling the page amount
		/// </summary>
		private int pageCount;
		public int PageCount
		{
        	get { return pageCount; }
			set{pageCount = value;}
		}
		/// <summary>
		///  the current random number of page to substract from current printer
		/// </summary>
		private int currentAmountOfPageToPrint;
		public int CurrentAmountOfPageToPrint
		{
			get { return currentAmountOfPageToPrint; }
			set { currentAmountOfPageToPrint = value; }
		}

		/// <summary>
		/// illustrate printing - decrease page count by ranndom number
		/// </summary>
		public void Print()
		{
			InkCount -= r.Next((int)(MAX_INK - InkCount)); //decrease ink amount randomly 
			CurrentAmountOfPageToPrint = r.Next(MAX_PAGES - pageCount);         //  the current random number of page to substract from current printer
			PageCount -= CurrentAmountOfPageToPrint;    //decrease page amount randomly 

			pageCountSlider.Value = PageCount; // update pageCountSlider to new amount of pages
			inkCountProgressBar.Value = InkCount;  // update inkCountProgressBar to new amount of ink

			if (PageCount <= 0) // if page over -> event
			{
				PageCount = 0; //cant be under 0
				pageLabel.Foreground = System.Windows.Media.Brushes.Red; // paint in red -> critical warnning
				PageMissing(this, new PrinterEventArgs(PrinterName, "  Page Missing !!!", true)); // send an event
			}
			if ((InkCount <= 15)&&(InkCount>=10))  // if ink amount is low -> event
			{
				inkLabel.Foreground = System.Windows.Media.Brushes.Yellow; // paint in yellow -> not critical warnning
				InkEmpty(this, new PrinterEventArgs(PrinterName, " Ink Empty", false)); // send an event
			}
			if ((InkCount <= 10) && (InkCount >= 1))  // if ink is lower -> event
			{
				inkLabel.Foreground = System.Windows.Media.Brushes.Orange; // paint in orange -> not critical warnning
				InkEmpty(this, new PrinterEventArgs(PrinterName, " Ink Empty", false));  // send an event
			}
			if (InkCount < 1)  // if ink over or under 1% -> event
			{
				InkCount = 0; // cant be under 0
				inkLabel.Foreground = System.Windows.Media.Brushes.Red; // paint in red -> critical warnning
				InkEmpty(this, new PrinterEventArgs(PrinterName, " Ink Empty", true)); // send an event
			}
		}

		/// <summary>
		/// randomly add pages to pagesCount
		/// </summary>
		public void AddPages()
		{
			PageCount += r.Next(MAX_PAGES - PageCount);
		}

		/// <summary>
		/// randomly add ink to InkCount
		/// </summary>
		/// 
		public void AddInk()
		{
			InkCount += r.Next((int)(MAX_INK - InkCount));
		}

        //--------------------------------------------- E V E N T S ------------------------------------------//
		/// <summary>
		/// increase font size of current printer name
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Label_MouseEnter(object sender, MouseEventArgs e)
		{
			Label l = sender as Label;
			if (l != null)
			{
				l.FontSize += 8; // increase font size of printer name
			}
		}

		/// <summary>
		/// decrease font size of current printer name
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Label_MouseLeave(object sender, MouseEventArgs e)
		{
			Label l = sender as Label;
			if (l != null)
			{
				l.FontSize -= 8;
			}
		}

		/// <summary>
		/// shows ink amount when mouse is over ProgressBar
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProgressBar_ToolTipOpening(object sender, ToolTipEventArgs e)
		{
			ProgressBar b = sender as ProgressBar;
			b.ToolTip = InkCount;
		}
		// ------------------------------------------------ end of E V E N T S ----------------------------------//
	}
}
