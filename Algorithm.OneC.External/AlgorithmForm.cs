using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Algorithm.OneC.App;

namespace Algorithm.OneC.External
{
    [ComVisible(true)]
	[Guid("C46FCEF9-53FB-4BB5-B12B-B08DCEABE999"), ClassInterface(ClassInterfaceType.None), ComSourceInterfaces(typeof(IAlgorithmDispatch))]
	public class AlgorithmForm: IAlgorithmForm
    {
	    private MainWindow _window;

		public void Show()
		{
			_window = new MainWindow();
			_window.Show();
			
		}

		public int AddElement(int elementID, int elementType, string elementName, string fName,
				int actionType, int actionRepeat, int actionPriority, int actionNumber,
				int[] elementPrevIDs, int[] elementNextIDs, bool refresh = false)
		{
			return _window.AddElement(elementID,  elementType, elementName, fName, 
				actionType, actionRepeat, actionPriority, actionNumber,
				elementPrevIDs, elementNextIDs, refresh);
		}
    }
}
