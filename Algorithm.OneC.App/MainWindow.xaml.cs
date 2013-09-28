using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Algorithm.OneC.App.Domain;
using Algorithm.OneC.App.Drawing;
using Action = System.Action;

namespace Algorithm.OneC.App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private AlgorithmSchema _schema = new AlgorithmSchema(null);
		public MainWindow()
		{
			InitializeComponent();
			CreateSchema();
			//DrawSchema();
		}


		public int AddElement(int elementID, int elementType, string elementName, string fName, 
				int actionType, int actionRepeat, int actionPriority, int actionNumber,
				int[] elementPrevIDs, int[] elementNextIDs, bool refresh = true)
		{
			var type = (ElementType) elementType;
			switch (type)
			{
				case ElementType.Action:
					var action = new AlgorithmAction
						(elementID, elementType, elementName, actionType, actionRepeat, 
						actionPriority, elementPrevIDs, elementNextIDs);
					_schema.AddElement(action, refresh);	
					break;					
			}
			if (refresh)
				DrawSchema();
			return 0;
		}

		private void CreateSchema()
		{
			//var action1 = new AlgorithmAction(1, 100, "First action", 1, 0, 1, null, null);
			AddElement(1, (int)ElementType.Action, "First action", string.Empty, 1, 0, 1, 111, null, null);
			var action2PrevElementIds = new int[] {1};
			AddElement(2, (int)ElementType.Action, "Second action", string.Empty, 0, 1, 0, 222, action2PrevElementIds, null);
			//var schema = new AlgorithmSchema(new List<AlgorithmElement>(){action1, action2});
			//return schema;
		}


		public void DrawSchema()
		{
			double topPosition = 100;
			double leftPosition = 100;

			canvasArea.Children.Clear();
			//_schema.Refresh();
			//_schema.Elements = new List<AlgorithmElement>(_schema.);
			foreach (var element in _schema.Elements)
			{
				var elementToDraw = element.Draw();
				
				Canvas.SetLeft(elementToDraw, leftPosition);
				Canvas.SetTop(elementToDraw, topPosition);

				element.PrevArrowEnd = new Point(leftPosition, topPosition+(element.Size/2));
				element.NextArrowStart = new Point(leftPosition + element.Size, topPosition + (element.Size / 2));

				element.DrawnShape = elementToDraw;
				canvasArea.Children.Add(elementToDraw);

				//topPosition += 30;
				leftPosition += element.Size + 100;
			}

			foreach (var element in _schema.Elements)
			{
				if (element.PrevElementIds.Any())
				{
					foreach (var prevId in element.PrevElementIds)
					{
						var prevElement = _schema.Elements.Single(c => c.ElementId == prevId);
						DrawArrow(prevElement, element);
					}
				}
			}
		}

		private void DrawArrow(AlgorithmElement prevElement, AlgorithmElement element)
		{
			ArrowLine aline1 = new ArrowLine();
			aline1.Stroke = Brushes.Black;
			aline1.StrokeThickness = 3;

			aline1.X1 = prevElement.NextArrowStart.X;
			aline1.Y1 = prevElement.NextArrowStart.Y;
			aline1.X2 = element.PrevArrowEnd.X;
			aline1.Y2 = element.PrevArrowEnd.Y;
			canvasArea.Children.Add(aline1);
		}
	}
}
