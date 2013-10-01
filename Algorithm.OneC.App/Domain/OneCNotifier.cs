using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.OneC.App.Domain
{
	public class OneCNotifier
	{
		public void NotifyRelationshipCreation(AlgorithmElement source, AlgorithmElement target)
		{
			throw new NotImplementedException();
		}


		[DllImport("user32.dll")]
		public static extern int SendMessage(
						  int hWnd,      // handle to destination window
						  uint Msg,       // message
						  long wParam,  // first message parameter
						  long lParam   // second message parameter
						  );
	}
}
