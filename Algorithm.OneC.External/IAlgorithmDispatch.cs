using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Algorithm.OneC.External
{
	[ComVisible(true)]
	[Guid("178F16A6-7242-46BE-B0EE-927299A91F30")]	
	internal interface IAlgorithmForm
	{
		[DispId(1)]
		void Show();

		[DispId(2)]
		int AddElement(int elementID, int elementType, string elementName, string fName,
		               int actionType, int actionRepeat, int actionPriority, int actionNumber,
		               int[] elementPrevIDs, int[] elementNextIDs, bool refresh);
	}

	[ComVisible(true)]
	[Guid("59843053-E4B6-491D-B3CB-2B55E15C82D7"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface IAlgorithmDispatch
	{
	}
}
