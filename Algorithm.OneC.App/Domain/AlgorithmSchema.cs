using System.Collections.Generic;
using System.Linq;
using System.Windows.Shapes;

namespace Algorithm.OneC.App.Domain
{
	public class AlgorithmSchema
	{
		public List<AlgorithmElement> Elements { get; set; }

		//private List<AlgorithmElement> BackGroundElements { get { return _backGroundElements; } }

		private List<AlgorithmElement> _backGroundElements; 

		public AlgorithmSchema(IEnumerable<AlgorithmElement> elements)
		{
			if (elements != null)
			{
				Elements = new List<AlgorithmElement>(elements);
				//BackGroundElements = new List<AlgorithmElement>(elements);
			}
			else
			{
				Elements = new List<AlgorithmElement>();			
				//BackGroundElements = new List<AlgorithmElement>();			
			}
				
		}

		//public void Refresh()
		//{
		//	Elements.Clear();
		//	Elements = BackGroundElements.Select(c => (AlgorithmElement)c.Clone()).ToList();
		//	BackGroundElements.Clear();
		//}

		public void DeleteAllElements()
		{
			//BackGroundElements.Clear();
		}

		public void AddElement(AlgorithmElement element, bool refresh = false)
		{
			Elements.Add(element);
			//BackGroundElements.Add(element);
			//if (refresh)
			//	Elements = BackGroundElements;
		}
		

		public AlgorithmElement GetAlgorithmElementByUIElement(Shape element)
		{
			return Elements.FirstOrDefault(c => c.DrawnShape == element);
		}
	}
}
