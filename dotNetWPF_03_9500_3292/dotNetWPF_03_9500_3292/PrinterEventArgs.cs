using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 
/// </summary>
namespace dotNetWPF_03_9500_3292
{
	class PrinterEventArgs : EventArgs
	{
		/// property
		private bool criticalWarning;
		public bool CriticalWarning { get { return criticalWarning; } }

		/// property
		private DateTime date_Time;
		public DateTime Date_Time { get { return date_Time; } }
		
		/// property
		private string error_Warning_Message;
		public string Error_Warning_Message { get { return error_Warning_Message; } }
		
		/// property
		private string printerName;
		public string PrinterName { get { return printerName; } }

		public PrinterEventArgs(string PrinterName, string Error_Warning_Message, bool CriticalWarning)
		{
			criticalWarning = CriticalWarning; // true or false
			date_Time = DateTime.Now; //current date and time
			error_Warning_Message = Error_Warning_Message;  // the warnning message
			printerName = PrinterName; // printer name
		}
	}
}
