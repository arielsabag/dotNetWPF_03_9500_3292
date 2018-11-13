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

			PrinterName = "Printer " + code++;

			//printer , printer 2 ...
			//pageCountSlider.Value = 50;
			AddInk();
			AddPages();
			DataContext = this;
			//Grid.DataContext = this;
		}
		PrinterEventArgs p;

		public delegate void EventHandler<PrinterEventArgs>(object sender, PrinterEventArgs e);
		public event EventHandler PageMissing;  // page missing  event
		public event EventHandler InkEmpty;  // ink empty event
		static Random r = new Random();      
		static int code = 1; // for each printer name

		/// <summary>
		/// constants values
		/// </summary>
		const double MAX_INK = 100;  
		const double MIN_ADD_INK = MAX_INK / 10;
		const double MAX_PRINT_INK = 70;
		const int MAX_PAGES = 400;
		const int MIN_ADD_PAGES = MAX_PAGES / 10;
		const int MAX_PRINT_PAGES = 300;
		// end of constants values



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

		private int pageCount;
		/// <summary>
		/// reveling the page amount
		/// </summary>
		public int PageCount
		{
        	get { return pageCount; }
			set{pageCount = value;}
		}
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
			CurrentAmountOfPageToPrint = r.Next(MAX_PAGES - pageCount);
			PageCount -= CurrentAmountOfPageToPrint;    //decrease page amount randomly 

			pageCountSlider.Value = PageCount; // update pageCountSlider to new amount of pages
			inkCountProgressBar.Value = InkCount;  // update inkCountProgressBar to new amount of ink

			if (PageCount <= 0) // if page over -> event
			{
				PageCount = 0;
				this.pageLabel.Foreground = System.Windows.Media.Brushes.Red;
				PageMissing(this, new PrinterEventArgs(PrinterName, "  Page Missing !!!", true));
			}
			if ((InkCount <= 15)&&(InkCount>=10))  // if ink over -> event
			{
				this.inkLabel.Foreground = System.Windows.Media.Brushes.Yellow;
				InkEmpty(this, new PrinterEventArgs(PrinterName, " Ink Empty", false));
			}
			if ((InkCount <= 10) && (InkCount >= 1))  // if ink over -> event
			{
				this.inkLabel.Foreground = System.Windows.Media.Brushes.Orange;
				InkEmpty(this, new PrinterEventArgs(PrinterName, " Ink Empty", false));
			}
			if (InkCount < 1)  // if ink over -> event
			{
				InkCount = 0;
				this.inkLabel.Foreground = System.Windows.Media.Brushes.Red;
				InkEmpty(this, new PrinterEventArgs(PrinterName, " Ink Empty", true));
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
		public void AddInk()
		{
			InkCount += r.Next((int)(MAX_INK - InkCount));
		}


        //-------------- E V E N T S---------------------//
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
	
	}
}
