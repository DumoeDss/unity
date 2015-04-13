using System;
using FullInspector;
using Kitware.VTK;
using UnityEngine;
using tk = FullInspector.tk<UFZ.VTK.Threshold>;

namespace UFZ.VTK
{
	public class Threshold : VtkAlgorithm
	{
		public Vector2 Range
		{
			get
			{
				var threshold = (vtkThreshold) Algorithm;
				return new Vector2((float)threshold.GetLowerThreshold(),
					(float)threshold.GetUpperThreshold());
			}
			set
			{
				if (value.x > value.y)
					return;
				_range = value;
				((vtkThreshold) Algorithm).ThresholdBetween(value.x, value.y);
			}
		}

		[SerializeField] private Vector2 _range;

		protected override void Initialize()
		{
			base.Initialize();

			if (Algorithm == null)
				Algorithm = vtkThreshold.New();
			var threshold = (vtkThreshold) Algorithm;

			Name = "Threshold";
			InputDataType = DataType.vtkUnstructuredGrid;
			OutputDataDataType = (DataType) Enum.Parse(typeof (DataType),
				Algorithm.GetOutputPortInformation(0).Get(vtkDataObject.DATA_TYPE_NAME()));

			Range = _range;

			SaveState();
		}

		public override tkControlEditor GetEditor()
		{
			var parentEditor = base.GetEditor();
			return new tkControlEditor(
				new tk.VerticalGroup
				{
					new tkTypeProxy<VtkAlgorithm, tkEmptyContext, Threshold, tkEmptyContext>(
						(tkControl<VtkAlgorithm, tkEmptyContext>) parentEditor.Control),
					new tk.Label(new fiGUIContent("Algorithm Properties")),
					new tk.PropertyEditor("Range")
				});
		}
	}
}
