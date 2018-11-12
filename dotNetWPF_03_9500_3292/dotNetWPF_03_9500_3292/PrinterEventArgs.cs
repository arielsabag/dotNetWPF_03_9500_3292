using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetWPF_03_9500_3292
{
	class PrinterEventArgs : EventArgs
	{


		private bool criticalWarning;
		public bool CriticalWarning { get { return criticalWarning; } }

		private DateTime date_Time;
		public DateTime Date_Time { get { return date_Time; } }

		private string error_Warning_Message;
		public string Error_Warning_Message { get { return error_Warning_Message; } }

		private string printerName;
		public string PrinterName { get { return printerName; } }

		public PrinterEventArgs(string PrinterName, string Error_Warning_Message, bool CriticalWarning)
		{
			criticalWarning = CriticalWarning;
			date_Time = DateTime.Now;
			error_Warning_Message = Error_Warning_Message;
			printerName = PrinterName;
		}


	}
}
