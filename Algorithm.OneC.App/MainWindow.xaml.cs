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
using Microsoft.VisualStudio.GraphModel;
using Action = System.Action;

namespace Algorithm.OneC.App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private AlgorithmSchema _schema = new AlgorithmSchema(null);
		private double _topPosition;
		private double _leftPosition;

		public MainWindow()
		{
			InitializeComponent();
			CreateSchema();
			//DrawSchema();
		}

		#region Public methods, exposed in COM interface

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


		public void ClearForm()
		{
			canvasArea.Children.Clear();
			_topPosition = 100;
			_leftPosition = 100;
			
		}

		#endregion Public methods, exposed in COM interface

		#region Drag'n'Drop arrows stuff
		
		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			base.OnMouseMove(e);
			UIElement element = e.Source as UIElement;
			if (element == null)
				return;
			var dataObject = new DataObject(this);
			dataObject.SetData("Object", this);
			if (element is Shape)
			{
				var algorithmElement = _schema.GetAlgorithmElementByUIElement((Shape)element);
				dataObject.SetData("AlgorithmElement", algorithmElement);
				DragDrop.DoDragDrop(element, dataObject, DragDropEffects.Link);				
			}

		}


		protected override void OnDrop(DragEventArgs args)
		{			
			FrameworkElement targetUIElem = args.Source as FrameworkElement;
			if (targetUIElem == null)
				return;
			IDataObject data = args.Data;
			var sourceElement = data.GetData("AlgorithmElement", true) as AlgorithmElement;		
			if (sourceElement == null)
				return;
			var targetElement = _schema.GetAlgorithmElementByUIElement((Shape)targetUIElem);
			if (targetElement == null)
				return;		
			
			// ----- Actually do your stuff here -----
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

		#endregion Drag'n'Drop arrows stuff


		#region Private methods

		private void CreateSchema()
		{
			//var action1 = new AlgorithmAction(1, 100, "First action", 1, 0, 1, null, null);
			AddElement(1, (int)ElementType.Action, "First action", string.Empty, 1, 0, 1, 111, null, null);
			//var action2PrevElementIds = new int[] {1};
			var action2PrevElementIds = new int[]{};
			AddElement(2, (int)ElementType.Action, "Second action", string.Empty, 0, 1, 0, 222, action2PrevElementIds, null);
			//var schema = new AlgorithmSchema(new List<AlgorithmElement>(){action1, action2});
			//return schema;
		}

		private void DrawSchema()
		{
			ClearForm();
			//_schema.Refresh();
			//_schema.Elements = new List<AlgorithmElement>(_schema.);
			foreach (var element in _schema.Elements)
			{
				var elementToDraw = element.Draw();
				
				Canvas.SetLeft(elementToDraw, _leftPosition);
				Canvas.SetTop(elementToDraw, _topPosition);

				element.PrevArrowEnd = new Point(_leftPosition, _topPosition+(element.Size/2));
				element.NextArrowStart = new Point(_leftPosition + element.Size, _topPosition + (element.Size / 2));

				element.DrawnShape = elementToDraw;
				canvasArea.Children.Add(elementToDraw);

				//topPosition += 30;
				_leftPosition += element.Size + 100;
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

		#endregion Private elements		
	}
}
