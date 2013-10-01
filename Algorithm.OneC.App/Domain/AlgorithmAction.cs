using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Algorithm.OneC.App.Domain
{
	[Serializable]
	public class AlgorithmAction : AlgorithmElement
	{
		public override int Size
		{
			get { return 100; }
		}


		public string ActionName { get; set; }
		public int ActionType { get; set; }
		public int ActionRepeat { get; set; }
		public int ActionPriority { get; set; }

		public ElementColor Color { get; private set; }

		public AlgorithmAction(int elementId, int elementType, string actionName, int actionType, 
			int actionRepeat, int actionPriority, int[] prevElementIds, int[] nextElementIds)
			: base(elementId, elementType, prevElementIds, nextElementIds)
		{
			ActionName = actionName;
			ActionType = actionType;
			Color = (ElementColor)actionType;
			ActionRepeat = actionRepeat;
			ActionRepeat = actionPriority;
		}
		
		public override Shape Draw()
		{
			var shape = new Rectangle();
			shape.Width = shape.Height = Size;

			shape.Stroke = new SolidColorBrush(Colors.Black);

			shape.StrokeThickness = 2;

			FillShape(shape);		

			 // Add Rectangle to Canvas

			shape.AllowDrop = true;

			return shape;
		}

		private void FillShape(Shape shape)
		{
			switch (this.Color)
			{
				case ElementColor.Red:
					shape.Fill = new SolidColorBrush(Colors.Red);
					break;					
				case ElementColor.Blue:
					shape.Fill = new SolidColorBrush(Colors.Blue);
					break;										
			}
			
		}

	}
}
