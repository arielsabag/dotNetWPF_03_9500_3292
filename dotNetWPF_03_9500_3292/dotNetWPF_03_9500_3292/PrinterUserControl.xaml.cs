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
		public event EventHandler PageMissing;
		public event EventHandler InkEmpty;
		static Random r = new Random();
		static int code = 1;
		const double MAX_INK = 100;
		const double MIN_ADD_INK = MAX_INK / 10;
		const double MAX_PRINT_INK = 70;
		const int MAX_PAGES = 400;
		const int MIN_ADD_PAGES = MAX_PAGES / 10;
		const int MAX_PRINT_PAGES = 300;

		/// <summary>
		/// reveling the printer name
		/// </summary>
		public string PrinterName
		{
			get;
			set;


		}
		/// <summary>
		/// reveling the ink amount
		/// </summary>
		public double InkCount
		{
			get;
			set;
		}
		private int pageCount;
		/// <summary>
		/// reveling the page amount
		/// </summary>
		public int PageCount
		{
			get { return pageCount; }
			set
			{
				pageCount = value;
			}
		}

		/// <summary>
		/// illustrate printing - decrease page count by ranndom number
		/// </summary>
		public void Print()
		{

			InkCount -= r.Next((int)(MAX_INK - InkCount));
			PageCount -= r.Next(MAX_PAGES - pageCount);

			pageCountSlider.Value = PageCount;
			inkCountProgressBar.Value = InkCount;
			if (PageCount <= 0)
			{
				PageCount = 0;
				PageMissing(this, new PrinterEventArgs(PrinterName, "page over", true));
			}
			if (InkCount <= 0)
			{
				InkCount = 0;
				InkEmpty(this, new PrinterEventArgs(PrinterName, "Ink Empty", true));
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
		private void Label_MouseEnter(object sender, MouseEventArgs e)
		{
			Label l = sender as Label;
			if (l != null)
			{
				l.FontSize += 8;
			}
		}
		private void Label_MouseLeave(object sender, MouseEventArgs e)
		{
			Label l = sender as Label;
			if (l != null)
			{
				l.FontSize -= 8;
			}
		}
		private void ProgressBar_ToolTipOpening(object sender, ToolTipEventArgs e)
		{
			ProgressBar b = sender as ProgressBar;
			b.ToolTip = InkCount;
		}
		private void Slider_ToolTipOpening(object sender, ToolTipEventArgs e)
		{
			Slider s = sender as Slider;
			s.ToolTip = PageCount;
		}
	}
}
