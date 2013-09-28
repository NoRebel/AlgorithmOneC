using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Shapes;

namespace Algorithm.OneC.App.Domain
{
	[Serializable]
	public abstract class AlgorithmElement : ICloneable
	{
		public virtual int Size
		{
			get { return 0; }
		}

		public int ElementId { get; set; }
		public int ElementType { get; set; }
		
		public int[] PrevElementIds { get; set; }
		public int[] NextElementIds { get; set; }

		#region Graphical stuff

		public Shape DrawnShape { get; set; }
		public Point NextArrowStart { get; set; }
		public Point PrevArrowEnd { get; set; }

		#endregion Graphical stuff

		public AlgorithmElement(int elementId, int elementType, int[] prevElementIds, int[] nextElementIds)
		{
			ElementId = elementId;
			ElementType = elementType;
			PrevElementIds = prevElementIds ?? new int[]{};
			NextElementIds = nextElementIds ?? new int[]{};
		}

		public object Clone()
		{
			return null;

			//var result = new AlgorithmElement()
			//	{
			//		ElementId = this.ElementId,
			//		ElementType = this.ElementType,
			//		NextElementIds = new int[](this.NextElementIds),
			//		PrevElementIds = new List<int>(this.PrevElementIds)
			//	};

			//return result;


			//MemoryStream ms = new MemoryStream();
			//BinaryFormatter bf = new BinaryFormatter();

			//bf.Serialize(ms, this);

			//ms.Position = 0;
			//object obj = bf.Deserialize(ms);
			//ms.Close();

			//return obj as AlgorithmElement;
		}

		public abstract Shape Draw();
	}
}
